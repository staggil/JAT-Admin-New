using System.Configuration;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Web;

namespace JAT
{
	public class LogActivity
	{
		public void LogStaffActivity(int staffIDValue, string activityDetailValue)
		{
			DateTime timestampValue = DateTime.Now;
			string machineLogValue = Environment.MachineName;
			string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			string userCom = this.GetipAddress();
			Object hostName = HttpContext.Current.Request;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string sqlQuery = "INSERT INTO StaffActivity ([staffID], [staffActivityDetail], [staffTimestamp], [machine_log]) VALUES (@StaffID, @StaffActivityDetail, @StaffTimestamp, @MachineLog)";

				using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
				{

					cmd.Parameters.AddWithValue("@StaffID", staffIDValue);
					cmd.Parameters.AddWithValue("@StaffActivityDetail", activityDetailValue);
					cmd.Parameters.AddWithValue("@StaffTimestamp", timestampValue);
					cmd.Parameters.AddWithValue("@MachineLog", userCom);

					cmd.ExecuteNonQuery();
				}
			}
		}

		protected string GetipAddress()
		{
			string IP4Address = String.Empty;
			foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
			{
				if (IPA.AddressFamily.ToString() == "InterNetwork")
				{
					IP4Address = $"{Dns.GetHostName()} (IPaddress = '{IPA}')";
					break;
				}
			}
			

			if (IP4Address != String.Empty)
			{
				return IP4Address;
			}

			foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
			{
				if (IPA.AddressFamily.ToString() == "InterNetwork")
				{
					IP4Address = $"{Dns.GetHostName()} (IPaddress = '{IPA}')";
					break;
				}
			}

			return IP4Address;
			//string strHostName = Request.UserHostAddress;
			//return strHostName;
		}
	}
}