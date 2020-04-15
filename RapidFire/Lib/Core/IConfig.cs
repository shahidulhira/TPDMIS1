using RapidFireLib.Lib.Authintication;
using RapidFireLib.Lib.Extension;
using RapidFireLib.Models;
using System;
using System.Collections.Generic;

namespace RapidFireLib.Lib.Core
{
    public enum SAASType
    {
        NoSaas = 0,
        Schema,
        Database
    }

    public interface IConfig
    {
        void Configure(ref Configuration configuration);
    }

    public class Configuration
    {
        //internal SYSTEM SYSTEM;// = new SYSTEM();
        public APP APP = new APP();
        public DB DB = new DB();
        public FCM FCM = new FCM();
        public SMS SMS = new SMS();
        public JwtKeys JwtKeys = new JwtKeys();
        public Login Login = new Login();
        public Scripts SAASScripts = new Scripts();
    }
    public class Login
    {
        public LoginType LoginType { get; set; } = LoginType.DbLogin;
        public string ADLoginServer { get; set; }
    }
    public class JwtKeys
    {
        public string SecretKey { get; set; } = @"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIg
                                                Givly4ABfZkrDr1RKcYEI8Oyi9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+Y
                                                qS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3g
                                                ukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==";
        public TimeSpan ExpireDuration { get; set; } = TimeSpan.FromDays(14);
    }

    public static class ConfigLib
    {
        public static bool CheckTablePermission { get; set; } = false;
        public static List<ModelAccess> ModelAccess = new List<ModelAccess>();
    }

    public class APP
    {
        public object User { get; set; }
        public object UserId { get => User.GetPropertyValue("UserId"); }
        public object UserName { get => User.GetPropertyValue("UserName"); }
    }

    public class DB
    {
        //private RFCoreDbContext _DefaultDbContext = null;
        //public RFCoreDbContext DefaultDbContext
        //{
        //    get
        //    {
        //        var context = (RFCoreDbContext)new Dynamic().GetInstance(_DefaultDbContext.GetType());
        //        //context.SetTenantStatus(Config.SAAS.SaasType);
        //        return context;
        //    }
        //    set => _DefaultDbContext = value;
        //}
        public RFCoreDbContext DefaultDbContext { get; set; }
    }

    public class FCM
    {
        public string ServerKey { get; set; }
        public string RequestUri { get; set; }
        public string SenderId { get; set; }
    }

    public class SMS
    {
        public string TextSMSRequestUri { get; set; }
        public string VoiceSMSRequestUri { get; set; }
    }

    public class Scripts
    {
        internal string NewSchemaDataSet { get; set; } = @"../RapidFire/Scripts/insertData.sql";
        public string NewDbDataSetSQL { get; set; } //Auto
    }
}
