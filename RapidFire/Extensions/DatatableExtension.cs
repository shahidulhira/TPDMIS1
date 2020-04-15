using RapidFireLib.Lib.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RapidFireLib.Extensions
{
    public static class Extensions
    {
        public static List<T> DatatableToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        obj.SetPropertyValue(pro.Name, dr[column.ColumnName]);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
