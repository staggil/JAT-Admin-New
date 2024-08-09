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
					string activityDetail = $"Deleted data in a table 'companyReport' where staffid is '{_staffID}' unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Deleted data in a table 'companyReport' where staffid is '{_staffID}' unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
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
                SqlDataReader myRead = sqlCmd.ExecuteReader();

                string strPayPeriod = "";
                string intPayDuration = "";
                //string datExpireDate;
                string strCurrentDate02 = "";
                //string datCurrentDate02 = "";

                DateTime datExpireDate = DateTime.MinValue;
                DateTime datCurrentDate02 = DateTime.MinValue;

                string activityDetailWhile = "";
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

                    if (companyId == "001120")
                        Debug.WriteLine($"Company Name : {companyId} {companyNmE}");

                    if ((datExpireDate < datCurrentDate02) && (datExpireDate != DateTime.MinValue)) // calculate only member want invoice or member who not have money enough for pay by bank
                    {
                        if (drplstType.SelectedItem.Text.ToUpper() == "ACCRUED")
                        {
                            string InsertSQL = "INSERT INTO CompanyReport (staffId, companyId, companyNmJ, companyNmE, address, phone, fax, contNm, contPosition, payMethod, startDate, termDate, expiredDate, totalPerMonth, noPayMonth, total, flagPrintReport) " +
                                               "VALUES (@staffId, @companyId, @companyNmJ, @companyNmE, @address, @phone, @fax, @contNm, @contPosition, @payMethod, @startDate, @termDate, @expiredDate, @totalPerMonth, @noPayMonth, @total, @flagPrintReport)";
                            SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                            var noPayMonth = (int)DateDiff("M", datCurrentDate02, datExpireDate);
                            var startDate = DateTime.ParseExact("01" + datExpireDate.AddMonths(1).ToString("/MM/yyyy"), "dd/MM/yyyy", culture);
                            var termDate = datExpireDate.AddMonths(noPayMonth);
                            long val = Int64.Parse(totalPerMonth);
                            var total = (int)val * (int)noPayMonth;
                            conn.Open();
                            vlozSQL.Parameters.AddWithValue("@staffId", _staffID);
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
                            vlozSQL.Parameters.AddWithValue("@noPayMonth", noPayMonth);
                            vlozSQL.Parameters.AddWithValue("@startDate", startDate);
                            vlozSQL.Parameters.AddWithValue("@termDate", termDate);
                            vlozSQL.Parameters.AddWithValue("@total", total);
                            vlozSQL.Parameters.AddWithValue("@flagPrintReport", false);
                            try
                            {
								//vlozSQL.ExecuteNonQuery();
                                var _queryAffect = vlozSQL.ExecuteNonQuery();

                                Debug.Print($"Insert : {_queryAffect} => {vlozSQL.CommandText}");
                                foreach ( var item in vlozSQL.Parameters ) { 
                                    Debug.Print($"\t - {item.ToString()}"); 
                                }
                                activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {staffID})";
                                
							}
							catch (SqlException sqlex)
							{
								activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {staffID})";
								break;
							}
							catch (Exception ex)
							{
								activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {staffID})";
								break;
							}

							vlozSQL.Parameters.Clear();
                            conn.Close();

                        }

                        if (drplstType.SelectedItem.Text.ToUpper() == "DEFERRED")
                        {
                            //string InsertSQL = "INSERT INTO CompanyReport (staffId, companyId, companyNmJ, companyNmE, address, phone, fax, contNm, contPosition, payMethod, startDate, termDate, expiredDate, totalPerMonth, noPayMonth, total, flagPrintReport) " +
                            //                   "VALUES (@staffId, @companyId, @companyNmJ, @companyNmE, @address, @phone, @fax, @contNm, @contPosition, @payMethod, @startDate, @termDate, @expiredDate, @totalPerMonth, @noPayMonth, @total, @flagPrintReport)";

                            long vals = Int64.Parse(intPayDuration);
                            int noPayMonth = (int)vals;
                            var startDate = DateTime.ParseExact(datExpireDate.AddMonths(1).ToString("yyyy/MM/") + "01", "yyyy/M/d", culture);
                            var termDate = datExpireDate.AddMonths(noPayMonth);
                            long val = Int64.Parse(totalPerMonth);
                            var total = (int)val * noPayMonth;
                            string InsertSQL = $"INSERT INTO CompanyReport (staffId, companyId, companyNmJ, " +
                                                    $"companyNmE, address, phone, fax, contNm, contPosition, " +
                                                    $"payMethod, startDate, termDate, expiredDate, " +
                                                    $"totalPerMonth, noPayMonth, total, flagPrintReport) " +
                                               $"VALUES ('{_staffID}', '{companyId}', '{companyNmJ}', '{companyNmE}', " +
                                               $"'{address}', '{phone}', '{fax}', '{contNm}', '{contPosition}', " +
                                               $"'{payMethod}', '{startDate}', '{termDate}', '{expiredDate}', " +
                                               $"'{totalPerMonth}', '{noPayMonth}', '{total}', 'false')";

                            SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);

                            conn.Open();
                            //vlozSQL.Parameters.AddWithValue("@staffId", _staffID);
                            //vlozSQL.Parameters.AddWithValue("@companyId", companyId);
                            //vlozSQL.Parameters.AddWithValue("@companyNmJ", companyNmJ);
                            //vlozSQL.Parameters.AddWithValue("@companyNmE", companyNmE);
                            //vlozSQL.Parameters.AddWithValue("@address", address);
                            //vlozSQL.Parameters.AddWithValue("@phone", phone);
                            //vlozSQL.Parameters.AddWithValue("@fax", fax);
                            //vlozSQL.Parameters.AddWithValue("@contNm", contNm);
                            //vlozSQL.Parameters.AddWithValue("@payMethod", payMethod);
                            //vlozSQL.Parameters.AddWithValue("@contPosition", contPosition);
                            //vlozSQL.Parameters.AddWithValue("@totalPerMonth", totalPerMonth);
                            //vlozSQL.Parameters.AddWithValue("@expiredDate", expiredDate);
                            //vlozSQL.Parameters.AddWithValue("@noPayMonth", noPayMonth);
                            //vlozSQL.Parameters.AddWithValue("@startDate", startDate);
                            //vlozSQL.Parameters.AddWithValue("@termDate", termDate);
                            //vlozSQL.Parameters.AddWithValue("@total", total);
                            //vlozSQL.Parameters.AddWithValue("@flagPrintReport", false);
                            try
                            {
                                //vlozSQL.ExecuteNonQuery();
                                var _queryAffect = vlozSQL.ExecuteNonQuery();

                                Debug.Print($"Insert : {_queryAffect} => {vlozSQL.CommandText}");
                                foreach (var item in vlozSQL.Parameters)
                                {
                                    Debug.Print($"\t -> {item.ToString()}");
                                }
                                activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {staffID})";

                            }
                            catch (SqlException sqlex)
                            {
                                activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {staffID})";

                            }
                            catch (Exception ex)
                            {
                                activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {staffID})";
                            }
                            vlozSQL.Parameters.Clear();
                            conn.Close();
                        }
                    }
                    else
                    {
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

       //                     SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                            
       //                     conn.Open();
       //                     //vlozSQL.Parameters.AddWithValue("@staffId", _staffID);
       //                     //vlozSQL.Parameters.AddWithValue("@companyId", companyId);
       //                     //vlozSQL.Parameters.AddWithValue("@companyNmJ", companyNmJ);
       //                     //vlozSQL.Parameters.AddWithValue("@companyNmE", companyNmE);
       //                     //vlozSQL.Parameters.AddWithValue("@address", address);
       //                     //vlozSQL.Parameters.AddWithValue("@phone", phone);
       //                     //vlozSQL.Parameters.AddWithValue("@fax", fax);
       //                     //vlozSQL.Parameters.AddWithValue("@contNm", contNm);
       //                     //vlozSQL.Parameters.AddWithValue("@payMethod", payMethod);
       //                     //vlozSQL.Parameters.AddWithValue("@contPosition", contPosition);
       //                     //vlozSQL.Parameters.AddWithValue("@totalPerMonth", totalPerMonth);
       //                     //vlozSQL.Parameters.AddWithValue("@expiredDate", expiredDate);
       //                     //vlozSQL.Parameters.AddWithValue("@noPayMonth", noPayMonth);
       //                     //vlozSQL.Parameters.AddWithValue("@startDate", startDate);
       //                     //vlozSQL.Parameters.AddWithValue("@termDate", termDate);
       //                     //vlozSQL.Parameters.AddWithValue("@total", total);
       //                     //vlozSQL.Parameters.AddWithValue("@flagPrintReport", false);
							//try
							//{
       //                         //vlozSQL.ExecuteNonQuery();
       //                         var _queryAffect = vlozSQL.ExecuteNonQuery();

       //                         Debug.Print($"Insert : {_queryAffect} => {vlozSQL.CommandText}");
       //                         foreach (var item in vlozSQL.Parameters)
       //                         {
       //                             Debug.Print($"\t -> {item.ToString()}");
       //                         }
       //                         activityDetailWhile = $"Added new data into a table 'CompanyReport' successful (user id = {staffID})";
								
							//}
							//catch (SqlException sqlex)
							//{
							//	activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{sqlex.Message}] (user id = {staffID})";

							//}
							//catch (Exception ex)
							//{
							//	activityDetailWhile = $"Added new data into a table 'CompanyReport' unsuccessful [{ex.Message}] (user id = {staffID})";
							//}
							//vlozSQL.Parameters.Clear();
       //                     conn.Close();
       //                 }
                    }
                    
                    Thread.Sleep(50);
                }
				logActivity.LogStaffActivity(staffID, activityDetailWhile);
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
            catch (Exception e)
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
                            //string nameEGrid = row.Cells[2].Text.Replace("'", "''");
                            wherenme += "'" + row.Cells[2].Text + "',";
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

                string sql = "SELECT * FROM " +
                        "(SELECT companyNmE, address, contNm, contPosition, startDate, termDate, noPayMonth, total " +
                        ", ROW_NUMBER() OVER(PARTITION BY companyNmE ORDER BY companyNmE) AS ROW FROM CompanyReport) AS a " +
                        "WHERE ROW = 1 ";
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