using Newtonsoft.Json;
using RapidFireLib.Models;
using RapidFireLib.UX.JGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Core
{

    public static class Tools
    {
        static Configuration ConfigTool = new Configuration();
        public static object DictionaryToModel(this Dictionary<string,string> requestSourceDictionary,object destinationModel)
        {
            foreach (var item in requestSourceDictionary)
            {
                var destinationType = destinationModel.GetType();
                var destinationField = destinationType.GetProperty(item.Key);
                if (destinationField != null)
                {
                    Type fieldType = Nullable.GetUnderlyingType(destinationField.PropertyType) ?? destinationField.PropertyType;
                    var fieldValue = (item.Value == null || item.Value == "") ? null : Convert.ChangeType(item.Value, fieldType);
                    destinationField.SetValue(destinationModel, fieldValue, null);
                }
            }
            return destinationModel;
        }
        public static void CopyClass(this object Destination, object Source)
        {
            Type srcT = Source.GetType();
            Type dstT = Destination.GetType();
            foreach (System.Reflection.FieldInfo f in srcT.GetFields())
            {
                System.Reflection.FieldInfo dstF = dstT.GetField(f.Name);
                if (dstF == null)
                {
                    continue;
                }

                dstF.SetValue(Destination, f.GetValue(Source));
            }

            foreach (System.Reflection.PropertyInfo f in srcT.GetProperties())
            {
                System.Reflection.PropertyInfo dstF = dstT.GetProperty(f.Name);
                if (dstF == null)
                {
                    continue;
                }

                if (!(f.Name.Contains("PacketList")) && !(f.Name.Contains("Packet")))
                {
                    dstF.SetValue(Destination, f.GetValue(Source, null), null);
                }
            }
        }

        public static void CopyClass(this object Destination, Dictionary<string, string> Source)
        {
            foreach (KeyValuePair<string, string> item in Source)
            {
                Type destinationType = Destination.GetType();
                System.Reflection.PropertyInfo destinationField = destinationType.GetProperty(item.Key);
                Type fieldType = Nullable.GetUnderlyingType(destinationField.PropertyType) ?? destinationField.PropertyType;
                object fieldValue = (item.Value == null) ? null : Convert.ChangeType(item.Value, fieldType);
                destinationField.SetValue(Destination, fieldValue, null);
            }
        }

        public static string EmailSignature()
        {
            string signature = "";
            signature += "<br /><span style=\"font-family:Arial;font-size:12px;\"><b>" + "ONDESK SERVICE" +
                "</b>|<span style=\"color: red;\"> Save the Children in Bangladesh</span> | " +
                "<br />House CWN (A) 35, Road 43, Gulshan 2, Dhaka 1212, Bangladesh <br />";
            signature += " https://bangladesh.savethechildren.net/<br />";
            signature += @"Tel: +88-02-882 8081, Ext. 1065 | Fax: +88-02-881 2523 <br /></span>";
            return signature;
        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;

        }

        public static bool HasAccess(string modelName, DBOperations dBOperations, bool IsAccessPermission)
        {
            if (!IsAccessPermission)
            {
                return true;
            }

            ModelAccess ma = ConfigLib.ModelAccess.Find(m => m.ModelName == modelName);  //Remove Static
            switch (dBOperations)
            {
                case DBOperations.INSERT: return ma.IsAdd;
                case DBOperations.UPDATE: return ma.IsUpdate;
                case DBOperations.DELETE: return ma.IsDelete;
                case DBOperations.SELECT: return ma.IsRead;
                default: return false;
            }
        }

        public static AuditTrail LogAuditTrail(object model, DBOperations OperationType)
        {
            AuditTrail at = new AuditTrail()
            {
                ModelName = model.GetType().IsGenericType ? model.GetType().GetGenericArguments()[0].Name : model.GetType().Name,
                OperationType = OperationType.ToString(),
                UserId = ConfigTool.APP.User != null ? ConfigTool.APP.UserId.ToString() : GetFieldValue(model, "UserId").ToString(),
                AuditDate = System.DateTime.Now
            };
            return at;
        }

        internal static string GetFieldValue(object model, string name)
        {
            return GetPropValue(model, name)?.ToString();
        }
        internal static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static MulticastDelegate Expression<T>(Func<T, bool> func) where T : class
        {
            MulticastDelegate multicastDelegate = func;
            return multicastDelegate;
        }

        public static T GetFilter<T>(JGridRequest packet) where T : class, new()
        {
            if (packet.Data != null && packet.RequestType != "Update")
            {
                return JsonConvert.DeserializeObject<T>(packet.Data.ToString());
            }
            else
            {
                return new T();
            }
        }

        public static string RenderView(string viewName, bool partial = true)
        {
            RenderViewHelper renderViewHelper = new RenderViewHelper();
            Tuple<string, HttpContextItems> renderSupport = renderViewHelper.CoreItemsForRenderView(viewName);
            var result = renderViewHelper.ViewHtmlGenerator(renderSupport.Item1, renderSupport.Item2, partial);
            return result.Result;
        }

        public static async Task<string> RenderViewAsync(string viewName, bool partial = true)
        {
            return await Task.Run<string>(() => RenderView(viewName, partial));
        }
    }
}
