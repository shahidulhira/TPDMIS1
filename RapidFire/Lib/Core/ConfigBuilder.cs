using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.Core
{
    public class ConfigBuilder
    {
        public Configuration Get(IConfig config)
        {
            Configuration configuration = new Configuration();
            config.Configure(ref configuration);
            return configuration;
        }
    }
}
