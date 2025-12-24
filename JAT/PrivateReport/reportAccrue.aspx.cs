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

        protected void Print_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			var DateFrom = datepicker1Input.Value.ToString();
            var DateTo = datepicker2Input.Value.ToString();
            var payMeth = drplstPayMethod.SelectedValue.ToString();
            var viewmode = Format.SelectedValue.ToString();
            var splitmode = PaymentSplit.SelectedValue.ToString();
            bool chk = false;
            if (viewmode == "M")
            {
                if (payMeth == "J" || payMeth == "S" || payMeth == "T")
                {
                    chk = true;
                }
                else
                {
                    chk = false;
                }
            }
            else
            {
                chk = true;
            }
            try
            {
				if (chk)
				{
                	connection();
                	conn.Open();
                //select any
					string sql = "SET dateformat dmy EXEC psPrivateReportAccrue '" + payMeth + "','" + viewmode + "','" + splitmode + "','" + DateFrom + "','" + DateTo + "'";
					if (viewmode == "L")
					{
						ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateAccrue.rdlc");
						ReportParameterCollection reportParameters = new ReportParameterCollection();
						reportParameters.Add(new ReportParameter("fromDate", DateFrom));
						this.ReportViewer1.LocalReport.SetParameters(reportParameters);
						reportParameters.Add(new ReportParameter("toDate", DateTo));
						this.ReportViewer1.LocalReport.SetParameters(reportParameters);
						reportParameters.Add(new ReportParameter("payMethod", payMeth));
						this.ReportViewer1.LocalReport.SetParameters(reportParameters);
					}
					else
					{
						if (payMeth == "S" || payMeth == "T")
						{
							ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateAccrueLetter1.rdlc");
						}
						else
						{
							ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateAccrueLetter.rdlc");
						}
					}
					SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    string activityDetail = $"Excuted procedure name 'psPrivateReportAccrue' successful (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID,activityDetail);

					adapter.SelectCommand.CommandTimeout = 3600;
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					ReportDataSource rds = new ReportDataSource("DataSet1", dt);

					conn.Close();

					ReportViewer1.LocalReport.DataSources.Clear();
					ReportViewer1.LocalReport.DataSources.Add(rds);
					alert.Visible = false;
					ReportViewer1.Visible = true;
				}
				else
				{
					ReportViewer1.Visible = false;
					alert.Visible = true;
					alert.Text = "Please check input again";
				}
			}
            catch (SqlException ex)
			{
				string activityDetail = $"Excuted procedure name 'psPrivateReportAccrue' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Excuted procedure name 'psPrivateReportAccrue' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

		}
    }
}