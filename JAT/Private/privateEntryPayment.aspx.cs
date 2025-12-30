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
using JAT.Core;
using System.Web.Configuration; // ต้องมี using ตัวนี้


namespace JAT.Private
{
    public partial class privateEntryPayment : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
		//public string postBackId;

		private string showfristMem;
        private string showMem;
        string addValue;
        string tran;
        string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));
        string toDayDateOnly = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));

        protected void Page_Load(object sender, EventArgs e)
        {
            showfristMem = Request.QueryString["firstmemberid"];
            showMem = Request.QueryString["memberid"];
            addValue = Request.QueryString["mode"];

            if (showMem != null || showfristMem != null)
            {
                if (!Page.IsPostBack)
                {
                    update.Visible = false;
                    if (addValue == "add")
                    {
                        EnabledForm();
                    }
                    else
                    {
                        DisabledForm();
                    }

                    // ใช้ repository แทน connection() + SqlCommand
                    string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

                    string sql = @"
                SELECT TOP 1 pp.tranId, pp.payMethod
                FROM PrivatePayment pp
                INNER JOIN PrivateDetail pd ON pp.memberid = pd.memberid
                WHERE pp.memberid = @firstMemberId
                ORDER BY pp.tranId";

                    var parameters = new Dictionary<string, object>
            {
                { "@firstMemberId", showfristMem }
            };

                    DataTable dt = paymentRepo.ExecuteSelectQuery(sql, parameters);

                    if (dt.Rows.Count > 0)
                    {
                        Label5.Text = dt.Rows[0]["tranId"].ToString();
                        Label6.Text = dt.Rows[0]["payMethod"].ToString();
                        // updateBy.Text = ??? (เรียก lastEditor() แทน)
                    }

                    lastEditor(); // ถ้าแก้ lastEditor ให้เรียก repository แล้วก็ยังใช้ได้
                    GetDropDown();
                    BindData();

                    if (addValue != "add")
                    {
                        BindData2();
                    }

                    showInGrid();
                }
            }
        }


        /*
        private void connection() c0mment for unuse
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
            cmd.CommandTimeout = 600;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        */

        /* commentted to use new refactor
        private void lastEditor()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "select top 1 staffFName " +
                "from PrivatePayment pp " +
                "left outer join SStaff ss on pp.updatedBy = ss.staffID " +
                "where memberid = '" + showfristMem + "' " +
                "order by pp.updatedDate desc";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    updateBy.Text = rd.GetValue(0).ToString();
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
        */

        private void lastEditor()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = @"
        SELECT TOP 1 ss.staffFName
        FROM PrivatePayment pp
        LEFT JOIN SStaff ss ON pp.updatedBy = ss.staffID
        WHERE pp.memberid = @memberid
        ORDER BY pp.updatedDate DESC";

            var parameters = new Dictionary<string, object>
    {
        {"@memberid", showfristMem}
    };

            DataTable dt = paymentRepo.ExecuteSelectQuery(sql, parameters);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                updateBy.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                updateBy.Text = "N/A";
            }
        }



        private void GetDropDown()
        {
            int tmpSelect = 0;
            ///BindPaymentMethodList();
            BindMemberTypeList();
            BindMemberFamilyList();
            BindMemberAccountList();
        }


        /* commentted for used new refactor
        protected void BindPaymentMethodList()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from SPayMethod order by payMethod", con);

                    adapter.Fill(subjects);
                    DropDownList3.DataSource = subjects;
                    DropDownList3.DataValueField = "payMethod";
                    DropDownList3.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            DropDownList3.Items.Insert(0, new ListItem("--Any--", "0"));
        }
        */

        protected void BindPaymentMethodList()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = "SELECT * FROM SPayMethod ORDER BY payMethod";

            DataTable subjects = paymentRepo.ExecuteSelectQuery(sql);

            DropDownList3.DataSource = subjects;
            DropDownList3.DataValueField = "payMethod";
            DropDownList3.DataBind();

            DropDownList3.Items.Insert(0, new ListItem("--Any--", "0"));
        }




        /* commentted to used new refactor
        protected void BindMemberTypeList()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from SMemberType where EffectiveID = (select TOP(1) EffectiveID from tblEffective where EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate>= floor(cast(getdate() as float))) " +
                                                                "order by memberType ", con);

                    adapter.Fill(subjects);
                    DropDownList2.DataSource = subjects;
                    DropDownList2.DataValueField = "MemberType";
                    DropDownList2.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            DropDownList2.Items.Insert(0, new ListItem("0", "0"));
            DropDownList2.Items.Insert(0, new ListItem("--Any--", "99"));
        }
        protected void BindMemberFamilyList()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select memberId, (prefixNm + ' '+ nameE) as nameEng from privateDetail where firstmemberId = '" + showfristMem + "'", con);

                    //adapter.Fill(subjects);
                    //DropDownList1.DataSource = subjects;
                    //DropDownList1.DataValueField = "nameEng";
                    //DropDownList1.DataBind();

                    adapter.Fill(subjects);
                    DropDownList1.DataSource = subjects;
                    string[] tmpMap = { "memberId", "nameEng" };
                    DropDownList1.DataValueField = tmpMap[0];
                    //DropDownList1.DataTextField = tmpMap[0];

                    //DropDownList1.DataValueField = tmpMap[1];
                    DropDownList1.DataTextField = tmpMap[1];
                    DropDownList1.DataBind();

                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            DropDownList1.Items.Insert(0, new ListItem("--Any--", "0"));
        }
        */

        protected void BindMemberTypeList()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = @"
        SELECT * 
        FROM SMemberType 
        WHERE EffectiveID = (
            SELECT TOP(1) EffectiveID 
            FROM tblEffective 
            WHERE EffectiveDate <= FLOOR(CAST(GETDATE() AS FLOAT)) 
              AND ExpireDate >= FLOOR(CAST(GETDATE() AS FLOAT))
        )
        ORDER BY memberType";

            DataTable subjects = paymentRepo.ExecuteSelectQuery(sql);

            DropDownList2.DataSource = subjects;
            DropDownList2.DataValueField = "MemberType";
            DropDownList2.DataBind();

            DropDownList2.Items.Insert(0, new ListItem("0", "0"));
            DropDownList2.Items.Insert(0, new ListItem("--Any--", "99"));
        }

        protected void BindMemberFamilyList()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = "SELECT memberId, (prefixNm + ' ' + nameE) AS nameEng FROM privateDetail WHERE firstmemberId = @firstMemberId";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            DataTable subjects = paymentRepo.ExecuteSelectQuery(sql, parameters);

            DropDownList1.DataSource = subjects;
            DropDownList1.DataValueField = "memberId";
            DropDownList1.DataTextField = "nameEng";
            DropDownList1.DataBind();

            DropDownList1.Items.Insert(0, new ListItem("--Any--", "0"));
        }


        /* commentted to used new refactor
        protected void BindMemberAccountList()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select accId, (accNo+ ':' + bankCode + ':' + convert(nvarchar(2), payAccDuration)) as bankDetail " +
                                                                "from privateaccount where memberId ='" + showfristMem + "'", con);
                    adapter.Fill(subjects);
                    DropDownList4.DataSource = subjects;
                    string[] tmpMap = { "accId", "bankDetail" };
                    DropDownList4.DataValueField = tmpMap[0];
                    DropDownList4.DataTextField = tmpMap[1];
                    DropDownList4.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            DropDownList4.Items.Insert(0, new ListItem("--Any--", "0"));


        }
        */
        protected void BindMemberAccountList()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = @"
        SELECT accId, 
               (accNo + ':' + bankCode + ':' + CONVERT(nvarchar(2), payAccDuration)) AS bankDetail
        FROM privateaccount
        WHERE memberId = @firstMemberId";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            DataTable subjects = paymentRepo.ExecuteSelectQuery(sql, parameters);

            DropDownList4.DataSource = subjects;
            DropDownList4.DataValueField = "accId";
            DropDownList4.DataTextField = "bankDetail";
            DropDownList4.DataBind();

            DropDownList4.Items.Insert(0, new ListItem("--Any--", "0"));
        }





        private void EnabledForm()
        {
            addBTN.Visible = false;
            //addBTN.Text = "";

            saveBtn.Visible = true;
            cancelBtn.Visible = true;

            numMemBox.Attributes.Remove("disabled");
            PayDuration.Attributes.Remove("disabled");
            paymentDateBox.Attributes.Remove("disabled");
            EffectiveDateInput.Attributes.Remove("disabled");
            entranceVal.Attributes.Remove("disabled");
            annualVal.Attributes.Remove("disabled");
            remarkBox.Attributes.Remove("disabled");

            DropDownList1.Attributes.Remove("disabled");
            DropDownList2.Attributes.Remove("disabled");
            DropDownList3.Attributes.Remove("disabled");
            DropDownList4.Attributes.Add("disabled", "disabled");
            ddpayat.Attributes.Remove("disabled");
            RadioButtonList1.Enabled = true;
            PaymentType.Enabled = true;

            GetReceiptChk.Enabled = true;
            CheckShort.Enabled = true;
            bankPanel.Attributes.Add("disabled", "disabled");

            //18oct2021
            GetReceiptChk.Checked = true;
            cNoBox.Attributes.Remove("disabled");

            entranceVal.ReadOnly = false;
            annualVal.ReadOnly = false;
            totolSum.ReadOnly = false;
        }
        private void EnabledFormUpdate()
        {
            addBTN.Visible = false;
            //addBTN.Text = "";
            update.Visible = true;
            saveBtn.Visible = false;
            cancelBtn.Visible = true;

            numMemBox.Attributes.Remove("disabled");
            PayDuration.Attributes.Remove("disabled");
            paymentDateBox.Attributes.Remove("disabled");
            EffectiveDateInput.Attributes.Remove("disabled");
            entranceVal.Attributes.Remove("disabled");
            annualVal.Attributes.Remove("disabled");
            remarkBox.Attributes.Remove("disabled");


            DropDownList1.Attributes.Remove("disabled");
            DropDownList2.Attributes.Remove("disabled");
            DropDownList3.Attributes.Remove("disabled");
            ddpayat.Attributes.Remove("disabled");

            PaymentType.Enabled = true;

            GetReceiptChk.Enabled = true;
            CheckShort.Enabled = true;

            //18oct2021
            GetReceiptChk.Checked = true;
            cNoBox.Attributes.Remove("disabled");

            entranceVal.ReadOnly = false;
            annualVal.ReadOnly = false;
            totolSum.ReadOnly = false;
        }

        private void DisabledForm()
        {
            addBTN.Visible = true;
            saveBtn.Visible = false;
            cancelBtn.Visible = false;

            //saveBtn.Visible = false;
            //cancelBtn.Visible = false;

            //Box1.Enabled = false;
            //Box2.Attributes.Add("disabled", "disabled");
            DropDownList1.Attributes.Add("disabled", "disabled");
            DropDownList2.Attributes.Add("disabled", "disabled");
            DropDownList3.Attributes.Add("disabled", "disabled");
            DropDownList4.Attributes.Add("disabled", "disabled");
            ddpayat.Attributes.Add("disabled", "disabled");
            RadioButtonList1.Enabled = false;
            PaymentType.Enabled = false;
            //paymentDateBox.Enabled = false;
            numMemBox.Attributes.Add("disabled", "disabled");
            PayDuration.Attributes.Add("disabled", "disabled");
            paymentDateBox.Attributes.Add("disabled", "disabled");
            EffectiveDateInput.Attributes.Add("disabled", "disabled");
            entranceVal.Attributes.Add("disabled", "disabled");
            annualVal.Attributes.Add("disabled", "disabled");
            remarkBox.Attributes.Add("disabled", "disabled");
            bankPanel.Attributes.Add("disabled", "disabled");

            GetReceiptChk.Enabled = false;
            CheckShort.Enabled = false;


        }


        /* commentted to use new refactor
        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT t1.nameJ, CONCAT(t1.prefixNm, t1.nameE), t2.companyNm " +
                            "FROM PrivateDetail t1 " +
                            "INNER JOIN privateAddress t2 ON t1.firstmemberid = t2.memberid " +
                            "WHERE t1.memberid = '" + showfristMem + "'" + "AND t2.addressType = '2'";
            //string sqlBrithPlace = "SELECT birthPlace FROM PrivateDetail WHERE memberid = '" + companyId +"'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in member information
                    Label1.Text = rd.GetValue(0).ToString();
                    Label2.Text = rd.GetValue(1).ToString();
                    Label3.Text = rd.GetValue(2).ToString();
                }

            }
            catch { }

            //Box2.Value = showVal;


            conn.Close();
        }
        */

        protected void BindData()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = @"
        SELECT t1.nameJ, CONCAT(t1.prefixNm, t1.nameE), t2.companyNm
        FROM PrivateDetail t1
        INNER JOIN privateAddress t2 ON t1.firstmemberid = t2.memberid
        WHERE t1.memberid = @firstMemberId
          AND t2.addressType = 2";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            DataTable dt = paymentRepo.ExecuteSelectQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                Label1.Text = dt.Rows[0][0].ToString();
                Label2.Text = dt.Rows[0][1].ToString();
                Label3.Text = dt.Rows[0][2].ToString();
            }
        }





        /* commetted to use new refactor
        protected void BindData2()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "";
            if (Label6.Text == "B" || Label6.Text == "T" || Label6.Text == "S")
            {
                sql = "select pp.tranId,pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember,payDuration,FORMAT(paymentDate, 'dd/MM/yyyy'), " +
                         "FORMAT(effectiveDate, 'dd/MM/yyyy'),FORMAT(expireDate, 'dd/MM/yyyy'),payMethod,payRemark,receiptNo,checkReceipt," +
                         "entranceFee,newsletterFee,checkShort,FORMAT(shortFrom, 'dd/MM/yyyy'),FORMAT(shortTo, 'dd/MM/yyyy'),accno,bankcode,payAccDuration " +
                         "from PrivatePayment pp " +
                         "left outer join privateDetail d on pp.payBy = d.memberId " +
                          "left outer join privatePayAccount ppa on ppa.tranId = pp.tranId " +
                          "left outer join privateAccount pa on pa.memberid = pp.memberid and pa.accId = ppa.accId " +
                          "left outer join PrivatePayShort pps on pp.tranId = pps.tranId " +
                         //"left outer join SStaff ss on pp.updatedBy = ss.staffID " +
                         "where pp.memberid = '" + showfristMem + "' and ppa.tranId = '" + Label5.Text + "' " +
                         "order by pp.tranId desc";

                //sql = "select pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember,payDuration,paymentDate, " +
                //          "effectiveDate,expireDate,payMethod,payRemark,receiptNo,checkReceipt,entranceFee, " +
                //          "newsletterFee,pp.tranId ,checkShort ,pa.accno,pa.bankcode,pa.payAccDuration " +
                //          "from PrivatePayment pp " +
                //          "left outer join privateDetail d on pp.payBy = d.memberId " +
                //          "left outer join privatePayAccount ppa on ppa.tranId = pp.tranId " +
                //          "left outer join privateAccount pa on pa.memberid = pp.memberid and pa.accId = ppa.accId " +
                //          "where pp.memberid = '" + showfirstMem + "' and pp.tranId = '" + tranId + "' ";
            }
            else
            {
                sql = "select top 1 pp.tranId,pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember, " +
                      "payDuration,FORMAT(paymentDate, 'dd/MM/yyyy'), FORMAT(effectiveDate, 'dd/MM/yyyy'),FORMAT(expireDate, 'dd/MM/yyyy'),payMethod,payRemark,receiptNo,checkReceipt,entranceFee, " +
                      "newsletterFee,checkShort,FORMAT(shortFrom, 'dd/MM/yyyy'),FORMAT(shortTo, 'dd/MM/yyyy') " +
                      "from PrivatePayment pp " +
                      "inner join PrivateDetail pd on pp.memberid = pd.memberid " +
                      "left outer join PrivatePayShort pps on pp.tranId = pps.tranId " +
                      //"left outer join SStaff ss on pp.updatedBy = ss.staffID " +
                      "where pp.memberid = '" + showfristMem + "' " +
                      "order by tranId ";

            }

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                sc.CommandTimeout = 480;
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in member information
                    DropDownList1.SelectedItem.Text = rd.GetValue(2).ToString();
                    DropDownList2.SelectedValue = rd.GetValue(3).ToString();
                    numMemBox.Text = rd.GetValue(4).ToString();
                    PayDuration.Text = rd.GetValue(5).ToString();
                    paymentDateBox.Value = rd.GetValue(6).ToString();
                    EffectiveDateInput.Value = rd.GetValue(7).ToString();
                    ExpiredDate.Text = rd.GetValue(8).ToString();
                    //paymentDateBox.Value = ((DateTime)rd.GetValue(6)).ToString("").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //EffectiveDateInput.Value = ((DateTime)rd.GetValue(7)).ToString("").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    //ExpiredDate.Value = ((DateTime)rd.GetValue(8)).ToString("").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                    string paymethodcho = rd.GetValue(9).ToString();
                    if (paymethodcho == "")
                    {
                        paymethodcho = "-- Any --";
                    }
                    DropDownList3.SelectedValue = paymethodcho;
                    remarkBox.Value = rd.GetValue(10).ToString();
                    cNoBox.Value = rd.GetValue(11).ToString();
					//GetReceiptChk.Checked = rd.GetValue(11).ToString();
					bool ch2;
					if (rd.GetValue(12).GetType() == typeof(DBNull))
                    {
                        ch2 = false;
                    }
                    else
                    {
						ch2 = (bool)rd.GetValue(12);
					}
                    var ch22 = rd.GetValue(12).GetType();
					string ch11 = rd.GetValue(15).ToString();
					
                    GetReceiptChk.Checked = ch2;
                    entranceVal.Text = rd.GetValue(13).ToString();
                    annualVal.Text = rd.GetValue(14).ToString();
                    bool ch1;
					if (rd.GetValue(15).GetType() == typeof(DBNull))
					{
						 ch1 = false;
					}
					else
					{
						 ch1 = (bool)rd.GetValue(15);
					}
					
                    CheckShort.Checked = ch1;

                    ShortFromBox.Value = rd.GetValue(16).ToString();
                    ShortToBox.Value = rd.GetValue(17).ToString();
                    if (Label6.Text == "S" || Label6.Text == "T" || Label6.Text == "B")
                    {
                        bankPanel.Value = rd.GetValue(18).ToString();
                        bankCodeBox.Value = rd.GetValue(19).ToString();
                        RadioButtonList1.SelectedValue = rd.GetValue(20).ToString();
                    }
                    else
                    {
                        //?
                    }


                }

            }
            catch { }


            conn.Close();
        }
        */
        protected void BindData2()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = "";
            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem },
        { "@tranId", Label5.Text }
    };

            if (Label6.Text == "B" || Label6.Text == "T" || Label6.Text == "S")
            {
                sql = @"
            SELECT pp.tranId, pp.memberId, (prefixNm + ' ' + nameE) as nameEng, pp.memberType, payNoMember, payDuration,
                   FORMAT(paymentDate, 'dd/MM/yyyy'), FORMAT(effectiveDate, 'dd/MM/yyyy'), FORMAT(expireDate, 'dd/MM/yyyy'),
                   payMethod, payRemark, receiptNo, checkReceipt, entranceFee, newsletterFee, checkShort,
                   FORMAT(shortFrom, 'dd/MM/yyyy'), FORMAT(shortTo, 'dd/MM/yyyy'), accno, bankcode, payAccDuration
            FROM PrivatePayment pp
            LEFT OUTER JOIN privateDetail d ON pp.payBy = d.memberId
            LEFT OUTER JOIN privatePayAccount ppa ON ppa.tranId = pp.tranId
            LEFT OUTER JOIN privateAccount pa ON pa.memberid = pp.memberid AND pa.accId = ppa.accId
            LEFT OUTER JOIN PrivatePayShort pps ON pp.tranId = pps.tranId
            WHERE pp.memberid = @firstMemberId AND ppa.tranId = @tranId
            ORDER BY pp.tranId DESC";
            }
            else
            {
                sql = @"
            SELECT TOP 1 pp.tranId, pp.memberId, (prefixNm + ' ' + nameE) as nameEng, pp.memberType, payNoMember,
                   payDuration, FORMAT(paymentDate, 'dd/MM/yyyy'), FORMAT(effectiveDate, 'dd/MM/yyyy'), FORMAT(expireDate, 'dd/MM/yyyy'),
                   payMethod, payRemark, receiptNo, checkReceipt, entranceFee, newsletterFee, checkShort,
                   FORMAT(shortFrom, 'dd/MM/yyyy'), FORMAT(shortTo, 'dd/MM/yyyy')
            FROM PrivatePayment pp
            INNER JOIN PrivateDetail pd ON pp.memberid = pd.memberid
            LEFT OUTER JOIN PrivatePayShort pps ON pp.tranId = pps.tranId
            WHERE pp.memberid = @firstMemberId
            ORDER BY tranId";
            }

            DataTable dt = paymentRepo.ExecuteSelectQuery(sql, parameters);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                DropDownList1.SelectedItem.Text = row[2].ToString();
                DropDownList2.SelectedValue = row[3].ToString();
                numMemBox.Text = row[4].ToString();
                PayDuration.Text = row[5].ToString();
                paymentDateBox.Value = row[6].ToString();
                EffectiveDateInput.Value = row[7].ToString();
                ExpiredDate.Text = row[8].ToString();

                string paymethodcho = string.IsNullOrEmpty(row[9].ToString()) ? "-- Any --" : row[9].ToString();
                DropDownList3.SelectedValue = paymethodcho;

                remarkBox.Value = row[10].ToString();
                cNoBox.Value = row[11].ToString();
                GetReceiptChk.Checked = row[12] != DBNull.Value && (bool)row[12];
                entranceVal.Text = row[13].ToString();
                annualVal.Text = row[14].ToString();
                CheckShort.Checked = row[15] != DBNull.Value && (bool)row[15];
                ShortFromBox.Value = row[16].ToString();
                ShortToBox.Value = row[17].ToString();

                if (Label6.Text == "S" || Label6.Text == "T" || Label6.Text == "B")
                {
                    bankPanel.Value = row[18].ToString();
                    bankCodeBox.Value = row[19].ToString();
                    RadioButtonList1.SelectedValue = row[20].ToString();
                }
            }
        }






        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void cancelTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateCancel.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void specialTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateFeature.aspx?firstmemberid=" + showfristMem);
            }
        }

        /* commentted for new refactor
        protected void showInGrid() //about payment list under private entrypayment page
        {
            DataTable td;

            //td = SelectSqlTable("SELECT t1.tranId, t1.payBy, t2.nameE, FORMAT(t1.paymentDate, 'yyyy-MMM-dd') AS paymentDate, " +
            //                    "FORMAT(t1.effectiveDate, 'yyyy-MMM-dd') AS effectiveDate, FORMAT(t1.expireDate, 'yyyy-MMM-dd') AS expireDate, " +
            //                    "t1.payMethod, t3.accno, t3.bankcode, t1.receiptNo, t1.payNoMember, t1.entranceFee, t1.payDuration, t1.newsletterFee," +
            //                    " t1.payRemark " +
            //                    "FROM PrivatePayment t1 " +
            //                    "INNER JOIN PrivateDetail t2 ON t1.memberid = t2.memberid INNER JOIN PrivateAccount t3 ON t2.memberid = t3.accId " +
            //                    "WHERE t2.firstmemberid = " + "'" + showfirstMem + "'");

            // *** 2024-09-06 02.55pm : Toon Jiradech.K have revised code
            td = SelectSqlTable("select t1.tranId, t1.payBy, t2.nameE, FORMAT(t1.paymentDate, 'dd/MM/yyyy') AS paymentDate, " +
                                "FORMAT(t1.effectiveDate, 'dd/MM/yyyy') AS effectiveDate, FORMAT(t1.expireDate, 'dd/MM/yyyy') AS expireDate, " +
                                "t1.payMethod, t3.accno, t3.bankcode, t1.receiptNo, t1.payNoMember, t1.entranceFee, " +
                                "t1.payDuration, t1.newsletterFee, t1.payRemark, t3.accId, t1.payat " +
                                //"t1.payDuration, t1.newsletterFee, t1.payRemark, t1.payat " +
                                "from PrivatePayment t1 " +
                                "left outer join privateDetail t2 on t1.payBy = t2.memberId " +
                                "left outer join privatePayAccount ppa on ppa.tranId = t1.tranId " +
                                "left outer join privateAccount t3 on t3.memberid = t1.memberid and t3.accId = ppa.accId " +
                                "WHERE t2.firstmemberid = " + "'" + showfristMem + "' " +
                                "AND t1.Deleted_at IS NULL " +

                                //add new code
                                "AND t1.checkpay = 'C'"+
                                //end line 

                                //old code
                                //"ORDER BY t1.paymentDate DESC, t1.expireDate DESC, t1.receiptNo");
                                "ORDER BY t1.expireDate DESC");
            
                                //comment for unused 15/08/2025 14:52
                                //+ ", t1.receiptNo");

            



            // *** End Of Ryvised
            //GridView1.Columns[0].Visible = false;
            GridView1.DataSource = td;
            //ImageButton1.Visible = true;
            //ImageButton2.Visible = true;

            GridView1.DataBind();
            conn.Close();

        }
        */

        protected void showInGrid()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = @"
        SELECT 
            t1.tranId, t1.payBy, t2.nameE, 
            FORMAT(t1.paymentDate, 'dd/MM/yyyy') AS paymentDate, 
            FORMAT(t1.effectiveDate, 'dd/MM/yyyy') AS effectiveDate, 
            FORMAT(t1.expireDate, 'dd/MM/yyyy') AS expireDate, 
            t1.payMethod, t3.accno, t3.bankcode, 
            t1.receiptNo, t1.payNoMember, t1.entranceFee, 
            t1.payDuration, t1.newsletterFee, t1.payRemark, 
            t3.accId, t1.payat
        FROM PrivatePayment t1
        LEFT OUTER JOIN privateDetail t2 ON t1.payBy = t2.memberId
        LEFT OUTER JOIN privatePayAccount ppa ON ppa.tranId = t1.tranId
        LEFT OUTER JOIN privateAccount t3 ON t3.memberid = t1.memberid AND t3.accId = ppa.accId
        WHERE t2.firstmemberid = @firstMemberId
          AND t1.Deleted_at IS NULL
          AND t1.checkpay = 'C'
        ORDER BY t1.expireDate DESC";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            DataTable td = paymentRepo.ExecuteSelectQuery(sql, parameters);

            GridView1.DataSource = td;
            GridView1.DataBind();
        }






        /* commetted to used new refactor
        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            //EnabledForm();
            //saveBtn.Visible = false;
            //updateBtn.Visible = true;

            GridView1.Columns[15].Visible = false;
            GridView1.Columns[16].Visible = false;


            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            //HiddenField1.Value = row.Cells[0].Text;

            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT t1.tranId, t1.payBy, t2.nameE, FORMAT(t1.paymentDate, 'dd/MM/yyyy') AS paymentDate, FORMAT(t1.effectiveDate, 'dd/MM/yyyy') AS effectiveDate, FORMAT(t1.expireDate, 'dd/MM/yyyy') AS expireDate, t1.payMethod, t3.accno, t3.bankcode, t1.receiptNo, t1.payNoMember, t1.entranceFee, t1.payDuration, t1.newsletterFee, t1.payRemark, t1.memberType, t1.checkReceipt, t1.checkShort " +
                            "FROM PrivatePayment t1  " +
                            "INNER JOIN PrivateDetail t2 ON t1.payBy = t2.memberid INNER JOIN PrivateAccount t3 ON t2.memberid = t3.accId WHERE t1.tranId = " + "'" + row.Cells[0].Text + "'";


            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();


                while (rd.Read())
                {
                    BindInPayBy();

                    bool chkRec = (bool)rd.GetValue(16);
                    GetReceiptChk.Checked = chkRec;
                    bool chkShort = (bool)rd.GetValue(17);
                    CheckShort.Checked = chkShort;

                    string memType = rd.GetValue(15).ToString();
                    if (memType == "0")
                    {
                        DropDownList2.SelectedIndex = 1;
                    }
                    else if (memType == "1")
                    {
                        DropDownList2.SelectedIndex = 2;
                    }
                    else if (memType == "2")
                    {
                        DropDownList2.SelectedIndex = 3;
                    }
                    else if (memType == "3A")
                    {
                        DropDownList2.SelectedIndex = 4;
                    }
                    else if (memType == "3B")
                    {
                        DropDownList2.SelectedIndex = 5;
                    }
                    else if (memType == "4")
                    {
                        DropDownList2.SelectedIndex = 6;
                    }
                    else if (memType == "6A")
                    {
                        DropDownList2.SelectedIndex = 7;
                    }
                    else if (memType == "6B")
                    {
                        DropDownList2.SelectedIndex = 8;
                    }
                    else if (memType == "7")
                    {
                        DropDownList2.SelectedIndex = 9;
                    }
                    else
                    {
                        DropDownList2.ClearSelection();
                    }

                    numMemBox.Text = rd.GetValue(10).ToString();
                    PayDuration.Text = rd.GetValue(12).ToString();
                    paymentDateBox.Value = rd.GetValue(3).ToString();
                    EffectiveDateInput.Value = rd.GetValue(4).ToString();
                    ExpiredDate.Text = rd.GetValue(5).ToString();
                    //Box3.Value = rd.GetValue(4).ToString();

                    string paymentMet = rd.GetValue(6).ToString();
                    if (paymentMet == "B")
                    {
                        DropDownList3.SelectedIndex = 1;
                    }
                    else if (paymentMet == "F")
                    {
                        DropDownList3.SelectedIndex = 2;
                    }
                    else if (paymentMet == "J")
                    {
                        DropDownList3.SelectedIndex = 3;
                    }
                    else if (paymentMet == "K")
                    {
                        DropDownList3.SelectedIndex = 4;
                    }
                    else if (paymentMet == "P")
                    {
                        DropDownList3.SelectedIndex = 5;
                    }
                    else if (paymentMet == "S")
                    {
                        DropDownList3.SelectedIndex = 6;
                    }
                    else if (paymentMet == "T")
                    {
                        DropDownList3.SelectedIndex = 7;
                    }
                    else
                    {
                        DropDownList3.ClearSelection();
                    }

                    cNoBox.Value = rd.GetValue(9).ToString();
                    bankCodeBox.Value = rd.GetValue(7).ToString();
                    bankPanel.Value = rd.GetValue(8).ToString();
                    remarkBox.Value = rd.GetValue(14).ToString();
                    entranceVal.Text = rd.GetValue(11).ToString();
                    annualVal.Text = rd.GetValue(13).ToString();

                    int num1 = Int32.Parse(entranceVal.Text);
                    int num2 = Int32.Parse(annualVal.Text);
                    int sum = num1 + num2;
                    totolSum.Text = sum.ToString();

                    if (num1 > 0)
                    {
                        PaymentType.SelectedValue = "Entrance Fee";
                    }
                    else if (num2 > 0)
                    {
                        PaymentType.SelectedValue = "Annual Fee";
                    }
                    else if (num1 > 0 && num2 > 0)
                    {
                        PaymentType.SelectedValue = "Both";
                    }

                    int payDurationChk = Int32.Parse(PayDuration.Text);
                    if (payDurationChk >= 12)
                    {
                        RadioButtonList1.SelectedValue = "12";
                    }
                    else if (payDurationChk >= 6)
                    {
                        RadioButtonList1.SelectedValue = "6";
                    }
                    else
                    {
                        RadioButtonList1.SelectedValue = "1";
                    }
                }

            }
            catch { }

            conn.Close();

        }
        */
        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            GridView1.Columns[15].Visible = false;
            GridView1.Columns[16].Visible = false;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            string tranId = row.Cells[0].Text;

            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var repo = new PrivatePaymentRepository(connStr);

            try
            {
                string sql = @"SELECT t1.tranId, t1.payBy, t2.nameE, 
                              FORMAT(t1.paymentDate, 'dd/MM/yyyy') AS paymentDate, 
                              FORMAT(t1.effectiveDate, 'dd/MM/yyyy') AS effectiveDate, 
                              FORMAT(t1.expireDate, 'dd/MM/yyyy') AS expireDate, 
                              t1.payMethod, t3.accno, t3.bankcode, 
                              t1.receiptNo, t1.payNoMember, t1.entranceFee, 
                              t1.payDuration, t1.newsletterFee, t1.payRemark, 
                              t1.memberType, t1.checkReceipt, t1.checkShort
                       FROM PrivatePayment t1
                       INNER JOIN PrivateDetail t2 ON t1.payBy = t2.memberid
                       INNER JOIN PrivateAccount t3 ON t1.tranId = t3.accId
                       WHERE t1.tranId=@tranId";

                var dt = repo.ExecuteSelectQuery(sql, new Dictionary<string, object> { { "@tranId", tranId } });

                if (dt.Rows.Count > 0)
                {
                    var rowData = dt.Rows[0];

                    BindInPayBy();

                    GetReceiptChk.Checked = Convert.ToBoolean(rowData["checkReceipt"]);
                    CheckShort.Checked = Convert.ToBoolean(rowData["checkShort"]);

                    string memType = rowData["memberType"].ToString();
                    DropDownList2.SelectedValue = memType;

                    numMemBox.Text = rowData["payNoMember"].ToString();
                    PayDuration.Text = rowData["payDuration"].ToString();
                    paymentDateBox.Value = rowData["paymentDate"].ToString();
                    EffectiveDateInput.Value = rowData["effectiveDate"].ToString();
                    ExpiredDate.Text = rowData["expireDate"].ToString();

                    DropDownList3.SelectedValue = rowData["payMethod"].ToString();

                    cNoBox.Value = rowData["receiptNo"].ToString();
                    bankCodeBox.Value = rowData["bankcode"].ToString();
                    bankPanel.Value = rowData["accno"].ToString();
                    remarkBox.Value = rowData["payRemark"].ToString();
                    entranceVal.Text = rowData["entranceFee"].ToString();
                    annualVal.Text = rowData["newsletterFee"].ToString();

                    int num1 = int.Parse(entranceVal.Text);
                    int num2 = int.Parse(annualVal.Text);
                    totolSum.Text = (num1 + num2).ToString();

                    if (num1 > 0 && num2 > 0) PaymentType.SelectedValue = "Both";
                    else if (num1 > 0) PaymentType.SelectedValue = "Entrance Fee";
                    else if (num2 > 0) PaymentType.SelectedValue = "Annual Fee";

                    int payDurationChk = int.Parse(PayDuration.Text);
                    if (payDurationChk >= 12) RadioButtonList1.SelectedValue = "12";
                    else if (payDurationChk >= 6) RadioButtonList1.SelectedValue = "6";
                    else RadioButtonList1.SelectedValue = "1";
                }
            }
            catch (Exception ex)
            {
                // Log error ถ้าต้องการ
            }
        }





        /* commentted to used new refactor
        protected void BindInPayBy()
        {
            DataTable td;

            td = SelectSqlTable("SELECT DISTINCT  t1.nameE " +
                                "FROM PrivateDetail t1 " +
                                "INNER JOIN PrivatePayment t2 ON t1.firstmemberid = t2.payBy WHERE t2.memberid = " + "'" + showfristMem + "'");
            //GridView1.Columns[0].Visible = false;


            DropDownList1.DataSource = td;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "nameE";
            DropDownList1.DataValueField = "nameE";
            DropDownList1.DataBind();

            conn.Close();
        }
        */
        protected void BindInPayBy()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = @"
        SELECT DISTINCT t1.nameE
        FROM PrivateDetail t1
        INNER JOIN PrivatePayment t2 ON t1.firstmemberid = t2.payBy
        WHERE t2.memberid = @firstMemberId";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            DataTable td = paymentRepo.ExecuteSelectQuery(sql, parameters);

            DropDownList1.DataSource = td;
            DropDownList1.DataTextField = "nameE";
            DropDownList1.DataValueField = "nameE";
            DropDownList1.DataBind();
        }





        /* commentted to used new refactor
        protected void BindInBankAccount()
        {
            DataTable td;

            td = SelectSqlTable("SELECT CONCAT(accId, ':', accno, ':', bankcode, ':', payAccDuration) AS BankAccount " +
                                "FROM PrivateAccount " +
                                "WHERE memberid = " + "'" + showfristMem + "'");
            //GridView1.Columns[0].Visible = false;


            DropDownList4.DataSource = td;
            DropDownList4.DataBind();
            DropDownList4.DataTextField = "BankAccount";
            DropDownList4.DataValueField = "BankAccount";
            DropDownList4.DataBind();

            conn.Close();
        }
        */

        protected void BindInBankAccount()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            PrivatePaymentRepository paymentRepo = new PrivatePaymentRepository(connStr);

            string sql = "SELECT CONCAT(accId, ':', accno, ':', bankcode, ':', payAccDuration) AS BankAccount " +
                         "FROM PrivateAccount WHERE memberid = @firstMemberId";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            DataTable td = paymentRepo.ExecuteSelectQuery(sql, parameters);

            DropDownList4.DataSource = td;
            DropDownList4.DataTextField = "BankAccount";
            DropDownList4.DataValueField = "BankAccount";
            DropDownList4.DataBind();
        }







        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            //DisabledForm();
            Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            Response.Redirect("privateEntryPayment.aspx?mode=add&" + "firstmemberid=" + showfristMem);
        }





        /* commentted to used new refactor
        protected void GridView1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
			//string tranId = e.CommandArgument.ToString();
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string tranId = commandArgs[0];
            string payMethod = commandArgs[1];
            string accid = commandArgs[2];
            if (e.CommandName == "delete")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    connection();

                    // *** 2024-09-06 10.22am : Toon Jiradech.k have changed code from hard deleting to soft deleting
                    #region 'Hard Deleting'
                    //cmd = new SqlCommand("DELETE FROM PrivatePayment WHERE tranId = '" + tranId + "'", conn);
                    #endregion

                    #region 'Soft Deleting'
                    cmd = new SqlCommand($"UPDATE PrivatePayment SET Deleted_at = GETDATE() " +
                                         $"WHERE tranId = '{tranId}'", conn);
                    #endregion
                    // *** End of revise

                    conn.Open();
                    try
                    {
						cmd.ExecuteNonQuery();
						string activityDetail = $"Soft deleted data in a table 'PrivatePayment' where tranId is '{tranId}' successful (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
                    catch (SqlException ex)
                    {
                        //string activityDetail = $"Soft deleted data in a table 'PrivatePayment' where tranId is '{tranId}' unsuccessful [{ex.Message}] (user id = {staffID})";
                        logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                                                              $"{ex.Message}");
                    }
					catch (Exception ex)
					{
                        //string activityDetail = $"Soft deleted data in a table 'PrivatePayment' where tranId is '{tranId}' unsuccessful [{ex.Message}] (user id = {staffID})";
                        logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
                    }

					conn.Close();
                    // *** 2024-09-06 02.53pm : Too Jiradech.K have revised code 
                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    this.showInGrid();
                    // *** End of Revised
                }
            }
            else if (e.CommandName == "edit")
            {
                connection();
                SqlCommand sc;
                SqlDataReader rd;
                string sql = "";

                if (payMethod == "B" || payMethod == "T" || payMethod == "S")
                {
                    sql = "select pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember,payDuration,FORMAT(paymentDate, 'dd/MM/yyyy'), " +
                          "FORMAT(effectiveDate, 'dd/MM/yyyy'),FORMAT(expireDate, 'dd/MM/yyyy'),payMethod,payRemark,receiptNo,checkReceipt,entranceFee, " +
                          "newsletterFee,pp.tranId ,checkShort ,FORMAT(shortFrom, 'dd/MM/yyyy'),FORMAT(shortTo, 'dd/MM/yyyy') ,pa.accno,pa.bankcode,pa.payAccDuration,ppa.accId,pp.payat " +
                          "from PrivatePayment pp " +
                          "left outer join privateDetail d on pp.payBy = d.memberId " +
                          "left outer join privatePayAccount ppa on ppa.tranId = pp.tranId " +
                          "left outer join privateAccount pa on pa.memberid = pp.memberid and pa.accId = ppa.accId " +
                          "left outer join PrivatePayShort pps on pp.tranId = pps.tranId " +
                          //"where payBy = '" + showfirstMem + "' and pp.tranId = '" + tranId + "' ";
                          "where pp.tranId = '" + tranId + "' "; //update
                    //sql = "select pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember,payDuration,paymentDate, " +
                    //      "effectiveDate,expireDate,payMethod,payRemark,receiptNo,checkReceipt,entranceFee, " +
                    //      "newsletterFee,pp.tranId ,checkShort ,pa.accno,pa.bankcode,pa.payAccDuration,ppa.accId " +
                    //      "from PrivatePayment pp " +
                    //      "left outer join privateDetail d on pp.payBy = d.memberId " +
                    //      "left outer join privatePayAccount ppa on ppa.tranId = pp.tranId " +
                    //      "left outer join privateAccount pa on pa.memberid = pp.memberid and pa.accId = ppa.accId " +
                    //      "where pp.memberid = '" + showfirstMem + "' and pp.tranId = '" + tranId + "' ";
                }
                else
                {
                    sql = "select pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember,payDuration,FORMAT(paymentDate, 'dd/MM/yyyy'), " +
                         "FORMAT(effectiveDate, 'dd/MM/yyyy'),FORMAT(expireDate, 'dd/MM/yyyy'),payMethod,payRemark,receiptNo,checkReceipt,entranceFee, " +
                         "newsletterFee,pp.tranId ,checkShort,FORMAT(shortFrom, 'dd/MM/yyyy'),FORMAT(shortTo, 'dd/MM/yyyy'),'','','','',pp.payat " +
                         "from PrivatePayment pp " +
                         "inner join PrivateDetail pd on pp.memberid = pd.memberid " +
                         "left outer join PrivatePayShort pps on pp.tranId = pps.tranId " +
                         //"where payBy = '" + showfirstMem + "' and pp.tranId = '" + tranId + "' ";
                         "where pp.tranId = '" + tranId + "' ";
                    //sql = "select pp.memberId,(prefixNm + ' '+ nameE) as nameEng, pp.memberType,payNoMember,payDuration,paymentDate, " +
                    //     "effectiveDate,expireDate,payMethod,payRemark,receiptNo,checkReceipt,entranceFee,newsletterFee,pp.tranId ,checkShort " +
                    //     "from PrivatePayment pp " +
                    //     "inner join PrivateDetail pd on pp.memberid = pd.memberid " +
                    //     "where firstmemberid = '" + showfirstMem + "' and pp.tranId = '" + tranId + "' ";
                }


                try
                {
                    conn.Open();
                    sc = new SqlCommand(sql, conn);
                    sc.CommandTimeout = 480;
                    rd = sc.ExecuteReader();

                    while (rd.Read())
                    {
                        //show in member information
                        DropDownList1.SelectedItem.Text = rd.GetValue(1).ToString();
                        DropDownList2.SelectedValue = rd.GetValue(2).ToString();
                        numMemBox.Text = rd.GetValue(3).ToString();
                        PayDuration.Text = rd.GetValue(4).ToString();
                        paymentDateBox.Value = rd.GetValue(5).ToString();
                        EffectiveDateInput.Value = rd.GetValue(6).ToString();
                        ExpiredDate.Text = rd.GetValue(7).ToString();
                        HiddenExpiredDate.Value = rd.GetValue(7).ToString();
                        //paymentDateBox.Value = ((DateTime)rd.GetValue(5)).ToString("").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                        //EffectiveDateInput.Value = ((DateTime)rd.GetValue(6)).ToString("").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                        //ExpiredDate.Value = ((DateTime)rd.GetValue(7)).ToString("").ToString().Replace("1/1/2443", " ").Split(' ')[0];
                        string paymethodcho = rd.GetValue(8).ToString();
                        if (paymethodcho == "")
                        {
                            paymethodcho = "-- Any --";
                        }
                        DropDownList3.SelectedValue = paymethodcho;
                        remarkBox.Value = rd.GetValue(9).ToString();
                        cNoBox.Value = rd.GetValue(10).ToString();
                        //GetReceiptChk.Checked = rd.GetValue(11).ToString();
                        bool ch2 = (bool)rd.GetValue(11);
                        GetReceiptChk.Checked = ch2;
                        entranceVal.Text = rd.GetValue(12).ToString();
                        annualVal.Text = rd.GetValue(13).ToString();

                        Label4.Text = rd.GetValue(14).ToString();
                        HiddenTranId.Value = rd.GetValue(14).ToString();
                        bool ch1 = (bool)rd.GetValue(15);
                        CheckShort.Checked = ch1;
                        ShortFromBox.Value = rd.GetValue(16).ToString();
                        ShortToBox.Value = rd.GetValue(17).ToString();
                        if (rd.GetValue(22).ToString() == "")
                        {
                            ddpayat.SelectedIndex = 0;
                        }
                        else
                        {
                            ddpayat.SelectedValue = rd.GetValue(22).ToString();
                        }
                        if (DropDownList3.SelectedValue == "B" || DropDownList3.SelectedValue == "T" || DropDownList3.SelectedValue == "S")
                        {
                            DropDownList4.Attributes.Remove("disabled");
                            bankPanel.Attributes.Remove("disabled");
                            bankCodeBox.Attributes.Remove("disabled");
                            bankPanel.Value = rd.GetValue(18).ToString();
                            bankCodeBox.Value = rd.GetValue(19).ToString();
                            if (rd.GetValue(20).ToString() != "")
                            {
                                RadioButtonList1.SelectedValue = rd.GetValue(20).ToString();
                            }
                            DropDownList4.SelectedValue = rd.GetValue(21).ToString();
                            if (GetReceiptChk.Checked == true)
                            {
                                cNoBox.Attributes.Remove("disabled");
                            }

                            if (CheckShort.Checked == true)
                            {
                                ShortFromBox.Attributes.Remove("disabled");
                                ShortToBox.Attributes.Remove("disabled");
                            }
                        }
                        else
                        {
                            DropDownList4.Attributes.Add("disabled", "disabled");
                            bankPanel.Attributes.Add("disabled", "disabled");
                            bankCodeBox.Attributes.Add("disabled", "disabled");
                            bankPanel.Value = "";
                            bankCodeBox.Value = "";
                            RadioButtonList1.ClearSelection();
                            DropDownList4.SelectedIndex = 0;
                            RadioButtonList1.Enabled = false;

                            if (GetReceiptChk.Checked == true)
                            {
                                cNoBox.Attributes.Remove("disabled");
                            }

                            if (CheckShort.Checked == true)
                            {
                                ShortFromBox.Attributes.Remove("disabled");
                                ShortToBox.Attributes.Remove("disabled");
                            }

                        }
                    }

                }
                catch { }

                EnabledFormUpdate();

                conn.Close();
            }
        }
        */
        protected void GridView1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            string[] commandArgs = e.CommandArgument.ToString().Split(',');
            string tranId = commandArgs[0];
            string payMethod = commandArgs[1];
            string accid = commandArgs[2];

            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var repo = new PrivatePaymentRepository(connStr);

            if (e.CommandName == "delete")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    try
                    {
                        string sql = "UPDATE PrivatePayment SET Deleted_at = GETDATE() WHERE tranId=@tranId";
                        repo.ExecuteNonQuery(sql, new Dictionary<string, object> { { "@tranId", tranId } });

                        string activityDetail = $"Soft deleted data in PrivatePayment where tranId='{tranId}' (user id={staffID})";
                        logActivity.LogStaffActivity(staffID, activityDetail);

                        this.showInGrid();
                    }
                    catch (Exception ex)
                    {
                        logActivity.LogStaffActivity(staffID, $"ERROR deleting tranId={tranId}: {ex.Message}");
                    }
                }
            }
            else if (e.CommandName == "edit")
            {
                try
                {
                    string sql = @"SELECT pp.memberId, (prefixNm + ' ' + nameE) as nameEng, pp.memberType, payNoMember, payDuration,
                                  FORMAT(paymentDate,'dd/MM/yyyy') as paymentDate, FORMAT(effectiveDate,'dd/MM/yyyy') as effectiveDate,
                                  FORMAT(expireDate,'dd/MM/yyyy') as expireDate, payMethod, payRemark, receiptNo, checkReceipt,
                                  entranceFee, newsletterFee, pp.tranId, checkShort, FORMAT(shortFrom,'dd/MM/yyyy') as shortFrom,
                                  FORMAT(shortTo,'dd/MM/yyyy') as shortTo, pa.accno, pa.bankcode, pa.payAccDuration,
                                  ppa.accId, pp.payat
                           FROM PrivatePayment pp
                           LEFT JOIN privateDetail d ON pp.payBy = d.memberId
                           LEFT JOIN privatePayAccount ppa ON ppa.tranId = pp.tranId
                           LEFT JOIN privateAccount pa ON pa.memberid = pp.memberid AND pa.accId = ppa.accId
                           LEFT JOIN PrivatePayShort pps ON pp.tranId = pps.tranId
                           WHERE pp.tranId=@tranId";

                    var dt = repo.ExecuteSelectQuery(sql, new Dictionary<string, object> { { "@tranId", tranId } });

                    if (dt.Rows.Count > 0)
                    {
                        var rd = dt.Rows[0];

                        DropDownList1.SelectedItem.Text = rd["nameEng"].ToString();
                        DropDownList2.SelectedValue = rd["memberType"].ToString();
                        numMemBox.Text = rd["payNoMember"].ToString();
                        PayDuration.Text = rd["payDuration"].ToString();
                        paymentDateBox.Value = rd["paymentDate"].ToString();
                        EffectiveDateInput.Value = rd["effectiveDate"].ToString();
                        ExpiredDate.Text = rd["expireDate"].ToString();
                        HiddenExpiredDate.Value = rd["expireDate"].ToString();
                        DropDownList3.SelectedValue = string.IsNullOrEmpty(rd["payMethod"].ToString()) ? "-- Any --" : rd["payMethod"].ToString();
                        remarkBox.Value = rd["payRemark"].ToString();
                        cNoBox.Value = rd["receiptNo"].ToString();
                        GetReceiptChk.Checked = Convert.ToBoolean(rd["checkReceipt"]);
                        entranceVal.Text = rd["entranceFee"].ToString();
                        annualVal.Text = rd["newsletterFee"].ToString();
                        Label4.Text = rd["tranId"].ToString();
                        HiddenTranId.Value = rd["tranId"].ToString();
                        CheckShort.Checked = Convert.ToBoolean(rd["checkShort"]);
                        ShortFromBox.Value = rd["shortFrom"].ToString();
                        ShortToBox.Value = rd["shortTo"].ToString();
                        ddpayat.SelectedValue = string.IsNullOrEmpty(rd["payat"].ToString()) ? "0" : rd["payat"].ToString();

                        if (DropDownList3.SelectedValue == "B" || DropDownList3.SelectedValue == "T" || DropDownList3.SelectedValue == "S")
                        {
                            DropDownList4.Attributes.Remove("disabled");
                            bankPanel.Attributes.Remove("disabled");
                            bankCodeBox.Attributes.Remove("disabled");
                            bankPanel.Value = rd["accno"].ToString();
                            bankCodeBox.Value = rd["bankcode"].ToString();
                            if (rd["payAccDuration"].ToString() != "")
                                RadioButtonList1.SelectedValue = rd["payAccDuration"].ToString();
                            DropDownList4.SelectedValue = rd["accId"].ToString();

                            if (GetReceiptChk.Checked)
                                cNoBox.Attributes.Remove("disabled");
                            if (CheckShort.Checked)
                            {
                                ShortFromBox.Attributes.Remove("disabled");
                                ShortToBox.Attributes.Remove("disabled");
                            }
                        }
                        else
                        {
                            DropDownList4.Attributes.Add("disabled", "disabled");
                            bankPanel.Attributes.Add("disabled", "disabled");
                            bankCodeBox.Attributes.Add("disabled", "disabled");
                            bankPanel.Value = "";
                            bankCodeBox.Value = "";
                            RadioButtonList1.ClearSelection();
                            DropDownList4.SelectedIndex = 0;
                            RadioButtonList1.Enabled = false;

                            if (GetReceiptChk.Checked)
                                cNoBox.Attributes.Remove("disabled");
                            if (CheckShort.Checked)
                            {
                                ShortFromBox.Attributes.Remove("disabled");
                                ShortToBox.Attributes.Remove("disabled");
                            }
                        }
                    }

                    EnabledFormUpdate();
                }
                catch (Exception ex)
                {
                    logActivity.LogStaffActivity(staffID, $"ERROR editing tranId={tranId}: {ex.Message}");
                }
            }
        }










        /* commentted for use new refactor
        protected void saveBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                connection();
                var accNumberTemp = "";
                var bankCodeTemp = "";
                var accidTemp = "";
                SqlCommand sc3;
                SqlDataReader rd3;
                //
                string sql3 = "select accId,accno,bankcode " +
                              "from PrivateAccount " +
                              "where memberid = '" + showfristMem + "' and accno = '" + bankPanel.Value + "' and bankcode = '" + bankCodeBox.Value + "' ";
                try
                {
                    conn.Open();
                    sc3 = new SqlCommand(sql3, conn);
                    sc3.CommandTimeout = 480;
                    rd3 = sc3.ExecuteReader();
                    while (rd3.Read())
                    {
                        accidTemp = rd3.GetValue(0).ToString();
                        accNumberTemp = rd3.GetValue(1).ToString();
                        bankCodeTemp = rd3.GetValue(2).ToString();
                    }
                    conn.Close();
                }
                catch (Exception exA)
                {
                    totolSum.Text = "x";
                }

                if (DropDownList3.SelectedValue == "S" || DropDownList3.SelectedValue == "T" || DropDownList3.SelectedValue == "B")
                {
                    if (bankPanel.Value == accNumberTemp && bankCodeBox.Value == bankCodeTemp) //กรณีที่ bankPanel กับ bankPanel ตรงกับ db 
                    {
                        string status = "C";
                        var accid2 = "";
                        var tranId2 = "";
                        string paymethodcho = DropDownList3.SelectedValue;
                        if (paymethodcho == "-- Any --")
                        {
                            paymethodcho = "";
                        }
                        cmd = new SqlCommand("SET dateformat dmy INSERT INTO PrivatePayment (memberid,     payBy,     memberType,     payMethod,     payRemark,     payDuration,     payNoMember,     receiptNo,     paymentDate,     effectiveDate,     expireDate,     entranceFee,     newsletterFee,     checkShort,     checkReceipt,     updatedDate,     updatedBy,     locked,     lockedBy,     payat,     checkpay )" +
                            " VALUES('" + showfristMem + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + paymethodcho + "'," +
                                             "'" + remarkBox.Value + "','" + PayDuration.Text + "','" + numMemBox.Text + "','" + cNoBox.Value + "','" + paymentDateBox.Value + "','" + EffectiveDateInput.Value + "'," +
                                             "'" + HiddenExpiredDate.Value + "','" + entranceVal.Text + "','" + annualVal.Text + "','" + CheckShort.Checked + "','" + GetReceiptChk.Checked + "',CURRENT_TIMESTAMP," 
                                             + Session["UID"] + ",'','','" + ddpayat.SelectedValue.ToString() + "','" + status + "')", conn);
                        cmd.CommandTimeout = 480;
						conn.Open();
                        try
                        {
							cmd.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayment' successful (User id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
							string activityDetail = $"Added new data into a table 'PrivatePayment' unsuccessful [{ex.Message}] (User id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayment' unsuccessful [{ex.Message}] (User id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}

						conn.Close();

                        SqlCommand sc2;
                        SqlDataReader rd2;

                        string sql2 = "select Top 1 tranId " +
                                      "from PrivatePayment " +
                                      "order by tranId desc ";
                        try
                        {
                            conn.Open();
                            sc2 = new SqlCommand(sql2, conn);
                            sc2.CommandTimeout = 480;
                            rd2 = sc2.ExecuteReader();

                            while (rd2.Read())
                            {
                                tranId2 = rd2.GetValue(0).ToString();
                            }

                        }
                        catch (Exception exB)
                        {
                            totolSum.Text = "x";
                        }

                        conn.Close();

                        string InsertSQL2 = "insert into PrivatePayAccount (tranId, accId) VALUES (@tranId,@accId)";
                        SqlCommand vlozSQL2 = new SqlCommand(InsertSQL2, conn);
                        vlozSQL2.CommandTimeout = 480;
                        conn.Open();
                        vlozSQL2.Parameters.AddWithValue("@tranId", tranId2);
                        vlozSQL2.Parameters.AddWithValue("@accId", DropDownList4.SelectedValue);
                        
                        try
                        {
							vlozSQL2.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						vlozSQL2.Parameters.Clear();
                        conn.Close();

                        string InsertSQL3 = "SET dateformat dmy insert into PrivatePayShort (tranId, shortFrom, shortTo) VALUES (@tranId,@shortFrom,@shortTo)";
                        SqlCommand vlozSQL3 = new SqlCommand(InsertSQL3, conn);
                        vlozSQL3.CommandTimeout = 480;
                        conn.Open();
                        vlozSQL3.Parameters.AddWithValue("@tranId", tranId2);
                        vlozSQL3.Parameters.AddWithValue("@shortFrom", ShortFromBox.Value);
                        vlozSQL3.Parameters.AddWithValue("@shortTo", ShortToBox.Value);
                        
						try
						{
							vlozSQL3.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						vlozSQL3.Parameters.Clear();
                        conn.Close();

                        Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);

                    }
                    else //กรณีที่ bankPanel กับ bankPanel ไม่ตรงกับ db สร้าง accId ใหม่
                    {
                        connection();
                        var accid = "";
                        var tranId = "";
						string status = "C";
                        string paymethodcho = DropDownList3.SelectedValue;
                        if (paymethodcho == "-- Any --")
                        {
                            paymethodcho = "";
                        }
                        cmd = new SqlCommand("SET dateformat dmy INSERT INTO PrivatePayment (memberid,     payBy,     memberType,     payMethod,     payRemark,     payDuration,     payNoMember,     receiptNo,     paymentDate,     effectiveDate,     expireDate,     entranceFee,     newsletterFee,     checkShort,     checkReceipt,     updatedDate,     updatedBy,     locked,     lockedBy,     payat,     checkpay )" +
                            " VALUES('" + showfristMem + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + paymethodcho + "'," +
                                             "'" + remarkBox.Value + "','" + PayDuration.Text + "','" + numMemBox.Text + "','" + cNoBox.Value + "','" + paymentDateBox.Value + "','" + EffectiveDateInput.Value + "'," +
                                             "'" + HiddenExpiredDate.Value + "','" + entranceVal.Text + "','" + annualVal.Text + "','" + CheckShort.Checked + "','" + GetReceiptChk.Checked + "',CURRENT_TIMESTAMP," 
                                             + Session["UID"] + ",'','','" + ddpayat.SelectedValue.ToString() + "','" + status + "')", conn);
						cmd.CommandTimeout = 480;
						conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayment' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayment' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayment' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();


                        string InsertSQL2 = "INSERT INTO PrivateAccount (memberId, accno,bankcode,payAccDuration) VALUES (@memberId,@accno,@bankcode,@payAccDuration)";
                        SqlCommand vlozSQL2 = new SqlCommand(InsertSQL2, conn);
                        vlozSQL2.CommandTimeout = 480;
                        conn.Open();
                        vlozSQL2.Parameters.AddWithValue("@memberId", showfristMem);
                        vlozSQL2.Parameters.AddWithValue("@accno", bankPanel.Value);
                        vlozSQL2.Parameters.AddWithValue("@bankcode", bankCodeBox.Value);
                        vlozSQL2.Parameters.AddWithValue("@payAccDuration", RadioButtonList1.SelectedValue);
						try
						{
							vlozSQL2.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						vlozSQL2.Parameters.Clear();
                        conn.Close();

                        SqlCommand sc;
                        SqlDataReader rd;

                        string sql = "select top 1 accId " +
                                     "from PrivateAccount " +
                                     "where memberId = '" + showfristMem + "' " +
                                     "order by accId desc";
                        try
                        {
                            conn.Open();
                            sc = new SqlCommand(sql, conn);
                            rd = sc.ExecuteReader();

                            while (rd.Read())
                            {
                                accid = rd.GetValue(0).ToString();
                            }

                        }
                        catch (Exception exC)
                        {
                            totolSum.Text = "x";
                        }

                        conn.Close();

                        SqlCommand sc2;
                        SqlDataReader rd2;

                        string sql2 = "select Top 1 tranId " +
                                      "from PrivatePayment " +
                                      "order by tranId desc ";
                        try
                        {
                            conn.Open();
                            sc2 = new SqlCommand(sql2, conn);
                            rd2 = sc2.ExecuteReader();

                            while (rd2.Read())
                            {
                                tranId = rd2.GetValue(0).ToString();
                            }

                        }
                        catch (Exception exD)
                        {
                            totolSum.Text = "x";
                        }

                        conn.Close();

                        string InsertSQL = "INSERT INTO PrivatePayAccount (tranId, accId) VALUES (@tranId,@accId)";
                        SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                        conn.Open();
                        vlozSQL.Parameters.AddWithValue("@tranId", tranId);
                        vlozSQL.Parameters.AddWithValue("@accId", accid);
                        vlozSQL.CommandTimeout = 480;
						try
						{
							vlozSQL.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						vlozSQL.Parameters.Clear();
                        conn.Close();

                        string InsertSQL3 = "SET dateformat dmy insert into PrivatePayShort (tranId, shortFrom, shortTo) VALUES (@tranId,@shortFrom,@shortTo)";
                        SqlCommand vlozSQL3 = new SqlCommand(InsertSQL3, conn);
                        vlozSQL3.CommandTimeout = 480;
						conn.Open();
                        vlozSQL3.Parameters.AddWithValue("@tranId", tranId);
                        vlozSQL3.Parameters.AddWithValue("@shortFrom", ShortFromBox);
                        vlozSQL3.Parameters.AddWithValue("@shortTo", ShortToBox);
                        
						try
						{
							vlozSQL3.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' successful (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						vlozSQL3.Parameters.Clear();
                        conn.Close();

                        Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
                    }


                }
                else
                {
                    var tranId = "";
                    connection();
                    string paymethodcho = DropDownList3.SelectedValue;
                    string status = "C";
                    if (paymethodcho == "-- Any --")
                    {
                        paymethodcho = "";
                    }
                    cmd = new SqlCommand("SET dateformat dmy INSERT INTO PrivatePayment (memberid,     payBy,     memberType,     payMethod,     payRemark,     payDuration,     payNoMember,     receiptNo,     paymentDate,     effectiveDate,     expireDate,     entranceFee,     newsletterFee,     checkShort,     checkReceipt,     updatedDate,     updatedBy,     locked,     lockedBy,     payat,     checkpay )" +
                        " VALUES('" + showfristMem + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "','" + paymethodcho + "'," +
                                         "'" + remarkBox.Value + "','" + PayDuration.Text + "','" + numMemBox.Text + "','" + cNoBox.Value + "','" + paymentDateBox.Value + "','" + EffectiveDateInput.Value + "'," +
                                         "'" + HiddenExpiredDate.Value + "','" + entranceVal.Text + "','" + annualVal.Text + "','" + CheckShort.Checked + "','" + GetReceiptChk.Checked + "',CURRENT_TIMESTAMP," 
                                         + Session["UID"] + ",'','','" + ddpayat.SelectedValue.ToString() + "','" + status + "')", conn);
                    cmd.CommandTimeout = 480;
					conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						string activityDetail = $"Added new data into a table 'PrivatePayment' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Added new data into a table 'PrivatePayment' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Added new data into a table 'PrivatePayment' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					conn.Close();

                    SqlCommand sc2;
                    SqlDataReader rd2;

                    string sql2 = "select Top 1 tranId " +
                                  "from PrivatePayment " +
                                  "order by tranId desc ";
                    try
                    {
                        conn.Open();
                        sc2 = new SqlCommand(sql2, conn);
                        sc2.CommandTimeout = 480;
						rd2 = sc2.ExecuteReader();

                        while (rd2.Read())
                        {
                            tranId = rd2.GetValue(0).ToString();
                        }

                    }
                    catch (Exception exE)
                    {
                        totolSum.Text = "x";
                    }
                    conn.Close();

                    string InsertSQL3 = "SET dateformat dmy insert into PrivatePayShort (tranId, shortFrom, shortTo) VALUES (@tranId,@shortFrom,@shortTo)";
                    SqlCommand vlozSQL3 = new SqlCommand(InsertSQL3, conn);
                    conn.Open();
                    vlozSQL3.CommandTimeout = 480;
					vlozSQL3.Parameters.AddWithValue("@tranId", tranId);
                    vlozSQL3.Parameters.AddWithValue("@shortFrom", ShortFromBox.Value);
                    vlozSQL3.Parameters.AddWithValue("@shortTo", ShortToBox.Value);
					try
					{
						vlozSQL3.ExecuteNonQuery();
						string activityDetail = $"Added new data into a table 'PrivatePayAccount' successful (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Added new data into a table 'PrivatePayAccount' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}

					vlozSQL3.Parameters.Clear();
                    conn.Close();

                    //cmd = new SqlCommand("INSERT INTO PrivatePayment VALUES('" + showfirstMem + "','" + DropDownList1.SelectedValue + "','" + DropDownList2.SelectedValue + "' )", conn);

                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();

                    Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
                }
            }
            else
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }


        }
        */

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue != "Yes")
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var repo = new PrivatePaymentRepository(connStr);

            try
            {
                // 1️⃣ ตรวจสอบ Bank Account เก่า/ใหม่
                string sqlCheckAcc = @"SELECT accId 
                               FROM PrivateAccount 
                               WHERE memberid = @memberid AND accno = @accno AND bankcode = @bankcode";

                var dtAccCheck = repo.ExecuteSelectQuery(sqlCheckAcc, new Dictionary<string, object>
        {
            { "@memberid", showfristMem },
            { "@accno", bankPanel.Value },
            { "@bankcode", bankCodeBox.Value }
        });

                string accIdToUse;

                if (dtAccCheck.Rows.Count > 0)
                {
                    // Bank Account มีอยู่แล้ว
                    accIdToUse = dtAccCheck.Rows[0]["accId"].ToString();
                }
                else
                {
                    // Bank Account ใหม่ -> insert ลง PrivateAccount
                    string insertAccSql = @"INSERT INTO PrivateAccount (memberId, accno, bankcode, payAccDuration) 
                                    VALUES (@memberId, @accno, @bankcode, @payAccDuration);
                                    SELECT SCOPE_IDENTITY();";

                    var dtNewAcc = repo.ExecuteSelectQuery(insertAccSql, new Dictionary<string, object>
            {
                { "@memberId", showfristMem },
                { "@accno", bankPanel.Value },
                { "@bankcode", bankCodeBox.Value },
                { "@payAccDuration", RadioButtonList1.SelectedValue }
            });

                    accIdToUse = dtNewAcc.Rows[0][0].ToString();
                    logActivity.LogStaffActivity(staffID, $"Added new Bank Account {bankPanel.Value}/{bankCodeBox.Value} for member {showfristMem}");
                }

                // 2️⃣ Insert PrivatePayment
                var paymentParams = new Dictionary<string, object>
        {
            { "@memberid", showfristMem },
            { "@payBy", DropDownList1.SelectedValue },
            { "@memberType", DropDownList2.SelectedValue },
            { "@payMethod", DropDownList3.SelectedValue == "-- Any --" ? "" : DropDownList3.SelectedValue },
            { "@payRemark", remarkBox.Value },
            { "@payDuration", PayDuration.Text },
            { "@payNoMember", numMemBox.Text },
            { "@receiptNo", cNoBox.Value },
            { "@paymentDate", paymentDateBox.Value },
            { "@effectiveDate", EffectiveDateInput.Value },
            { "@expireDate", HiddenExpiredDate.Value },
            { "@entranceFee", entranceVal.Text },
            { "@newsletterFee", annualVal.Text },
            { "@checkShort", CheckShort.Checked },
            { "@checkReceipt", GetReceiptChk.Checked },
            { "@updatedBy", staffID },
            { "@payat", ddpayat.SelectedValue },
            { "@checkpay", "C" }
        };

                int tranId = repo.InsertPrivatePayment(paymentParams);
                logActivity.LogStaffActivity(staffID, $"Added PrivatePayment for member {showfristMem}, tranId={tranId}");

                // 3️⃣ Insert PrivatePayAccount
                repo.InsertPrivatePayAccount(tranId, accIdToUse);
                logActivity.LogStaffActivity(staffID, $"Linked tranId={tranId} to accId={accIdToUse} in PrivatePayAccount");

                // 4️⃣ Insert PrivatePayShort
                repo.InsertPrivatePayShort(tranId, ShortFromBox.Value, ShortToBox.Value);
                logActivity.LogStaffActivity(staffID, $"Added PrivatePayShort for tranId={tranId} ({ShortFromBox.Value} -> {ShortToBox.Value})");

                // 5️⃣ Redirect
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
            }
            catch (Exception ex)
            {
                totolSum.Text = "x";
                logActivity.LogStaffActivity(staffID, $"saveBtn_Click failed: {ex.Message}");
            }
        }





        /* commentted to used new refactor
        protected void update_Click(object sender, EventArgs e)
        {

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                connection();
                var accNumberTemp = "";
                var bankCodeTemp = "";
                var accidTemp = "";
                SqlCommand sc3;
                SqlDataReader rd3;
                //
                string sql3 = "select accId,accno,bankcode " +
                              "from PrivateAccount " +
                              "where memberid = '" + showfristMem + "' and accno = '" + bankPanel.Value + "' and bankcode = '" + bankCodeBox.Value + "' ";
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

                // payBy_ Update
                try
                {
                    //14FEB2022
                    DataTable payBy_Val = SelectSqlTable("SELECT memberId, (prefixNm + ' '+ nameE) AS nameEng FROM privateDetail  WHERE firstmemberid = '" + showfristMem + "' AND (prefixNm + ' '+ nameE) = '" + DropDownList1.SelectedItem + "'");
                    cmd = new SqlCommand("update PrivatePayment set payBy = '" + payBy_Val.Rows[0]["memberId"] + "' where tranId = '" + Label4.Text + "' ", conn);
                    conn.Open();

					try
					{
						cmd.ExecuteNonQuery();
						string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' successful (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					conn.Close();
                }
                catch { }

                if (DropDownList3.SelectedValue == "S" || DropDownList3.SelectedValue == "T" || DropDownList3.SelectedValue == "B")
                {
                    if (bankPanel.Value == accNumberTemp && bankCodeBox.Value == bankCodeTemp)
                    {
                        connection();
                        cmd = new SqlCommand("SET dateformat dmy update PrivatePayment set memberType = '" + DropDownList2.SelectedValue + "', payNoMember = '" + numMemBox.Text + "', payDuration='" + PayDuration.Text + "', paymentDate='" + paymentDateBox.Value + "', " +
                                            "effectiveDate='" + EffectiveDateInput.Value + "', expireDate='" + HiddenExpiredDate.Value + "', payMethod='" + DropDownList3.SelectedValue + "', payRemark='" + remarkBox.Value + "', receiptNo='" + cNoBox.Value + "', " +
                                            "checkReceipt='" + GetReceiptChk.Checked + "', entranceFee='" + entranceVal.Text + "', newsletterFee='" + annualVal.Text + "', checkShort='" + CheckShort.Checked + "',updatedDate = '" + toDayDate + "',updatedBy = " + Session["UID"] + ", payat = '" + ddpayat.SelectedValue.ToString() + "'" + " " +
                                            "where tranId = '" + Label4.Text + "' ", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();

                        cmd = new SqlCommand("SET dateformat dmy update PrivatePayAccount set accId = '" + DropDownList4.SelectedValue + "' where tranId = '" + Label4.Text + "' ", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Changed data into a table 'PrivatePayAccount' where tranId is '{Label4.Text}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayAccount' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayAccount' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();

                        cmd = new SqlCommand("SET dateformat dmy BEGIN " +
                                                 "IF NOT EXISTS(SELECT * FROM PrivatePayShort " +
                                                                "WHERE tranId = '" + Label4.Text + "') " +
                                                 "BEGIN " +
                                                    "INSERT INTO PrivatePayShort(tranId, shortFrom, shortTo) " +
                                                    "VALUES('" + Label4.Text + "', '" + ShortFromBox.Value + "', '" + ShortToBox.Value + "') " +
                                                 "END " +
                                                 "else " +
                                                 "BEGIN " +
                                                    "update PrivatePayShort set shortFrom = '" + ShortFromBox.Value + "', shortTo = '" + ShortToBox.Value + "' where tranId = '" + Label4.Text + "' " +
                                                 "END " +
                                             "END ", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivatePayShort' and Changed data in a table 'PrivatePayShort' where tranId is '{Label4.Text}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayShort' and Changed data in a table 'PrivatePayShort' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivatePayShort' and Changed data in a table 'PrivatePayShort' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();

                        Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);

                    }
                    else
                    {
                        var accid = "";
                        var tranId = "";
                        connection();
                        cmd = new SqlCommand("SET dateformat dmy update PrivatePayment set memberType = '" + DropDownList2.SelectedValue + "', payNoMember = '" + numMemBox.Text + "', payDuration='" + PayDuration.Text + "', paymentDate='" + paymentDateBox.Value + "', " +
                                            "effectiveDate='" + EffectiveDateInput.Value + "', expireDate='" + HiddenExpiredDate.Value + "', payMethod='" + DropDownList3.SelectedValue + "', payRemark='" + remarkBox.Value + "', receiptNo='" + cNoBox.Value + "', " +
                                            "checkReceipt='" + GetReceiptChk.Checked + "', entranceFee='" + entranceVal.Text + "', newsletterFee='" + annualVal.Text + "', checkShort='" + CheckShort.Checked + "',updatedDate = '" + toDayDate + "',updatedBy = " + Session["UID"] + ", payat = '" + ddpayat.SelectedValue.ToString() + "'" + " " +
                                            "where tranId = '" + Label4.Text + "' ", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();

                        string InsertSQL2 = "INSERT INTO PrivateAccount (memberId, accno,bankcode,payAccDuration) VALUES (@memberId,@accno,@bankcode,@payAccDuration)";
                        SqlCommand vlozSQL2 = new SqlCommand(InsertSQL2, conn);
                        conn.Open();
                        vlozSQL2.Parameters.AddWithValue("@memberId", showfristMem);
                        vlozSQL2.Parameters.AddWithValue("@accno", bankPanel.Value);
                        vlozSQL2.Parameters.AddWithValue("@bankcode", bankCodeBox.Value);
                        vlozSQL2.Parameters.AddWithValue("@payAccDuration", RadioButtonList1.SelectedValue);
                        
						try
						{
							vlozSQL2.ExecuteNonQuery();
							string activityDetail = $"Added new data into a table 'PrivateAccount' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Added new data into a table 'PrivateAccount' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Added new data into a table 'PrivateAccount' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						vlozSQL2.Parameters.Clear();
                        conn.Close();

                        SqlCommand sc;
                        SqlDataReader rd;

                        string sql = "select top 1 accId " +
                                     "from PrivateAccount " +
                                     "where memberId = '" + showfristMem + "' " +
                                     "order by accId desc";
                        try
                        {
                            conn.Open();
                            sc = new SqlCommand(sql, conn);
                            rd = sc.ExecuteReader();

                            while (rd.Read())
                            {
                                accid = rd.GetValue(0).ToString();
                            }

                        }
                        catch { }

                        conn.Close();

                        SqlCommand sc2;
                        SqlDataReader rd2;

                        string sql2 = "select Top 1 tranId " +
                                      "from PrivatePayment " +
                                      "order by tranId desc ";
                        try
                        {
                            conn.Open();
                            sc2 = new SqlCommand(sql2, conn);
                            rd2 = sc2.ExecuteReader();

                            while (rd2.Read())
                            {
                                tranId = rd2.GetValue(0).ToString();
                            }

                        }
                        catch { }

                        conn.Close();

                        cmd = new SqlCommand("update PrivatePayAccount set accId = '" + accid + "' where tranId = '" + Label4.Text + "' ", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Changed data into a table 'PrivatePayAccount' where tranId is '{Label4.Text}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayAccount' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayAccount' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();

                        cmd = new SqlCommand("SET dateformat dmy update PrivatePayShort set shortFrom = '" + ShortFromBox.Value + "' , shortTo = '" + ShortToBox.Value + "' where tranId = '" + Label4.Text + "' ", conn);
                        conn.Open();
						try
						{
							cmd.ExecuteNonQuery();
							string activityDetail = $"Changed data into a table 'PrivatePayShort' where tranId is '{Label4.Text}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (SqlException ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayShort' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Changed data into a table 'PrivatePayShort' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						conn.Close();

                        Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
                    }
                }
                else
                {
                    string paymethodcho = DropDownList3.SelectedValue;
                    if (paymethodcho == "-- Any --")
                    {
                        paymethodcho = "";
                    }
                    connection();
                    cmd = new SqlCommand("SET dateformat dmy update PrivatePayment set memberType = '" + DropDownList2.SelectedValue + "', payNoMember = '" + numMemBox.Text + "', payDuration='" + PayDuration.Text + "', paymentDate='" + paymentDateBox.Value + "', " +
                                        "effectiveDate='" + EffectiveDateInput.Value + "', expireDate='" + HiddenExpiredDate.Value + "', payMethod='" + paymethodcho + "', payRemark='" + remarkBox.Value + "', receiptNo='" + cNoBox.Value + "', " +
                                        "checkReceipt='" + GetReceiptChk.Checked + "', entranceFee='" + entranceVal.Text + "', newsletterFee='" + annualVal.Text + "', checkShort='" + CheckShort.Checked + "',updatedDate = '" + toDayDate + "',updatedBy = " + Session["UID"] + ", payat = '" + ddpayat.SelectedValue.ToString() + "'" + " " +
                                        "where tranId = '" + Label4.Text + "' ", conn);
                    //cmd = new SqlCommand("update PrivatePayment set payRemark = '" + remarkBox.Value + "' " +
                    //                    "where memberid = '" + showfirstMem + "' and tranId = '" + Label4.Text + "' ", conn);
                    conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' successful (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Changed data into a table 'PrivatePayment' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					conn.Close();

                    cmd = new SqlCommand("SET dateformat dmy update PrivatePayShort set shortFrom = '" + ShortFromBox.Value + "' , shortTo = '" + ShortToBox.Value + "' where tranId = '" + Label4.Text + "' ", conn);
                    conn.Open();
					try
					{
						cmd.ExecuteNonQuery();
						string activityDetail = $"Changed data into a table 'PrivatePayShort' where tranId is '{Label4.Text}' successful (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Changed data into a table 'PrivatePayShort' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Changed data into a table 'PrivatePayShort' where tranId is '{Label4.Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					conn.Close();

                    Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
                }
            }
            else
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
            }

        }
        */



        protected void update_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue != "Yes")
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var repo = new PrivatePaymentRepository(connStr);

            try
            {
                // 1️⃣ ตรวจสอบ Bank Account เก่า/ใหม่
                string sqlCheckAcc = @"SELECT accId FROM PrivateAccount 
                               WHERE memberid=@memberid AND accno=@accno AND bankcode=@bankcode";

                var dtAccCheck = repo.ExecuteSelectQuery(sqlCheckAcc, new Dictionary<string, object>
        {
            { "@memberid", showfristMem },
            { "@accno", bankPanel.Value },
            { "@bankcode", bankCodeBox.Value }
        });

                string accIdToUse;

                if (dtAccCheck.Rows.Count > 0)
                {
                    // Bank Account มีอยู่แล้ว
                    accIdToUse = dtAccCheck.Rows[0]["accId"].ToString();
                }
                else
                {
                    // Bank Account ใหม่ -> insert ลง PrivateAccount
                    string insertAccSql = @"INSERT INTO PrivateAccount (memberId, accno, bankcode, payAccDuration) 
                                    VALUES (@memberId, @accno, @bankcode, @payAccDuration);
                                    SELECT SCOPE_IDENTITY();";

                    var dtNewAcc = repo.ExecuteSelectQuery(insertAccSql, new Dictionary<string, object>
            {
                { "@memberId", showfristMem },
                { "@accno", bankPanel.Value },
                { "@bankcode", bankCodeBox.Value },
                { "@payAccDuration", RadioButtonList1.SelectedValue }
            });

                    accIdToUse = dtNewAcc.Rows[0][0].ToString();
                    logActivity.LogStaffActivity(staffID, $"Added new Bank Account {bankPanel.Value}/{bankCodeBox.Value} for member {showfristMem}");
                }

                // 2️⃣ Update PrivatePayment
                var updatePaymentParams = new Dictionary<string, object>
        {
            { "@tranId", Label4.Text },
            { "@memberType", DropDownList2.SelectedValue },
            { "@payNoMember", numMemBox.Text },
            { "@payDuration", PayDuration.Text },
            { "@paymentDate", paymentDateBox.Value },
            { "@effectiveDate", EffectiveDateInput.Value },
            { "@expireDate", HiddenExpiredDate.Value },
            { "@payMethod", DropDownList3.SelectedValue == "-- Any --" ? "" : DropDownList3.SelectedValue },
            { "@payRemark", remarkBox.Value },
            { "@receiptNo", cNoBox.Value },
            { "@checkReceipt", GetReceiptChk.Checked },
            { "@entranceFee", entranceVal.Text },
            { "@newsletterFee", annualVal.Text },
            { "@checkShort", CheckShort.Checked },
            { "@updatedBy", staffID },
            { "@payat", ddpayat.SelectedValue }
        };
                repo.UpdatePrivatePayment(updatePaymentParams);
                logActivity.LogStaffActivity(staffID, $"Updated PrivatePayment tranId={Label4.Text}");

                // 3️⃣ Update PrivatePayAccount
                repo.UpdatePrivatePayAccount(Label4.Text, accIdToUse);
                logActivity.LogStaffActivity(staffID, $"Updated PrivatePayAccount tranId={Label4.Text} accId={accIdToUse}");

                // 4️⃣ Update PrivatePayShort
                repo.UpdatePrivatePayShort(Label4.Text, ShortFromBox.Value, ShortToBox.Value);
                logActivity.LogStaffActivity(staffID, $"Updated PrivatePayShort tranId={Label4.Text}");

                // 5️⃣ Redirect
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
            }
            catch (Exception ex)
            {
                logActivity.LogStaffActivity(staffID, $"updateBtn_Click failed: {ex.Message}");
            }
        }



        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var val = DropDownList1.SelectedValue;
        //    var val2 = DropDownList1.SelectedItem;
        //    Response.Write("<script>alert('" + val + "')</script>");
        //    Response.Write("<script>alert('" + val2 + "')</script>");

        //}

        protected void DropDownList4_SelectedIndexChanged1(object sender, EventArgs e)
        {
            var val = DropDownList4.SelectedValue;
            var val2 = DropDownList4.SelectedItem;
            Response.Write("<script>alert('" + val + "')</script>");
            Response.Write("<script>alert('" + val2 + "')</script>");
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GetReceiptChk.Checked)
            {
                cNoBox.Disabled = false;
            }
            else
            {
                cNoBox.Disabled = true;
            }

            if (DropDownList3.SelectedValue == "B" || DropDownList3.SelectedValue == "P" || DropDownList3.SelectedValue == "S" || DropDownList3.SelectedValue == "T")
            {
                bankPanel.Attributes.Remove("disabled");
                bankCodeBox.Disabled = false;
                DropDownList4.Attributes.Remove("disabled");

            }
            else
            {
                bankPanel.Attributes.Add("disabled", "disabled");
                bankCodeBox.Disabled = true;
                DropDownList4.Attributes.Add("disabled", "disabled");
                bankPanel.Value = "";
                bankCodeBox.Value = "";
                RadioButtonList1.ClearSelection();
                DropDownList4.SelectedIndex = 0;
            }
            expireDateInit();
        }

        protected void GetReceiptChk_CheckedChanged(object sender, EventArgs e)
        {
            if (GetReceiptChk.Checked)
            {
                cNoBox.Disabled = false;
            }
            else
            {
                cNoBox.Disabled = true;
            }

        }

        protected void CheckShort_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckShort.Checked)
            {
                ShortFromBox.Disabled = false;
                ShortToBox.Disabled = false;
            }
            else
            {
                ShortFromBox.Disabled = true;
                ShortToBox.Disabled = true;
            }

        }

        /* commentted to used new refactor
        protected void calculateFee(object sender, EventArgs e)
        {

            if (GetReceiptChk.Checked == true)
            {
                cNoBox.Attributes.Remove("disabled");
            }


            var entrancetmp = 0;
            var entrancetmp2 = 0;
            var annualtmp = 0;

            System.Data.DataTable memTtypeData = SelectSqlTable("select * from SMemberType where MemberType = '" + DropDownList2.SelectedValue + "' AND EffectiveID = (select TOP(1) EffectiveID from tblEffective where EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate>= floor(cast(getdate() as float)))");


            System.Data.DataTable EntranceFeeStart = SelectSqlTable("select * from sMemberEntranceFee where EffectiveID = (select TOP(1) EffectiveID from tblEffective where EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate>= floor(cast(getdate() as float))) order by EffectiveID");
            if (EntranceFeeStart != null)
            {
                entrancetmp = int.Parse(EntranceFeeStart.Rows[0]["FirstMember"].ToString());
                entrancetmp2 = int.Parse(EntranceFeeStart.Rows[0]["SecondMember"].ToString());
            }





            //if (DropDownList2.SelectedValue == "0" || DropDownList2.SelectedValue == "1" || DropDownList2.SelectedValue == "3A")
            //{

            //    //entrancetmp = 600;
            //    //entrancetmp = EntranceFeeStart.Rows[0][0].ToString;
            //    //entrancetmp = int.Parse(EntranceFeeStart.Rows[0]["FirstMember"].ToString());

            //    if (DropDownList2.SelectedValue != "3A")
            //    {
            //        annualtmp = 200;
            //    }
            //    else
            //    {
            //        annualtmp = 100;
            //    }
            //}
            //else if (DropDownList2.SelectedValue == "2")
            //{
            //    //entrancetmp = 800;
            //    annualtmp = 300;
            //}
            //else if (DropDownList2.SelectedValue == "6A")
            //{
            //    //entrancetmp = 0;
            //    annualtmp = 100;
            //}
            //else if (DropDownList2.SelectedValue == "6B")
            //{
            //    //entrancetmp = 0;
            //    annualtmp = 50;
            //}
            //else if (DropDownList2.SelectedValue == "7")
            //{
            //    //entrancetmp = 200;
            //    annualtmp = 100;
            //}
            //else
            //{
            //    //entrancetmp = 0;
            //    annualtmp = 0;
            //}
            try
            {
                //Entrance Fee funcation & Annual Fee
                var nummem = int.Parse(numMemBox.Text);
                var nummemTotal = int.Parse(memTtypeData.Rows[0]["Newsletter"].ToString());
                var nummon = int.Parse(PayDuration.Text);
                if (nummem <= 1 && DropDownList2.SelectedValue != "2")
                {
                    entrancetmp = int.Parse(EntranceFeeStart.Rows[0]["FirstMember"].ToString());

                    annualtmp = nummemTotal * nummon;
                }
                else if (nummem > 1 && DropDownList2.SelectedValue != "2")
                {

                    var downMem = nummem - 1;
                    entrancetmp = entrancetmp + (downMem * nummemTotal);

                    annualtmp = (nummemTotal + (downMem * 100)) * nummon;
                }

                //2DEC2021
                //memberType2
                if (nummem <= 1 && DropDownList2.SelectedValue == "2")
                {
                    var memtype2Val_1 = int.Parse(EntranceFeeStart.Rows[0]["FirstMember"].ToString());
                    var memtype2Val_2 = int.Parse(EntranceFeeStart.Rows[0]["SecondMember"].ToString());
                    entrancetmp = memtype2Val_1 + memtype2Val_2;

                    annualtmp = nummemTotal * nummon;
                }
                else if (nummem > 1 && DropDownList2.SelectedValue == "2")
                {

                    var downMem = nummem - 1;
                    var memtype2Val_1 = int.Parse(EntranceFeeStart.Rows[0]["FirstMember"].ToString());
                    var memtype2Val_2 = int.Parse(EntranceFeeStart.Rows[0]["SecondMember"].ToString());
                    var memtype2valTotal = memtype2Val_1 + memtype2Val_2;

                    //entrancetmp = entrancetmp + (downMem * nummemTotal);
                    entrancetmp = memtype2valTotal + (downMem * nummemTotal);

                    annualtmp = (nummemTotal + (downMem * 100)) * nummon;
                }

                //memberType6A&&6B
                if (nummem <= 1 && DropDownList2.SelectedValue == "6A" || DropDownList2.SelectedValue == "6B")
                {
                    entrancetmp = 0;

                    annualtmp = nummemTotal * nummon;
                }
                else if (nummem > 1 && DropDownList2.SelectedValue == "6A" || DropDownList2.SelectedValue == "6B")
                {

                    var downMem = nummem - 1;
                    entrancetmp = 0;

                    annualtmp = (nummemTotal + (downMem * 100)) * nummon;
                }

                //memberType7
                if (nummem <= 1 && DropDownList2.SelectedValue == "7")
                {
                    entrancetmp = 200;

                    annualtmp = nummemTotal * nummon;
                }
                else if (nummem > 1 && DropDownList2.SelectedValue == "7")
                {

                    var downMem = nummem - 1;
                    //var memtype2Val_1 = int.Parse(EntranceFeeStart.Rows[0]["FirstMember"].ToString());
                    //var memtype2Val_2 = int.Parse(EntranceFeeStart.Rows[0]["SecondMember"].ToString());
                    //var memtype2valTotal = memtype2Val_1 + memtype2Val_2;

                    entrancetmp = 200;

                    entrancetmp = entrancetmp + (downMem * nummemTotal);
                    //entrancetmp = memtype2valTotal + (downMem * nummemTotal);

                    annualtmp = (nummemTotal + (downMem * 100)) * nummon;
                }

                //entrancetmp = entrancetmp * nummem * nummon;
                //entrancetmp = entrancetmp + (nummem * nummemTotal);
                //annualtmp = annualtmp * nummem * nummon;
            }
            catch { }
            //*Total fee=entrance fee+annual fee
            // * entrance fee=base case+addition
            // * base case=type==1||3A||3B?600:100
            // * addition=type==x
           //  * annual fee=base case+addition
            // * base case=type==1||3A?200:100
           //  * addition=type==x
           //  * 1 3A count first
           //  * 7 3B count member
             

            if (PaymentType.SelectedValue == "Entrance Fee")
            {
                entranceVal.Text = entrancetmp.ToString();
                annualVal.Text = "0";
            }
            else if (PaymentType.SelectedValue == "Annual Fee")
            {
                entranceVal.Text = "0";
                annualVal.Text = annualtmp.ToString();
            }
            else if (PaymentType.SelectedValue == "No Pay")
            {
                entranceVal.Text = "0";
                annualVal.Text = "0";
            }
            else
            {
                entranceVal.Text = entrancetmp.ToString();
                annualVal.Text = annualtmp.ToString();
            }
            totalCalculate();
            expireDateInit();
            //numMemBox.Text = "1";
        }
        */
        protected void calculateFee(object sender, EventArgs e)
        {
            if (GetReceiptChk.Checked)
                cNoBox.Attributes.Remove("disabled");

            int entranceFee = 0;
            int annualFee = 0;

            try
            {
                string memberType = DropDownList2.SelectedValue;
                int nummem = int.TryParse(numMemBox.Text, out var tmp1) ? tmp1 : 1;
                int payDuration = int.TryParse(PayDuration.Text, out var tmp2) ? tmp2 : 1;

                // ใช้ repository แทน SelectSqlTable
                var repo = new PrivatePaymentRepository(connStr);

                // ดึงข้อมูล SMemberType
                string sqlMemType = @"
            SELECT * 
            FROM SMemberType 
            WHERE MemberType = @memberType 
              AND EffectiveID = (
                  SELECT TOP 1 EffectiveID 
                  FROM tblEffective 
                  WHERE EffectiveDate <= FLOOR(CAST(GETDATE() AS FLOAT)) 
                    AND ExpireDate >= FLOOR(CAST(GETDATE() AS FLOAT))
              )";

                var memTypeData = repo.ExecuteSelectQuery(sqlMemType, new Dictionary<string, object>
        {
            { "@memberType", memberType }
        });

                if (memTypeData.Rows.Count == 0) return; // ไม่มีข้อมูล

                int newsletterFee = int.Parse(memTypeData.Rows[0]["Newsletter"].ToString());

                // ดึงค่า EntranceFee
                string sqlEntrance = @"
            SELECT TOP 1 * 
            FROM SMemberEntranceFee 
            WHERE EffectiveID = (
                SELECT TOP 1 EffectiveID 
                FROM tblEffective 
                WHERE EffectiveDate <= FLOOR(CAST(GETDATE() AS FLOAT)) 
                  AND ExpireDate >= FLOOR(CAST(GETDATE() AS FLOAT))
            ) 
            ORDER BY EffectiveID";

                var entranceData = repo.ExecuteSelectQuery(sqlEntrance, new Dictionary<string, object>());

                int firstMemberFee = int.Parse(entranceData.Rows[0]["FirstMember"].ToString());
                int secondMemberFee = int.Parse(entranceData.Rows[0]["SecondMember"].ToString());

                // คำนวณ Entrance Fee
                if (memberType == "2") // MemberType 2
                {
                    entranceFee = firstMemberFee + secondMemberFee;
                    if (nummem > 1)
                        entranceFee += (nummem - 1) * newsletterFee;
                }
                else if (memberType == "6A" || memberType == "6B")
                {
                    entranceFee = 0;
                }
                else if (memberType == "7")
                {
                    entranceFee = 200;
                    if (nummem > 1)
                        entranceFee += (nummem - 1) * newsletterFee;
                }
                else
                {
                    entranceFee = nummem == 1 ? firstMemberFee : firstMemberFee + (nummem - 1) * newsletterFee;
                }

                // คำนวณ Annual Fee
                annualFee = nummem * newsletterFee * payDuration;
                if (nummem > 1 && (memberType != "2" && memberType != "6A" && memberType != "6B"))
                {
                    annualFee += (nummem - 1) * 100 * payDuration;
                }

                // กำหนดค่าเข้า TextBox ตาม PaymentType
                switch (PaymentType.SelectedValue)
                {
                    case "Entrance Fee":
                        entranceVal.Text = entranceFee.ToString();
                        annualVal.Text = "0";
                        break;
                    case "Annual Fee":
                        entranceVal.Text = "0";
                        annualVal.Text = annualFee.ToString();
                        break;
                    case "No Pay":
                        entranceVal.Text = "0";
                        annualVal.Text = "0";
                        break;
                    default: // Both
                        entranceVal.Text = entranceFee.ToString();
                        annualVal.Text = annualFee.ToString();
                        break;
                }

                totalCalculate();
                expireDateInit();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine("calculateFee error: " + ex.Message);
            }
        }





        protected void totalCalculate()
        {
            int tmp = 0;
            string annualTmp = annualVal.Text;
            string entranceTmp = entranceVal.Text;
            int annual = 0;
            int entrance = 0;
            if (annualTmp.Trim() != "")
            {
                annual = int.Parse(annualTmp);
            }
            if (entranceTmp.Trim() != "")
            {
                entrance = int.Parse(entranceTmp);
            }
            tmp += annual + entrance;
            totolSum.Text = tmp.ToString();
        }
        protected void expireDateInit()
        {
            ExpiredDate.Text = HiddenExpiredDate.Value;
            Label4.Text = HiddenTranId.Value;
        }
    }
}