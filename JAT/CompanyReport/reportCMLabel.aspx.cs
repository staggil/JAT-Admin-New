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


namespace JAT.CompanyReport
{
    public partial class reportCMLabel : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
        //private int valOfTable;
        protected void Page_Load(object sender, EventArgs e)
        {
			//CreateData();
			//valOfTable = 0;
			if (!Page.IsPostBack)
            {
                Binddata();
                ReportViewer1.Visible = false;
            }
            //Binddata();
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        private void Binddata()
        {
            DataGrid1.DataSource = GetData().Tables["CompanyMember"].DefaultView;
            DataGrid1.DataBind();
            Label1.Text = "Search results returned <b>" + GetData().Tables["CompanyMember"].DefaultView.Count.ToString() + "</b> items";
        }
        private DataSet GetData()
        {
            //IFormatProvider culture = new CultureInfo(Session["language"].ToString(), true);
            IFormatProvider culture = new CultureInfo("en-US", true);
            connection();
            ///this is sql statement which returns records

            //String strSqlSelect = " SELECT companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
            //    // " expiredDate = DATEADD(DAY, -1, DATEADD(Month, companyPayment.noPayMonth, companyPayment.effectiveDate)), " +
            //    " companyPayment.expiredDate, " +
            //    " companyMember.represNmE, companyMember.represPosition, companyMember.address " +
            //    " FROM companyMember LEFT OUTER JOIN companyPayment " +
            //    " ON ( companyMember.companyId = companyPayment.companyId ) " +
            //    " WHERE companyMember.memberStatus = 'A' " +
            //    " AND companyMember.sendType <> 'Z' " +
            //    " AND companyPayment.newsFee IS NOT NULL AND CompanyPayment.Deleted_at IS NULL " +
            //    " AND companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
            //    "                               FROM companyPayment companyPayment_2 " +
            //    "                               WHERE companyPayment.companyId = companyPayment_2.companyId ) ";

            String strSqlSelect = " SELECT companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
                // " expiredDate = DATEADD(DAY, -1, DATEADD(Month, companyPayment.noPayMonth, companyPayment.effectiveDate)), " +
                " companyPayment.expiredDate, " +
                " companyMember.represNmE, companyMember.represPosition, companyMember.address " +
                " FROM companyMember LEFT OUTER JOIN companyPayment " +
                " ON ( companyMember.companyId = companyPayment.companyId ) " +
                " WHERE companyMember.memberStatus = 'A' " +
                " AND companyMember.sendType <> 'Z' " +
                " AND companyPayment.newsFee IS NOT NULL " +
                " AND companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
                "                               FROM companyPayment companyPayment_2 " +
                "                               WHERE companyPayment.companyId = companyPayment_2.companyId ) ";

            //" AND companyPayment.newsFee <> 0 " +

            if (txtCompanyId.Value.Trim() != "")
            {
                strSqlSelect = strSqlSelect + " AND companyMember.companyId like '%" + txtCompanyId.Value.Trim().Replace("'", "''")+ "%' ";
            }

            if (txtCompanyNameJpn.Value.Trim() != "")
            {
                strSqlSelect = strSqlSelect + " AND companyMember.companyNmJ LIKE N'%" + txtCompanyNameJpn.Value.Trim().Replace("'", "''")+ "%' ";
            }

            if (txtCompanyNameEng.Value.Trim() != "")
            {
               
                //new code fix bug "&" unicode 16:05 15/05/2568
                string cleanText = HttpUtility.HtmlDecode(txtCompanyNameEng.Value.Trim());
                cleanText = cleanText.Replace("'", "''");
                strSqlSelect = strSqlSelect + " AND companyMember.companyNmE LIKE '%" + cleanText + "%' ";
                //end code


                /* old code 15/05/2025 16:04
                strSqlSelect = strSqlSelect + " AND companyMember.companyNmE LIKE '%" + txtCompanyNameEng.Value.Trim().Replace("'", "''")+ "%' ";
                */

            }

            strSqlSelect = strSqlSelect + " ORDER BY companyMember.companyNmE, companyMember.companyNmJ ";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSqlSelect, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
			dataAdapter.SelectCommand.CommandTimeout = 1800;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "CompanyMember");
            return myDataSet;
        }

        protected void cmdView_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;
            DataGrid1.Visible = true;
            Binddata();
            ReportViewer1.LocalReport.DataSources.Clear();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtCompanyId.Value = "";
            txtCompanyNameJpn.Value = "";
            txtCompanyNameEng.Value = "";
            Binddata();
        }

        protected void cmdPrintSel_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			ReportViewer1.Visible = true;
            //Binddata();
            DataGrid1.Visible = false;
            connection();
            conn.Open();
            //string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ ASC) AS 'rn', " +
            //        "companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
            //        "companyPayment.expiredDate, " +
            //        "companyMember.represNmE, companyMember.represPosition, companyMember.address " +
            //        "INTO #TEMP " +
            //        "FROM companyMember LEFT OUTER JOIN companyPayment " +
            //        "ON ( companyMember.companyId = companyPayment.companyId ) " +
            //        "WHERE companyMember.memberStatus = 'A' AND CompanyPayment.Deleted_at IS NULL ";

            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ ASC) AS 'rn', " +
                    "companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
                    "companyPayment.expiredDate, " +
                    "companyMember.represNmE, companyMember.represPosition, companyMember.address " +
                    "INTO #TEMP " +
                    "FROM companyMember LEFT OUTER JOIN companyPayment " +
                    "ON ( companyMember.companyId = companyPayment.companyId ) " +
                    "WHERE companyMember.memberStatus = 'A' ";

            string wherenme = "";


            //new code for fix as customer want  15:40 20/05/2025
            int dataRowCount = 0;
            GridViewRow singleRow = null;

            foreach (GridViewRow row in DataGrid1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    dataRowCount++;
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                    bool chk = chkRow.Checked;

                    if (chk)
                    {
                        string companyName = HttpUtility.HtmlDecode(row.Cells[2].Text);
                        wherenme += "'" + companyName.Replace("'", "''") + "',";
                    }
                    else
                    {
                        singleRow = row; // เก็บไว้กรณียังไม่มี checkbox ใดถูกติ๊ก
                    }
                }
            }
            //เจอแถวเดียว
            if (wherenme == "" && dataRowCount == 1 && singleRow != null)
            {
                string companyName = HttpUtility.HtmlDecode(singleRow.Cells[2].Text);
                wherenme += "'" + companyName.Replace("'", "''") + "',";
            }

            if (string.IsNullOrEmpty(wherenme))
            {
                // warning
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please choose at least one bill.');", true);
                return;
            }

            //new code end here





            System.Diagnostics.Debug.Print(wherenme.Length.ToString());
            try
            {
                wherenme = wherenme.Remove(wherenme.Length - 1, 1);
                wherenme = "AND CompanyMember.companyNmE in (" + wherenme + ") ";
                sql += wherenme;
                sql += "AND companyMember.sendType <> 'Z' " +
                        "AND companyPayment.newsFee IS NOT NULL " +
                        "AND companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
                        "FROM companyPayment companyPayment_2 " +
                        "WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
                        "ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ " +
                        "SELECT rn % 2 AS 'RowNum',* FROM #TEMP " +
                        "DROP TABLE #TEMP";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                string activityDetail = $"Removed a table name #temp successful (user name = '{staffID}')";
                logActivity.LogStaffActivity(staffID,activityDetail);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyLabel.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            catch (SqlException ex) 
            {
				string activityDetail = $"Removed a table name #temp unsuccessful [{ex.Message}] (user name = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Removed a table name #temp unsuccessful [{ex.Message}] (user name = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			conn.Close();
        }

        protected void cmdPrintAll_Click(object sender, EventArgs e)
        {
			//DataGrid1.Visible = false;
			//connection();
			//conn.Open();

			//string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ ASC) AS 'rn', " +
			//        "companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
			//        "companyPayment.expiredDate, " +
			//        "companyMember.represNmE, companyMember.represPosition, companyMember.address " +
			//        "INTO #TEMP " +
			//        "FROM companyMember LEFT OUTER JOIN companyPayment " +
			//        "ON ( companyMember.companyId = companyPayment.companyId ) " +
			//        "WHERE companyMember.memberStatus = 'A' " +
			//        "AND companyMember.sendType <> 'Z' " +
			//        "AND companyPayment.newsFee IS NOT NULL " +
			//        "AND companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
			//        "FROM companyPayment companyPayment_2 " +
			//        "WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
			//        "ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ " +
			//        "SELECT rn % 2 AS 'RowNum',* FROM #TEMP " +
			//        "DROP TABLE #TEMP";
			//SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
			//DataTable dt = new DataTable();

			//adapter.Fill(dt);

			//ReportDataSource rds = new ReportDataSource("DataSet1", dt);
			//ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyLabel.rdlc");
			//ReportParameterCollection reportParameters = new ReportParameterCollection();
			//this.ReportViewer1.LocalReport.SetParameters(reportParameters);
			//ReportViewer1.LocalReport.DataSources.Clear();
			//ReportViewer1.LocalReport.DataSources.Add(rds);

			//conn.Close();
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			ReportViewer1.Visible = true;
            DataGrid1.Visible = false;
            connection();
            conn.Open();

            //       string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ ASC) AS 'rn', " +
            //               "companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
            //               "companyPayment.expiredDate, " +
            //               "companyMember.represNmE, companyMember.represPosition, companyMember.address " +
            //               "INTO #TEMP " +
            //               "FROM companyMember LEFT OUTER JOIN companyPayment " +
            //               "ON ( companyMember.companyId = companyPayment.companyId ) " +
            //"WHERE companyMember.memberStatus = 'A' AND CompanyPayment.Deleted_at IS NULL ";


            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ ASC) AS 'rn', " +
                    "companyMember.flagPrintLabel, companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
                    "companyPayment.expiredDate, " +
                    "companyMember.represNmE, companyMember.represPosition, companyMember.address " +
                    "INTO #TEMP " +
                    "FROM companyMember LEFT OUTER JOIN companyPayment " +
                    "ON ( companyMember.companyId = companyPayment.companyId ) " +
                    "WHERE companyMember.memberStatus = 'A' ";

            string wherenme = "";
            foreach (GridViewRow row in DataGrid1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                    bool chk = chkRow.Checked;
                    if (chk||!chk)
                    {

                        /* old code   commentted for test  20/05/2025 14:19 
                        wherenme += "'" + row.Cells[2].Text + "',";
                        */

                        //new code 15/05/2025 15:09
                        string companyName = HttpUtility.HtmlDecode(row.Cells[2].Text);
                        System.Diagnostics.Debug.WriteLine("ชื่อบริษัท after decode: " + companyName);

                        wherenme += "'" + companyName.Replace("'", "''") + "',";
                        //end debug

                    }
                }
            }
            System.Diagnostics.Debug.Print(wherenme.Length.ToString());
            try
            {
                wherenme = wherenme.Remove(wherenme.Length - 1, 1);
                wherenme = "AND CompanyMember.companyNmE in (" + wherenme + ") ";
                sql += wherenme;
                sql += "AND companyMember.sendType <> 'Z' " +
                        "AND companyPayment.newsFee IS NOT NULL " +
                        "AND companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
                        "FROM companyPayment companyPayment_2 " +
                        "WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
                        "ORDER BY CompanyMember.companyNmE,CompanyMember.companyNmJ " +
                        "SELECT rn % 2 AS 'RowNum',* FROM #TEMP " +
                        "DROP TABLE #TEMP";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				string activityDetail = $"Created a table name #temp then Removed a table name #temp successful (user name = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
				DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyLabel.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
			catch (SqlException ex)
			{
				string activityDetail = $"Created a table name #temp then Removed a table name #temp unsuccessful [{ex.Message}] (user name = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Created a table name #temp then Removed a table name #temp unsuccessful [{ex.Message}] (user name = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			conn.Close();
        }
    }
}