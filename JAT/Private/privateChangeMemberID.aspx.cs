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
using JAT.Core;

namespace JAT.Private
{
    public partial class privateChangeMemberID : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
		private SqlCommand cmd;
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

		protected void Page_Load(object sender, EventArgs e)
        {
			//Session.Clear();
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

			var oldID = oldMemID.Value.ToString();
			var newID = newMemID.Value.ToString();

			if (oldID == newID)
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Old member id can not same as new member id.')", true);
			}
			else
			{
				try
				{
					td = SelectSqlTable("UPDATE PrivateDetail " +
										"SET firstmemberid = CASE WHEN firstmemberid = memberid OR firstmemberid != memberid THEN '" + newID + "'" + "WHEN firstmemberid = NULL THEN firstmemberid ELSE firstmemberid END " +
										"WHERE firstmemberid = '" + oldID + "'" + 
										"UPDATE PrivateDetail " + 
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE Private " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivateAccount " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE privateAddress " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivateBoard " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivateChild " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivateClub " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivatePayment " +
										"SET memberid = '" + newID + "'," + "payBy = '" + newID + "'" + 
										"WHERE memberid = '" + oldID + "'" + "AND payBy = '" + oldID + "'"+
										"UPDATE PrivateRefer " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivateRemark " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'" +
										"UPDATE PrivateSendHistory " +
										"SET memberid = '" + newID + "'" +
										"WHERE memberid = '" + oldID + "'");

					string activityDetail = $"Changed data in 11 tables ('PrivateDetail, Private, PrivateAccount, privateAddress, PrivateBoard, PrivateChild, PrivateClub, PrivatePayment, PrivateRefer, PrivateRemark, PrivateSendHistory') where memberid is '{oldID}' successful (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);

					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member id : " + oldID + " " + "change to : " + newID + "'" + ")", true);

				}
				catch (SqlException ex)
				{
					string activityDetail = $"Changed data in 11 tables ('PrivateDetail, Private, PrivateAccount, privateAddress, PrivateBoard, PrivateChild, PrivateClub, PrivatePayment, PrivateRefer, PrivateRemark, PrivateSendHistory') where memberid is '{oldID}' unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member id : " + oldID + " " + "not found." + "'" + ")", true);
					
				}
				catch (Exception ex)
				{
					string activityDetail = $"Changed data in 11 tables ('PrivateDetail, Private, PrivateAccount, privateAddress, PrivateBoard, PrivateChild, PrivateClub, PrivatePayment, PrivateRefer, PrivateRemark, PrivateSendHistory') where memberid is '{oldID}' unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Member id : " + oldID + " " + "not found." + "'" + ")", true);

				}
			}

		}

        protected void Button1_Click1(object sender, EventArgs e)
        {
			Page.Response.Redirect(Page.Request.Url.ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}