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
using Microsoft.Owin;
using System.Drawing;

namespace JAT.Private
{
    public partial class privateEntry : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();

		//public string postBackId;

		private string showfirstMem;
        private string showMem;
        public static string memberstatus;
        public static string cancelDateTmp;

        string addValue;

        string dateToCancel;

        string toDayDateSh = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
        DateTime toDayDateTime = DateTime.Now;

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
            dateToCancel = DateTime.Now.ToString("dd/MM/yyyy");

            //Box3.Value = toDayDateSh;

            //Label1.Text = dateToCancel;
            //mode.Value = Request.QueryString["mode"];

            showfirstMem = Request.QueryString["firstmemberid"];
            showMem = Request.QueryString["memberid"];
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully" + companyId + "'" + ")", true);

            addValue = Request.QueryString["mode"];




            // Add Data function
            if (addValue == "add")
            {
                BindDataCheckBoxTemp();
                if (!Page.IsPostBack)
                {
                    BindMemberTypeList();
                    EnabledForm();
                }
                Box2.Attributes.Remove("disabled");

                RequiredFieldValidator1.Visible = true;
                RequiredFieldValidator2.Visible = true;
                RequiredFieldValidator3.Visible = true;
            }
            else
            {
                BindDataCheckBoxTemp();
                //BindMemberTypeList();
                DisabledForm();
                Box2.Attributes.Add("disabled", "disabled");

                RequiredFieldValidator1.Visible = false;
                RequiredFieldValidator2.Visible = false;
                RequiredFieldValidator3.Visible = false;
            }

            // Edit Data function
            if (showMem != null || showfirstMem != null)
            {

                addBTN.Visible = false;
                cancelBtn.Visible = false;
                editBTN.Visible = true;
                updateBtn.Visible = false;
                cancelBtnMem.Visible = false;

                //BindData();
                //BindDataAddress1();
                //showInGrid();
                //showInGrid2();
                //BindDataCheckBox();
                //BindDataRemark();
                if (!Page.IsPostBack)
                {
                    BindDataAddress1();
                    showInGrid();
                    showInGrid2();
                    BindDataCheckBox();
                    BindDataRemark();
                    BindDataCheckBoxTemp();
                    BindMemberTypeList();
                    BindData();
                }

            }
            else
            {
                editBTN.Visible = false;
                updateBtn.Visible = false;
                cancelBtnMem.Visible = false;
            }

            //if (!Page.IsPostBack)
            //{
            //	BindData();
            //	BindDataAddress1();
            //	showInGrid();
            //	showInGrid2();
            //	BindDataCheckBox();
            //	BindDataRemark();
            //}

        }

        private void EnabledForm()
        {
            BindData();
            addBTN.Visible = false;


            if (showMem != null || showfirstMem != null)
            {

                addBTN.Visible = false;
                cancelBtn.Visible = false;
                updateBtn.Visible = true;
                cancelBtnMem.Visible = true;

            }
            else
            {
                saveBtn.Visible = true;
                updateBtn.Visible = false;
                cancelBtnMem.Visible = false;

            }



            //Box1.Enabled = true;
            //Box2.Attributes.Remove("disabled");
            Box3.Attributes.Remove("disabled");
            //Box4.Enabled = true;
            Box5.Attributes.Remove("disabled");
            Box6.Attributes.Remove("disabled");
            //Box7.Enabled = true;
            Box8.Attributes.Remove("disabled");
            Box9.Attributes.Remove("disabled");
            //Box10.Enabled = true;
            Box11.Attributes.Remove("disabled");
            //Box12.Enabled = true;
            Box13.Attributes.Remove("disabled");
            //Box14.Enabled = true;
            Box15.Attributes.Remove("disabled");
            //Box16.Enabled = true;
            Box17.Attributes.Remove("disabled");
            Box18.Attributes.Remove("disabled");
            //Box19.Enabled = true;
            Box20.Attributes.Remove("disabled");
            //Box21.Enabled = true;
            Box22.Attributes.Remove("disabled");
            //Box23.Enabled = true;
            Box24.Attributes.Remove("disabled");
            //Box25.Enabled = true;
            //Box26.Attributes.Remove("disabled");
            //Box27.Enabled = true;
            //Box28.Enabled = true;
            //Box29.Attributes.Remove("disabled");
            //Box30.Enabled = true;
            //Box31.Enabled = true;
            cbEngtest.Enabled = true;
            cbOnevent.Enabled = true;
            cbSoftball.Enabled = true;
            cbYoga.Enabled = true;



            if (ev_tmp1.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue1.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue1.Enabled = true;
            }
            if (ev_tmp2.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue2.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue2.Enabled = true;
            }
            if (ev_tmp3.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue3.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue3.Enabled = true;
            }
            if (sub_tmp1.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue4.Enabled = false;

            }
            else if (addValue != "add")
            {
                cbValue4.Enabled = true;
            }
            if (sub_tmp2.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue5.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue5.Enabled = true;
            }
            //else
            //{
            //    cbValue1.Enabled = true;
            //    cbValue2.Enabled = true;
            //    cbValue3.Enabled = true;
            //}



            cbBoard.Enabled = true;
            cbBoardlist.Enabled = true;
            cbGolf.Enabled = true;
            cbLady.Enabled = true;
            cbClubSecre.Enabled = true;
            cbBaVolun.Enabled = true;
            cbSocialMem.Enabled = true;
            cbYouthMem.Enabled = true;

            //cbValue4.Enabled = true;
            //cbValue5.Enabled = true;

            cbSukusukuMem.Enabled = true;
            cbChildLibMem.Enabled = true;

            cbOverseasMem.Enabled = true;

            Box32.Attributes.Remove("disabled");
            Box33.Attributes.Remove("disabled");
            Box34.Attributes.Remove("disabled");
            Box35.Attributes.Remove("disabled");
            Box36.Attributes.Remove("disabled");
            Box37.Attributes.Remove("disabled");
            Box38.Attributes.Remove("disabled");
            Box39.Enabled = true;
            preFixSel.Attributes.Remove("disabled");
            //BoxChkChild.Enabled = true;
            email.Attributes.Remove("disabled");
        }

        private void DisabledForm()
        {
            addBTN.Visible = true;

            saveBtn.Visible = false;
            cancelBtn.Visible = false;

            //Box1.Enabled = false;
            //Box2.Attributes.Add("disabled", "disabled");
            Box3.Attributes.Add("disabled", "disabled");
            //Box4.Enabled = false;
            Box5.Attributes.Add("disabled", "disabled");
            Box6.Attributes.Add("disabled", "disabled");
            //Box7.Enabled = false;
            Box8.Attributes.Add("disabled", "disabled");
            Box9.Attributes.Add("disabled", "disabled");
            //Box10.Enabled = false;
            Box11.Attributes.Add("disabled", "disabled");
            //Box12.Enabled = false;
            Box13.Attributes.Add("disabled", "disabled");
            //Box14.Enabled = false;
            Box15.Attributes.Add("disabled", "disabled");
            //Box16.Enabled = false;
            Box17.Attributes.Add("disabled", "disabled");
            Box18.Attributes.Add("disabled", "disabled");
            //Box19.Enabled = false;
            Box20.Attributes.Add("disabled", "disabled");
            //Box21.Enabled = false;
            Box22.Attributes.Add("disabled", "disabled");
            //Box23.Enabled = false;
            Box24.Attributes.Add("disabled", "disabled");
            //Box25.Enabled = false;
            //Box26.Attributes.Add("disabled", "disabled");
            //Box27.Enabled = false;
            //Box28.Enabled = false;
            //Box29.Attributes.Add("disabled", "disabled");
            //Box30.Enabled = false;
            //Box31.Enabled = false;
            cbEngtest.Enabled = false;
            cbOnevent.Enabled = false;
            cbSoftball.Enabled = false;
            cbYoga.Enabled = false;

            if (ev_tmp1.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue1.Enabled = false;

            }
            else if (addValue != "add")
            {
                cbValue1.Enabled = false;
            }
            if (ev_tmp2.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue2.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue2.Enabled = false;
            }
            if (ev_tmp3.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue3.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue3.Enabled = false;
            }
            if (sub_tmp1.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue4.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue4.Enabled = false;
            }
            if (sub_tmp2.Text.Trim().ToString() == "&nbsp;")
            {
                cbValue5.Enabled = false;
            }
            else if (addValue != "add")
            {
                cbValue5.Enabled = false;
            }
            cbValue1.Enabled = false;
            cbValue2.Enabled = false;
            cbValue3.Enabled = false;

            cbBoard.Enabled = false;
            cbBoardlist.Enabled = false;
            cbGolf.Enabled = false;
            cbLady.Enabled = false;
            cbClubSecre.Enabled = false;
            cbBaVolun.Enabled = false;
            cbSocialMem.Enabled = false;
            cbYouthMem.Enabled = false;
            cbValue4.Enabled = false;
            cbValue5.Enabled = false;

            cbSukusukuMem.Enabled = false;
            cbChildLibMem.Enabled = false;

            cbOverseasMem.Enabled = false;

            Box32.Attributes.Add("disabled", "disabled");
            Box33.Attributes.Add("disabled", "disabled");
            Box34.Attributes.Add("disabled", "disabled");
            Box35.Attributes.Add("disabled", "disabled");
            Box36.Attributes.Add("disabled", "disabled");
            Box37.Attributes.Add("disabled", "disabled");
            Box38.Attributes.Add("disabled", "disabled");
            Box39.Enabled = false;
            preFixSel.Attributes.Add("disabled", "disabled");

            email.Attributes.Add("disabled", "disabled");

            //BoxChkChild.Enabled = false;
        }


        protected void BindData()
        {
            connection();
            SqlCommand sc;
            //SqlCommand sc2;
            SqlDataReader rd;
            //SqlDataReader rd2;

            //string sql = "SELECT  t1.firstmemberid, t1.memberid, t1.prefixNm, t1.nameJ, t1.nameE, FORMAT(t1.birthDate, 'yyyy-MMM-dd') AS brithDay, FORMAT(t1.appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(t1.cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, t1.memberStatus, t1.memberType, t2.birthPlace, t3.companyNm, t3.address, t3.phone, t3.fax, (SELECT sendType FROM PrivateSendHistory WHERE memberid = " + "'" + showfirstMem + "'" + " AND endDate IS NULL " + ")" + "AS sendType, t5.sortBoard, t5.sortLady, t5.boardPosition " +
            //string sql = "SELECT  t1.firstmemberid, t1.memberid, t1.prefixNm, t1.nameJ, t1.nameE, FORMAT(t1.birthDate, 'yyyy-MMM-dd') AS brithDay, FORMAT(t1.appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(t1.cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, t1.memberStatus, t1.memberType, t2.birthPlace, t3.companyNm, t3.address, t3.phone, t3.fax, t2.sendType, t5.sortBoard, t5.sortLady, t5.boardPosition,updatedBy,staffFName " +
            //                "FROM PrivateDetail t1 " +
            //                "INNER JOIN Private t2 ON t1.memberid = t2.memberid INNER JOIN privateAddress t3 ON t1.memberid = t3.memberid INNER JOIN PrivateBoard t5 ON t1.firstmemberid = t5.memberId INNER JOIN SStaff ss ON t1.updatedBy = ss.staffID " +
            //                "WHERE t1.memberid = '" + showfirstMem + "'" + "AND t3.addressType = '2'" + "AND t1.memberid = t1.firstmemberid ";
            //string sqlBrithPlace = "SELECT birthPlace FROM PrivateDetail WHERE memberid = '" + companyId +"'";
            string sql = "select p.memberid, p.checkmember, p.checkCompanyNm, p.checkCompanyAddress, " +
                "p.checkCompanyPhone, p.checkCompanyFax, p.checkHomeAddress, p.checkHomePhone, " +
                "p.checkHomeMobile, p.sendType, p.birthPlace, p.email, p.changeAddressDate, " +
                "d.firstmemberid, d.memberid, d.prefixNm, d.nameJ, d.nameE, FORMAT(d.birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(d.appliedDate, 'dd/MM/yyyy') AS appliedDate, " +
                "d.updatedDate, FORMAT(d.cancelledDate, 'dd/MM/yyyy') AS cancelledDate, d.memberStatus, d.memberType, d.updatedBy, d.locked, d.lockedBy, " +
                "c.memberid, c.golf, c.board, c.lady, c.children, c.zukuzuku, c.meijinkai, " +
                "ah.address as homeAddress, ah.phone as homePhone, ah.mobile as mobile, " +
                "ac.address as companyAddress, ac.phone as companyPhone, ac.fax as companyFax, ac.companyNm, " +
                "b.memberId, b.sortBoard, b.boardPosition, b.sortLady, p.getSplitPayment, dd.remark as privateremark1, p.checkMainName, p.checkBirthPlace, p.checkFamilyName,staffFName,p.zip_code " +
                "from private p left join privateDetail d on p.memberid = d.memberid " +
                "left join privateAddress ah on p.memberid = ah.memberid and ah.addresstype = 1 " +
                "left join privateAddress ac on p.memberid = ac.memberid and ac.addresstype = 2 " +
                "left join privateClub c on p.memberid = c.memberid " +
                "left join privateBoard b on p.memberId = b.memberId " +
                "left join privateremark dd on p.memberid = dd.memberid " +
                "left join SStaff ss ON updatedBy = staffID " +
                "where p.memberid = '" + showfirstMem + "' ";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                //sc2 = new SqlCommand(sqlBrithPlace, conn);
                rd = sc.ExecuteReader();
                //rd2 = sc2.ExecuteReader();

                while (rd.Read())
                {
                    //show in member information
                    Box2.Value = rd.GetValue(0).ToString();
                    Box3.Value = rd.GetValue(19).ToString();
                    Box5.Value = rd.GetValue(16).ToString();
                    string tmp = rd.GetValue(15).ToString();
                    if (tmp == "")
                    {
                        tmp = "-- ANY --";
                    }
                    preFixSel.SelectedValue = tmp;
                    Box6.Value = rd.GetValue(17).ToString();
                    Box8.Value = rd.GetValue(10).ToString();
                    Box9.Value = rd.GetValue(18).ToString();
                    //show in company informantion
                    Box11.Value = rd.GetValue(40).ToString();
                    Box13.Value = rd.GetValue(37).ToString();
                    Box15.Value = rd.GetValue(38).ToString();
                    Box17.Value = rd.GetValue(39).ToString();
                    //Box17.Value = rd.GetValue(24).ToString();
                    //show in information
                    //Box20.Value = rd.GetValue(20).ToString();
                    //Box22.Value = rd.GetValue(21).ToString();
                    //Box24.Value = rd.GetValue(24).ToString();
                    Box18.Value = rd.GetValue(51).ToString();
                    statusdate.Text = rd.GetValue(21).ToString();
                    if (statusdate.Text == "01/01/1900")
                    {
                        statusdate.Text = "";
                    }
                    cancelDateTmp = rd.GetValue(21).ToString();
                    Box32.SelectedValue = rd.GetValue(22).ToString();
                    memberstatus = rd.GetValue(22).ToString();
                    Box33.SelectedValue = rd.GetValue(9).ToString();
                    Box34.SelectedValue = rd.GetValue(23).ToString();
                    Box35.Value = rd.GetValue(42).ToString();
                    Box36.Value = rd.GetValue(44).ToString();
                    Box37.Value = rd.GetValue(43).ToString();
                    //Box38.Value = rd.GetValue(24).ToString();

                    HiddenField1.Value = rd.GetValue(9).ToString();

                    updateBy.Text = rd.GetValue(50).ToString();

                    //if (updateBtn.Visible == true)
                    //{
                    //	break;
                    //}
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

            //Box2.Value = showVal;


            conn.Close();
        }

        protected void BindDataAddress1()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT distinct t3.address, t3.phone, t3.mobile, t2.email, FORMAT(t2.changeAddressDate, 'dd/MM/yyyy', 'en-us') " +
                            "FROM PrivateDetail t1 " +
                            "INNER JOIN private t2 ON t1.memberid = t2.memberid " +
                            "INNER JOIN privateAddress t3 ON t1.memberid = t3.memberid " +
                            "WHERE t1.memberid = '" + showfirstMem + "'" + "AND t3.addressType = '1'" + "AND t1.memberid = firstmemberid";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in information Address type 1
                    Box20.Value = rd.GetValue(0).ToString();
                    Box22.Value = rd.GetValue(1).ToString();
                    Box24.Value = rd.GetValue(2).ToString();
                    email.Value = rd.GetValue(3).ToString();
                    changeAddressDate.Text = rd.GetValue(4).ToString();
                }

            }
            catch { }

            //Box2.Value = showVal;


            conn.Close();
        }

        protected void BindDataCheckBox()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT t2.* ,t1.getSplitPayment " +
                            "FROM Private t1 " +
                            "INNER JOIN PrivateClub t2 on t1.memberid = t2.memberid " +
                            "WHERE t1.memberid = '" + showfirstMem + "'" + "AND t1.memberid = '" + showfirstMem + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    bool chkDataInDB11 = (bool)rd.GetValue(22);
                    Box39.Checked = chkDataInDB11;

                    bool chkDataInDB12 = (bool)rd.GetValue(1);
                    cbGolf.Checked = chkDataInDB12;

                    bool chkDataInDB13 = (bool)rd.GetValue(2);
                    cbBoard.Checked = chkDataInDB13;

                    bool chkDataInDB14 = (bool)rd.GetValue(3);
                    cbLady.Checked = chkDataInDB14;

                    bool chkDataInDB15 = (bool)rd.GetValue(4);
                    cbChildLibMem.Checked = chkDataInDB15;

                    bool chkDataInDB16 = (bool)rd.GetValue(5);
                    cbSukusukuMem.Checked = chkDataInDB16;

                    bool chkDataInDB17 = (bool)rd.GetValue(7);
                    cbEngtest.Checked = chkDataInDB17;

                    bool chkDataInDB18 = (bool)rd.GetValue(8);
                    cbOnevent.Checked = chkDataInDB18;

                    bool chkDataInDB19 = (bool)rd.GetValue(9);
                    cbSoftball.Checked = chkDataInDB19;

                    bool chkDataInDB20 = (bool)rd.GetValue(10);
                    cbYoga.Checked = chkDataInDB20;

                    bool chkDataInDB21 = (bool)rd.GetValue(11);
                    cbValue1.Checked = chkDataInDB21;

                    bool chkDataInDB22 = (bool)rd.GetValue(12);
                    cbValue2.Checked = chkDataInDB22;

                    bool chkDataInDB23 = (bool)rd.GetValue(13);
                    cbValue3.Checked = chkDataInDB23;

                    bool chkDataInDB24 = (bool)rd.GetValue(14);
                    cbBoardlist.Checked = chkDataInDB24;

                    bool chkDataInDB25 = (bool)rd.GetValue(15);
                    cbClubSecre.Checked = chkDataInDB25;

                    bool chkDataInDB26 = (bool)rd.GetValue(16);
                    cbBaVolun.Checked = chkDataInDB26;

                    bool chkDataInDB27 = (bool)rd.GetValue(17);
                    cbSocialMem.Checked = chkDataInDB27;

                    bool chkDataInDB28 = (bool)rd.GetValue(18);
                    cbYouthMem.Checked = chkDataInDB28;

                    bool chkDataInDB29 = (bool)rd.GetValue(19);
                    cbValue4.Checked = chkDataInDB29;

                    bool chkDataInDB30 = (bool)rd.GetValue(20);
                    cbValue5.Checked = chkDataInDB30;

                    bool chkDataInDB31 = (bool)rd.GetValue(21);
                    cbOverseasMem.Checked = chkDataInDB31;
                }

            }
            catch { }
            //Box2.Value = showVal;
            conn.Close();
        }
        protected void BindDataCheckBoxTemp()
        {
            DataTable td;
            td = SelectSqlTable("SELECT * FROM privateClubDetail");
            foreach (DataRow tmprow in td.Rows)
            {
                switch (tmprow["itemNm"].ToString().Trim())
                {
                    case "ev_tmp1": ev_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "ev_tmp2": ev_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "ev_tmp3": ev_tmp3.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "sub_tmp1": sub_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "sub_tmp2": sub_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                }
            }
        }


        protected void BindDataRemark()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT remark " +
                            "FROM PrivateRemark " +
                            "WHERE memberid = '" + showfirstMem + "'" + "AND memberid = '" + showfirstMem + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();
                while (rd.Read())
                {
                    //show in Remark
                    Box38.Value = rd.GetValue(0).ToString();
                }
            }
            catch { }
        }

        protected void showInGrid()
        {
            DataTable td;

            //var memSatCho = DropDownList1.SelectedValue.ToString();
            //var memberTypeCho = RadioButtonList1.SelectedValue.ToString();
            //var textInputOpt = DropDownList2.SelectedValue.ToString();
            //textInput = Text1.Value.ToString();
            //var sortOpt = DropDownList3.SelectedValue.ToString();
            //var sortCouOpt = DropDownList4.SelectedValue.ToString();



            td = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(appliedDate, 'dd/MM/yyyy') AS appliedDate, FORMAT(cancelledDate, 'dd/MM/yyyy') AS cancelledDate, memberType, memberStatus " +
                                "FROM PrivateDetail " +
                                "WHERE firstmemberid =" + "'" + showfirstMem + "'" + "AND firstmemberid != memberid");
            //GridView1.Columns[5].Visible = false;
            GridView1.DataSource = td;
            //GridView2.DataSource = td;
            //ImageButton1.Visible = true;
            //ImageButton2.Visible = true;

            GridView1.DataBind();
            //GridView2.DataBind();
            conn.Close();

        }

        protected void showInGrid2()
        {
            DataTable td;


            td = SelectSqlTable("SELECT sendType, FORMAT(startDate, 'dd/MM/yyyy') AS startDate, FORMAT(endDate, 'dd/MM/yyyy') AS endDate " +
                                "FROM PrivateSendHistory " +
                                "WHERE memberId =" + "'" + showfirstMem + "'");

            GridView2.DataSource = td;
            GridView2.DataBind();
            conn.Close();

        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            // Response.Redirect("privateEntry.aspx?mode=add");
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            DisabledForm();
            Response.Redirect("privateEntry.aspx");
        }


        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfirstMem != null)
            {
                Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfirstMem);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfirstMem != null)
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfirstMem);
            }
        }

        protected void paymentTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfirstMem != null)
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfirstMem);
            }
        }
        protected void cancelTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfirstMem != null)
            {
                Response.Redirect("privateCancel.aspx?firstmemberid=" + showfirstMem);
            }
        }

        protected void specialTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfirstMem != null)
            {
                Response.Redirect("privateFeature.aspx?firstmemberid=" + showfirstMem);
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            string checkinput = checkInput();
            if (confirmValue == "Yes" && checkinput == "Valid")
            {
                DataTable td;
                var memIDInput = Box2.Value.ToString().Replace("'", "''");
                var aplliedDateinput = Box3.Value.ToString().Replace("'", "''");
                var nameJinput = Box5.Value.ToString().Replace("'", "''");
                var nameEinput = Box6.Value.ToString().Replace("'", "''");
                var birthPlaceinput = Box8.Value.ToString().Replace("'", "''");
                var birthDateinput = Box9.Value.ToString().Replace("'", "''");
                var preFixCho = preFixSel.SelectedValue.ToString();
                if (preFixCho == "-- ANY --")
                {
                    preFixCho = "";
                }
                var comNminput = Box11.Value.ToString().Replace("'", "''");
                var comAddinput = Box13.Value.ToString().Replace("'", "''");
                var comPhoneinput = Box15.Value.ToString().Replace("'", "''");
                var comFaxinput = Box17.Value.ToString().Replace("'", "''");
                var zipcode = Box18.Value.ToString().Replace("'", "''");
                var Addinput = Box20.Value.ToString().Replace("'", "''");
                var Phoneinput = Box22.Value.ToString().Replace("'", "''");
                var Mobileinput = Box24.Value.ToString().Replace("'", "''");
                var chkGolf = cbGolf.Checked;
                var chkChild = cbChildLibMem.Checked;
                var chkBoard = cbBoard.Checked;
                var chkSukuzuku = cbSukusukuMem.Checked;
                var chkLady = cbLady.Checked;
                var chkEngtest = cbEngtest.Checked;
                var chkOnevent = cbOnevent.Checked;
                var chkSoftball = cbSoftball.Checked;
                var chkYoga = cbYoga.Checked;
                var chkValue1 = cbValue1.Checked;
                var chkValue2 = cbValue2.Checked;
                var chkValue3 = cbValue3.Checked;
                var chkBoardlist = cbBoardlist.Checked;
                var chkClubSecre = cbClubSecre.Checked;
                var chkBaVolun = cbBaVolun.Checked;
                var chkSocialMem = cbSocialMem.Checked;
                var chkYouthMem = cbYouthMem.Checked;
                var chkValue4 = cbValue4.Checked;
                var chkValue5 = cbValue5.Checked;
                var chkOverseasMem = cbOverseasMem.Checked;
                string cancelDate = "Null";
                if (Box32.SelectedValue.ToString() == "NA")
                {
                    cancelDate = toDayDateTime.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    cancelDate = "'" + cancelDate + "'";
                }
                var memStaCho = Box32.SelectedValue.ToString();
                var sendMethodCho = Box33.SelectedValue.ToString();
                var memTypeCho = Box34.SelectedValue.ToString();
                var sortBoardinput = Box35.Value;
                var sortLadyinput = Box36.Value;
                var positioninput = Box37.Value.ToString().Replace("'", "''");
                var remarkInput = Box38.Value.ToString().Replace("'", "''");
                var chkPayment = Box39.Checked;
                var updateuid = Session["UID"];
                var emailVal = email.Value.ToString().Replace("'", "''");

                var uid = Session["UID"];
                int staffID = uid != null ? Convert.ToInt32(uid) : 0;

                try
                {
                    

                    if (addValue == "add" && sendMethodCho != "any")
                    {
						
						try
                        {
							td = SelectSqlTable("SET dateformat dmy INSERT INTO PrivateDetail(firstmemberid, memberid, nameJ, nameE, prefixNm, cancelledDate, memberStatus, birthDate, memberType, appliedDate, updatedBy)" +
											"VALUES('" + memIDInput + "'" + "," + "'" + memIDInput + "'" + "," + "N'" + nameJinput + "'" + "," + "'" + nameEinput + "'" + "," + "'" + preFixCho + "'" + "," + cancelDate + "," + "'" + memStaCho + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + memTypeCho + "'" + "," + "'" + aplliedDateinput + "'" + "," + "'" + updateuid + "'" + ")" +
											"INSERT INTO PrivateSendHistory(memberId, sendType)" +
											"VALUES('" + memIDInput + "'" + "," + "'" + sendMethodCho + "'" + ")" +
											"INSERT INTO PrivateRemark(memberId, remark)" +
											"VALUES('" + memIDInput + "'" + "," + "N'" + remarkInput + "'" + ")" +
											"INSERT INTO PrivateRefer(memberid)" +
											"VALUES('" + memIDInput + "'" + ")" +
											"INSERT INTO PrivatePayment(memberid)" +
											"VALUES('" + memIDInput + "'" + ")" +
											"INSERT INTO PrivateBoard(memberId, sortBoard, sortLady, boardPosition)" +
											"VALUES('" + memIDInput + "'" + "," + "'" + sortBoardinput + "'" + "," + "'" + sortLadyinput + "'" + "," + "'" + positioninput + "'" + ")" +
											"INSERT INTO privateAddress(memberid, addressType, address, phone, mobile)" +
											"VALUES('" + memIDInput + "'" + "," + "'" + "1" + "'" + "," + "'" + Addinput + "'" + "," + "'" + Phoneinput + "'" + "," + "'" + Mobileinput + "'" + ")" +
											"INSERT INTO privateAddress(memberid, addressType, companyNm, address, phone, fax)" +
											"VALUES('" + memIDInput + "'" + "," + "'" + "2" + "'" + "," + "N'" + comNminput + "'" + "," + "N'" + comAddinput + "'" + "," + "'" + comPhoneinput + "'" + "," + "'" + comFaxinput + "'" + ")" +
											"INSERT INTO PrivateAccount(memberId)" +
											"VALUES('" + memIDInput + "'" + ")" +
											"INSERT INTO Private(memberid, birthPlace, sendType, checkmember, checkMainName, checkBirthPlace, checkCompanyNm, checkCompanyAddress, checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, checkHomeMobile, getSplitPayment,email,zip_code)" +
											"VALUES('" + memIDInput + "'" + "," + "N'" + birthPlaceinput + "'" + "," + "'" + sendMethodCho + "'" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "'" + chkPayment + "'" + "," + "'" + emailVal + "'" + "," + "'" + zipcode + "'" + ")" +
											"INSERT INTO PrivateClub(memberid, golf, children, board, zukuzuku, lady, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member)" +
											"VALUES('" + memIDInput + "'" + "," + "'" + chkGolf + "'" + "," + "'" + chkChild + "'" + "," + "'" + chkBoard + "'" + "," + "'" + chkSukuzuku + "'" + "," + "'" + chkLady + "'" + "," + "'" + chkEngtest + "'" + "," + "'" + chkOnevent + "'" + "," + "'" + chkSoftball + "'" + "," + "'" + chkYoga + "'" + "," + "'" + chkValue1 + "'" + "," + "'" + chkValue2 + "'" + "," + "'" + chkValue3 + "'" + "," + "'" + chkBoardlist + "'" + "," + "'" + chkClubSecre + "'" + "," + "'" + chkBaVolun + "'" + "," + "'" + chkSocialMem + "'" + "," + "'" + chkYouthMem + "'" + "," + "'" + chkValue4 + "'" + "," + "'" + chkValue5 + "'" + "," + "'" + chkOverseasMem + "'" + ")");

							string activityDetail = $"Added new data into 10 tables ('PrivateDetail, PrivateSendHistory, PrivateRemark, PrivateRefer, PrivatePayment, PrivateBoard, privateAddress, PrivateAccount, Private, PrivateClub') successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
                            string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                        }
                        catch (Exception ex)
                        {
                            string activityDetail = $@"Error: {ex.Message}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
						}


						Response.Redirect("privateEntry.aspx?firstmemberid=" + Box2.Value + "&memberid=" + Box2.Value);
						}
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have to select send method.')", true);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        lbError.Text = "Duplicate member id.";
                    }
                    else
                    {
                        lbError.Text = "Database error: input may not be in the proper format.";
                    }
                    string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                catch (Exception ex)
                {
                    lbError.Text = ex.ToString();
                    string activityDetail = $@"Error: {ex.Message}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
            }
            else if (confirmValue == "Yes" && checkinput != "Valid")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + checkinput + "')", true);
            }
        }

        protected void editBTN_Click(object sender, EventArgs e)
        {
            EnabledForm();
            Box2.Attributes.Add("disabled", "disabled");
            editBTN.Visible = false;
        }

        protected void cancelBtnMem_Click(object sender, EventArgs e)
        {
            DisabledForm();
            Response.Redirect("privateEntry.aspx?firstmemberid=" + showfirstMem + "&memberid=" + showMem);
        }

        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            Response.Redirect("privateEntryMember.aspx?mode=edit&firstmemberid=" + showfirstMem + "&memberid=" + row.Cells[0].Text);
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string confirmValue = Request.Form["confirm_value"];
            string checkinput = checkInput();
            if (confirmValue == "Yes" && checkinput == "Valid")
            {
                DataTable td;

                // update_date
                try
                {
					td = SelectSqlTable("SET dateformat dmy " +
										"UPDATE Private " +
										"SET changeAddressDate = GETDATE() " +
										"WHERE memberid =" + "'" + showfirstMem + "'");
                    string activityDetail = $"Changed data in table a 'private' where memberid is '{showfirstMem}' successful (user id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID,activityDetail);
				}
                catch (SqlException ex)
                {
                    string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                catch (Exception ex)
                {
                    string activityDetail = $@"Error: {ex.Message}";
                    logActivity.LogStaffActivity(staffID, activityDetail);
				}




				var memIDInput = Box2.Value.ToString();

                // 2024-09-18 11.38am : Toon Fix
                var AppliedInput = Date_MsSqlStandard.CastQuery(Box3.Value.ToString());
                // *** End Fix ****

                var nameJinput = Box5.Value.ToString();
                var nameEinput = Box6.Value.ToString();
                var birthPlaceinput = Box8.Value.ToString();

                // 2024-09-18 11.38am : Toon Fix
                var birthDateinput = Date_MsSqlStandard.CastQuery(Box9.Value.ToString());
                // *** End Fix ***

                var preFixCho = preFixSel.SelectedValue.ToString();
                if (preFixCho == "-- ANY --")
                {
                    preFixCho = "";
                }
                var comNminput = Box11.Value.ToString().Replace("'", "''");
                var comAddinput = Box13.Value.ToString().Replace("'", "''");
                var comPhoneinput = Box15.Value.ToString().Replace("'", "''");
                var comFaxinput = Box17.Value.ToString().Replace("'", "''");
                var zipcode = Box18.Value.ToString().Replace("'", "''");
                var Addinput = Box20.Value.ToString().Replace("'", "''");
                var Phoneinput = Box22.Value.ToString().Replace("'", "''");
                var Mobileinput = Box24.Value.ToString().Replace("'", "''");
                var chkGolf = cbGolf.Checked;
                var chkChild = cbChildLibMem.Checked;
                var chkBoard = cbBoard.Checked;
                var chkSukuzuku = cbSukusukuMem.Checked;
                var chkLady = cbLady.Checked;
                var chkEngtest = cbEngtest.Checked;
                var chkOnevent = cbOnevent.Checked;
                var chkSoftball = cbSoftball.Checked;
                var chkYoga = cbYoga.Checked;
                var chkValue1 = cbValue1.Checked;
                var chkValue2 = cbValue2.Checked;
                var chkValue3 = cbValue3.Checked;
                var chkBoardlist = cbBoardlist.Checked;
                var chkClubSecre = cbClubSecre.Checked;
                var chkBaVolun = cbBaVolun.Checked;
                var chkSocialMem = cbSocialMem.Checked;
                var chkYouthMem = cbYouthMem.Checked;
                var chkValue4 = cbValue4.Checked;
                var chkValue5 = cbValue5.Checked;
                var chkOverseasMem = cbOverseasMem.Checked;
                string cancelDate = "'" + cancelDateTmp + "'";
                string updateNAToA = "";
                if (memberstatus == "A" && Box32.SelectedValue.ToString() == "NA")
                {
                    cancelDate = "'" + toDayDateTime.ToString("dd/MM/yyyy") + "'";
                    updateNAToA = ", date_do_status_na_to_a = NULL ";
                }
                else if (memberstatus == "NA" && Box32.SelectedValue.ToString() == "A")
                {
                    cancelDate = "Null";
                    updateNAToA = ", date_do_status_na_to_a = CURRENT_TIMESTAMP ";
                }
                else if (memberstatus == "NA" && Box32.SelectedValue.ToString() == "NA")
                {
                    //update_01JUL2022
                    DataTable old_cancel_Date = SelectSqlTable(string.Format("SELECT cancelledDate FROM PrivateDetail WHERE memberid = '" + showfirstMem + "'"));
                    if (old_cancel_Date.Rows.Count > 0)
                    {
                        // *** 2024-08-26 at 04.48pm : Toon Jiradech.K revised the below code to fix the bug.
                        // *** Because the app has taken today's value to update it to the value of a canceled date.

                        #region "Bug cause."
                        // cancelDate = "'" + cancelDateTmp.ToString() + "'";               // *** Bug cause.
                        #endregion

                        #region "Fixed"
                        var _oldCancelDate = old_cancel_Date.Rows[0]["cancelledDate"].ToString();           // *** Fixed it.
                        cancelDate = DateTime.Parse(_oldCancelDate).ToString("dd/MM/yyyy HH:mm:ss.fff");    // *** Fixed it.
                        #endregion
                        // *** End of revised ***
                    }

                    //var canceldatearray = cancelDateTmp.Split('/');
                    //cancelDate = "'" + canceldatearray[0] + "/" + canceldatearray[1] + "/" + canceldatearray[2] + "'";
                    //try
                    //{
                    //    DateTime canceldatetime = DateTime.ParseExact(cancelDateTmp, "dd/MM/yyyy", null);
                    //    cancelDate = "'" + cancelDateTmp.ToString() + "'";
                    //}
                    //catch (Exception ex)
                    //{
                    //    cancelDate = "'" + cancelDateTmp.ToString() + "'";
                    //}
                }
                else if (memberstatus == "A" && Box32.SelectedValue.ToString() == "A")
                {
                    cancelDate = "Null";
                }
                var memStaCho = Box32.SelectedValue.ToString();
                var sendMethodCho = Box33.SelectedValue.ToString();
                var memTypeCho = Box34.SelectedValue.ToString();
                var sortBoardinput = Box35.Value;
                var sortLadyinput = Box36.Value;
                var positioninput = Box37.Value.ToString().Replace("'", "''");
                var remarkInput = Box38.Value.ToString().Replace("'", "''");
                var chkPayment = Box39.Checked;
                var updateuid = Session["UID"];
                var emailVal = email.Value.ToString().Replace("'", "''");
                string updateSendMethod;
                string insertToHistory;
                if (sendMethodCho != HiddenField1.Value)
                {
                    updateSendMethod = "," + " sendType = " + "'" + sendMethodCho + "'";
                    //Already logging below
                    insertToHistory = "INSERT INTO PrivateSendHistory(memberId, sendType, endDate)" + "VALUES('" + memIDInput + "'" + "," + "'" + sendMethodCho + "'" + "," + "'" + dateToCancel + "'" + ")";
                }
                else
                {
                    updateSendMethod = " ";
                    insertToHistory = " ";
                }
                string logHiddenField = HiddenField1.Value;
                if (sendMethodCho != "any")
                {
                    System.Data.DataTable chID_Board = SelectSqlTable("SELECT memberId FROM PrivateBoard WHERE memberId = '" + showfirstMem + "'");
                    if (chID_Board.Rows.Count <= 0)
                    {
                        // insert_memberID_TO_Board
                        //try
                        //{
                        try
                        {
                            var _queryStr = $"SET dateformat dmy " +
                                            $"UPDATE Private " +
                                            $"SET changeAddressDate = GETDATE() " +
                                            $"WHERE memberid = '{showfirstMem}' " +
                                            $"and (not exists (select * from privateAddress " +
                                            $"where address = N'{comAddinput}' and " +
                                            $"phone = '{comPhoneinput}' and fax = '{comFaxinput}' " +
                                            $"and companyNm = N'{comNminput}' and addressType = '2') or " +
                                            $"not exists (select * from privateAddress " +
                                            $"where address = N'{Addinput}' and phone = '{Phoneinput}' " +
                                            $"and mobile = '{Mobileinput}' and addressType = '1')) \n\n" +
                                            
                                            $"UPDATE Private " +
                                            $"SET birthplace = N'{birthPlaceinput}', " +
                                            $"checkmember = ' ', checkMainName = ' ', " +
                                            $"checkBirthPlace = ' ', checkCompanyNm = ' ', " +
                                            $"checkCompanyAddress = ' ', checkCompanyPhone = ' ', " +
                                            $"checkCompanyFax = ' ', checkHomeAddress = ' ', " +
                                            $"checkHomePhone = ' ', checkHomeMobile = ' ', " +
                                            $"getSplitPayment = '{chkPayment}', zip_code = '{zipcode}', " +
                                            $"email = '{emailVal}', sendType = '{updateSendMethod}' " +
                                            $"WHERE memberid = '{showfirstMem}' \n\n" +

                                            $"UPDATE PrivateDetail " +
                                            $"SET nameJ = N'{nameJinput}', nameE = '{nameEinput}', " +
                                            $"prefixNm = '{preFixCho}', cancelledDate = {cancelDate}, " +
                                            $"memberStatus = '{memStaCho}', " +
                                            $"birthDate = {birthDateinput}, " +
                                            $"memberType = '{memTypeCho}', " +
                                            $"appliedDate = {AppliedInput}, " +
                                            $"updatedBy = '{updateuid}' {updateNAToA} " +
                                            $"WHERE memberid = '{showfirstMem}' \n\n" +

                                            $"UPDATE privateAddress " +
                                            $"SET address = N'{comAddinput}', phone = '{comPhoneinput}', fax = '{comFaxinput}', companyNm = N'{comNminput}' " +
                                            $"WHERE memberid = '{showfirstMem}' AND addressType = '2' \n\n" +
                                            
                                            $"UPDATE privateAddress " +
                                            $"SET address = N'{Addinput}', phone = '{Phoneinput}', mobile = '{Mobileinput}' " +
                                            $"WHERE memberid = '{showfirstMem}' AND addressType = '1' \n\n" +
                                            
                                            $"UPDATE PrivateClub " +
                                            $"SET golf = '{chkGolf}', children = '{chkChild}', board = '{chkBoard}', zukuzuku = '{chkSukuzuku}', lady = '{chkLady}', ev_1 = '{chkEngtest}', ev_2 = '{chkOnevent}', ev_3 = '{chkSoftball}', ev_4 = '{chkYoga}', ev_tmp1 = '{chkValue1}', ev_tmp2 = '{chkValue2}', ev_tmp3 = '{chkValue3}', sub_board_list = '{chkBoardlist}', sub_secretary = '{chkClubSecre}', sub_volunteer = '{chkBaVolun}', sub_social = '{chkSocialMem}', sub_member = '{chkYouthMem}', sub_tmp1 = '{chkValue4}', sub_tmp2 = '{chkValue5}', ov_member = '{chkOverseasMem}' " +
                                            $"WHERE memberid = '{showfirstMem}' " +
                                            $"IF NOT EXISTS (SELECT * FROM PrivateRemark " +
                                            $"WHERE memberid = '{showfirstMem}') " +
                                            $"BEGIN INSERT INTO PrivateRemark " +
                                            $"VALUES('{showfirstMem}','') END \n\n" +
                                            
                                            $"UPDATE PrivateRemark " +
                                            $"SET remark = N'{remarkInput}' " +
                                            $"WHERE memberid = '{showfirstMem}' \n\n" +

                                            $"INSERT INTO PrivateBoard (memberId) " +
                                            $"VALUES ('{showfirstMem}') \n\n" +

                                            $"UPDATE PrivateBoard " +
                                            $"SET sortBoard = '{sortBoardinput}', " +
                                            $"sortLady = '{sortLadyinput}', " +
                                            $"boardPosition = '{positioninput}' " +
                                            $"WHERE memberid = '{showfirstMem}' " +
                                            $"{insertToHistory}";

                            td = SelectSqlTable(_queryStr);

                            
							string activityDetail = $"Changed value in 6 tables ('Private, PrivateDetail, privateAddress, PrivateClub, PrivateRemark, PrivateBoard') where memberid is '{showfirstMem}' " +
                                $"and Added new data into 2 tables ('PrivateBoard, PrivateSendHistory') successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
                            string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
                        }
                        catch (Exception ex)
                        {
                            string activityDetail = $@"Error: {ex.Message}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
						}

						Response.Redirect("privateEntry.aspx?firstmemberid=" + Box2.Value + "&memberid=" + Box2.Value);
                        //}
                        //catch (SqlException ex)
                        //{
                        //    if (ex.Number == 2627)
                        //    {
                        //        lbError.Text = "Duplicate member id.";
                        //    }
                        //    else
                        //    {
                        //        lbError.Text = "Database error: input may not be in the proper format.";
                        //    }
                        //}
                    }
                    else
                    {
                        try
                        {
                            // *** 2024-08-27 at 02.49pm :
                            //     Toon Jiradech.K have revised the sql transaction statement from string
                            //     concatenate to string interpolate. Because, It's easier to read and
                            //     debugging the code.

                            // *** The original code from 2022. It's too difficult to read and debugging. ****
                            #region "The original code from 2022"
                            //td = SelectSqlTable("SET dateformat dmy " +
                            //	        "UPDATE Private " +
                            //	        "SET changeAddressDate = GETDATE() " +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" + " and (not exists (select * from privateAddress where address = N'" + comAddinput + "' and phone = '" + comPhoneinput + "' and fax = '" + comFaxinput + "' and companyNm = N'" + comNminput + "' and addressType = '2') or " +
                            //	        "not exists (select * from privateAddress where address = N'" + Addinput + "' and phone = '" + Phoneinput + "' and mobile = '" + Mobileinput + "' and addressType = '1'))" +
                            //	        "UPDATE Private " +
                            //	        "SET birthplace = " + "N'" + birthPlaceinput + "'" + "," + "checkmember = " + "' '" + "," + "checkMainName = " + "' '" + "," + "checkBirthPlace = " + "' '" + "," + "checkCompanyNm = " + "' '" + "," + "checkCompanyAddress = " + "' '" + "," + "checkCompanyPhone = " + "' '" + "," + "checkCompanyFax = " + "' '" + "," + "checkHomeAddress = " + "' '" + "," + "checkHomePhone = " + "' '" + "," + "checkHomeMobile = " + "' '" + "," + "getSplitPayment = " + "'" + chkPayment + "'" + "," + "zip_code = " + "'" + zipcode + "'" + "," + "email = " + "'" + emailVal + "'" + updateSendMethod +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" +
                            //	        "UPDATE PrivateDetail " +
                            //	        "SET nameJ = " + "N'" + nameJinput + "'" + "," + "nameE = " + "'" + nameEinput + "'" + "," + "prefixNm = " + "'" + preFixCho + "'" + "," + "cancelledDate = " + cancelDate + "," + "memberStatus = " + "'" + memStaCho + "'" + "," + "birthDate = " + "'" + birthDateinput + "'" + "," + "memberType = " + "'" + memTypeCho + "'" + "," + "appliedDate = " + "'" + AppliedInput + "'" + "," + "updatedBy = " + "'" + updateuid + "'" + updateNAToA +// chkEngtest + "'" + "," + "'" + chkOnevent + "'" + "," + "'" + chkSoftball + "'" + "," + "'" + chkYoga + "'" + "," + "'" + chkValue1 + "'" + "," + "'" + chkValue2 + "'" + "," + "'" + chkValue3 + "'" + "," + "'" + chkBoardlist + "'" + "," + "'" + chkClubSecre + "'" + "," + "'" + chkBaVolun + "'" + "," + "'" + chkSocialMem + "'" + "," + "'" + chkYouthMem + "'" + "," + "'" + chkValue4 + "'" + "," + "'" + chkValue5 + "'" + "," + "'" + chkOverseasMem + "'" + ")");
                            //	        																																																																																																													//"SET nameJ = " + "N'" + nameJinput + "'" + "," + "nameE = " + "'" + nameEinput + "'" + "," + "prefixNm = " + "'" + preFixCho + "'" + "," + "cancelledDate = " + cancelDate + "," + "memberStatus = " + "'" + memStaCho + "'" + "," + "birthDate = " + "'" + birthDateinput + "'" + "," + "memberType = " + "'" + memTypeCho + "'" + "," + "appliedDate = " + "'" + AppliedInput + "'" + "," + "updatedBy = " + "'" + updateuid + "'" +// chkEngtest + "'" + "," + "'" + chkOnevent + "'" + "," + "'" + chkSoftball + "'" + "," + "'" + chkYoga + "'" + "," + "'" + chkValue1 + "'" + "," + "'" + chkValue2 + "'" + "," + "'" + chkValue3 + "'" + "," + "'" + chkBoardlist + "'" + "," + "'" + chkClubSecre + "'" + "," + "'" + chkBaVolun + "'" + "," + "'" + chkSocialMem + "'" + "," + "'" + chkYouthMem + "'" + "," + "'" + chkValue4 + "'" + "," + "'" + chkValue5 + "'" + "," + "'" + chkOverseasMem + "'" + ")");
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" +
                            //	        "UPDATE privateAddress " +
                            //	        "SET address = " + "N'" + comAddinput + "'" + "," + "phone = " + "'" + comPhoneinput + "'" + "," + "fax = " + "'" + comFaxinput + "'" + "," + "companyNm = " + "N'" + comNminput + "'" +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" + "AND addressType = '2'" +
                            //	        "UPDATE privateAddress " +
                            //	        "SET address = " + "N'" + Addinput + "'" + "," + "phone = " + "'" + Phoneinput + "'" + "," + "mobile = " + "'" + Mobileinput + "'" +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" + "AND addressType = '1'" +
                            //	        "UPDATE PrivateClub " +
                            //	        "SET golf = " + "'" + chkGolf + "'" + "," + "children = " + "'" + chkChild + "'" + "," + "board = " + "'" + chkBoard + "'" + "," + "zukuzuku = " + "'" + chkSukuzuku + "'" + "," + "lady = " + "'" + chkLady + "'" + "," + "ev_1 = " + "'" + chkEngtest + "'" + "," + "ev_2 = " + "'" + chkOnevent + "'" + "," + "ev_3 = " + "'" + chkSoftball + "'" + "," + "ev_4 = " + "'" + chkYoga + "'" + "," + "ev_tmp1 = " + "'" + chkValue1 + "'" + "," + "ev_tmp2 = " + "'" + chkValue2 + "'" + "," + "ev_tmp3 = " + "'" + chkValue3 + "'" + "," + "sub_board_list = " + "'" + chkBoardlist + "'" + "," + "sub_secretary = " + "'" + chkClubSecre + "'" + "," + "sub_volunteer = " + "'" + chkBaVolun + "'" + "," + "sub_social = " + "'" + chkSocialMem + "'" + "," + "sub_member = " + "'" + chkYouthMem + "'" + "," + "sub_tmp1 = " + "'" + chkValue4 + "'" + "," + "sub_tmp2 = " + "'" + chkValue5 + "'" + "," + "ov_member = " + "'" + chkOverseasMem + "'" +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" +
                            //	        "IF NOT EXISTS (SELECT * FROM PrivateRemark WHERE memberid = '" + showfirstMem + "') BEGIN INSERT INTO PrivateRemark VALUES('" + showfirstMem + "','') END " +
                            //	        "UPDATE PrivateRemark " +
                            //	        "SET remark = " + "N'" + remarkInput + "'" +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" +
                            //	        "UPDATE PrivateBoard " +
                            //	        "SET sortBoard = " + "'" + sortBoardinput + "'" + "," + "sortLady = " + "'" + sortLadyinput + "'" + "," + "boardPosition = " + "'" + positioninput + "'" +
                            //	        "WHERE memberid =" + "'" + showfirstMem + "'" +
                            //	        insertToHistory);

                            // *** End of the original code ***
                            #endregion

                            // *** This is the part of code revised ****
                            #region "This is the part of code revised on 2024-08-27"


                            var _sqlTrans = $"--SET dateformat dmy \n\n" +
                                            $"UPDATE Private \n" +
                                            $"  SET changeAddressDate = GETDATE() \n" +
                                            $"WHERE memberid = '{showfirstMem}' \n" +
                                            $"  AND (NOT EXISTS (SELECT * FROM privateAddress \n" +
                                            $"                      WHERE address = N'{comAddinput}' \n" +
                                            $"                              AND phone = '{comPhoneinput}' \n" +
                                            $"                              AND fax = '{comFaxinput}' \n" +
                                            $"                              AND companyNm = N'{comNminput}' \n" +
                                            $"                              AND addressType = '2') \n" +
                                            $"  OR NOT EXISTS (SELECT * FROM privateAddress \n" +
                                            $"                      WHERE address = N'{Addinput}' \n" +
                                            $"                              AND phone = '{Phoneinput}' \n" +
                                            $"                              AND mobile = '{Mobileinput}' \n" +
                                            $"                              AND addressType = '1') \n" +
                                            $" ) \n\n" +

                                            $"UPDATE Private " +
                                            $"  SET birthplace = N'{birthPlaceinput}',     \n" +
                                            $"      checkmember = ' ', \n" +
                                            $"      checkMainName = ' ', \n" +
                                            $"      checkBirthPlace = ' ', \n" +
                                            $"      checkCompanyNm = ' ', \n" +
                                            $"      checkCompanyAddress = ' ', \n" +
                                            $"      checkCompanyPhone = ' ', \n" +
                                            $"      checkCompanyFax = ' ', \n" +
                                            $"      checkHomeAddress = ' ', \n" +
                                            $"      checkHomePhone = ' ', \n" +
                                            $"      checkHomeMobile = ' ', \n" +
                                            $"      getSplitPayment = '{chkPayment}', \n" +
                                            $"      zip_code = '{zipcode}', \n" +
                                            $"      email = '{emailVal}', " +
                                            $"      sendType = '{sendMethodCho}'  \n" +
                                            $"WHERE memberid = '{showfirstMem}' \n\n";

                            var _privateDetail = $"UPDATE PrivateDetail \n" +
                                              $"  SET nameJ = N'{nameJinput}', \n" +
                                              $"      nameE = '{nameEinput}', \n" +
                                              $"      prefixNm = '{preFixCho}', \n";

                            if (cancelDate == "Null")
                            {
                                _privateDetail += $"      cancelledDate = {cancelDate}, \n";
                            }
                            else
                            {
                                // var _cancelDate = Date_MsSqlStandard.CastQuery(cancelDate);
                                _privateDetail += $"      cancelledDate = GETDATE(), \n";
                            }

                            _privateDetail += $"      memberStatus = '{memStaCho}', \n" +
                                              $"      birthDate = {birthDateinput}, \n" +
                                              $"      memberType = '{memTypeCho}', \n" +
                                              $"      appliedDate = {AppliedInput}, \n" +
                                              $"      updatedBy = '{updateuid}' \n" +
                                              // $", '{updateNAToA}'
                                              $"WHERE memberid = '{showfirstMem}' \n\n";

                            var _sqlTrans1 = $"UPDATE privateAddress " +
                                            $"  SET address = N'{comAddinput}', \n" +
                                            $"      phone = '{comPhoneinput}', \n" +
                                            $"      fax = '{comFaxinput}', \n" +
                                            $"      companyNm = N'{comNminput}' \n" +
                                            $"WHERE memberid = '{showfirstMem}' \n" +
                                            $"      AND addressType = '2' \n\n" +

                                            $"UPDATE privateAddress \n" +
                                            $"  SET address = N'{Addinput}', \n" +
                                            $"      phone = '{Phoneinput}', \n" +
                                            $"      mobile = '{Mobileinput}' \n" +
                                            $"WHERE memberid = '{showfirstMem}' \n" +
                                            $"      AND addressType = '1' \n\n" +

                                            $"UPDATE PrivateClub \n" +
                                            $"  SET golf = '{chkGolf}', \n" +
                                            $"      children = '{chkChild}', \n" +
                                            $"      board = '{chkBoard}', \n" +
                                            $"      zukuzuku = '{chkSukuzuku}', \n" +
                                            $"      lady = '{chkLady}', \n" +
                                            $"      ev_1 = '{chkEngtest}', \n" +
                                            $"      ev_2 = '{chkOnevent}', \n" +
                                            $"      ev_3 = '{chkSoftball}', \n" +
                                            $"      ev_4 = '{chkYoga}', \n" +
                                            $"      ev_tmp1 = '{chkValue1}', \n" +
                                            $"      ev_tmp2 = '{chkValue2}', \n" +
                                            $"      ev_tmp3 = '{chkValue3}', \n" +
                                            $"      sub_board_list = '{chkBoardlist}', \n" +
                                            $"      sub_secretary = '{chkClubSecre}', \n" +
                                            $"      sub_volunteer = '{chkBaVolun}', \n" +
                                            $"      sub_social = '{chkSocialMem}', \n" +
                                            $"      sub_member = '{chkYouthMem}', \n" +
                                            $"      sub_tmp1 = '{chkValue4}', \n" +
                                            $"      sub_tmp2 = '{chkValue5}', \n" +
                                            $"      ov_member = '{chkOverseasMem}' \n" +
                                            $"WHERE memberid = '{showfirstMem}' \n\n" +

                                            $"IF NOT EXISTS (SELECT * FROM PrivateRemark \n" +
                                            $"      WHERE memberid = '{showfirstMem}') \n" +
                                            $"BEGIN \n" +
                                            $"  INSERT INTO PrivateRemark VALUES('{showfirstMem}', '') \n" +
                                            $"END \n\n" +

                                            $"UPDATE PrivateRemark SET remark = N'{remarkInput}' \n" +
                                            $"WHERE memberid = '{showfirstMem}' \n\n" +

                                            $"UPDATE PrivateBoard \n" +
                                            $"  SET sortBoard = '{sortBoardinput}', \n" +
                                            $"      sortLady = '{sortLadyinput}', \n" +
                                            $"      boardPosition = '{positioninput}' \n" +
                                            $"WHERE memberid = '{showfirstMem}'; \n";
                            //{insertToHistory}
                            _sqlTrans += _privateDetail + _sqlTrans1;

                            td = SelectSqlTable(_sqlTrans);
                            // *** End of revised **** 
                            #endregion

                            string activityDetail = $"Changed data in 6 tables ('Private, PrivateDetail, " +
                                $"privateAddress, PrivateClub, PrivateRemark, PrivateBoard') where memberid is '{showfirstMem}' successful (user id = {staffID})";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
                            string activityDetail = $@"Sql Error: {ex.ErrorCode} {ex.Message}";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $@"Error: {ex.Message}";
                            logActivity.LogStaffActivity(staffID, activityDetail);
						}

						Response.Redirect("privateEntry.aspx?firstmemberid=" + Box2.Value + "&memberid=" + Box2.Value);
                        //}
                        //catch (SqlException ex)
                        //{
                        //    if (ex.Number == 2627)
                        //    {
                        //        lbError.Text = "Duplicate member id.";
                        //    }
                        //    else
                        //    {
                        //        lbError.Text = "Database error: input may not be in the proper format.";
                        //    }
                        //}
                    }


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have to select send method.')", true);
                }
            }
            else if (confirmValue == "Yes" && checkinput != "Valid")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + checkinput + "')", true);
            }
        }
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
                    Box34.DataSource = subjects;
                    Box34.DataValueField = "MemberType";
                    Box34.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            Box34.Items.Insert(0, new ListItem("--Any--", "0"));
            Box34.SelectedIndex = 1;
        }
        protected string checkInput()
        {
            string result = "Valid";
            int tint;
            DateTime tdatetime;
            Int32 tint32;
            string etxt = "Please check input at ";
            while (result == "Valid")
            {
                if (Box2.Value.ToString().Trim() == "")
                {
                    etxt += "Member Id";
                    result = etxt;
                    break;
                }

                if (!int.TryParse(Box2.Value, out tint))
                {
                    etxt += "Member Id";
                    result = etxt;
                    break;
                }

                if(this.Box33.SelectedIndex == 0) // sendType's value is "--Any--"
                {
                    etxt += "Send Method";
                    this.Box33.BackColor = Color.Red;
                    result = etxt;
                    break;
                }

                if (this.Box34.SelectedIndex == 0) // memberType's value is "--Any--"
                {
                    etxt += "Member Type";
                    this.Box34.BackColor = Color.Red;
                    result = etxt;
                    break;
                }

                this.Box33.BackColor = this.Box34.BackColor = Color.White;

                break;
            }
            return result;
        }
    }
}