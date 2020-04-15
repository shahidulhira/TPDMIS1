using BDCO.Domain;
using BDCO.Domain.Models.Systems;
using BDCO.Domain.Utility;
using BDCO.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace BDCO.Web.Utility.JGrid
{
    public class JGridHelper<T> where T : class
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        UtilityHelper _utilityHelper = new UtilityHelper();
        public object EditDelete(int operationType, JGridRequest packet, Assembly[] asm)
        {
            string tableName = typeof(T).Name;
            var pk = GetPrimaryKeyInfo<T>();
            var pkType = pk.PropertyType;
            //List<DataVerificationLog> lstverification = new List<DataVerificationLog>(); //// For log
            if (operationType == 1)
            {
                var requestSourceDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(packet.Data.ToString());
                var ids = packet.RecordId.Split(',');
                List<T> list = new List<T>();
                //requestSourceDictionary.Remove("PK");

                foreach (var recordId in ids)
                {

                    var exist = unitOfWork.GenericRepositories<T>().GetById(Convert.ChangeType(recordId, pkType));
                    foreach (var item in requestSourceDictionary)
                    {
                        var destinationType = exist.GetType();
                        var destinationField = destinationType.GetProperty(item.Key);
                        if (destinationField != null)
                        {
                            Type fieldType = Nullable.GetUnderlyingType(destinationField.PropertyType) ?? destinationField.PropertyType;
                            var fieldValue = (item.Value == null || item.Value == "") ? null : Convert.ChangeType(item.Value, fieldType);
                            destinationField.SetValue(exist, fieldValue, null);
                        }
                    }


                    list.Add(exist);
                    //// For log
                    //InsertVerificationLog(packet.ModelName, "", pk.Name, Convert.ToInt64(recordId), Convert.ToInt32(exist.GetPropertyValue("UserId") ?? 0), Convert.ToInt32(exist.GetPropertyValue("ModifiedBy") ?? 0), packet.RecordType);
                    ////End

                }
                unitOfWork.GenericRepositories<T>().UpdateAll(list);
            }
            else if (operationType == 2)
            {
                if (packet.RecordType == "Master")
                {
                    var childList = GetChildTableList<T>();
                    var query = unitOfWork.GenericRepositories<T>().GetAll().Where($"{pk.Name} = {packet.RecordId}");
                    //Child Include Start
                    foreach (var childitem in childList.ToList())
                    {
                        query = query.Include(childitem);
                    }
                    //Child Include End
                    var existingMasterWithChild = query.FirstOrDefault();

                    Type item = null;
                    foreach (var childListItem in childList)
                    {
                        for (int i = 0; i < asm.Length; i++)
                        {
                            item = asm[i].GetTypes().Where(x => x.Name == childListItem).FirstOrDefault();
                            if (item != null)
                            {
                                var jgridType = typeof(Repository<>);
                                var jgridObject = jgridType.MakeGenericType(item);
                                var obj = Activator.CreateInstance(jgridObject, unitOfWork.context);
                                var childs = (IEnumerable<object>)existingMasterWithChild.GetPropertyValue(childListItem);

                                ////For log
                                foreach (var childItem in childs)
                                {
                                    var primaryKey = _utilityHelper.GetPrimaryKeyInfo(childItem.GetType()).Name.ToString();
                                    //InsertVerificationLog("", childItem.GetType().Name, primaryKey, Convert.ToInt64(childItem.GetPropertyValue(primaryKey) ?? 0), Convert.ToInt32(childItem.GetPropertyValue("UserId") ?? 0), Convert.ToInt32(childItem.GetPropertyValue("ModifiedBy") ?? 0), packet.RecordType);
                                }
                                //// End

                                MethodInvoker("DeleteAll", obj, new object[] { childs });


                                break;
                            }
                        }


                    }

                    //Master Table Data Delete                       
                    unitOfWork.GenericRepositories<T>().Delete(existingMasterWithChild);

                    // For log
                    var PK = _utilityHelper.GetPrimaryKeyInfo(existingMasterWithChild.GetType()).Name.ToString();
                    //InsertVerificationLog("", existingMasterWithChild.GetType().Name, PK, Convert.ToInt64(existingMasterWithChild.GetPropertyValue(PK) ?? 0), Convert.ToInt32(existingMasterWithChild.GetPropertyValue("UserId") ?? 0), Convert.ToInt32(existingMasterWithChild.GetPropertyValue("ModifiedBy") ?? 0), packet.RecordType);
                    //// End

                }
                else
                {
                    var exist = unitOfWork.GenericRepositories<T>().GetById(Convert.ChangeType(packet.RecordId, pkType));
                    unitOfWork.GenericRepositories<T>().Delete(exist);

                    //// For log
                    //InsertVerificationLog("", packet.ModelName, pk.Name, Convert.ToInt64(packet.RecordId), Convert.ToInt32(exist.GetPropertyValue("UserId") ?? 0), Convert.ToInt32(exist.GetPropertyValue("ModifiedBy") ?? 0), packet.RecordType);
                    //// End
                }
            }

            unitOfWork.SaveChange();
            return "Operation Succeeded.";

        }

        public object Insert(JGridRequest packet, Assembly[] asm, Type item)
        {
            string tableName = typeof(T).Name;
            var pk = GetPrimaryKeyInfo<T>();
            var pkType = pk.PropertyType;

            var requestSourceDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(packet.Data.ToString());
            var obj = Activator.CreateInstance(item);
            foreach (var items in requestSourceDictionary)
            {
                var destinationType = obj.GetType();
                var destinationField = destinationType.GetProperty(items.Key);
                Type fieldType = Nullable.GetUnderlyingType(destinationField.PropertyType) ?? destinationField.PropertyType;
                var fieldValue = (items.Value == null || items.Value == "") ? null : Convert.ChangeType(items.Value, fieldType);
                destinationField.SetValue(obj, fieldValue, null);
            }
            unitOfWork.GenericRepositories<T>().Insert((T)obj);
            unitOfWork.SaveChange();
            return "Operation Succeeded.";

        }
        private object MethodInvoker(string methodName, object obj, params object[] parameters)
        {
            Type t = obj.GetType();
            MethodInfo method = t.GetMethod(methodName);
            var returnValue = method.Invoke(obj, parameters);
            return returnValue;
        }
        private PropertyInfo GetPrimaryKeyInfo<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pI in properties)
            {
                System.Object[] attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute.GetType().Name == "KeyAttribute")
                    {
                        return pI;
                    }

                }
            }
            return null;
        }
        private List<string> GetChildTableList<T>()
        {
            List<string> arr = new List<string>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            var propertyList = properties.Where(p => p.PropertyType.Name.Contains("List"));

            foreach (var item in propertyList)
            {
                PropertyInfo[] property = unitOfWork.context.GetType().GetProperties();
                foreach (var citem in property)
                {
                    if (citem.Name == item.Name)
                        arr.Add(citem.Name);
                }
            }
            return arr;
        }
        public List<T> ShowList(string sqlQuery = "")
        {
            return unitOfWork.GenericRepositories<T>().GetRecordSet(sqlQuery).ToList();
        }
        public T Show(JGridRequest packet)
        {
            var pk = GetPrimaryKeyInfo<T>();
            var pkType = pk.PropertyType;
            return unitOfWork.GenericRepositories<T>().GetById(Convert.ChangeType(packet.RecordId, pkType));
        }
        public string Export(string sqlQuery = "")
        {
            var dt = unitOfWork.GetDataTable(sqlQuery);
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            return sb.ToString();
        }

        public T GetVerifyInfo(JGridRequest packet, string pkName)
        {
            var sql = string.Format(@"SELECT ISNULL(ud.FullName, '') as UserName,
                                        d.EntryDate as DataCollectionDate
                                        ,ISNULL(d.IsVerified,0) as IsVerified
                                        ,ISNULL(u.FullName,'') as VerifierName
                                        ,d.VerificationDate as VerificationDate
                                        ,ISNULL(d.VerificationNote,'') as VerificationNote
                                        FROM {0} d
                                        LEFT JOIN AspNetUsers u on u.UserId = d.VerifiedBy
                                        LEFT JOIN AspNetUsers ud on u.UserId = d.UserId
                                        WHERE d.{1} = '{2}'", packet.ModelName, pkName, packet.RecordId);
            return unitOfWork.GenericRepositories<T>().GetRecordSet(sql).FirstOrDefault();
        }
        public ChartData ChartData(string sqlQuery = "")
        {
            ChartData chartData = new ChartData();
            var dataTable = unitOfWork.GetDataTable(sqlQuery);
            List<string> categories = new List<string>();
            List<SeriesObjects> series = new List<SeriesObjects>();

            foreach (DataRow row in dataTable.Rows)
                categories.Add((string)row[0]);


            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                SeriesObjects seriesObjects = new SeriesObjects();
                seriesObjects.name = dataTable.Columns[i].ColumnName;
                List<string> categoryValue = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                    categoryValue.Add((string)row[i]);

                seriesObjects.data = categoryValue;
                series.Add(seriesObjects);
            }
            chartData.Categories = categories;
            chartData.Series = series;
            return chartData;
        }

        //public void InsertVerificationLog(string updatedFrom, string deletedFrom, string primaryKey, long id, int notifiedTo, int notifiedBy, string RecordType)
        //{
        //    var aLog = new DataVerificationLog
        //    {
        //        RecordUpdatedFrom = updatedFrom == "" ? null : updatedFrom,
        //        RecordDeletedFrom = deletedFrom == "" ? null : deletedFrom,
        //        PrimaryKey = primaryKey,
        //        Id = id,
        //        NotifiedTo = notifiedTo,
        //        NotifiedBy = notifiedBy,
        //        IsClientUpdated = 0,
        //        RecordType = RecordType == "Details" ? 2 : 1
        //    };
        //    unitOfWork.GenericRepositories<DataVerificationLog>().Insert(aLog);
        //}
    }
    public static class PropertyHandler
    {
        /// <summary>
        /// Get Property Value using Property Name
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object src, string propertyName)
        {
            if (src == null) throw new ArgumentNullException($"Source object can not be null");
            return src.GetType().GetProperty(propertyName)?.GetValue(src, null);
        }
        /// <summary>
        /// Set Property Value using property name
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this object src, string propertyName, object value)
        {
            if (src == null) throw new ArgumentNullException($"Source object can not be null");
            PropertyInfo propertyInfo = src.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
                propertyInfo.SetValue(src, value, null);
            else
                throw new ArgumentException($"No Property Found With This Name {propertyName}");
        }

    }


}
