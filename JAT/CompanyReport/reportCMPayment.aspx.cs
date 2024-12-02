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
using System.Threading;

namespace JAT.CompanyReport
{
    public partial class reportCMPayment : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		string _staff;
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (reportType.SelectedValue == "History")
            //{
            //    payMethod.Visible = true;
            //    paymethodacc.Visible = false;

            //    Months.Visible = false;
            //    month.Visible = false;

            //    DropDownList1.Visible = false;

            //}
            //else if (reportType.SelectedValue == "Accrued")
            //{
            //    payMethod.Visible = false;
            //    paymethodacc.Visible = true;
            //    Months.Visible = true;
            //    month.Visible = true;

            //    DropDownList1.Visible = false;
            //}
            //else 
            //{
            //    payMethod.Visible = false;
            //    paymethodacc.Visible = false;
            //    Months.Visible = false;
            //    month.Visible = false;

            //    DropDownList1.Visible = true;
            //}

            if (Session["User"] != null)
            {
                _staff = Session["UID"].ToString().Trim();
            }
            if (!Page.IsPostBack)
            {
                ConfigureScreen();
                //expdateFrom.Value = DateTime.Now.ToString("yyyy-MMM-dd", new CultureInfo("en-US"));
                //expdateTo.Value = DateTime.Now.ToString("yyyy-MMM-dd", new CultureInfo("en-US"));


            }
        }

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
        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        //}
        private void CreateData()
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			//IFormatProvider culture = new CultureInfo(Session["language"].ToString(), true);
			IFormatProvider culture = new CultureInfo("en-US", true);
            connection();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {
                con.Open();
                String strSqlSelect = " DELETE FROM companyReport " +
                                      " WHERE staffid = '" + _staff + "'";
                SqlCommand sqlCmd = new SqlCommand(strSqlSelect, con);
                try
                {
					sqlCmd.ExecuteNonQuery();
					string activityDetail = $"Deleted data in a table companyReport where staffid is '{_staff}' successful (user name = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
                catch (SqlException ex)
                {
                    //string activityDetail = $"Deleted data in a table companyReport where staffid is '{_staff}' unsuccessful [{sqlex.Message}] (user name = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                                                          $"{ex.Message}");
                }
				catch (Exception ex)
				{
                    //string activityDetail = $"Deleted data in a table companyReport where staffid is '{_staff}' unsuccessful [{ex.Message}] (user name = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
                }

                //connection();
                //cmd = new SqlCommand("DELETE FROM companyReport WHERE staffid = 1", conn);
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();

                //strSqlSelect = " SELECT CompanyMember.companyId, CompanyMember.companyNmJ, " +
                //            " CompanyMember.companyNmE, CompanyMember.address, CompanyMember.phone, " +
                //            " CompanyMember.fax, CompanyMember.contNm,CompanyMember.contPosition, " +
                //            " CompanyMember.payMethod, CompanyPayment.totalPerMonth, CompanyMember.payPeriod, " +
                //            " CompanyMember.payDuration, " +
                //            " companyPayment.expiredDate " +
                //            " FROM CompanyMember LEFT OUTER JOIN CompanyPayment " +
                //            " ON (CompanyMember.companyId = CompanyPayment.companyId ) " +
                //            " WHERE CompanyPayment.tranid = ( SELECT TOP 1 companyPayment_2.tranid " +
                //            "                               FROM companyPayment companyPayment_2 " +
                //            "                               WHERE companyPayment.companyId = companyPayment_2.companyId " +
                //            "                               ORDER BY companyPayment_2.expiredDate DESC ) " +
                //            " AND CompanyMember.memberStatus = 'A' " +
                //            " AND (companyMember.getInvoice <> 0 OR CompanyPayment.checkShort <> 0 )" +
                //            " AND companyMember.companyNmJ <> 'Deleted' AND CompanyPayment.Deleted_at IS NULL " +
                //            " ORDER BY CompanyMember.companyNmE ";

                strSqlSelect = " SELECT CompanyMember.companyId, CompanyMember.companyNmJ, " +
                " CompanyMember.companyNmE, CompanyMember.address, CompanyMember.phone, " +
                " CompanyMember.fax, CompanyMember.contNm,CompanyMember.contPosition, " +
                " CompanyMember.payMethod, CompanyPayment.totalPerMonth, CompanyMember.payPeriod, " +
                " CompanyMember.payDuration, " +
                " companyPayment.expiredDate " +
                " FROM CompanyMember LEFT OUTER JOIN CompanyPayment " +
                " ON (CompanyMember.companyId = CompanyPayment.companyId ) " +
                " WHERE CompanyPayment.tranid = ( SELECT TOP 1 companyPayment_2.tranid " +
                "                               FROM companyPayment companyPayment_2 " +
                "                               WHERE companyPayment.companyId = companyPayment_2.companyId " +
                "                               ORDER BY companyPayment_2.expiredDate DESC ) " +
                " AND CompanyMember.memberStatus = 'A' " +
                " AND (companyMember.getInvoice <> 0 OR CompanyPayment.checkShort <> 0 )" +
                " AND companyMember.companyNmJ <> 'Deleted' " +
                " ORDER BY CompanyMember.companyNmE ";

                sqlCmd.CommandText = strSqlSelect;
                sqlCmd.CommandTimeout = 600;
                SqlDataReader myRead = sqlCmd.ExecuteReader();

                string strPayPeriod = "";
                string intPayDuration = "";
                //string datExpireDate;
                string strCurrentDate02 = DateTime.Today.ToString();
                //string datCurrentDate02 = "";

                DateTime datExpireDate = DateTime.MinValue;
                DateTime datCurrentDate02 = DateTime.MinValue;

				string activityDetailForWhile = "";
				while (myRead.Read())
                {
                    strPayPeriod = myRead.GetValue(10).ToString();
                    intPayDuration = myRead.GetValue(11).ToString();
                    datExpireDate = (DateTime)myRead["expiredDate"];

                    string companyId = myRead.GetValue(0).ToString();
                    string companyNmJ = myRead.GetValue(1).ToString();
                    string companyNmE = myRead.GetValue(2).ToString();
                    string address = myRead.GetValue(3).ToString();
                    string phone = myRead.GetValue(4).ToString();
                    string fax = myRead.GetValue(5).ToString();
                    string contNm = myRead.GetValue(6).ToString();
                    string contPosition = myRead.GetValue(7).ToString();
                    string payMethod = myRead.GetValue(8).ToString();
                    string totalPerMonth = myRead.GetValue(9).ToString();

                    string expiredDate = myRead.GetValue(12).ToString();
                    string payDuration = myRead.GetValue(11).ToString();


                    if (strPayPeriod.Trim() == "93")
                    {
                        if (intPayDuration != "12")
                        {
                            if (DateTime.Today.Month <= 3)
                            {
								// strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/31";
								strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/30";
							}
                                
                            else if ((DateTime.Today.Month > 3) && (DateTime.Today.Month <= 9))
                            {
								strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/09/30";
							}
                                
                            else if (DateTime.Today.Month > 9)
                            {
								// strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/31";
								strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/30";
							}
                                
                        }
                        else
                        {
                            if (DateTime.Today.Month <= 3)
                            {
								// strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/31";
								strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/30";
							}

                            else
                            {
								// strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/31";
								strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/30";
							}
                                
                        }
                    }
                    else if (strPayPeriod.Trim() == "612")
                    {
                        if (intPayDuration != "12")
                        {
                            if (DateTime.Today.Month <= 6)
                            {
								strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/06/30";
							}
                                
                            else if (DateTime.Today.Month > 6)
                            {
								// strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
								strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/30";
							}
                                
                        }
                        else
                        {
                            // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
                            strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/30";
                        }
                    }
                    var m = month.Text.ToString();
                    if (m == "More than 9 month")
                    {
                        m = ">=9";
                    }

                    String strFromDate = expdateFrom.Value;
                    String strToDate = expdateTo.Value;
                    DateTime datFromDate = DateTime.ParseExact(strFromDate.Substring(0, 2) + "/" + strFromDate.Substring(3, 2) + "/" + strFromDate.Substring(6, 4), "dd/MM/yyyy", culture);
                    DateTime datToDate = DateTime.ParseExact(strToDate.Substring(0, 2) + "/" + strToDate.Substring(3, 2) + "/" + strToDate.Substring(6, 4), "dd/MM/yyyy", culture);

                    //datCurrentDate02 = DateTime.ParseExact(strCurrentDate02, "dd/MM/yyyy", culture);
                    datCurrentDate02 = DateTime.Parse(strCurrentDate02);
                    if ((datExpireDate < datCurrentDate02) && (datExpireDate != DateTime.MinValue)) // calculate only member want invoice or member who not have money enough for pay by bank
                    {
                        double chkMonth = System.Math.Round(DateDiff("M", datToDate, DateTime.ParseExact("01" + datExpireDate.AddMonths(1).ToString("/MM/yyyy"), "dd/MM/yyyy", culture)), 0);
                        if ((month.Text.Equals("ALL") || month.Text.Equals(chkMonth.ToString())) ||
                            (m.Equals(">=9") && chkMonth >= 9))
                        {
                            string InsertSQL = "INSERT INTO CompanyReport (staffId, companyId, companyNmJ, companyNmE, address, phone, fax, contNm, contPosition, payMethod, startDate, termDate, expiredDate, totalPerMonth, noPayMonth, total, flagPrintReport) " +
                                               "VALUES (@staffId, @companyId, @companyNmJ, @companyNmE, @address, @phone, @fax, @contNm, @contPosition, @payMethod, @startDate, @termDate, @expiredDate, @totalPerMonth, @noPayMonth, @total, @flagPrintReport)";
                            SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);

                            var startDate = DateTime.ParseExact("01" + datExpireDate.AddMonths(1).ToString("/MM/yyyy"), "dd/MM/yyyy", culture);
                            var noPayMonth = (int)DateDiff("M", datToDate, startDate);
                            var accruedMonth = ((datToDate.Year - datExpireDate.Year) * 12) + datToDate.Month - datExpireDate.Month;
                            var termDate = datToDate;
                            long val = Int64.Parse(totalPerMonth);
                            var total = (int)val * (int)noPayMonth;
							conn.Open();
							try
                            {
								
								vlozSQL.Parameters.AddWithValue("@staffId", _staff);
								vlozSQL.Parameters.AddWithValue("@companyId", companyId);
								vlozSQL.Parameters.AddWithValue("@companyNmJ", companyNmJ);
								vlozSQL.Parameters.AddWithValue("@companyNmE", companyNmE);
								vlozSQL.Parameters.AddWithValue("@address", address);
								vlozSQL.Parameters.AddWithValue("@phone", phone);
								vlozSQL.Parameters.AddWithValue("@fax", fax);
								vlozSQL.Parameters.AddWithValue("@contNm", contNm);
								vlozSQL.Parameters.AddWithValue("@payMethod", payMethod);
								vlozSQL.Parameters.AddWithValue("@contPosition", contPosition);
								vlozSQL.Parameters.AddWithValue("@totalPerMonth", totalPerMonth);
								vlozSQL.Parameters.AddWithValue("@expiredDate", expiredDate);
								vlozSQL.Parameters.AddWithValue("@noPayMonth", accruedMonth);
								vlozSQL.Parameters.AddWithValue("@startDate", startDate);
								vlozSQL.Parameters.AddWithValue("@termDate", termDate);
								vlozSQL.Parameters.AddWithValue("@total", total);
								vlozSQL.Parameters.AddWithValue("@flagPrintReport", false);
								vlozSQL.ExecuteNonQuery();
								vlozSQL.Parameters.Clear();
								
								activityDetailForWhile = $"Added new data into a table 'CompanyReport' successful (user id = '{staffID}')";
							}
                            catch (SqlException ex)
                            {
								activityDetailForWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							}
							catch (Exception ex)
							{
								activityDetailForWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							}
							conn.Close();
						}
                        else { }
                    }
                }

                logActivity.LogStaffActivity(staffID, activityDetailForWhile);
            }

        }

        protected void print_Click(object sender, EventArgs e)
        {
            var DateFrom = expdateFrom.Value.ToString();
            var DateTo = expdateTo.Value.ToString();
            var m = month.Text.ToString();
            var pm = payMethod.Text.ToString();

            DateTime today = DateTime.Now;
            var ReportType = "";
            connection();
            conn.Open();

            if (reportType.Text == "History")
            {
                //string sql = "SELECT FORMAT(paymentDate, 'yyyy/MM/dd ') as paymentDate, " +
                //    "case when companyNmEE != '' then companyNmEE else companyNmE end as 'companyNmE', FORMAT(termDate, 'yyyy/MM/dd ') as termDate," +
                //    "bNo,cNo, memberFee as memberFeeRAW," +
                //    "case when cNo = '' or companyNmE in ('The Platinum Group PLC','Siam Property Development Co.,Ltd.') then CAST(ROUND(memberFee / 1.07, 3) as decimal(18, 3)) " +
                //    "when newsFee != '' then CAST(ROUND(memberFee / 1.070, 3) as decimal(18, 3)) " +
                //    "else memberFee end as memberFee, " +
                //    "case when cNo = '' or companyNmE in ('The Platinum Group PLC','Siam Property Development Co.,Ltd.') then CAST(ROUND(memberFee -(memberFee / 1.07), 3) as decimal(18, 3)) " +
                //    "when newsFee != '' then CAST(ROUND(memberFee -(memberFee / 1.070), 3) as decimal(18, 3)) " +
                //    "else totalFee - memberFee end as vat, " +
                //    "newsFee," +
                //    "case when companyNmE in ('The Platinum Group PLC','Siam Property Development Co.,Ltd.') then CAST(ROUND(memberFee / 1.070, 3) as decimal(18, 3)) + CAST(ROUND(memberFee -(memberFee / 1.070), 3) as decimal(18, 3))" +
                //    "else totalFee end as totalFee," +
                //    "payRemark, " +
                //    "case when withHolding = '1' then CAST(ROUND((memberFee/ 1.07)*0.03,3)as decimal(18, 3)) else '0.000' end as withHolding " +
                //    "FROM companyMember INNER JOIN companyPayment " +
                //    "ON(companyMember.companyId = companyPayment.companyId) " +
                //    "WHERE(companyPayment.paymentDate >= '" + DateFrom + "' OR '" + DateFrom + "' IS Null) " +
                //    "AND(companyPayment.paymentDate <= '" + DateTo + "' OR '" + DateTo + "' IS Null) ";
                //BY P.
                //string sql = "SELECT FORMAT(paymentDate, 'yyyy/MM/dd ') as paymentDate, " +
                //    "case when companyNmEE != '' then companyNmEE else companyNmE end as 'companyNmE', FORMAT(termDate, 'yyyy/MM/dd ') as termDate," +
                //    "bNo,cNo, memberFee as memberFeeRAW," +
                //    "case when cNo = '' or companyNmE = 'The Platinum Group PLC' then CAST(ROUND(memberFee / 1.07, 2) as decimal(18, 2)) " +
                //    "when newsFee != '' then CAST(ROUND(memberFee / 1.07, 2) as decimal(18, 2)) " +
                //    "else memberFee end as memberFee, " +
                //    "case when cNo = '' or companyNmE = 'The Platinum Group PLC' then CAST(ROUND(memberFee -(memberFee / 1.07), 2) as decimal(18, 2)) " +
                //    "when newsFee != '' then CAST(ROUND(memberFee -(memberFee / 1.07), 2) as decimal(18, 2)) " +
                //    "else totalFee - memberFee end as vat, " +
                //    "newsFee,totalFee,payRemark, " +
                //    //"case when withHolding = '1' then CAST(ROUND((memberFee/ 1.07) * 0.03,2)as decimal(18, 2)) else '0.00' end as withHolding " +
                //    "case when withHolding = '1' then CAST(ROUND((memberFee/ 1.07) * 0.03,3)as decimal(18, 3)) else '0.00' end as withHolding " +
                //    "FROM companyMember INNER JOIN companyPayment " +
                //    "ON(companyMember.companyId = companyPayment.companyId) " +
                //    "WHERE(companyPayment.paymentDate >= '" + DateFrom + "' OR '" + DateFrom + "' IS Null) " +
                //    "AND(companyPayment.paymentDate <= '" + DateTo + "' OR '" + DateTo + "' IS Null) ";

                //string sql = "SELECT FORMAT(paymentDate, 'yyyy/MM/dd ') as paymentDate, " +
                //    "case when companyNmEE != '' then companyNmEE else companyNmE end as 'companyNmE', FORMAT(termDate, 'yyyy/MM/dd ') as termDate," +
                //    "bNo,cNo, memberFee as memberFeeRAW,memberFee,vat, " +
                //    "newsFee,totalFee,payRemark, " +
                //    //"case when withHolding = '1' then CAST(ROUND((memberFee/ 1.07) * 0.03,2)as decimal(18, 2)) else '0.00' end as withHolding " +
                //    "case when withHolding = '1' then CAST(ROUND((memberFee/ 1.07) * 0.03,3)as decimal(18, 3)) else '0.00' end as withHolding " +
                //    "FROM companyMember INNER JOIN companyPayment " +
                //    "ON(companyMember.companyId = companyPayment.companyId) " +
                //    "WHERE(companyPayment.paymentDate >= '" + DateFrom + "' OR '" + DateFrom + "' IS Null) " +
                //    "AND(companyPayment.paymentDate <= '" + DateTo + "' OR '" + DateTo + "' IS Null) ";
                string sql = "SET dateformat dmy SELECT FORMAT(paymentDate, 'dd/MM/yyyy ') as paymentDate, " +
                     "case when companyNmEE != '' then companyNmEE else companyNmE end as 'companyNmE', FORMAT(expiredDate, 'dd/MM/yyyy') as termDate," +
                     "bNo,cNo, memberFee as true," +
                     "case when cNo = '' or companyNmE = 'The Quartier Hotel Sukhumvit 39 Bangkok' then CAST(memberFee / 1.0 as decimal(18, 2)) " +
                     "when newsFee != '' then  CAST(memberFee / 1.07 as decimal(18, 2))  " +
                     "else memberFee end as memberFee, " +
                     "case when cNo = '' or companyNmE = 'The Quartier Hotel Sukhumvit 39 Bangkok' then CAST(ROUND(memberFee * 0.07 , 3) as decimal(18, 2)) " +
                     "when newsFee != '' then  CAST(ROUND(memberFee -(memberFee / 1.07), 3) as decimal(18, 2)) " +
                     "else totalFee - memberFee end as vat, " +
                     "newsFee,totalFee,payRemark, " +
                     "case when withHolding = '1'and companyNmE = 'The Quartier Hotel Sukhumvit 39 Bangkok' then CAST(memberFee / 1.0 * 0.03 as decimal(18, 3)) " +
                     "when withHolding = '1' then CAST(ROUND((memberFee/ 1.07) * 0.03,3) as decimal(18, 3)) else '0.000' end as withHolding   " +
                     "FROM companyMember INNER JOIN companyPayment " +
                     "ON(companyMember.companyId = companyPayment.companyId) " +
                      //"WHERE(expiredDate between '" + DateFrom + "' and '" + DateTo + "') ";

                      "WHERE(paymentDate between '" + DateFrom + "' and '" + DateTo + "') AND CompanyPayment.Deleted_at IS NULL ";
                //"WHERE(paymentDate between '" + DateFrom + "' and '" + DateTo + "') ";


                //"WHERE(companyPayment.paymentDate >= '" + DateFrom + "' OR '" + DateFrom + "' IS Null) " +
                //"AND(companyPayment.paymentDate <= '" + DateTo + "' OR '" + DateTo + "' IS Null) ";
                if (payAt.Value == "Annex")
                {
                    sql = sql + " AND payRemark like '%annex%'";
                    //sql = sql + " AND payRemark like '%annex%'";
                }

                if (payMethod.Text.Trim() == "C")
                {
                    //sql = sql + " AND cp.payMethod in ('J', 'K','P') order by paymentDate,bNo";
                    sql = sql + " AND companyPayment.payMethod in ('P','C','K','J') order by paymentDate,bNo";
                }
                else
                {
                    //sql = sql + " AND cp.payMethod = '" + payMethod.Text + "' order by paymentDate ,bNo";
                    sql = sql + " AND companyPayment.payMethod = '" + payMethod.Text + "' order by paymentDate ,bNo";
                }
                ReportType = "payment history";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompayPaymentHistory.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.CommandTimeout = 1600;
				DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompayPaymentHistory.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("PayMethod", payMethod.Text));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("ReportType", ReportType));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            else if (reportType.Text == "Accrued")
            {
                string val = "";
                CreateData();
                if (month.SelectedValue == "ALL")
                {
                    val = " and noPayMonth > '0'  ";
                }
                else if (month.SelectedValue == "More than 9 month")
                {
                    val = " and noPayMonth > '8' ";
                }
                else
                {
                    val = " and noPayMonth = '" + month.SelectedValue + "' ";
                }
                string sql = "select cr.companyNmE,cr.contNm,cr.phone,cr.startDate as Start,cr.termDate as Term,cr.noPayMonth as Months,(cr.noPayMonth)*cr.totalPerMonth as Total " +
                             "from CompanyReport cr " +
                             "inner join CompanyMember cm on cr.companyId = cm.companyId " +
                             "where cr.staffId = '" + _staff + "' AND cr.noPayMonth <> 0 and cr.payMethod = '" + payMethod.SelectedValue + "' " + val +
                             "order by cr.companyNmE";

                ReportType = "accrued expense";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompayPaymentAccrued.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("PayMethod", payMethod.Text));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("ReportType", ReportType));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (reportType.Text == "Book")
            {

                //string sql = "select companyNmE,totalPerMonth,cm.payMethod,getInvoice,expiredDate " +
                //             "from CompanyPayment cp " +
                //             "left join CompanyMember cm on cp.companyId = cm.companyId " +
                //             "where effectiveDate between '"+ DateFrom + "' and '" + DateTo + "' and appliedDate between '" + DateFrom + "' and '" + DateTo + "' ";
                string sql = "SET dateformat dmy SELECT companyMember.companyNmE, companyMember.memberStatus, companyMember.getInvoice, companyMember.payMethod, " +
                    "companyPayment.expiredDate, " +
                    "companyPayment.TotalPerMonth, companyAccount.accNumber, companyAccount.bankCode " +
                    "FROM companyMember LEFT OUTER JOIN companyPayment " +
                    "ON(companyMember.companyId = companyPayment.companyId) " +
                    "LEFT OUTER JOIN companyAccount " +
                    "ON(companyMember.companyId = companyAccount.companyId " +
                    "AND companyPayment.accId = companyAccount.accId) " +
                    //"WHERE companyMember.memberStatus = 'A' AND CompanyPayment.Deleted_at IS NULL " +
                    "WHERE companyMember.memberStatus = 'A' " +


                    //"AND(companyMember.appliedDate >= '" + DateFrom + "' OR '" + DateFrom + "' is null) " +
                    //"AND(companyMember.appliedDate <= '" + DateTo + "' OR '" + DateTo + "' is null) " +
                    "AND(companyMember.appliedDate BETWEEN" + "'" + DateFrom + "'" + "AND" + "'" + DateTo + "')" +
                    "AND companyPayment.tranid = (SELECT MAX(companyPayment_2.tranid) " +
                    "FROM companyPayment companyPayment_2 " +
                    "WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
                    "ORDER BY companyMember.companyNmE ASC ";
                //if (payAt.Value == "Annex")
                //{
                //    sql = sql + " AND payRemark like '%annex%' order by companyNmE";
                //}
                //else
                //{
                //    sql = sql + " order by companyNmE";
                //}
                ReportType = "";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompayPaymentBook.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                //reportParameters.Add(new ReportParameter("PayMethod", payMethod.Text));
                //this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                //reportParameters.Add(new ReportParameter("ReportType", ReportType));
                //this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            else if (reportType.Text == "List Receiver")
            {
                string sql = "SET dateformat dmy select distinct cm.companyNmE as companyNmE,cp.totalPerMonth as totalPerMonth,cp.payMethod as payMethod,ca.accNumber as accNumber,ca.bankCode as bankCode,cp.expiredDate as expiredDate,cp.noPayMonth as Months " +
                             "from CompanyPayment cp " +
                             "inner join CompanyMember cm on cp.companyId = cm.companyId " +
                             "left JOIN CompanyAccount ca ON cp.companyId = ca.companyId and cp.accId = ca.accId " +
                             "where tranId = (" +
                             "SELECT MAX(tranId) " +
                             "FROM CompanyPayment " +
							 "WHERE companyId = cp.companyId " +
                             //") and paymentDate between '" + DateFrom + "' and '" + DateTo + "' and memberStatus = 'A' AND cp.Deleted_at IS NULL " +
                             ") and paymentDate between '" + DateFrom + "' and '" + DateTo + "' and memberStatus = 'A' " +
                             " order by cm.companyNmE";

                ReportType = "";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompayPaymentListReceiver.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                //reportParameters.Add(new ReportParameter("PayMethod", payMethod.Text));
                //this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                //reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                //this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                //reportParameters.Add(new ReportParameter("toDate", DateTo));
                //this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                //reportParameters.Add(new ReportParameter("ReportType", ReportType));
                //this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            conn.Close();
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void reportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigureScreen();
            //if (reportType.SelectedValue == "History")
            //{
            //    payMethod.Visible = true;
            //    paymethodacc.Visible = false;

            //    Months.Visible = false;
            //    month.Visible = false;

            //}
            //else if (reportType.SelectedValue == "Accrued")
            //{
            //    payMethod.Visible = false;
            //    paymethodacc.Visible = true;
            //    Months.Visible = true;
            //    month.Visible = true;
            //}
        }
        private void ConfigureScreen()
        {
            month.Visible = false;
            Months.Visible = false;
            if (reportType.SelectedItem.Text.ToUpper() == "HISTORY")
            {
                payMethod.Enabled = true;
                payMethod.Items.Clear();
                payMethod.Items.Add("C");
                payMethod.Items.Add("T");
                payMethod.Items.Add("S");
                payMethod.Items.Add("B");
            }
            else if (reportType.SelectedItem.Text.ToUpper() == "ACCRUED")
            {
                payMethod.Enabled = true;
                payMethod.Items.Clear();
                payMethod.Items.Add("K");
                payMethod.Items.Add("J");
                payMethod.Items.Add("P");
                payMethod.Items.Add("S");
                payMethod.Items.Add("T");
                payMethod.Items.Add("B");
                month.Items.Clear();
                month.Items.Add("ALL");
                //for (int i = 1; i <= 50; i++)
                //{
                //    month.Items.Add(i.ToString());
                //}
                for (int i = 1; i <= 50; i++)
                {
                    month.Items.Add(i.ToString());
                }
                month.Items.Add("More than 9 month");
                month.Visible = true;
                Months.Visible = true;
            }
            else if ((reportType.SelectedItem.Text.ToUpper() == "BOOK")
                || (reportType.SelectedItem.Text.ToUpper() == "LIST RECEIVER"))
            {
                payMethod.Items.Clear();
                payMethod.Enabled = false;
            }

        }

        private double DateDiff(string howtocompare, DateTime startDate, DateTime endDate)
        {
            double diff = 0;
            try
            {
                System.TimeSpan TS = new
                System.TimeSpan(startDate.Ticks - endDate.Ticks);
                #region converstion options
                switch (howtocompare.Trim())
                {
                    case "m":
                        diff = Convert.ToDouble(TS.TotalMinutes);
                        break;
                    case "s":
                        diff = Convert.ToDouble(TS.TotalSeconds);
                        break;
                    case "t":
                        diff = Convert.ToDouble(TS.Ticks);
                        break;
                    case "d":
                        diff = Convert.ToDouble(TS.TotalDays);
                        break;
                    case "M":
                        diff = Convert.ToDouble(TS.TotalDays / 30);
                        break;
                    case "mm":
                        diff = Convert.ToDouble(TS.TotalMilliseconds);
                        break;
                    case "yyyy":
                        diff = Convert.ToDouble(TS.TotalDays / 365);
                        break;
                    case "q":
                        diff = Convert.ToDouble((TS.TotalDays / 365) / 4);
                        break;
                    default:
                        //d
                        diff = Convert.ToDouble(TS.TotalDays);
                        break;
                }
                #endregion
            }
            catch (Exception)
            {
                diff = -1;
            }
            return diff;
        }


    }
}