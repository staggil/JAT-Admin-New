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


		/* commentted 
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


		protected void Page_Load(object sender, EventArgs e)
        {
			//Session.Clear();
		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            var oldID = oldMemID.Value?.Trim();
            var newID = newMemID.Value?.Trim();

            if (string.IsNullOrEmpty(oldID) || string.IsNullOrEmpty(newID))
            {
                ScriptManager.RegisterClientScriptBlock(
                    this, GetType(), "alertMessage",
                    "alert('Member id is empty.')", true);
                return;
            }

            if (oldID == newID)
            {
                ScriptManager.RegisterClientScriptBlock(
                    this, GetType(), "alertMessage",
                    "alert('Old member id can not same as new member id.')", true);
                return;
            }

            try
            {
                var connStr = WebConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var repo = new PrivateChangeMemberRepository(connStr);
                repo.ChangeMemberId(oldID, newID, staffID);

                ScriptManager.RegisterClientScriptBlock(
                    this, GetType(), "alertMessage",
                    $"alert('Member id : {oldID} change to : {newID}')", true);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(
                    this, GetType(), "alertMessage",
                    $"alert('Member id : {oldID} not found.')", true);
            }
        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
			Page.Response.Redirect(Page.Request.Url.ToString(), false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}