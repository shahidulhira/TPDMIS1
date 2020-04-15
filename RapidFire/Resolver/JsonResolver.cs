using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Resolver
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
