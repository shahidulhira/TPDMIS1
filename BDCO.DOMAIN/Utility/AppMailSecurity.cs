using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace BDCO.Domain.Utility
{
    public class AppMailSecurity
    {

        protected UtilityHelper uh = null;

        public AppMailSecurity()
        {

            uh = new UtilityHelper();
        }
        static protected bool CheckCert(Object sender, X509Certificate certificate,
        X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; }
        protected ExchangeService GetServiceEx(string sEmailID, string sEmailPass)
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service.Credentials = new NetworkCredential(sEmailID, sEmailPass);
            ServicePointManager.ServerCertificateValidationCallback = CheckCert;
            service.Url = new Uri("https://dbxprd0310.outlook.com/EWS/Exchange.asmx");
            return service;
        }
    }

    #region Login Helper Class
    public class AppLoginHelper : AppMailSecurity
    {
        public AppLoginHelper() { }
        private bool IsValidOutlookLogin(Page _Page, string sEmailID, string sEmailPass)
        {
            bool bRet = false;
            try
            {
                ExchangeService service = GetServiceEx(sEmailID, sEmailPass);
                List<AttendeeInfo> attendees = new List<AttendeeInfo>();
                attendees.Add(new AttendeeInfo(sEmailID));

                GetUserAvailabilityResults results = service.GetUserAvailability(attendees,
                        new TimeWindow(DateTime.Now, DateTime.Now.AddHours(24)), AvailabilityData.FreeBusy);

                AttendeeAvailability myAvailablity = results.AttendeesAvailability.FirstOrDefault();
                if (myAvailablity != null) { Console.WriteLine(String.Format("FREE", myAvailablity.CalendarEvents.Count)); }
                return true;
            }
            catch (Exception ex)
            {
                var w32ex = ex as Win32Exception;
                uh.ShowHTMLMessage(_Page, ErrorNumber.SetType("00", errorType.LoginError), ex.Message);
                bRet = false;
            }
            return bRet;
        }
        public bool IsValidLogin_SCIMail(Page _Page, string sEmailID, string sEmailPass)
        {
            bool bRet = false;
            if (IsValidOutlookLogin(_Page, sEmailID, sEmailPass))
                bRet = true;
            else
            {
                uh.ShowHTMLMessage(_Page, ErrorNumber.SetType(ErrorNumber.PermissionDenied, errorType.LoginError), "Login Failure. You are not authorized user!!");
                bRet = false;
            }
            return bRet;
        }
        protected class LdapAuthentication
        {
            private String _path;
            private String _filterAttribute;
            public LdapAuthentication(String path) { _path = path; }
            public bool IsAuthenticated(String domain, String username, String pwd)
            {
                String domainAndUsername = domain + @"\" + username;
                DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
                try
                {
                    //Bind to the native AdsObject to force authentication.			
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);

                    search.Filter = "(SAMAccountName=" + username + ")";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();

                    if (null == result) { return false; }

                    _path = result.Path; //Update the new path to the user in the directory.
                    _filterAttribute = (String)result.Properties["cn"][0];
                }
                catch (Exception ex)
                {
                    return false;
                    throw new Exception("Error authenticating user. " + ex.Message);
                }
                return true;
            }

            public String GetGroups()
            {
                DirectorySearcher search = new DirectorySearcher(_path);
                search.Filter = "(cn=" + _filterAttribute + ")";
                search.PropertiesToLoad.Add("memberOf");
                StringBuilder groupNames = new StringBuilder();
                try
                {
                    SearchResult result = search.FindOne();
                    int propertyCount = result.Properties["memberOf"].Count;
                    String dn;
                    int equalsIndex, commaIndex;

                    for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                    {
                        dn = (String)result.Properties["memberOf"][propertyCounter];

                        equalsIndex = dn.IndexOf("=", 1);
                        commaIndex = dn.IndexOf(",", 1);
                        if (-1 == equalsIndex) { return null; }

                        groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                        groupNames.Append("|");
                    }
                }
                catch (Exception ex) { throw new Exception("Error obtaining group names. " + ex.Message); }
                return groupNames.ToString();
            }
        }
        public bool IsValidLogin_SCIAD(Page _Page, string ADUserName, string ADPassword)
        {
            bool bRet = false;
            string adPath = "LDAP://dhaka.org";
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            if (adAuth.IsAuthenticated("dhaka.org", ADUserName, ADPassword))
                bRet = true;
            else
            {
                uh.ShowHTMLMessage(_Page, ErrorNumber.SetType(ErrorNumber.PermissionDenied, errorType.LoginError), "Login Failure. You are not authorized user!!");
                bRet = false;
            }
            return bRet;
        }
    }
    #endregion



    #region SendMail Helper Class
    public class MailHelper : AppMailSecurity
    {
        public MailHelper()
        { }
        public bool SendOutlookMail(Page _Page, string sMailTo, string sSubject, string sBody, string sAttachmentPath)
        {
            try
            {
                string sqlStr = "SELECT TOP(1) [EmailID],[Password] FROM [dbo].[SysSet]";
                DataTable dt = null;//new AppManager().DataAccess.RecordSet(sqlStr, new string[] { });
                if (dt != null && dt.Rows.Count > 0)
                {
                    string sEmailID = dt.Rows[0]["EmailID"].ToString();
                    string sEmailPass = dt.Rows[0]["Password"].ToString();
                    //sMailTo = "shyamal.mondal@savethechildren.org";
                    EmailMessage message = new EmailMessage(GetServiceEx(sEmailID, sEmailPass));
                    message.Subject = sSubject;
                    message.Body = sBody;
                    if (sAttachmentPath != "") message.Attachments.AddFileAttachment(sAttachmentPath);
                    message.ToRecipients.Add(sMailTo);
                    message.SendAndSaveCopy();
                    return true;
                }
                else
                {
                    uh.ShowHTMLMessage(_Page, "100", "From email Id not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                uh.ShowHTMLMessage(_Page, "100", ex.Message);
                return false;
            }
        }

        public bool SendOutlookMail(Page _Page, string[] sMailTo, string[] ccEmail, string sSubject, string sBody, string[] sAttachmentPath, string[] bcclist = null)
        {
            try
            {
                string sqlStr = "SELECT TOP(1) [EmailID],[Password] FROM [dbo].[SysSet]";
                DataTable dt = null;//AppManager().DataAccess.RecordSet(sqlStr, new string[] { });
                if (dt != null && dt.Rows.Count > 0)
                {
                    string sEmailID = dt.Rows[0]["EmailID"].ToString();
                    string sEmailPass = dt.Rows[0]["Password"].ToString();
                    EmailMessage message = new EmailMessage(GetServiceEx(sEmailID, sEmailPass));
                    message.Subject = sSubject;
                    message.Body = sBody;

                    if (sAttachmentPath != null && sAttachmentPath.Length > 0)
                    {
                        foreach (string file in sAttachmentPath)
                        {
                            if (!string.IsNullOrEmpty(file))
                            {
                                message.Attachments.AddFileAttachment(file);
                            }
                        }
                    }

                    if (sMailTo != null && sMailTo.Length > 0)
                    {
                        foreach (string tm in sMailTo)
                        {
                            if (!string.IsNullOrEmpty(tm))
                            {
                                message.ToRecipients.Add(tm);
                            }
                        }

                    }

                    if (ccEmail != null && ccEmail.Length > 0)
                    {
                        foreach (string ccm in ccEmail)
                        {
                            if (!string.IsNullOrEmpty(ccm))
                            {
                                message.CcRecipients.Add(ccm);
                            }
                        }

                    }
                    if (bcclist != null && bcclist.Length > 0)
                    {
                        foreach (string bcc in bcclist)
                        {
                            if (!string.IsNullOrEmpty(bcc))
                            {
                                message.BccRecipients.Add(bcc);
                            }
                        }

                    }
                    message.SendAndSaveCopy();
                    return true;
                }
                else
                {
                    uh.ShowHTMLMessage(_Page, "100", "From email Id not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                uh.ShowHTMLMessage(_Page, "100", ex.Message);
                return false;
            }
        }
    }
    #endregion
}
