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
using System.Globalization;
using System.Threading;
using System.Drawing;
using Microsoft.Reporting.WebForms;

namespace JAT.Inquery.Private
{
    public partial class listSummaryMember : System.Web.UI.Page
    {
        private SqlConnection conn;

        private string SQLTable;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            if (PrintType.SelectedValue == "0")
            {
                lblFromDate.Visible = false;
                drpYear.Visible = false;
                drpMonth.Visible = false;

                lblToDate.Visible = false;
                drpYearTo.Visible = false;
                drpMonthTo.Visible = false;
            }
            else
            {
                lblFromDate.Visible = true;
                drpYear.Visible = true;
                drpMonth.Visible = true;

                lblToDate.Visible = true;
                drpYearTo.Visible = true;
                drpMonthTo.Visible = true;
            }
            int iFor = 0;
            if (!IsPostBack)
            {
                for (iFor = 1999; iFor <= DateTime.Now.Year + 10; iFor++)
                    drpYear.Items.Add(iFor.ToString());
                drpYear.Items.Add("9999");
                drpYear.Text = DateTime.Now.Year.ToString();
                drpMonth.Text = Convert.ToString((DateTime.Now.Month));

                for (iFor = 1999; iFor <= DateTime.Now.Year + 10; iFor++)
                    drpYearTo.Items.Add(iFor.ToString());
                drpYearTo.Items.Add("9999");
                drpYearTo.Text = DateTime.Now.Year.ToString();
                drpMonthTo.Text = Convert.ToString((DateTime.Now.Month));
                BindData();
                lblCurrentMonth.Text = "Current Month : " + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[DateTime.Now.Month - 1];
            }
            
            
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        private void BindData()
        {
            connection();
            switch (PrintType.SelectedValue)
            {
                case "0":
                    SQLTable = "PrivateDetail";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(0);
                    break;
                case "1":
                    SQLTable = "PrivateDetail";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(0);
                    break;
            }
            DataGrid1.Visible = true;
            DataGrid1.DataSource = GetData().Tables[SQLTable].DefaultView;
            DataGrid1.DataBind();
        }
        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        //}

        private DataSet GetData()
        {
            string Grid1_t1, Grid1_t2, Grid1_t3, Grid1_t4, Grid1_t5;
            string Grid2_t1, Grid2_t2, Grid2_t3, Grid2_t4, Grid2_t5;
            if (Session["language"] == "ja-JP")
            {
                Grid1_t1 = "datediffa";
                Grid1_t2 = "Expire Month";
                Grid1_t3 = "Main Member";
                Grid1_t4 = "Family Member";
                Grid1_t5 = "Amount";
                Grid2_t1 = "datediffa";
                Grid2_t2 = "Expire Month";
                Grid2_t3 = "Main Member";
                Grid2_t4 = "Family Member";
                Grid2_t5 = "Amount";
            }
            else
            {
                Grid1_t1 = "datediff";
                Grid1_t2 = "Expire Month";
                Grid1_t3 = "Main Member";
                Grid1_t4 = "Family Member";
                Grid1_t5 = "Amount";
                Grid2_t1 = "datediff";
                Grid2_t2 = "Expire Month";
                Grid2_t3 = "Main Member";
                Grid2_t4 = "Family Member";
                Grid2_t5 = "Amount";
            }
            //IFormatProvider culture = new CultureInfo("en-US", true);
            String SQLStatement = "";

            Int32 MonthFrom = 0;
            Int32 MonthTo = 0;

            SQLTable = "PrivateDetail";
            switch (PrintType.SelectedValue)
            {
                case "0":
                    SQLStatement = " select datediff(month, getdate(), cast(ExpireMonth + '/01' as datetime)) as '"+Grid1_t1+ "', ExpireMonth as '" + Grid1_t2 + "', MainMember as '" + Grid1_t3 + "', FamilyMember as '" + Grid1_t4 + "', (MainMember + FamilyMember) as '" + Grid1_t5 + "' from ( " +
                                   " select ExpireMonth, count(*) as MainMember, sum((payNoMember - 1)) as FamilyMember from ( " +
                                   " select (cast(year(expiredDate) as varchar) + '/' + case when month(expiredDate) < 10 then '0' + cast(month(expiredDate) as varchar) else cast(month(expiredDate) as varchar) end) as ExpireMonth, pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate, pp.payNoMember, pp.payDuration, pp.payRemark, pp.checkShort " +
                                   " from privatepayment pp inner join ( select ppp.memberId, ppp.expiredate as expiredDate, Max(ppp.tranId) as transId " +
                                   " from privatepayment ppp inner join ( select pppp.memberId, max(pppp.expiredate) as expiredDate " +
                                   " from privatepayment pppp " +
                                   " group by pppp.memberId) as pppp on ppp.memberId = pppp.memberId and ppp.expiredate = pppp.expiredDate " +
                                   " group by ppp.memberId, ppp.expireDate ) " +
                                   " as pEx on pp.memberId = pEx.memberId and pp.expiredate = pEx.expiredDate and pp.tranId = pEx.transId inner join privatedetail bb on pp.memberId = bb.memberid where memberstatus = 'A' and (bb.membertype <> '4' and bb.membertype <> '6')) as ooo  " +
                                   " group by ExpireMonth) as oop " +
                                   " order by cast(replace(ExpireMonth,'/','') as int) ";
                    break;
                case "1":
                    MonthFrom = Convert.ToInt32(drpYear.Text + drpMonth.Text);
                    MonthTo = Convert.ToInt32(drpYearTo.Text + drpMonthTo.Text);
                    SQLStatement = " select * from ( " +
                                   " select datediff(month, getdate(), cast(ExpireMonth + '/01' as datetime)) as '" + Grid2_t1 + "', ExpireMonth as '" + Grid2_t2 + "', MainMember as '" + Grid2_t3 + "', FamilyMember as '" + Grid2_t4 + "', (MainMember + FamilyMember) as '" + Grid2_t5 + "' from ( " +
                                   " select ExpireMonth, count(*) as MainMember, sum((payNoMember - 1)) as FamilyMember from ( " +
                                   " select (cast(year(expiredDate) as varchar) + '/' + case when month(expiredDate) < 10 then '0' + cast(month(expiredDate) as varchar) else cast(month(expiredDate) as varchar) end) as ExpireMonth, pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate, pp.payNoMember, pp.payDuration, pp.payRemark, pp.checkShort " +
                                   " from privatepayment pp inner join ( select ppp.memberId, ppp.expiredate as expiredDate, Max(ppp.tranId) as transId " +
                                   " from privatepayment ppp inner join ( select pppp.memberId, max(pppp.expiredate) as expiredDate " +
                                   " from privatepayment pppp " +
                                   " group by pppp.memberId) as pppp on ppp.memberId = pppp.memberId and ppp.expiredate = pppp.expiredDate " +
                                   " group by ppp.memberId, ppp.expireDate ) " +
                                   " as pEx on pp.memberId = pEx.memberId and pp.expiredate = pEx.expiredDate and pp.tranId = pEx.transId inner join privatedetail bb on pp.memberId = bb.memberid where memberstatus = 'A' and (bb.membertype <> '4' and bb.membertype <> '6')) as ooo  " +
                                   " group by ExpireMonth) as oop ) as ppi " +
                                   " where cast(replace([Expire Month],'/','') as int) between " + MonthFrom + " and " + MonthTo + " " +
                                   " order by cast(replace([Expire Month],'/','') as int) ";
                    break;
            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            dataAdapter.SelectCommand.CommandTimeout = 600;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "PrivateDetail");
            return myDataSet;
        }
        protected void view_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rv = (DataRowView)e.Item.DataItem;
                string ExpireMonth = Convert.ToString(rv.Row.ItemArray[1]);
                string monthTemp = "";
                if (DateTime.Now.Month < 10)
                    monthTemp = "0" + DateTime.Now.Month.ToString();
                else
                    monthTemp = DateTime.Now.Month.ToString();
                if (ExpireMonth == (DateTime.Now.Year.ToString() + "/" + monthTemp))
                {
                    e.Item.Cells[0].BackColor = Color.Silver;
                    e.Item.Cells[0].Font.Bold = true;
                    e.Item.Cells[1].BackColor = Color.Silver;
                    e.Item.Cells[1].Font.Bold = true;
                    e.Item.Cells[2].BackColor = Color.Silver;
                    e.Item.Cells[2].Font.Bold = true;
                    e.Item.Cells[3].BackColor = Color.Silver;
                    e.Item.Cells[3].Font.Bold = true;
                    e.Item.Cells[4].BackColor = Color.Silver;
                    e.Item.Cells[4].Font.Bold = true;
                }
            }
        }

        protected void PrintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrintType.SelectedValue == "0")
            {
                lblFromDate.Visible = false;
                drpYear.Visible = false;
                drpMonth.Visible = false;

                lblToDate.Visible = false;
                drpYearTo.Visible = false;
                drpMonthTo.Visible = false;
            }
            else
            {
                lblFromDate.Visible = true;
                drpYear.Visible = true;
                drpMonth.Visible = true;

                lblToDate.Visible = true;
                drpYearTo.Visible = true;
                drpMonthTo.Visible = true;
            }
        }

        protected void btnPrintGrid_Click(object sender, EventArgs e)
        {
            connection();
            conn.Open();
            IFormatProvider culture = new CultureInfo("en-US", true);
            String sql = "";

            Int32 MonthFrom = 0;
            Int32 MonthTo = 0;


            //string Grid_t1;
            //string Grid_t2;
            //string Grid_t3;
            //string Grid_t4;
            //string Grid_t5;
            //if (Session["language"] == "ja-JP")
            //{
            //    Grid_t1 = "monthdiff";
            //    Grid_t2 = "検索終了日";
            //    Grid_t3 = "Main Member";
            //    Grid_t4 = "家族会員";
            //    Grid_t5 = "Amount";
            //}
            //else
            //{
            //    Grid_t1 = "monthdiff";
            //    Grid_t2 = "Expire Month";
            //    Grid_t3 = "Main Member";
            //    Grid_t4 = "Family Member";
            //    Grid_t5 = "Amount";
            //}


            switch (PrintType.SelectedValue)
            {
                case "0":
                    //sql = " select datediff(month, getdate(), cast(ExpireMonth + '/01' as datetime)) as monthdiff, ExpireMonth as 'ExpireMonth', MainMember as 'MainMember', FamilyMember as 'FamilyMember', (MainMember + FamilyMember) as Amount from ( " +
                    //               " select ExpireMonth, count(*) as MainMember, sum((payNoMember - 1)) as FamilyMember from ( " +
                    //               " select (cast(year(expiredDate) as varchar) + '/' + case when month(expiredDate) < 10 then '0' + cast(month(expiredDate) as varchar) else cast(month(expiredDate) as varchar) end) as ExpireMonth, pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate, pp.payNoMember, pp.payDuration, pp.payRemark, pp.checkShort " +
                    //               " from privatepayment pp inner join ( select ppp.memberId, ppp.expiredate as expiredDate, Max(ppp.tranId) as transId " +
                    //               " from privatepayment ppp inner join ( select pppp.memberId, max(pppp.expiredate) as expiredDate " +
                    //               " from privatepayment pppp " +
                    //               " group by pppp.memberId) as pppp on ppp.memberId = pppp.memberId and ppp.expiredate = pppp.expiredDate " +
                    //               " group by ppp.memberId, ppp.expireDate ) " +
                    //               " as pEx on pp.memberId = pEx.memberId and pp.expiredate = pEx.expiredDate and pp.tranId = pEx.transId inner join privatedetail bb on pp.memberId = bb.memberid where memberstatus = 'A' and (bb.membertype <> '4' and bb.membertype <> '6')) as ooo  " +
                    //               " group by ExpireMonth) as oop " +
                    //               " order by cast(replace(ExpireMonth,'/','') as int) ";
                    sql = " select datediff(month, getdate(), cast(ExpireMonth + '/01' as datetime)) as monthdiff, ExpireMonth as 'ExpireMonth', MainMember as 'MainMember', FamilyMember as 'FamilyMember', (MainMember + FamilyMember) as Amount, (cast(year(getdate()) as varchar)  + '/' + case when month(getdate()) < 10 then '0' + cast(month(getdate()) as varchar) else cast(month(getdate()) as varchar) end) as CurrentMonth  from ( " +
                        "select ExpireMonth, count(*) as MainMember, sum((payNoMember - 1)) as FamilyMember from( " +
                        "select(cast(year(expiredDate) as varchar) + '/' + case when month(expiredDate) < 10 then '0' + cast(month(expiredDate) as varchar) else cast(month(expiredDate) as varchar) end) as ExpireMonth, pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate, pp.payNoMember, pp.payDuration, pp.payRemark, pp.checkShort " +
                        "from privatepayment pp inner join(select ppp.memberId, ppp.expiredate as expiredDate, Max(ppp.tranId) as transId " +
                        "from privatepayment ppp inner join ( select pppp.memberId, max(pppp.expiredate) as expiredDate " +
                        "from privatepayment pppp " +
                        "group by pppp.memberId) as pppp on ppp.memberId = pppp.memberId and ppp.expiredate = pppp.expiredDate " +
                        "group by ppp.memberId, ppp.expireDate ) " +
                        "as pEx on pp.memberId = pEx.memberId and pp.expiredate = pEx.expiredDate and pp.tranId = pEx.transId inner join privatedetail bb on pp.memberId = bb.memberid where memberstatus = 'A') as ooo " +
                        "group by ExpireMonth) as oop " +
                        "order by cast(replace(ExpireMonth, '/', '') as int) ";
                    break;
                case "1":
                    MonthFrom = Convert.ToInt32(drpYear.Text + drpMonth.Text);
                    MonthTo = Convert.ToInt32(drpYearTo.Text + drpMonthTo.Text);
                    //sql = " select * from ( " +
                    //               " select datediff(month, getdate(), cast(ExpireMonth + '/01' as datetime)) as monthdiff, ExpireMonth as 'ExpireMonth', MainMember as 'MainMember', FamilyMember as 'FamilyMember', (MainMember + FamilyMember) as Amount from ( " +
                    //               " select ExpireMonth, count(*) as MainMember, sum((payNoMember - 1)) as FamilyMember from ( " +
                    //               " select (cast(year(expiredDate) as varchar) + '/' + case when month(expiredDate) < 10 then '0' + cast(month(expiredDate) as varchar) else cast(month(expiredDate) as varchar) end) as ExpireMonth, pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate, pp.payNoMember, pp.payDuration, pp.payRemark, pp.checkShort " +
                    //               " from privatepayment pp inner join ( select ppp.memberId, ppp.expiredate as expiredDate, Max(ppp.tranId) as transId " +
                    //               " from privatepayment ppp inner join ( select pppp.memberId, max(pppp.expiredate) as expiredDate " +
                    //               " from privatepayment pppp " +
                    //               " group by pppp.memberId) as pppp on ppp.memberId = pppp.memberId and ppp.expiredate = pppp.expiredDate " +
                    //               " group by ppp.memberId, ppp.expireDate ) " +
                    //               " as pEx on pp.memberId = pEx.memberId and pp.expiredate = pEx.expiredDate and pp.tranId = pEx.transId inner join privatedetail bb on pp.memberId = bb.memberid where memberstatus = 'A' and (bb.membertype <> '4' and bb.membertype <> '6')) as ooo  " +
                    //               " group by ExpireMonth) as oop ) as ppi " +
                    //               " where cast(replace([ExpireMonth],'/','') as int) between " + MonthFrom + " and " + MonthTo + " " +
                    //               " order by cast(replace([ExpireMonth],'/','') as int) ";
                    sql = "select * from ( " +
                        "select datediff(month, getdate(), cast(ExpireMonth +'/01' as datetime)) as monthdiff, ExpireMonth as 'ExpireMonth', MainMember as 'MainMember', FamilyMember as 'FamilyMember', (MainMember + FamilyMember) as Amount, (cast(year(getdate()) as varchar) + '/' + case when month(getdate()) < 10 then '0' + cast(month(getdate()) as varchar) else cast(month(getdate()) as varchar) end) as CurrentMonth from( " +
                        "select ExpireMonth, count(*) as MainMember, sum((payNoMember - 1)) as FamilyMember from( " +
                        "select(cast(year(expiredDate) as varchar) + '/' + case when month(expiredDate) < 10 then '0' + cast(month(expiredDate) as varchar) else cast(month(expiredDate) as varchar) end) as ExpireMonth, pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate, pp.payNoMember, pp.payDuration, pp.payRemark, pp.checkShort " +
                        "from privatepayment pp inner join(select ppp.memberId, ppp.expiredate as expiredDate, Max(ppp.tranId) as transId " +
                        "from privatepayment ppp inner join ( select pppp.memberId, max(pppp.expiredate) as expiredDate " +
                        "from privatepayment pppp " +
                        "group by pppp.memberId) as pppp on ppp.memberId = pppp.memberId and ppp.expiredate = pppp.expiredDate " +
                        "group by ppp.memberId, ppp.expireDate ) " +
                        "as pEx on pp.memberId = pEx.memberId and pp.expiredate = pEx.expiredDate and pp.tranId = pEx.transId inner join privatedetail bb on pp.memberId = bb.memberid where memberstatus = 'A') as ooo " +
                        "group by ExpireMonth) as oop ) as ppi " + 
                        "where cast(replace([ExpireMonth],'/','') as int) between '"+ MonthFrom + "' and '"+ MonthTo + "' " +
                        "order by cast(replace([ExpireMonth], '/', '') as int) ";
                    break;
            }
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Inquery/Private/ReportPage/PrivateMemberSummary.rdlc");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            ReportDataSource rds = new ReportDataSource("DataSet2", dt);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);

            conn.Close();

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;
            // string deviceInfo = "<DeviceInfo>" +
            //"  <OutputFormat>PDF</OutputFormat>" +
            //"  <PageWidth>9.8386in</PageWidth>" +
            //"  <PageHeight>11.6929197in</PageHeight>" +
            //"  <MarginTop>0.1in</MarginTop>" +
            //"  <MarginLeft>0.05in</MarginLeft>" +
            //"  <MarginRight>0.05in</MarginRight>" +
            //"  <MarginBottom>0.1in</MarginBottom>" +
            //"</DeviceInfo>";
            string deviceInfo = "<DeviceInfo>" +
                      "<OutputFormat>PDF</OutputFormat>" +
                      "  <PageWidth>8.27in</PageWidth>" +
                      "  <PageHeight>11.69in</PageHeight>" +
                      "  <MarginTop>0.0in</MarginTop>" +
                      "  <MarginLeft>0.0in</MarginLeft>" +
                      "  <MarginRight>0.0in</MarginRight>" +
                      "  <MarginBottom>0.0in</MarginBottom>" +
                      "  <EmbedFonts>None</EmbedFonts>" +
                      "</DeviceInfo>";

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
    }
}