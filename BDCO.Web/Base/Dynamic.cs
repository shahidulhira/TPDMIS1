using BDCO.Domain;
using BDCO.Repository;
using BDCO.Web.Utility.JGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace BDCO.Web
{
    public partial class WebBase
    {
        public struct Dynamic
        {
            public static object GetInstance(string strFullyQualifiedName)
            {
                Type type = Type.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetType(strFullyQualifiedName);
                    if (type != null)
                        return Activator.CreateInstance(type);
                }
                return null;
            }

            public static T CreateInstance<T>() where T : class
            {
                return null;
            }

            private static object MethodInvoker(string methodName, object obj, params object[] parameters)
            {
                Type t = obj.GetType();
                MethodInfo method = null;
                if (methodName == "Delete")
                {
                    method = t.GetMethod(methodName, new Type[] { });
                }
                else
                {
                    method = t.GetMethod(methodName);
                }
                var returnValue = method.Invoke(obj, parameters);
                return returnValue;
            }
            public static void DynamicRepo(object dynamicModel, object context, DBOperations dbOperation)
            {
                string[] dboperation = new string[] { "Insert", "Update", "Delete"};
                var dynamicRepo = typeof(GenericRepository<>);
                var dynamicObject = dynamicRepo.MakeGenericType(dynamicModel.GetType());
                var obj = Activator.CreateInstance(dynamicObject, context);

                // Need to Introduce API Helper class LIKE JGridHelper
                if((int)dbOperation == 1)//// Update Using GenericRepository Update Function it should be transfer to API Helpter
                    MethodInvoker(dboperation[(int)dbOperation], obj, new object[] { dynamicModel, true });
                else if ((int)dbOperation == 2) //// Delete Using JGridHelper EditUpdate Function It shound it transfer to API Helpter
                {
                    MethodInvoker("DeleteByEntity", obj, dynamicModel);
                    //MethodInvoker(dboperation[(int)dbOperation], obj, dynamicModel);
                    //dynamicRepo = typeof(JGridHelper<>);
                    //dynamicObject = dynamicRepo.MakeGenericType(dynamicModel.GetType());
                    //obj = Activator.CreateInstance(dynamicObject);
                    ////obj = Activator.CreateInstance(dynamicObject, context);

                    //JGridRequest packet = new JGridRequest() { Data = new JavaScriptSerializer().Serialize(dynamicModel).ToString(), RecordType = "Master" };
                    //MethodInvoker("EditDelete", obj, new object[] { 2, packet, null, 0 } );
                }
                else //// Insert Using GenericRepository Insert Function It shound it transfer to API Helpter
                    MethodInvoker(dboperation[(int)dbOperation], obj, dynamicModel);
            }

            public static IEnumerable<object> DynamicSelectWithUUID(object dynamicModel, object context, object modelData)
            {
                string[] dboperation = new string[] { "Insert", "Update", "Delete", "ShowList" };
                var dynamicRepo = typeof(Utility.JGrid.JGridHelper<>);
                var dynamicObject = dynamicRepo.MakeGenericType(dynamicModel.GetType());
                var obj = Activator.CreateInstance(dynamicObject);
                var tableName = GetPropValue(modelData, "TableName").ToString();
                var uuid = GetPropValue(modelData, "UUID").ToString();
                string sql = string.Format(@"SELECT * FROM {0} WHERE UUID = '{1}'", tableName, uuid);

                var res = (IList) MethodInvoker(dboperation[3], obj, new object[] { sql });
                IEnumerable<object> list = res.Cast<object>();
                return list;
            }

            public static IEnumerable<object> DynamicSelectWithPaging(object dynamicModel, object modelData, int pageNo, int pageSize)
            {
                string[] dboperation = new string[] { "Insert", "Update", "Delete", "ShowList" };
                var dynamicRepo = typeof(Utility.JGrid.JGridHelper<>);
                var dynamicObject = dynamicRepo.MakeGenericType(dynamicModel.GetType());
                var obj = Activator.CreateInstance(dynamicObject);
                var tableName = GetPropValue(modelData, "ModelName").ToString();
                var where = GetPropValue(modelData, "Where") != null ? GetPropValue(modelData, "Where").ToString() : "" ;
                //var uuid = GetPropValue(modelData, "UUID").ToString();
                //string sql = string.Format(@"SELECT * FROM {0} WHERE UUID = '{1}'", tableName, uuid);
                where = where.Replace("\'", "\'\'");
                string sql = string.Format(@"EXEC GetDataWithPaging '{0}','{1}','{2}','{3}'",tableName,where,pageNo,pageSize);
                var res = (IList)MethodInvoker(dboperation[3], obj, new object[] { sql});
                IEnumerable<object> list = res.Cast<object>();
                return list;
            }

            public static int DynamicSelectTotalRecortCount(object dynamicModel, object modelData)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var tableName = GetPropValue(modelData, "ModelName").ToString();
                var where = GetPropValue(modelData, "Where").ToString();
                string sql = string.Format(@"EXEC GetDataWithPagingTotalCount '{0}','{1}'", tableName, where);
                var res = unitOfWork.context.Database.SqlQuery<int>(sql).FirstOrDefault();         
                return res;
            }

            public static object GetPropValue(object src, string propName)
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }

            public static string GetFullyQualifiedPath(string modelName)
            {
                string path = "";            
                List<Assembly> asmList = AppDomain.CurrentDomain.GetAssemblies().Where(x=>x.FullName.Contains("BDCO")).ToList();
                Type item = null;
                foreach (var asm in asmList)
                {
                    //item = asm.DefinedTypes.FirstOrDefault(x => x.Name == modelName).GetType();
                    item = asm.GetTypes().FirstOrDefault(x => x.Name == modelName);
                    if (item != null)
                    {
                        path = item.AssemblyQualifiedName.Split(',').FirstOrDefault();
                        break;
                    }
                }
                return path;
                
            }           

            public static Type GetObjectType(string modelName)
            {
                List<Assembly> asmList = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("BDCO")).ToList();
                Type item = null;
                foreach (var asm in asmList)
                {
                    item = asm.GetTypes().FirstOrDefault(x => x.Name == modelName);
                    if(item != null)
                    {
                        break;
                    }                   
                }
                return item;
            }
        }

        public enum DBOperations
        {
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            SELECT = 3
        }

        public class GetDataPacket
        {
            public string ModelName { get; set; }
            public string Where { get; set; }
        }
    }
}