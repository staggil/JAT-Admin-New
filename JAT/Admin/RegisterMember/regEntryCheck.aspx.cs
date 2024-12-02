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
namespace JAT.Admin.RegisterMember
{
    public partial class regEntryCheck : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();

		//public string postBackId;

		private string run_num;
        private string reg_id;
        private string type;
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
 

            run_num = Request.QueryString["runnum"];
            reg_id = Request.QueryString["registerid"];
             type = Request.QueryString["type"];
            addValue = Request.QueryString["mode"];
            
            DataTable td;
            td = SelectSqlTable("SELECT register_status  FROM RegisterPrivate WHERE register_id = '"+reg_id+"' AND register_status = 'AP' ");
            if(td.Rows.Count > 0)
            {
                Response.Redirect("~/Admin/RegisterMember/regApprove?registerid="+ reg_id + "");
            }
            else
            {
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
                if (run_num != null || reg_id != null)
                {

                    editBTN.Visible = true;
                    updateBtn.Visible = false;
                    cancelBtnMem.Visible = false;

                    if (!Page.IsPostBack)
                    {
                        SelectTypeRegister();
                        BindDataAddress1();
                        showInGrid();
                        BindDataCheckBox();
                        BindDataRemark();
                        BindDataCheckBoxTemp();
                        BindMemberTypeList();
                        BindData();
                        RemarkPayment();
                    }

                }
                else
                {
                    editBTN.Visible = false;
                    updateBtn.Visible = false;
                    cancelBtnMem.Visible = false;
                }
            }
            // Add Data function
           
             

        }
        private void SelectTypeRegister()
        {
            if(type != null)
            {
                if(type == "2")
                {
                    Label1.Text = "Register Type : ";
                    Label2.Text = "Re-enroll / 再入会";
                }
                if(type == "1")
                {
                    Label1.Text = "Register Type : ";
                    Label1.Text = "New enrollment / 新規入会";
                }
            }
            else
            {
                Label1.Text = "";
            }
        }
        private void EnabledForm()
        {
            BindData();


            if (run_num != null || reg_id != null)
            {
                updateBtn.Visible = true;
                cancelBtnMem.Visible = true;

            }
            else
            {
                //saveBtn.Visible = true;
                updateBtn.Visible = false;
                cancelBtnMem.Visible = false;

            }

            CheckBox1.Enabled = false;
            CheckBox2.Enabled = false;
            
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
            CheckBox1.Enabled = false;
            CheckBox2.Enabled = false;
            TextareaPayment.Attributes.Add("disabled", "disabled");
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
            SqlDataReader rd;

            string sql = "SELECT prefixNm, nameJp, nameEn, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate, companyAdd, companyTel, companyFax, companyName, firstmember_id, FORMAT(appliedDate, 'dd/MM/yyyy') AS appliedDate, " +
                        "birthPlace, remark, memberStatus, sendType, memberType, sortBoard, sortLady, boardPosition " +
                        "FROM RegisterFirstMem " +
                        "WHERE register_id = '" + reg_id + "' ";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in member information
                    Box2.Value = rd.GetValue(8).ToString();
                    Box3.Value = rd.GetValue(9).ToString();
                    Box5.Value = rd.GetValue(1).ToString();
                    string tmp = rd.GetValue(0).ToString();
                    if (tmp == "")
                    {
                        tmp = "-- ANY --";
                    }
                    preFixSel.SelectedValue = tmp;
                    Box6.Value = rd.GetValue(2).ToString();
                    Box8.Value = rd.GetValue(10).ToString();
                    Box9.Value = rd.GetValue(3).ToString();
                    Box11.Value = rd.GetValue(7).ToString();
                    Box13.Value = rd.GetValue(4).ToString();
                    Box15.Value = rd.GetValue(5).ToString();
                    Box17.Value = rd.GetValue(6).ToString();
                    Box38.Value = rd.GetValue(11).ToString();

                    string memstatus = rd.GetValue(12).ToString();
                    if (memstatus == "")
                    {
                        memstatus = "-- ANY --";
                    }
                    Box32.SelectedValue = memstatus;

                    string sendType = rd.GetValue(13).ToString();
                    if (sendType == "")
                    {
                        sendType = "-- ANY --";
                    }
                    Box33.SelectedValue = sendType;

                    string memType = rd.GetValue(14).ToString();
                    if (memType == "")
                    {
                        memType = "-- ANY --";
                    }
                    Box34.SelectedValue = memType;

                    Box35.Value = rd.GetValue(15).ToString();
                    Box36.Value = rd.GetValue(16).ToString();
                    Box37.Value = rd.GetValue(17).ToString();
                }

            }
            catch { }
            finally
            {
                //if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                //{
                //    updateBy.Text = "N/A";
                //}
            }

            //Box2.Value = showVal;


            conn.Close();
        }
        protected void RemarkPayment()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT register_payment,register_remark FROM RegisterPrivate WHERE register_id = '" + reg_id + "' ";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    if(rd.GetValue(0).ToString() == "private")
                    {
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        CheckBox2.Checked = true;
                    }
                    TextareaPayment.Value = rd.GetValue(1).ToString();
                    
                }

            }
            catch { }
            finally
            {
               
            }
            conn.Close();
        }
        protected void BindDataAddress1()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT homeAdd, homeTel, homeMobile, email, zipCode " +
                            "FROM RegisterFirstMem " +
                            "WHERE register_id = '" + reg_id + "'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in information Address
                    Box20.Value = rd.GetValue(0).ToString();
                    Box22.Value = rd.GetValue(1).ToString();
                    Box24.Value = rd.GetValue(2).ToString();
                    email.Value = rd.GetValue(3).ToString();
                    Box18.Value = rd.GetValue(4).ToString();
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

            string sql = "SELECT ev_1, ev_2, ev_3, ev_4, ev_tmp1, ev_tmp2, ev_tmp3, board, sub_board_list, golf, lady, sub_secretary, sub_volunteer, sub_social, sub_member, sub_tmp1, sub_tmp2, zukuzuku, children, ov_member, checkShort " +
                            "FROM RegisterFirstMem " +
                            "WHERE register_id = '" + reg_id + "'";
            //try
            //{
            conn.Open();
            sc = new SqlCommand(sql, conn);
            rd = sc.ExecuteReader();
            while (rd.Read())
            {

                if (rd.GetValue(0) == System.DBNull.Value || rd.GetValue(0).ToString().Trim() == "False")
                {
                    cbEngtest.Checked = false;
                }
                else
                {
                    cbEngtest.Checked = true;
                }

                if (rd.GetValue(1) == System.DBNull.Value || rd.GetValue(1).ToString().Trim() == "False")
                {
                    cbOnevent.Checked = false;
                }
                else
                {
                    cbOnevent.Checked = true;
                }

                if (rd.GetValue(2) == System.DBNull.Value || rd.GetValue(2).ToString().Trim() == "False")
                {
                    cbSoftball.Checked = false;
                }
                else
                {
                    cbSoftball.Checked = true;
                }

                if (rd.GetValue(3) == System.DBNull.Value || rd.GetValue(3).ToString().Trim() == "False")
                {
                    cbYoga.Checked = false;
                }
                else
                {
                    cbYoga.Checked = true;
                }

                if (rd.GetValue(4) == System.DBNull.Value || rd.GetValue(4).ToString().Trim() == "False")
                {
                    cbValue1.Checked = false;
                }
                else
                {
                    cbValue1.Checked = true;
                }

                if (rd.GetValue(5) == System.DBNull.Value || rd.GetValue(5).ToString().Trim() == "False")
                {
                    cbValue2.Checked = false;
                }
                else
                {
                    cbValue2.Checked = true;
                }

                if (rd.GetValue(6) == System.DBNull.Value || rd.GetValue(6).ToString().Trim() == "False")
                {
                    cbValue3.Checked = false;
                }
                else
                {
                    cbValue3.Checked = true;
                }

                if (rd.GetValue(7) == System.DBNull.Value || rd.GetValue(7).ToString().Trim() == "False")
                {
                    cbBoard.Checked = false;
                }
                else
                {
                    cbBoard.Checked = true;
                }

                if (rd.GetValue(8) == System.DBNull.Value || rd.GetValue(8).ToString().Trim() == "False")
                {
                    cbBoardlist.Checked = false;
                }
                else
                {
                    cbBoardlist.Checked = true;
                }

                if (rd.GetValue(9) == System.DBNull.Value || rd.GetValue(9).ToString().Trim() == "False")
                {
                    cbGolf.Checked = false;
                }
                else
                {
                    cbGolf.Checked = true;
                }

                if (rd.GetValue(10) == System.DBNull.Value || rd.GetValue(10).ToString().Trim() == "False")
                {
                    cbLady.Checked = false;
                }
                else
                {
                    cbLady.Checked = true;
                }

                if (rd.GetValue(11) == System.DBNull.Value || rd.GetValue(11).ToString().Trim() == "False")
                {
                    cbClubSecre.Checked = false;
                }
                else
                {
                    cbClubSecre.Checked = true;
                }

                if (rd.GetValue(12) == System.DBNull.Value || rd.GetValue(12).ToString().Trim() == "False")
                {
                    cbBaVolun.Checked = false;
                }
                else
                {
                    cbBaVolun.Checked = true;
                }

                if (rd.GetValue(13) == System.DBNull.Value || rd.GetValue(13).ToString().Trim() == "False")
                {
                    cbSocialMem.Checked = false;
                }
                else
                {
                    cbSocialMem.Checked = true;
                }

                if (rd.GetValue(14) == System.DBNull.Value || rd.GetValue(14).ToString().Trim() == "False")
                {
                    cbYouthMem.Checked = false;
                }
                else
                {
                    cbYouthMem.Checked = true;
                }

                if (rd.GetValue(15) == System.DBNull.Value || rd.GetValue(15).ToString().Trim() == "False")
                {
                    cbValue4.Checked = false;
                }
                else
                {
                    cbValue4.Checked = true;
                }

                if (rd.GetValue(16) == System.DBNull.Value || rd.GetValue(16).ToString().Trim() == "False")
                {
                    cbValue5.Checked = false;
                }
                else
                {
                    cbValue5.Checked = true;
                }

                if (rd.GetValue(17) == System.DBNull.Value || rd.GetValue(17).ToString().Trim() == "False")
                {
                    cbSukusukuMem.Checked = false;
                }
                else
                {
                    cbSukusukuMem.Checked = true;
                }

                if (rd.GetValue(18) == System.DBNull.Value || rd.GetValue(18).ToString().Trim() == "False")
                {
                    cbChildLibMem.Checked = false;
                }
                else
                {
                    cbChildLibMem.Checked = true;
                }

                if (rd.GetValue(19) == System.DBNull.Value || rd.GetValue(19).ToString().Trim() == "False")
                {
                    cbOverseasMem.Checked = false;
                }
                else
                {
                    cbOverseasMem.Checked = true;
                }

                if (rd.GetValue(20) == System.DBNull.Value || rd.GetValue(20).ToString().Trim() == "False")
                {
                    Box39.Checked = false;
                }
                else
                {
                    Box39.Checked = true;
                }
            }

            //}
            //catch { }
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
                            "WHERE memberid = '" + reg_id + "'" + "AND memberid = '" + reg_id + "'";
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


            //td = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(appliedDate, 'dd/MM/yyyy') AS appliedDate, FORMAT(cancelledDate, 'dd/MM/yyyy') AS cancelledDate, memberType, memberStatus " +
            //                    "FROM PrivateDetail " +
            //                    "WHERE firstmemberid =" + "'" + reg_id + "'" + "AND firstmemberid != memberid");


            td = SelectSqlTable("SELECT run_num,familymember_id, nameJp, CONCAT(prefixNm, nameEn) AS nameE, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate " +
                                "FROM RegisterFamMem " +
                                "WHERE register_id =" + "'" + reg_id + "'");

            GridView1.DataSource = td;

            GridView1.DataBind();

            conn.Close();

        }

        protected void familyTab_Click(object sender, EventArgs e)
        {
            var chkFirstMember = new DataTable();
            chkFirstMember = SelectSqlTable("SELECT firstmember_id FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");

            if (chkFirstMember.Rows[0][0].ToString().Trim().Length >= 1 && !String.IsNullOrEmpty(chkFirstMember.Rows[0][0].ToString().Trim()))
            {
                if (run_num != null || reg_id != null)
                {
                    Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "insertID();", true);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            var chkFirstMember = new DataTable();
            chkFirstMember = SelectSqlTable("SELECT firstmember_id FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");

            if (chkFirstMember.Rows[0][0].ToString().Trim().Length >= 1 && !String.IsNullOrEmpty(chkFirstMember.Rows[0][0].ToString().Trim()))
            {
                if (run_num != null || reg_id != null)
                {
                    Response.Redirect("regKidCheck.aspx?registerid=" + reg_id);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "insertID();", true);
            }
        }

        protected void Approve_Click(object sender, EventArgs e)
        {
            Response.Redirect("regApprove.aspx?registerid=" + reg_id);
            //string confirmValue = Request.Form["confirm_value"];
            //if (confirmValue == "Yes")
            //{
            //    //check familyID before approve

            //    var chkFirstMember = new DataTable();
            //    chkFirstMember = SelectSqlTable("SELECT firstmember_id FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");

            //    var FirstMember_Data = new DataTable();
            //    FirstMember_Data = SelectSqlTable("SELECT * FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");

            //    var Family_Data = new DataTable();
            //    Family_Data = SelectSqlTable("SELECT * FROM RegisterFamMem WHERE register_id = '" + reg_id + "'");

            //    var Kid_Data = new DataTable();
            //    Kid_Data = SelectSqlTable("SELECT * FROM RegisterChildMem WHERE register_id = '" + reg_id + "'");

            //    if (chkFirstMember.Rows[0][0].ToString().Trim().Length >= 1 && !String.IsNullOrEmpty(chkFirstMember.Rows[0][0].ToString().Trim()))
            //    {
            //        DataTable td;

            //        td = SelectSqlTable("UPDATE RegisterPrivate " +
            //                            "SET register_status = 'AP'" +
            //                            "WHERE register_id = '" + reg_id + "'" +
            //                            //private_detail
            //                            "SET dateformat dmy INSERT INTO PrivateDetail(firstmemberid, memberid, nameJ, nameE, prefixNm, memberStatus, birthDate, memberType, appliedDate)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["nameJp"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["nameJp"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["prefixNm"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["memberStatus"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["birthDate"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["memberType"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["appliedDate"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivateSendHistory(memberId, sendType)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sendType"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivateRemark(memberId, remark)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["remark"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivateRefer(memberid)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivatePayment(memberid)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivateBoard(memberId, sortBoard, sortLady, boardPosition)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sortBoard"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sortLady"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["boardPosition"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO privateAddress(memberid, addressType, address, phone, mobile)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + "1" + "'" + "," + "'" + FirstMember_Data.Rows[0]["homeAdd"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["homeTel"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["homeMobile"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO privateAddress(memberid, addressType, companyNm, address, phone, fax)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + "2" + "'" + "," + "N'" + FirstMember_Data.Rows[0]["companyName"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["companyAdd"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["companyTel"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["companyFax"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivateAccount(memberId)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO Private(memberid, birthPlace, sendType, checkmember, checkMainName, checkBirthPlace, checkCompanyNm, checkCompanyAddress, checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, checkHomeMobile, getSplitPayment,email,zip_code)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["birthPlace"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sendType"].ToString().Trim() + "'" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "'" + FirstMember_Data.Rows[0]["checkShort"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["email"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ZipCode"].ToString().Trim() + "'" + ")" +

            //                                "INSERT INTO PrivateClub(memberid, golf, children, board, zukuzuku, lady, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["golf"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["children"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["board"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["zukuzuku"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["lady"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["ev_1"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_2"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_3"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_4"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["ev_tmp1"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_tmp2"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_tmp3"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_board_list"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["sub_secretary"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_volunteer"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_social"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_member"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["sub_tmp1"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_tmp2"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ov_member"].ToString().Trim() + "'" + ")");

            //        DataTable td_family;
            //        for (int i = 0; i < Family_Data.Rows.Count; i++)
            //        {
            //            td_family = SelectSqlTable(//Private_Family
            //                                "SET dateformat dmy INSERT INTO PrivateDetail(appliedDate, nameJ, nameE, prefixNm, memberStatus, birthDate, memberType, memberid, firstmemberid, spouse,email) " +
            //                                "VALUES('" + Family_Data.Rows[i]["appliedDate"].ToString().Trim() + "'" + "," + "N'" + Family_Data.Rows[i]["nameJp"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["nameEn"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["prefixNm"].ToString().Trim() + "'" + "," +
            //                                "'" + Family_Data.Rows[i]["memberStatus"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["birthDate"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["memberType"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["familymember_id"].ToString().Trim() + "'" + "," +
            //                                "'" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["spouse"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["email"].ToString().Trim() + "'" + ") " +

            //                                "INSERT INTO privateAddress(phone, mobile, addressType, memberid) " +
            //                                "VALUES('" + Family_Data.Rows[i]["homeTel"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["mobile"].ToString().Trim() + "'" + "," + "'" + "1" + "'" + "," + "'" + Family_Data.Rows[i]["familymember_id"].ToString().Trim() + "'" + ") " +
            //                                "INSERT INTO PrivateClub(golf, board, lady, children, zukuzuku, memberid, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member) " +
            //                                "VALUES('" + Family_Data.Rows[i]["golf"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["board"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["lady"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["children"].ToString().Trim() + "'" + "," +
            //                                "'" + Family_Data.Rows[i]["zukuzuku"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["familymember_id"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_1"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_2"].ToString().Trim() + "'" + "," +
            //                                "'" + Family_Data.Rows[i]["ev_3"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_4"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_tmp1"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_tmp2"].ToString().Trim() + "'" + "," +
            //                                "'" + Family_Data.Rows[i]["ev_tmp3"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_board_list"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_secretary"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_volunteer"].ToString().Trim() + "'" + "," +
            //                                "'" + Family_Data.Rows[i]["sub_social"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_member"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_tmp1"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_tmp2"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ov_member"].ToString().Trim() + "'" + ")");

            //        }

            //        DataTable td_kid;
            //        for (int j = 0; j < Kid_Data.Rows.Count; j++)
            //        {
            //            td_kid = SelectSqlTable(//Prive_Kid
            //                                "SET dateformat dmy INSERT INTO PrivateChild(memberid, nameKidJ, nameKidE, birthdate, prefixKid)" +
            //                                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + Kid_Data.Rows[j]["nameJp"].ToString().Trim() + "'" + "," + "N'" + Kid_Data.Rows[j]["nameEn"].ToString().Trim() + "'" + "," + "'" + Kid_Data.Rows[j]["birthDate"].ToString().Trim() + "'" + "," + "'" + Kid_Data.Rows[j]["gender"].ToString().Trim() + "'" + ")");


            //        }

            //        Response.Redirect("regCheck.aspx");
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "insertID();", true);
            //    }

            //}
        }

        protected void Reject_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;
                try
                {
					td = SelectSqlTable("UPDATE RegisterPrivate " +
									"SET register_status = 'RJ'" +
									"WHERE register_id = '" + reg_id + "'");
                    string activityDetail = $"Changed value in a table 'RegisterPrivate' where register_id is '{reg_id}' successful (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
				}
                catch (SqlException ex)
                {
					string activityDetail = $"Changed value in a table 'RegisterPrivate' where register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Changed value in a table 'RegisterPrivate' where register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}


				Response.Redirect("regCheck.aspx");
            }
        }

        protected void editBTN_Click(object sender, EventArgs e)
        {
            EnabledForm();
            //Box2.Attributes.Add("disabled", "disabled");
            Box2.Attributes.Remove("disabled");

            editBTN.Visible = false;
        }

        protected void cancelBtnMem_Click(object sender, EventArgs e)
        {
            DisabledForm();

            Response.Redirect("regEntryCheck.aspx?registerid=" + reg_id);
        }

        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            Response.Redirect("regFamilyCheck.aspx?mode=edit&registerid=" + reg_id + "&runnum=" + row.Cells[0].Text);
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

                var memIDInput = Box2.Value.ToString();
                var AppliedInput = Box3.Value.ToString();
                var nameJinput = Box5.Value.ToString();
                var nameEinput = Box6.Value.ToString();
                var birthPlaceinput = Box8.Value.ToString();
                var birthDateinput = Box9.Value.ToString();
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

                var memStaCho = Box32.SelectedValue.ToString();
                var sendMethodCho = Box33.SelectedValue.ToString();
                var memTypeCho = Box34.SelectedValue.ToString();
                var sortBoardinput = Box35.Value;
                var sortLadyinput = Box36.Value;
                var positioninput = Box37.Value.ToString().Replace("'", "''");
                var remarkInput = Box38.Value.ToString().Replace("'", "''");
                var chkPayment = Box39.Checked;
                //var updateuid = Session["UID"];
                var emailVal = email.Value.ToString().Replace("'", "''");

                if (sendMethodCho != "any")
                {
                    try
                    {

                        var firstIdChk = new DataTable();
                        firstIdChk = SelectSqlTable("SELECT firstmember_id FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");
                        string fristChk = "";
                        if (String.IsNullOrEmpty(firstIdChk.Rows[0][0].ToString()))
                        {
                            fristChk = ", firstmember_id = " + "'" + memIDInput + "'";
                        }
                        else
                        {
                            //fristChk = "";
                            fristChk = ", firstmember_id = " + "'" + memIDInput + "'";
                        }
                        try
                        {
							td = SelectSqlTable("SET dateformat dmy " +
											"UPDATE RegisterFirstMem " +
											"SET prefixNm = " + "'" + preFixCho + "'" + "," + "nameJp = " + "'" + nameJinput + "'" + "," + "nameEn = " + "'" + nameEinput + "'" + "," + "birthDate = " + "'" + Date_MsSqlStandard.CastQuery(birthDateinput) + "'" + "," +
											"companyName = " + "'" + comNminput + "'" + "," + "companyAdd = " + "'" + comAddinput + "'" + "," + "companyTel = " + "'" + comPhoneinput + "'" + "," + "companyFax = " + "'" + comFaxinput + "'" + "," + "homeAdd = " + "'" + Addinput + "'" + "," +
											"homeTel = " + "'" + Phoneinput + "'" + "," + "homeMobile = " + "'" + Mobileinput + "'" + "," + "email = " + "'" + emailVal + "'" + "," + "birthPlace = " + "'" + Date_MsSqlStandard.CastQuery(birthPlaceinput) + "'" + "," + "zipCode = " + "'" + zipcode + "'" + "," + "appliedDate = " + "'" + Date_MsSqlStandard.CastQuery(AppliedInput) + "'" + "," +
											"ev_1 = " + "'" + chkEngtest + "'" + "," + "ev_2 = " + "'" + chkOnevent + "'" + "," + "ev_3 = " + "'" + chkSoftball + "'" + "," + "ev_4 = " + "'" + chkYoga + "'" + "," + "ev_tmp1 = " + "'" + chkValue1 + "'" + "," + "ev_tmp2 = " + "'" + chkValue2 + "'" + "," + "ev_tmp3 = " + "'" + chkValue3 + "'" + "," +
											"board = " + "'" + chkBoard + "'" + "," + "sub_board_list = " + "'" + chkBoardlist + "'" + "," + "golf = " + "'" + chkGolf + "'" + "," + "lady = " + "'" + chkLady + "'" + "," + "sub_secretary = " + "'" + chkClubSecre + "'" + "," + "sub_volunteer = " + "'" + chkBaVolun + "'" + "," +
											"sub_social = " + "'" + chkSocialMem + "'" + "," + "sub_member = " + "'" + chkYouthMem + "'" + "," + "sub_tmp1 = " + "'" + chkValue4 + "'" + "," + "sub_tmp2 = " + "'" + chkValue5 + "'" + "," + "zukuzuku = " + "'" + chkSukuzuku + "'" + "," +
											"children = " + "'" + chkChild + "'" + "," + "ov_member = " + "'" + chkOverseasMem + "'" + "," + "memberStatus = " + "'" + memStaCho + "'" + "," + "sendType = " + "'" + sendMethodCho + "'" + "," + "memberType = " + "'" + memTypeCho + "'" + "," + "sortBoard = " + "'" + sortBoardinput + "'" + "," +
											"sortLady = " + "'" + sortLadyinput + "'" + "," + "boardPosition = " + "'" + positioninput + "'" + "," + "remark = " + "'" + remarkInput + "'" + "," + "checkShort = " + "'" + chkPayment + "'" +
											fristChk +
											"WHERE register_id =" + "'" + reg_id + "'");
                            string activityDetail = $"Changed value in a table 'RegisterFirstMem' where register_id is'{reg_id}' successful (User id = '{staffID}')";
                            logActivity.LogStaffActivity(staffID, activityDetail);
						}
                        catch (SqlException ex)
                        {
							string activityDetail = $"Changed value in a table 'RegisterFirstMem' where register_id is'{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}
						catch (Exception ex)
						{
							string activityDetail = $"Changed value in a table 'RegisterFirstMem' where register_id is'{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
							logActivity.LogStaffActivity(staffID, activityDetail);
						}



						Response.Redirect("regEntryCheck.aspx?registerid=" + reg_id);
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
                catch (Exception)
                {
                    // Handle the error

                    //string activityDetail = $@"Error: {ex.Message} in {this}";
                    //logActivity.LogStaffActivity(staffID, activityDetail);
                }

            }
            Box34.Items.Insert(0, new ListItem("--Any--", "0"));
            Box34.SelectedIndex = 1;
        }
        protected string checkInput()
        {
            string result = "Valid";
            int tint;
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
                if (Box3.Value.ToString().Trim() == "")
                {
                    etxt += "Applied Date";
                    result = etxt;
                    break;
                }
                if (Box5.Value.ToString().Trim() == "")
                {
                    etxt += "Name Japan";
                    result = etxt;
                    break;
                }
                if (preFixSel.SelectedValue.ToString().Trim() == "" || preFixSel.SelectedValue.ToString().Trim() == "-- ANY --")
                {
                    etxt += "select a gender";
                    result = etxt;
                    break;
                }
                if (Box6.Value.ToString().Trim() == "")
                {
                    etxt += "Name English";
                    result = etxt;
                    break;
                }
                break;
            }
            return result;
        }
    }
}