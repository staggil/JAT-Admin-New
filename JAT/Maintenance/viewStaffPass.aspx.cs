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

namespace JAT.Maintenance
{
    public partial class viewStaffPass : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        string showMem;

		private LogActivity logActivity = new LogActivity();
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
            showMem = Request.QueryString["staffID"];
            if (showMem != null)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            //connection();
            //SqlCommand sc;
            //SqlDataReader rd;
            //string sql = "SELECT * FROM SStaff WHERE staffID = '" + showMem + "'";
            //try
            //{
            //    conn.Open();
            //    sc = new SqlCommand(sql, conn);
            //    rd = sc.ExecuteReader();
            //    while (rd.Read())
            //    {
            //        //show in member information
            //        staffName.Text = rd.GetValue(3).ToString();
            //    }
            //    conn.Close();

            //}
            //catch { }

            //if (staffPass.Value != "" && staffPass.Value == password.Value)
            //{
            //    //cmd = new SqlCommand("UPDATE SStaff SET staffPass = '" + staffPass.Value + "'  WHERE staffID = '" + showMem + "' AND staffPass = '" + staffPass.Value + "' ", conn);
            //    cmd = new SqlCommand("UPDATE SStaff SET staffPass = '" + staffPass.Value + "'  WHERE staffID = '" + showMem + "' AND staffPass = '" + passOld.Value + "'", conn);
            //    conn.Open();
            //    int k = cmd.ExecuteNonQuery();
            //    if (k != 0)
            //    {
            //        //Response.Write("alert('DATA UPDATED Complete!!')");
            //        string script1 = "alert(\"DATA UPDATED Complete!!\");";
            //        ScriptManager.RegisterStartupScript(this, GetType(),
            //                              "ServerControlScript", script1, true);
            //    }
            //    else
            //    {
            //        //Response.Write("alert('DATA UPDATED NOT Complete!!')");
            //        //string script2 = "alert(\"DATA UPDATED NOT Complete!!!\");";
            //        //ScriptManager.RegisterStartupScript(this, GetType(),
            //        //					  "ServerControlScript", script2, true);
            //        var t = staffPass.Value;
            //        string script3 = "alert('" + t + "');";
            //        ScriptManager.RegisterStartupScript(this, GetType(),
            //                              "ServerControlScript", script3, true);
            //    }
            //    conn.Close();
            //}
            //else if (staffPass.Value != password.Value)
            //{
            //    string script3 = "alert(\"Passwords Don't Match\");";
            //    ScriptManager.RegisterStartupScript(this, GetType(),
            //                          "ServerControlScript", script3, true);

            //}
            connection();
            SqlCommand sc;
            SqlDataReader rd;

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string sql = "SELECT * FROM SStaff WHERE staffID = '" + showMem + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //show in member information
                    staffName.Text = rd.GetValue(3).ToString();
                }
                conn.Close();

            }
            catch { }

            if (staffPass.Value != "" && staffPass.Value == password.Value)
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    cmd = new SqlCommand("UPDATE SStaff SET staffPass = '" + staffPass.Value + "'  WHERE staffID = '" + showMem + "' AND staffPass = '" + passOld.Value + "'", conn);
                    conn.Open();
                    try
                    {

						int k = cmd.ExecuteNonQuery();
						if (k != 0)
						{
							string script1 = "alert(\"DATA UPDATED Complete!!\");";
							ScriptManager.RegisterStartupScript(this, GetType(),
												  "ServerControlScript", script1, true);
							string activityDetail = $"Changed data in a table 'SStaff' where staffid is '{showMem}' and staffpass is '{passOld.Value}' successful (User id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
							Response.Redirect("viewStaff.aspx", false);
							
						}
						else
						{
							var t = staffPass.Value;
							string script3 = "alert('DATA UPDATED Not Complete!!');";
							ScriptManager.RegisterStartupScript(this, GetType(),
												  "ServerControlScript", script3, true);
							
							Page.Response.Redirect(Page.Request.Url.ToString(), false);
							string activityDetail = $"Changed data in a table 'SStaff' where staffid is '{showMem}' and staffpass is '{passOld.Value}' unsuccessful (User id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}

					}
                    catch (SqlException ex)
                    {
						string activityDetail = $"Changed data in a table 'SStaff' where staffid is ' {showMem} ' and staffpass is '{passOld.Value}' unsuccessful [{ex.Message}] (User id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Changed data in a table 'SStaff' where staffid is ' {showMem} ' and staffpass is '{passOld.Value}' unsuccessful [{ex.Message}] (User id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}


					conn.Close();
                }
                else
                {
                    //Response.Redirect("viewStaffAdd.aspx");
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
				staffPass.Value = "";
				password.Value = "";
			}
			else if (staffPass.Value != password.Value)
            {
                string script3 = "alert(\"Passwords Don't Match\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script3, true);

            }
            
            
        }

        protected void save_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewStaff.aspx");
        }
    }
}