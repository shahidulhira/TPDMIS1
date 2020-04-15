
//using System.Data;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace BDCO.Domain.Utility
//{
//    public class ReportHelper
//    {
//        public ReportHelper()
//        {
//        }

//        /// <summary>
//        /// Export To PDF
//        /// </summary>
//        /// <param name="reportFileFullPath"></param>
//        /// <param name="dt"></param>
//        /// <param name="pdfFileName"></param>
//        /// <param name="isDownload"></param>
//        public void PrintReport(string reportFileFullPath, DataTable dt, string pdfFileName, bool isDownload = false)
//        {
//            using (ReportClass report = new ReportClass())
//            {
//                report.FileName = reportFileFullPath;
//                report.Load();
//                report.SetDataSource(dt);
//                report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, isDownload, pdfFileName);
//                report.Dispose();
//                report.Close();
//            }
//        }

//        public string Export_To_Excel(string sFileName, DataTable dt)
//        {
//            StringBuilder sbrHTML = new StringBuilder();
//            StringWriter stringWriter = new StringWriter();
//            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
//            DataGrid tmpGrid = new DataGrid();          
//            tmpGrid.DataSource = dt;
//            tmpGrid.DataBind();
//            tmpGrid.RenderControl(htmlWrite);
//            string strPath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Documents/"), sFileName + ".xls");
//            StreamWriter swXLS = new StreamWriter(strPath);
//            swXLS.Write(stringWriter);
//            swXLS.Close();
//            swXLS.Dispose();
//            System.Web.HttpContext.Current.Response.Redirect(strPath);
//            return strPath;// 
//        }
       
//        //protected void btnExport_Click(object sender, EventArgs e)
//        //{
//        //    StringBuilder sbrHTML = new StringBuilder();
//        //    StringWriter stringWriter = new StringWriter();
//        //    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);

//        //    string sFileName = "Recruitment Tracking";
//        //    string sqlStr = "EXEC rptRecruitmentTracker '" + (cbxAll.Checked ? 0 : Master.PafID) + "','" + Master.PafFD.ToString("dd-MMM-yyyy") + "','" + Master.PafTD.ToString("dd-MMM-yyyy") + "'";

//        //    DataGrid tmpGrid = new DataGrid();
//        //    sLink.grdLoad(sqlStr, tmpGrid);
//        //    tmpGrid.RenderControl(htmlWrite);

//        //    StreamWriter swXLS = new StreamWriter(Server.MapPath(".") + "\\" + sFileName + ".xls");
//        //    swXLS.Write(stringWriter);
//        //    swXLS.Close();

//        //    Response.Redirect(sFileName + ".xls");
//        //}

//        //protected void btnShow_Click(object sender, EventArgs e)
//        //{
//        //    try
//        //    {
//        //        string sqlStr = "EXEC rptRecruitmentTracker '" + (cbxAll.Checked ? 0 : Master.PafID) + "','" + Master.PafFD.ToString("dd-MMM-yyyy") + "','" + Master.PafTD.ToString("dd-MMM-yyyy") + "'";
//        //        sLink.grdLoad(sqlStr, dgView);
//        //    }
//        //    catch (SqlException sExp)
//        //    {
//        //        lblError.Text = sExp.Message.ToString();
//        //        lblError.Visible = true;
//        //        return;
//        //    }
//        //    catch (Exception gExp)
//        //    {
//        //        lblError.Text = gExp.Message.ToString();
//        //        lblError.Visible = true;
//        //        return;
//        //    }
//        //}
//    }
//}
