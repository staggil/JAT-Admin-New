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
using Microsoft.Reporting.WebForms;

namespace JAT.Inquery.Private
{
    public partial class listPrivateStat : System.Web.UI.Page
    {
        private SqlConnection conn;

        private string SQLTable;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (rblViewType.SelectedValue == "1" || rblViewType.SelectedValue == "2" || rblViewType.SelectedValue == "3")
            {
                lblfromdate.Visible = false;
                txtFromDate.Visible = false;

                lbltodate.Visible = false;
                txtToDate.Visible = false;

                glyphicon1.Visible = false;
                glyphicon2.Visible = false;


            }
            else if (rblViewType.SelectedValue == "4" || rblViewType.SelectedValue == "5")
            {
                lblfromdate.Visible = true;
                txtFromDate.Visible = true;

                lbltodate.Visible = true;
                txtToDate.Visible = true;

                glyphicon1.Visible = true;
                glyphicon2.Visible = true;
            }
            if (rblViewType.SelectedValue == "5")
            {
                btnprintgrid.Visible = true;
            }
            else
            {
                btnprintgrid.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        private void BindData()
        {
            Label1.Text = rblViewType.SelectedItem.Text;

            switch (rblViewType.SelectedValue)
            {
                case "1":
                    SQLTable = "SumByPayment";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(0);
                    break;
                case "2":
                    SQLTable = "SumByPrefix";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(0);
                    break;
                case "3":
                    SQLTable = "SumByAge";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(0);
                    break;
                case "4":
                    SQLTable = "SumByAccrue";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(650);
                    break;
                case "5":
                    SQLTable = "SumByDuration";
                    DataGrid1.Width = System.Web.UI.WebControls.Unit.Pixel(450);
                    break;
            }
            if (SQLTable != "SumByAccrue")
            {
                //DataGrid1.Visible = true;
                DataGrid1.DataSource = GetData().Tables[SQLTable].DefaultView;
                DataGrid1.DataBind();
                if (SQLTable == "SumByDuration")
                {
                    DataTable ii = GetData1().Tables[SQLTable];
                    DataGrid2.DataSource = SummaryBindDataDuration(ii).DefaultView;
                    DataGrid2.DataBind();
                }
            }
            else
            {
                //DataGrid1.Visible = false;
                DataTable ii = GetData().Tables[SQLTable];
                DataGrid1.DataSource = SummaryBindData(ii).DefaultView;
                DataGrid1.DataBind();

            }
            if (SQLTable == "SumByDuration")
            {
                DataGrid1.Visible = false;
                DataGrid2.Visible = true;
            }
            else
            {
                DataGrid1.Visible = true;
                DataGrid2.Visible = false;
            }
        }

        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        //}
        private DataSet GetData()
        {
            connection();
            IFormatProvider culture = new CultureInfo("en-US", true);
            String SQLStatement = "";

            String strDateFrom = txtFromDate.Value.ToString();
            String strDateTo = txtToDate.Value.ToString();

            switch (rblViewType.SelectedValue)
            {
                case "1":

                    string Grid1_t1;
                    string Grid1_t2;
                    if (Session["language"] == "ja-JP")
                    {
                        Grid1_t1 = "会員種別";
                        Grid1_t2 = "メンバー数";

                    }
                    else
                    {
                        Grid1_t1 = "Member Type";
                        Grid1_t2 = "Number of Member";
                    }
                    SQLStatement = "SELECT d.membertype as '" + Grid1_t1 + "', count(*) as '" + Grid1_t2 + "' " + //, (m.newsletter * count(*)) as 'Total Fee'"+
											"FROM privateDetail d inner join SMemberType m on (d.membertype = m.membertype and m.EffectiveID = (select top(1) EffectiveID from tblEffective where EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate>= floor(cast(getdate() as float) ) order by EffectiveDate asc)) " +
                                            "where memberstatus = 'A' group by d.memberType, m.newsletter order by d.memberType";
                    break;
                case "2":

                    string Grid2_t1;
                    string Grid2_t2;
                    if (Session["language"] == "ja-JP")
                    {
                        Grid2_t1 = "敬称";
                        Grid2_t2 = "合計";

                    }
                    else
                    {
                        Grid2_t1 = "Prefix Name";
                        Grid2_t2 = "Total";
                    }

                    SQLStatement = "SELECT  prefixNm as '" + Grid2_t1 + "', count(*) as " + Grid2_t2 + " FROM privatedetail where memberstatus = 'A' group by prefixNm order by prefixNm";
                    break;
                //********* Table sumbyagemain???? ***********
                case "3":

                    string Grid3_t2;
                    string Grid3_t3;
                    string Grid3_t4;
                    if (Session["language"] == "ja-JP")
                    {
                        Grid3_t2 = "Duration";
                        Grid3_t3 = "最初のメンバー";
                        Grid3_t4 = "Second Member";
                    }
                    else
                    {
                        Grid3_t2 = "Duration";
                        Grid3_t3 = "First Member";
                        Grid3_t4 = "Second Member";
                    }


                    SQLStatement = "SELECT  sp.orderid as No, sp.mask as Duration, sp.memberterm as '" + Grid3_t3 + "', sw.membertermw as 'Second Member' " +
                        "FROM  sumbyagemain sp inner join sumbyageother sw on sp.orderid = sw.orderid order by sp.orderid";
                    break;
                case "4":

                    // comment by Wuttipong.l (MSTT) 2007-04-12 START
                    //              SQLStatement = "SET dateformat dmy select Type = 'Private',Duration1, (cast(Total_Amount as int) - cast(Amount_Over as int)) as PrivateAmount, Duration, Amount_Over, Total_Amount from " +
                    //               " (SELECT " +
                    //               " Duration = 'Over " + txtToDate.Value + "' , " +
                    //               " Amount_Over = ( SELECT SUM((newsletterFee/payduration) *  " +
                    //               " case when day(effectivedate) = 1 then datediff(month, '" + strDateTo + "', dateadd(month, payduration, effectivedate) -1) " +
                    //               " else " +
                    //               " datediff(month, '" + strDateTo + "', dateadd(month, payduration, effectivedate)) end ) " +
                    //               " From privatepayment " +
                    //               " WHERE payduration <> 0  AND paymentdate Between '" + strDateFrom + "' And '" + strDateTo + "' " +
                    //               " AND datediff(Month, '" + strDateTo + "', DateAdd(Month, payDuration, effectiveDate)) > 0 ), " +
                    //               " Duration1 = '" + txtFromDate.Value + " - " + txtToDate.Value + "', " +
                    //               " Total_Amount = ( SELECT sum(entrancefee + newsletterFee) " +
                    //               " From privatepayment " +
                    //               " WHERE paymentdate Between '" + strDateFrom + "' And '" + strDateTo + "')) as aaa " +
                    //               " UNION all " +
                    //               " select Type = 'Company',Duration1, (cast(Total_Amount as int) - cast(Amount_Over as int)) as CompanyAmount, Duration, Amount_Over, Total_Amount from " +
                    //               " (SELECT " +
                    //               " Duration = 'Over " + txtToDate.Value + "' , " +
                    //               " Amount_Over = ( SELECT sum(TotalPerMonth * DateDiff(Month, '" + strDateTo + "', termDate)) " +
                    //               " From companyPayment " +
                    //" WHERE Deleted_at IS NULL AND paymentDate between '" + strDateFrom + "' And '" + strDateTo + "' " +
                    //               " AND DateDiff(Month, '" + strDateTo + "', termDate) > 0 ), " +
                    //              " Duration1 = '" + txtFromDate.Value + " - " + txtToDate.Value + "', " +
                    //               " Total_Amount =( SELECT sum(memberFee+newsFee) AS totalamount " +
                    //               " From companyPayment " +
                    //" WHERE Deleted_at IS NULL AND paymentDate Between '" + strDateFrom + "' And '" + strDateTo + "')) as bbb order by type ";

                    SQLStatement = "SET dateformat dmy select Type = 'Private',Duration1, (cast(Total_Amount as int) - cast(Amount_Over as int)) as PrivateAmount, Duration, Amount_Over, Total_Amount from " +
                     " (SELECT " +
                     " Duration = 'Over " + txtToDate.Value + "' , " +
                     " Amount_Over = ( SELECT SUM((newsletterFee/payduration) *  " +
                     " case when day(effectivedate) = 1 then datediff(month, '" + strDateTo + "', dateadd(month, payduration, effectivedate) -1) " +
                     " else " +
                     " datediff(month, '" + strDateTo + "', dateadd(month, payduration, effectivedate)) end ) " +
                     " From privatepayment " +
                     " WHERE payduration <> 0  AND paymentdate Between '" + strDateFrom + "' And '" + strDateTo + "' " +
                     " AND datediff(Month, '" + strDateTo + "', DateAdd(Month, payDuration, effectiveDate)) > 0 ), " +
                     " Duration1 = '" + txtFromDate.Value + " - " + txtToDate.Value + "', " +
                     " Total_Amount = ( SELECT sum(entrancefee + newsletterFee) " +
                     " From privatepayment " +
                     " WHERE paymentdate Between '" + strDateFrom + "' And '" + strDateTo + "')) as aaa " +
                     " UNION all " +
                     " select Type = 'Company',Duration1, (cast(Total_Amount as int) - cast(Amount_Over as int)) as CompanyAmount, Duration, Amount_Over, Total_Amount from " +
                     " (SELECT " +
                     " Duration = 'Over " + txtToDate.Value + "' , " +
                     " Amount_Over = ( SELECT sum(TotalPerMonth * DateDiff(Month, '" + strDateTo + "', termDate)) " +
                     " From companyPayment " +
                     " WHERE paymentDate between '" + strDateFrom + "' And '" + strDateTo + "' " +
                     " AND DateDiff(Month, '" + strDateTo + "', termDate) > 0 ), " +
                    " Duration1 = '" + txtFromDate.Value + " - " + txtToDate.Value + "', " +
                     " Total_Amount =( SELECT sum(memberFee+newsFee) AS totalamount " +
                     " From companyPayment " +
                     " WHERE paymentDate Between '" + strDateFrom + "' And '" + strDateTo + "')) as bbb order by type ";
                    break;
                case "5":
                    SQLStatement = "SET dateformat dmy select '1 - 6' as 'Pay Duration', sum(nopay) as 'No Duration', sum(nofee) as 'Summary Duration' from ( " +
                          " select payduration, count(payduration) as nopay, sum(newsletterfee) as nofee from privatepayment " +
                          " where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and payduration <> 0 " +
                          " group by payduration) as ii where payduration between 1 and 6 " +
                          " union all " +
                          " select '7 - 12' as 'Pay Duration', sum(nopay) as 'No Duration', sum(nofee) as 'Summary Duration' from ( " +
                          " select payduration, count(payduration) as nopay, sum(newsletterfee) as nofee from privatepayment " +
                          " where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and payduration <> 0 " +
                          " group by payduration) as ii where payduration between 7 and 12 " +
                          " union all " +
                          " select '13 - 18' as 'Pay Duration', sum(nopay) as 'No Duration', sum(nofee) as 'Summary Duration' from ( " +
                          " select payduration, count(payduration) as nopay, sum(newsletterfee) as nofee from privatepayment " +
                          " where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and payduration <> 0 " +
                          " group by payduration) as ii where payduration between 13 and 18 " +
                          " union all " +
                          " select '19 - 24' as 'Pay Duration', sum(nopay) as 'No Duration', sum(nofee) as 'Summary Duration' from ( " +
                          " select payduration, count(payduration) as nopay, sum(newsletterfee) as nofee from privatepayment " +
                          " where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and payduration <> 0 " +
                          " group by payduration) as ii where payduration between 19 and 24 " +
                          " union all " +
                          " select '25 UP' as 'Pay Duration', sum(nopay) as 'No Duration', sum(nofee) as 'Summary Duration' from ( " +
                          " select payduration, count(payduration) as nopay, sum(newsletterfee) as nofee from privatepayment " +
                          " where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and payduration <> 0 " +
                          " group by payduration) as ii where payduration >= 25 ";
                    break;
            }

            ///instantiate sql connection and command object
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            dataAdapter.SelectCommand.CommandTimeout = 1800;

			myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, SQLTable);
            return myDataSet;
        }

        protected void view_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void setDateAttribute(string objectNm)
        {
            TextBox txtbox = (TextBox)Form.FindControl(objectNm);

            txtbox.Attributes.Add("onFocus", "javascript:vDateType='2'");
            txtbox.Attributes.Add("onKeyUp", "DateFormat(this,this.value,event,false,'2')");
            txtbox.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'2');");
        }

        private DataTable SummaryBindData(DataTable iii)
        {
            string Grid4_t1;
            string Grid4_t2;
            string Grid4_t3;
            string Grid4_t4;
            if (Session["language"] == "ja-JP")
            {
                Grid4_t1 = "期間";
                Grid4_t2 = "最初のメンバー";
                Grid4_t3 = "会社";
                Grid4_t4 = "合計";
            }
            else
            {
                Grid4_t1 = "Duration";
                Grid4_t2 = "Private";
                Grid4_t3 = "Company";
                Grid4_t4 = "Total";
            }
            Int32 tt = 0;
            Int32 tttmp = 0;
            DataSet dt = new DataSet();
            DataTable Dtable = new DataTable("tblTemp");
            dt.Tables.Add(Dtable);
            dt.Tables["tblTemp"].Columns.Add(Grid4_t1);
            dt.Tables["tblTemp"].Columns.Add(Grid4_t2);
            dt.Tables["tblTemp"].Columns.Add(Grid4_t3);
            dt.Tables["tblTemp"].Columns.Add(Grid4_t4);
            DataRow row = dt.Tables["tblTemp"].NewRow();
            row[Grid4_t1] = iii.Rows[0][1].ToString();
            //Label2.Text = iii.Rows[0][1].ToString();
            Int32.TryParse(iii.Rows[1][2].ToString(), out tt);
            row[Grid4_t2] = tt.ToString("#,##0;(#,##0);Zero");
            Int32.TryParse(iii.Rows[0][2].ToString(), out tt);
            row[Grid4_t3] = tt.ToString("#,##0;(#,##0);Zero");
            Int32.TryParse(iii.Rows[1][2].ToString(), out tt);
            Int32.TryParse(iii.Rows[0][2].ToString(), out tttmp);
            tt += tttmp;
            row[Grid4_t4] = tt.ToString("#,##0;(#,##0);Zero");
            dt.Tables["tblTemp"].Rows.Add(row);
            DataRow row1 = dt.Tables["tblTemp"].NewRow();
            row1[Grid4_t1] = iii.Rows[0][3].ToString();
            Int32.TryParse(iii.Rows[1][4].ToString(), out tt);
            row1[Grid4_t2] = tt.ToString("#,##0;(#,##0);Zero");
            Int32.TryParse(iii.Rows[0][4].ToString(), out tt);
            row1[Grid4_t3] = tt.ToString("#,##0;(#,##0);Zero");
            Int32.TryParse(iii.Rows[1][4].ToString(), out tt);
            Int32.TryParse(iii.Rows[0][4].ToString(), out tttmp);
            tt += tttmp;
            row1[Grid4_t4] = tt.ToString("#,##0;(#,##0);Zero");
            dt.Tables["tblTemp"].Rows.Add(row1);
            DataRow row2 = dt.Tables["tblTemp"].NewRow();
            row2[Grid4_t1] = Grid4_t4;
            Int32.TryParse(iii.Rows[1][5].ToString(), out tt);
            row2[Grid4_t2] = tt.ToString("#,##0;(#,##0);Zero");
            Int32.TryParse(iii.Rows[0][5].ToString(), out tt);
            row2[Grid4_t3] = tt.ToString("#,##0;(#,##0);Zero");
            Int32.TryParse(iii.Rows[1][5].ToString(), out tt);
            Int32.TryParse(iii.Rows[0][5].ToString(), out tttmp);
            tt += tttmp;
            row2[Grid4_t4] = tt.ToString("#,##0;(#,##0);Zero");
            dt.Tables["tblTemp"].Rows.Add(row2);
            return dt.Tables["tblTemp"];

        }

        private DataSet GetData1()
        {
            connection();
            IFormatProvider culture = new CultureInfo("en-US", true);
            string strDateOver = "";
            DateTime TToDate = DateTime.ParseExact(txtToDate.Value, "dd/MM/yyyy", culture);

            if (TToDate.Month == 12)
                strDateOver = DateTime.ParseExact(TToDate.AddYears(1).Year + "/" + TToDate.AddMonths(1).Month + "/" + "01", "yyyy/M/d", culture).ToString("yyyy/MM/dd");
            else
                strDateOver = DateTime.ParseExact(TToDate.Year + "/" + TToDate.AddMonths(1).Month + "/" + "01", "yyyy/M/dd", culture).ToString("yyyy/MM/dd");
            //strDateOver2 = DateTime.TryParseExact(TToDate.Year + "/" + TToDate.AddMonths(1).Month + "/" + "01", "yyyy/M/d", culture).ToString("yyyy/MM/dd");
            //strDateOver = DateTime.TryParseExact(TToDate.Year + "/" + TToDate.AddMonths(1).Month + "/" + "01", "yyyy/M/d", culture).ToString("yyyy/MM/dd");

            string SQLStatement1 = "SET dateformat dmy " +
               " select 'Pay Duration' = '1 - 6', count(*) as 'No Member', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'Summary Total' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >=1 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 6 and payDuration <> 0 " +
               " union all " +
               " select 'Pay Duration' = '7 - 12', count(*) as 'No Member', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'Summary Total' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >= 7 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 12 and payDuration <> 0 " +
               " union all " +
               " select 'Pay Duration' = '13 - 18', count(*) as 'No Member', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'Summary Total' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >= 13 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 18 and payDuration <> 0 " +
               " union all " +
               " select 'Pay Duration' = '19 - 24', count(*) as 'No Member', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'Summary Total' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >=19 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 24 and payDuration <> 0 " +
               " union all " +
               " select 'Pay Duration' = '25 Up', count(*) as 'No Member', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'Summary Total' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >= 25 and payDuration <> 0 ";

            ///instantiate sql connection and command object
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement1, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, SQLTable);
            return myDataSet;
        }

        private DataTable SummaryBindDataDuration(DataTable iii)
        {
            string Grid5_t1;
            string Grid5_t2;
            string Grid5_t3;
            if (Session["language"] == "ja-JP")
            {
                Grid5_t1 = "支払い期間";
                Grid5_t2 = "非会員";
                Grid5_t3 = "Summary Duration";
            }
            else
            {
                Grid5_t1 = "Pay Duration";
                Grid5_t2 = "No Member";
                Grid5_t3 = "Summary Duration";
            }
            Int32 tt = 0;
            DataSet dt = new DataSet();
            DataTable Dtable = new DataTable("tblTemp");
            dt.Tables.Add(Dtable);
            dt.Tables["tblTemp"].Columns.Add(Grid5_t1);
            dt.Tables["tblTemp"].Columns.Add(Grid5_t2);
            dt.Tables["tblTemp"].Columns.Add(Grid5_t3);

            DataRow row = dt.Tables["tblTemp"].NewRow();
            row[Grid5_t1] = iii.Rows[0][0].ToString();
            //Label2.Text = iii.Rows[0][1].ToString();
            Int32.TryParse(iii.Rows[0][1].ToString(), out tt);
            row[Grid5_t2] = tt.ToString("#,##0;(#,##0);0");

            Int32.TryParse(iii.Rows[0][2].ToString(), out tt);

            row[Grid5_t3] = tt.ToString("#,##0;(#,##0);0");
            dt.Tables["tblTemp"].Rows.Add(row);

            DataRow row1 = dt.Tables["tblTemp"].NewRow();
            row1[Grid5_t1] = iii.Rows[1][0].ToString();
            Int32.TryParse(iii.Rows[1][1].ToString(), out tt);
            row1[Grid5_t2] = tt.ToString("#,##0;(#,##0);0");
            Int32.TryParse(iii.Rows[1][2].ToString(), out tt);
            row1[Grid5_t3] = tt.ToString("#,##0;(#,##0);0");
            dt.Tables["tblTemp"].Rows.Add(row1);

            DataRow row2 = dt.Tables["tblTemp"].NewRow();
            row2[Grid5_t1] = iii.Rows[2][0].ToString();
            Int32.TryParse(iii.Rows[2][1].ToString(), out tt);
            row2[Grid5_t2] = tt.ToString("#,##0;(#,##0);0");
            Int32.TryParse(iii.Rows[2][2].ToString(), out tt);
            row2[Grid5_t3] = tt.ToString("#,##0;(#,##0);0");
            dt.Tables["tblTemp"].Rows.Add(row2);

            DataRow row3 = dt.Tables["tblTemp"].NewRow();
            row3[Grid5_t1] = iii.Rows[3][0].ToString();
            Int32.TryParse(iii.Rows[3][1].ToString(), out tt);
            row3[Grid5_t2] = tt.ToString("#,##0;(#,##0);0");
            Int32.TryParse(iii.Rows[3][2].ToString(), out tt);
            row3[Grid5_t3] = tt.ToString("#,##0;(#,##0);0");
            dt.Tables["tblTemp"].Rows.Add(row3);

            DataRow row4 = dt.Tables["tblTemp"].NewRow();
            row4[Grid5_t1] = iii.Rows[4][0].ToString();
            Int32.TryParse(iii.Rows[4][1].ToString(), out tt);
            row4[Grid5_t2] = tt.ToString("#,##0;(#,##0);0");
            Int32.TryParse(iii.Rows[4][2].ToString(), out tt);
            row4[Grid5_t3] = tt.ToString("#,##0;(#,##0);0");
            dt.Tables["tblTemp"].Rows.Add(row4);

            return dt.Tables["tblTemp"];

        }

        protected void rblViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblViewType.SelectedValue == "1" || rblViewType.SelectedValue == "2" || rblViewType.SelectedValue == "3")
            {
                lblfromdate.Visible = false;
                txtFromDate.Visible = false;

                lbltodate.Visible = false;
                txtToDate.Visible = false;

                glyphicon1.Visible = false;
                glyphicon2.Visible = false;

            }
            else if (rblViewType.SelectedValue == "4" || rblViewType.SelectedValue == "5")
            {
                lblfromdate.Visible = true;
                txtFromDate.Visible = true;

                lbltodate.Visible = true;
                txtToDate.Visible = true;

                glyphicon1.Visible = true;
                glyphicon2.Visible = true;
            }
            if (rblViewType.SelectedValue == "5")
            {
                btnprintgrid.Visible = true;
            }
            else
            {
                btnprintgrid.Visible = false;
            }
        }

        protected void btnprintgrid_Click(object sender, EventArgs e)
        {
            connection();
            conn.Open();
            IFormatProvider culture = new CultureInfo("en-US", true);
            string strDateOver = "";
            DateTime TToDate = DateTime.ParseExact(txtToDate.Value, "dd/MM/yyyy", culture);

            if (TToDate.Month == 12)
                strDateOver = DateTime.ParseExact(TToDate.AddYears(1).Year + "/" + TToDate.AddMonths(1).Month + "/" + "01", "yyyy/M/d", culture).ToString("yyyy/MM/dd");
            else
                strDateOver = DateTime.ParseExact(TToDate.Year + "/" + TToDate.AddMonths(1).Month + "/" + "01", "yyyy/M/dd", culture).ToString("yyyy/MM/dd");
            string sql = "SET dateformat dmy" +
               " select 'PayDuration' = '1 - 6', count(*) as 'NoMember', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'SummaryDuration' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >=1 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 6 and payDuration <> 0 " +
               " union all " +
               " select 'PayDuration' = '7 - 12', count(*) as 'NoMember', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'SummaryDuration' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >= 7 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 12 and payDuration <> 0 " +
               " union all " +
               " select 'PayDuration' = '13 - 18', count(*) as 'NoMember', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'SummaryDuration' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >= 13 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 18 and payDuration <> 0 " +
               " union all " +
               " select 'PayDuration' = '19 - 24', count(*) as 'NoMember', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'SummaryDuration' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >=19 and (datediff(month, '" + strDateOver + "', expiredate) + 1) <= 24 and payDuration <> 0 " +
               " union all " +
               " select 'PayDuration' = '25 Up', count(*) as 'NoMember', isnull(sum(((newsletterfee / payDuration) * (datediff(month, '" + strDateOver + "', expiredate) + 1))),0) as 'SummaryDuration' " +
               " from privatepayment where paymentdate between '" + txtFromDate.Value + "' and '" + txtToDate.Value + "' and " +
               " (datediff(month, '" + strDateOver + "', expiredate) + 1) >= 25 and payDuration <> 0 ";


            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Inquery/Private/ReportPage/privateDuration.rdlc");
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