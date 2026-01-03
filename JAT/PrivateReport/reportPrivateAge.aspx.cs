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
using JAT.Core;

namespace JAT.PrivateReport
{
    public partial class reportPrivateAge : System.Web.UI.Page
    {

        private SqlConnection conn;
        private SqlCommand cmd;

        //private ReportDocument repSource = new ReportDocument();

        //private ReportDocument reportDocument;


        /*
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
        */


        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                DropDownList1.Items.Add(i.ToString());
                DropDownList2.Items.Add(i.ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            int startAge = int.Parse(DropDownList1.SelectedValue);
            int toAge = int.Parse(DropDownList2.SelectedValue);

            
            PrivateAgeReportRepository repo = new PrivateAgeReportRepository();

            
            string title, reportUrl;
            DataTable dt = repo.GetAgeReport(startAge, toAge, out title, out reportUrl);

           
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(reportUrl);

            
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("showAge", title));
            ReportViewer1.LocalReport.SetParameters(reportParameters);

    
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
        }





    }
}