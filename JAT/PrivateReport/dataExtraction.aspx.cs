using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

namespace JAT.PrivateReport
{
    public partial class dataExtraction : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
        private SqlCommand cmd;

        //private ReportDocument repSource = new ReportDocument();

        //private ReportDocument reportDocument;

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
            if (!IsPostBack)
            {
                inputdiv2.Visible = true;
                inputdiv3.Visible = false;
            }
        }

        protected void DataExtraction_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
				if (Page.IsValid)
				{
					string sql = "SELECT ROW_NUMBER() OVER(PARTITION BY memberid ORDER BY childid) AS 'rn',memberid,childid,birthdate INTO #TEMP FROM PrivateChild " +
							"SELECT pd.memberid AS 'Member ID',prefixNm AS 'Honorific Title',CAST(birthDate AS DATE) AS 'Birth Date', " +
							"year(birthDate) AS 'Birth Year', " +
							"CASE WHEN DATEDIFF(year,birthdate,GETDATE())<0 THEN 0 ELSE DATEDIFF(year,birthdate,GETDATE()) END AS 'Age', " +
							"CAST(appliedDate AS DATE) AS 'Applied Date',year(appliedDate) AS 'Applied Year', " +
							"DATEDIFF(year,appliedDate,GETDATE()) AS 'Years of enrollment',memberType AS 'Member Type', " +
							"birthPlace AS 'Birthplace', " +
							"(SELECT COUNT(childid) FROM PrivateChild WHERE memberid=pd.memberid) AS 'Number of Children', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=1) AS 'Age of each Children', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=2) AS 'children2', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=3) AS 'children3', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=4) AS 'children4', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=5) AS 'children5', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=6) AS 'children6', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=7) AS 'children7', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=8) AS 'children8', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=9) AS 'children9', " +
							"(SELECT DATEDIFF(year,birthdate,GETDATE()) FROM #TEMP WHERE memberid=pd.memberid AND rn=10) AS 'children10' " +
							"FROM PrivateDetail pd INNER JOIN Private p ON p.memberid=pd.memberid ";
					string whereclause = "";
					whereclause = "WHERE memberStatus='" + MembershipStatus.SelectedValue + "' ";
					if (birthplace1.SelectedValue.ToString() != "")
					{
						whereclause += "AND birthPlace='" + birthplace1.SelectedValue.ToString() + "' ";
					}
					if (ExtractionPeriod.SelectedValue == "Age" && age_fr.Text != "" || age_to.Text != "")
					{
						if (age_fr.Text.Trim() == "" && age_to.Text.Trim() != "")
						{
							whereclause += "AND DATEDIFF(year,birthdate,GETDATE()) <= " + age_to.Text;
						}
						else if (age_fr.Text.Trim() != "" && age_to.Text.Trim() == "")
						{
							whereclause += "AND DATEDIFF(year,birthdate,GETDATE()) >= " + age_fr.Text;
						}
						else
						{
							whereclause += "AND DATEDIFF(year,birthdate,GETDATE()) BETWEEN '" + age_fr.Text + "' AND '" + age_to.Text + "' ";
						}
					}
					else if (ExtractionPeriod.SelectedValue == "Applied Date" && date_fr.Text != "" || date_to.Text != "")
					{
						if (date_fr.Text.Trim() == "" && date_to.Text.Trim() != "")
						{
							var tmp = date_to.Text.Split('-');
							var formattedDateTo = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
							whereclause += "AND appliedDate <= '" + formattedDateTo + "' ";
						}
						else if (date_fr.Text.Trim() != "" && date_to.Text.Trim() == "")
						{
							var tmp = date_fr.Text.Split('-');
							var formattedDateFr = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
							whereclause += "AND appliedDate >= '" + formattedDateFr + "' ";
						}
						else
						{
							var tmp = date_fr.Text.Split('-');
							var formattedDateFr = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
							tmp = date_to.Text.Split('-');
							var formattedDateTo = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
							whereclause += "AND appliedDate BETWEEN '" + formattedDateFr + "' AND '" + formattedDateTo + "' ";
						}
					}
					whereclause += "DROP TABLE #TEMP";
					sql += whereclause;
					connection();
					conn.Open();
					SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
					string activityDetail = $"Create table '#TEMP', Insert new data into table '#TEMP' and Drop table #TEMP successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
					DataTable dt = new DataTable();
					adapter.SelectCommand.CommandTimeout = 1800;
					adapter.Fill(dt);
					var dataTable = dt;

					conn.Close();


					StringBuilder builder = new StringBuilder();
					List<string> columnNames = new List<string>();
					List<string> rows = new List<string>();
					foreach (DataColumn column in dataTable.Columns)
					{
						columnNames.Add(column.ColumnName);
						if (column.ColumnName == "children5")
						{
							break;
						}
					}
					builder.Append(string.Join(",", columnNames.ToArray())).Append("\n");
					foreach (DataRow row in dataTable.Rows)
					{
						List<string> currentRow = new List<string>();
						foreach (DataColumn column in dataTable.Columns)
						{
							object item = row[column];
							currentRow.Add(item.ToString());
						}
						rows.Add(string.Join(",", currentRow.ToArray()));
					}
					builder.Append(string.Join("\n", rows.ToArray()));
					Response.Clear();
					Response.ContentType = "text/csv";
					Response.AddHeader("Content-Disposition", "attachment;filename=Data.csv");
					Response.Write('\uFEFF');
					Response.Write(builder.ToString());
					HttpContext.Current.ApplicationInstance.CompleteRequest();
				}
            }
            catch (SqlException ex)
			{
				string activityDetail = $"Create table '#TEMP', Insert new data into table '#TEMP' and Drop table '#TEMP' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);

			}
            catch (Exception ex)
			{
				string activityDetail = $"Create table '#TEMP', Insert new data into table '#TEMP' and Drop table '#TEMP' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);

			}
        }

        protected void ExtractionPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ExtractionPeriod.SelectedValue == "Age")
            {
                inputdiv2.Visible = true;
                inputdiv3.Visible = false;
            }
            else if (ExtractionPeriod.SelectedValue == "Applied Date")
            {
                inputdiv2.Visible = false;
                inputdiv3.Visible = true;
            }
            else
            {
                inputdiv2.Visible = false;
                inputdiv3.Visible = false;
            }
        }

        protected void CustomValidate1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ExtractionPeriod.SelectedValue == "Age")
            {
                outputdiv1.Visible = true;
                if (age_fr.Text.Trim() == "" || age_to.Text.Trim() == "")
                {
                    if (age_fr.Text.Trim() != "" && age_to.Text.Trim() == "")
                    {
                        Int16 intchk;
                        if (!Int16.TryParse(age_fr.Text, out intchk))
                        {
                            args.IsValid = false;
                        }
                    }
                    else if (age_fr.Text.Trim() == "" && age_to.Text.Trim() != "")
                    {
                        Int16 intchk;
                        if (!Int16.TryParse(age_to.Text, out intchk))
                        {
                            args.IsValid = false;
                        }
                    }
                }
                else
                {
                    Int16 intchk;
                    if (!Int16.TryParse(age_fr.Text, out intchk) || !Int16.TryParse(age_to.Text, out intchk))
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        try
                        {
                            var startAge = Int16.Parse(age_fr.Text);
                            var endAge = Int16.Parse(age_to.Text);
                            if (endAge < startAge)
                            {
                                args.IsValid = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            args.IsValid = false;
                        }
                    }
                }
            }
            else if (ExtractionPeriod.SelectedValue == "Applied Date")
            {
                outputdiv1.Visible = true;
                if (date_fr.Text.Trim() == "" || date_to.Text.Trim() == "")
                {
                    if (date_fr.Text.Trim() != "" && date_to.Text.Trim() == "")
                    {
                        try
                        {
                            var tmp = date_fr.Text.Split('-');
                            var formattedDateFr = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
                            DateTime datechk;
                            if (!DateTime.TryParse(formattedDateFr, out datechk))
                            {
                                args.IsValid = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            args.IsValid = false;
                        }
                    }
                    else if (date_fr.Text.Trim() == "" && date_to.Text.Trim() != "")
                    {
                        try
                        {
                            var tmp = date_to.Text.Split('-');
                            var formattedDateTo = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
                            DateTime datechk;
                            if (!DateTime.TryParse(formattedDateTo, out datechk))
                            {
                                args.IsValid = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            args.IsValid = false;
                        }
                    }
                }
                else
                {
                    try
                    {
                        var tmp = date_fr.Text.Split('-');
                        var formattedDateFr = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
                        tmp = date_to.Text.Split('-');
                        var formattedDateTo = tmp[1] + "-" + tmp[0] + "-" + tmp[2];
                        DateTime datechk;
                        DateTime startDate = DateTime.Parse("01-01-1900");
                        DateTime endDate = DateTime.Parse("01-01-1900");
                        if (!DateTime.TryParse(formattedDateFr, out datechk) || !DateTime.TryParse(formattedDateTo, out datechk))
                        {
                            args.IsValid = false;
                        }
                        else
                        {
                            startDate = DateTime.Parse(formattedDateFr);
                            endDate = DateTime.Parse(formattedDateTo);
                            if (endDate < startDate)
                            {
                                args.IsValid = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        args.IsValid = false;
                    }
                }
            }
            else
            {
                outputdiv1.Visible = false;
                args.IsValid = false;
            }
        }
    }
}