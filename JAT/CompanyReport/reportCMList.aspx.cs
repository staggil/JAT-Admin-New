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
                string sql = "SET dateformat dmy select companyNmJ,companyNmE,address,busType,represNm,represPosition,phone,fax,updatedDate " +
                             "from CompanyMember " +
                             "where cancelDate between '" + DateFrom + "' and '" + DateTo + "' " +
                             "order by updatedDate ";
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