using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JAT.Maintenance
{
    public partial class ChangePassword : System.Web.UI.Page
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
            //companyId = Request.QueryString["staffID"];
            //if (companyId != null)
            //{
            //    BindData();
            //}
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    //staffName.Text = Session["User"].ToString();

                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }
        protected void BindData()
        {
			//connection();

			//if (staffPass.Value != "" && staffPass.Value == password.Value)
			//{
			//    string confirmValue = Request.Form["confirm_value"];
			//    companyId = Session["UID"].ToString();
			//    if (confirmValue == "Yes")
			//    {
			//        cmd = new SqlCommand("UPDATE SStaff SET staffPass = '" + staffPass.Value + "'  WHERE staffID = '" + companyId + "' AND staffPass = '" + passOld.Value + "'", conn);
			//        conn.Open();
			//        int k = cmd.ExecuteNonQuery();
			//        if (k != 0)
			//        {
			//            string script1 = "alert(\"DATA UPDATED Complete!!\");";
			//            ScriptManager.RegisterStartupScript(this, GetType(),
			//                                  "ServerControlScript", script1, true);
			//            Response.Redirect("~/Login.aspx");
			//        }
			//        else
			//        {
			//            var t = staffPass.Value;
			//            string script3 = "alert('DATA UPDATED Not Complete!!');";
			//            ScriptManager.RegisterStartupScript(this, GetType(),
			//                                  "ServerControlScript", script3, true);
			//            Page.Response.Redirect(Page.Request.Url.ToString(), true);
			//        }
			//        conn.Close();
			//    }
			//    else
			//    {
			//        //Response.Redirect("viewStaffAdd.aspx");
			//        Page.Response.Redirect(Page.Request.Url.ToString(), true);
			//    }

			//}
			//else if (staffPass.Value != password.Value)
			//{
			//    string script3 = "alert(\"Passwords Don't Match\");";
			//    ScriptManager.RegisterStartupScript(this, GetType(),
			//                          "ServerControlScript", script3, true);

			//}
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			connection();

            if (staffPass.Value != "" && staffPass.Value == password.Value)
            {
                string confirmValue = Request.Form["confirm_value"];
                showMem = Session["UID"].ToString();

                string pass = "";
                DataTable dt = SelectSqlTable("SELECT TOP 1 * FROM Maintenance_Password");
                if (dt.Rows.Count > 0)
                {
                    pass = dt.Rows[0][0].ToString();
                }
                
                if (confirmValue == "Yes")
                {
                    cmd = new SqlCommand("UPDATE Maintenance_Password SET password = '" + staffPass.Value + "'  Where password = '" + passOld.Value + "'", conn);
                    conn.Open();
                    //int k = cmd.ExecuteNonQuery();
                    try
                    {
						cmd.ExecuteNonQuery();
                        string activityDetail = $"Changed value in a table 'Maintenance_Password' where old password is '{passOld.Value}' successful (User id = '{staffID}')'";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        string script1 = "alert(\"DATA UPDATED Complete!!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script1, true);
                        //Response.Redirect("~/Login.aspx");
                        Page.Response.Redirect(Page.Request.Url.ToString(), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    catch (SqlException ex)
                    {
                        string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        string script3 = "alert(\"Data Not Completed\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script3, true);
                        
                    }
					catch (Exception ex)
					{
                        string activityDetail = $@"Error: {ex.Message} in {this}";
						logActivity.LogStaffActivity(staffID, activityDetail);
						string script3 = "alert(\"Data Not Completed\");";
						ScriptManager.RegisterStartupScript(this, GetType(),
											  "ServerControlScript", script3, true);

					}
					conn.Close();
				}
                else
                {
                    //Response.Redirect("viewStaffAdd.aspx");
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }

            }
            else if (staffPass.Value != password.Value)
            {
				string activityDetail = $"Changed value in a table 'Maintenance_Password' where old password is '{passOld.Value}' unsuccessful (User id = '{staffID}')'";
				logActivity.LogStaffActivity(staffID, activityDetail);
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
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}