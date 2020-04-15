using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RapidFireLib.Lib.Core
{
    public class Dynamic
    {
        public object GetInstance(Type classType, Type genericClassType = null, params object[] methodParameters)
        {
            if (classType.IsGenericType) classType = classType.MakeGenericType(genericClassType);
            return Activator.CreateInstance(classType, methodParameters);
        }

        public object GetInstance(string strFullyQualifiedName, Type genericClassType = null, params object[] methodParameters)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
            {
                if (type.IsGenericType) type = type.MakeGenericType(genericClassType);
                return Activator.CreateInstance(type, methodParameters);
            }
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                {
                    if (type.IsGenericType) type = type.MakeGenericType(genericClassType);
                    return Activator.CreateInstance(type, methodParameters);
                }
            }
            return null;
        }

        public object InvokeMethod(string methodName, object targetClass, Type genericClassType = null, Type[] methodParameterType = null, params object[] methodParameters)
        {
            MethodInfo method = null;
            if (methodParameterType == null) method = targetClass.GetType().GetMethod(methodName, new Type[] { });
            else method = targetClass.GetType().GetMethod(methodName, methodParameterType);
            if (method.IsGenericMethod) method = method.MakeGenericMethod(genericClassType);
            if (method == null) throw new MissingMethodException($"Unable to invoke method :- {methodName}");
            var returnValue = method.Invoke(targetClass, methodParameters);
            return returnValue;
        }

        public object Repository(object dynamicModel, object context, DBOperations dbOperation, Type[] methodParameterType = null, params object[] methodParameters)
        {
            object ret = null;
            string[] dboperation = new string[] { "Insert", "Update", "Delete", "Get" };
            Type genericTypeArguments = null;
            if (dynamicModel.GetType().IsGenericType)
                genericTypeArguments = dynamicModel.GetType().GetGenericArguments()[0];
            var targetClass = GetInstance(GetRepositoryBase((DbContext)context), genericTypeArguments?? dynamicModel.GetType(), context);
            switch (dbOperation)
            {
                case DBOperations.INSERT:
                    ret = InvokeMethod(dboperation[(int)dbOperation], targetClass, dynamicModel.GetType(), methodParameterType, methodParameters);
                    break;
                case DBOperations.DELETE:
                    ret = InvokeMethod(dboperation[(int)dbOperation], targetClass, dynamicModel.GetType(), methodParameterType, methodParameters);
                    break;
                case DBOperations.UPDATE:
                    ret = InvokeMethod(dboperation[(int)dbOperation], targetClass, dynamicModel.GetType(), methodParameterType, methodParameters);
                    break;
                case DBOperations.SELECT:
                    ret = InvokeMethod(dboperation[(int)dbOperation], targetClass, dynamicModel.GetType(), methodParameterType, methodParameters);
                    break;
            }
            return ret;
        }

        internal string GetFieldValue(object model, string name)
        {
            return this.GetPropValue(model, name)?.ToString();
        }

        public Type GetRepositoryBase(DbContext context)
        {
            Type type = null;
            string connectionType = context.Database.ProviderName;
            if (connectionType.Equals("Microsoft.EntityFrameworkCore.SqlServer")) type = typeof(RepositoryMSSQL<>);
            else if (connectionType.Equals("SQLiteConnection")) type = null;// typeof(RepositorySQLite<>);
            return type;
        }

        public void DynamicRepo(object dynamicModel, object context, DBOperations dbOperation)
        {
            string[] dboperation = new string[] { "Insert", "Update", "Delete" };
            var dynamicRepo = typeof(RepositoryMSSQL<>);
            var dynamicObject = dynamicRepo.MakeGenericType(dynamicModel.GetType());
            var obj = Activator.CreateInstance(dynamicObject, context);
            //upadte 
            /// MethodInvoker(dboperation[(int)dbOperation], obj, dynamicModel, true);
        }


        //var dynamicRepo = typeof(Db);
        //var obj = Activator.CreateInstance(dynamicRepo, context);
        //MethodInfo method = obj.GetType().GetMethod("Get1");
        //var dynamicObject = method.MakeGenericMethod(dynamicModel.GetType());
        //var returnValue = dynamicObject.Invoke(obj, new object[] { "select "});//obj?
        //public void DynamicRepoSH(object dynamicModel, object context) //non t class t method 
        //{
        //    var targetClass1 = GetInstance(typeof(testcls<>), dynamicModel.GetType(), context);
        //    var targetClass = GetInstance(GetFullyQualifiedPath("Student"), dynamicModel.GetType());
        //    var ret = InvokeMethod("m2", targetClass, dynamicModel.GetType(), "select");
        //}

        public void DynamicRepoSH1(object dynamicModel, object context) //non t class t method 
        {
            var dynamicRepo = typeof(Db);
            var obj = Activator.CreateInstance(dynamicRepo, context);
            MethodInfo method = obj.GetType().GetMethod("Get1");
            var dynamicObject = method.MakeGenericMethod(dynamicModel.GetType());
            var returnValue = dynamicObject.Invoke(obj, new object[] { "select " });//obj?
            //MethodInvoker("Get", obj, dynamicModel, true);
        }

        public IEnumerable<object> DynamicSelectWithUUID(object dynamicModel, object context, object modelData)
        {
            string[] dboperation = new string[] { "Insert", "Update", "Delete", "ShowList" };
            var dynamicRepo = typeof(ObfuscateAssemblyAttribute/*JGridHelper<>*/);
            var dynamicObject = dynamicRepo.MakeGenericType(dynamicModel.GetType());
            var obj = Activator.CreateInstance(dynamicObject);
            var tableName = GetPropValue(modelData, "TableName").ToString();
            var uuid = GetPropValue(modelData, "UUID").ToString();
            string sql = string.Format(@"SELECT * FROM {0} WHERE UUID = '{1}'", tableName, uuid);

            var res = (IList)InvokeMethod(dboperation[3], obj, null, null, new object[] { sql });
            IEnumerable<object> list = res.Cast<object>();
            return list;
        }

        public object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public Type GetTypeByClassName(string className)
        {
            Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();
            Type item = null;
            for (int i = 0; i < asm.Length; i++)
            {
                item = asm[i].GetTypes().Where(x => x.Name == className).FirstOrDefault();
                if (item != null)
                {
                    break;
                }
            }
            return item;
        }

        public string GetFullyQualifiedPath(string className, string root = "")
        {
            Assembly[] asm = null;
            string path = "";
            if (!string.IsNullOrEmpty(root))
                asm = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(root)).ToArray();
            else
                asm = AppDomain.CurrentDomain.GetAssemblies();
            Type item = null;
            for (int i = 0; i < asm.Length; i++)
            {
                item = asm[i].GetTypes().FirstOrDefault(x => x.Name == className);
                //item = asm[i].GetTypes().Where(x => x.Name == className).FirstOrDefault();
                if (item != null)
                {
                    path = item.AssemblyQualifiedName.Split(',').FirstOrDefault();
                    break;
                }
            }
            return path;
        }
        
        //public PropertyInfo GetPrimaryKeyInfo<T>()
        //{
        //    PropertyInfo[] properties = typeof(T).GetProperties();
        //    foreach (PropertyInfo pI in properties)
        //    {
        //        System.Object[] attributes = pI.GetCustomAttributes(true);
        //        foreach (object attribute in attributes)
        //        {
        //            if (attribute.GetType().Name == "KeyAttribute")
        //            {
        //                return pI;
        //            }

        //        }
        //    }
        //    return null;
        //}

    }
}


/*
 <configuration>
        <connectionStrings>
            <add name="database1" connectionString="Server=localhost;Integrated Security=SSPI;Database=Northwind;" providerName="System.Data.SqlClient" />
            <add name="database2" connectionString="Data Source=|DataDirectory|\Database.sdf" providerName="System.Data.SqlServerCe.3.5" />
            <add name="database3" connectionString="Data Source=|DataDirectory|\Database.sqlite;Version=3;" providerName="System.Data.SQLite" />
            <add name="database4" connectionString="server=localhost;user id=myusername;password=mypassword;persist security info=True;database=mydatabasename;CharSet='utf8';" providerName="MySql.Data.MySqlClient" />
            <add name="database5" connectionString="SERVER=localhost;Database=mydatabasename;User name=myusername;Password=mypassword" providerName="Npgsql2" />
            <add name="database6" connectionString="Server=localhost;User=myusername;Password=mypassword;Charser=NONE;Database=C:\Database.fdb" providerName="FirebirdSql.Data.FirebirdClient" />
            <add name="database7" connectionString="encrypted:0jx0NNG6POnEZ4/5VKXfeUj0u5WhEa9AEdPx7mYrIiFGmPNPJw8dVZvrcc8gjuy35mz/lt8M2s4e9dQFXHZzgQ##" providerName="System.Data.SqlClient" />
        </connectionStrings>
    </configuration>

*/
