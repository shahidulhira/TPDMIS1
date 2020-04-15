using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace BDCO.Web.Utility.JGrid
{
    public class FCMPushNotification
    {
        public FCMPushNotification()
        {
            // TODO: Add constructor logic here  
        }
        public bool Successful
        {
            get;
            set;
        }
        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }
        public FCMPushNotification SendNotification(string _title, string _message, string _topic, string deviceId)
        {
            FCMPushNotification result = new FCMPushNotification();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;  
                string serverKey = "AAAAuYZIzbo:APA91bFDt1ekYu2n_HfpQNn1M69bdWJPSDL2o-84nLZELW3YKObVly-f9UzaFxYR_RCE2v7qRgtyrCTOM8G8V0IsRbonaxI-lzJ0tkdhHrJ36u-ETOtJdP6Tc1qSHOdUfpdiYczT1YnT";
                string senderId = "796821867962";
                var requestUri = "https://fcm.googleapis.com/fcm/send";
                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                webRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId, // this if you want to test for single device  
                    //                        to = "/topics/" + _topic, // this is for topic  
                    priority = "high",
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        show_in_foreground = "True",
                        icon = "myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
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
    }
}