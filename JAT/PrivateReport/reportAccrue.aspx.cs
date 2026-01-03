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
    public partial class reportAccrue : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.Visible = false;
                alert.Visible = false;
            }
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
            cmd.CommandTimeout = 600;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        */
        protected void Print_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            var DateFrom = datepicker1Input.Value.ToString();
            var DateTo = datepicker2Input.Value.ToString();
            var payMeth = drplstPayMethod.SelectedValue.ToString();
            var viewmode = Format.SelectedValue.ToString();
            var splitmode = PaymentSplit.SelectedValue.ToString();

            // ตรวจสอบเงื่อนไข chk
            bool chk = (viewmode != "M") || (payMeth == "J" || payMeth == "S" || payMeth == "T");

            if (!chk)
            {
                ReportViewer1.Visible = false;
                alert.Visible = true;
                alert.Text = "Please check input again";
                return;
            }

            try
            {
                // สร้าง repository ด้วย connection string
                string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                var repo = new AccrueReportRepository(connStr);

                // ดึง DataTable ผ่าน repository
                DataTable dt = repo.GetAccrueReport(payMeth, viewmode, splitmode, DateFrom, DateTo);

                // กำหนด report path ตาม viewmode และ payMethod
                if (viewmode == "L")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateAccrue.rdlc");
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath(
                        (payMeth == "S" || payMeth == "T")
                            ? "~/PrivateReport/ReportPage/printPrivateAccrueLetter1.rdlc"
                            : "~/PrivateReport/ReportPage/printPrivateAccrueLetter.rdlc"
                    );
                }

                // กำหนด report parameters
                var reportParameters = new ReportParameterCollection
        {
            new ReportParameter("fromDate", DateFrom),
            new ReportParameter("toDate", DateTo),
            new ReportParameter("payMethod", payMeth)
        };
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                // เพิ่ม DataSource ให้ ReportViewer
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                // แสดง report
                alert.Visible = false;
                ReportViewer1.Visible = true;

                // log activity
                string activityDetail = $"Executed procedure 'psPrivateReportAccrue' successful (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
            catch (Exception ex)
            {
                string activityDetail = $"Executed procedure 'psPrivateReportAccrue' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
        }

    }
}