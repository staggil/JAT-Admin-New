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
using JAT.Core;


namespace JAT.Admin.CancelMember
{
    public partial class checkCancelMember : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();

		private string showfristMem;
        private string cancelid;

        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));
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

        protected void Page_Load(object sender, EventArgs e)
        {

            cancelid = Request.QueryString["cancelid"];
            showfristMem = Request.QueryString["firstmemberid"];
            checkstatus();

            Label4.Visible = false;
            Label5.Visible = false;

            if (showfristMem != null || cancelid != null)
            {
                BindData();
                BindDataMode();
                if (DropDownList1.SelectedValue == "Cancel some family member")
                {

                    showInGridCanMem();

                }
                checkstatus();
            }
        }
        protected void checkstatus()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT cancel_status FROM CancelPrivate WHERE cancel_id = '" + cancelid + "'";
            string status = "";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();



                while (rd.Read())
                {
                    status = rd.GetValue(0).ToString();

                }

            }
            catch { }

            conn.Close();
            if (status != "WT")
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
                showGridAP();
            }
        }
        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT nameJ, CONCAT(prefixNm, nameE),firstmemberid  FROM PrivateDetail " +
                         "WHERE firstmemberid = '" + showfristMem + "'" + " AND memberid = firstmemberid";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();



                while (rd.Read())
                {
                    Label1.Text = rd.GetValue(0).ToString();
                    Label2.Text = rd.GetValue(1).ToString();
                    Label3.Text = rd.GetValue(2).ToString();
                }

            }
            catch { }

            conn.Close();
        }
        protected void BindDataMode()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT cancel_type FROM CancelPrivate WHERE cancel_id = '" + cancelid + "'";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();



                while (rd.Read())
                {
                    DropDownList1.SelectedValue = rd.GetValue(0).ToString();

                }

            }
            catch { }

            conn.Close();
        }
        protected void cancelAllMember()
        {
            DataTable td;
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			try
            {
				td = SelectSqlTable("UPDATE PrivateDetail " +
								"SET cancelledDate = CURRENT_TIMESTAMP, memberStatus = " + "'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
								 "WHERE firstmemberid = '" + showfristMem + "' AND cancelledDate IS NULL");
                string activityDetail = $"Updated data in table 'PrivateDetail' where firstmemberid = '{showfristMem}' and cancelledDate is NULL successful (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
                string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
            {
                string activityDetail = $@"Error: {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}

        }
        protected void cancelAllFamily()
        {
            DataTable td;
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			
			try
			{
				td = SelectSqlTable("UPDATE PrivateDetail " +
								"SET cancelledDate = CURRENT_TIMESTAMP, memberStatus = " + "'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
								 "WHERE firstmemberid = '" + showfristMem + "'" + "AND firstmemberid != memberid AND cancelledDate IS NULL");

				string activityDetail = $"Updated data in table 'PrivateDetail' where firstmemberid = '{showfristMem}' and firstmemberid != memberid and cancelledDate is NULL successful (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
                string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
                string activityDetail = $@"Error: {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
		}

		//protected void approveBtn_Click(object sender, EventArgs e)
  //      {
		//	Button clickedButton = (Button)sender;
		//	string buttonValue = clickedButton.CommandArgument;

		//	String tmp = "";
		//	String btn_type = "approve";
		//	if (DropDownList1.SelectedValue == "Cancel all member")
		//	{
		//		tmp = "All members have been cancelled.";
		//		cancelAllMember();
		//		updateStatus(btn_type);
		//		Label3.Visible = true;
		//	}

		//	else if (DropDownList1.SelectedValue == "Cancel all family member")
		//	{
		//		tmp = "All family members have been cancelled.";
		//		cancelAllFamily();
		//		updateStatus(btn_type);
		//		Label4.Visible = true;
		//	}
		//	else if (DropDownList1.SelectedValue == "Cancel some family member")
		//	{
		//		Label5.Visible = true;
		//		tmp = "Some family members have been cancelled.";
		//		showInGridCanMem();
		//		changesomemem();
		//		updateStatus(btn_type);
		//	}

		//	if (tmp != "")
		//	{
		//		string strJavaScript = "<script language='javascript'>alert('" + tmp + "');</script>";
		//		//Page.RegisterStartupScript("Msgbox", strJavaScript);
		//		ClientScriptManager cs = Page.ClientScript;
		//		if (!cs.IsStartupScriptRegistered("Msgbox"))
		//		{
		//			cs.RegisterStartupScript(this.GetType(), "Msgbox", strJavaScript, true);
		//		}
		//	}
		//}

		//protected void rejectBtn_Click(object sender, EventArgs e)
		//{
		//	Button clickedButton = (Button)sender;
		//	string buttonValue = clickedButton.CommandArgument;

		//	String tmp = "";
  //          String btn_type = "reject";
		//	if (DropDownList1.SelectedValue == "Cancel all member")
		//	{
		//		tmp = "All members have been cancelled.";
		//		cancelAllMember();
		//		updateStatus(btn_type);
		//		Label3.Visible = true;
		//	}

		//	else if (DropDownList1.SelectedValue == "Cancel all family member")
		//	{
		//		tmp = "All family members have been cancelled.";
		//		cancelAllFamily();
		//		updateStatus(btn_type);
		//		Label4.Visible = true;
		//	}
		//	else if (DropDownList1.SelectedValue == "Cancel some family member")
		//	{
		//		Label5.Visible = true;
		//		tmp = "Some family members have been cancelled.";
		//		showInGridCanMem();
		//		changesomemem();
		//		updateStatus(btn_type);
		//	}

		//	if (tmp != "")
		//	{
		//		string strJavaScript = "<script language='javascript'>alert('" + tmp + "');</script>";
		//		//Page.RegisterStartupScript("Msgbox", strJavaScript);
		//		ClientScriptManager cs = Page.ClientScript;
		//		if (!cs.IsStartupScriptRegistered("Msgbox"))
		//		{
		//			cs.RegisterStartupScript(this.GetType(), "Msgbox", strJavaScript, true);
		//		}
		//	}
		//}
		protected void cancelMemberPressed(object sender, EventArgs e)
		{
			Button clickedButton = (Button)sender;
			string buttonValue = clickedButton.CommandArgument;

			String tmp = "";
			if (DropDownList1.SelectedValue == "Cancel all member")
			{
				tmp = "All members have been cancelled.";
				cancelAllMember();
				updateStatus(buttonValue);
				Label3.Visible = true;
			}

			else if (DropDownList1.SelectedValue == "Cancel all family member")
			{
				tmp = "All family members have been cancelled.";
				cancelAllFamily();
				updateStatus(buttonValue);
				Label4.Visible = true;
			}
			else if (DropDownList1.SelectedValue == "Cancel some family member")
			{
				Label5.Visible = true;
				tmp = "Some family members have been cancelled.";
				showInGridCanMem();
				changesomemem();
				updateStatus(buttonValue);
			}

			if (tmp != "")
			{
				string strJavaScript = "<script language='javascript'>alert('" + tmp + "');</script>";
				//Page.RegisterStartupScript("Msgbox", strJavaScript);
				ClientScriptManager cs = Page.ClientScript;
				if (!cs.IsStartupScriptRegistered("Msgbox"))
				{
					cs.RegisterStartupScript(this.GetType(), "Msgbox", strJavaScript, true);
				}
			}
		}

        protected void updateStatus(string btnValue)
        {
            DataTable td;
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            if (btnValue == "approve")
            {
				try
				{
					td = SelectSqlTable("UPDATE CancelPrivate SET cancel_status = 'AP' ,staff_id = '" + Session["UID"] + "',staff_date = '" + toDayDate + "' WHERE cancel_id = '" + cancelid + "'");
					string activityDetail = $"Changed value in table 'CancelPrivate' where cancel_id is '{cancelid}' success (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
				{
                    string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
                    string activityDetail = $@"Error: {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
				}
			}
			else if (btnValue == "reject")
            {
				try
				{
					td = SelectSqlTable("UPDATE CancelPrivate SET cancel_status = 'RJ' ,staff_id = '" + Session["UID"] + "',staff_date = '" + toDayDate + "' WHERE cancel_id = '" + cancelid + "'");
					string activityDetail = $"Changed value in table 'CancelPrivate' where cancel_id is '{cancelid}' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
				{
                    string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
                    string activityDetail = $@"Error: {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
				}
			}
            
            Button1.Enabled = false;
            Button2.Enabled = false;
            showGridAP();
        }
        protected void changesomemem()
        {
            DataTable td;
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            string activityDetail = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
				{
                    try
                    {
						td = SelectSqlTable("UPDATE PrivateDetail " +
									"SET cancelledDate = " + "'" + toDayDate + "'" + "," + "memberStatus = 'NA'" + "," + "updatedBy = " + Session["UID"] + " " +
									 "WHERE firstmemberid = '" + showfristMem + "'" + "AND memberid = '" + GridView1.Rows[i].Cells[0].Text + "'");
						activityDetail = $"Changed value in table 'PrivateDetail' where firstmemberid is '{showfristMem}' and memberid is '{GridView1.Rows[i].Cells[0].Text}' successful (User id = '{staffID}')";
					
					}
					catch (SqlException ex)
					{
						activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
						break;
					}
					catch (Exception ex)
					{
						activityDetail = $@"Error: {ex.Message} in {this}";
						break;
					}
				}
			logActivity.LogStaffActivity(staffID, activityDetail);
		}
        protected void showInGridCanMem()
        {
            DataTable td;

            td = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, memberType, memberStatus  " +
                "FROM CancelPrivate " +
                "INNER JOIN PrivateDetail ON CancelPrivate.cancel_member = PrivateDetail.firstmemberid " +
                "WHERE cancel_id = '" + cancelid + "'  AND firstmemberid = '" + showfristMem + "' AND firstmemberid != memberid  AND cancelledDate IS NULL");

            GridView1.DataSource = td;

            GridView1.DataBind();

            conn.Close();

        }
        protected void showGridAP()
        {
            DataTable td;

            td = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, memberType, memberStatus  " +
                "FROM CancelPrivate " +
                "INNER JOIN CancleMember ON  CancelPrivate.cancel_id = CancleMember.cancle_id " +
                "INNER JOIN PrivateDetail ON PrivateDetail.memberid = CancleMember.DelMem " +
                "WHERE cancel_id = '" + cancelid + "' ");

            GridView1.DataSource = td;

            GridView1.DataBind();

            conn.Close();

        }
    }
}