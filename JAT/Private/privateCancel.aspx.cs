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


namespace JAT.Private
{
    public partial class privateCancel : System.Web.UI.Page
    {

        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
		//public string postBackId;

		private string showfristMem;
        private string showMem;
        string addValue;

        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));

        protected void Page_Load(object sender, EventArgs e)
        {
            showfristMem = Request.QueryString["firstmemberid"];
            showMem = Request.QueryString["memberid"];

            addValue = Request.QueryString["mode"];

            Label3.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;

            if (showMem != null || showfristMem != null)
            {
                lastEditor();
                BindData();
            }
        }
        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT nameJ, CONCAT(prefixNm, nameE) " +
                            "FROM PrivateDetail " +
                            "WHERE firstmemberid = '" + showfristMem + "'" + "AND memberid = firstmemberid";
            //string sqlBrithPlace = "SELECT birthPlace FROM PrivateDetail WHERE memberid = '" + showMem +"'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in member information
                    Label1.Text = rd.GetValue(0).ToString();
                    Label2.Text = rd.GetValue(1).ToString();
                }

            }
            catch { }

            //Box2.Value = showVal;


            conn.Close();
        }
        private void lastEditor()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT top 1 " +
                "CASE " +
                "WHEN cancelledDate is not null THEN staffFName " +
                "END as cancelledBy " +
                "FROM PrivateDetail pd " +
                "left join SStaff ss on pd.updatedBy = ss.staffID " +
                "WHERE firstmemberid = '" + showfristMem + "' " +
                "order by cancelledDate desc ";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    updateBy.Text = rd.GetValue(0).ToString();
                }

            }
            catch { }
            finally
            {
                if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                {
                    updateBy.Text = "N/A";
                }
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
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.SelectCommand.CommandTimeout = 600;
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }

        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void paymentTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
            }
        }
        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void specialTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateFeature.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void showInGridCanMem()
        {
            DataTable td;

            //var memSatCho = DropDownList1.SelectedValue.ToString();
            //var memberTypeCho = RadioButtonList1.SelectedValue.ToString();


            td = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, memberType, memberStatus " +
                                "FROM PrivateDetail " +
                                "WHERE firstmemberid =" + "'" + showfristMem + "'" + "AND firstmemberid != memberid" + " AND cancelledDate IS NULL");

            GridView1.DataSource = td;

            GridView1.DataBind();

            conn.Close();

        }

        protected void processBtn_Click(object sender, EventArgs e)
        {
            String tmp = "";

            if (DropDownList1.SelectedValue == "Cancel all member")
            {
                tmp = "All members have been cancelled.";
                cancelAllMember();
                Label3.Visible = true;
            }
            else if (DropDownList1.SelectedValue == "Cancel first member")
            {
                DataTable rn_count = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, memberType, memberStatus " +
                                    "FROM PrivateDetail " +
                                    "WHERE firstmemberid =" + "'" + showfristMem + "'" + "AND firstmemberid != memberid" + " AND cancelledDate IS NULL");
                if (rn_count.Rows.Count > 0)
                {
                    GridView1.Columns[0].Visible = true;
                    GridView1.Columns[1].Visible = false;
                    showInGridCanMem();
                    Label5.Visible = true;
                }
                else
                {
                    tmp = "All members have been cancelled.";
                    cancelAllMember();
                    Label3.Visible = true;
                }
            }
            else if (DropDownList1.SelectedValue == "Cancel all family member")
            {
                tmp = "All family members have been cancelled.";
                cancelAllFamily();
                Label4.Visible = true;
            }
            else if (DropDownList1.SelectedValue == "Cancel some family member")
            {
                Label5.Visible = true;

                GridView1.Columns[1].Visible = true;
                GridView1.Columns[0].Visible = false;

                showInGridCanMem();
            }

            if (tmp != "")
            {
                string strJavaScript = "<script language='javascript'>alert('" + tmp + "');</script>";
                Page.RegisterStartupScript("Msgbox", strJavaScript);
            }
        }

        protected void cancelAllMember()
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			DataTable td;
            try
            {
				td = SelectSqlTable("UPDATE PrivateDetail " +
								"SET date_do_status_na_to_a = NULL, cancelledDate = CURRENT_TIMESTAMP, memberStatus = " + "'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
								 //"WHERE firstmemberid = '" + showfristMem + "' AND cancelledDate IS NULL");
								 "WHERE firstmemberid = '" + showfristMem + "' AND memberStatus = 'A'");
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND memberStatus is 'A' successful (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND memberStatus is 'A' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND memberStatus is 'A' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			//Response.Redirect("privateCancel.aspx?firstmemberid=" + showfristMem);
		}

        protected void cancelAllFamily()
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

            
			try
			{
				td = SelectSqlTable("UPDATE PrivateDetail " +
								"SET date_do_status_na_to_a = NULL, cancelledDate = CURRENT_TIMESTAMP, memberStatus = " + "'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
								 //"WHERE firstmemberid = '" + showfristMem + "'" + "AND firstmemberid != memberid AND cancelledDate IS NULL");
								 "WHERE firstmemberid = '" + showfristMem + "'" + "AND firstmemberid != memberid AND memberStatus = 'A' ");
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND firstmemberid in not memberid AND memberStatus is 'A' successful (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND firstmemberid is not memberid AND memberStatus is 'A' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND firstmemberid is not memberid AND memberStatus is 'A' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			//Response.Redirect("privateCancel.aspx?firstmemberid=" + showfristMem);
		}

        protected void GridView_Button_ChnageFrist(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;
            DataTable td_Del;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            DataTable ref_count = SelectSqlTable("SELECT * " +
                                    "FROM PrivateRefer " +
                                    "WHERE memberid =" + "'" + row.Cells[2].Text + "'");
            //if (ref_count.Rows.Count > 0)
            //{
            //    td_Del = SelectSqlTable("DELETE FROM PrivateDetail WHERE memberid = " + "'" + row.Cells[2].Text + "'");
            //    td = SelectSqlTable("exec psChangeFirstMember '" + showfristMem + "','" + row.Cells[2].Text + "','1'");

            //}

            //Label3.Text = row.Cells[1].Text;

            //td = SelectSqlTable("UPDATE PrivateDetail " +
            //                    "SET firstmemberid = '" + row.Cells[2].Text + "'" + "," + "updatedDate = " + "'" + toDayDate + "'" + "," + "updatedBy = " + Session["UID"] + " " +
            //                     "WHERE firstmemberid = '" + showfristMem + "'" + "AND memberid = '" + row.Cells[2].Text + "'" +
            //                     "UPDATE PrivateDetail " +
            //                     "SET firstmemberid = '" + row.Cells[2].Text + "'" + "," + "updatedBy = " + Session["UID"] + " " +
            //                     "WHERE firstmemberid = '" + showfristMem + "'" +
            //                     "UPDATE PrivateDetail " +
            //                     "SET firstmemberid = '" + row.Cells[2].Text + "'" + "," + "cancelledDate = '" + toDayDate + "'" + "," + "memberStatus = 'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
            //                     "WHERE firstmemberid = '" + row.Cells[2].Text + "'" + "AND memberid = '" + showfristMem + "'");
            
            try
            {
				td = SelectSqlTable("exec psChangeFirstMember '" + showfristMem + "','" + row.Cells[2].Text + "','1'");
				string activityDetail = $"Executed procedure name 'psChangeFirstMember' successful (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
				string activityDetail = $"Executed procedure name 'psChangeFirstMember' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Executed procedure name 'psChangeFirstMember' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			Response.Redirect("privateEntry.aspx?firstmemberid=" + row.Cells[2].Text);

        }
        protected void GridView_Button_ChnageSome(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            //Label3.Text = row.Cells[1].Text;
            try
            {
				td = SelectSqlTable("UPDATE PrivateDetail " +
												//"SET date_do_status_na_to_a = NULL, cancelledDate = " + "'" + toDayDate + "'" + "," + "memberStatus = 'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
												"SET date_do_status_na_to_a = NULL, cancelledDate = CURRENT_TIMESTAMP, " + "memberStatus = 'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
												 "WHERE firstmemberid = '" + showfristMem + "'" + "AND memberid = '" + row.Cells[2].Text + "'");
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND memberid is '{row.Cells[2].Text}' successful (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			catch (SqlException ex)
			{
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND memberid is '{row.Cells[2].Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Changed value in a table 'PrivateDetail' Where firstmemberid is '{showfristMem}' AND memberid is '{row.Cells[2].Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}


			Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);

        }
    }
}