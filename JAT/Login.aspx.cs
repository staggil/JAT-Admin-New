using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using System.Data;
using System.Security.Cryptography;
using JAT.Core; // namespace ของ JAT.Core
namespace JAT
{
    public partial class Login : Page
    {
        private LogActivity logActivity = new LogActivity();
		private SqlConnection conn;
        private SqlCommand cmd;
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // เรียกใช้ Date_MsSqlStandard
                string date = Date_MsSqlStandard.Cast("24/12/2025");
                logActivity.LogStaffActivity(123, "User login attempt [Stub test]");

                // แสดงใน Label
                lblDate.Text = "วันที่: " + date;

                if (Session["User"] != null)
                {
                    Session["User"] = null;
                    Session["requestpassword"] = null;
                }
            }
        }


        public class LogActivity
        {
            public void LogStaffActivity(int staffID, string activityDetail)
            {
                // แค่แสดง debug แทน DB
                System.Diagnostics.Debug.WriteLine($"[Stub] StaffID: {staffID}, Activity: {activityDetail}");
            }
        }







        protected void LoginControl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            bool authenticated = this.ValidateCredentials(Login1.UserName, Login1.Password);

            if (authenticated)
            {
                Session["User"] = Login1.UserName;
                connection();
                SqlDataReader rd;
                string sql = "SELECT staffID,authority FROM SStaff WHERE staffName = '" + Login1.UserName + "'";
                try
                {
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Session["UID"] = rd.GetValue(0).ToString();
                        Session["Role"] = rd.GetValue(1).ToString();
                    }
                }
                catch { }
                finally
                {
                    if (conn != null) conn.Close();
                }
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
            }
        }
        private bool ValidateCredentials(string userName, string password)
        {
            string activityDetail;
			bool returnValue = false;

            try
            {



                string sql = "SELECT COUNT(*) as columnCount,staffID FROM SStaff WHERE staffName = @username AND staffPass = @password group by staffID";

                connection();
                cmd = new SqlCommand(sql, conn);

				SqlParameter user = new SqlParameter("@username", SqlDbType.VarChar);
				user.Value = userName.Trim();
                cmd.Parameters.Add(user);

				SqlParameter pass = new SqlParameter("@password", SqlDbType.VarChar);
				pass.Value = password.Trim();
                cmd.Parameters.Add(pass);

                conn.Open();

				using (var reader = cmd.ExecuteReader())
				{
                    bool checkData = reader.Read();
					if (checkData == true)
					{
						int count = reader.GetInt32(0); // Retrieve the count from the first column
						if (count > 0)
						{
							int userId = reader.GetInt32(1); // Retrieve the ID from the second column
							int staffID = userId != null ? Convert.ToInt32(userId) : 0;// Do something with the userId
							activityDetail = $"User login success (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
							returnValue = true;
						}
					}
                    else if(checkData == false)
                    {
                        returnValue = false;
                    }
				}
			}
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {
                // Log your error
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return returnValue;
        }
    }
}