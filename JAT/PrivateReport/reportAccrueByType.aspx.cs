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

namespace JAT.PrivateReport
{
    public partial class reportAccrueByType : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

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
            cmd.CommandTimeout = 600;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }

        protected void print_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			var DateFrom = datepicker1Input.Value.ToString();
            var DateTo = datepicker2Input.Value.ToString();
            var payMeth = paymethod.SelectedValue.ToString();
            var payspli = paysplit.SelectedValue.ToString();
            connection();
            conn.Open();
            var viewmode = retype.SelectedValue.ToString();
            string sql = "SET dateformat dmy exec psPrivateReportAccrueByType_Main @payMethod=N'" + payMeth + "',@viewMode=N'" + viewmode + "',@SplitMode=N'" + payspli + "',@fromDate=N'" + DateFrom + "',@toDate=N'" + DateTo + "'";
            if (viewmode == "1")
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateAccrueByType.rdlc");
            }
            else
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateAccrueByType2.rdlc");
            }
            switch (payMeth)
            {
                case "XX": payMeth = "All"; break;
                default: payMeth = payMeth; break;
            }
            switch (payspli)
            {
                case "XX": payspli = "All"; break;
                case "1": payspli = "No"; break;
                case "2": payspli = "Yes"; break;
            }
            try
            {
				SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				adapter.SelectCommand.CommandTimeout = 1600;
				DataTable dt = new DataTable();
				adapter.Fill(dt);
				string activityDetail = $"Excuted procedure name 'psPrivateReportAccrueByType_Main' successful (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
				
				ReportDataSource rds = new ReportDataSource("DataSet1", dt);
				ReportParameterCollection reportParameters = new ReportParameterCollection();
				reportParameters.Add(new ReportParameter("fromDate", DateFrom));
				this.ReportViewer1.LocalReport.SetParameters(reportParameters);
				reportParameters.Add(new ReportParameter("toDate", DateTo));
				this.ReportViewer1.LocalReport.SetParameters(reportParameters);
				reportParameters.Add(new ReportParameter("paysplit", payspli));
				this.ReportViewer1.LocalReport.SetParameters(reportParameters);
				reportParameters.Add(new ReportParameter("payMethod", payMeth));
				this.ReportViewer1.LocalReport.SetParameters(reportParameters);

				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.DataSources.Add(rds);
			}
			catch (SqlException ex)
			{
				string activityDetail = $"Excuted procedure name 'psPrivateReportAccrueByType_Main' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
            {
				string activityDetail = $"Excuted procedure name 'psPrivateReportAccrueByType_Main' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			conn.Close();
		}
	}
}