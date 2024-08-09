using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JAT.PrivateReport
{
    public partial class reportMonthlyReport : System.Web.UI.Page
    {
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
            if (!Page.IsPostBack)
            {
                Box1.Value = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                Box2.Value = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] splitBox1 = Box1.Value.Split('/');
            string DateFrom = splitBox1[2] + "/" + splitBox1[1] + "/" + splitBox1[0];
            string[] splitBox2 = Box2.Value.Split('/');
            string DateTo = splitBox2[2] + "/" + splitBox2[1] + "/" + splitBox2[0];
            //string DateFrom = DateTime.Parse(Box1.Value.ToString()).ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            //string DateTo = DateTime.Parse(Box2.Value.ToString()).ToString("yyyy-MM-dd", new CultureInfo("en-US"));

            var displayCho1 = RadioButtonList1.SelectedValue.ToString();

            //System.Diagnostics.Debug.WriteLine(DateFrom);

            if (displayCho1 == "A")
            {
                if (Page.IsValid)
                {
                    connection();
                    conn.Open();

                    string sql = "SELECT memberid AS 'memberid', nameE AS 'nameE', nameJ AS 'nameJ', prefixNm AS 'prefixNm' FROM PrivateDetail " +
                                "WHERE memberStatus = 'A' AND appliedDate BETWEEN '" + DateFrom + "' AND '" + DateTo + "'";
                    System.Diagnostics.Debug.Write(sql);
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    conn.Close();
                    var dataTable = dt;
                    StringBuilder builder = new StringBuilder();
                    List<string> columnNames = new List<string>();
                    List<string> rows = new List<string>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        columnNames.Add(column.ColumnName);
                        if (column.ColumnName == "prefixNm")
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
                    string dateFrom = Box1.Value.ToString().Replace('/', '_');
                    string dateTo = Box2.Value.ToString().Replace('/', '_');
                    string filename = "memberType_A_" + dateFrom + "-" + dateTo;
                    //Response.AddHeader("Content-Disposition", "attachment;filename=Data.csv");
                    string exportFile = "attachment;filename=" + filename + ".csv";
                    Response.AddHeader("Content-Disposition", exportFile);
                    Response.Write('\uFEFF');
                    Response.Write(builder.ToString());
                    Response.End();
                }
            }
            else
            {
                if (Page.IsValid)
                {
                    connection();
                    conn.Open();

                    string sql = "SELECT memberid AS 'memberid', nameE AS 'nameE', nameJ AS 'nameJ', prefixNm AS 'prefixNm' FROM PrivateDetail " +
                                 "WHERE memberStatus = 'NA' AND cancelledDate BETWEEN '" + DateFrom + "' AND DATEADD(day, 1, '" + DateTo + "')";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    conn.Close();
                    var dataTable = dt;
                    StringBuilder builder = new StringBuilder();
                    List<string> columnNames = new List<string>();
                    List<string> rows = new List<string>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        columnNames.Add(column.ColumnName);
                        if (column.ColumnName == "prefixNm")
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
                    string dateFrom = Box1.Value.ToString().Replace('/', '_');
                    string dateTo = Box2.Value.ToString().Replace('/', '_');
                    string filename = "memberType_NA_" + dateFrom + "-" + dateTo;
                    //Response.AddHeader("Content-Disposition", "attachment;filename=Data.csv");
                    string exportFile = "attachment;filename=" + filename + ".csv";
                    Response.AddHeader("Content-Disposition", exportFile);
                    Response.Write('\uFEFF');
                    Response.Write(builder.ToString());
                    Response.End();
                }
            }

        }
    }
}