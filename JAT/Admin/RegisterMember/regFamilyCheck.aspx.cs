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
    public partial class regFamilyCheck : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
        private SqlCommand cmd;

        //public string postBackId;

        private string run_num;
        private string reg_id;
        private string type;
        string addValue;

        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));
        string toDayDateSh = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
        public static string cancelDateTmp;
        protected void Page_Load(object sender, EventArgs e)
        {
            run_num = Request.QueryString["runnum"];
            reg_id = Request.QueryString["registerid"];
            addValue = Request.QueryString["mode"];
            type = Request.QueryString["type"];
            if (reg_id != null || run_num != null)
            {
                DataTable td;
                td = SelectSqlTable("SELECT register_status  FROM RegisterPrivate WHERE register_id = '" + reg_id + "' AND register_status = 'AP' ");
                if (td.Rows.Count > 0)
                {
                    Response.Redirect("~/Admin/RegisterMember/regApprove?registerid=" + reg_id + "");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        SelectTypeRegister();
                        Box6.Value = toDayDateSh;
                        HideForm();
                        lastEditor();
                        BindData();
                        showInGrid();
                        BindDataCheckBoxTemp();
                        BindMemberTypeList();
                        if (addValue == "add")
                        {
                            ShowForm();
                            Box7.SelectedValue = "7";
                        }
                        else if (addValue == "edit")
                        {
                            BindEditFormEntry();
                            saveBtn.Visible = false;
                        }
                    }
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
        protected void setGridHeader()
        {
            //DataTable td;
            //td = SelectSqlTable("SELECT * FROM privateClubDetail");
            //foreach (DataRow tmprow in td.Rows)
            //{
            //    switch (tmprow["itemNm"].ToString().Trim())
            //    {
            //        case "ev_tmp1": GridView1.HeaderRow.Cells[11].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
            //        case "ev_tmp2": GridView1.HeaderRow.Cells[12].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
            //        case "ev_tmp3": GridView1.HeaderRow.Cells[13].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
            //        case "sub_tmp1": GridView1.HeaderRow.Cells[22].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
            //        case "sub_tmp2": GridView1.HeaderRow.Cells[23].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
            //    }
            //}
            //if (GridView1.HeaderRow.Cells[11].Text == "&nbsp;")
            //{
            //    GridView1.HeaderRow.Cells[11].Visible = false;
            //    GridView1.Columns[11].Visible = false;
            //}
            //else
            //{
            //    GridView1.HeaderRow.Cells[11].Visible = true;
            //    GridView1.Columns[12].Visible = true;
            //}
            //if (GridView1.HeaderRow.Cells[12].Text == "&nbsp;")
            //{
            //    GridView1.HeaderRow.Cells[12].Visible = false;
            //    GridView1.Columns[12].Visible = false;
            //}
            //else
            //{
            //    GridView1.HeaderRow.Cells[12].Visible = true;
            //    GridView1.Columns[12].Visible = true;
            //}
            //if (GridView1.HeaderRow.Cells[13].Text == "&nbsp;")
            //{
            //    GridView1.HeaderRow.Cells[13].Visible = false;
            //    GridView1.Columns[13].Visible = false;
            //}
            //else
            //{
            //    GridView1.HeaderRow.Cells[13].Visible = true;
            //    GridView1.Columns[13].Visible = true;
            //}
            //if (GridView1.HeaderRow.Cells[22].Text == "&nbsp;")
            //{
            //    GridView1.HeaderRow.Cells[22].Visible = false;
            //    GridView1.Columns[22].Visible = false;
            //}
            //else
            //{
            //    GridView1.HeaderRow.Cells[22].Visible = true;
            //    GridView1.Columns[22].Visible = true;
            //}
            //if (GridView1.HeaderRow.Cells[23].Text == "&nbsp;")
            //{
            //    GridView1.HeaderRow.Cells[23].Visible = false;
            //    GridView1.Columns[23].Visible = false;
            //}
            //else
            //{
            //    GridView1.HeaderRow.Cells[23].Visible = true;
            //    GridView1.Columns[23].Visible = true;
            //}
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

        private void ShowForm()
        {
            addBTN.Visible = false;
            saveBtn.Visible = true;
            cancelBtn.Visible = true;

            Box1.Attributes.Remove("disabled");
            Box2.Attributes.Remove("disabled");
            Box3.Attributes.Remove("disabled");
            Box4.Attributes.Remove("disabled");
            Box5.Attributes.Remove("disabled");
            Box6.Attributes.Remove("disabled");
            Box7.Attributes.Remove("disabled");
            Box8.Attributes.Remove("disabled");
            Box9.Attributes.Remove("disabled");
            Box10.Attributes.Remove("disabled");

            //Box11.Enabled = true;
            //Box12.Enabled = true;
            //Box13.Enabled = true;
            //Box14.Enabled = true;
            //Box15.Enabled = true;
            //Box16.Enabled = true;

            cbEngtest.Enabled = true;
            cbOnevent.Enabled = true;
            cbSoftball.Enabled = true;
            cbYoga.Enabled = true;
            cbValue1.Enabled = true;
            cbValue2.Enabled = true;
            cbValue3.Enabled = true;

            cbBoard.Enabled = true;
            cbBoardlist.Enabled = true;
            cbGolf.Enabled = true;
            cbLady.Enabled = true;
            cbClubSecre.Enabled = true;
            cbBaVolun.Enabled = true;
            cbSocialMem.Enabled = true;
            cbYouthMem.Enabled = true;
            cbValue4.Enabled = true;
            cbValue5.Enabled = true;

            cbSukusukuMem.Enabled = true;
            cbChildLibMem.Enabled = true;

            cbOverseasMem.Enabled = true;

            Box17.Enabled = true;
            //Box18.Enabled = true;
            Box19.Attributes.Remove("disabled");

            RequiredFieldValidator1.Visible = true;
            RegularExpressionValidator1.Visible = true;
            RequiredFieldValidator2.Visible = true;
            RequiredFieldValidator3.Visible = true;
            RequiredFieldValidator4.Visible = true;
            //RegularExpressionValidator2.Visible = true;
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
        }

        private void HideForm()
        {
            saveBtn.Visible = false;
            cancelBtn.Visible = false;

            Box1.Attributes.Add("disabled", "disabled");
            Box2.Attributes.Add("disabled", "disabled");
            Box3.Attributes.Add("disabled", "disabled");
            Box4.Attributes.Add("disabled", "disabled");
            Box5.Attributes.Add("disabled", "disabled");
            Box6.Attributes.Add("disabled", "disabled");
            Box7.Attributes.Add("disabled", "disabled");
            Box8.Attributes.Add("disabled", "disabled");
            Box9.Attributes.Add("disabled", "disabled");
            Box10.Attributes.Add("disabled", "disabled");

            //Box11.Enabled = false;
            //Box12.Enabled = false;
            //Box13.Enabled = false;
            //Box14.Enabled = false;
            //Box15.Enabled = false;
            //Box16.Enabled = false;

            cbEngtest.Enabled = false;
            cbOnevent.Enabled = false;
            cbSoftball.Enabled = false;
            cbYoga.Enabled = false;
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

            Box17.Enabled = false;
            //Box18.Enabled = false;
            Box19.Attributes.Add("disabled", "disabled");

            RequiredFieldValidator1.Visible = false;
            RegularExpressionValidator1.Visible = false;
            RequiredFieldValidator2.Visible = false;
            RequiredFieldValidator3.Visible = false;
            RequiredFieldValidator4.Visible = false;
            //RegularExpressionValidator2.Visible = false;

        }

        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT nameJp, CONCAT(prefixNm, nameEn) " +
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
                    Label1.Text = rd.GetValue(0).ToString();
                    Label2.Text = rd.GetValue(1).ToString();

                }

            }
            catch { }

            //Box2.Value = showVal;


            conn.Close();
        }
        private void lastEditor()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT TOP(1) t2.staffFName FROM PrivateDetail t1 " +
                    "LEFT OUTER JOIN SStaff t2 ON updatedBy=staffID WHERE firstmemberid='" + run_num + "' " +
                    "ORDER BY t1.updatedDate DESC";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                //while (rd.Read())
                //{
                //    updateBy.Text = rd.GetValue(0).ToString();
                //}

            }
            catch { }
            finally
            {
                //if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                //{
                //    updateBy.Text = "N/A";
                //}
            }
        }
        protected void showInGrid()
        {
            DataTable td;

            td = SelectSqlTable("SELECT run_num,familymember_id, nameJp, CONCAT(prefixNm, nameEn) AS nameE, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(appliedDate, 'dd/MM/yyyy') AS appliedDate, memberType, memberStatus,  ev_2, ev_3, ev_4, ev_tmp1, ev_tmp2, ev_tmp3, board, sub_board_list, golf, lady, sub_secretary, sub_volunteer, sub_social, sub_member, sub_tmp1, sub_tmp2, zukuzuku, children, ov_member  " +
                                "FROM RegisterFamMem " +
                                "WHERE register_id = " + "'" + reg_id + "'");

            GridView1.DataSource = td;
            //ImageButton1.Visible = true;
            //ImageButton2.Visible = true;

            GridView1.DataBind();
            conn.Close();
            if (td.Rows.Count > 0)
            {
                setGridHeader();
            }
        }

        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (reg_id != null || run_num != null)
            {
                Response.Redirect("regEntryCheck.aspx?registerid=" + reg_id);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (reg_id != null || run_num != null)
            {
                Response.Redirect("regKidCheck.aspx?registerid=" + reg_id);
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
					string activityDetail = $"Changed data in a table 'RegisterPrivate' where register_id is '{reg_id}' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
				{
					string activityDetail = $"Changed data in a table 'RegisterPrivate' where register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Changed data in a table 'RegisterPrivate' where register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}

				Response.Redirect("regCheck.aspx");
            }
        }

        protected void GridView_EditButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect("privateEntryMember.aspx?firstmemberid=" + run_num +"&memberid=" + Box1.Value);
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            Response.Redirect("regFamilyCheck.aspx?mode=edit&registerid=" + reg_id + "&runnum=" + row.Cells[0].Text);

            ShowForm();
            addBTN.Visible = false;
            saveBtn.Visible = false;
            //Box1.Attributes.Add("disabled", "disabled");

            GridView1.Columns[14].Visible = false;
            GridView1.Columns[15].Visible = false;

            updateBtn.Visible = true;


            connection();
            SqlCommand sc;
            SqlDataReader rd;

            //string sql = "SELECT t1.memberid, t1.nameJ, t1.prefixNm, t1.nameE, FORMAT(t1.birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(t1.appliedDate, 'yyyy-MMM-dd') AS appliedDate, t1.memberType, t1.memberStatus, t2.phone, t2.mobile, t3.golf, t3.board, t3.lady, t3.children, t3.zukuzuku, t1.spouse, t4.checkFamilyName,email, " +
            //             "ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member " +
            //                "FROM PrivateDetail t1 " +
            //                "INNER JOIN privateAddress t2 on t1.memberid = t2.memberid INNER JOIN PrivateClub t3 on t1.memberid = t3.memberid INNER JOIN Private t4 on t1.memberid = t4.memberid WHERE t1.memberid = " + "'" + row.Cells[0].Text + "'" + "AND t1.firstmemberid != t1.memberid AND t2.addressType = '1' ";
            //string sql = "select d.firstmemberid, d.memberid, d.prefixNm, d.nameJ, d.nameE,FORMAT(d.birthDate, 'dd/MM/yyyy') AS birthDate,FORMAT(d.appliedDate, 'dd/MM/yyyy') AS appliedDate, " +
            //    "d.updatedDate,FORMAT(d.cancelledDate, 'dd/MM/yyyy') AS cancelledDate, d.memberStatus, d.memberType, d.updatedBy, d.locked, d.lockedBy, " +
            //    "c.memberid, c.golf, c.board, c.lady, c.children, c.zukuzuku, " +
            //    "ah.address as homeAddress, ah.phone as homePhone, " +
            //    "ah.mobile as mobile, ppp.checkFamilyName, spouse,d.email,ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member " +
            //    "from privateDetail d left join privateAddress ah on d.memberid = ah.memberid and ah.addresstype = 1 " +
            //    "left join privateClub c on d.memberid = c.memberid left join private ppp on d.memberid = ppp.memberid " +
            //    "where d.memberid = '" + row.Cells[0].Text + "' ";

            string sql = "SELECT * , FORMAT(birthDate, 'dd/MM/yyyy'), FORMAT(appliedDate, 'dd/MM/yyyy')" +
                            "FROM RegisterFamMem " +
                            "WHERE run_num = '" + row.Cells[0].Text + "' ";

            //try
            //{
            conn.Open();
            sc = new SqlCommand(sql, conn);
            rd = sc.ExecuteReader();

            while (rd.Read())
            {
                Box1.Value = rd.GetValue(2).ToString();
                Box2.Value = rd.GetValue(4).ToString();
                string tmp = rd.GetValue(3).ToString();
                if (tmp == "")
                {
                    tmp = "-- ANY --";
                }
                Box3.SelectedValue = tmp;
                Box4.Value = rd.GetValue(5).ToString();
                Box5.Value = rd.GetValue(34).ToString();
                Box6.Value = rd.GetValue(35).ToString();
                string memType = rd.GetValue(11).ToString();
                if (memType == "")
                {
                    memType = "99";
                }
                Box7.SelectedValue = memType;

                //cancelDateTmp = rd.GetValue(8).ToString().Trim();

                string memStatus = rd.GetValue(12).ToString();
                if (memStatus == "")
                {
                    memStatus = "A";
                }
                Box8.SelectedValue = memStatus;

                Box9.Value = rd.GetValue(10).ToString();
                Box10.Value = rd.GetValue(7).ToString();
                Box19.Value = rd.GetValue(8).ToString();

                bool chkDataInDB12 = (bool)rd.GetValue(22);
                cbGolf.Checked = chkDataInDB12;

                bool chkDataInDB13 = (bool)rd.GetValue(20);
                cbBoard.Checked = chkDataInDB13;

                bool chkDataInDB14 = (bool)rd.GetValue(23);
                cbLady.Checked = chkDataInDB14;

                bool chkDataInDB15 = (bool)rd.GetValue(31);
                cbChildLibMem.Checked = chkDataInDB15;

                bool chkDataInDB16 = (bool)rd.GetValue(30);
                cbSukusukuMem.Checked = chkDataInDB16;

                bool chkDataInDB17 = (bool)rd.GetValue(13);
                cbEngtest.Checked = chkDataInDB17;

                bool chkDataInDB18 = (bool)rd.GetValue(14);
                cbOnevent.Checked = chkDataInDB18;

                bool chkDataInDB19 = (bool)rd.GetValue(15);
                cbSoftball.Checked = chkDataInDB19;

                bool chkDataInDB20 = (bool)rd.GetValue(16);
                cbYoga.Checked = chkDataInDB20;

                bool chkDataInDB21 = (bool)rd.GetValue(17);
                cbValue1.Checked = chkDataInDB21;

                bool chkDataInDB22 = (bool)rd.GetValue(18);
                cbValue2.Checked = chkDataInDB22;

                bool chkDataInDB23 = (bool)rd.GetValue(19);
                cbValue3.Checked = chkDataInDB23;

                bool chkDataInDB24 = (bool)rd.GetValue(21);
                cbBoardlist.Checked = chkDataInDB24;

                bool chkDataInDB25 = (bool)rd.GetValue(24);
                cbClubSecre.Checked = chkDataInDB25;

                bool chkDataInDB26 = (bool)rd.GetValue(25);
                cbBaVolun.Checked = chkDataInDB26;

                bool chkDataInDB27 = (bool)rd.GetValue(26);
                cbSocialMem.Checked = chkDataInDB27;

                bool chkDataInDB28 = (bool)rd.GetValue(27);
                cbYouthMem.Checked = chkDataInDB28;

                bool chkDataInDB29 = (bool)rd.GetValue(28);
                cbValue4.Checked = chkDataInDB29;

                bool chkDataInDB30 = (bool)rd.GetValue(29);
                cbValue5.Checked = chkDataInDB30;

                bool chkDataInDB31 = (bool)rd.GetValue(32);
                cbOverseasMem.Checked = chkDataInDB31;


                // check spouse
                int chkDataInDB7 = (int)rd.GetValue(33);
                if (chkDataInDB7 == 1)
                {
                    Box17.Checked = true;
                }

                //bool chkDataInDB8 = (bool)rd.GetValue(17);
                //Box18.Checked = chkDataInDB8;

                //DataTable td;
                //td = SelectSqlTable("SELECT * FROM privateClubDetail");
                //foreach (DataRow tmprow in td.Rows)
                //{
                //    switch (tmprow["itemNm"].ToString().Trim())
                //    {
                //        case "ev_tmp1": ev_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "ev_tmp2": ev_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "ev_tmp3": ev_tmp3.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "sub_tmp1": sub_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "sub_tmp2": sub_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //    }
                //}

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
        protected void GridView_DeleteButton_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            //td = SelectSqlTable("DELETE FROM PrivateDetail " +
            //                    "WHERE memberid = " + "'" + row.Cells[0].Text + "'" +
            //                    "DELETE FROM Private " +
            //                    "WHERE memberid = " + "'" + row.Cells[0].Text + "'" +
            //                    "DELETE FROM privateAddress " +
            //                    "WHERE memberid = " + "'" + row.Cells[0].Text + "'");
            try
            {
                // *** 2024-09-06 02.17pm : Toon Jiradech.K Toon Jiradech.k have changed code from hard deleting to soft deleting
                #region 'Hard Deleting'
                //       td = SelectSqlTable("DELETE FROM RegisterFamMem " +
                //"WHERE run_num = " + "'" + row.Cells[0].Text + "'" + "AND register_id = " + "'" + reg_id + "'");
                #endregion

                #region 'Soft Deleting'
                td = SelectSqlTable($"UPDATE RegisterFamMem SET Deleted_at = GETDATE() " +
                                    $"WHERE run_num = '{row.Cells[0].Text}' AND register_id = '{reg_id}'");
                #endregion
                // *** End of Revised
                string activityDetail = $"Soft deleted data in a table 'RegisterFamMem' where run_num is '{row.Cells[0].Text}' and register_id is '{reg_id}' successful (User id = '{reg_id}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
            {
				//string activityDetail = $"Deleted data in a table 'RegisterFamMem' where run_num is '{row.Cells[0].Text}' and register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{reg_id}')";
				logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                    $"{ex.Message}");
			}
			catch (Exception ex)
			{
                //string activityDetail = $"Deleted data in a table 'RegisterFamMem' where run_num is '{row.Cells[0].Text}' and register_id is '{reg_id}' unsuccessful [{ex.Message}] (User id = '{reg_id}')";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
            }

			Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id);

        }

        protected void BindEditFormEntry()
        {
            HideForm();
            Box1.Attributes.Add("disabled", "disabled");

            connection();
            SqlCommand sc;
            SqlDataReader rd;

            //string sql = "SELECT t1.memberid, t1.nameJ, t1.prefixNm, t1.nameE, FORMAT(t1.birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(t1.appliedDate, 'yyyy-MMM-dd') AS appliedDate, t1.memberType, t1.memberStatus, t2.phone, t2.mobile, t3.golf, t3.board, t3.lady, t3.children, t3.zukuzuku, t1.spouse, t4.checkFamilyName,email, " +
            //             "ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member " +
            //                "FROM PrivateDetail t1 " +
            //                "INNER JOIN privateAddress t2 on t1.memberid = t2.memberid INNER JOIN PrivateClub t3 on t1.memberid = t3.memberid INNER JOIN Private t4 on t1.memberid = t4.memberid WHERE t1.memberid = " + "'" + reg_id + "'" + "AND t1.firstmemberid != t1.memberid AND t2.addressType = '1' ";
            string sql = "SELECT * , FORMAT(birthDate, 'dd/MM/yyyy'), FORMAT(appliedDate, 'dd/MM/yyyy')" +
                            "FROM RegisterFamMem " +
                            "WHERE run_num = '" + run_num + "' ";

            //try
            //{
            conn.Open();
            sc = new SqlCommand(sql, conn);
            rd = sc.ExecuteReader();

            while (rd.Read())
            {
                Box1.Value = rd.GetValue(2).ToString();
                Box2.Value = rd.GetValue(4).ToString();
                string tmp = rd.GetValue(3).ToString();
                if (tmp == "")
                {
                    tmp = "-- ANY --";
                }
                Box3.SelectedValue = tmp;
                Box4.Value = rd.GetValue(5).ToString();
                Box5.Value = rd.GetValue(34).ToString();
                Box6.Value = rd.GetValue(35).ToString();
                string memType = rd.GetValue(11).ToString();
                if (memType == "")
                {
                    memType = "99";
                }
                Box7.SelectedValue = memType;

                //cancelDateTmp = rd.GetValue(8).ToString().Trim();

                string memStatus = rd.GetValue(12).ToString();
                if (memStatus == "")
                {
                    memStatus = "A";
                }
                Box8.SelectedValue = memStatus;

                Box9.Value = rd.GetValue(10).ToString();
                Box10.Value = rd.GetValue(7).ToString();
                Box19.Value = rd.GetValue(8).ToString();
                if (rd.GetValue(13).ToString().Trim() != "")
                {
                    bool chkDataInDB17 = (bool)rd.GetValue(13);
                    cbEngtest.Checked = chkDataInDB17;
                }
                if (rd.GetValue(14).ToString().Trim() != "")
                {
                    bool chkDataInDB18 = (bool)rd.GetValue(14);
                    cbOnevent.Checked = chkDataInDB18;
                }
                if (rd.GetValue(15).ToString().Trim() != "")
                {
                    bool chkDataInDB19 = (bool)rd.GetValue(15);
                    cbSoftball.Checked = chkDataInDB19;
                }
                if (rd.GetValue(16).ToString().Trim() != "")
                {
                    bool chkDataInDB20 = (bool)rd.GetValue(16);
                    cbYoga.Checked = chkDataInDB20;
                }
                if (rd.GetValue(17).ToString().Trim() != "")
                {
                    bool chkDataInDB21 = (bool)rd.GetValue(17);
                    cbValue1.Checked = chkDataInDB21;
                }
                if (rd.GetValue(18).ToString().Trim() != "")
                {
                    bool chkDataInDB22 = (bool)rd.GetValue(18);
                    cbValue2.Checked = chkDataInDB22;
                }
                if (rd.GetValue(19).ToString().Trim() != "")
                {
                    bool chkDataInDB23 = (bool)rd.GetValue(19);
                    cbValue3.Checked = chkDataInDB23;
                }
                if (rd.GetValue(20).ToString().Trim() != "")
                {
                    bool chkDataInDB13 = (bool)rd.GetValue(20);
                    cbBoard.Checked = chkDataInDB13;
                }
                if (rd.GetValue(21).ToString().Trim() != "")
                {
                    bool chkDataInDB24 = (bool)rd.GetValue(21);
                    cbBoardlist.Checked = chkDataInDB24;
                }
                if (rd.GetValue(22).ToString().Trim() != "")
                {
                    bool chkDataInDB12 = (bool)rd.GetValue(22);
                    cbGolf.Checked = chkDataInDB12;
                }
                if (rd.GetValue(23).ToString().Trim() != "")
                {
                    bool chkDataInDB14 = (bool)rd.GetValue(23);
                    cbLady.Checked = chkDataInDB14;
                }
                if (rd.GetValue(24).ToString().Trim() != "")
                {
                    bool chkDataInDB25 = (bool)rd.GetValue(24);
                    cbClubSecre.Checked = chkDataInDB25;
                }
                if (rd.GetValue(25).ToString().Trim() != "")
                {
                    bool chkDataInDB26 = (bool)rd.GetValue(25);
                    cbBaVolun.Checked = chkDataInDB26;
                }
                if (rd.GetValue(26).ToString().Trim() != "")
                {
                    bool chkDataInDB27 = (bool)rd.GetValue(26);
                    cbSocialMem.Checked = chkDataInDB27;
                }
                if (rd.GetValue(27).ToString().Trim() != "")
                {
                    bool chkDataInDB28 = (bool)rd.GetValue(27);
                    cbYouthMem.Checked = chkDataInDB28;
                }
                if (rd.GetValue(28).ToString().Trim() != "")
                {
                    bool chkDataInDB29 = (bool)rd.GetValue(28);
                    cbValue4.Checked = chkDataInDB29;
                }
                if (rd.GetValue(29).ToString().Trim() != "")
                {
                    bool chkDataInDB30 = (bool)rd.GetValue(29);
                    cbValue5.Checked = chkDataInDB30;
                }
                if (rd.GetValue(30).ToString().Trim() != "")
                {
                    bool chkDataInDB16 = (bool)rd.GetValue(30);
                    cbSukusukuMem.Checked = chkDataInDB16;
                }
                if (rd.GetValue(31).ToString().Trim() != "")
                {
                    bool chkDataInDB15 = (bool)rd.GetValue(31);
                    cbChildLibMem.Checked = chkDataInDB15;
                }
                if (rd.GetValue(32).ToString().Trim() != "")
                {
                    bool chkDataInDB31 = (bool)rd.GetValue(32);
                    cbOverseasMem.Checked = chkDataInDB31;
                }
                // check spouse
                if (rd.GetValue(33).ToString().Trim() != "")
                {
                    int chkDataInDB7 = (int)rd.GetValue(33);
                    if (chkDataInDB7 == 1)
                    {
                        Box17.Checked = true;
                    }
                }
                

                //bool chkDataInDB8 = (bool)rd.GetValue(17);
                //Box18.Checked = chkDataInDB8;

                //DataTable td;
                //td = SelectSqlTable("SELECT * FROM privateClubDetail");
                //foreach (DataRow tmprow in td.Rows)
                //{
                //    switch (tmprow["itemNm"].ToString().Trim())
                //    {
                //        case "ev_tmp1": ev_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "ev_tmp2": ev_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "ev_tmp3": ev_tmp3.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "sub_tmp1": sub_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //        case "sub_tmp2": sub_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                //    }
                //}

            }

            //Box2.Value = showVal;
             
            conn.Close();
            addBTN.Visible = false;
            saveBtn.Visible = false;
            ShowForm();
            updateBtn.Visible = true;
        }


        protected void saveBtn_Click1(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;
                var memIDInput = Box1.Value.ToString();
                var AppliedInput = Box6.Value.ToString();
                var nameJinput = Box2.Value.ToString();
                var preFixCho = Box3.SelectedValue.ToString();
                if (preFixCho == "-- ANY --")
                {
                    preFixCho = "";
                }
                var nameEinput = Box4.Value.ToString();
                var Phoneinput = Box9.Value.ToString();
                var mobileinput = Box10.Value.ToString();
                var memTypeCho = Box7.SelectedValue.ToString();
                var memStaCho = Box8.SelectedValue.ToString();
                var birthDateinput = Box5.Value.ToString();
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
                var email = Box19.Value.ToString();
                int chkspouse;
                if (Box17.Checked == true)
                {
                    chkspouse = 1;
                }
                else
                {
                    chkspouse = 0;
                }
                try
                {
                    //td = SelectSqlTable("SET dateformat dmy INSERT INTO PrivateDetail(appliedDate, nameJ, nameE, prefixNm, memberStatus, birthDate, memberType, memberid, firstmemberid, spouse, updatedBy,updatedDate) " +
                    //                    "VALUES('" + AppliedInput + "'" + "," + "N'" + nameJinput + "'" + "," + "'" + nameEinput + "'" + "," + "'" + preFixCho + "'" + "," + "'" + memStaCho + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + memTypeCho + "'" + "," + "'" + memIDInput + "'" + "," + "'" + run_num + "'" + "," + "'" + chkspouse + "'" + "," + Session["UID"] + "," + "'" + toDayDate + "'" + ") " +
                    //                    "INSERT INTO PrivatePayment(memberid) " +
                    //                    "VALUES('" + memIDInput + "'" + ") " +
                    //                    "INSERT INTO privateAddress(phone, mobile, addressType, memberid) " +
                    //                    "VALUES('" + Phoneinput + "'" + "," + "'" + mobileinput + "'" + "," + "'" + "1" + "'" + "," + "'" + memIDInput + "'" + ") " +
                    //                    "INSERT INTO privateAddress(memberid, addressType) " +
                    //                    "VALUES('" + memIDInput + "'" + "," + "'" + "2" + "'" + ") " +
                    //                    "INSERT INTO PrivateClub(golf, board, lady, children, zukuzuku, memberid, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member) " +
                    //                    "VALUES('" + chkGolf + "'" + "," + "'" + chkBoard + "'" + "," + "'" + chkLady + "'" + "," + "'" + chkChild + "'" + "," + "'" + chkSukuzuku + "'" + "," + "'" + memIDInput + "'" + "," + "'" + chkEngtest + "'" + "," + "'" + chkOnevent + "'" + "," + "'" + chkSoftball + "'" + "," + "'" + chkYoga + "'" + "," + "'" + chkValue1 + "'" + "," + "'" + chkValue2 + "'" + "," + "'" + chkValue3 + "'" + "," + "'" + chkBoardlist + "'" + "," + "'" + chkClubSecre + "'" + "," + "'" + chkBaVolun + "'" + "," + "'" + chkSocialMem + "'" + "," + "'" + chkYouthMem + "'" + "," + "'" + chkValue4 + "'" + "," + "'" + chkValue5 + "'" + "," + "'" + chkOverseasMem + "'" + ") " +
                    //                    "INSERT INTO PrivateAccount(memberId) " +
                    //                    "VALUES('" + memIDInput + "'" + ")" +
                    //                    "INSERT INTO PrivateSendHistory(memberId, sendType) " +
                    //                    "VALUES('" + memIDInput + "'" + "," + "'" + "#" + "'" + ") " +
                    //                    "INSERT INTO PrivateRemark(memberId) " +
                    //                    "VALUES('" + memIDInput + "'" + ") " +
                    //                    "INSERT INTO PrivateRefer(memberid) " +
                    //                    "VALUES('" + memIDInput + "'" + ") " +
                    //                    "INSERT INTO Private(memberid, sendType,email) " +
                    //                    "VALUES('" + memIDInput + "'" + "," + "'" + "#" + "'" + "," + "'" + email + "'" + ") " +
                    //                    "INSERT INTO PrivateBoard(memberId) " +
                    //                    "VALUES('" + memIDInput + "'" + ") ");

                    try
                    {
						td = SelectSqlTable("SET dateformat dmy INSERT INTO RegisterFamMem(appliedDate, nameJp, nameEn, prefixNm, memberStatus, birthDate, memberType, familymember_id, register_id, spouse, email, homeTel, mobile, golf, board, lady, children, zukuzuku, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member) " +
										"VALUES('" + Date_MsSqlStandard.CastQuery(AppliedInput) + "'" + "," + "N'" + nameJinput + "'" + "," + "'" + nameEinput + "'" + "," + "'" + preFixCho + "'" + "," + "'" + memStaCho + "'" + "," + "'" + Date_MsSqlStandard.CastQuery(birthDateinput) + "'" + "," + "'" + memTypeCho + "'" + "," + "'" + memIDInput + "'" + "," + "'" + reg_id + "'" + "," + "'" + chkspouse + "'" + "," + "'" + email + "'" + "," +
										"'" + Phoneinput + "'" + "," + "'" + mobileinput + "'" + "," + "'" + chkGolf + "'" + "," + "'" + chkBoard + "'" + "," + "'" + chkLady + "'" + "," + "'" + chkChild + "'" + "," + "'" + chkSukuzuku + "'" + "," + "'" + chkEngtest + "'" + "," + "'" + chkOnevent + "'" + "," + "'" + chkSoftball + "'" + "," + "'" + chkYoga + "'" + "," + "'" + chkValue1 + "'" + "," +
										"'" + chkValue2 + "'" + "," + "'" + chkValue3 + "'" + "," + "'" + chkBoardlist + "'" + "," + "'" + chkClubSecre + "'" + "," + "'" + chkBaVolun + "'" + "," + "'" + chkSocialMem + "'" + "," + "'" + chkYouthMem + "'" + "," + "'" + chkValue4 + "'" + "," + "'" + chkValue5 + "'" + "," + "'" + chkOverseasMem + "'" + ")");
                        string activityDetail = $"Added new data into a table 'RegisterFamMem' successful (User id = '{staffID}')";
                        logActivity.LogStaffActivity(staffID,activityDetail);
					}
                    catch (SqlException ex)
                    {
						string activityDetail = $"Added new data into a table 'RegisterFamMem' unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Added new data into a table 'RegisterFamMem' unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        lbError.Text = "Duplicate member id.";
                    }
                    else
                    {
                        lbError.Text = "Database error.";
                    }
                }
                catch (Exception ex)
                {
                    lbError.Text = ex.ToString();
                }
            }
            else
            {
            }
        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            Response.Redirect("regFamilyCheck.aspx?mode=add&registerid=" + reg_id);
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            HideForm();
            Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id);
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;
                var memIDInput = Box1.Value.ToString();
                var AppliedInput = Box6.Value.ToString();
                var nameJinput = Box2.Value.ToString();
                var preFixCho = Box3.SelectedValue.ToString();
                if (preFixCho == "-- ANY --")
                {
                    preFixCho = "";
                }
                var nameEinput = Box4.Value.ToString();
                var Phoneinput = Box9.Value.ToString();
                var mobileinput = Box10.Value.ToString();
                var memTypeCho = Box7.SelectedValue.ToString();
                var memStaCho = Box8.SelectedValue.ToString();
                var birthDateinput = Box5.Value.ToString();
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
                var email = Box19.Value.ToString();
                int chkspouse;
                if (Box17.Checked == true)
                {
                    chkspouse = 1;
                }
                else
                {
                    chkspouse = 0;
                }

                try
                {
                    td = SelectSqlTable("SET dateformat dmy UPDATE RegisterFamMem " +
                                    "SET appliedDate = " + "'" + Date_MsSqlStandard.CastQuery(AppliedInput) + "'" + "," + "nameJp = " + "N'" + nameJinput + "'" + "," + "prefixNm = " + "'" + preFixCho + "'" + "," + "nameEn = " + "'" + nameEinput + "'" + "," + "memberType = " + "'" + memTypeCho + "'" + "," + "birthDate = " + "'" + Date_MsSqlStandard.CastQuery(birthDateinput) + "'" + "," + "memberStatus = " + "'" + memStaCho + "'" + "," + "spouse = " + "'" + chkspouse + "'" + "," + "email = " + "'" + email + "' " + "," +
                                    "homeTel = " + "'" + Phoneinput + "'" + ", " + "mobile = " + "'" + mobileinput + "'" + ", " + "golf = " + "'" + chkGolf + "'" + ", " + "children = " + "'" + chkChild + "'" + ", " + "board = " + "'" + chkBoard + "'" + ", " + "zukuzuku = " + "'" + chkSukuzuku + "'" + ", " + "lady = " + "'" + chkLady + "'" + ", " + "ev_1 = " + "'" + chkEngtest + "'" + ", " + "ev_2 = " + "'" + chkOnevent + "'" + ", " + "ev_3 = " + "'" + chkSoftball + "'" + ", " +
                                    "ev_4 = " + "'" + chkYoga + "'" + ", " + "ev_tmp1 = " + "'" + chkValue1 + "'" + ", " + "ev_tmp2 = " + "'" + chkValue2 + "'" + ", " + "ev_tmp3 = " + "'" + chkValue3 + "'" + ", " + "sub_board_list = " + "'" + chkBoardlist + "'" + ", " + "sub_secretary = " + "'" + chkClubSecre + "'" + ", " + "sub_volunteer = " + "'" + chkBaVolun + "'" + ", " + "sub_social = " + "'" + chkSocialMem + "'" + ", " + "sub_member = " + "'" + chkYouthMem + "'" + ", " +
                                    "sub_tmp1 = " + "'" + chkValue4 + "'" + ", " + "sub_tmp2 = " + "'" + chkValue5 + "'" + ", " + "ov_member = " + "'" + chkOverseasMem + "'" + ", " + "familymember_id = " + "'" + memIDInput + "'" +
                                    "WHERE register_id =" + "'" + reg_id + "' " + "AND run_num = " + "'" + run_num + "'");
					string activityDetail = $"Changed data in a table 'RegisterFamMem' where register_id is '{reg_id}' and run_num is '{run_num}' successful (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);

                    Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id, false);
                }
                catch (SqlException ex)
                {
					string activityDetail = $"Changed data in a table 'RegisterFamMem' where register_id is '{reg_id}' and run_num is '{run_num}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
					lbError.Text = ex.ToString();
                }
				catch (Exception ex)
				{
					string activityDetail = $"Changed data in a table 'RegisterFamMem' where register_id is '{reg_id}' and run_num is '{run_num}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
					lbError.Text = ex.ToString();
				}
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
                    Box7.DataSource = subjects;
                    Box7.DataValueField = "MemberType";
                    Box7.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            Box7.Items.Insert(0, new ListItem("0", "0"));
            Box7.Items.Insert(0, new ListItem("--Any--", "99"));
        }
    }
}