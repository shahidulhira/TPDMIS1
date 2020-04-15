using RapidFireLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.Core
{
    public class TenantManager
    {
        Db db = null;
        Configuration Config = null;

        public TenantManager(IConfig config)
        {
            Config = new ConfigBuilder().Get(config);
            db = new Db(config);
        }

        public void RegisterTenant(Tenant tenant)
        {
            // Add entry to RFRouteBD
        }
        public void ApprovedTenant(Tenant tenant)
        {
            //Read from RF Request Context / With Query Params
            var databaseIsExist = db.IsDatabaseExist(tenant.SubdomainPrefix);
            if (!databaseIsExist)
            {
                db.CreateDatabase(tenant.SubdomainPrefix);
                db.ExecuteSQLFile(Config.SAASScripts.NewDbDataSetSQL, Config.DB.DefaultDbContext, tenant.SubdomainPrefix);
            }
        }
        public void SuspendTenant()
        {
            //disable client in RFRouteBD  //how to protect disable?
        }
    }
}
