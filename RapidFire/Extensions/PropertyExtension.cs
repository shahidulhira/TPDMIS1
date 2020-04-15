using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Extension
{
    public static class PropertyExtension
    {
        public static object GetPropertyValue(this object src, string propertyName)
        {
            if (src == null) throw new ArgumentNullException($"Source object can not be null");
            return src.GetType().GetProperty(propertyName)?.GetValue(src, null);
        }
        public static void SetPropertyValue(this object src , string propertyName,object value)
        {
            var propertyInfo = src.GetType().GetProperty(propertyName);
            Type fieldType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            var fieldValue = (string.IsNullOrEmpty(value.ToString())) ? null : Convert.ChangeType(value, fieldType);
            propertyInfo.SetValue(src, fieldValue, null);
        }
        public static bool HasProperty(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetProperty(methodName) != null;
        }
    }
}
