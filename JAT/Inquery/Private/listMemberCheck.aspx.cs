using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Globalization;
using System.Threading;
using Microsoft.Reporting.WebForms;
namespace JAT.Inquery.Private
{
    public partial class listMemberCheck : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            print();
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        public DataTable SelectSqlTable(string Sqlcmd)
        {
            connection();
            var table = new DataTable();
            string sql = Sqlcmd;
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }

        private void print()
        {
            connection();
            conn.Open();
            IFormatProvider culture = new CultureInfo("en-US", true);

            string sql = "select a.firstmemberid, prefixNm, NameJ, nameE, checkmember as memberid, checkMainName as fullNameE, checkBirthPlace as birthPlace, checkFamilyName as RowNum, checkCompanyNm as companyNm, checkCompanyAddress as address, " +
                "checkCompanyPhone as ComPhone, checkCompanyFax as faxCom, checkHomeAddress as HomeAdd, checkHomePhone as HomePhone, checkHomeMobile as mobile " +
                "from privateDetail a inner join private b on a.firstmemberid = b.memberid where a.memberid in (select distinct firstmemberid from privateDetail) and memberstatus = 'A' " +
                "order by namee ";

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Inquery/Private/ReportPage/printlistMemberCheck.rdlc");
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Inquery/Private/ReportPage/printlistMemberCheck - Copy.rdlc");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.SelectCommand.CommandTimeout = 1600;
            adapter.Fill(dt);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);

            conn.Close();

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;
            string deviceInfo = "<DeviceInfo>" +
                     "<OutputFormat>PDF</OutputFormat>" +
                     "  <PageWidth>11in</PageWidth>" +
                     "  <PageHeight>8.5in</PageHeight>" +
                     "  <MarginTop>0.0in</MarginTop>" +
                     "  <MarginLeft>0.0in</MarginLeft>" +
                     "  <MarginRight>0.0in</MarginRight>" +
                     "  <MarginBottom>0.0in</MarginBottom>" +
                     "  <EmbedFonts>None</EmbedFonts>" +
                     "</DeviceInfo>";


            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=Member List Information." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}