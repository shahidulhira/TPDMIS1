using BDCO.Domain;
using BDCO.Domain.Utility;
using BDCO.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace BDCO.Web
{
    public partial class WebBase
    {
        public struct Messaging
        {
            public static MessagingResult SendPushMessage(string serverKey, string token, Object pushData, string senderId)
            {
                MessagingResult result = new MessagingResult { Successful = true, Error = null };
                try
                {

                    WebRequest webRequest = WebRequest.Create(Config.requestUri);
                    webRequest.Method = "POST";
                    webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                    webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    webRequest.ContentType = "application/json";

                    var modelName = Dynamic.GetPropValue(pushData, "ModelName").ToString();
                    var path = Dynamic.GetFullyQualifiedPath(modelName);

                   
                    var fcmdata = new
                    {
                        to = token,
                        priority = "high",
                        data = pushData
                    };

                    var serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(fcmdata);
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

                                    //var dynamicModel = Dynamic.GetInstance("BDCO.Domain.Aggregates.Push.PushData");
                                    var dynamicModel = Dynamic.GetInstance(path);
                                    Tools.CopyClass(dynamicModel, pushData);
                                    UnitOfWork unitOfWork = new UnitOfWork();
                                    Dynamic.DynamicRepo(dynamicModel, unitOfWork.context, DBOperations.INSERT);
                                    unitOfWork.SaveChange();
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

            public static void UpdateMessage(int NotifiedTo, int NotifiedBy)
            {
                string sql = string.Format(@"UPDATE DataVerificationLog SET IsNotified = 1 WHERE NotifiedTo = {0} AND NotifiedBy = {1}", NotifiedTo, NotifiedBy);
                UnitOfWork unitOfWork = new UnitOfWork();
                unitOfWork.context.Database.ExecuteSqlCommand(sql);
            }            
        }
        
        //public class UserFCMToken
        //{
        //    public int UserID;
        //    public String AppID;
        //    public String Token;

        //    public UserFCMToken(int userID, String appID, String token)
        //    {
        //        this.UserID = userID;
        //        this.AppID = appID;
        //        this.Token = token;
        //    }
        //}
        public class MessagingResult
        {
            public bool Successful { get; set; }
            public string Response { get; set; }
            public Exception Error { get; set; }
        }
    }
}