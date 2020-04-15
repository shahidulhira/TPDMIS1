using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RapidFireLib.Lib.DB;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace RapidFireLib.Lib.Core
{
    public class Messaging
    {
        Configuration Config = null;
        DbContext context;
        Db db;
        

        public FireBaseMessaging FireBase;
        public TextSMSMessaging TextSMS;
        public VoiceSMSMessaging VoiceSMS;

        public Messaging(IConfig config)
        {
            Config = new ConfigBuilder().Get(config);
            context = Config.DB.DefaultDbContext;
            db = new Db(config);

            FireBase = new FireBaseMessaging(context, config);
            TextSMS = new TextSMSMessaging();
            VoiceSMS = new VoiceSMSMessaging();
        }

        public class FireBaseMessaging
        {
            IConfig config;
            Configuration Config = null;
            DbContext context;
            Db db;
            public FireBaseMessaging(DbContext dbContext, IConfig config)
            {
                Config = new ConfigBuilder().Get(config);
                context = dbContext;
                db = new Db(config);
            }
            Dynamic Dynamic { get { return new Dynamic(); } }
            public MessagingResult SendPushMessage(string token, object pushData)
            {
                MessagingResult result = new MessagingResult { Successful = true, Error = null };
                try
                {

                    WebRequest webRequest = WebRequest.Create(Config.FCM.RequestUri);
                    webRequest.Method = "POST";
                    webRequest.Headers.Add(string.Format("Authorization: key={0}", Config.FCM.ServerKey));
                    webRequest.Headers.Add(string.Format("Sender: id={0}", Config.FCM.SenderId));
                    webRequest.ContentType = "application/json";

                    var fcmdata = new
                    {
                        to = token,
                        priority = "high",
                        data = pushData
                    };

                    //var serializer = new JavaScriptSerializer();
                    var json = JsonConvert.SerializeObject(fcmdata);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    webRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = webRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse webResponse = webRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = webResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    result.Response = sResponseFromServer;

                                    var modelName = Dynamic.GetPropValue(pushData, "ModelName").ToString();
                                    var classPath = Dynamic.GetFullyQualifiedPath(modelName);
                                    var dynamicModel = Dynamic.GetInstance(classPath);
                                    Tools.CopyClass(dynamicModel, pushData);
                                    db.Insert(dynamicModel);
                                    db.Commit();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Successful = false;
                    result.Response = null;
                    result.Error = ex;
                }
                return result;
            }

            //UnitOfWork unitOfWork = new UnitOfWork(null);
            //Dynamic.Repository(dynamicModel, context, DBOperations.INSERT, dynamicModel);

            public void UpdateMessage(int NotifiedTo, int NotifiedBy)
            {
                string sql = string.Format(@"UPDATE DataVerificationLog SET IsNotified = 1 WHERE NotifiedTo = {0} AND NotifiedBy = {1}", NotifiedTo, NotifiedBy);
                //UnitOfWork unitOfWork = new UnitOfWork();
                //unitOfWork.context.Database.ExecuteSqlCommand(sql);
            }
        }
        public class TextSMSMessaging
        {

        }
        public class VoiceSMSMessaging
        {

        }
    }

    public class MessagingResult
    {
        public bool Successful { get; set; }
        public string Response { get; set; }
        public Exception Error { get; set; }
    }

}