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
    public partial class reportLabel : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();
		private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable td;
                td = SelectSqlTable("SELECT * FROM privateClubDetail");
                foreach (DataRow tmprow in td.Rows)
                {
                    switch (tmprow["itemNm"].ToString().Trim())
                    {
                        case "ev_tmp1": RadioButtonList1.Items[4].Text = tmprow["itemVal"].ToString(); break;
                        case "ev_tmp2": RadioButtonList1.Items[5].Text = tmprow["itemVal"].ToString(); break;
                        case "ev_tmp3": RadioButtonList1.Items[6].Text = tmprow["itemVal"].ToString(); break;
                        case "sub_tmp1": RadioButtonList2.Items[7].Text = tmprow["itemVal"].ToString(); break;
                        case "sub_tmp2": RadioButtonList2.Items[8].Text = tmprow["itemVal"].ToString(); break;
                    }
                }
                initLabel();
                ReportViewer1.Visible = false;
            }

            if (drplstReportTyp.SelectedValue.ToString() == "Private")
            {
                //drplstClub.Enabled = false;
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Club")
            {
                //drplstClub.Enabled = true;
                RadioButtonList1.Enabled = true;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Subcommittee")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = true;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Sukusuku")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = true;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Children")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = true;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Oversea")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = true;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;
                //drplstClub.Enabled = false;
                datepicker1Input.Disabled = false;
                datepicker2Input.Disabled = false;
            }
        }

        protected void initLabel()
        {
            if (RadioButtonList1.Items[4].Text.Trim() == "")
            {
                RadioButtonList1.Items[4].Enabled = false;
                RadioButtonList1.Items[4].Selected = false;
            }
            if (RadioButtonList1.Items[5].Text.Trim() == "")
            {
                RadioButtonList1.Items[5].Enabled = false;
                RadioButtonList1.Items[5].Selected = false;
            }
            if (RadioButtonList1.Items[6].Text.Trim() == "")
            {
                RadioButtonList1.Items[6].Enabled = false;
                RadioButtonList1.Items[6].Selected = false;
            }
            if (RadioButtonList2.Items[7].Text.Trim() == "")
            {
                RadioButtonList2.Items[7].Enabled = false;
                RadioButtonList2.Items[7].Selected = false;
            }
            if (RadioButtonList2.Items[8].Text.Trim() == "")
            {
                RadioButtonList2.Items[8].Enabled = false;
                RadioButtonList2.Items[8].Selected = false;
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

        protected void view_Click(object sender, EventArgs e)
        {
            Binddata();
            ReportViewer1.LocalReport.DataSources.Clear();
            DataGrid1.Visible = true;
            ReportViewer1.Visible = false;
        }

        protected void reset_Click(object sender, EventArgs e)
        {

        }

        private void Binddata()
        {
            DataGrid1.DataSource = GetData().Tables["PrivateReportLabel"].DefaultView;
            DataGrid1.DataBind();
        }

        private DataSet GetData()
        {
            IFormatProvider culture = new CultureInfo("en-US", true);
            connection();
            var DateFrom = datepicker1Input.Value.ToString();
            var DateTo = datepicker2Input.Value.ToString();
            ///this is sql statement which returns records
            ///
            String strSqlSelect = "SET dateformat dmy select d.firstmemberId,d.memberId, d.nameJ, (d.prefixNm + '' + d.nameE) as nameEng, isnull(ha.address, '') as homeAddress, " +
            "isnull(ca.companyNm, '') as companyNm, isnull(ca.address, '') as companyAddress , isnull(sendtype, '$') as sendType " +
            "from privateDetail d " +
            "left join privateAddress ha on d.firstmemberId = ha.memberId and ha.addressType = 1 " +
            "left join privateAddress ca on d.firstmemberId = ca.memberId and ca.addressType = 2 " +
            "left join private p on d.memberId = p.memberId left join privateClub c on d.memberId = c.memberId " +
            "where d.memberStatus = 'A' ";

            if (tbmemberid.Text.Trim() != "")
                strSqlSelect = strSqlSelect + " and d.memberId = '" + tbmemberid.Text.Trim() + "' ";


            if (tbname.Text.Trim() != "")
                strSqlSelect = strSqlSelect + " and d.nameJ like N'" + tbname.Text.Trim() + "%' ";


            if (tbnameE.Text.Trim() != "")
                strSqlSelect = strSqlSelect + " and d.nameE like '" + tbnameE.Text.Trim() + "%' ";

            if (drplstReportTyp.SelectedValue == "Private")
            {
                strSqlSelect = strSqlSelect + " and (p.sendType = '#' or p.sendType = '$') and d.memberType <> '3B' and d.firstmemberId = d.memberId and d.firstmemberId = d.memberId ";
            }
            else if (drplstReportTyp.SelectedValue == "Club" && RadioButtonList1.SelectedValue != "")
            {
                strSqlSelect = strSqlSelect + " and c." + RadioButtonList1.SelectedValue + " = 1 ";
            }
            else if (drplstReportTyp.SelectedValue == "Subcommittee" && RadioButtonList2.SelectedValue != "")
            {
                strSqlSelect = strSqlSelect + " and c." + RadioButtonList2.SelectedValue + " = 1 ";
            }
            else if (drplstReportTyp.SelectedValue == "Sukusuku" && RadioButtonList3.SelectedValue != "")
            {
                strSqlSelect = strSqlSelect + " and c." + RadioButtonList3.SelectedValue + " = 1 ";
            }
            else if (drplstReportTyp.SelectedValue == "Children" && RadioButtonList4.SelectedValue != "")
            {
                strSqlSelect = strSqlSelect + " and c." + RadioButtonList4.SelectedValue + " = 1 ";
            }
            else if (drplstReportTyp.SelectedValue == "Oversea" && RadioButtonList5.SelectedValue != "")
            {
                strSqlSelect = strSqlSelect + " and c." + RadioButtonList5.SelectedValue + " = 1 ";
            }
            //else if (drplstReportTyp.SelectedValue == "Club" && drplstClub.SelectedValue != "")
            //{
            //    strSqlSelect = strSqlSelect + " and c." + drplstClub.SelectedValue + " = 1 ";
            //}
            else if (drplstReportTyp.SelectedValue == "ChangeAddress")
            {
                if (DateFrom != "" && DateTo != "")
                {
                    strSqlSelect = strSqlSelect + " and d.firstmemberId = d.memberId and changeAddressDate between '" + DateFrom + "' and '" + DateTo + "' ";
                }
                else
                    strSqlSelect = strSqlSelect + " and d.firstmemberId = d.memberId ";
            }

            strSqlSelect = strSqlSelect + " order by d.nameE";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSqlSelect, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            dataAdapter.SelectCommand.CommandTimeout = 600;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "PrivateReportLabel");
            totaltxt.Text = "Search results returned <b>" + myDataSet.Tables["PrivateReportLabel"].Rows.Count.ToString() + "</b> row(s)";
            totaltxt.Visible = true;
            return myDataSet;
        }

        protected void print_Click(object sender, EventArgs e)
        {
            totaltxt.Visible = false;
            DataGrid1.Visible = false;
            ReportViewer1.Visible = true;
            connection();
            conn.Open();

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			totaltxt.Visible = false;
			DataGrid1.Visible = false;
			ReportViewer1.Visible = true;
			connection();
			conn.Open();
			try
			{
				string sql = "SELECT ROW_NUMBER() OVER(ORDER BY d.nameE) AS 'rn',d.firstmemberId,d.memberId, d.nameJ, " +
					"(d.prefixNm + '' + d.nameE) AS nameEng, isnull(ha.address, '') AS homeAddress, isnull(ca.companyNm, '') AS companyNm, isnull(ca.address, '') AS companyAddress , isnull(sendtype, '$') AS sendType " +
					"INTO #TEMP FROM privateDetail d LEFT JOIN privateAddress ha ON d.firstmemberId = ha.memberId and ha.addressType = 1 " +
					"LEFT JOIN privateAddress ca ON d.firstmemberId = ca.memberId AND ca.addressType = 2 " +
					"LEFT JOIN private p ON d.memberId = p.memberId LEFT JOIN privateClub c ON d.memberId = c.memberId " +
					"WHERE d.memberStatus = 'A' ";
				string wherememberid = "";
				foreach (GridViewRow row in DataGrid1.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
						bool chk = chkRow.Checked;
						if (chk)
						{
							wherememberid += "'" + row.Cells[1].Text + "',";
						}
					}
				}
				wherememberid = "AND d.memberId IN (" + wherememberid + "'') ";
				sql += wherememberid;
				sql += "SELECT (rn-1) / 15 AS 'PageGroup',rn % 3 AS 'RowNum',* FROM #TEMP ORDER BY rn " +
						"DROP TABLE IF EXISTS #TEMP";
				SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				DataTable dt = new DataTable();
				adapter.SelectCommand.CommandTimeout = 600;

				string activityDetail = $"Create and insert data in table '#TEMP' and drop table '#TEMP' if exist successful (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
				adapter.Fill(dt);

				ReportDataSource rds = new ReportDataSource("DataSet1", dt);
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateLabel.rdlc");
				ReportParameterCollection reportParameters = new ReportParameterCollection();
				this.ReportViewer1.LocalReport.SetParameters(reportParameters);

				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.DataSources.Add(rds);
			}
			catch (SqlException ex)
			{
				string activityDetail = $"Create and insert data in table '#TEMP' and drop table '#TEMP' if exist unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Create and insert data in table '#TEMP' and drop table '#TEMP' if exist unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			conn.Close();
        }

        protected void DataGrid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataGrid1.PageIndex = e.NewPageIndex;
            Binddata();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList2.ClearSelection();
            RadioButtonList3.ClearSelection();
            RadioButtonList4.ClearSelection();
            RadioButtonList5.ClearSelection();

        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.ClearSelection();
            RadioButtonList3.ClearSelection();
            RadioButtonList4.ClearSelection();
            RadioButtonList5.ClearSelection();
        }

        protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.ClearSelection();
            RadioButtonList2.ClearSelection();
            RadioButtonList4.ClearSelection();
            RadioButtonList5.ClearSelection();
        }

        protected void RadioButtonList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.ClearSelection();
            RadioButtonList2.ClearSelection();
            RadioButtonList3.ClearSelection();
            RadioButtonList5.ClearSelection();
        }

        protected void RadioButtonList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.ClearSelection();
            RadioButtonList2.ClearSelection();
            RadioButtonList3.ClearSelection();
            RadioButtonList4.ClearSelection();
        }

        protected void drplstReportTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drplstReportTyp.SelectedValue.ToString() == "Private")
            {
                //drplstClub.Enabled = false;
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Club")
            {
                //drplstClub.Enabled = true;
                RadioButtonList1.Enabled = true;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Subcommittee")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = true;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Sukusuku")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = true;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Children")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = true;
                RadioButtonList5.Enabled = false;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Oversea")
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = true;

                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else
            {
                RadioButtonList1.Enabled = false;
                RadioButtonList2.Enabled = false;
                RadioButtonList3.Enabled = false;
                RadioButtonList4.Enabled = false;
                RadioButtonList5.Enabled = false;
                //drplstClub.Enabled = false;
                datepicker1Input.Disabled = false;
                datepicker2Input.Disabled = false;
            }
        }
    }
}