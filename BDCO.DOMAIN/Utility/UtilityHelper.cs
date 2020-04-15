using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;


namespace BDCO.Domain.Utility
{
    #region Utility Helper Class

    public class UtilityHelper
    {
      
        public UtilityHelper()
        {
        }
       

        public void ShowHTMLMessage(Page _Page, string ErroeCode, string ErrorMessage, bool IsError = true)
        {
            if (!IsError)
                ShowAlert(_Page, ErrorMessage, ErroeCode, MessageType.Error);
            else
                ShowAlert(_Page, ErrorMessage, ErroeCode, MessageType.Success);
        }

        public void ShowHTMLAlert(Page _Page, string ErroeCode, string ErrorMessage)
        {
            ShowAlert(_Page, ErrorMessage, ErroeCode, MessageType.Success);
        }

        public void ShowAlert(Page _Page, string ErrorMessage, string ErroeCode = "0000", MessageType type = MessageType.Error)
        {
            string eMsg = ErrorMessage.Replace("\r\n", " ").Replace("'", "\"");
            eMsg = "<script type='text/javascript'>showMessage('" + ErroeCode + "','" + eMsg + "'," + (int)type + ");</script>";
           // ScriptManager.RegisterClientScriptBlock(_Page, _Page.GetType(), "script", eMsg, false);
        }

     
        public object GetCookeRecord(Page _Page, AppsCookie _AppCookie)
        {
            string CookeName = _AppCookie.ToString();
            HttpCookie _Cookie = new HttpCookie(CookeName);
            _Cookie = _Page.Request.Cookies[CookeName];
            if (_Cookie != null) return _Cookie.Value; else return "";
        }

        public void SetCookeRecord(Page _Page, AppsCookie _AppCookie, string CookeValue)
        {
            string CookeName = _AppCookie.ToString();
            HttpCookie _Cookie = new HttpCookie(CookeName);
            _Cookie.Value = CookeValue;                     // Set the cookie value.
            _Cookie.Expires = DateTime.Now.AddDays(30);     // Set the cookie expiration date.
            _Page.Response.Cookies.Add(_Cookie);                  // Add the cookie.
            _Page.Request.Cookies[CookeName].Value = CookeValue;  //HttpCookie cookieCode = new HttpCookie(CookeName, CookeValue);//Response.SetCookie(cookieCode);
        }

        public object GetSessionRecord(Page _Page, AppsCookie _AppCookie)
        {
            string CookeName = _AppCookie.ToString();
            if (_Page.Session[CookeName] != null) return _Page.Session[CookeName].ToString(); else return "";
        }

        public void SetSessionRecord(Page _Page, AppsCookie _AppCookie, string CookeValue)
        {
            string CookeName = _AppCookie.ToString();
            _Page.Session[CookeName] = CookeValue;
        }

        public void FileDownload(Page _page, string urlPath, string filename)
        {
            Stream stream = null;
            //This controls how many bytes to read at a time and send to the client
            int bytesToRead = 10000;
            // Buffer to read bytes in chunk size specified above
            byte[] buffer = new Byte[bytesToRead];
            try
            {
                string fileName = filename;

                //Create a WebRequest to get the file
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(urlPath + fileName);
                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();
                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;
                //Get the Stream returned from the response
                stream = fileResp.GetResponseStream();
                // prepare the response to the client. resp is the client Response
                var resp = HttpContext.Current.Response;
                //Indicate the type of data being sent
                resp.ContentType = "application/octet-stream";
                resp.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                resp.AddHeader("Content-Length", fileResp.ContentLength.ToString());
                int length;
                do
                {
                    // Verify that the client is connected.
                    if (resp.IsClientConnected)
                    {
                        // Read data into the buffer.
                        length = stream.Read(buffer, 0, bytesToRead);
                        // and write it out to the response's output stream
                        resp.OutputStream.Write(buffer, 0, length);
                        // Flush the data
                        resp.Flush();
                        //Clear the buffer
                        buffer = new Byte[bytesToRead];
                    }
                    else
                    {
                        // cancel the download if client has disconnected
                        length = -1;
                    }
                } while (length > 0); //Repeat until no data is read

            }
            catch (Exception ex)
            {
                ShowHTMLMessage(_page, ex.HResult.ToString(), ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    //Close the input stream
                    stream.Close();
                }
            }
        }
        public PropertyInfo GetPrimaryKeyInfo(Type classType)
        {
            PropertyInfo[] properties = classType.GetProperties();
            foreach (PropertyInfo pI in properties)
            {
                System.Object[] attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute.GetType().Name == "KeyAttribute")
                    {
                        return pI;
                    }

                }
            }
            return null;
        }
        public bool IsValidDate(object sDate)
        {
            try
            {
                if (sDate == null) return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
    #endregion
}
