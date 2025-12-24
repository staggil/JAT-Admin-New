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
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using JAT.Core;

namespace JAT.CompanyReport
{
    public partial class reportCMBill : System.Web.UI.Page
    {
        private SqlConnection conn;
        string _staffID;

		private LogActivity logActivity = new LogActivity();

		protected void Page_Load(object sender, EventArgs e)
        {
            //CreateData();
            if (Session["User"] != null)
            {
                _staffID = Session["UID"].ToString().Trim();
            }
            if (!Page.IsPostBack)
            {
                Binddata();
                ReportViewer1.Visible = false;
            }
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        protected override void InitializeCulture()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        }
        private void Binddata()
        {
            DataGrid1.DataSource = GetData().Tables["CompanyReport"].DefaultView;
            DataGrid1.DataBind();
        }
        private DataSet GetData()
        {
            //IFormatProvider culture = new CultureInfo(Session["language"].ToString(), true);
            IFormatProvider culture = new CultureInfo("en-US", true);
            connection();


            //for debug 15/05/2025 14:18
            System.Diagnostics.Debug.WriteLine("🟡 _staffID = " + _staffID);
            //end here

            ///this is sql statement which returns records
            String strSqlSelect = "SET dateformat dmy SELECT * FROM companyReport WHERE companyReport.staffId = '" + _staffID + "' " +
                " AND companyReport.noPayMonth <> 0 ";

            if (cboPayMethod.Text.Trim() != "")
            {
                strSqlSelect = strSqlSelect + " AND companyReport.payMethod = '" + cboPayMethod.Text.Trim() + "' ";
            }

            if (txtFromDate.Value.Trim() != "" && txtToDate.Value.Trim() != "")
            {
                string strFromDate = DateTime.ParseExact(txtFromDate.Value.Trim(), "dd/mm/yyyy", culture).ToString("dd/mm/yyyy");
                string strToDate = DateTime.ParseExact(txtToDate.Value.Trim(), "dd/mm/yyyy", culture).ToString("dd/mm/yyyy");
                //strSqlSelect = strSqlSelect + " AND expiredDate between '" + strFromDate + "' AND '" + strToDate + "' ";
                strSqlSelect = strSqlSelect + " AND expiredDate between '" + txtFromDate.Value.Trim() + "' AND '" + txtToDate.Value.Trim() + "' ";
            }


            //if (txtCompanyId.Value.Trim() != "")
            //{
            //    strSqlSelect = strSqlSelect + " AND companyReport.companyId LIKE '%" + txtCompanyId.Value.Trim() + "%' ";
            //}

            if (txtCompanyNameJpn.Value.Trim() != "")
            {
                strSqlSelect = strSqlSelect + " AND companyReport.companyNmJ LIKE N'%" + txtCompanyNameJpn.Value.Trim() + "%' ";
            }

            if (txtCompanyNameEng.Value.Trim() != "")
            {
                strSqlSelect = strSqlSelect + " AND companyReport.companyNmE LIKE '%" + txtCompanyNameEng.Value.Trim() + "%' ";
            }

            strSqlSelect = strSqlSelect + " ORDER BY companyReport.companyNmJ, companyReport.companyNmE ASC ";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSqlSelect, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "CompanyReport");
            return myDataSet;
        }
        
        private void CreateData()
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			IFormatProvider culture = new CultureInfo("en-US", true);
            connection();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {
                con.Open();
                String strSqlSelect = " DELETE FROM companyReport " +
                                      " WHERE staffid = '" + _staffID + "'";
                SqlCommand sqlCmd = new SqlCommand(strSqlSelect, con);
                sqlCmd.CommandTimeout = 600;
                try
                {
					sqlCmd.ExecuteNonQuery();
					string activityDetail = $"Deleted data in a table 'companyReport' where staffid is '{_staffID}' successful (user id = {staffID})";
                    logActivity.LogStaffActivity(staffID,activityDetail);
				}
                catch (SqlException ex)
                {
					//string activityDetail = $"Deleted data in a table 'companyReport' where staffid is '{_staffID}' unsuccessful [{ex.Message}] (user id = {staffID})";
                    logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                    $"{ex.Message}");
                }
				catch (Exception ex)
				{
                    //string activityDetail = $"Deleted data in a table 'companyReport' where staffid is '{_staffID}' unsuccessful [{ex.Message}] (user id = {staffID})";
                    logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
                }

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
                " AND companyMember.payMethod = '" + this.cboPayMethod.SelectedValue + "' " +
                " ORDER BY CompanyMember.companyNmE ";

                // ***
                sqlCmd.CommandText = strSqlSelect;
                //SqlDataReader myRead; = sqlCmd.ExecuteReader();
                //SqlConnection _conn = con;
                
                SqlDataAdapter _dataAdapter = new SqlDataAdapter(sqlCmd.CommandText, con);
                DataSet _ds = new DataSet();
                _dataAdapter.Fill(_ds);
                sqlCmd.ExecuteNonQuery();
                
                string strPayPeriod = "";
                string intPayDuration = "";
                string strCurrentDate02 = "";

                DateTime datExpireDate = DateTime.MinValue;
                DateTime datCurrentDate02 = DateTime.MinValue;

                string activityDetailWhile = "";

                foreach(DataRow _literal in _ds.Tables[0].Rows)
                {
                    strPayPeriod = _literal["payPeriod"].ToString();
                    intPayDuration = _literal["payDuration"].ToString();
                    datExpireDate = DateTime.Parse(_literal["expiredDate"].ToString()); 

                    string companyId = _literal["companyId"].ToString();
                    string companyNmJ = _literal["companyNmJ"].ToString();
                    string companyNmE = _literal["companyNmE"].ToString();
                    string address = _literal["address"].ToString();
                    string phone = _literal["phone"].ToString();
                    string fax = _literal["fax"].ToString();
                    string contNm = _literal["contNm"].ToString();
                    string contPosition = _literal["contPosition"].ToString();
                    string payMethod = _literal["payMethod"].ToString();
                    string totalPerMonth = _literal["totalPerMonth"].ToString();
                    string expiredDate = _literal["expiredDate"].ToString();
                    string payDuration = _literal["payDuration"].ToString(); ;

                    if (strPayPeriod.Trim() == "93")
                    {
                        if (intPayDuration != "12")
                        {
                            if (DateTime.Today.Month <= 3)
                                // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/31";
                                strCurrentDate02 = "30/03/" + DateTime.Today.Year.ToString("####");
                            else if ((DateTime.Today.Month > 3) && (DateTime.Today.Month <= 9))
                                strCurrentDate02 = "30/09/" + DateTime.Today.Year.ToString("####");
                            else if (DateTime.Today.Month > 9)
                                // strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/31";
                                strCurrentDate02 = "30/03/" + ((Int16)(DateTime.Today.Year + 1)).ToString("####");
                        }
                        else
                        {
                            if (DateTime.Today.Month <= 3)
                                // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/31";
                                strCurrentDate02 = "30/03/" + DateTime.Today.Year.ToString("####");
                            else
                                // strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/31";
                                strCurrentDate02 = "30/03/" + ((Int16)(DateTime.Today.Year + 1)).ToString("####");
                        }
                    }
                    else if (strPayPeriod.Trim() == "612")
                    {
                        if (intPayDuration != "12")
                        {
                            if (DateTime.Today.Month <= 6)
                                strCurrentDate02 = "30/06/" + DateTime.Today.Year.ToString("####");
                            else if (DateTime.Today.Month > 6)
                                // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
                                strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
                        }
                        else
                        {
                            // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
                            strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
                        }
                    }
                    else
                    {
                        if (intPayDuration != "12")
                        {
                            if (DateTime.Today.Month <= 6)
                                strCurrentDate02 = "30/06/" + DateTime.Today.Year.ToString("####");
                            else if (DateTime.Today.Month > 6)
                                // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
                                strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
                        }
                        else
                        {
                            // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
                            strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
                        }
                    }
                    datCurrentDate02 = DateTime.ParseExact(strCurrentDate02, "dd/MM/yyyy", culture);

                    if ((datExpireDate < datCurrentDate02) && (datExpireDate != DateTime.MinValue)) // calculate only member want invoice or member who not have money enough for pay by bank
                    {
                        if (drplstType.SelectedItem.Text.ToUpper() == "ACCRUED")
                        {
                            var noPayMonth = (int)DateDiff("M", datCurrentDate02, datExpireDate);
                            var startDate = DateTime.ParseExact("01" + datExpireDate.AddMonths(1).ToString("/MM/yyyy"), "dd/MM/yyyy", culture);
                            var termDate = datExpireDate.AddMonths(noPayMonth);
                            long val = Int64.Parse(totalPerMonth);
                            var total = (int)val * (int)noPayMonth;

                            activityDetailWhile = this.InsertCompanyReport(_staffID, companyId, companyNmJ, companyNmE,
                                                                           address, phone, fax, contNm, contPosition,
                                                                           payMethod, startDate, termDate, expiredDate,
                                                                           totalPerMonth, noPayMonth, total, 0);
                        }

                    if (drplstType.SelectedItem.Text.ToUpper() == "DEFERRED")
                            {
                                long vals = Int64.Parse(intPayDuration);
                                int noPayMonth = (int)vals;
                                var startDate = DateTime.ParseExact(datExpireDate.AddMonths(1).ToString("yyyy/MM/") + "01", "yyyy/M/d", culture);
                                var _year = datExpireDate.AddMonths(noPayMonth).Year;
                                var _month = datExpireDate.AddMonths(noPayMonth).Month;
                                var _maxDaysOfMonth = DateTime.DaysInMonth(_year, _month);
                                var termDate = datExpireDate.AddMonths(noPayMonth);

                            var _termDate = DateTime.Parse($"{_year}/{_month}/{_maxDaysOfMonth}");

                                long val = Int64.Parse(totalPerMonth);
                                var total = (int)val * noPayMonth;

                                activityDetailWhile = this.InsertCompanyReport(_staffID, companyId, companyNmJ, companyNmE,
                                                                               address, phone, fax, contNm, contPosition,
                                                                               payMethod, startDate, _termDate, expiredDate,
                                                                               totalPerMonth, noPayMonth, total, 0);
                            }
                        }
                        else
                        {   // *** 
                            if (drplstType.SelectedItem.Text.ToUpper() == "DEFERRED")
                            {
                                long vals = Int64.Parse(intPayDuration);
                                int noPayMonth = (int)vals;
                                var startDate = DateTime.ParseExact(datExpireDate.AddMonths(1).ToString("yyyy/MM/") + "01", "yyyy/M/d", culture);
                                var _year = datExpireDate.AddMonths(noPayMonth).Year;
                                var _month = datExpireDate.AddMonths(noPayMonth).Month;
                                var _maxDaysOfMonth = DateTime.DaysInMonth(_year, _month);
                                var termDate = datExpireDate.AddMonths(noPayMonth);

                                var _termDate = DateTime.Parse($"{_year}/{_month}/{_maxDaysOfMonth}");

                                long val = Int64.Parse(totalPerMonth);
                                var total = (int)val * noPayMonth;

                                activityDetailWhile = this.InsertCompanyReport(_staffID, companyId, companyNmJ, companyNmE,
                                                                               address, phone, fax, contNm, contPosition,
                                                                               payMethod, startDate, _termDate, expiredDate,
                                                                               totalPerMonth, noPayMonth, total, 0);
                            }
                            // ***
                        }
                    }

       //         while (myRead.Read())
       //         {
       //             strPayPeriod = myRead.GetValue(10).ToString();
       //             intPayDuration = myRead.GetValue(11).ToString();
       //             datExpireDate = (DateTime)myRead["expiredDate"];
       //             // datExpireDate = DateTime.Parse(myRead["expiredDate"].ToString());

       //             string companyId = myRead.GetValue(0).ToString();
       //             string companyNmJ = myRead.GetValue(1).ToString();
       //             string companyNmE = myRead.GetValue(2).ToString();
       //             string address = myRead.GetValue(3).ToString();
       //             string phone = myRead.GetValue(4).ToString();
       //             string fax = myRead.GetValue(5).ToString();
       //             string contNm = myRead.GetValue(6).ToString();
       //             string contPosition = myRead.GetValue(7).ToString();
       //             string payMethod = myRead.GetValue(8).ToString();
       //             string totalPerMonth = myRead.GetValue(9).ToString();
       //             string expiredDate = myRead.GetValue(12).ToString();
       //             string payDuration = myRead.GetValue(11).ToString();

       //             if (strPayPeriod.Trim() == "93")
       //             {
       //                 if (intPayDuration != "12")
       //                 {
       //                     if (DateTime.Today.Month <= 3)
       //                         // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/31";
       //                         strCurrentDate02 = "30/03/" + DateTime.Today.Year.ToString("####");
       //                     else if ((DateTime.Today.Month > 3) && (DateTime.Today.Month <= 9))
       //                         strCurrentDate02 = "30/09/" + DateTime.Today.Year.ToString("####");
       //                     else if (DateTime.Today.Month > 9)
       //                         // strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/31";
       //                         strCurrentDate02 = "30/03/" + ((Int16)(DateTime.Today.Year + 1)).ToString("####");
       //                 }
       //                 else
       //                 {
       //                     if (DateTime.Today.Month <= 3)
       //                         // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/03/31";
       //                         strCurrentDate02 = "30/03/" + DateTime.Today.Year.ToString("####");
       //                     else
       //                         // strCurrentDate02 = ((Int16)(DateTime.Today.Year + 1)).ToString("####") + "/03/31";
       //                         strCurrentDate02 = "30/03/" + ((Int16)(DateTime.Today.Year + 1)).ToString("####");
       //                 }
       //             }
       //             else if (strPayPeriod.Trim() == "612")
       //             {
       //                 if (intPayDuration != "12")
       //                 {
       //                     if (DateTime.Today.Month <= 6)
       //                         strCurrentDate02 = "30/06/" + DateTime.Today.Year.ToString("####");
       //                     else if (DateTime.Today.Month > 6)
       //                         // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
       //                         strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
       //                 }
       //                 else
       //                 {
       //                     // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
       //                     strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
       //                 }
       //             }
       //             else
       //             {
       //                 if (intPayDuration != "12")
       //                 {
       //                     if (DateTime.Today.Month <= 6)
       //                         strCurrentDate02 = "30/06/" + DateTime.Today.Year.ToString("####");
       //                     else if (DateTime.Today.Month > 6)
       //                         // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
       //                         strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
       //                 }
       //                 else
       //                 {
       //                     // strCurrentDate02 = DateTime.Today.Year.ToString("####") + "/12/31";
       //                     strCurrentDate02 = "30/12/" + DateTime.Today.Year.ToString("####");
       //                 }
       //             }
       //             datCurrentDate02 = DateTime.ParseExact(strCurrentDate02, "dd/MM/yyyy", culture);

       //             if (companyId == "001028")
       //             {
       //                 // datCurrentDate02 = DateTime.Parse("2024-03-31");
       //                 // expiredDate = datCurrentDate02.ToString();
       //                 Debug.WriteLine($"Company EXP Date : {datExpireDate}");
       //                 Debug.WriteLine($"Company Name : {companyId} {companyNmE}");
       //             }

       //             if ((datExpireDate < datCurrentDate02) && (datExpireDate != DateTime.MinValue)) // calculate only member want invoice or member who not have money enough for pay by bank
       //             {
       //                 if (drplstType.SelectedItem.Text.ToUpper() == "ACCRUED")
       //                 {
       //                     string InsertSQL = "INSERT INTO CompanyReport (staffId, companyId, companyNmJ, companyNmE, address, phone, fax, contNm, contPosition, payMethod, startDate, termDate, expiredDate, totalPerMonth, noPayMonth, total, flagPrintReport) " +
       //                                        "VALUES (@staffId, @companyId, @companyNmJ, @companyNmE, @address, @phone, @fax, @contNm, @contPosition, @payMethod, @startDate, @termDate, @expiredDate, @totalPerMonth, @noPayMonth, @total, @flagPrintReport)";
       //                     SqlCommand _sqlCmd = new SqlCommand(InsertSQL, conn);
       //                     var noPayMonth = (int)DateDiff("M", datCurrentDate02, datExpireDate);
       //                     var startDate = DateTime.ParseExact("01" + datExpireDate.AddMonths(1).ToString("/MM/yyyy"), "dd/MM/yyyy", culture);
       //                     var termDate = datExpireDate.AddMonths(noPayMonth);
       //                     long val = Int64.Parse(totalPerMonth);
       //                     var total = (int)val * (int)noPayMonth;
       //                     conn.Open();
       //                     _sqlCmd.Parameters.AddWithValue("@staffId", _staffID);
       //                     _sqlCmd.Parameters.AddWithValue("@companyId", companyId);
       //                     _sqlCmd.Parameters.AddWithValue("@companyNmJ", companyNmJ);
       //                     _sqlCmd.Parameters.AddWithValue("@companyNmE", companyNmE);
       //                     _sqlCmd.Parameters.AddWithValue("@address", address);
       //                     _sqlCmd.Parameters.AddWithValue("@phone", phone);
       //                     _sqlCmd.Parameters.AddWithValue("@fax", fax);
       //                     _sqlCmd.Parameters.AddWithValue("@contNm", contNm);
       //                     _sqlCmd.Parameters.AddWithValue("@payMethod", payMethod);
       //                     _sqlCmd.Parameters.AddWithValue("@contPosition", contPosition);
       //                     _sqlCmd.Parameters.AddWithValue("@totalPerMonth", totalPerMonth);
       //                     _sqlCmd.Parameters.AddWithValue("@expiredDate", expiredDate);
       //                     _sqlCmd.Parameters.AddWithValue("@noPayMonth", noPayMonth);
       //                     _sqlCmd.Parameters.AddWithValue("@startDate", startDate);
       //                     _sqlCmd.Parameters.AddWithValue("@termDate", termDate);
       //                     _sqlCmd.Parameters.AddWithValue("@total", total);
       //                     _sqlCmd.Parameters.AddWithValue("@flagPrintReport", false);
       //                     try
       //                     {
							//	//_sqlCmd.ExecuteNonQuery();
       //                         var _queryAffect = _sqlCmd.ExecuteNonQuery();

       //                         Debug.Print($"Insert : {_queryAffect} => {_sqlCmd.CommandText}");
       //                         foreach ( var item in _sqlCmd.Parameters ) { 
       //                             Debug.Print($"\t - {item.ToString()}"); 
       //                         }
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {staffID})";
                                
							//}
							//catch (SqlException sqlex)
							//{
							//	activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {staffID})";
							//	break;
							//}
							//catch (Exception ex)
							//{
							//	activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {staffID})";
							//	break;
							//}

							//_sqlCmd.Parameters.Clear();
       //                     conn.Close();

       //                 }

       //                 if (drplstType.SelectedItem.Text.ToUpper() == "DEFERRED")
       //                 {
       //                     //string InsertSQL = "INSERT INTO CompanyReport (staffId, companyId, companyNmJ, companyNmE, address, phone, fax, contNm, contPosition, payMethod, startDate, termDate, expiredDate, totalPerMonth, noPayMonth, total, flagPrintReport) " +
       //                     //                   "VALUES (@staffId, @companyId, @companyNmJ, @companyNmE, @address, @phone, @fax, @contNm, @contPosition, @payMethod, @startDate, @termDate, @expiredDate, @totalPerMonth, @noPayMonth, @total, @flagPrintReport)";

       //                     long vals = Int64.Parse(intPayDuration);
       //                     int noPayMonth = (int)vals;
       //                     var startDate = DateTime.ParseExact(datExpireDate.AddMonths(1).ToString("yyyy/MM/") + "01", "yyyy/M/d", culture);
       //                     var termDate = datExpireDate.AddMonths(noPayMonth);
       //                     long val = Int64.Parse(totalPerMonth);
       //                     var total = (int)val * noPayMonth;
       //                     string InsertSQL = $"INSERT INTO CompanyReport (staffId, companyId, companyNmJ, " +
       //                                             $"companyNmE, address, phone, fax, contNm, contPosition, " +
       //                                             $"payMethod, startDate, termDate, expiredDate, " +
       //                                             $"totalPerMonth, noPayMonth, total, flagPrintReport) " +
       //                                        $"VALUES ('{_staffID}', '{companyId}', '{companyNmJ}', '{companyNmE}', " +
       //                                        $"'{address}', '{phone}', '{fax}', '{contNm}', '{contPosition}', " +
       //                                        $"'{payMethod}', '{startDate}', '{termDate}', '{expiredDate}', " +
       //                                        $"'{totalPerMonth}', '{noPayMonth}', '{total}', 'false')";

       //                     SqlCommand _sqlCmd = new SqlCommand(InsertSQL, conn);

       //                     conn.Open();
       //                     //_sqlCmd.Parameters.AddWithValue("@staffId", _staffID);
       //                     //_sqlCmd.Parameters.AddWithValue("@companyId", companyId);
       //                     //_sqlCmd.Parameters.AddWithValue("@companyNmJ", companyNmJ);
       //                     //_sqlCmd.Parameters.AddWithValue("@companyNmE", companyNmE);
       //                     //_sqlCmd.Parameters.AddWithValue("@address", address);
       //                     //_sqlCmd.Parameters.AddWithValue("@phone", phone);
       //                     //_sqlCmd.Parameters.AddWithValue("@fax", fax);
       //                     //_sqlCmd.Parameters.AddWithValue("@contNm", contNm);
       //                     //_sqlCmd.Parameters.AddWithValue("@payMethod", payMethod);
       //                     //_sqlCmd.Parameters.AddWithValue("@contPosition", contPosition);
       //                     //_sqlCmd.Parameters.AddWithValue("@totalPerMonth", totalPerMonth);
       //                     //_sqlCmd.Parameters.AddWithValue("@expiredDate", expiredDate);
       //                     //_sqlCmd.Parameters.AddWithValue("@noPayMonth", noPayMonth);
       //                     //_sqlCmd.Parameters.AddWithValue("@startDate", startDate);
       //                     //_sqlCmd.Parameters.AddWithValue("@termDate", termDate);
       //                     //_sqlCmd.Parameters.AddWithValue("@total", total);
       //                     //_sqlCmd.Parameters.AddWithValue("@flagPrintReport", false);
       //                     try
       //                     {
       //                         //_sqlCmd.ExecuteNonQuery();
       //                         var _queryAffect = _sqlCmd.ExecuteNonQuery();

       //                         Debug.Print($"Insert : {_queryAffect} => {_sqlCmd.CommandText}");
       //                         foreach (var item in _sqlCmd.Parameters)
       //                         {
       //                             Debug.Print($"\t -> {item.ToString()}");
       //                         }
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {staffID})";

       //                     }
       //                     catch (SqlException sqlex)
       //                     {
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {staffID})";

       //                     }
       //                     catch (Exception ex)
       //                     {
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {staffID})";
       //                     }
       //                     _sqlCmd.Parameters.Clear();
       //                     conn.Close();
       //                 }
       //             }
       //             else
       //             {
       //                 // ***

       //                 if (drplstType.SelectedItem.Text.ToUpper() == "DEFERRED")
       //                 {
       //                     //string InsertSQL = "INSERT INTO CompanyReport (staffId, companyId, companyNmJ, companyNmE, address, phone, fax, contNm, contPosition, payMethod, startDate, termDate, expiredDate, totalPerMonth, noPayMonth, total, flagPrintReport) " +
       //                     //                   "VALUES (@staffId, @companyId, @companyNmJ, @companyNmE, @address, @phone, @fax, @contNm, @contPosition, @payMethod, @startDate, @termDate, @expiredDate, @totalPerMonth, @noPayMonth, @total, @flagPrintReport)";

       //                     long vals = Int64.Parse(intPayDuration);
       //                     int noPayMonth = (int)vals;
       //                     var startDate = DateTime.ParseExact(datExpireDate.AddMonths(1).ToString("yyyy/MM/") + "01", "yyyy/M/d", culture);
       //                     var termDate = datExpireDate.AddMonths(noPayMonth);
       //                     long val = Int64.Parse(totalPerMonth);
       //                     var total = (int)val * noPayMonth;
       //                     string InsertSQL = $"INSERT INTO CompanyReport (staffId, companyId, companyNmJ, " +
       //                                             $"companyNmE, address, phone, fax, contNm, contPosition, " +
       //                                             $"payMethod, startDate, termDate, expiredDate, " +
       //                                             $"totalPerMonth, noPayMonth, total, flagPrintReport) " +
       //                                        $"VALUES ('{_staffID}', '{companyId}', '{companyNmJ}', '{companyNmE}', " +
       //                                        $"'{address}', '{phone}', '{fax}', '{contNm}', '{contPosition}', " +
       //                                        $"'{payMethod}', '{startDate}', '{termDate}', '{expiredDate}', " +
       //                                        $"'{totalPerMonth}', '{noPayMonth}', '{total}', 'false')";

       //                     SqlCommand _sqlCmd = new SqlCommand(InsertSQL, conn);

       //                     conn.Open();
       //                     //_sqlCmd.Parameters.AddWithValue("@staffId", _staffID);
       //                     //_sqlCmd.Parameters.AddWithValue("@companyId", companyId);
       //                     //_sqlCmd.Parameters.AddWithValue("@companyNmJ", companyNmJ);
       //                     //_sqlCmd.Parameters.AddWithValue("@companyNmE", companyNmE);
       //                     //_sqlCmd.Parameters.AddWithValue("@address", address);
       //                     //_sqlCmd.Parameters.AddWithValue("@phone", phone);
       //                     //_sqlCmd.Parameters.AddWithValue("@fax", fax);
       //                     //_sqlCmd.Parameters.AddWithValue("@contNm", contNm);
       //                     //_sqlCmd.Parameters.AddWithValue("@payMethod", payMethod);
       //                     //_sqlCmd.Parameters.AddWithValue("@contPosition", contPosition);
       //                     //_sqlCmd.Parameters.AddWithValue("@totalPerMonth", totalPerMonth);
       //                     //_sqlCmd.Parameters.AddWithValue("@expiredDate", expiredDate);
       //                     //_sqlCmd.Parameters.AddWithValue("@noPayMonth", noPayMonth);
       //                     //_sqlCmd.Parameters.AddWithValue("@startDate", startDate);
       //                     //_sqlCmd.Parameters.AddWithValue("@termDate", termDate);
       //                     //_sqlCmd.Parameters.AddWithValue("@total", total);
       //                     //_sqlCmd.Parameters.AddWithValue("@flagPrintReport", false);
       //                     try
       //                     {
       //                         //_sqlCmd.ExecuteNonQuery();
       //                         var _queryAffect = _sqlCmd.ExecuteNonQuery();

       //                         Debug.Print($"Insert : {_queryAffect} => {_sqlCmd.CommandText}");
       //                         foreach (var item in _sqlCmd.Parameters)
       //                         {
       //                             Debug.Print($"\t -> {item.ToString()}");
       //                         }
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {staffID})";

       //                     }
       //                     catch (SqlException sqlex)
       //                     {
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {staffID})";

       //                     }
       //                     catch (Exception ex)
       //                     {
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {staffID})";
       //                     }
       //                     _sqlCmd.Parameters.Clear();
       //                     conn.Close();
       //                 }
       //                 // ***
       //             }
       //         }
				
                logActivity.LogStaffActivity(staffID, activityDetailWhile);
			}
        }

        private string InsertCompanyReport(string _staffID, string _companyId, string _companyNmJ, string _companyNmE, 
                                           string _address, string _phone, string _fax, string _contNm, string _contPosition, 
                                           string _payMethod, DateTime _startDate, DateTime _termDate, string _expiredDate, 
                                           string _totalPerMonth, int _noPayMonth, int _total, int _flagPrintReport)
        {
            string activityDetailWhile = "";
            string InsertSQL = $"INSERT INTO CompanyReport (staffId, companyId, companyNmJ, " +
                                $"companyNmE, address, phone, fax, contNm, contPosition, " +
                                $"payMethod, startDate, termDate, expiredDate, " +
                                $"totalPerMonth, noPayMonth, total, flagPrintReport) " +
                                $"VALUES ('{_staffID}', '{_companyId}', '{_companyNmJ}', '{_companyNmE}', " +
                                $"'{_address}', '{_phone}', '{_fax}', '{_contNm}', '{_contPosition}', " +
                                $"'{_payMethod}', '{_startDate}', '{_termDate}', '{_expiredDate}', " +
                                $"'{_totalPerMonth}', '{_noPayMonth}', '{_total}', {_flagPrintReport})";

            SqlCommand _sqlCmd = new SqlCommand(InsertSQL, conn);
            conn.Open();

            try
            {
                _sqlCmd.ExecuteNonQuery();
                activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {_staffID})";
            }
            catch (SqlException sqlex)
            {
                activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {_staffID})";
            }
            catch (Exception ex)
            {
                activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {_staffID})";
            }
            conn.Close();
            return activityDetailWhile;
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
        protected void cmdView_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;
            DataGrid1.Visible = true;
            CreateData();
            Binddata();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Value = "";
            txtToDate.Value = "";
            txtCompanyId.Value = "";
            txtCompanyNameJpn.Value = "";
            txtCompanyNameEng.Value = "";
            Binddata();
        }

        protected void cmdPrintAll_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;
            if (drplstType.Text == "Accrued")
            {
                DataGrid1.Visible = false;
                connection();
                conn.Open();
                //string sql = "SELECT * FROM CompanyReport WHERE staffId='"+_staffID+"' ";
                //sql += "ORDER BY companyNmE";
                string sql = "SELECT * FROM CompanyReport WHERE staffId='" + _staffID + "' ";
                string wherenme = "";
                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                        bool chk = chkRow.Checked;
                        if (chk || !chk)
                        {



                            /* old code commentted for test 13:41 20/05/2025
                            //string nameEGrid = row.Cells[2].Text.Replace("'", "''");
                            wherenme += "'" + row.Cells[2].Text + "',";
                            */

                            //new code 15/05/2025 15:09
                            string companyName = HttpUtility.HtmlDecode(row.Cells[2].Text);
                            System.Diagnostics.Debug.WriteLine("ชื่อบริษัท after decode: " + companyName);


                            // ✅ ใช้ชื่อที่ decode แล้วใน WHERE clause และ escape เครื่องหมาย single quote
                            wherenme += "'" + companyName.Replace("'", "''") + "',";
                            //end debug

                        }
                    }
                }
                wherenme = wherenme.Remove(wherenme.Length - 1, 1);
                wherenme = wherenme.Replace("&#39;", "''");
                wherenme = "AND companyNmE in (" + wherenme + ") ";
                sql += wherenme;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyBill.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                var tmpdate = DateTime.Now;
                var jdate = tmpdate.Year.ToString() + "年" + tmpdate.Month.ToString() + "月" + tmpdate.Day.ToString() + "日";
                reportParameters.Add(new ReportParameter("todaydate", jdate));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                conn.Close();
            }
            else
            {
                DataGrid1.Visible = false;
                connection();
                conn.Open();
                //string sql = "SELECT * FROM CompanyReport WHERE staffId='"+ _staffID + "' ";
                //sql += "ORDER BY companyNmE";
                string sql = "SELECT * FROM CompanyReport WHERE staffId='" + _staffID + "' ";
                string wherenme = "";
                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                        bool chk = chkRow.Checked;
                        if (chk || !chk)
                        {
                            wherenme += "'" + row.Cells[2].Text + "',";
                        }
                    }
                }
                wherenme = wherenme.Remove(wherenme.Length - 1, 1);
                wherenme = "AND companyNmE in (" + wherenme + ") ";
                sql += wherenme;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyBill_Deferred.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                var tmpdate = DateTime.Now;
                var jdate = tmpdate.Year.ToString() + "年" + tmpdate.Month.ToString() + "月" + tmpdate.Day.ToString() + "日";
                reportParameters.Add(new ReportParameter("todaydate", jdate));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                conn.Close();
            }
        }

        protected void cmdPrintSel_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;
            if (drplstType.Text == "Accrued")
            {
                DataGrid1.Visible = false;
                connection();
                conn.Open();

                //for debug 15/05/2025
                System.Diagnostics.Debug.WriteLine("จำนวนแถวใน DataGrid1: " + DataGrid1.Rows.Count);
                //end debug


                string sql = "SELECT * FROM " +
                        "(SELECT companyNmE, address, contNm, contPosition, startDate, termDate, noPayMonth, total " +
                        ", ROW_NUMBER() OVER(PARTITION BY companyNmE ORDER BY companyNmE) AS ROW FROM CompanyReport) AS a " +
                        "WHERE ROW = 1 ";
                string wherenme = "";



                /* commentted for test 10:20 20/05/2025
                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                        bool chk = chkRow.Checked;
                        if (chk)
                        {

                            // 🟡 จุดที่ใส่ Debug เพื่อตรวจชื่อบริษัท 15/05/2025
                            System.Diagnostics.Debug.WriteLine("ชื่อบริษัทที่เลือก: " + row.Cells[2].Text);
                            //end debug

                            //new code 15/05/2025 15:09
                            string companyName = HttpUtility.HtmlDecode(row.Cells[2].Text);
                            System.Diagnostics.Debug.WriteLine("✅ ชื่อบริษัท after decode: " + companyName);


                            // ✅ ใช้ชื่อที่ decode แล้วใน WHERE clause และ escape เครื่องหมาย single quote
                            wherenme += "'" + companyName.Replace("'", "''") + "',";
                            //end debug



                            /*old code commented 15/05/2025 15:12
                            wherenme += "'" + row.Cells[2].Text + "',";
                            


                        }
                    }
                }
                */

                //new code for fix as customer want  11:32 20/05/2025
                int dataRowCount = 0;
                GridViewRow singleRow = null;

                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        dataRowCount++;
                        CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                        bool chk = chkRow.Checked;

                        if (chk)
                        {
                            string companyName = HttpUtility.HtmlDecode(row.Cells[2].Text);
                            wherenme += "'" + companyName.Replace("'", "''") + "',";
                        }
                        else
                        {
                            singleRow = row; // เก็บไว้กรณียังไม่มี checkbox ใดถูกติ๊ก
                        }
                    }
                }
                //เจอแถวเดียว
                if (wherenme == "" && dataRowCount == 1 && singleRow != null)
                {
                    string companyName = HttpUtility.HtmlDecode(singleRow.Cells[2].Text);
                    wherenme += "'" + companyName.Replace("'", "''") + "',";
                }

                if (string.IsNullOrEmpty(wherenme))
                {
                    // warning
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please choose at least one bill.');", true);
                    return;
                }

                //new code end here

                try
                {
                    wherenme = wherenme.Remove(wherenme.Length - 1, 1);
                    wherenme = wherenme.Replace("&#39;", "''");
                    wherenme = "AND companyNmE in (" + wherenme + ") ";
                    sql += wherenme;
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyBill.rdlc");
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    var tmpdate = DateTime.Now;
                    var jdate = tmpdate.Year.ToString() + "年" + tmpdate.Month.ToString() + "月" + tmpdate.Day.ToString() + "日";
                    reportParameters.Add(new ReportParameter("todaydate", jdate));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                catch { }
                conn.Close();
            }
            else
            {
                DataGrid1.Visible = false;
                connection();
                conn.Open();

                string sql = "SELECT * FROM CompanyReport WHERE staffId='" + _staffID + "' ";
                string wherenme = "";
                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                        bool chk = chkRow.Checked;
                        if (chk)
                        {
                            wherenme += "'" + row.Cells[2].Text + "',";
                        }
                    }
                }
                try
                {
                    wherenme = wherenme.Remove(wherenme.Length - 1, 1);
                    wherenme = wherenme.Replace("&#39;", "''");
                    wherenme = "AND companyNmE in (" + wherenme + ") ";
                    sql += wherenme;
                    sql += "ORDER BY companyNmE";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CompanyReport/ReportPage/CompanyBill_Deferred.rdlc");
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    var tmpdate = DateTime.Now;
                    var jdate = tmpdate.Year.ToString() + "年" + tmpdate.Month.ToString() + "月" + tmpdate.Day.ToString() + "日";
                    reportParameters.Add(new ReportParameter("todaydate", jdate));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                catch { }
                conn.Close();
            }

        }
    }
}