using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Authintication;
using RapidFireLib.Models;
using RapidFireLib.UX;
using System;
using System.Collections.Generic;

namespace RapidFireLib.Lib.Core
{
    public class RapidFire
    {
        //public Config Config { get { return new Config(); } }
        public Dynamic Dynamic { get { return new Dynamic(); } }
        public Db Db { get; set; }
        public Messaging Messaging { get; set; }
        public UI UI { get; set; }
        public Contexts Context => new Contexts();
        public Casts Cast => new Casts();
        public Login Login => new Login();
        public WebApi Api { get; set; }
        public TenantManager TenantManager { get; set; }
        
        public UserAuthintications UserAuthintications;
        //private Delegate void ExecuteConfiguration;

        public RapidFire(IConfig config, bool checkTablePermission = true)
        {
            Configuration configuration = new Configuration();
            config.Configure(ref configuration);
            ForceMinimumConfiguration(configuration);

            ConfigLib.CheckTablePermission = checkTablePermission;
            if (checkTablePermission) ConfigLib.ModelAccess = GetModelAccess(configuration.APP.UserId.ToString());//Store DB Table Permission Once and Store it in DIC

            
            Db = new Db(config);
            Messaging = new Messaging(config);
            UI = new UI(config);
            Api =new WebApi(config);
            TenantManager = new TenantManager(config);
            UserAuthintications = new UserAuthintications(config);
        }

        public void ForceMinimumConfiguration(Configuration configuration)
        {
            if (configuration.DB.DefaultDbContext == null) throw new NotImplementedException();
            // if (configuration.APP.UserId.ToString() == "") throw new NotImplementedException();
        }

        private List<ModelAccess> GetModelAccess(string userId)
        {
            List<ModelAccess> modelAccesses = new List<ModelAccess>();
            //Db.Get(modelAccesses, "");
            return modelAccesses;
        }

        public class Casts
        {
            public View View => new View();
        }

        public class Contexts
        {
            public Http Http => new Http();
        }
    }
}