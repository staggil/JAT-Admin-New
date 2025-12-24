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
    public partial class viewStaffAdd : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
		protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        protected void save_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			if (confirmValue == "Yes")
            {
                var branch = "";
                connection();
                if (password.Value == staffPass.Value)
                {
                    if (staffBranch.SelectedValue == "Sathorn")
                    {
                        branch = "1";
                    }
                    else if (staffBranch.SelectedValue == "Sukhumvit")
                    {
                        branch = "2";
                    }
                    cmd = new SqlCommand("INSERT INTO SStaff VALUES('" + staffName.Value + "','" + staffPass.Value + "','" + staffFName.Value + "','" + staffEmail.Value + "','" + branch + "',' ',' ','" + rdbAuthority.SelectedValue.ToString() + "')", conn);
                    conn.Open();
                    
                    try
                    {
						cmd.ExecuteNonQuery();
                        string activityDetail = $"Added new data into a table 'SStaff' successful (User id = {staffID})";
                        logActivity.LogStaffActivity(staffID, activityDetail);
					}
                    catch (SqlException ex)
                    {
						string activityDetail = $"Added new data into a table 'SStaff' unsuccessful [{ex.Message}] (User id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Added new data into a table 'SStaff' unsuccessful [{ex.Message}] (User id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					conn.Close();
                    Response.Redirect("viewStaff.aspx");
                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
            }
            else
            {
                //Response.Redirect("viewStaffAdd.aspx");
                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }


        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewStaff.aspx");
        }
    }
}