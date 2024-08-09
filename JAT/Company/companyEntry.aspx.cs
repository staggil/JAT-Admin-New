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

namespace JAT.Company
{
    public partial class companyEntry : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private LogActivity logActivity = new LogActivity();
        string showMem;
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
            establishedDate.Disabled = false;
            sendType.Disabled = false;
            memberStatus.Disabled = false;
            represID.Disabled = false;
            Retrieve.Enabled = true;
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
            showMem = Request.QueryString["companyId"];
            if (showMem != null)
            {
                BindData2();
                var showMem2 = "(" + showMem + ")";
                id.Text = showMem2;
                addBTN.Visible = true;
                Button1.Visible = false;
                cancel.Visible = false;
                companyNmJ.Disabled = true;
                companyNmE.Disabled = true;
                companyNmEE.Disabled = true;
                busType.Disabled = true;
                appliedDate.Disabled = true;
                address.Disabled = true;
                phone.Disabled = true;
                fax.Disabled = true;
                email.Disabled = true;
                establishedDate.Disabled = true;
                sendType.Disabled = true;
                memberStatus.Disabled = true;
                represID.Disabled = true;
                Retrieve.Enabled = false;
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
            string sql = "SELECT * FROM CompanyMember WHERE companyId = '" + showMem + "'";
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
            SqlDataReader rd;
            string sql = "SELECT *,FORMAT(cancelDate, 'dd/MM/yyyy', 'en-us') FROM CompanyMember LEFT JOIN SStaff ON updatedBy=staffID LEFT JOIN PrivateDetail ON represID=memberid WHERE companyId = '" + showMem + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //show in member information
                    companyNmJ.Value = rd.GetValue(1).ToString();
                    companyNmE.Value = rd.GetValue(2).ToString();
                    companyNmEE.Value = rd.GetValue(3).ToString();
                    busType.Value = rd.GetValue(4).ToString();
                    //appliedDate.Value = ((DateTime)rd.GetValue(5)).ToString("d/M/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    appliedDate.Value = ((DateTime)rd.GetValue(5)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];

                    //appliedDate.Value = rd.GetValue(5).ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //appliedDate.Value = rd.GetValue(5).ToString();
                    //statusdate.Text = ((DateTime)rd.GetValue(8)).ToString("yyyy/MM/dd").ToString();
                    statusdate.Text = rd.GetValue(71).ToString();
                    cancelDateTmp = rd.GetValue(71).ToString();
                    address.Value = rd.GetValue(12).ToString();
                    phone.Value = rd.GetValue(13).ToString();
                    fax.Value = rd.GetValue(14).ToString();
                    email.Value = rd.GetValue(15).ToString();
                    establishedDate.Value = ((DateTime)rd.GetValue(6)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //establishedDate.Value = rd.GetValue(6).ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //establishedDate.Value = rd.GetValue(6).ToString();
                    sendType.Value = rd.GetValue(10).ToString();
                    memberStatus.Value = rd.GetValue(9).ToString();
                    memberstatus = rd.GetValue(9).ToString();//variable to check for update
                    represMem.Text = rd.GetValue(63).ToString();
                    represID.Value = rd.GetValue(16).ToString();
                    //represMem.Text = rd.GetValue(9).ToString();
                    represNm.Value = rd.GetValue(17).ToString();
                    represNmE.Value = rd.GetValue(18).ToString();
                    represEm.Value = rd.GetValue(32).ToString();
                    represTp.Value = rd.GetValue(33).ToString();
                    represPosition.Value = rd.GetValue(19).ToString();
                    personinchargeNm.Value = rd.GetValue(34).ToString();
                    personinchargeNmE.Value = rd.GetValue(20).ToString();
                    personinchargeEm.Value = rd.GetValue(36).ToString();
                    personinchargeTp.Value = rd.GetValue(37).ToString();
                    personinchargePosition.Value = rd.GetValue(21).ToString();
                    AccNm.Value = rd.GetValue(39).ToString();
                    AccNmE.Value = rd.GetValue(40).ToString();
                    AccEm.Value = rd.GetValue(41).ToString();
                    AccTp.Value = rd.GetValue(42).ToString();
                    AccPosition.Value = rd.GetValue(43).ToString();
                    remark.Text = rd.GetValue(22).ToString();
                    bool ch1 = (bool)rd.GetValue(26);
                    getInvoice.Checked = ch1;
                    bool ch2 = (bool)rd.GetValue(27);
                    withHolding.Checked = ch2;
                    payMethod.Value = rd.GetValue(23).ToString();
                    payPeriod.Value = rd.GetValue(24).ToString();
                    payDuration.SelectedValue = rd.GetValue(25).ToString();
                    updateBy.Text = rd.GetValue(46).ToString();
                    taxID.Value = rd.GetValue(44).ToString();
                }

            }
            catch { }
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
                cancelDate = toDayDateTime.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
            }
            var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;


			cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyMember VALUES('" + comid + "' , N'" + comNmJ + "', '" + comNmE + "', '" + comNmEE + "', N'" + busType.Value + "', '" + appliedDate.Value + "', '" + establishedDate.Value + "', '" + " " + "', '" + cancelDate + "', '"
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
				string activityDetail = $"Added new data into a table 'CompanyMember' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Added new data into a table 'CompanyMember' unsuccessful [{ex.Message}] (User id = '{staffID}')";
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
            if (showMem != null)
            {
                Response.Redirect("companyPayment.aspx?companyId=" + showMem);
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
            //update.Visible = true;
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
            Retrieve.Enabled = true;
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
            establishedDate.Disabled = false;
            sendType.Disabled = false;
            memberStatus.Disabled = false;
            represID.Disabled = false;
            Retrieve.Enabled = true;
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
                cancelDate = "'" + toDayDateTime.ToString("dd/MM/yyyy", new CultureInfo("en-US")) + "'";
            }
            else if (memberstatus == "NA" && memberStatus.Value == "A")
            {
                cancelDate = "Null";
            }
            else
            {
                cancelDate = "'" + cancelDateTmp + "'";
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
            //cmd = new SqlCommand("UPDATE CompanyMember SET appliedDate = '" + appliedDate.Value + "'  WHERE companyId = '" + showMem + "'", conn);
            cmd = new SqlCommand("SET dateformat dmy UPDATE CompanyMember SET companyNmJ = N'" + comNameJ + "'," +
                                "companyNmE = '" + comNameE + "',companyNmEE = '" + comNameEE + "',busType =  N'" + busType.Value + "'," +
                                "appliedDate = '" + appliedDate.Value + "',establishedDate = '" + establishedDate.Value + "', cancelDate= " + cancelDate + ", memberStatus= '" + memberStatus.Value + "',sendType ='" + sendType.Value + "',address ='" + address.Value + "'," +
                                "phone = '" + ph + "', fax = '" + fax.Value + "', email= '" + email.Value + "',represID ='" + represID.Value + "',represNm = N'" + represNm.Value + "'," +
                                "represNmE = '" + represNmE.Value + "', represPosition = '" + represPosition.Value + "', remark= '" + remarkInput + "',payMethod ='" + payMethod.Value + "',payPeriod ='" + payPeriod.Value + "'," +
                                "payDuration = '" + payDuration.SelectedValue + "', getInvoice = '" + getInvoice.Checked + "', withHolding= '" + withHolding.Checked + "',represEm =" + ereptmp + ",represTp ='" + represTp.Value + "'," +
                                "personinchargeNm = N'" + personinchargeNm.Value + "', contNm = '" + personinchargeNmE.Value + "', personinchargeEm= " + epictmp + ",personinchargeTp ='" + personinchargeTp.Value + "',contPosition ='" + personinchargePosition.Value + "'," +
                                "AccNm = N'" + AccNm.Value + "', AccNmE = '" + AccNmE.Value + "', AccEm= " + eacctmp + ",AccTp ='" + AccTp.Value + "',AccPosition ='" + AccPosition.Value + "',TaxID ='" + taxID.Value.ToString().Trim() + "', updatedDate =CURRENT_TIMESTAMP,updatedBy ='" + uid + "' " +
                                "WHERE companyId = '" + showMem + "'", conn);
            conn.Open();
            try
            {
				if (cmd.ExecuteNonQuery() > 0)
				{
					string actDetail = $"Changed data in a table 'CompanyMember' where companyId is '{showMem}' successful (User id = '{staffID}')";
					string script1 = "alert(\"DATA UPDATED Complete!!\");";
					ScriptManager.RegisterStartupScript(this, GetType(),
										  "ServerControlScript", script1, true);
					logActivity.LogStaffActivity(staffID, actDetail);
					Page.Response.Redirect(Page.Request.Url.ToString(), false);

				}
				else
				{
					string actDetail = $"Changed data in a table 'CompanyMember' where companyId is '{showMem}' unsuccessful (User id = '{staffID}')";
					string script2 = "alert(\"DATA UPDATED NOT Complete!!!\");";
					ScriptManager.RegisterStartupScript(this, GetType(),
										  "ServerControlScript", script2, true);
					logActivity.LogStaffActivity(staffID, actDetail);
					Page.Response.Redirect(Page.Request.Url.ToString(), false);
				}
			}
            catch (SqlException ex)
            {
				string actDetail = $"Changed data in a table 'CompanyMember' where companyId is '{showMem}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, actDetail);
			}
			catch (Exception ex)
			{
				string actDetail = $"Changed data in a table 'CompanyMember' where companyId is '{showMem}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, actDetail);
			}
			//var k = cmd.ExecuteNonQuery();

			conn.Close();
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