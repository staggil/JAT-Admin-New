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
    public partial class reportLabel : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (drplstReportTyp.SelectedValue.ToString() == "Private")
            {
                drplstClub.Enabled = false;
                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else if (drplstReportTyp.SelectedValue.ToString() == "Club")
            {
                drplstClub.Enabled = true;
                datepicker1Input.Disabled = true;
                datepicker2Input.Disabled = true;
            }
            else
            {
                drplstClub.Enabled = false;
                datepicker1Input.Disabled = false;
                datepicker2Input.Disabled = false;
            }
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection2"];
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
                String strSqlSelect = "select d.firstmemberId,d.memberId, d.nameJ, (d.prefixNm + '' + d.nameE) as nameEng, isnull(ha.address, '') as homeAddress, " +
                "isnull(ca.companyNm, '') as companyNm, isnull(ca.address, '') as companyAddress , isnull(sendtype, '$') as sendType " +
                "from privateDetail d " +
                "left join privateAddress ha on d.firstmemberId = ha.memberId and ha.addressType = 1 " +
                "left join privateAddress ca on d.firstmemberId = ca.memberId and ca.addressType = 2 " +
                "left join private p on d.memberId = p.memberId left join privateClub c on d.memberId = c.memberId " +
                "where d.memberStatus = 'A' ";

            if (tbmemberid.Text.Trim() != "")
                strSqlSelect = strSqlSelect + " and d.memberId = '" + tbmemberid.Text.Trim() + "' ";


            if (tbname.Text.Trim() != "")
                strSqlSelect = strSqlSelect + " and d.nameJ like N'"+tbname.Text.Trim() + "%' ";


            if (tbnameE.Text.Trim() != "")
                strSqlSelect = strSqlSelect + " and d.nameE like '"+tbnameE.Text.Trim() + "%' ";

            if (drplstReportTyp.SelectedValue == "Private")
            {
                strSqlSelect = strSqlSelect + " and (p.sendType = '#' or p.sendType = '$') and d.memberType <> '3B' and d.firstmemberId = d.memberId ";
            }
            else if (drplstReportTyp.SelectedValue == "Club" && drplstClub.SelectedValue != "")
            {
                strSqlSelect = strSqlSelect + " and c." + drplstClub.SelectedValue + " = 1 ";
            }
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
            connection();
            conn.Open();

            string sql = "SELECT ROW_NUMBER() OVER(ORDER BY d.memberid) AS 'rn',d.firstmemberId,d.memberId, d.nameJ, " +
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
            sql += "SELECT rn % 2 AS 'RowNum',* FROM #TEMP " +
                    "DROP TABLE IF EXISTS #TEMP";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.CommandTimeout = 600;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateLabel.rdlc");
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            this.ReportViewer1.LocalReport.SetParameters(reportParameters);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);

            conn.Close();
        }

        protected void DataGrid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataGrid1.PageIndex = e.NewPageIndex;
            Binddata();
        }
    }
}