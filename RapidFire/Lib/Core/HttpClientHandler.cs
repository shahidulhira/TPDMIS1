using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RapidFireLib.Lib.Core
{
    public class HttpClientHandler
    {
        private HttpClient httpClient = new HttpClient();

        public object PostRequest() {
            return true;
        } 
        public object GetRequest() {
            return true;
        }
        public object PutRequest()
        {
            return true;
        }
        public object DeleteRequest()
        {
            return true;
        }
    }
}
