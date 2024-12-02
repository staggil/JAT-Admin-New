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

namespace JAT.Company
{
    public partial class companyPayment : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        string showMem;
		private LogActivity logActivity = new LogActivity();
        //private uid = Session['UID'];
		

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
            showMem = Request.QueryString["companyId"];
            save.Visible = false;
            update.Visible = false;
            cancel.Visible = false;

            cclt.Disabled = true;

            totalPerMonth.Enabled = false;
            checkShort.Disabled = true;
            bNo.Disabled = true;
            cNo.Disabled = true;
            noPayMonth.Disabled = true;
            paymentDate.Disabled = true;
            effectiveDate.Disabled = true;
            expiredDate.Disabled = true;
            Remark1.Enabled = false;
            memberFee.Disabled = true;
            pay70.Disabled = true;
            pay100.Disabled = true;
            newsFee.Disabled = true;
            totalFee.Disabled = true;
            payRemark.Enabled = false;
            accNumber.Disabled = true;
            bankCode.Disabled = true;
            cboBankAcc.Enabled = false;


            if (showMem != null)
            {
                //if (payMethod.Text == "S" || payMethod.Text == "T")
                //{
                //    accNumber.Disabled = true;
                //    bankCode.Disabled = true;
                //    cboBankAcc.Enabled = false;

                //}
                //else
                //{
                //    accNumber.Disabled = false;
                //    bankCode.Disabled = false;
                //    cboBankAcc.Enabled = true;

                //}
                var showMem2 = "(" + showMem + ")";
                id.Text = showMem2;
                getData();
                if (!Page.IsPostBack)
                {
                    getDropDownTotal();
                    getDropDownBankAcc();
                    connection();
                    SqlCommand sc;
                    SqlDataReader rd;
                    //string sql = "select top 1 cp.tranId,cp.accId,cp.companyId from CompanyPayment cp,CompanyAccount ca where cp.companyId = ca.companyId AND cp.companyId = '" + companyId + "' order by cp.tranId DESC ,cp.accId DESC;";
                    //string sql = "select top 1 tranId,accId,companyId from CompanyPayment where companyId = '" + companyId + "' AND Deleted_at is null order by tranId DESC";
                    string sql = "select top 1 tranId,accId,companyId from CompanyPayment where companyId = '" + showMem + "' order by tranId DESC";
                    try
                    {
                        conn.Open();
                        sc = new SqlCommand(sql, conn);
                        rd = sc.ExecuteReader();
                        while (rd.Read())
                        {
                            Label1.Text = rd.GetValue(0).ToString();
                            Label2.Text = rd.GetValue(1).ToString();
                        }
                        conn.Close();
                    }
                    catch { }
                    if (Label1.Text != "" && Label1.Text != null)
                    {
                        BindCompanyPayment();
                        getData2();
                    }
                    else
                    {

                        if (payMethod.Text == "S" || payMethod.Text == "T")
                        {
                            accNumber.Disabled = false;
                            bankCode.Disabled = false;
                            cboBankAcc.Enabled = true;

                        }
                        else
                        {
                            accNumber.Disabled = true;
                            bankCode.Disabled = true;
                            cboBankAcc.Enabled = false;

                        }
                        add.Visible = false;
                        edit.Visible = false;

                        cclt.Disabled = false;

                        save.Visible = true;
                        //update.Visible = true;
                        cancel.Visible = true;

                        totalPerMonth.Enabled = true;
                        checkShort.Disabled = false;
                        bNo.Disabled = false;
                        cNo.Disabled = false;
                        noPayMonth.Disabled = false;
                        paymentDate.Disabled = false;
                        effectiveDate.Disabled = false;
                        expiredDate.Disabled = false;
                        Remark1.Enabled = true;
                        memberFee.Disabled = false;
                        pay70.Disabled = true;
                        pay100.Disabled = true;
                        newsFee.Disabled = false;
                        totalFee.Disabled = false;
                        payRemark.Enabled = true;
                    }

                }


            }


        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        protected void getDropDownTotal()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from SCompanyFee where EffectiveID = (select TOP(1) EffectiveID from tblEffective where EffectiveDate <= cast(floor(cast(getdate() as float))as datetime) and ExpireDate >= cast(floor(cast(getdate() as float))as datetime)) " +
                                                                "order by CFeeID", con);
                    adapter.Fill(subjects);
                    totalPerMonth.DataSource = subjects;
                    totalPerMonth.DataValueField = "CTotalPerMonth";
                    totalPerMonth.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            totalPerMonth.Items.Insert(0, new ListItem("--Any--", "0"));
        }
        protected void getDropDownBankAcc()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select accId, (accNumber + ':' + bankCode) as bankDetail " +
                                                                "from companyAccount where companyId ='" + showMem + "'", con);

                    adapter.Fill(subjects);
                    cboBankAcc.DataSource = subjects;
                    string[] tmpMap = { "accId", "bankDetail" };
                    cboBankAcc.DataValueField = tmpMap[0];
                    cboBankAcc.DataTextField = tmpMap[0];

                    cboBankAcc.DataValueField = tmpMap[1];
                    cboBankAcc.DataTextField = tmpMap[1];
                    cboBankAcc.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            cboBankAcc.Items.Insert(0, new ListItem("--Any--", "0"));
        }

        protected void getData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;
            string sql = "SELECT * FROM CompanyMember WHERE companyId = '" + showMem + "' ";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //************************Payment Management*********************************
                    PcompanyNmJ.Text = rd.GetValue(1).ToString();
                    PcompanyNmE.Text = rd.GetValue(2).ToString();
                    PpayPeriod.Text = rd.GetValue(24).ToString();
                    PpayDuration.Text = rd.GetValue(25).ToString();
                    PsendType.Text = rd.GetValue(10).ToString();
                    payMethod.Text = rd.GetValue(23).ToString();
                }
            }
            catch { }
        }
        protected void getData3(object tran, object accid)
        {
            connection();
            Label1.Text = (string)tran;
            Label2.Text = (string)accid;
            SqlCommand sc;
            SqlDataReader rd;
            //string sql = "SELECT cp.totalPerMonth,cp.bNo,cp.cNo,cp.noPayMonth,cp.paymentDate,cp.effectiveDate,cp.expiredDate,cp.memberFee,cp.newsFee,cp.totalFee,cp.payRemark,ca.bankCode,ca.accNumber FROM CompanyPayment cp,CompanyAccount ca WHERE cp.companyId = ca.companyId AND cp.companyId = '" + companyId + "' AND cp.tranId = '" + tran + "'AND ca.accid = '" + accid + "'";
            //string sql = "SELECT * FROM CompanyPayment cp LEFT JOIN CompanyAccount ca ON cp.companyId=ca.companyId AND cp.accId=ca.accId WHERE cp.companyId='" + companyId + "' AND cp.tranId='" + Label1.Text + "'  AND cp.Deleted_at IS NULL ORDER BY tranid DESC";
            string sql = "SELECT * FROM CompanyPayment cp LEFT JOIN CompanyAccount ca ON cp.companyId=ca.companyId AND cp.accId=ca.accId WHERE cp.companyId='" + showMem + "' AND cp.tranId='" + Label1.Text + "' ORDER BY tranid DESC";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //************************Payment Management*********************************
                    //totalPerMonth.SelectedValue = rd.GetValue(0).ToString();
                    //bNo.Value = rd.GetValue(1).ToString();
                    //cNo.Value = rd.GetValue(2).ToString();
                    //noPayMonth.Value = rd.GetValue(3).ToString();
                    //paymentDate.Value = ((DateTime)rd.GetValue(4)).ToString("yyyy/MM/dd").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //effectiveDate.Value = ((DateTime)rd.GetValue(5)).ToString("yyyy/MM/dd").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //expiredDate.Value = ((DateTime)rd.GetValue(6)).ToString("yyyy/MM/dd").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //memberFee.Value = rd.GetValue(7).ToString();
                    //newsFee.Value = rd.GetValue(8).ToString();
                    //totalFee.Value = rd.GetValue(9).ToString();
                    //payRemark.Text = rd.GetValue(10).ToString();
                    //bankCode.Value = rd.GetValue(11).ToString();
                    //accNumber.Value = rd.GetValue(12).ToString();
                    //***new bind***
                    totalPerMonth.SelectedValue = rd.GetValue(13).ToString();
                    bNo.Value = rd.GetValue(2).ToString();
                    cNo.Value = rd.GetValue(3).ToString();
                    noPayMonth.Value = rd.GetValue(6).ToString();
                    paymentDate.Value = ((DateTime)rd.GetValue(4)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    effectiveDate.Value = ((DateTime)rd.GetValue(5)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    expiredDate.Value = ((DateTime)rd.GetValue(7)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    memberFee.Value = rd.GetValue(11).ToString();
                    newsFee.Value = rd.GetValue(12).ToString();
                    totalFee.Value = rd.GetValue(14).ToString();
                    payRemark.Text = rd.GetValue(9).ToString();
                    bankCode.Value = rd.GetValue(23).ToString();
                    accNumber.Value = rd.GetValue(22).ToString();
                    checkShort.Checked = bool.Parse(rd.GetValue(10).ToString());
                }
            }
            catch { }
        }
        protected void getData2()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

			
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            //string sql = "SELECT cp.totalPerMonth,cp.bNo,cp.cNo,cp.noPayMonth,cp.paymentDate,cp.effectiveDate,cp.expiredDate,cp.memberFee,cp.newsFee,cp.totalFee,cp.payRemark,ca.bankCode,ca.accNumber,st.staffName FROM CompanyPayment cp,CompanyAccount ca,SStaff st WHERE cp.companyId = ca.companyId AND cp.accId=st.staffID AND cp.companyId = '" + companyId + "' AND cp.tranId = '" + Label1.Text + "' AND ca.accId = '" + Label2.Text + "'";
            //string sql = "SELECT * FROM CompanyPayment cp LEFT JOIN CompanyAccount ca ON cp.companyId=ca.companyId AND cp.accId=ca.accId LEFT JOIN CompanyMember cm ON cp.companyId=cm.companyId LEFT JOIN SStaff ON updatedBy=staffID WHERE cp.companyId='" + companyId + "' AND cp.accId='" + Label2.Text + "' AND cp.tranId='" + Label1.Text + "'  AND cp.Deleted_at IS NULL ORDER BY tranid DESC";
            string sql = "SELECT * FROM CompanyPayment cp LEFT JOIN CompanyAccount ca ON cp.companyId=ca.companyId AND cp.accId=ca.accId LEFT JOIN CompanyMember cm ON cp.companyId=cm.companyId LEFT JOIN SStaff ON updatedBy=staffID WHERE cp.companyId='" + showMem + "' AND cp.accId='" + Label2.Text + "' AND cp.tranId='" + Label1.Text + "' ORDER BY tranid DESC";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //************************Payment Management*********************************
                    //totalPerMonth.SelectedValue = rd.GetValue(0).ToString();
                    //bNo.Value = rd.GetValue(1).ToString();
                    //cNo.Value = rd.GetValue(2).ToString();
                    //noPayMonth.Value = rd.GetValue(3).ToString();
                    //paymentDate.Value = ((DateTime)rd.GetValue(4)).ToString("yyyy/MM/dd").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //effectiveDate.Value = ((DateTime)rd.GetValue(5)).ToString("yyyy/MM/dd").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //expiredDate.Value = ((DateTime)rd.GetValue(6)).ToString("yyyy/MM/dd").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //memberFee.Value = rd.GetValue(7).ToString();
                    //newsFee.Value = rd.GetValue(8).ToString();
                    //totalFee.Value = rd.GetValue(9).ToString();
                    //payRemark.Text = rd.GetValue(10).ToString();
                    //bankCode.Value = rd.GetValue(11).ToString();
                    //accNumber.Value = rd.GetValue(12).ToString();
                    //updateBy.Text = rd.GetValue(13).ToString();
                    //***new bind***
                    totalPerMonth.SelectedValue = rd.GetValue(13).ToString();
                    bNo.Value = rd.GetValue(2).ToString();
                    cNo.Value = rd.GetValue(3).ToString();
                    noPayMonth.Value = rd.GetValue(6).ToString();
                    paymentDate.Value = ((DateTime)rd.GetValue(4)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    effectiveDate.Value = ((DateTime)rd.GetValue(5)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    expiredDate.Value = ((DateTime)rd.GetValue(7)).ToString("dd/MM/yyyy").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    memberFee.Value = rd.GetValue(11).ToString();
                    newsFee.Value = rd.GetValue(12).ToString();
                    totalFee.Value = rd.GetValue(14).ToString();
                    payRemark.Text = rd.GetValue(9).ToString();
                    Remark1.Text = rd.GetValue(19).ToString();
                    bankCode.Value = rd.GetValue(23).ToString();
                    accNumber.Value = rd.GetValue(22).ToString();
                    updateBy.Text = rd.GetValue(70).ToString();
                    checkShort.Checked = bool.Parse(rd.GetValue(10).ToString());
                }
            }
            catch { }
            finally
            {
                if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                {
                    updateBy.Text = "N/A";
                }
            }
        }

        protected void MemberTab_Click(object sender, EventArgs e)
        {
            if (showMem != null)
            {
                Response.Redirect("companyEntry.aspx?companyId=" + showMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void insertData()
        {
        	
            connection();
            SqlCommand sc;
            SqlDataReader rd;
            //string sql = "select top 1 tranId,companyId from CompanyPayment where companyId = '" + companyId + "' AND Deleted_at IS NULL order by tranId desc;";
            string sql = "select top 1 tranId,companyId from CompanyPayment where companyId = '" + showMem + "' order by tranId desc;";
            var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            string activityDetail;

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
            var temp = "";
            SqlCommand sc2;
            SqlDataReader rd2;
            string sql2 = "select top 1 accId,companyId from CompanyAccount where companyId = '" + showMem + "' order by accId desc;";
            try
            {
                conn.Open();
                sc2 = new SqlCommand(sql2, conn);
                rd2 = sc2.ExecuteReader();
                while (rd2.Read())
                {
                    temp = rd2.GetValue(0).ToString();
                }
                conn.Close();
            }
            catch { }

            var accNumberTemp = "";
            var bankCodeTemp = "";
            var accidTemp = "";
            SqlCommand sc3;
            SqlDataReader rd3;
            //string sql3 = "select top 1 accId,companyId from CompanyAccount where companyId = '" + companyId + "' order by accId desc;";
            string sql3 = "select top 1 accId,accNumber,bankCode from CompanyAccount where companyId = '" + showMem + "' AND accNumber = '" + accNumber.Value + "' AND bankCode = '" + bankCode.Value + "'";
            try
            {
                conn.Open();
                sc3 = new SqlCommand(sql3, conn);
                rd3 = sc3.ExecuteReader();
                while (rd3.Read())
                {
                    accidTemp = rd3.GetValue(0).ToString();
                    accNumberTemp = rd3.GetValue(1).ToString();
                    bankCodeTemp = rd3.GetValue(2).ToString();
                }
                conn.Close();
            }
            catch { }

            if (lbl.Text == "" || lbl.Text == null)
            {
                if (payMethod.Text == "S" || payMethod.Text == "T")
                {
                    if (temp == "" || temp == null)
                    {
                        connection();
                        int tranId = 1;
                        int accId = 1;
                        cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "','" + accId + "','" + payMethod.Text + "',' ',' ','"+Remark1.Text+"',null)", conn);
                        conn.Open();
                        try
                        {   
							cmd.ExecuteNonQuery();
                            activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
                            logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
                            activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
						}
						catch (Exception ex)
						{
                            activityDetail = $@"Error: {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }
                        
                        conn.Close();

                        string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                        SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                        conn.Open();
                        vlozSQL.Parameters.AddWithValue("@temp", accId);
                        vlozSQL.Parameters.AddWithValue("@companyId", showMem);
                        vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                        vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
                        try
                        {
							vlozSQL.ExecuteNonQuery();
							activityDetail = $"Added new data into a table 'CompanyAccount' successful (user id = '{staffID}')";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                        }
                        catch (SqlException ex)
						{
                            activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }
						catch (Exception ex)
						{
                            activityDetail = $@"Error: {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }


						vlozSQL.Parameters.Clear();
                        conn.Close();

                        Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        Context.ApplicationInstance.CompleteRequest();



                    }
                    else if ((temp != "" || temp != null))
                    {
                        if (accNumber.Value == accNumberTemp && bankCode.Value == bankCodeTemp)
                        {
                            int tranId = 1;
                            cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "','" + accidTemp + "','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                            conn.Open();
                            try
                            {
								cmd.ExecuteNonQuery();
								activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
								logActivity.LogStaffActivity(staffID, activityDetail);
							}
                            catch (SqlException ex)
                            {
                                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							catch (Exception ex)
							{
                                activityDetail = $@"Error: {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }

							conn.Close();

                            //string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                            //SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                            //conn.Open();
                            //vlozSQL.Parameters.AddWithValue("@temp", accidTemp);
                            //vlozSQL.Parameters.AddWithValue("@companyId", companyId);
                            //vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                            //vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
                            //vlozSQL.ExecuteNonQuery();
                            //vlozSQL.Parameters.Clear();
                            //conn.Close();
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            connection();
                            long val = Int64.Parse(temp);
                            long accId = ++val;
                            int tranId = 1;
                            cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "','" + accId + "','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                            conn.Open();
							try
							{
								cmd.ExecuteNonQuery();
								activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
								logActivity.LogStaffActivity(staffID, activityDetail);
							}
							catch (SqlException ex)
							{
                                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							catch (Exception ex)
							{
                                activityDetail = $@"Error: {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							conn.Close();

                            string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                            SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                            conn.Open();
                            vlozSQL.Parameters.AddWithValue("@temp", accId);
                            vlozSQL.Parameters.AddWithValue("@companyId", showMem);
                            vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                            vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
							try
							{
								vlozSQL.ExecuteNonQuery();
								activityDetail = $"Added new data into a table 'CompanyAccount' successful (user id = '{staffID}')";
								logActivity.LogStaffActivity(staffID, activityDetail);
							}
							catch (SqlException ex)
							{
                                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							catch (Exception ex)
							{
                                activityDetail = $@"Error: {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							vlozSQL.Parameters.Clear();
                            conn.Close();
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    connection();
                    int tranId = 1;
                    cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "',' ','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                    conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
                        activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					catch (Exception ex)
					{
                        activityDetail = $@"Error: {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					conn.Close();
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else if (lbl.Text != "" || lbl.Text != null)
            {
                if (payMethod.Text == "S" || payMethod.Text == "T")
                {
                    if (temp == "" || temp == null)
                    {
                        connection();
                        long val = Int64.Parse(lbl.Text);
                        long tranId = ++val;
                        int accId = 1;
                        cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "','" + accId + "','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
                            activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }
						catch (Exception ex)
						{
                            activityDetail = $@"Error: {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }
						conn.Close();

                        string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                        SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                        conn.Open();
                        vlozSQL.Parameters.AddWithValue("@temp", accId);
                        vlozSQL.Parameters.AddWithValue("@companyId", showMem);
                        vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                        vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
						try
						{
							vlozSQL.ExecuteNonQuery();
							activityDetail = $"Added new data into a table 'CompanyAccount' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
                            activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }
						catch (Exception ex)
						{
                            activityDetail = $@"Error: {ex.Message} in {this}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                            logActivity.IssueReport(ex);
                        }
						vlozSQL.Parameters.Clear();
                        conn.Close();

                        Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else if (temp != "" || temp != null)
                    {
                        if (accNumber.Value == accNumberTemp && bankCode.Value == bankCodeTemp)
                        {
                            //int tranId = 1;
                            long val2 = Int64.Parse(lbl.Text);
                            long tranId = ++val2;
                            cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "','" + accidTemp + "','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                            conn.Open();
							try
							{
								cmd.ExecuteNonQuery();
								activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = {staffID})";
								logActivity.LogStaffActivity(staffID, activityDetail);
							}
							catch (SqlException ex)
							{
                                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							catch (Exception ex)
							{
                                activityDetail = $@"Error: {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							conn.Close();

                            //string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                            //SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                            //conn.Open();
                            //vlozSQL.Parameters.AddWithValue("@temp", accidTemp);
                            //vlozSQL.Parameters.AddWithValue("@companyId", companyId);
                            //vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                            //vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
                            //vlozSQL.ExecuteNonQuery();
                            //vlozSQL.Parameters.Clear();
                            //conn.Close();
                            Page.Response.Redirect(Page.Request.Url.ToString(), true);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            connection();
                            long val = Int64.Parse(temp);
                            long accId = ++val;
                            long val2 = Int64.Parse(lbl.Text);
                            long tranId = ++val2;
                            cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "','" + accId + "','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                            conn.Open();
							try
							{
								cmd.ExecuteNonQuery();
								activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
								logActivity.LogStaffActivity(staffID, activityDetail);
							}
							catch (SqlException ex)
							{
                                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							catch (Exception ex)
							{
                                activityDetail = $@"Error: {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							conn.Close();

                            string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                            SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                            conn.Open();
                            vlozSQL.Parameters.AddWithValue("@temp", accId);
                            vlozSQL.Parameters.AddWithValue("@companyId", showMem);
                            vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                            vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
							try
							{
								vlozSQL.ExecuteNonQuery();
								activityDetail = $"Added new data into a table 'CompanyAccount' successful (user id = '{staffID}')";
								logActivity.LogStaffActivity(staffID, activityDetail);
							}
							catch (SqlException ex)
							{
                                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							catch (Exception ex)
							{
                                activityDetail = $@"Error: {ex.Message} in {this}";
                                logActivity.LogStaffActivity(staffID, activityDetail);
                                logActivity.IssueReport(ex);
                            }
							vlozSQL.Parameters.Clear();
                            conn.Close();
                        }


                        Page.Response.Redirect(Page.Request.Url.ToString(), true);
                        Context.ApplicationInstance.CompleteRequest();
                    }

                }
                else
                {
                    connection();
                    long val = Int64.Parse(lbl.Text);
                    long tranId = ++val;
                    cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.CompanyPayment VALUES('" + tranId + "','" + showMem + "','" + bNo.Value + "','" + cNo.Value + "','" + paymentDate.Value + "','" + effectiveDate.Value + "','" + noPayMonth.Value + "','" + expiredDate.Value + "',' ','" + payRemark.Text + "','" + checkShort.Checked + "','" + memberFee.Value + "','" + newsFee.Value + "','" + totalPerMonth.SelectedValue + "','" + totalFee.Value + "',' ','" + payMethod.Text + "',' ',' ','" + Remark1.Text + "',null)", conn);
                    conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						activityDetail = $"Added new data into a table 'CompanyPayment' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
                        activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					catch (Exception ex)
					{
                        activityDetail = $@"Error: {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					conn.Close();
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            insertData();
            lasteditor();
        }
        private void BindCompanyPayment()
        {
            DataGrid1.DataSource = GetStaffData().Tables["CompanyPayment"].DefaultView;
            DataGrid1.DataBind();

            string sessionRole = Session["Role"].ToString().Trim();
            if(sessionRole != "Administrator")
            {
                this.DataGrid1.Columns[11].Visible = false;
            }
        }
        private DataSet GetStaffData()
        {
            connection();
            string nameComJ = PcompanyNmJ.Text.Replace("'","''");            
            String SQLStatement = "SELECT tranId, N'" + nameComJ + "' as PcompanyNmJ,totalFee,paymentDate,effectiveDate,expiredDate,payMethod,bNo,cNo,payRemark,accId " +
            "FROM  CompanyPayment WHERE companyId = '" + showMem + "' and Deleted_at IS NULL order by tranId DESC ";
            //"FROM  CompanyPayment WHERE companyId = '" + companyId + "' order by tranId DESC ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "CompanyPayment");
            return myDataSet;
        }

        protected void add_Click(object sender, EventArgs e)
        {
            bNo.Value = "";
            cNo.Value = "";

            if (payMethod.Text == "T" || payMethod.Text == "S")
            {
                accNumber.Disabled = false;
                bankCode.Disabled = false;
                cboBankAcc.Enabled = true;
            }

            this.DataGrid1.Columns[10].Visible = false;

            add.Visible = false;
            edit.Visible = false;

            save.Visible = true;
            cancel.Visible = true;

            cclt.Disabled = false;
            totalPerMonth.Enabled = true;
            checkShort.Disabled = false;
            bNo.Disabled = false;
            cNo.Disabled = false;
            noPayMonth.Disabled = false;
            paymentDate.Disabled = false;
            effectiveDate.Disabled = false;
            expiredDate.Disabled = false;
            Remark1.Enabled = true;
            memberFee.Disabled = false;
            pay70.Disabled = false;
            pay100.Disabled = false;
            newsFee.Disabled = false;
            totalFee.Disabled = false;
            payRemark.Enabled = true;

        }

        protected void edit_Click(object sender, EventArgs e)
        {
            if (payMethod.Text == "T" || payMethod.Text == "S")
            {
                accNumber.Disabled = false;
                bankCode.Disabled = false;
                cboBankAcc.Enabled = true;
            }

            this.DataGrid1.Columns[10].Visible = false;

            add.Visible = false;
            edit.Visible = false;

            cclt.Disabled = false;

            update.Visible = true;
            cancel.Visible = true;

            totalPerMonth.Enabled = true;
            checkShort.Disabled = false;
            bNo.Disabled = false;
            cNo.Disabled = false;
            noPayMonth.Disabled = false;
            paymentDate.Disabled = false;
            effectiveDate.Disabled = false;
            expiredDate.Disabled = false;
            Remark1.Enabled = true;
            memberFee.Disabled = false;
            pay70.Disabled = false;
            pay100.Disabled = false;
            newsFee.Disabled = false;
            totalFee.Disabled = false;
            payRemark.Enabled = true;
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string activityDetail;

			string confirmValue = Request.Form["confirm_value"];
			if (e.CommandName == "delete")
			{
				if (confirmValue == "Yes")
				{
					string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
					string tranId = commandArgs[0];
					string accId = commandArgs[1];
					connection();
					//cmd = new SqlCommand("DELETE FROM CompanyPayment  WHERE tranId = '" + tranId + "'", conn);
					cmd = new SqlCommand($"Update CompanyPayment set Deleted_at = GETDATE() where tranId = '{tranId}' and companyid = '{showMem}'", conn);
					conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						activityDetail = $"Changed data in a table 'CompanyPayment' where tranId is '{tranId}' and companyid is '{showMem}' successful (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);

					}
					catch (SqlException ex)
					{
                        activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        //activityDetail = $"Changed data in a table 'CompanyPayment' where tranId is '{tranId}' and companyid is '{showMem}' unsuccessful [{sqlex.Message}] (User id = '{staffID}')";
                        logActivity.LogStaffActivity(staffID, $"Sql Error: {ex.ErrorCode} {ex.Message} in {this}");
                        logActivity.IssueReport(ex);

                    }
					catch (Exception ex)
					{
                        activityDetail = $@"Error: {ex.Message} in {this}";
                        //activityDetail = $"Changed data in a table 'CompanyPayment' where tranId is '{tranId}' and companyid is '{showMem}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                        logActivity.LogStaffActivity(staffID, $"Error : {ex.Message} in {this}");
                        logActivity.IssueReport(ex);
                    }

					conn.Close();

					//cmd = new SqlCommand("DELETE FROM CompanyAccount  WHERE accId = '" + accId + "'", conn);
					//conn.Open();
					//cmd.ExecuteNonQuery();
					//conn.Close();
					Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
				else { }
				
			}
			else if (e.CommandName == "edit")
			{
				string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
				string tranId = commandArgs[0];
				string accId = commandArgs[1];
				connection();
				SqlCommand sc;
				SqlDataReader rd;
                //string sql = "select cp.tranId,ca.accId,cp.companyId from CompanyPayment cp,CompanyAccount ca where cp.companyId = ca.companyId AND cp.companyId = '" + companyId + "' AND cp.tranId = '" + tranId + "' AND ca.accId = '" + accId + "' AND cp.Deleted_at IS NULL";
                string sql = "select cp.tranId,ca.accId,cp.companyId from CompanyPayment cp,CompanyAccount ca where cp.companyId = ca.companyId AND cp.companyId = '" + showMem + "' AND cp.tranId = '" + tranId + "' AND ca.accId = '" + accId + "'";
                try
				{
					conn.Open();
					sc = new SqlCommand(sql, conn);
					rd = sc.ExecuteReader();
					int chk = 0;
					while (rd.Read())
					{
						chk++;
						Label1.Text = rd.GetValue(0).ToString();
						Label2.Text = rd.GetValue(1).ToString();
					}
					if (chk == 0)
					{
						Label1.Text = tranId;
					}
					conn.Close();
				}
				catch { }

				//var temp = "";
				//SqlCommand sc2;
				//SqlDataReader rd2;
				//string sql2 = "select accId,companyId from CompanyAccount where companyId = '" + companyId + "' AND accId = '" + accId + "'";
				//try
				//{
				//    conn.Open();
				//    sc2 = new SqlCommand(sql2, conn);
				//    rd2 = sc2.ExecuteReader();
				//    while (rd2.Read())
				//    {
				//        temp = rd2.GetValue(0).ToString();
				//    }
				//    conn.Close();
				//}
				//catch { }
				getData3(Label1.Text, Label2.Text);
				if (payMethod.Text == "T" || payMethod.Text == "S")
				{
					accNumber.Disabled = false;
					bankCode.Disabled = false;
					cboBankAcc.Enabled = true;
				}

				this.DataGrid1.Columns[10].Visible = false;

				add.Visible = false;
				edit.Visible = false;

				cclt.Disabled = false;

				//save.Visible = true;
				update.Visible = true;
				cancel.Visible = true;

				totalPerMonth.Enabled = true;
				checkShort.Disabled = false;
				bNo.Disabled = false;
				cNo.Disabled = false;
				noPayMonth.Disabled = false;
				paymentDate.Disabled = false;
				effectiveDate.Disabled = false;
				expiredDate.Disabled = false;
				Remark1.Enabled = true;
				memberFee.Disabled = false;
				pay70.Disabled = false;
				pay100.Disabled = false;
				newsFee.Disabled = false;
				totalFee.Disabled = false;
				payRemark.Enabled = true;
			}
			
			
        }
        protected void update_Click(object sender, EventArgs e)
        {
            updateData();
            lasteditor();
        }
        private void updateData()
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string activityDetail;
			connection();
            var accNumberTemp = "";
            var bankCodeTemp = "";
            var accidTemp = "";
            SqlCommand sc3;
            SqlDataReader rd3;
            string sql3 = "select top 1 accId,accNumber,bankCode from CompanyAccount where companyId = '" + showMem + "' AND accNumber = '" + accNumber.Value + "' AND bankCode = '" + bankCode.Value + "'";
            try
            {
                conn.Open();
                sc3 = new SqlCommand(sql3, conn);
                rd3 = sc3.ExecuteReader();
                while (rd3.Read())
                {
                    accidTemp = rd3.GetValue(0).ToString();
                    accNumberTemp = rd3.GetValue(1).ToString();
                    bankCodeTemp = rd3.GetValue(2).ToString();
                }
                conn.Close();
            }
            catch { }
            var temp = "";
            SqlCommand sc2;
            SqlDataReader rd2;
            string sql2 = "select top 1 accId,companyId from CompanyAccount where companyId = '" + showMem + "' order by accId desc;";
            try
            {
                conn.Open();
                sc2 = new SqlCommand(sql2, conn);
                rd2 = sc2.ExecuteReader();
                while (rd2.Read())
                {
                    temp = rd2.GetValue(0).ToString();
                }
                conn.Close();
            }
            catch { }
            if (payMethod.Text == "S" || payMethod.Text == "T")
            {
                if (accNumber.Value == accNumberTemp && bankCode.Value == bankCodeTemp)
                {
                    connection();
                    cmd = new SqlCommand("SET dateformat dmy update companypayment set bno = '" + bNo.Value + "'," +
                                    "cno = '" + cNo.Value + "',paymentdate = '" + paymentDate.Value + "',effectivedate =  '" + effectiveDate.Value + "'," +
                                    "nopaymonth= '" + noPayMonth.Value + "',expireddate ='" + expiredDate.Value + "',payremark ='" + payRemark.Text + "'," +
                                    "checkshort = '" + checkShort.Checked + "', memberfee = '" + memberFee.Value + "', newsfee= '" + newsFee.Value + "',totalpermonth ='" + totalPerMonth.SelectedValue + "',totalfee ='" + totalFee.Value + "'," +
                                    "paymethod ='" + payMethod.Text + "', accId ='" + accidTemp + "',payRemark2 ='"+Remark1.Text +"' "+
                                    "where companyid = '" + showMem + "' and tranid = '" + Label1.Text + "'", conn);
                    conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						activityDetail = $"Changed data in a table 'CompanyPayment' Where tranId is '{Label1.Text}' and companyid is '{showMem}' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);

					}
					catch (SqlException ex)
					{
                        activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					catch (Exception ex)
					{
                        activityDetail = $@"Error: {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }

					conn.Close();
                }
                else
                {
                    long val = Int64.Parse(temp);
                    long accId = ++val;
                    connection();
                    cmd = new SqlCommand("SET dateformat dmy update companypayment set bno = '" + bNo.Value + "'," +
                                    "cno = '" + cNo.Value + "',paymentdate = '" + paymentDate.Value + "',effectivedate =  '" + effectiveDate.Value + "'," +
                                    "nopaymonth= '" + noPayMonth.Value + "',expireddate ='" + expiredDate.Value + "',payremark ='" + payRemark.Text + "'," +
                                    "checkshort = '" + checkShort.Checked + "', memberfee = '" + memberFee.Value + "', newsfee= '" + newsFee.Value + "',totalpermonth ='" + totalPerMonth.SelectedValue + "',totalfee ='" + totalFee.Value + "'," +
                                    "paymethod ='" + payMethod.Text + "', accId ='" + accId + "',payRemark2 ='" + Remark1.Text + "' " +
                                    "where companyid = '" + showMem + "' and tranid = '" + Label1.Text + "'", conn);
                    conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						activityDetail = $"Changed data in a table 'CompanyPayment' Where tranId is '{Label1.Text}' and companyid is '{showMem}' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);

					}
					catch (SqlException ex)
					{
                        activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					catch (Exception ex)
					{
                        activityDetail = $@"Error: {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					conn.Close();

                    //insert

                    string InsertSQL = "INSERT INTO dbo.CompanyAccount (accId, companyId, accNumber,bankCode) VALUES (@temp,@companyId,@accNumber,@bankCode)";
                    SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                    conn.Open();
                    vlozSQL.Parameters.AddWithValue("@temp", accId);
                    vlozSQL.Parameters.AddWithValue("@companyId", showMem);
                    vlozSQL.Parameters.AddWithValue("@accNumber", accNumber.Value);
                    vlozSQL.Parameters.AddWithValue("@bankCode", bankCode.Value);
                    
					try
					{
						vlozSQL.ExecuteNonQuery();
						activityDetail = $"Added new data into a table 'CompanyAccount' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);

					}
					catch (SqlException ex)
					{
                        activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					catch (Exception ex)
					{
                        activityDetail = $@"Error: {ex.Message} in {this}";
                        logActivity.LogStaffActivity(staffID, activityDetail);
                        logActivity.IssueReport(ex);
                    }
					vlozSQL.Parameters.Clear();
                    conn.Close();
                }
                //connection();
                //cmd = new SqlCommand("update companypayment set bno = '" + bNo.Value + "'," +
                //                "cno = '" + cNo.Value + "',paymentdate = '" + paymentDate.Value + "',effectivedate =  '" + effectiveDate.Value + "'," +
                //                "nopaymonth= '" + noPayMonth.Value + "',expireddate ='" + expiredDate.Value + "',payremark ='" + payRemark.Text + "'," +
                //                "checkshort = '" + checkShort.Checked + "', memberfee = '" + memberFee.Value + "', newsfee= '" + newsFee.Value + "',totalpermonth ='" + totalPerMonth.SelectedValue + "',totalfee ='" + totalFee.Value + "'," +
                //                "paymethod ='" + payMethod.Text + "'" +
                //                "where companyid = '" + companyId + "' and tranid = '" + Label1.Text + "'", conn);
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();

                //cmd = new SqlCommand("update CompanyAccount set accNumber = '" + accNumber.Value + "'," +
                //                "bankCode ='" + bankCode.Value + "'" +
                //                "where companyid = '" + companyId + "' and accId = '" + Label2.Text + "'", conn);
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();

                getData3(Label1.Text, Label2.Text);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                connection();
                cmd = new SqlCommand("SET dateformat dmy update companypayment set bno = '" + bNo.Value + "'," +
                                "cno = '" + cNo.Value + "',paymentdate = '" + paymentDate.Value + "',effectivedate =  '" + effectiveDate.Value + "'," +
                                "nopaymonth= '" + noPayMonth.Value + "',expireddate ='" + expiredDate.Value + "',payremark ='" + payRemark.Text + "'," +
                                "checkshort = '" + checkShort.Checked + "', memberfee = '" + memberFee.Value + "', newsfee= '" + newsFee.Value + "',totalpermonth ='" + totalPerMonth.SelectedValue + "',totalfee ='" + totalFee.Value + "'," +
                                "paymethod ='" + payMethod.Text + "',payRemark2 ='" + Remark1.Text + "' " +
                                "where companyid = '" + showMem + "' and tranid = '" + Label1.Text + "'", conn);
                conn.Open();

                try
                {
					//var k = cmd.ExecuteNonQuery();
					if (cmd.ExecuteNonQuery() > 0)
					{
						activityDetail = $"Changed data in a table 'CompanyPayment' Where tranId is '{Label1.Text}' and companyid is '{showMem}' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
						Page.Response.Redirect(Page.Request.Url.ToString(), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
					else
					{
						activityDetail = $"Changed data in a table 'CompanyPayment' Where tranId is '{Label1.Text}' and companyid is '{showMem}' unsuccessful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
						Page.Response.Redirect(Page.Request.Url.ToString(), false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
				}
                catch (SqlException ex)
                {
                    activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                    logActivity.IssueReport(ex);
                }
				catch (Exception ex)
				{
                    activityDetail = $@"Error: {ex.Message} in {this}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                    logActivity.IssueReport(ex);
                }

				//try
				//{
				//	var k = cmd.ExecuteNonQuery();
				//	activityDetail = $"Update CompanyPayment Where tranId = '{Label1.Text}' and companyid = '{companyId}' successful (User id = '{staffID}')";
				//	logActivity.LogStaffActivity(staffID, activityDetail);

				//}
				//catch
				//{
				//	activityDetail = $"Update CompanyPayment Where tranId = '{Label1.Text}' and companyid = '{companyId}' unsuccessful (User id = '{staffID}')";
				//	logActivity.LogStaffActivity(staffID, activityDetail);
				//}
				getData3(Label1.Text, Label2.Text);
                conn.Close();
            }
        }
        protected void lasteditor()
        {
			var uid = Session["UID"];
			string activityDetail;
            if (uid != null)
            {
                uid = Session["UID"].ToString();
            }
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string query = "UPDATE CompanyMember SET updatedBy = '" + uid + "' WHERE companyid = '" + showMem + "'";
            cmd = new SqlCommand(query, conn);
            conn.Open();
            try
            {
				cmd.ExecuteNonQuery();
				activityDetail = $"changed data in a table 'CompanyPayment' Where companyid is '{showMem}' successful (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
                activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
                logActivity.IssueReport(ex);
            }
			catch (Exception ex)
			{
                activityDetail = $@"Error: {ex.Message} in {this}";
                logActivity.LogStaffActivity(staffID, activityDetail);
                logActivity.IssueReport(ex);
            }
			conn.Close();
        }
    }
}