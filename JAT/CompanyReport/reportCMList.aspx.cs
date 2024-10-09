using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Linq;

namespace JAT.CompanyReport
{
    public partial class reportCMList : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

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
        protected void print_Click(object sender, EventArgs e)
        {
            connection();
            conn.Open();
            var DateFrom = txtFromDate.Value.ToString();
            var DateTo = txtToDate.Value.ToString();
            if (cbViewType.Text == "New Member")
            {
                string sql = "SET dateformat dmy select companyNmJ,companyNmE,address,busType,represNm,represPosition,phone,fax,appliedDate " +
                             "from CompanyMember " +
                             "where appliedDate between '"+ DateFrom + "' and '"+ DateTo + "' " +
                             "order by appliedDate ";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyListNew.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            else if (cbViewType.Text == "Quit")
            {
                //DateFrom = Date_MsSqlStandard.CastQuery(DateFrom);
                //DateTo = Date_MsSqlStandard.CastQuery(DateTo);

                var _dateFrom = DateTime.ParseExact(DateFrom, "dd/MM/yyyy", null);
                var _dateTo = DateTime.ParseExact(DateTo, "dd/MM/yyyy", null);

                var _dateFrom_Formatted = _dateFrom.ToString("yyyy-MM-dd");
                var _dateTo_Formatted = _dateTo.ToString("yyyy-MM-dd");

                _dateFrom_Formatted += " 00:00:00.000";
                _dateTo_Formatted += " 23:59:59.999";

                //_dateFrom += " 00:00:00.000";
                //_dateTo += " 23:59:59.999";
                string sql = $@"SET dateformat dmy 
                                SELECT companyNmJ,companyNmE,address,busType,represNm, 
                                        represPosition,phone,fax,updatedDate 
                                FROM CompanyMember 
                                WHERE updatedDate BETWEEN CONVERT(DATETIME, '{_dateFrom_Formatted}', 20) 
                                        AND CONVERT(DATETIME, '{_dateTo_Formatted}', 20) 
                                        AND (memberStatus = 'NA')
                                ORDER BY updatedDate ASC";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyListQuit.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            //test
            //connection();
            //conn.Open();
            //string sql = "select companyNmE " +
            //             "from CompanyMember";
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/report.rdlc");
            //SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //ReportDataSource rds = new ReportDataSource("DataSet2", dt);
            //ReportParameterCollection reportParameters = new ReportParameterCollection();

            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rds);
        }
    }
}