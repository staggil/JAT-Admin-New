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
    public partial class reportPayment : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
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

        protected void Print_Click(object sender, EventArgs e)
        {
            // 1. ดึงค่าจาก UI
            string DateFrom = datepicker1Input.Value.ToString();
            string DateTo = datepicker2Input.Value.ToString();
            string payAtValue = PayAt.SelectedValue;
            string payMethodValue = payMethod.SelectedValue;

            // 2. สร้าง instance repository
            PaymentReportRepository repo = new PaymentReportRepository();

            // 3. เรียก repository เพื่อดึง DataTable และ report URL
            string reportUrl;
            DataTable dt = repo.GetPaymentReport(DateFrom, DateTo, payAtValue, payMethodValue, out reportUrl);

            // 4. Bind DataTable กับ ReportViewer
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(reportUrl);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            // 5. Set Parameters
            ReportParameterCollection reportParameters = new ReportParameterCollection
    {
        new ReportParameter("dateFrom", DateFrom),
        new ReportParameter("dateTo", DateTo),
        new ReportParameter("paymethod", payMethodValue)
    };
            ReportViewer1.LocalReport.SetParameters(reportParameters);

            // 6. Refresh Report
            ReportViewer1.LocalReport.Refresh();
        }


    }
}