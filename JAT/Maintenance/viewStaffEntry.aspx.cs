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



namespace JAT.Maintenance
{
    public partial class viewStaffEntry : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        string showMem;

		private LogActivity logActivity = new LogActivity();

		protected void Page_Load(object sender, EventArgs e)
        {
            showMem = Request.QueryString["staffID"];
            if (showMem != null)
            {
                if (!Page.IsPostBack) 
                {
                    BindData();
                }
            }
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;
            string sql = "SELECT * FROM SStaff WHERE staffID = '" + showMem + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //show in member information
                    staffFName.Value = rd.GetValue(3).ToString();
                    staffEmail.Value = rd.GetValue(4).ToString();
                    string v = rd.GetValue(5).ToString();
                    if (v == "1")
                    {
                        v = "Sathorn";
                        staffBranch.SelectedValue = v;
                    }
                    else if (v == "2")
                    {
                        v = "Sukhumvit";
                        staffBranch.SelectedValue = v;
                    }
                    //staffBranch.SelectedValue = rd.GetValue(5).ToString();
                    staffName.Value = rd.GetValue(1).ToString();
                    string a = rd.GetValue(8).ToString();
                    rdbAuthority.SelectedValue = a;
                }

            }
            catch { }

        }
        protected void BindData2()
        {
            connection();

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			var name = staffFName.Value;
            var branch="";
            if (staffBranch.SelectedValue == "Sathorn")
            {
                branch = "1";
            }
            else if (staffBranch.SelectedValue == "Sukhumvit")
            {
                branch = "2";
            }
            //cmd = new SqlCommand("UPDATE SStaff SET staffFName = '" + name + "'  WHERE staffID = '" + companyId + "'", conn);
            cmd = new SqlCommand("UPDATE SStaff SET staffFName = '" + name + "'," +
                                "staffEmail = '" + staffEmail.Value + "'," +
                                "staffBranch= '" + branch + "',staffName ='" + staffName.Value + "',authority ='" + rdbAuthority.SelectedValue.ToString() + "'" +
                                "WHERE staffID = '" + showMem + "'", conn);
            conn.Open();

            try
            {
				cmd.ExecuteNonQuery();
				string script1 = "alert(\"DATA UPDATED Complete!!\");";
				ScriptManager.RegisterStartupScript(this, GetType(),
									  "ServerControlScript", script1, true);
				string activityDetail = $"Changed data in a table 'SStaff' where staffid is '{showMem}' successful (User id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
				string activityDetail = $"Changed data in a table 'SStaff' where staffid is '{showMem}' unsuccessful [{ex.Message}] (User id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Changed data in a table 'SStaff' where staffid is '{showMem}' unsuccessful [{ex.Message}] (User id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			//int k = cmd.ExecuteNonQuery();
			//if (k != 0)
			//{
			//    string script1 = "alert(\"DATA UPDATED Complete!!\");";
			//    ScriptManager.RegisterStartupScript(this, GetType(),
			//                          "ServerControlScript", script1, true);
			//}
			conn.Close();
        }
        protected void update_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                BindData2();
                string script1 = "alert(\"DATA UPDATED Complete!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script1, true);
                Response.Redirect("viewStaff.aspx");
            }
            else
            {
                //Response.Redirect("viewStaffAdd.aspx");
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewStaff.aspx");
        }
    }
}