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

        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
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
                    string sql = @"IF OBJECT_ID('tempdb..#TEMP') IS NOT NULL DROP TABLE #TEMP; 
                        SELECT ROW_NUMBER() OVER(PARTITION BY memberid ORDER BY childid) AS rn, memberid, childid, birthdate 
                        INTO #TEMP FROM PrivateChild;
                        
                        SELECT pd.memberid AS 'Member ID', prefixNm AS 'Honorific Title', 
                        CAST(birthDate AS DATE) AS 'Birth Date', YEAR(birthDate) AS 'Birth Year',
                        CASE WHEN DATEDIFF(year, birthdate, GETDATE()) < 0 THEN 0 ELSE DATEDIFF(year, birthdate, GETDATE()) END AS 'Age',
                        CAST(appliedDate AS DATE) AS 'Applied Date', YEAR(appliedDate) AS 'Applied Year',
                        DATEDIFF(year, appliedDate, GETDATE()) AS 'Years of enrollment', memberType AS 'Member Type',
                        birthPlace AS 'Birthplace', (SELECT COUNT(childid) FROM PrivateChild WHERE memberid=pd.memberid) AS 'Number of Children'
                        FROM PrivateDetail pd INNER JOIN Private p ON p.memberid=pd.memberid ";

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    string whereclause = "WHERE memberStatus = @memberStatus ";
                    parameters.Add(new SqlParameter("@memberStatus", MembershipStatus.SelectedValue));

                    if (!string.IsNullOrEmpty(birthplace1.SelectedValue))
                    {
                        whereclause += "AND birthPlace LIKE @birthPlace ";
                        parameters.Add(new SqlParameter("@birthPlace", "%" + birthplace1.SelectedValue + "%"));
                    }

                    if (ExtractionPeriod.SelectedValue == "Age" && (!string.IsNullOrEmpty(age_fr.Text) || !string.IsNullOrEmpty(age_to.Text)))
                    {
                        if (!string.IsNullOrEmpty(age_fr.Text) && !string.IsNullOrEmpty(age_to.Text))
                        {
                            whereclause += "AND DATEDIFF(year, birthdate, GETDATE()) BETWEEN @ageFrom AND @ageTo ";
                            parameters.Add(new SqlParameter("@ageFrom", age_fr.Text));
                            parameters.Add(new SqlParameter("@ageTo", age_to.Text));
                        }
                    }
                    else if (ExtractionPeriod.SelectedValue == "Applied Date" && (!string.IsNullOrEmpty(date_fr.Text) || !string.IsNullOrEmpty(date_to.Text)))
                    {
                        DateTime parsedDateFr, parsedDateTo;
                        if (DateTime.TryParseExact(date_fr.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateFr))
                        {
                            whereclause += "AND appliedDate >= @dateFrom ";
                            parameters.Add(new SqlParameter("@dateFrom", parsedDateFr.ToString("yyyy-MM-dd")));
                        }
                        if (DateTime.TryParseExact(date_to.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTo))
                        {
                            whereclause += "AND appliedDate <= @dateTo ";
                            parameters.Add(new SqlParameter("@dateTo", parsedDateTo.ToString("yyyy-MM-dd")));
                        }
                    }

                    sql += whereclause + "; DROP TABLE #TEMP";

                    connection();
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddRange(parameters.ToArray());
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    conn.Close();

                    // ✅ ใช้ MemoryStream + UTF-8 Encoding By:Potae Time:11:50 AM. Date:3/31/2025
                    StringBuilder builder = new StringBuilder();
                    builder.Append('\uFEFF'); // ✅ ป้องกันปัญหา Encoding By:Potae Time:11:50 AM. Date:3/31/2025

                    List<string> columnNames = new List<string>();
                    foreach (DataColumn column in dt.Columns)
                    {
                        columnNames.Add($"\"{column.ColumnName}\""); // ✅ ห่อข้อมูลด้วย " ป้องกันคอลัมน์ผิดพลาด By:Potae Time:11:50 AM. Date:3/31/2025
                    }
                    builder.Append(string.Join(",", columnNames)).Append("\n");

                    foreach (DataRow row in dt.Rows)
                    {
                        List<string> currentRow = new List<string>();
                        foreach (DataColumn column in dt.Columns)
                        {
                            currentRow.Add($"\"{row[column].ToString().Replace("\"", "\"\"")}\""); // ✅ Escape double quotes By:Potae Time:11:50 AM. Date:3/31/2025
                        }
                        builder.Append(string.Join(",", currentRow)).Append("\n");
                    }

                    // ✅ ใช้ Response.OutputStream.Write() เพื่อป้องกัน Encoding ผิดพลาด By:Potae Time:11:50 AM. Date:3/31/2025
                    byte[] csvBytes = Encoding.UTF8.GetBytes(builder.ToString());

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Data.csv");
                    Response.OutputStream.Write(csvBytes, 0, csvBytes.Length);
                    Response.Flush();
                    Response.End(); // ✅ ปิด Response เพื่อป้องกันโค้ด HTML ติดไป By:Potae Time:11:50 AM. Date:3/31/2025
                }
            }
            catch (SqlException ex)
            {
                logActivity.LogStaffActivity(staffID, $"SQL Error: {ex.Message} (User id = '{staffID}')");
            }
            catch (Exception ex)
            {
                logActivity.LogStaffActivity(staffID, $"General Error: {ex.Message} (User id = '{staffID}')");
            }
        }

        protected void ExtractionPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ExtractionPeriod.SelectedValue;
            inputdiv2.Visible = (selectedValue == "Age");
            inputdiv3.Visible = (selectedValue == "Applied Date");
        }

        protected void CustomValidate1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !string.IsNullOrEmpty(ExtractionPeriod.SelectedValue);
        }
    }
}
