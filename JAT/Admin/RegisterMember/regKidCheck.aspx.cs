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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using JAT.Core;

namespace JAT.Admin.RegisterMember
{
    public partial class regKidCheck : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
        private SqlCommand cmd;

        //public string postBackId;

        private string run_num;
        private string reg_id;
        string addValue;

        string dateToCancel;

        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));
        string toDayDateSh = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
        protected void Page_Load(object sender, EventArgs e)
        {
            dateToCancel = DateTime.Now.ToString("dd/MM/yyyy");

            updateBtn.Visible = false;


            run_num = Request.QueryString["runnum"];
            reg_id = Request.QueryString["registerid"];

            addValue = Request.QueryString["mode"];
            DataTable td;
            td = SelectSqlTable("SELECT register_status  FROM RegisterPrivate WHERE register_id = '" + reg_id + "' AND register_status = 'AP' ");
            if (td.Rows.Count > 0)
            {
                Response.Redirect("~/Admin/RegisterMember/regApprove?registerid=" + reg_id + "");
            }
            else
            {
                if (addValue == "add")
                {
                    EnabledForm();
                    saveBtn.Visible = true;

                }
                else
                {
                    DisabledForm();
                    saveBtn.Visible = false;

                }

                if (run_num != null || reg_id != null)
                {
                    if (!Page.IsPostBack)
                    {
                        BindData();
                        showInGrid();
                    }
                }
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
                    //updateBy.Text = rd.GetValue(2).ToString();
                }

            }
            catch { }
            finally
            {
                //if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                //{
                //    //updateBy.Text = "N/A";
                //}
            }

            //Box2.Value = showVal;


            conn.Close();
        }

        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (run_num != null || reg_id != null)
            {
                Response.Redirect("regEntryCheck.aspx?registerid=" + reg_id);
            }
        }

        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (run_num != null || reg_id != null)
            {
                Response.Redirect("regFamilyCheck.aspx?registerid=" + reg_id);
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
        protected void showInGrid()
        {
            DataTable td;

            td = SelectSqlTable("SELECT run_num, gender, nameJp, nameEn, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate " +
                                "FROM RegisterChildMem " +
                                "WHERE register_id = '" + reg_id + "'");
            //GridView1.Columns[0].Visible = false;
            GridView1.DataSource = td;
            //ImageButton1.Visible = true;
            //ImageButton2.Visible = true;

            GridView1.DataBind();
            conn.Close();

        }

        private void EnabledForm()
        {
            addBTN.Visible = false;


            //saveBtn.Visible = true;
            cancelBtn.Visible = true;

            //child_ID.Attributes.Remove("disabled");
            Box1.Attributes.Remove("disabled");
            Box2.Attributes.Remove("disabled");
            Box3.Attributes.Remove("disabled");
            RadioButtonList1.Enabled = true;

            //RequiredFieldValidatorID.Visible = true;
            RequiredFieldValidator1.Visible = true;
            RequiredFieldValidator2.Visible = true;
            RequiredFieldValidator3.Visible = true;

        }

        private void DisabledForm()
        {
            addBTN.Visible = true;

            saveBtn.Visible = false;
            cancelBtn.Visible = false;

            //child_ID.Attributes.Add("disabled", "disabled");
            Box1.Attributes.Add("disabled", "disabled");
            Box2.Attributes.Add("disabled", "disabled");
            Box3.Attributes.Add("disabled", "disabled");
            RadioButtonList1.Enabled = false;

            //RequiredFieldValidatorID.Visible = false;
            RequiredFieldValidator1.Visible = false;
            RequiredFieldValidator2.Visible = false;
            RequiredFieldValidator3.Visible = false;

        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            EnabledForm();
            saveBtn.Visible = true;

            //Response.Redirect("privateEntryKid.aspx?mode=add");
            //Response.Redirect("privateEntryKid.aspx?mode=add&firstmemberid=" + reg_id);
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            DisabledForm();
            Response.Redirect("regKidCheck.aspx?registerid=" + reg_id);

        }

        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            EnabledForm();
            saveBtn.Visible = false;
            updateBtn.Visible = true;

            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;


            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            HiddenField1.Value = row.Cells[0].Text;

            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT run_num, gender, nameJp, nameEn, FORMAT(birthDate, 'dd/MM/yyyy') AS birthDate " +
                            "FROM RegisterChildMem " +
                            //"WHERE register_id = '" + row.Cells[0].Text + "'";
                            "WHERE run_num = '" + HiddenField1.Value + "'";



            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();


                while (rd.Read())
                {

                    string preNmChild = rd.GetValue(1).ToString();
                    if (preNmChild == "Boy")
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                    else if (preNmChild == "Girl")
                    {
                        RadioButtonList1.SelectedIndex = 1;
                    }
                    else
                    {
                        RadioButtonList1.ClearSelection();
                    }

                    //child_ID.Value = rd.GetValue(0).ToString();
                    Box1.Value = rd.GetValue(2).ToString();
                    Box2.Value = rd.GetValue(3).ToString();
                    Box3.Value = rd.GetValue(4).ToString();

                }

            }
            catch { }

            conn.Close();

        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;

                //var chkBirth = Box7.Checked;
                var preFixCho = RadioButtonList1.SelectedValue.ToString();

                //var chklastChild = new DataTable();
                //chklastChild = SelectSqlTable("SELECT TOP(1) childid FROM PrivateChild ORDER BY childid DESC");
                ////var childIdinput = child_ID.Value.ToString();
                //var childIdinput = Int32.Parse(chklastChild.Rows[0][0].ToString().Trim());
                //childIdinput++;
                var nameKIDJinput = Box1.Value.ToString();
                var nameKIDEinput = Box2.Value.ToString();
                var birthDateinput = Box3.Value.ToString();

                //string removeDateDefault = "";

                //var preFixCho = preFixSel.SelectedValue.ToString();


                //td = SelectSqlTable("SET dateformat dmy INSERT INTO PrivateChild(memberid, nameKidJ, nameKidE, birthdate, prefixKid,updatedBy,updatedDate)" +
                //                        "VALUES('" + reg_id + "'" + "," + "N'" + nameKIDJinput + "'" + "," + "N'" + nameKIDEinput + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + preFixCho + "'" + "," + Session["UID"] + "," + "'" + toDayDate + "'" + ")");

                
                try
                {
					td = SelectSqlTable("SET dateformat dmy SET IDENTITY_INSERT RegisterChildMem OFF INSERT INTO RegisterChildMem(register_id, nameJp, nameEn, birthDate, gender)" +
											"VALUES('" + reg_id + "'" + "," + "N'" + nameKIDJinput + "'" + "," + "N'" + nameKIDEinput + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + preFixCho + "'" + ")");
					string activityDetail = $"Added new data into a table 'RegisterChildMem' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
                {
					string activityDetail = $"Added new data into a table 'RegisterChildMem' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Added new data into a table 'RegisterChildMem' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}

				//DisabledForm();
				Response.Redirect("regKidCheck.aspx?registerid=" + reg_id, false );
            }
            else
            {
                Response.Redirect("regKidCheck.aspx?registerid=" + reg_id, false);
            }
        }

        protected void GridView_Delete_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            //ImageButton _myButton = (ImageButton)e.Item.FindControl("ibtn2");

            
			try
			{
                // *** 2024-09-06 02.10pm : Toon Jiradech.K Toon Jiradech.k have changed code from hard deleting to soft deleting
                #region 'Hard Deleting'
                // td = SelectSqlTable("DELETE FROM RegisterChildMem  " +
                //"WHERE run_num = " + "'" + row.Cells[0].Text + "'");
                #endregion

                #region 'Soft Deleting'
                td = SelectSqlTable($"UPDATE RegisterChildMem SET Deleted_at = GETDATE() " +
                                    $"WHERE run_num = '{row.Cells[0].Text}'");
                #endregion
                // *** End of Revised

                string activityDetail = $"Soft deleted data in a table 'RegisterChildMem' where run_num is '{row.Cells[0].Text}' successful (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
                //string activityDetail = $"Soft deleted data in a table 'RegisterChildMem' where run_num is '{row.Cells[0].Text}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                    $"{ex.Message}");
            }
			catch (Exception ex)
			{
                //string activityDetail = $"Soft deleted data in a table 'RegisterChildMem' where run_num is '{row.Cells[0].Text}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
            }

			Response.Redirect("regKidCheck.aspx?registerid=" + reg_id, false);

            conn.Close();

        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;

                //var chkBirth = Box7.Checked;
                var preFixCho = RadioButtonList1.SelectedValue.ToString();
                //var chklastChild = new DataTable();
                //chklastChild = SelectSqlTable("SELECT TOP(1) childid FROM PrivateChild ORDER BY childid DESC");
                ////var childIdinput = child_ID.Value.ToString();
                //var childIdinput = Int32.Parse(chklastChild.Rows[0][0].ToString().Trim());
                //childIdinput++;
                var nameKIDJinput = Box1.Value.ToString();
                var nameKIDEinput = Box2.Value.ToString();
                var birthDateinput = Box3.Value.ToString();


                //string removeDateDefault = "";

                //var preFixCho = preFixSel.SelectedValue.ToString();

				try
				{
					td = SelectSqlTable("SET dateformat dmy UPDATE RegisterChildMem " +
									"SET nameEn = '" + nameKIDEinput + "'" + "," + "nameJp = N'" + nameKIDJinput + "'" + "," + "gender = '" + preFixCho + "'" + "," + "birthDate = '" + birthDateinput + "'" +
									 "WHERE run_num = '" + HiddenField1.Value + "'");
					string activityDetail = $"Changed data in a table 'RegisterChildMem' where run_num is '{HiddenField1.Value}' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
				{
					string activityDetail = $"Changed data in a table 'RegisterChildMem' where run_num is '{HiddenField1.Value}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Changed data in a table 'RegisterChildMem' where run_num is '{HiddenField1.Value}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}

				//DisabledForm();
				Response.Redirect("regKidCheck.aspx?registerid=" + reg_id, false);
            }
            else
            {
                Response.Redirect("regKidCheck.aspx?registerid=" + reg_id, false);
            }
        }


    }
}