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

namespace JAT.PrivateReport
{
    public partial class reportPrivateAge : System.Web.UI.Page
    {

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
            for (int i = 0; i <= 100; i++)
            {
                DropDownList1.Items.Add(i.ToString());
                DropDownList2.Items.Add(i.ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string startAge = DropDownList1.SelectedValue;
            string toAge = DropDownList2.SelectedValue;

            connection();
            conn.Open();


            string sql = "";
            string title = "";
            string reporturl = "";
            try
            {
                var sa = int.Parse(startAge);
                var ta = int.Parse(toAge);
                var birthfr = DateTime.Now.AddYears(-(ta)).ToString("yyyy-01-01", new CultureInfo("en-US"));
                var birthto = DateTime.Now.AddYears(-(sa)).ToString("yyyy-12-31", new CultureInfo("en-US"));
                if (sa <= 20 || ta <= 20)
                {
                    title = "成人対象者";
                    sql = "SELECT * FROM(select d.firstmemberId as memberId, d.memberId as childid, " +
                        "nameJ = '', nameEng = '', " +
                        "nameJ as nameKidJ, (prefixNm +' '+nameE) as nameKidE, " +
                        "convert(nvarchar(12), birthdate, 111) as birthdate, memberType, address " +
                        "from privateDetail d left join privateAddress a on d.firstmemberId = a.memberId and addressType = 1 " +
                        "where birthdate between '"+birthfr+"' and '"+birthto+"' " +
                        "and memberStatus = 'A' and (memberType <> '3A' and memberType <> '3B') " +
                        "UNION ALL " +
                        "select p.memberId, c.childid, " +
                        "nameJ, (prefixNm +' '+nameE) as nameEng, " +
                        "nameKidJ, nameKidE, " +
                        "convert(nvarchar(12), c.birthdate, 111) as  birthdate, " +
                        "memberType = '', (address + ' ('+ phone +')') as address " +
                        "from private p inner join privateDetail d on p.memberId = d.memberId " +
                        "inner join privateChild c on p.memberId = c.memberId " +
                        "left join privateAddress a on p.memberId = a.memberId and addressType = 1 " +
                        "where c.birthdate between '" + birthfr + "' and '" + birthto + "' " +
                        "and memberStatus = 'A') As a " +
                        "ORDER BY nameKidE ASC,nameEng ASC";
                    reporturl = "~/PrivateReport/ReportPage/printPrivateAge.rdlc";
                }
                else
                {
                    title = "Member Age : " + startAge + " ~ " + toAge;
                    sql = "select d.firstmemberId, d.memberId, nameJ, (prefixNm +' '+nameE) as nameEng, convert(nvarchar(12), birthdate, 111)as birthdate,  convert(nvarchar(12), appliedDate, 111) as appliedDate, memberType, address " +
                    "from privateDetail d left join privateAddress a on d.firstmemberId = a.memberId and addressType = 1 " +
                    "where birthdate between '" + birthfr + "' and '" + birthto + "' " +
                    "and memberStatus = 'A' and (memberType <> '3A' and memberType <> '3B') " +
                    "order by nameE";
                    reporturl = "~/PrivateReport/ReportPage/printPrivateAge - Copy.rdlc";
                }
            }
            catch (Exception ex)
            {
                title = "Member Age : " + startAge + " ~ " + toAge;
                sql = "select d.firstmemberId, d.memberId, nameJ, (prefixNm +' '+nameE) as nameEng, convert(nvarchar(12), birthdate, 111)as birthdate,  convert(nvarchar(12), appliedDate, 111) as appliedDate, memberType, address " +
                "from privateDetail d left join privateAddress a on d.firstmemberId = a.memberId and addressType = 1 " +
                "where birthdate between '1/1/1900' and '1/1/1900' " +
                "and memberStatus = 'A' and (memberType <> '3A' and memberType <> '3B') " +
                "order by nameE";
                reporturl = "~/PrivateReport/ReportPage/printPrivateAge - Copy.rdlc";
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.SelectCommand.CommandTimeout = 600;
            adapter.Fill(dt);

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(reporturl);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            
            reportParameters.Add(new ReportParameter("showAge", title));
            this.ReportViewer1.LocalReport.SetParameters(reportParameters);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);

            conn.Close();
        }
    }
}