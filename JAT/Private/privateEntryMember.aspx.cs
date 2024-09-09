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

namespace JAT.Private
{
    public partial class privateEntryMember : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();

		//public string postBackId;

		private string showfristMem;
        private string showMem;
        string addValue;

        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));
        string toDayDateSh = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
        public static string cancelDateTmp;
        protected void Page_Load(object sender, EventArgs e)
        {
            showfristMem = Request.QueryString["firstmemberid"];
            showMem = Request.QueryString["memberid"];
            addValue = Request.QueryString["mode"];
            if (showMem != null || showfristMem != null)
            {
                if (!Page.IsPostBack)
                {
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
        protected void setGridHeader()
        {
            DataTable td;
            td = SelectSqlTable("SELECT * FROM privateClubDetail");
            foreach (DataRow tmprow in td.Rows)
            {
                switch (tmprow["itemNm"].ToString().Trim())
                {
                    case "ev_tmp1": GridView1.HeaderRow.Cells[12].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "ev_tmp2": GridView1.HeaderRow.Cells[13].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "ev_tmp3": GridView1.HeaderRow.Cells[14].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "sub_tmp1": GridView1.HeaderRow.Cells[23].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    case "sub_tmp2": GridView1.HeaderRow.Cells[24].Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                }
            }
            if (GridView1.HeaderRow.Cells[12].Text == "&nbsp;")
            {
                GridView1.HeaderRow.Cells[12].Visible = false;
                GridView1.Columns[12].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[12].Visible = true;
                GridView1.Columns[12].Visible = true;
            }
            if (GridView1.HeaderRow.Cells[13].Text == "&nbsp;")
            {
                GridView1.HeaderRow.Cells[13].Visible = false;
                GridView1.Columns[13].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[13].Visible = true;
                GridView1.Columns[13].Visible = true;
            }
            if (GridView1.HeaderRow.Cells[14].Text == "&nbsp;")
            {
                GridView1.HeaderRow.Cells[14].Visible = false;
                GridView1.Columns[14].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[14].Visible = true;
                GridView1.Columns[14].Visible = true;
            }
            if (GridView1.HeaderRow.Cells[23].Text == "&nbsp;")
            {
                GridView1.HeaderRow.Cells[23].Visible = false;
                GridView1.Columns[23].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[23].Visible = true;
                GridView1.Columns[23].Visible = true;
            }
            if (GridView1.HeaderRow.Cells[24].Text == "&nbsp;")
            {
                GridView1.HeaderRow.Cells[24].Visible = false;
                GridView1.Columns[24].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[24].Visible = true;
                GridView1.Columns[24].Visible = true;
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

            string sql = "SELECT nameJ, CONCAT(prefixNm, nameE) " +
                            "FROM PrivateDetail " +
                            "WHERE firstmemberid = '" + showfristMem + "'" + "AND memberid = firstmemberid";
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
                    "LEFT OUTER JOIN SStaff t2 ON updatedBy=staffID WHERE firstmemberid='" + showfristMem + "' " +
                    "ORDER BY t1.updatedDate DESC";

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
        protected void showInGrid()
        {
            DataTable td;



            td = SelectSqlTable("SELECT t1.memberid, t1.nameJ, CONCAT(t1.prefixNm, t1.nameE) AS nameE, FORMAT(t1.birthDate, 'dd/MM/yyyy') AS birthDate, FORMAT(t1.appliedDate, 'dd/MM/yyyy') AS appliedDate, FORMAT(t1.cancelledDate, 'dd/MM/yyyy') AS cancelledDate, t1.memberType, t1.memberStatus, t2.ev_1, t2.ev_2, t2.ev_3, t2.ev_4, t2.ev_tmp1, t2.ev_tmp2, t2.ev_tmp3, t2.board, t2.sub_board_list, t2.golf, t2.lady, t2.sub_secretary, t2.sub_volunteer, t2.sub_social, t2.sub_member, t2.sub_tmp1, t2.sub_tmp2, t2.zukuzuku, t2.children, t2.ov_member " +
                                "FROM PrivateDetail t1 " +
                                "INNER JOIN PrivateClub t2 on t1.memberid = t2.memberid WHERE firstmemberid =" + "'" + showfristMem + "'" + "AND t1.firstmemberid != t1.memberid");
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
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void paymentTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
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

        protected void GridView_EditButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem +"&memberid=" + Box1.Value);

            ShowForm();
            addBTN.Visible = false;
            saveBtn.Visible = false;
            Box1.Attributes.Add("disabled", "disabled");

            GridView1.Columns[14].Visible = false;
            GridView1.Columns[15].Visible = false;

            updateBtn.Visible = true;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;


            connection();
            SqlCommand sc;
            SqlDataReader rd;

            //string sql = "SELECT t1.memberid, t1.nameJ, t1.prefixNm, t1.nameE, FORMAT(t1.birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(t1.appliedDate, 'yyyy-MMM-dd') AS appliedDate, t1.memberType, t1.memberStatus, t2.phone, t2.mobile, t3.golf, t3.board, t3.lady, t3.children, t3.zukuzuku, t1.spouse, t4.checkFamilyName,email, " +
            //             "ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member " +
            //                "FROM PrivateDetail t1 " +
            //                "INNER JOIN privateAddress t2 on t1.memberid = t2.memberid INNER JOIN PrivateClub t3 on t1.memberid = t3.memberid INNER JOIN Private t4 on t1.memberid = t4.memberid WHERE t1.memberid = " + "'" + row.Cells[0].Text + "'" + "AND t1.firstmemberid != t1.memberid AND t2.addressType = '1' ";
            string sql = "select d.firstmemberid, d.memberid, d.prefixNm, d.nameJ, d.nameE,FORMAT(d.birthDate, 'dd/MM/yyyy') AS birthDate,FORMAT(d.appliedDate, 'dd/MM/yyyy') AS appliedDate, " +
                "d.updatedDate,FORMAT(d.cancelledDate, 'dd/MM/yyyy') AS cancelledDate, d.memberStatus, d.memberType, d.updatedBy, d.locked, d.lockedBy, " +
                "c.memberid, c.golf, c.board, c.lady, c.children, c.zukuzuku, " +
                "ah.address as homeAddress, ah.phone as homePhone, " +
                "ah.mobile as mobile, ppp.checkFamilyName, spouse,d.email,ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member, FORMAT(d.date_do_status_na_to_a, 'dd/MM/yyyy') " +
                "from privateDetail d left join privateAddress ah on d.memberid = ah.memberid and ah.addresstype = 1 " +
                "left join privateClub c on d.memberid = c.memberid left join private ppp on d.memberid = ppp.memberid " +
                "where d.memberid = '" + row.Cells[0].Text + "' ";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    Box1.Value = rd.GetValue(1).ToString();
                    Box2.Value = rd.GetValue(3).ToString();
                    string tmp = rd.GetValue(2).ToString();
                    if (tmp == "")
                    {
                        tmp = "-- ANY --";
                    }
                    Box3.SelectedValue = tmp;
                    Box4.Value = rd.GetValue(4).ToString();
                    Box5.Value = rd.GetValue(5).ToString();
                    Box6.Value = rd.GetValue(6).ToString();
                    Box7.SelectedValue = rd.GetValue(10).ToString();
                    cancelDateTmp = rd.GetValue(8).ToString().Trim();
                    Box8.SelectedValue = rd.GetValue(9).ToString();
                    //HiddenMemStatus.Value = rd.GetValue(9).ToString();
                    //if (HiddenMemStatus.Value == "NA")
                    //{
                    //    dateNAtoA.Text = rd.GetValue(41).ToString();
                    //}
                    //else
                    //{
                    //    dateNAtoA.Text = "";
                    //}

                    dateNAtoA.Text = rd.GetValue(41).ToString();


                    Box9.Value = rd.GetValue(21).ToString();
                    Box10.Value = rd.GetValue(22).ToString();

                    Box19.Value = rd.GetValue(25).ToString();

                    bool chkDataInDB12 = (bool)rd.GetValue(15);
                    cbGolf.Checked = chkDataInDB12;

                    bool chkDataInDB13 = (bool)rd.GetValue(16);
                    cbBoard.Checked = chkDataInDB13;

                    bool chkDataInDB14 = (bool)rd.GetValue(17);
                    cbLady.Checked = chkDataInDB14;

                    bool chkDataInDB15 = (bool)rd.GetValue(18);
                    cbChildLibMem.Checked = chkDataInDB15;

                    bool chkDataInDB16 = (bool)rd.GetValue(19);
                    cbSukusukuMem.Checked = chkDataInDB16;

                    bool chkDataInDB17 = (bool)rd.GetValue(26);
                    cbEngtest.Checked = chkDataInDB17;

                    bool chkDataInDB18 = (bool)rd.GetValue(27);
                    cbOnevent.Checked = chkDataInDB18;

                    bool chkDataInDB19 = (bool)rd.GetValue(28);
                    cbSoftball.Checked = chkDataInDB19;

                    bool chkDataInDB20 = (bool)rd.GetValue(29);
                    cbYoga.Checked = chkDataInDB20;

                    bool chkDataInDB21 = (bool)rd.GetValue(30);
                    cbValue1.Checked = chkDataInDB21;

                    bool chkDataInDB22 = (bool)rd.GetValue(31);
                    cbValue2.Checked = chkDataInDB22;

                    bool chkDataInDB23 = (bool)rd.GetValue(32);
                    cbValue3.Checked = chkDataInDB23;

                    bool chkDataInDB24 = (bool)rd.GetValue(33);
                    cbBoardlist.Checked = chkDataInDB24;

                    bool chkDataInDB25 = (bool)rd.GetValue(34);
                    cbClubSecre.Checked = chkDataInDB25;

                    bool chkDataInDB26 = (bool)rd.GetValue(35);
                    cbBaVolun.Checked = chkDataInDB26;

                    bool chkDataInDB27 = (bool)rd.GetValue(36);
                    cbSocialMem.Checked = chkDataInDB27;

                    bool chkDataInDB28 = (bool)rd.GetValue(37);
                    cbYouthMem.Checked = chkDataInDB28;

                    bool chkDataInDB29 = (bool)rd.GetValue(38);
                    cbValue4.Checked = chkDataInDB29;

                    bool chkDataInDB30 = (bool)rd.GetValue(39);
                    cbValue5.Checked = chkDataInDB30;

                    bool chkDataInDB31 = (bool)rd.GetValue(40);
                    cbOverseasMem.Checked = chkDataInDB31;


                    // check spouse
                    int chkDataInDB7 = (int)rd.GetValue(24);
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
        protected void GridView_DeleteButton_Click(object sender, EventArgs e)
        {
            DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
                // *** 2024-09-06 11.15am : Toon Jiradech.K Toon Jiradech.k have changed code from hard deleting to soft deleting
                #region 'Hard Deleting'
                //td = SelectSqlTable("DELETE FROM PrivateDetail " +
                //                    "WHERE memberid = " + "'" + row.Cells[0].Text + "'" +
                //                    "DELETE FROM Private " +
                //                    "WHERE memberid = " + "'" + row.Cells[0].Text + "'" +
                //                    "DELETE FROM privateAddress " +
                //                    "WHERE memberid = " + "'" + row.Cells[0].Text + "'");
                #endregion

                #region 'Soft Deleting'
                td = SelectSqlTable($"UPDATE PrivateDetail SET Deleted_at = GETDATE() " +
                                    $"WHERE memberid = '{row.Cells[0].Text}'" +

                                    $"UPDATE Private SET Deleted_at = GETDATE() " +
                                    $"WHERE memberid = '{row.Cells[0].Text}'" +

                                    $"UPDATE privateAddress SET Deleted_at = GETDATE() " +
                                    $"WHERE memberid = '{row.Cells[0].Text}'");
                #endregion
                // *** End of Revised

                string activityDetail = $"Soft deleted data in 3 tables ('PrivateDetail, Private, privateAddress') where memberid is '{row.Cells[0].Text}' successful (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
                //string activityDetail = $"Soft deleted data in 3 tables ('PrivateDetail, Private, privateAddress') where memberid is '{row.Cells[0].Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                    $"{ex.Message}");
            }
			catch (Exception ex)
			{
                //string activityDetail = $"Soft deleted data in 3 tables ('PrivateDetail, Private, privateAddress') where memberid is '{row.Cells[0].Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} " +
                    $"{ex.Message}");
            }


			Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);

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
            //                "INNER JOIN privateAddress t2 on t1.memberid = t2.memberid INNER JOIN PrivateClub t3 on t1.memberid = t3.memberid INNER JOIN Private t4 on t1.memberid = t4.memberid WHERE t1.memberid = " + "'" + companyId + "'" + "AND t1.firstmemberid != t1.memberid AND t2.addressType = '1' ";
            string sql = "select d.firstmemberid, d.memberid, d.prefixNm, d.nameJ, d.nameE,FORMAT(d.birthDate, 'dd/MM/yyyy') AS birthDate,FORMAT(d.appliedDate, 'dd/MM/yyyy') AS appliedDate, " +
                "d.updatedDate, FORMAT(d.cancelledDate, 'dd/MM/yyyy') AS cancelledDate, d.memberStatus, d.memberType, d.updatedBy, d.locked, d.lockedBy, " +
                "c.memberid, c.golf, c.board, c.lady, c.children, c.zukuzuku, " +
                "ah.address as homeAddress, ah.phone as homePhone, " +
                "ah.mobile as mobile, ppp.checkFamilyName, spouse,d.email,ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member " +
                "from privateDetail d left join privateAddress ah on d.memberid = ah.memberid and ah.addresstype = 1 " +
                "left join privateClub c on d.memberid = c.memberid left join private ppp on d.memberid = ppp.memberid " +
                "where d.memberid = '" + showMem + "' ";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    Box1.Value = rd.GetValue(1).ToString();
                    Box2.Value = rd.GetValue(3).ToString();
                    string tmp = rd.GetValue(2).ToString();
                    if (tmp == "")
                    {
                        tmp = "-- ANY --";
                    }
                    Box3.SelectedValue = tmp;
                    Box4.Value = rd.GetValue(4).ToString();
                    Box5.Value = rd.GetValue(5).ToString();
                    Box6.Value = rd.GetValue(6).ToString();
                    Box7.SelectedValue = rd.GetValue(10).ToString();
                    cancelDateTmp = rd.GetValue(8).ToString().Trim();
                    Box8.SelectedValue = rd.GetValue(9).ToString();
                    Box9.Value = rd.GetValue(21).ToString();
                    Box10.Value = rd.GetValue(22).ToString();

                    Box19.Value = rd.GetValue(25).ToString();

                    bool chkDataInDB12 = (bool)rd.GetValue(15);
                    cbGolf.Checked = chkDataInDB12;

                    bool chkDataInDB13 = (bool)rd.GetValue(16);
                    cbBoard.Checked = chkDataInDB13;

                    bool chkDataInDB14 = (bool)rd.GetValue(17);
                    cbLady.Checked = chkDataInDB14;

                    bool chkDataInDB15 = (bool)rd.GetValue(18);
                    cbChildLibMem.Checked = chkDataInDB15;

                    bool chkDataInDB16 = (bool)rd.GetValue(19);
                    cbSukusukuMem.Checked = chkDataInDB16;

                    bool chkDataInDB17 = (bool)rd.GetValue(27);
                    cbEngtest.Checked = chkDataInDB17;

                    bool chkDataInDB18 = (bool)rd.GetValue(28);
                    cbOnevent.Checked = chkDataInDB18;

                    bool chkDataInDB19 = (bool)rd.GetValue(29);
                    cbSoftball.Checked = chkDataInDB19;

                    bool chkDataInDB20 = (bool)rd.GetValue(30);
                    cbYoga.Checked = chkDataInDB20;

                    bool chkDataInDB21 = (bool)rd.GetValue(31);
                    cbValue1.Checked = chkDataInDB21;

                    bool chkDataInDB22 = (bool)rd.GetValue(32);
                    cbValue2.Checked = chkDataInDB22;

                    bool chkDataInDB23 = (bool)rd.GetValue(33);
                    cbValue3.Checked = chkDataInDB23;

                    bool chkDataInDB24 = (bool)rd.GetValue(34);
                    cbBoardlist.Checked = chkDataInDB24;

                    bool chkDataInDB25 = (bool)rd.GetValue(35);
                    cbClubSecre.Checked = chkDataInDB25;

                    bool chkDataInDB26 = (bool)rd.GetValue(36);
                    cbBaVolun.Checked = chkDataInDB26;

                    bool chkDataInDB27 = (bool)rd.GetValue(37);
                    cbSocialMem.Checked = chkDataInDB27;

                    bool chkDataInDB28 = (bool)rd.GetValue(38);
                    cbYouthMem.Checked = chkDataInDB28;

                    bool chkDataInDB29 = (bool)rd.GetValue(39);
                    cbValue4.Checked = chkDataInDB29;

                    bool chkDataInDB30 = (bool)rd.GetValue(40);
                    cbValue5.Checked = chkDataInDB30;

                    bool chkDataInDB31 = (bool)rd.GetValue(41);
                    cbOverseasMem.Checked = chkDataInDB31;


                    // check spouse
                    int chkDataInDB7 = (int)rd.GetValue(16);
                    if (chkDataInDB7 == 1)
                    {
                        Box17.Checked = true;
                    }

                    bool chkDataInDB8 = (bool)rd.GetValue(17);
                    //Box18.Checked = chkDataInDB8;

                    if (ev_tmp1.Text.Trim().ToString() == "&nbsp;")
                    {
                        cbValue1.Enabled = false;
                    }
                    else if (addValue != "edit")
                    {
                        cbValue1.Enabled = true;
                    }
                    if (ev_tmp2.Text.Trim().ToString() == "&nbsp;")
                    {
                        cbValue2.Enabled = false;
                    }
                    else if (addValue != "edit")
                    {
                        cbValue2.Enabled = true;
                    }
                    if (ev_tmp3.Text.Trim().ToString() == "&nbsp;")
                    {
                        cbValue3.Enabled = false;
                    }
                    else if (addValue != "edit")
                    {
                        cbValue3.Enabled = true;
                    }
                    if (sub_tmp1.Text.Trim().ToString() == "&nbsp;")
                    {
                        cbValue4.Enabled = false;

                    }
                    else if (addValue != "edit")
                    {
                        cbValue4.Enabled = true;
                    }
                    if (sub_tmp2.Text.Trim().ToString() == "&nbsp;")
                    {
                        cbValue5.Enabled = false;
                    }
                    else if (addValue != "edit")
                    {
                        cbValue5.Enabled = true;
                    }
                }

            }
            catch { }

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
                    //                    "VALUES('" + AppliedInput + "'" + "," + "N'" + nameJinput + "'" + "," + "'" + nameEinput + "'" + "," + "'" + preFixCho + "'" + "," + "'" + memStaCho + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + memTypeCho + "'" + "," + "'" + memIDInput + "'" + "," + "'" + showfristMem + "'" + "," + "'" + chkspouse + "'" + "," + Session["UID"] + "," + "'" + toDayDate + "'" + ") " +
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
                    td = SelectSqlTable("SET dateformat dmy INSERT INTO PrivateDetail(appliedDate, nameJ, nameE, prefixNm, memberStatus, birthDate, memberType, memberid, firstmemberid, spouse, updatedBy,updatedDate,email) " +
                        "VALUES('" + AppliedInput + "'" + "," + "N'" + nameJinput + "'" + "," + "'" + nameEinput + "'" + "," + "'" + preFixCho + "'" + "," + "'" + memStaCho + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + memTypeCho + "'" + "," + "'" + memIDInput + "'" + "," + "'" + showfristMem + "'" + "," + "'" + chkspouse + "'" + "," + Session["UID"] + "," + "'" + toDayDate + "'" + "," + "'" + email + "'" + ") " +
                        "INSERT INTO privateAddress(phone, mobile, addressType, memberid) " +
                        "VALUES('" + Phoneinput + "'" + "," + "'" + mobileinput + "'" + "," + "'" + "1" + "'" + "," + "'" + memIDInput + "'" + ") " +
                        "INSERT INTO PrivateClub(golf, board, lady, children, zukuzuku, memberid, ev_1,ev_2,ev_3,ev_4,ev_tmp1,ev_tmp2,ev_tmp3,sub_board_list,sub_secretary,sub_volunteer,sub_social,sub_member,sub_tmp1,sub_tmp2,ov_member) " +
                        "VALUES('" + chkGolf + "'" + "," + "'" + chkBoard + "'" + "," + "'" + chkLady + "'" + "," + "'" + chkChild + "'" + "," + "'" + chkSukuzuku + "'" + "," + "'" + memIDInput + "'" + "," + "'" + chkEngtest + "'" + "," + "'" + chkOnevent + "'" + "," + "'" + chkSoftball + "'" + "," + "'" + chkYoga + "'" + "," + "'" + chkValue1 + "'" + "," + "'" + chkValue2 + "'" + "," + "'" + chkValue3 + "'" + "," + "'" + chkBoardlist + "'" + "," + "'" + chkClubSecre + "'" + "," + "'" + chkBaVolun + "'" + "," + "'" + chkSocialMem + "'" + "," + "'" + chkYouthMem + "'" + "," + "'" + chkValue4 + "'" + "," + "'" + chkValue5 + "'" + "," + "'" + chkOverseasMem + "'" + ")");

					string activityDetail = $"Added new data into 3 tables ('PrivateDetail, privateAddress, PrivateClub') successful (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);

					
                }
                catch (SqlException ex)
                {
					string activityDetail = $"Added new data into 3 tables ('PrivateDetail, privateAddress, PrivateClub') unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
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
					string activityDetail = $"Insert new data into table 'PrivateDetail, privateAddress, PrivateClub' unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
					lbError.Text = ex.ToString();
                }
				Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem + "&memberid=" + memIDInput);
			}
			else
            {
            }
        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            Response.Redirect("privateEntryMember.aspx?mode=add&firstmemberid=" + showfristMem);
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            HideForm();
            Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);
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
                string updateNAToA = "";
                if (Box8.SelectedValue.ToString() == "A")
                {
                    if (cancelDateTmp != "")
                    {
                        updateNAToA = ", date_do_status_na_to_a = CURRENT_TIMESTAMP ";
                    }

                    cancelDateTmp = "NULL";
                }
                else if (cancelDateTmp == "" && Box8.SelectedValue.ToString() == "NA")
                {
                    cancelDateTmp = "CURRENT_TIMESTAMP";
                    updateNAToA = ", date_do_status_na_to_a = NULL ";

                }
                else
                {
                    try
                    {
                        //DateTime canceldatetime = DateTime.Parse(cancelDateTmp);
                        DateTime canceldatetime = DateTime.ParseExact(cancelDateTmp, "dd/MM/yyyy", null);
                        cancelDateTmp = "'" + cancelDateTmp.ToString() + "'";
                    }
                    catch (Exception ex)
                    {
                        cancelDateTmp = "CURRENT_TIMESTAMP";
                    }
                }

                //if (HiddenMemStatus.Value == "NA" && Box8.SelectedValue.ToString() == "A")
                //{
                //    updateNAToA = ", date_do_status_na_to_a = NULL ";
                //}

                try
                {
                    td = SelectSqlTable("SET dateformat dmy UPDATE PrivateDetail " +
                                    "SET appliedDate = " + "'" + AppliedInput + "'" + "," + "nameJ = " + "N'" + nameJinput + "'" + "," + "prefixNm = " + "'" + preFixCho + "'" + "," + "nameE = " + "'" + nameEinput + "'" + "," + "memberType = " + "'" + memTypeCho + "'" + "," + "birthDate = " + "'" + birthDateinput + "'" + "," + "memberStatus = " + "'" + memStaCho + "'" + "," + "spouse = " + "'" + chkspouse + "'" + "," + "updatedBy = '" + Session["UID"] + "'," + "updatedDate = " + "'" + toDayDate + "'" + "," + "cancelledDate = " + cancelDateTmp + "," + "email = " + "'" + email + "' " + updateNAToA +
                                    "WHERE memberid =" + "'" + memIDInput + "'" +

                                    "SELECT * FROM privateAddress WHERE memberId = '" + memIDInput + "' " +
                                    "if @@rowcount > 0 " +
                                    "UPDATE privateAddress " +
                                    "SET phone = " + "'" + Phoneinput + "'" + "," + "mobile = " + "'" + mobileinput + "'" +
                                    "WHERE memberid =" + "'" + memIDInput + "'" + "AND addressType = '1' " +
                                    "else " +
                                    "INSERT privateAddress(memberId, phone, mobile, addressType) " +
                                    "VALUES('" + memIDInput + "','" + Phoneinput + "','" + mobileinput + "', 1) " +

                                    "UPDATE PrivateClub " +
                                    //"SET golf = " + "'" + chkGolf + "'" + "," + "board = " + "'" + chkBoard + "'" + "," + "lady = " + "'" + chkLady + "'" + "," + "children = " + "'" + chkChild + "'" + "," + "zukuzuku = " + "'" + chkSukuzuku + "'" + "," + "meijinkai = " + "'" + chkMeijinkai + "'" +
                                    "SET golf = " + "'" + chkGolf + "'" + "," + "children = " + "'" + chkChild + "'" + "," + "board = " + "'" + chkBoard + "'" + "," + "zukuzuku = " + "'" + chkSukuzuku + "'" + "," + "lady = " + "'" + chkLady + "'" + "," + "ev_1 = " + "'" + chkEngtest + "'" + "," + "ev_2 = " + "'" + chkOnevent + "'" + "," + "ev_3 = " + "'" + chkSoftball + "'" + "," + "ev_4 = " + "'" + chkYoga + "'" + "," + "ev_tmp1 = " + "'" + chkValue1 + "'" + "," + "ev_tmp2 = " + "'" + chkValue2 + "'" + "," + "ev_tmp3 = " + "'" + chkValue3 + "'" + "," + "sub_board_list = " + "'" + chkBoardlist + "'" + "," + "sub_secretary = " + "'" + chkClubSecre + "'" + "," + "sub_volunteer = " + "'" + chkBaVolun + "'" + "," + "sub_social = " + "'" + chkSocialMem + "'" + "," + "sub_member = " + "'" + chkYouthMem + "'" + "," + "sub_tmp1 = " + "'" + chkValue4 + "'" + "," + "sub_tmp2 = " + "'" + chkValue5 + "'" + "," + "ov_member = " + "'" + chkOverseasMem + "'" +
                                    "WHERE memberid = " + "'" + memIDInput + "'");
					string activityDetail = $"Changed value in a table 'privateAddress' and Added new value into 2 tables ('privateAddress, PrivateClub') successful (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
                    //Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem + "&memberid=" + memIDInput);
                    
                }
				catch (SqlException ex)
				{
					string activityDetail = $"Changed value in a table 'privateAddress' and Added new value into 2 tables ('privateAddress, PrivateClub') unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
					lbError.Text = ex.ToString();
				}
				catch (Exception ex)
                {
					string activityDetail = $"Changed value in a table 'privateAddress' and Added new value into 2 tables ('privateAddress, PrivateClub') unsuccessful [{ex.Message}] (user id = {staffID})";
					logActivity.LogStaffActivity(staffID, activityDetail);
					lbError.Text = ex.ToString();
                }
				Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);
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
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            Box7.Items.Insert(0, new ListItem("0", "0"));
            Box7.Items.Insert(0, new ListItem("--Any--", "99"));
        }
    }
}