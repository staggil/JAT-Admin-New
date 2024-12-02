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
using System.Web.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using JATMEMBER.query;

namespace JAT.Company
{
    public partial class companyEntry : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private LogActivity logActivity = new LogActivity();
        string companyId;
        //var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
        //conn = new SqlConnection(connectionStr.ConnectionString);
        private static string memberstatus;
        private static string cancelDateTmp;

        string toDayDateSh = DateTime.Now.ToString("yyyy-MMM-dd", new CultureInfo("en-US"));
        DateTime toDayDateTime = DateTime.Now;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            companyNmJ.Disabled = false;
            companyNmE.Disabled = false;
            companyNmEE.Disabled = false;
            busType.Disabled = false;
            appliedDate.Disabled = false;
            address.Disabled = false;
            phone.Disabled = false;
            fax.Disabled = false;
            email.Disabled = false;

            // 2024-08-21 at 10.38am : Toon add
            txtPassword.Disabled = false;
            // -- End add --

            establishedDate.Disabled = false;
            sendType.Disabled = false;
            memberStatus.Disabled = false;
            represID.Disabled = false;
            //Retrieve.Enabled = true;
            represNm.Disabled = false;
            represNmE.Disabled = false;
            represEm.Disabled = false;
            represTp.Disabled = false;
            represPosition.Disabled = false;
            personinchargeNm.Disabled = false;
            personinchargeNmE.Disabled = false;
            personinchargeEm.Disabled = false;
            personinchargeTp.Disabled = false;
            personinchargePosition.Disabled = false;
            AccNm.Disabled = false;
            AccNmE.Disabled = false;
            AccEm.Disabled = false;
            AccTp.Disabled = false;
            AccPosition.Disabled = false;
            remark.Enabled = true;
            getInvoice.Enabled = true;
            withHolding.Enabled = true;
            payMethod.Disabled = false;
            payPeriod.Disabled = false;
            payDuration.Enabled = true;
            taxID.Disabled = false;
            companyId = Request.QueryString["companyId"];
            if (companyId != null)
            {
                BindData2();
                var showMem2 = "(" + companyId + ")";
                id.Text = showMem2;
                addBTN.Visible = true;
                Button1.Visible = false;
                cancel.Visible = false;
                this.btnAutoGenPwd.Disabled = false;
                companyNmJ.Disabled = true;
                companyNmE.Disabled = true;
                companyNmEE.Disabled = true;
                busType.Disabled = true;
                appliedDate.Disabled = true;
                address.Disabled = true;
                phone.Disabled = true;
                fax.Disabled = true;
                email.Disabled = true;

                // 2024-08-21 at 10.39am : Toon add
                txtPassword.Disabled = true;
                // -- End add --

                establishedDate.Disabled = true;
                sendType.Disabled = true;
                memberStatus.Disabled = true;
                represID.Disabled = true;
                //Retrieve.Enabled = false;
                represNm.Disabled = true;
                represNmE.Disabled = true;
                represEm.Disabled = true;
                represTp.Disabled = true;
                represPosition.Disabled = true;
                personinchargeNm.Disabled = true;
                personinchargeNmE.Disabled = true;
                personinchargeEm.Disabled = true;
                personinchargeTp.Disabled = true;
                personinchargePosition.Disabled = true;
                AccNm.Disabled = true;
                AccNmE.Disabled = true;
                AccEm.Disabled = true;
                AccTp.Disabled = true;
                AccPosition.Disabled = true;
                remark.Enabled = false;
                getInvoice.Enabled = false;
                withHolding.Enabled = false;
                payMethod.Disabled = true;
                payPeriod.Disabled = true;
                payDuration.Enabled = false;
                taxID.Disabled = true;
                if (!Page.IsPostBack)
                {
                    update.Visible = false;
                    BindData();
                }
            }
            else
            {
                update.Visible = false;
            }
        }
        protected void BindData2()
        {
            connection();
            //IFormatProvider culture = new CultureInfo("en-US", true);
            SqlCommand sc;
            SqlDataReader rd;
            string sql = "SELECT * FROM CompanyMember WHERE companyId = '" + companyId + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //show in member information
                    lbupdate.Text = ((DateTime)rd.GetValue(7)).ToString("dd/MM/yyyy hh:mm:ss tt").ToString().Replace("1/1/2443", " ");
                }

            }
            catch { }

        }

        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        protected void increment()
        {
            for (int i = 1; i < 10000; i++)
            {
                string value = String.Format("{0:D5}", i);
            }

        }

        protected void BindData()
        {
            connection();
            //IFormatProvider culture = new CultureInfo("en-US", true);
            SqlCommand sc;
            //SqlDataReader rd;
            //string sql = "SELECT *,FORMAT(cancelDate, 'dd/MM/yyyy', 'en-us') FROM CompanyMember LEFT JOIN SStaff ON updatedBy=staffID LEFT JOIN PrivateDetail ON represID=memberid WHERE companyId = '" + companyId + "'";
            
            // 2024-08-21 at 04.02pm : Toon revise
            string sql = $"SELECT cm.companyId, cm.companyNmJ, cm.companyNmE, cm.companyNmEE, cm.busType, \n" +
                         $"       cm.appliedDate, cm.cancelDate, cm.cancelDate, cm.address, cm.phone, cm.fax, \n" +
                         $"       cm.email, cm.establishedDate, cm.sendType, cm.memberStatus, cm.email, \n" +
                         $"       cm.represID, cm.represNm, cm.represNmE, cm.represEm, cm.represTp, \n" +
                         $"       cm.represPosition, cm.personinchargeNm, cm.personinchargeNmE, \n" +
                         $"       cm.personinchargeEm, cm.personinchargeTp, cm.personinchargePosition, cm.AccNm, \n" +
                         $"       cm.AccNmE, cm.AccEm, cm.AccTp, cm.AccPosition, cm.remark, cm.getInvoice, \n" +
                         $"       cm.withHolding, cm.payMethod, cm.payPeriod, cm.payDuration, cm.updatedBy, \n" +
                         $"       cm.TaxID, mu.PASSWORD, FORMAT(cancelDate, 'dd/MM/yyyy', 'en-us'), \n" +
                         $"       ss.staffFName \n" +
                         $"FROM CompanyMember cm \n" +
                         $"       LEFT JOIN SStaff ss ON cm.updatedBy = ss.staffID \n" +
                         $"       LEFT JOIN PrivateDetail pd ON cm.represID = pd.memberid \n" +
                         $"       LEFT JOIN m_user mu ON pd.memberid = mu.memberId \n" +
                         $"WHERE companyId = '{companyId}' \n";

            try
            {
                // conn.Open();
                
                // 2024-09-18 05.49pm : 
                var _dataTable = new DataTable();
                sc = new SqlCommand(sql, conn);
                var _adapter = new SqlDataAdapter(sc);

                conn.Open();
                _adapter.Fill(_dataTable);
                conn.Close();

                //show in member information
                companyNmJ.Value = _dataTable.Rows[0][1].ToString(); // rd.GetValue(1).ToString();
                companyNmE.Value = _dataTable.Rows[0][2].ToString(); // rd.GetValue(2).ToString();
                companyNmEE.Value = _dataTable.Rows[0][3].ToString();  // rd.GetValue(3).ToString();
                busType.Value = _dataTable.Rows[0][4].ToString();  // rd.GetValue(4).ToString();

                appliedDate.Value = DateTime.Parse(_dataTable.Rows[0][5].ToString()).ToString("dd/MM/yyyy"); // ((DateTime)rd.GetValue(5)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];

                statusdate.Text = _dataTable.Rows[0][7].ToString(); // rd.GetValue(6).ToString();
                cancelDateTmp = _dataTable.Rows[0][7].ToString(); // rd.GetValue(6).ToString();
                address.Value = _dataTable.Rows[0][8].ToString(); // rd.GetValue(8).ToString();
                phone.Value = _dataTable.Rows[0][9].ToString(); // rd.GetValue(9).ToString();
                fax.Value = _dataTable.Rows[0][10].ToString(); // rd.GetValue(10).ToString();
                email.Value = _dataTable.Rows[0][11].ToString(); // rd.GetValue(11).ToString();
                establishedDate.Value = DateTime.Parse(_dataTable.Rows[0][12].ToString()).ToString("dd/MM/yyyy"); // ((DateTime)rd.GetValue(12)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                sendType.Value = _dataTable.Rows[0][13].ToString(); // rd.GetValue(13).ToString();
                memberStatus.Value = _dataTable.Rows[0][14].ToString(); // rd.GetValue(14).ToString();
                memberstatus = _dataTable.Rows[0][14].ToString(); // rd.GetValue(14).ToString();
                represMem.Text = _dataTable.Rows[0][15].ToString(); // rd.GetValue(15).ToString();
                represID.Value = _dataTable.Rows[0][16].ToString(); // rd.GetValue(16).ToString();

                represNm.Value = _dataTable.Rows[0][17].ToString(); // rd.GetValue(17).ToString();
                represNmE.Value = _dataTable.Rows[0][18].ToString(); // rd.GetValue(18).ToString();
                represEm.Value =_dataTable.Rows[0][19].ToString(); //  rd.GetValue(19).ToString();
                represTp.Value = _dataTable.Rows[0][20].ToString(); // rd.GetValue(20).ToString();
                represPosition.Value = _dataTable.Rows[0][21].ToString(); // rd.GetValue(21).ToString();
                personinchargeNm.Value = _dataTable.Rows[0][22].ToString(); // rd.GetValue(22).ToString();
                personinchargeNmE.Value = _dataTable.Rows[0][23].ToString(); // rd.GetValue(23).ToString();
                personinchargeEm.Value = _dataTable.Rows[0][24].ToString(); // rd.GetValue(24).ToString();
                personinchargeTp.Value = _dataTable.Rows[0][25].ToString(); // rd.GetValue(25).ToString();
                personinchargePosition.Value = _dataTable.Rows[0][26].ToString(); // rd.GetValue(26).ToString();
                AccNm.Value = _dataTable.Rows[0][27].ToString(); // rd.GetValue(27).ToString();
                AccNmE.Value = _dataTable.Rows[0][28].ToString(); // rd.GetValue(28).ToString();
                AccEm.Value = _dataTable.Rows[0][29].ToString(); // rd.GetValue(29).ToString();
                AccTp.Value = _dataTable.Rows[0][30].ToString(); // rd.GetValue(30).ToString();
                AccPosition.Value = _dataTable.Rows[0][31].ToString(); // rd.GetValue(31).ToString();
                remark.Text = _dataTable.Rows[0][32].ToString(); // rd.GetValue(32).ToString();
                getInvoice.Checked = (bool)_dataTable.Rows[0][33]; // // (bool)rd.GetValue(33);

                withHolding.Checked = (bool)_dataTable.Rows[0][34]; // (bool)rd.GetValue(34);
                payMethod.Value = _dataTable.Rows[0][35].ToString(); // rd.GetValue(35).ToString();
                payPeriod.Value = _dataTable.Rows[0][36].ToString(); // rd.GetValue(36).ToString();
                payDuration.SelectedValue = _dataTable.Rows[0][37].ToString(); // rd.GetValue(37).ToString();
                updateBy.Text = _dataTable.Rows[0][42].ToString(); // rd.GetValue(38).ToString();
                taxID.Value = _dataTable.Rows[0][39].ToString(); // rd.GetValue(39).ToString();
                this.txtPassword.Value = _dataTable.Rows[0][40].ToString(); // rd.GetValue(40).ToString();
                // *** End of Fix ***

                //rd = sc.ExecuteReader();
                //while (rd.Read())
                //{
                //    //show in member information
                //    companyNmJ.Value = rd.GetValue(1).ToString();
                //    companyNmE.Value = rd.GetValue(2).ToString();
                //    companyNmEE.Value = rd.GetValue(3).ToString();
                //    busType.Value = rd.GetValue(4).ToString();
                //    //appliedDate.Value = ((DateTime)rd.GetValue(5)).ToString("d/M/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                //    appliedDate.Value = ((DateTime)rd.GetValue(5)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];

                //    //appliedDate.Value = rd.GetValue(5).ToString().Replace("1/1/2443", " ").Split(' ')[0];
                //    //appliedDate.Value = rd.GetValue(5).ToString();
                //    //statusdate.Text = ((DateTime)rd.GetValue(8)).ToString("yyyy/MM/dd").ToString();

                //    //statusdate.Text = rd.GetValue(71).ToString();
                //    //cancelDateTmp = rd.GetValue(71).ToString();

                //    statusdate.Text = rd.GetValue(6).ToString();
                //    cancelDateTmp = rd.GetValue(6).ToString();
                //    //address.Value = rd.GetValue(12).ToString();
                //    address.Value = rd.GetValue(8).ToString();
                //    //phone.Value = rd.GetValue(13).ToString();
                //    phone.Value = rd.GetValue(9).ToString();
                //    //fax.Value = rd.GetValue(14).ToString();
                //    fax.Value = rd.GetValue(10).ToString();
                //    //email.Value = rd.GetValue(15).ToString();
                //    email.Value = rd.GetValue(11).ToString();
                //    //establishedDate.Value = ((DateTime)rd.GetValue(6)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                //    establishedDate.Value = ((DateTime)rd.GetValue(12)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                //    //establishedDate.Value = rd.GetValue(6).ToString().Replace("1/1/2443", " ").Split(' ')[0];
                //    //establishedDate.Value = rd.GetValue(6).ToString();
                //    //sendType.Value = rd.GetValue(10).ToString();
                //    sendType.Value = rd.GetValue(13).ToString();
                //    //memberStatus.Value = rd.GetValue(9).ToString();
                //    //memberstatus = rd.GetValue(9).ToString();//variable to check for update
                //    memberStatus.Value = rd.GetValue(14).ToString();
                //    memberstatus = rd.GetValue(14).ToString();//variable to check for update
                //                                              //represMem.Text = rd.GetValue(63).ToString();
                //                                              //represMem.Text = rd.GetValue(16).ToString();

                //    //represID.Value = rd.GetValue(16).ToString();
                //    //represMem.Text = rd.GetValue(9).ToString();
                //    represMem.Text = rd.GetValue(15).ToString();
                //    represID.Value = rd.GetValue(16).ToString();

                //    represNm.Value = rd.GetValue(17).ToString();
                //    represNmE.Value = rd.GetValue(18).ToString();
                //    //represEm.Value = rd.GetValue(32).ToString();
                //    represEm.Value = rd.GetValue(19).ToString();
                //    //represTp.Value = rd.GetValue(33).ToString();
                //    represTp.Value = rd.GetValue(20).ToString();
                //    //represPosition.Value = rd.GetValue(19).ToString();
                //    represPosition.Value = rd.GetValue(21).ToString();
                //    //personinchargeNm.Value = rd.GetValue(34).ToString();
                //    //personinchargeNmE.Value = rd.GetValue(20).ToString();
                //    //personinchargeEm.Value = rd.GetValue(36).ToString();
                //    //personinchargeTp.Value = rd.GetValue(37).ToString();
                //    //personinchargePosition.Value = rd.GetValue(21).ToString();
                //    personinchargeNm.Value = rd.GetValue(22).ToString();
                //    personinchargeNmE.Value = rd.GetValue(23).ToString();
                //    personinchargeEm.Value = rd.GetValue(24).ToString();
                //    personinchargeTp.Value = rd.GetValue(25).ToString();
                //    personinchargePosition.Value = rd.GetValue(26).ToString();
                //    //AccNm.Value = rd.GetValue(39).ToString();
                //    //AccNmE.Value = rd.GetValue(40).ToString();
                //    //AccEm.Value = rd.GetValue(41).ToString();
                //    //AccTp.Value = rd.GetValue(42).ToString();
                //    //AccPosition.Value = rd.GetValue(43).ToString();
                //    AccNm.Value = rd.GetValue(27).ToString();
                //    AccNmE.Value = rd.GetValue(28).ToString();
                //    AccEm.Value = rd.GetValue(29).ToString();
                //    AccTp.Value = rd.GetValue(30).ToString();
                //    AccPosition.Value = rd.GetValue(31).ToString();
                //    //remark.Text = rd.GetValue(22).ToString();
                //    remark.Text = rd.GetValue(32).ToString();
                //    //bool ch1 = (bool)rd.GetValue(26);
                //    //getInvoice.Checked = ch1;
                //    getInvoice.Checked = (bool)rd.GetValue(33);

                //    //bool ch2 = (bool)rd.GetValue(27);
                //    //withHolding.Checked = ch2;

                //    withHolding.Checked = (bool)rd.GetValue(34);
                //    //payMethod.Value = rd.GetValue(23).ToString();
                //    //payPeriod.Value = rd.GetValue(24).ToString();
                //    //payDuration.SelectedValue = rd.GetValue(25).ToString();
                //    //updateBy.Text = rd.GetValue(46).ToString();
                //    payMethod.Value = rd.GetValue(35).ToString();
                //    payPeriod.Value = rd.GetValue(36).ToString();
                //    payDuration.SelectedValue = rd.GetValue(37).ToString();
                //    updateBy.Text = rd.GetValue(38).ToString();
                //    //taxID.Value = rd.GetValue(44).ToString();
                //    taxID.Value = rd.GetValue(39).ToString();
                //    this.txtPassword.Value = rd.GetValue(40).ToString();
                //}
            }
            catch(Exception err) { Debug.WriteLine($"ERROR : {err.Message}"); }

            // -- End revise --

            finally
            {
                if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                {
                    updateBy.Text = "N/A";
                }
                if (represMem.Text != "A" && represMem.Text != "NA")
                {
                    represMem.Text = "Not a member";
                }
                if (memberStatus.Value == "NA")
                {
                    statusdate.Visible = true;
                }
                else
                {
                    statusdate.Visible = false;
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;
            string sql = "select top 1 companyId from CompanyMember order by companyId desc; ";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    lbl.Text = rd.GetValue(0).ToString();
                }
                conn.Close();
            }
            catch { }
            long val = Int64.Parse(lbl.Text);
            long v = ++val;
            string comid = String.Format("{0:D6}", v);
            //Label1.Text = comid;
            string comNmJ = companyNmJ.Value.ToString();
            string comNmE = companyNmE.Value.ToString();
            string comNmEE = companyNmEE.Value.ToString();
            string remarkInput = remark.Text;
            comNmJ = comNmJ.Replace("'", "''");
            comNmE = comNmE.Replace("'", "''");
            comNmEE = comNmEE.Replace("'", "''");
            remarkInput = remarkInput.Replace("'", "''");
            string cancelDate = null;
            if (memberStatus.Value == "NA")
            {
                cancelDate = toDayDateTime.ToString("dd/MM/yyyy");
            }
            var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;


			cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyMember VALUES('" + comid + "' , N'" + comNmJ + "', '" + comNmE + "', '" + comNmEE + "', N'" + busType.Value + "', " + Date_MsSqlStandard.CastQuery(appliedDate.Value) + ", " + Date_MsSqlStandard.CastQuery(establishedDate.Value) + ", '" + " " + "', '" + cancelDate + "', '"
                                                                          + memberStatus.Value + "', '" + sendType.Value + "', '" + " " + "', '" + address.Value + "', '" + phone.Value + "', '" + fax.Value + "', '" + email.Value + "', '" + represID.Value + "', N'" + represNm.Value + "', '"
                                                                          + represNmE.Value + "', '" + represPosition.Value + "', '" + personinchargeNmE.Value + "', '" + personinchargePosition.Value + "', '" + remarkInput + "', '" + payMethod.Value + "', '" + payPeriod.Value + "', '" + payDuration.SelectedValue + "', '"
                                                                          + getInvoice.Checked + "', '" + withHolding.Checked + "', '" + uid + "', '" + " " + "', '" + " " + "', '" + " " + "', '" + represEm.Value + "', '" + represTp.Value + "', N'" + personinchargeNm.Value + "', '"
                                                                          + " " + "', '" + personinchargeEm.Value + "', '" + personinchargeTp.Value + "', '" + " " + "', N'" + AccNm.Value + "', '" + AccNmE.Value + "', '" + AccEm.Value + "', '" + AccTp.Value + "', '" + AccPosition.Value + "', '" + taxID.Value.ToString().Trim() + "')", conn);


            
            
            
            

            ;
            //cmd = new SqlCommand("INSERT INTO dbo.test VALUES('" + comid + "','" + companyNmJ.Text + "','" + companyNmE.Text + "')", conn);
            conn.Open();
   //         int k = cmd.ExecuteNonQuery();
   //         if (k != 0)
   //         {
   //             //Label1.Text = "Record Inserted Succesfully into the Database";
   //             logActivity.LogStaffActivity(staffID, activityDetail);
			//}

			
			try
			{
                cmd.ExecuteNonQuery();
				//Label1.Text = "Record Inserted Succesfully into the Database";
				string activityDetail = $"Added new data into a table 'CompanyMember' successful (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
            }
            catch (SqlException ex)
            {
                string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
                string activityDetail = $@"Error: {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}

			conn.Close();
            var posturl = "";
            if (comid != null)
            {
                posturl = Request.Path + "?companyId=" + comid;
            }
            else
            {
                posturl = Request.Path;
            }
            Response.Redirect(posturl);
        }

        protected void sendType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void PaymentTab_Click(object sender, EventArgs e)
        {
            if (companyId != null)
            {
                Response.Redirect("companyPayment.aspx?companyId=" + companyId);
            }
        }
        protected void getData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;
            string sql = "SELECT * FROM PrivateDetail WHERE memberid = '" + represID.Value + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    represMem.Text = rd.GetValue(9).ToString();
                    represNm.Value = rd.GetValue(3).ToString();
                    represNmE.Value = rd.GetValue(4).ToString();
                }

            }
            catch { }
        }
        protected void Retrieve_Click(object sender, EventArgs e)
        {
            getData();
            addBTN.Visible = false;
            update.Visible = true;
            cancel.Visible = true;
            companyNmJ.Disabled = false;
            companyNmE.Disabled = false;
            companyNmEE.Disabled = false;
            busType.Disabled = false;
            appliedDate.Disabled = false;
            address.Disabled = false;
            phone.Disabled = false;
            fax.Disabled = false;
            email.Disabled = false;
            establishedDate.Disabled = false;
            sendType.Disabled = false;
            memberStatus.Disabled = false;
            represID.Disabled = false;
            //Retrieve.Enabled = true;
            represNm.Disabled = false;
            represNmE.Disabled = false;
            represEm.Disabled = false;
            represTp.Disabled = false;
            represPosition.Disabled = false;
            personinchargeNm.Disabled = false;
            personinchargeNmE.Disabled = false;
            personinchargeEm.Disabled = false;
            personinchargeTp.Disabled = false;
            personinchargePosition.Disabled = false;
            AccNm.Disabled = false;
            AccNmE.Disabled = false;
            AccEm.Disabled = false;
            AccTp.Disabled = false;
            AccPosition.Disabled = false;
            remark.Enabled = true;
            getInvoice.Enabled = true;
            withHolding.Enabled = true;
            payMethod.Disabled = false;
            payPeriod.Disabled = false;
            payDuration.Enabled = true;
            taxID.Disabled = false;
        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            BindData();
            addBTN.Visible = false;
            update.Visible = true;
            cancel.Visible = true;
            companyNmJ.Disabled = false;
            companyNmE.Disabled = false;
            companyNmEE.Disabled = false;
            busType.Disabled = false;
            appliedDate.Disabled = false;
            address.Disabled = false;
            phone.Disabled = false;
            fax.Disabled = false;
            email.Disabled = false;

            // 2024-08-21 at 04.00pm : Toon add
            this.txtPassword.Disabled = false;
            // -- End add --

            establishedDate.Disabled = false;
            sendType.Disabled = false;
            memberStatus.Disabled = false;
            represID.Disabled = false;
            //Retrieve.Enabled = true;
            represNm.Disabled = false;
            represNmE.Disabled = false;
            represEm.Disabled = false;
            represTp.Disabled = false;
            represPosition.Disabled = false;
            personinchargeNm.Disabled = false;
            personinchargeNmE.Disabled = false;
            personinchargeEm.Disabled = false;
            personinchargeTp.Disabled = false;
            personinchargePosition.Disabled = false;
            AccNm.Disabled = false;
            AccNmE.Disabled = false;
            AccEm.Disabled = false;
            AccTp.Disabled = false;
            AccPosition.Disabled = false;
            remark.Enabled = true;
            getInvoice.Enabled = true;
            withHolding.Enabled = true;
            payMethod.Disabled = false;
            payPeriod.Disabled = false;
            payDuration.Enabled = true;
            taxID.Disabled = false;
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
        //public void UpdateSqlTable(string Sqlcmd)
        //{

        //    connection();
        //    string sql = Sqlcmd;
        //    conn.Open();
        //    cmd = new SqlCommand(sql, conn);

        //    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        //    dataAdapter.UpdateCommand = cmd;
        //    dataAdapter.UpdateCommand.ExecuteNonQuery();
        //    conn.Close();
        //}

        protected void update_Click(object sender, EventArgs e)
        {
            //"hh:mm:ss tt"

            var _confirmEdit = Request.Form["confirm_value"];

            if (_confirmEdit == "Yes")
            {
                string comNameJ = companyNmJ.Value.ToString();
                string comNameE = companyNmE.Value.ToString();
                string comNameEE = companyNmEE.Value.ToString();
                string remarkInput = remark.Text.ToString();
                comNameJ = comNameJ.Replace("'", "''");
                comNameE = comNameE.Replace("'", "''");
                comNameEE = comNameEE.Replace("'", "''");
                remarkInput = remarkInput.Replace("'", "''");
                string ph = phone.Value.ToString();
                DateTime now = DateTime.Now;
                string updatedDate = now.ToString();
                connection();
                string ereptmp = "NULL";
                string eacctmp = "NULL";
                string epictmp = "NULL";
                string cancelDate = cancelDateTmp;

                LogActivity logActivity = new LogActivity();
                var uid = Session["UID"];
                int staffID = uid != null ? Convert.ToInt32(uid) : 0;


                if (memberstatus == "A" && memberStatus.Value == "NA")
                {
                    cancelDate = "CURRENT_TIMESTAMP";  // toDayDateTime.ToString("yyyy/MM/dd");
                }
                else if (memberstatus == "NA" && memberStatus.Value == "A")
                {   // In case member status change from 'NA' to 'A'
                    cancelDate = "Null";
                }
                else
                {   // In case member status change from 'A' to 'NA'
                    cancelDate = cancelDateTmp;

                    var _cancelDate_Reform = DateTime.Parse(cancelDate).ToString("dd/MM/yyyy");
                    cancelDate = Date_MsSqlStandard.CastQuery(_cancelDate_Reform);
                }

                if (represEm.Value.Trim() != "")
                {
                    ereptmp = "'" + represEm.Value + "'";
                }
                if (AccEm.Value.Trim() != "")
                {
                    eacctmp = "'" + AccEm.Value + "'";
                }
                if (personinchargeEm.Value.Trim() != "")
                {
                    epictmp = "'" + personinchargeEm.Value + "'";
                }

                // 2024-09-19 11.17am : Toon fixed bug.

                #region 'Bug cause in below code'
                // The conversion of a varchar data type to a datetime data type resulted in an out-of-range value.
                //cmd = new SqlCommand("SET dateformat dmy UPDATE CompanyMember SET companyNmJ = N'" + comNameJ + "'," +
                //                    "companyNmE = '" + comNameE + "',companyNmEE = '" + comNameEE + "',busType =  N'" + busType.Value + "'," +
                //                    "appliedDate = '" + appliedDate.Value + "',establishedDate = '" + establishedDate.Value + "', cancelDate= " + cancelDate + ", memberStatus= '" + memberStatus.Value + "',sendType ='" + sendType.Value + "',address ='" + address.Value + "'," +
                //                    "phone = '" + ph + "', fax = '" + fax.Value + "', email= '" + email.Value + "',represID ='" + represID.Value + "',represNm = N'" + represNm.Value + "'," +
                //                    "represNmE = '" + represNmE.Value + "', represPosition = '" + represPosition.Value + "', remark= '" + remarkInput + "',payMethod ='" + payMethod.Value + "',payPeriod ='" + payPeriod.Value + "'," +
                //                    "payDuration = '" + payDuration.SelectedValue + "', getInvoice = '" + getInvoice.Checked + "', withHolding= '" + withHolding.Checked + "',represEm =" + ereptmp + ",represTp ='" + represTp.Value + "'," +
                //                    "personinchargeNm = N'" + personinchargeNm.Value + "', contNm = '" + personinchargeNmE.Value + "', personinchargeEm= " + epictmp + ",personinchargeTp ='" + personinchargeTp.Value + "',contPosition ='" + personinchargePosition.Value + "'," +
                //                    "AccNm = N'" + AccNm.Value + "', AccNmE = '" + AccNmE.Value + "', AccEm= " + eacctmp + ",AccTp ='" + AccTp.Value + "',AccPosition ='" + AccPosition.Value + "',TaxID ='" + taxID.Value.ToString().Trim() + "', updatedDate =CURRENT_TIMESTAMP,updatedBy ='" + uid + "' " +
                //                    "WHERE companyId = '" + companyId + "'", conn);
                #endregion

                cmd = new SqlCommand($"SET dateformat dmy \n" +
                                     $"UPDATE CompanyMember \n" +
                                     $"SET companyNmJ = N'{comNameJ}', \n" +
                                     $"companyNmE = '{comNameE} ', \n" +
                                     $"companyNmEE = '{comNameEE}', \n" +
                                     $"busType =  N'{busType.Value}', \n" +
                                     $"appliedDate = {Date_MsSqlStandard.CastQuery(appliedDate.Value)}, \n" +
                                     $"establishedDate = {Date_MsSqlStandard.CastQuery(establishedDate.Value)}, \n" +
                                     //$"cancelDate = (SELECT CONVERT(DATETIME, {cancelDate}, 20)), \n" +
                                     $"cancelDate = {cancelDate}, \n" +
                                     $"memberStatus = '{memberStatus.Value}', \n" +
                                     $"sendType ='" + sendType.Value + "',address ='" + address.Value + "'," +
                                    $"phone = '" + ph + "', fax = '" + fax.Value + "', email= '" + email.Value + "',represID ='" + represID.Value + "',represNm = N'" + represNm.Value + "'," +
                                    $"represNmE = '" + represNmE.Value + "', represPosition = '" + represPosition.Value + "', remark= '" + remarkInput + "',payMethod ='" + payMethod.Value + "',payPeriod ='" + payPeriod.Value + "'," +
                                    $"payDuration = '" + payDuration.SelectedValue + "', getInvoice = '" + getInvoice.Checked + "', withHolding= '" + withHolding.Checked + "',represEm =" + ereptmp + ",represTp ='" + represTp.Value + "'," +
                                    $"personinchargeNm = N'" + personinchargeNm.Value + "', contNm = '" + personinchargeNmE.Value + "', personinchargeEm= " + epictmp + ",personinchargeTp ='" + personinchargeTp.Value + "',contPosition ='" + personinchargePosition.Value + "'," +
                                    $"AccNm = N'" + AccNm.Value + "', AccNmE = '" + AccNmE.Value + "', AccEm= " + eacctmp + ",AccTp ='" + AccTp.Value + "',AccPosition ='" + AccPosition.Value + "',TaxID ='" + taxID.Value.ToString().Trim() + "', updatedDate =CURRENT_TIMESTAMP,updatedBy ='" + uid + "' " +
                                    $"WHERE companyId = '" + companyId + "'", conn);

                conn.Open();
                try
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        string actDetail = $"Changed data in a table 'CompanyMember' where companyId is '{companyId}' successful (User id = '{staffID}')";
                        string script1 = "alert(\"DATA UPDATED Complete!!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script1, true);
                        logActivity.LogStaffActivity(staffID, actDetail);
                        //Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    }
                    else
                    {
                        string actDetail = $"Changed data in a table 'CompanyMember' where companyId is '{companyId}' unsuccessful (User id = '{staffID}')";
                        string script2 = "alert(\"DATA UPDATED NOT Complete!!!\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script2, true);
                        logActivity.LogStaffActivity(staffID, actDetail);
                        //Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    }
                }
                catch (SqlException ex)
                {
                    string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                catch (Exception ex)
                {
                    string activityDetail = $@"Error: {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                //var k = cmd.ExecuteNonQuery();

                conn.Close();

                // 2024-08-21 at 04.22pm : Toon add

                // 2024-09-20 at 12.33pm : Toon Fix
                var _userFname = ""; // represNmE.Value.Split(' ')[1];
                var _userLname = ""; // represNmE.Value.Split(' ')[0];

                try { _userFname = this.represNmE.Value.Split(' ')[1]; }
                catch { _userFname = this.represNmE.Value; }

                try { _userLname = this.represNmE.Value.Split(' ')[0]; }
                catch { _userLname = this.represNmE.Value; }
                // *** End Of Fixed ***

                var _password = PublicFunction.getHashed(this.txtPassword.Value);
                var _regYearMonth_Day = $"{DateTime.Now.Year}-{DateTime.Now.Month.ToString("0#")}" +
                                        $"-{DateTime.Now.Day.ToString("0#")}";
                var _regTime = $"{DateTime.Now.Hour.ToString("0#")}:" +
                               $"{DateTime.Now.Minute.ToString("0#")}:" +
                               $"{DateTime.Now.Second.ToString("0#")}";

                this.cmd.CommandText = $"INSERT INTO m_user(USER_EMAIL, USER_FIRSTNAME, USER_LASTNAME, " +
                                       $"                   USER_PHONE, PASSWORD, AUTHORITY, REG_YMD, " +
                                       $"                   REG_TIME, Check_status, memberId, COMPANY_NAME) " +
                                       $"VALUES('{email.Value}', '{_userFname}', '{_userLname}', '{phone.Value}', " +
                                       $"       '{_password}', 'company', '{_regYearMonth_Day}', '{_regTime}', 'O', " +
                                       $"       '{represID.Value}', '{comNameE}');";

                try
                {
                    var _message = "";
                    this.conn.Open();

                    try
                    {
                        this.cmd.ExecuteNonQuery();
                        _message = "";
                    }
                    catch (Exception err)
                    {
                        this.cmd.CommandText = $"UPDATE m_user " +
                                               $"SET " +
                                               $"    USER_FIRSTNAME = '{_userFname}', USER_LASTNAME = '{_userLname}', " +
                                               $"    USER_PHONE = '{phone.Value}', PASSWORD = '{_password}', " +
                                               $"    AUTHORITY = 'company', Check_status = 'O', " +
                                               $"    memberId = '{represID.Value}', COMPANY_NAME = '{comNameE}' " +
                                               $"WHERE USER_EMAIL = '{email.Value}'";
                        try
                        {
                            this.cmd.ExecuteNonQuery();
                        }
                        catch (Exception _err) { err = _err; }
                        _message = err.Message;
                    }

                    var _alert = "alert('" + _message + "')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", _alert, true);
                    this.conn.Close();
                }
                catch (Exception)
                {

                }
                // -- End add --

                var posturl = "";
                if (Request.QueryString != null)
                {
                    posturl = Request.Path + "?" + Request.QueryString;
                }
                else
                {
                    posturl = Request.Path;
                }
                Response.Redirect(posturl);
            }
        }

        [WebMethod]
        public static string Insert_Datatest(string id)
        {
            return "test";
            //var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            //SqlCommand sc;
            //SqlDataReader rd;
            //string val="";
            //string sql = "SELECT * FROM PrivateDetail WHERE memberid = '" + id + "'";
            //using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            //{
            //    con.Open();
            //    sc = new SqlCommand(sql, con);
            //    rd = sc.ExecuteReader();
            //    while (rd.Read())
            //    {
            //        //represMem.Text = rd.GetValue(9).ToString();
            //        //represNm.Value = rd.GetValue(3).ToString();
            //        //represNmE.Value = rd.GetValue(4).ToString();
            //        val = rd.GetValue(9).ToString();
            //        //return val;
            //    }
            //    return "test";
            //}
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            var posturl = "";
            if (Request.QueryString != null)
            {
                posturl = Request.Path + "?" + Request.QueryString;
            }
            else
            {
                posturl = Request.Path;
            }
            Response.Redirect(posturl);
        }
    }
}