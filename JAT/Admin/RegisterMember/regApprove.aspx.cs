using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using JAT.Core;


namespace JAT.Admin.RegisterMember
{
    public partial class regApprove : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private LogActivity logActivity = new LogActivity();

        //public string postBackId;


        private string reg_id;
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {

            reg_id = Request.QueryString["registerid"];
            type = Request.QueryString["type"];
            checkApprove();
            if (reg_id != null)
            {
                if (!Page.IsPostBack)
                {
                    SelectTypeRegister();
                    RemarkPayment();
                    showInGridFist();
                    showInGridFamily();
                    showInGridChild();
                    checkApprove();

                }
            }

        }
        private void SelectTypeRegister()
        {
            if (type != null)
            {
                if (type == "2")
                {
                    Label3.Text = "Register Type : ";
                    Label4.Text = "Re-enroll / 再入会";
                }
                if (type == "1")
                {
                    Label3.Text = "Register Type : ";
                    Label4.Text = "New enrollment / 新規入会";
                }
            }
            else
            {
                Label3.Text = "";
            }
        }
        protected void RemarkPayment()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT register_payment,register_remark FROM RegisterPrivate WHERE register_id = '" + reg_id + "' ";
			conn.Open();
			sc = new SqlCommand(sql, conn);
			try
            {
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    if (rd.GetValue(0).ToString() == "private")
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
        private void checkApprove()
        {
            DataTable td;
            td = SelectSqlTable("SELECT register_status FROM RegisterPrivate WHERE register_id = '" + reg_id + "' AND register_status = 'AP' ");
            if (td.Rows.Count > 0)
            {
                saveBtn.Enabled = false;
            }
        }
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
        protected void showInGridFamily()
        {
            DataTable td;

            td = SelectSqlTable("SELECT run_num,familymember_id, nameJp, CONCAT(prefixNm, nameEn) AS nameE, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(appliedDate, 'dd/MM/yyyy') AS appliedDate, memberType, memberStatus, ev_1, ev_2, ev_3, ev_4, ev_tmp1, ev_tmp2, ev_tmp3, board, sub_board_list, golf, lady, sub_secretary, sub_volunteer, sub_social, sub_member, sub_tmp1, sub_tmp2, zukuzuku, children, ov_member  " +
                                "FROM RegisterFamMem " +
                                "WHERE register_id = " + "'" + reg_id + "'");

            GridView1.DataSource = td;

            GridView1.DataBind();
            conn.Close();

        }
        protected void showInGridChild()
        {
            DataTable td;

            td = SelectSqlTable("SELECT run_num, gender, nameJp, nameEn, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate " +
                                "FROM RegisterChildMem " +
                                "WHERE register_id = '" + reg_id + "'");
            GridView2.DataSource = td;
            GridView2.DataBind();
            conn.Close();

        }
        protected void showInGridFist()
        {
            DataTable td;
            td = SelectSqlTable("SELECT run_num,firstmember_id, nameJp, CONCAT(prefixNm, nameEn) AS nameE, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(appliedDate, 'dd/MM/yyyy') AS appliedDate, memberType, memberStatus, ev_1, ev_2, ev_3, ev_4, ev_tmp1, ev_tmp2, ev_tmp3, board, sub_board_list, golf, lady, sub_secretary, sub_volunteer, sub_social, sub_member, sub_tmp1, sub_tmp2, zukuzuku, children, ov_member  " +
                                           "FROM RegisterFirstMem " +
                                           "WHERE register_id = " + "'" + reg_id + "'");
            GridView3.DataSource = td;
            GridView3.DataBind();
            conn.Close();

        }
        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (reg_id != null)
            {
                Response.Redirect("regEntryCheck.aspx?registerid=" + reg_id);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (reg_id != null)
            {
                Response.Redirect("regKidCheck.aspx?registerid=" + reg_id);
            }
        }
        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (reg_id != null)
            {
                Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id);
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {

                var chkFirstMember = new DataTable();
                chkFirstMember = SelectSqlTable("SELECT firstmember_id FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");
                string member = "";
                var chkMember = new DataTable();
                chkMember = SelectSqlTable("SELECT familymember_id FROM RegisterFamMem WHERE register_id = '" + reg_id + "'");
                member = "(";
                for (int i = 0; i < chkMember.Rows.Count; i++)
                {
                    member += chkMember.Rows[i][0].ToString().Trim() + ",";
                }

                int lenmem = 0;
                lenmem = member.Length - 1;
                member = member.Substring(0, lenmem);
                member += ")";
                System.Diagnostics.Debug.Write(member);
                // Check Duplicate first member
                //var checkduplicateFirst = new DataTable();
                //checkduplicateFirst = SelectSqlTable("SELECT * FROM Private WHERE memberid = '" + chkFirstMember.Rows[0][0].ToString().Trim() + "'");

                //var checkduplicateFirst1 = new DataTable();
                //checkduplicateFirst1 = SelectSqlTable("SELECT * FROM PrivateDetail WHERE memberid = '" + chkFirstMember.Rows[0][0].ToString().Trim() + "'");
                System.Diagnostics.Debug.Write("SELECT * FROM PrivateDetail WHERE memberid IN  " + member.Trim() + "");
                //// Check Duplicate Family member
                var checkduplicateFam = new DataTable();
                if (chkMember.Rows.Count > 0)
                {
                    checkduplicateFam = SelectSqlTable("SELECT * FROM PrivateDetail WHERE memberid IN  " + member.Trim() + "");
                }



                string checkfnull = "";
                if (GridView3.Rows[0].Cells[0].Text.Trim() == "") { checkfnull = "N"; }
                string checkfamnull = "";
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (GridView1.Rows[i].Cells[0].Text.Trim() == "")
                    {
                        checkfamnull = "N";
                    }
                    else if (GridView1.Rows[i].Cells[0].Text.Trim() == GridView3.Rows[0].Cells[0].Text.Trim())
                    {
                        checkfamnull = "D";
                    }
                }
                if (checkfnull != "" || checkfamnull != "")
                {
                    if (checkfnull != "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "insertFID();", true);
                    }
                    if (checkfamnull == "N")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "insertFamID();", true);
                    }
                    if (checkfamnull == "D")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "DupicateFamID();", true);
                    }
                }
                else
                {
                    //if (checkduplicateFirst.Rows.Count > 0 || checkduplicateFirst1.Rows.Count > 0)
                    //{
                    //    if (checkduplicateFirst1.Rows.Count > 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "DupicateFINDB();", true);
                    //    }
                    //    if (checkduplicateFirst.Rows.Count > 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "DupicateFINDB();", true);
                    //    }


                    //}
                   if (checkduplicateFam.Rows.Count > 0)
                    {
                        if (checkduplicateFam.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "DupicateFamINDB();", true);
                        }
                    }
                    else
                    {
                        InserttoDB();
                        ApproveMemberLogin();
                        checkApprove();
                        Response.Redirect("~/Admin/RegisterMember/regCheck.aspx");
                    }
                }


            }
        }


        private void InserttoDB()
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            //Information First Member
            var firstmember_id = "SELECT rp.register_id, mu.memberId " +
                           "FROM RegisterPrivate rp " +
                           "LEFT JOIN m_user mu ON rp.register_email = mu.USER_EMAIL " +
                           "WHERE rp.register_id = '" + reg_id + "'";
            DataTable result_member = SelectSqlTable(firstmember_id);
            if (result_member.Rows.Count > 0)
            {
                firstmember_id = result_member.Rows[0]["memberId"].ToString().Trim();
            }
            var FirstMember_Data = new DataTable();
            FirstMember_Data = SelectSqlTable("SELECT * FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");
            string firstmember_update = "";
            if (FirstMember_Data.Rows.Count > 0)
            {
                firstmember_update = FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim();
            }
            //Information Familly Member
            var Family_Data = new DataTable();
            Family_Data = SelectSqlTable("SELECT * FROM RegisterFamMem WHERE register_id = '" + reg_id + "'");
            string Family_update = "";
            if (Family_Data.Rows.Count > 0)
            {
                Family_update = Family_Data.Rows[0]["familymember_id"].ToString().Trim();
            }
            //Information Kid
            var Kid_Data = new DataTable();
            Kid_Data = SelectSqlTable("SELECT * FROM RegisterChildMem WHERE register_id = '" + reg_id + "'");
            string Children_member = "";
            if (Kid_Data.Rows.Count > 0)
            {
                Children_member = Kid_Data.Rows[0][0].ToString().Trim();
            }
            DataTable td;
			
            string activityDetail = "";

            try
            {
                //    td = SelectSqlTable("SET dateformat mdy INSERT INTO PrivateDetail(firstmemberid, memberid, nameJ, nameE, prefixNm, memberStatus, birthDate, memberType, appliedDate)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["nameJp"].ToString().Trim() + "'" + "," +
                //                                 "'" + FirstMember_Data.Rows[0]["nameJp"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["prefixNm"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["memberStatus"].ToString().Trim() + "'" + "," +
                //                                 "'" + FirstMember_Data.Rows[0]["birthDate"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["memberType"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["appliedDate"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivateSendHistory(memberId, sendType)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sendType"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivateRemark(memberId, remark)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["remark"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivateRefer(memberid)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivatePayment(memberid)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivateBoard(memberId, sortBoard, sortLady, boardPosition)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sortBoard"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sortLady"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["boardPosition"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO privateAddress(memberid, addressType, address, phone, mobile)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + "1" + "'" + "," + "'" + FirstMember_Data.Rows[0]["homeAdd"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["homeTel"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["homeMobile"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO privateAddress(memberid, addressType, companyNm, address, phone, fax)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + "2" + "'" + "," + "N'" + FirstMember_Data.Rows[0]["companyName"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["companyAdd"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["companyTel"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["companyFax"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivateAccount(memberId)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO Private(memberid, birthPlace, sendType, checkmember, checkMainName, checkBirthPlace, checkCompanyNm, checkCompanyAddress, checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, checkHomeMobile, getSplitPayment,email,zip_code)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + FirstMember_Data.Rows[0]["birthPlace"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sendType"].ToString().Trim() + "'" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "' '" + "," + "'" + FirstMember_Data.Rows[0]["checkShort"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["email"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ZipCode"].ToString().Trim() + "'" + ")" +

                //                                 "INSERT INTO PrivateClub(memberid, golf, children, board, zukuzuku, lady, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member)" +
                //                                 "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["golf"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["children"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["board"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["zukuzuku"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["lady"].ToString().Trim() + "'" + "," +
                //                                 "'" + FirstMember_Data.Rows[0]["ev_1"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_2"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_3"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_4"].ToString().Trim() + "'" + "," +
                //                                 "'" + FirstMember_Data.Rows[0]["ev_tmp1"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_tmp2"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ev_tmp3"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_board_list"].ToString().Trim() + "'" + "," +
                //                                 "'" + FirstMember_Data.Rows[0]["sub_secretary"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_volunteer"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_social"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_member"].ToString().Trim() + "'" + "," +
                //                                 "'" + FirstMember_Data.Rows[0]["sub_tmp1"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["sub_tmp2"].ToString().Trim() + "'" + "," + "'" + FirstMember_Data.Rows[0]["ov_member"].ToString().Trim() + "'" + ")");
                // อัปเดตในตาราง PrivateDetail
                td = SelectSqlTable("UPDATE PrivateDetail " +
                                    "SET firstmemberid = '" + firstmember_update + "', " +
                                    "memberid = '" + firstmember_update + "', " +
                                    "nameJ = N'" + FirstMember_Data.Rows[0]["nameJp"].ToString().Trim() + "', " +
                                    "nameE = '" + FirstMember_Data.Rows[0]["nameJp"].ToString().Trim() + "', " +
                                    "prefixNm = '" + FirstMember_Data.Rows[0]["prefixNm"].ToString().Trim() + "', " +
                                    "memberStatus = '" + FirstMember_Data.Rows[0]["memberStatus"].ToString().Trim() + "', " +
                                    "birthDate = CONVERT(DATE, '" + FirstMember_Data.Rows[0]["birthDate"].ToString().Trim() + "', 103), " +
                                    "memberType = '" + FirstMember_Data.Rows[0]["memberType"].ToString().Trim() + "', " +
                                    "appliedDate = CONVERT(DATE, '" + FirstMember_Data.Rows[0]["appliedDate"].ToString().Trim() + "', 103) " +
                                    "WHERE firstmemberid = '" + firstmember_id + "'");
                // อัปเดตในตาราง PrivateSendHistory
                td = SelectSqlTable("UPDATE PrivateSendHistory " +
                                    "SET memberId = '" + firstmember_update + "', " +
                                    "sendType = '" + FirstMember_Data.Rows[0]["sendType"].ToString().Trim() + "' " +
                                    "WHERE memberId = '" + firstmember_id + "'");
                // อัปเดตในตาราง PrivateRemark
                td = SelectSqlTable("UPDATE PrivateRemark " +
                                    "SET memberid = '" + firstmember_update + "', " +
                                    "remark = N'" + FirstMember_Data.Rows[0]["remark"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");
                // อัปเดตในตาราง PrivateRefer
                td = SelectSqlTable("UPDATE PrivateRefer " +
                                    "SET memberid = '" + firstmember_update + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");
            // อัปเดตในตาราง PrivatePayment
                td = SelectSqlTable("UPDATE PrivatePayment " +
                                    "SET memberid = '" + firstmember_update + "', " + 
                                    "payBy = '" + firstmember_update + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");
            // อัปเดตในตาราง PrivateBoard
                td = SelectSqlTable("UPDATE PrivateBoard " +
                                    "SET memberid = '" + firstmember_update + "', " +
                                    "sortBoard = '" + FirstMember_Data.Rows[0]["sortBoard"].ToString().Trim() + "', " +
                                    "sortLady = '" + FirstMember_Data.Rows[0]["sortLady"].ToString().Trim() + "', " +
                                    "boardPosition = '" + FirstMember_Data.Rows[0]["boardPosition"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");
                // อัปเดตในตาราง privateAddress สำหรับ addressType = 1 (บ้าน) // ไม่ผ่าน
                td = SelectSqlTable("UPDATE privateAddress " +
                                    "SET memberid = '" + firstmember_update + "', " +
                                    "address = '" + FirstMember_Data.Rows[0]["homeAdd"].ToString().Trim() + "', " +
                                    "phone = '" + FirstMember_Data.Rows[0]["homeTel"].ToString().Trim() + "', " +
                                    "mobile = '" + FirstMember_Data.Rows[0]["homeMobile"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "' " +
                                    "AND addressType = '1'");
                // อัปเดตในตาราง privateAddress สำหรับ addressType = 2 (บริษัท) ไม่ผ่าน
                td = SelectSqlTable("UPDATE privateAddress " +
                                    "SET memberid = '" + firstmember_update + "', " +
                                    "companyNm = N'" + FirstMember_Data.Rows[0]["companyName"].ToString().Trim() + "', " +
                                    "address = N'" + FirstMember_Data.Rows[0]["companyAdd"].ToString().Trim() + "', " +
                                    "phone = '" + FirstMember_Data.Rows[0]["companyTel"].ToString().Trim() + "', " +
                                    "fax = '" + FirstMember_Data.Rows[0]["companyFax"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "' " +
                                    "AND addressType = '2'");
                // อัปเดตในตาราง PrivateAccount 
                td = SelectSqlTable("UPDATE PrivateAccount " +
                                    "SET memberId = '" + firstmember_update + "' " +
                                    "WHERE memberId = '" + firstmember_id + "'");
                // อัปเดตในตาราง Private
                td = SelectSqlTable("UPDATE Private " +
                                    "SET memberid = '" + firstmember_update + "', " +
                                    "birthPlace = N'" + FirstMember_Data.Rows[0]["birthPlace"].ToString().Trim() + "', " +
                                    "sendType = '" + FirstMember_Data.Rows[0]["sendType"].ToString().Trim() + "', " +
                                    "checkmember = ' ', " +
                                    "checkMainName = ' ', " +
                                    "checkBirthPlace = ' ', " +
                                    "checkCompanyNm = ' ', " +
                                    "checkCompanyAddress = ' ', " +
                                    "checkCompanyPhone = ' ', " +
                                    "checkCompanyFax = ' ', " +
                                    "checkHomeAddress = ' ', " +
                                    "checkHomePhone = ' ', " +
                                    "checkHomeMobile = ' ', " +
                                    "getSplitPayment = '" + FirstMember_Data.Rows[0]["checkShort"].ToString().Trim() + "', " +
                                    "email = '" + FirstMember_Data.Rows[0]["email"].ToString().Trim() + "', " +
                                    "zip_code = '" + FirstMember_Data.Rows[0]["ZipCode"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");
            // อัปเดตในตาราง PrivateClub
                td = SelectSqlTable("UPDATE PrivateClub " +
                                    "SET memberId = '" + firstmember_update + "', " +
                                    "golf = '" + FirstMember_Data.Rows[0]["golf"].ToString().Trim() + "', " +
                                    "children = '" + FirstMember_Data.Rows[0]["children"].ToString().Trim() + "', " +
                                    "board = '" + FirstMember_Data.Rows[0]["board"].ToString().Trim() + "', " +
                                    "zukuzuku = '" + FirstMember_Data.Rows[0]["zukuzuku"].ToString().Trim() + "', " +
                                    "lady = '" + FirstMember_Data.Rows[0]["lady"].ToString().Trim() + "', " +
                                    "ev_1 = '" + FirstMember_Data.Rows[0]["ev_1"].ToString().Trim() + "', " +
                                    "ev_2 = '" + FirstMember_Data.Rows[0]["ev_2"].ToString().Trim() + "', " +
                                    "ev_3 = '" + FirstMember_Data.Rows[0]["ev_3"].ToString().Trim() + "', " +
                                    "ev_4 = '" + FirstMember_Data.Rows[0]["ev_4"].ToString().Trim() + "', " +
                                    "ev_tmp1 = '" + FirstMember_Data.Rows[0]["ev_tmp1"].ToString().Trim() + "', " +
                                    "ev_tmp2 = '" + FirstMember_Data.Rows[0]["ev_tmp2"].ToString().Trim() + "', " +
                                    "ev_tmp3 = '" + FirstMember_Data.Rows[0]["ev_tmp3"].ToString().Trim() + "', " +
                                    "sub_board_list = '" + FirstMember_Data.Rows[0]["sub_board_list"].ToString().Trim() + "', " +
                                    "sub_secretary = '" + FirstMember_Data.Rows[0]["sub_secretary"].ToString().Trim() + "', " +
                                    "sub_volunteer = '" + FirstMember_Data.Rows[0]["sub_volunteer"].ToString().Trim() + "', " +
                                    "sub_social = '" + FirstMember_Data.Rows[0]["sub_social"].ToString().Trim() + "', " +
                                    "sub_member = '" + FirstMember_Data.Rows[0]["sub_member"].ToString().Trim() + "', " +
                                    "sub_tmp1 = '" + FirstMember_Data.Rows[0]["sub_tmp1"].ToString().Trim() + "', " +
                                    "sub_tmp2 = '" + FirstMember_Data.Rows[0]["sub_tmp2"].ToString().Trim() + "', " +
                                    "ov_member = '" + FirstMember_Data.Rows[0]["ov_member"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");


            activityDetail = $"Added new data into 10 tables ('PrivateDetail, PrivateSendHistory, PrivateRemark, PrivateRefer, PrivatePayment, PrivateBoard, privateAddress, PrivateAccount, Private and PrivateClub') successful (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
            catch (SqlException ex)
            {
                activityDetail = $"Added new data into 10 tables ('PrivateDetail, PrivateSendHistory, PrivateRemark, PrivateRefer, PrivatePayment, PrivateBoard, privateAddress, PrivateAccount, Private and PrivateClub') unsuccessful [{ex.Message}] (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
			catch (Exception ex)
			{
				activityDetail = $"Added new data into 10 tables ('PrivateDetail, PrivateSendHistory, PrivateRemark, PrivateRefer, PrivatePayment, PrivateBoard, privateAddress, PrivateAccount, Private and PrivateClub') unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}



            DataTable td_family;
            for (int i = 0; i < Family_Data.Rows.Count; i++)
            {

                try
                {
                    //td_family = SelectSqlTable(//Private_Family
                    //                "SET dateformat dmy INSERT INTO PrivateDetail(appliedDate, nameJ, nameE, prefixNm, memberStatus, birthDate, memberType, memberid, firstmemberid, spouse,email) " +
                    //                "VALUES('" + Family_Data.Rows[i]["appliedDate"].ToString().Trim() + "'" + "," + "N'" + Family_Data.Rows[i]["nameJp"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["nameEn"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["prefixNm"].ToString().Trim() + "'" + "," +
                    //                "'" + Family_Data.Rows[i]["memberStatus"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["birthDate"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["memberType"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["familymember_id"].ToString().Trim() + "'" + "," +
                    //                "'" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["spouse"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["email"].ToString().Trim() + "'" + ") " +

                    //                "INSERT INTO privateAddress(phone, mobile, addressType, memberid) " +
                    //                "VALUES('" + Family_Data.Rows[i]["homeTel"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["mobile"].ToString().Trim() + "'" + "," + "'" + "1" + "'" + "," + "'" + Family_Data.Rows[i]["familymember_id"].ToString().Trim() + "'" + ") " +
                    //                "INSERT INTO PrivateClub(golf, board, lady, children, zukuzuku, memberid, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member) " +
                    //                "VALUES('" + Family_Data.Rows[i]["golf"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["board"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["lady"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["children"].ToString().Trim() + "'" + "," +
                    //                "'" + Family_Data.Rows[i]["zukuzuku"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["familymember_id"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_1"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_2"].ToString().Trim() + "'" + "," +
                    //                "'" + Family_Data.Rows[i]["ev_3"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_4"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_tmp1"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ev_tmp2"].ToString().Trim() + "'" + "," +
                    //                "'" + Family_Data.Rows[i]["ev_tmp3"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_board_list"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_secretary"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_volunteer"].ToString().Trim() + "'" + "," +
                    //                "'" + Family_Data.Rows[i]["sub_social"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_member"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_tmp1"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["sub_tmp2"].ToString().Trim() + "'" + "," + "'" + Family_Data.Rows[i]["ov_member"].ToString().Trim() + "'" + ")");

                    td_family = SelectSqlTable("UPDATE PrivateDetail " +
                                    "SET memberid = '" + Family_update + "', " +
                                    "appliedDate = CONVERT(DATE, '" + Family_Data.Rows[i]["appliedDate"].ToString().Trim() + "', 103), " +
                                    "nameJ = N'" + Family_Data.Rows[i]["nameJp"].ToString().Trim() + "', " +
                                    "nameE = '" + Family_Data.Rows[i]["nameEn"].ToString().Trim() + "', " +
                                    "prefixNm = '" + Family_Data.Rows[i]["prefixNm"].ToString().Trim() + "', " +
                                    "memberStatus = '" + Family_Data.Rows[i]["memberStatus"].ToString().Trim() + "', " +
                                    "birthdate = CONVERT(DATE, '" + Family_Data.Rows[i]["birthDate"].ToString().Trim() + "', 103), " +
                                    "memberType = '" + Family_Data.Rows[i]["memberType"].ToString().Trim() + "', " +
                                    "spouse = '" + Family_Data.Rows[i]["spouse"].ToString().Trim() + "', " +
                                    "email = '" + Family_Data.Rows[i]["email"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");

                    // Update privateAddress
                    td_family = SelectSqlTable("UPDATE privateAddress " +
                                    "SET memberid = '" + Family_update + "', " +
                                    "phone = '" + Family_Data.Rows[i]["homeTel"].ToString().Trim() + "', " +
                                    "mobile = '" + Family_Data.Rows[i]["mobile"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");

                    // Update PrivateClub
                    td_family = SelectSqlTable("UPDATE PrivateClub " +
                                    "SET memberid = '" + Family_update + "', " +
                                    "golf = '" + Family_Data.Rows[i]["golf"].ToString().Trim() + "', " +
                                    "board = '" + Family_Data.Rows[i]["board"].ToString().Trim() + "', " +
                                    "lady = '" + Family_Data.Rows[i]["lady"].ToString().Trim() + "', " +
                                    "children = '" + Family_Data.Rows[i]["children"].ToString().Trim() + "', " +
                                    "zukuzuku = '" + Family_Data.Rows[i]["zukuzuku"].ToString().Trim() + "', " +
                                    "ev_1 = '" + Family_Data.Rows[i]["ev_1"].ToString().Trim() + "', " +
                                    "ev_2 = '" + Family_Data.Rows[i]["ev_2"].ToString().Trim() + "', " +
                                    "ev_3 = '" + Family_Data.Rows[i]["ev_3"].ToString().Trim() + "', " +
                                    "ev_4 = '" + Family_Data.Rows[i]["ev_4"].ToString().Trim() + "', " +
                                    "ev_tmp1 = '" + Family_Data.Rows[i]["ev_tmp1"].ToString().Trim() + "', " +
                                    "ev_tmp2 = '" + Family_Data.Rows[i]["ev_tmp2"].ToString().Trim() + "', " +
                                    "ev_tmp3 = '" + Family_Data.Rows[i]["ev_tmp3"].ToString().Trim() + "', " +
                                    "sub_board_list = '" + Family_Data.Rows[i]["sub_board_list"].ToString().Trim() + "', " +
                                    "sub_secretary = '" + Family_Data.Rows[i]["sub_secretary"].ToString().Trim() + "', " +
                                    "sub_volunteer = '" + Family_Data.Rows[i]["sub_volunteer"].ToString().Trim() + "', " +
                                    "sub_social = '" + Family_Data.Rows[i]["sub_social"].ToString().Trim() + "', " +
                                    "sub_member = '" + Family_Data.Rows[i]["sub_member"].ToString().Trim() + "', " +
                                    "sub_tmp1 = '" + Family_Data.Rows[i]["sub_tmp1"].ToString().Trim() + "', " +
                                    "sub_tmp2 = '" + Family_Data.Rows[i]["sub_tmp2"].ToString().Trim() + "', " +
                                    "ov_member = '" + Family_Data.Rows[i]["ov_member"].ToString().Trim() + "' " +
                                    "WHERE memberid = '" + firstmember_id + "'");

                    activityDetail = $"Added new data into 3 tables ('PrivateDetail, privateAddress and PrivateClub') successful (User id = '{staffID}')";

                }
                catch (SqlException ex)
                {
                    activityDetail = $"Added new data into 3 tables ('PrivateDetail, privateAddress and PrivateClub') unsuccessful [{ex.Message}] (User id = '{staffID}')";
                    break;
                }
				catch (Exception ex)
				{
					activityDetail = $"Added new data into 3 tables ('PrivateDetail, privateAddress and PrivateClub') unsuccessful [{ex.Message}] (User id = '{staffID}')";
					break;
				}
			}
            //Need if else for none fam insert
            if (Family_Data.Rows.Count > 0)
            {
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            
            DataTable td_kid;
            for (int j = 0; j < Kid_Data.Rows.Count; j++)
            {
                try
                {
                    //td_kid = SelectSqlTable(//Prive_Kid
                    //                "SET dateformat dmy INSERT INTO PrivateChild(memberid, nameKidJ, nameKidE, birthdate, prefixKid)" +
                    //                "VALUES('" + FirstMember_Data.Rows[0]["firstmember_id"].ToString().Trim() + "'" + "," + "N'" + Kid_Data.Rows[j]["nameJp"].ToString().Trim() + "'" + "," + "N'" + Kid_Data.Rows[j]["nameEn"].ToString().Trim() + "'" + "," + "'" + Kid_Data.Rows[j]["birthDate"].ToString().Trim() + "'" + "," + "'" + Kid_Data.Rows[j]["gender"].ToString().Trim() + "'" + ")");

                    td_kid = SelectSqlTable("UPDATE PrivateChild " +
                                            "SET memberid = '" + Children_member + "', " +
                                            "nameKidJ = N'" + Kid_Data.Rows[j]["nameJp"].ToString().Trim() + "', " +
                                            "nameKidE = N'" + Kid_Data.Rows[j]["nameEn"].ToString().Trim() + "', " +
                                            "birthdate = CONVERT(DATE, '" + Kid_Data.Rows[j]["birthDate"].ToString().Trim() + "', 103), " +
                                            "prefixKid = '" + Kid_Data.Rows[j]["gender"].ToString().Trim() + "' " +
                                            "WHERE memberid = '" + firstmember_id + "'");

                    activityDetail = $"Added new data into a table 'PrivateChild' successful (User id = '{staffID}')";
                }
                catch (SqlException ex)
                {
                    activityDetail = $"Added new data into a table 'PrivateChild' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                    break;
                }
				catch (Exception ex)
				{
					activityDetail = $"Added new data into a table 'PrivateChild' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					break;
				}
			}
			if (Kid_Data.Rows.Count > 0)
			{
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			


            //string activityDetail = $"Child private member (user id is {staffID})";
            //logActivity.LogStaffActivity(staffID, activityDetail);

        }

        private void ApproveMemberLogin()
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            var chkEmail = new DataTable();
            chkEmail = SelectSqlTable("SELECT register_email FROM RegisterPrivate WHERE register_id = '" + reg_id + "'");
            string email = "";
            string activityDetail = "";
            if (chkEmail.Rows.Count > 0)
            {
                email = chkEmail.Rows[0][0].ToString().Trim();
            }
            var chkFirstMember = new DataTable();
            chkFirstMember = SelectSqlTable("SELECT firstmember_id FROM RegisterFirstMem WHERE register_id = '" + reg_id + "'");
            string firstmember = "";
            if (chkFirstMember.Rows.Count > 0)
            {
                firstmember = chkFirstMember.Rows[0][0].ToString().Trim();
            }
            String date = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable UpdateRegister;

            try
            {
                UpdateRegister = SelectSqlTable("UPDATE RegisterPrivate SET register_status = 'AP',register_member = '" + firstmember.Trim() + "',register_approve = '" + date + "' WHERE register_id = '" + reg_id + "'");
                activityDetail = $"Changed value in a table 'RegisterPrivate' where register_id is '{reg_id}' successful (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
            catch (SqlException ex)
            {
                activityDetail = $"Changed value in a table 'RegisterPrivate' where register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
			catch (Exception ex)
			{
				activityDetail = $"Changed value in a table 'RegisterPrivate' where register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			DataTable Updateemail;

			try
			{
				Updateemail = SelectSqlTable("UPDATE m_user SET memberId = '" + firstmember + "',Check_status = 'O'  WHERE USER_EMAIL = '" + email + "'");
				activityDetail = $"Changed value in a table 'm_user' where USER_EMAIL is '{email}' successful (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
				activityDetail = $"Changed value in a table 'm_user' where USER_EMAIL is '{email}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				activityDetail = $"Changed value in a table 'm_user' where USER_EMAIL is '{email}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
		}
	}
}