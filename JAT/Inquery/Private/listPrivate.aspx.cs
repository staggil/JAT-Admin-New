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
using CrystalDecisions.CrystalReports.Engine;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using JATMEMBER.View.Private;

namespace JAT.Inquery.Private
{
    public partial class listPrivate : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

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

            //string message = "";
            //foreach (ListItem item in lstFruits.Items)
            //{
            //    if (item.Selected)
            //    {
            //        message += item.Text + " " + item.Value + "\\n";
            //    }
            //}
            if (!Page.IsPostBack)
            {
                LoadSubjects();
                LoadSubjects2();
                LoadSubjects3();
                LoadSubjects4();
                Binddata();

                DataTable td;
                td = SelectSqlTable("SELECT * FROM privateClubDetail");
                foreach (DataRow tmprow in td.Rows)
                {
                    switch (tmprow["itemNm"].ToString().Trim())
                    {
                        case "ev_tmp1": CheckBoxList2.Items[4].Text = tmprow["itemVal"].ToString(); break;
                        case "ev_tmp2": CheckBoxList2.Items[5].Text = tmprow["itemVal"].ToString(); break;
                        case "ev_tmp3": CheckBoxList2.Items[6].Text = tmprow["itemVal"].ToString(); break;
                        case "sub_tmp1": CheckBoxList3.Items[7].Text = tmprow["itemVal"].ToString(); break;
                        case "sub_tmp2": CheckBoxList3.Items[8].Text = tmprow["itemVal"].ToString(); break;
                    }
                }
                initLabel();
            }
            txtQuitDateFrom.Disabled = true;
            txtQuitDateTo.Disabled = true;
        }

        protected void initLabel()
        {
            if (CheckBoxList2.Items[4].Text.Trim() == "")
            {
                CheckBoxList2.Items[4].Enabled = false;
                CheckBoxList2.Items[4].Selected = false;
            }
            if (CheckBoxList2.Items[5].Text.Trim() == "")
            {
                CheckBoxList2.Items[5].Enabled = false;
                CheckBoxList2.Items[5].Selected = false;
            }
            if (CheckBoxList2.Items[6].Text.Trim() == "")
            {
                CheckBoxList2.Items[6].Enabled = false;
                CheckBoxList2.Items[6].Selected = false;
            }
            if (CheckBoxList3.Items[7].Text.Trim() == "")
            {
                CheckBoxList3.Items[7].Enabled = false;
                CheckBoxList3.Items[7].Selected = false;
            }
            if (CheckBoxList3.Items[8].Text.Trim() == "")
            {
                CheckBoxList3.Items[8].Enabled = false;
                CheckBoxList3.Items[8].Selected = false;
            }
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        private void LoadSubjects()
        {

            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct MemberType FROM SMemberType", con);
                    adapter.Fill(subjects);

                    drplstMemberType.DataSource = subjects;
                    //drplstMemberType.DataTextField = "MemberType";
                    drplstMemberType.DataValueField = "MemberType";
                    drplstMemberType.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            drplstMemberType.Items.Insert(0, new ListItem("0", "0"));

        }
        private void LoadSubjects2()
        {

            DataTable subjects2 = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    //SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT sendtype FROM SSendType WHERE sectionFlag = '2'", con);
                    SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT sendtype FROM SSendType", con);
                    adapter2.Fill(subjects2);

                    drplstSendType.DataSource = subjects2;
                    //drplstMemberType.DataTextField = "MemberType";
                    drplstSendType.DataValueField = "sendtype";
                    drplstSendType.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            drplstSendType.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        private void LoadSubjects3()
        {

            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT itemNm,itemVal FROM privateClubDetail " +
                                                                "where itemNm like 'ev_%' and itemVal != '' ", con);
                    adapter.Fill(subjects);

                    drpEvent.DataSource = subjects;

                    drpEvent.DataTextField = "itemVal";
                    drpEvent.DataValueField = "itemNm";
                    drpEvent.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            drpEvent.Items.Insert(0, new ListItem("--Any--", "0"));
            drpEvent.Items.Insert(1, new ListItem("English Test", "ev_1"));
            drpEvent.Items.Insert(2, new ListItem("Online Event", "ev_2"));
            drpEvent.Items.Insert(3, new ListItem("Softball", "ev_3"));
            drpEvent.Items.Insert(4, new ListItem("Yoga", "ev_4"));

        }
        private void LoadSubjects4()
        {

            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT itemNm,itemVal FROM privateClubDetail " +
                                                                "where itemNm like 'sub_%' and itemVal != '' ", con);
                    adapter.Fill(subjects);

                    drplstClub.DataSource = subjects;

                    drplstClub.DataTextField = "itemVal";
                    drplstClub.DataValueField = "itemNm";
                    drplstClub.DataBind();
                }
                catch (Exception)
                {
                    // Handle the error
                }

            }
            drplstClub.Items.Insert(0, new ListItem("--Any--", "0"));
            drplstClub.Items.Insert(1, new ListItem("Board", "board"));
            //drplstClub.Items.Insert(2, new ListItem("Board [List]", "sub_board_list"));
            drplstClub.Items.Insert(2, new ListItem("Golf", "golf"));
            drplstClub.Items.Insert(3, new ListItem("Lady", "lady"));
            drplstClub.Items.Insert(4, new ListItem("Club secretary", "sub_secretary"));
            drplstClub.Items.Insert(5, new ListItem("Bazaar volunteer", "sub_volunteer"));
            drplstClub.Items.Insert(6, new ListItem("Social gathering members", "sub_social"));
            drplstClub.Items.Insert(7, new ListItem("Youth circle members", "sub_member"));

        }


        private void Binddata()
        {
            //au_Wirte_14_MAY_2021
            try
            {
                grdCompany.DataSource = GetMemberTypeData().Tables["PrivateDetail"].DefaultView;
                grdCompany.DataBind();

            }
            catch (Exception)
            {
                grdCompany.CurrentPageIndex = 0;
                grdCompany.DataBind();
            }
        }
        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        //}
        private DataSet GetMemberTypeData()
        {
            IFormatProvider culture = new CultureInfo("en-US", true);
            connection();

            string sqlName = "", sqlName2 = "";
            string sqlValue = "", sqlValue2 = "";
            string sqlCheck = "", sqlCheck2 = "";
            string sqlAll = "", sqlAll2 = "";
            //int total = 0;
            //DataTable dt = SelectSqlTable("SELECT itemNm,itemVal FROM privateClubDetail " +
            //    "where itemNm like 'sub_%' ");
            //----------------------------------------------------
            DataTable dtt = SelectSqlTable("SELECT itemNm,itemVal, CASE WHEN itemVal = '' THEN itemNm END AS itemCheck " +
                "FROM privateClubDetail  " +
                "where itemNm like 'ev_%' ");
            int j = 0;
            foreach (DataRow tmprow in dtt.Rows)
            {
                sqlName2 = tmprow["itemNm"].ToString();
                sqlValue2 = tmprow["itemVal"].ToString();
                sqlCheck2 = tmprow["itemCheck"].ToString();
                //if club value null=error
                var field = grdCompany.Columns[28 + j] as BoundColumn;
                if (sqlName2 != sqlCheck2)
                {
                    sqlAll2 += string.Format(",'{0}' = PrivateClub.{1}", sqlValue2, sqlName2);
                    field.DataField = sqlValue2;
                    field.HeaderText = sqlValue2;
                }
                else
                {
                    sqlAll2 += string.Format(",'{0}' = PrivateClub.{1}", sqlName2, sqlName2);
                    field.DataField = sqlName2;
                    field.HeaderText = sqlName2;
                    field.Visible = false;
                    //grdCompany.Columns.Remove(grdCompany.Columns[28 + j]);
                }
                j++;

            }
            //----------------------------------------------------
            DataTable dt = SelectSqlTable("SELECT itemNm,itemVal, CASE WHEN itemVal = '' THEN itemNm END AS itemCheck " +
                "FROM privateClubDetail  " +
                "where itemNm like 'sub_%' ");
            int i = 0;
            foreach (DataRow tmprow in dt.Rows)
            {
                sqlName = tmprow["itemNm"].ToString();
                sqlValue = tmprow["itemVal"].ToString();
                sqlCheck = tmprow["itemCheck"].ToString();

                var field = grdCompany.Columns[38 + i] as BoundColumn;

                //grdCompany.Columns[indexCount].Visible = true;
                if (sqlName != sqlCheck)
                {
                    sqlAll += string.Format(",'{0}' = PrivateClub.{1}", sqlValue, sqlName);
                    field.DataField = sqlValue;
                    field.HeaderText = sqlValue;
                }
                else
                {
                    sqlAll += string.Format(",'{0}' = PrivateClub.{1}", sqlName, sqlName);
                    field.DataField = sqlName;
                    field.HeaderText = sqlName;
                    field.Visible = false;
                    //grdCompany.Columns.Remove(grdCompany.Columns[39 + i]);
                }
                i++;

            }
            //total = 31 + i;



            ///this is sql statement which returns records
            string SQLStatement = string.Format(" SELECT 'Member Id' = PrivateDetail.memberid,'Firstmember Id' = PrivateDetail.firstmemberid, 'Prefix Name' = PrivateDetail.prefixNm, " +
            " 'Member Name (JPN)' = PrivateDetail.nameJ, 'Member Name (Eng)' = PrivateDetail.nameE, " +
            " 'Birth Place' = Private.birthplace, 'Applied Date' = PrivateDetail.appliedDate, " +
            " 'Birth Date' = PrivateDetail.birthdate, 'Effective Date' = PrivatePayment.effectiveDate, " +
            " 'Expired' = PrivatePayment.expireDate, 'Pay Duration' = PrivatePayment.payDuration, " +
            " 'Send Type' = Private.sendtype, 'Company Name' = PrivateAddressC.companyNm, " +
            " 'Company Address' = PrivateAddressC.Address, 'Company Phone' = PrivateAddressC.Phone, " +
            " 'Company Fax' = PrivateAddressC.Fax, 'Home Address' = PrivateAddressH.Address, " +
            " 'Home Phone' = PrivateAddressH.Phone, 'Mobile Phone' = PrivateAddressH.Mobile, " +
            " 'Position' = PrivateBoard.boardPosition, 'Member Type' = PrivateDetail.membertype, " +
            " 'Member Status' = PrivateDetail.memberStatus, 'Golf' = PrivateClub.golf, " +
            " 'Board' = PrivateClub.board, 'Lady' = PrivateClub.lady, " +
            " 'Children' = PrivateClub.children, 'Zukuzuku' = PrivateClub.zukuzuku ,   " +
            //" 'Meijinkai' = PrivateClub.meijinkai ," +
            "'Board List' = PrivateClub.sub_board_list  ,  'Club secretary' = PrivateClub.sub_secretary,  " +
            "'Bazaar volunteer' = PrivateClub.sub_volunteer,  'Social gathering members' = PrivateClub.sub_social,  " +
            "'Youth circle members' = PrivateClub.sub_member {0} {1} ," +
            " 'English test' = PrivateClub.ev_1 ,'Online Event' = PrivateClub.ev_2 ," +
            "'Softball' = PrivateClub.ev_3 , 'Yoga' = PrivateClub.ev_4,'Overseas resident members' = PrivateClub.ov_member ", sqlAll, sqlAll2);

            SQLStatement = SQLStatement + " FROM PrivateDetail LEFT OUTER JOIN Private " +
                " ON ( PrivateDetail.memberid = Private.memberid ) " +
                " LEFT OUTER JOIN PrivateAddress PrivateAddressC " +
                " ON ( PrivateDetail.memberid = PrivateAddressC.memberid " +
                " AND PrivateAddressC.addressType = 2) " +
                " LEFT OUTER JOIN PrivateAddress PrivateAddressH " +
                " ON ( PrivateDetail.memberid = PrivateAddressH.memberid " +
                " AND PrivateAddressH.addressType = 1) " +
                " LEFT OUTER JOIN PrivateClub " +
                " ON ( PrivateDetail.memberid = PrivateClub.memberid ) " +
                " LEFT OUTER JOIN PrivateBoard " +
                " ON ( PrivateDetail.memberid = PrivateBoard.memberid ) " +


                //old code comment for test 02/07/2025 14:16
                //" INNER JOIN PrivatePayment " +

                //new code comment for test 02/07/2025 14:16
                //" LEFT JOIN PrivatePayment " +

                //old code comment for test 02/07/2025 14:16
                //" ON ( PrivateDetail.memberid = PrivatePayment.memberid ) " +
                //end line


                //comment for test new code (over data) 14:33 02/07/2025
              
                //new code for test 14:18 02/07/2025 move where clause here
                 " LEFT JOIN ( " +
                 " SELECT * FROM PrivatePayment p1 " +
                 " WHERE p1.tranid = (SELECT MAX(p2.tranid) FROM PrivatePayment p2 WHERE p2.memberid = p1.memberid) " +
                 " ) AS PrivatePayment ON PrivateDetail.memberid = PrivatePayment.memberid " +
                //end line
             



                " LEFT OUTER JOIN PrivatePayAccount " +
                " ON ( PrivatePayment.tranid = PrivatePayAccount.tranid ) " +
                " LEFT OUTER JOIN PrivateAccount " +
                " ON ( PrivatePayAccount.accId = PrivateAccount.accId " +
                " AND PrivateDetail.memberid = PrivateAccount.memberId ) " 


               /*
                //comment for test 02/07/2025 14:15
             +  " WHERE PrivatePayment.tranid = ( SELECT MAX(PrivatePayment_2.tranid) " +
                " FROM PrivatePayment PrivatePayment_2 " +
                " WHERE PrivatePayment.memberId = PrivatePayment_2.memberId ) " 
                */


                
                //unused for show 5B and 7 02/07/2025 13:12
                
               //+ " AND PrivateDetail.firstMemberid = PrivateDetail.memberid "
                //end line
                


                ;



            //if (drplstSendType.SelectedValue != "0")
            //{
            //    SQLStatement = SQLStatement + " AND  ";
            //}

            if (companyNm.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivateAddressC.companyNm Like '%" + companyNm.Value.Trim() + "%' ";
            }

            if (rblMemberStatus.SelectedValue.ToUpper() != "BOTH")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail.memberStatus = '" + rblMemberStatus.SelectedValue.Trim() + "' ";
            }
            foreach (ListItem item in drplstClub.Items)
            {
                if (item.Selected)
                {
                    //message += item.Value + "\\n";
                    if (item.Value.ToUpper() == "GOLF")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.golf = 1 ";
                    }
                    else if (item.Value.ToUpper() == "BOARD")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.board = 1 ";
                    }
                    else if (item.Value.ToUpper() == "LADY")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.lady = 1 ";
                    }
                    else if (item.Value.ToUpper() == "CHILDREN")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.children = 1 ";
                    }
                    else if (item.Value.ToUpper() == "SUKUSUKU")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.zukuzuku = 1 ";
                    }
                    else if (item.Value == "sub_board_list")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_board_list = 1 ";
                    }
                    else if (item.Value == "sub_secretary")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_secretary = 1 ";
                    }
                    else if (item.Value == "sub_volunteer")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_volunteer = 1 ";
                    }
                    else if (item.Value == "sub_social")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_social = 1 ";
                    }
                    else if (item.Value == "sub_member")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_member = 1 ";
                    }
                    else if (item.Value.ToString().Trim() == "sub_tmp1")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_tmp1 = 1 ";
                    }
                    else if (item.Value.ToString().Trim() == "sub_tmp2")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.sub_tmp2 = 1 ";
                    }
                }
            }
            //drplstClub.Items.Insert(0, new ListItem("--Any--", "0"));
            //drplstClub.Items.Insert(1, new ListItem("Board", "board"));
            //drplstClub.Items.Insert(2, new ListItem("Board [List]", "sub_board_list"));
            //drplstClub.Items.Insert(3, new ListItem("Golf", "golf"));
            //drplstClub.Items.Insert(4, new ListItem("Lady", "lady"));
            //drplstClub.Items.Insert(5, new ListItem("Club secretary", "sub_secretary"));
            //drplstClub.Items.Insert(6, new ListItem("Bazaar volunteer", "sub_volunteer"));
            //drplstClub.Items.Insert(7, new ListItem("Social gathering members", "sub_social"));
            //drplstClub.Items.Insert(8, new ListItem("Youth circle members", "sub_member"));

            //DropDownList2.Items.Insert(1, new ListItem("English Test", "ev_1"));
            //DropDownList2.Items.Insert(2, new ListItem("Online Event", "ev_2"));
            //DropDownList2.Items.Insert(3, new ListItem("Softball", "ev_3"));
            //DropDownList2.Items.Insert(4, new ListItem("Yoga", "ev_4"));
            foreach (ListItem item in drpEvent.Items)
            {
                if (item.Selected)
                {
                    //message += item.Value + "\\n";
                    if (item.Value == "ev_1")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_1 = 1 ";
                    }
                    else if (item.Value == "ev_2")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_2 = 1 ";
                    }
                    else if (item.Value == "ev_3")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_3 = 1 ";
                    }
                    else if (item.Value == "ev_4")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_4 = 1 ";
                    }
                    else if (item.Value.ToString().Trim() == "ev_tmp1")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_tmp1 = 1 ";
                    }
                    else if (item.Value.ToString().Trim() == "ev_tmp2")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_tmp2 = 1 ";
                    }
                    else if (item.Value.ToString().Trim() == "ev_tmp3")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ev_tmp3 = 1 ";
                    }
                }
            }
            foreach (ListItem item in drpOther.Items)
            {
                if (item.Selected)
                {
                    //message += item.Value + "\\n";
                    if (item.Value == "zukuzuku")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.zukuzuku = 1 ";
                    }
                    else if (item.Value == "children")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.children = 1 ";
                    }
                    else if (item.Value == "ov_member")
                    {
                        SQLStatement = SQLStatement + " AND PrivateClub.ov_member = 1 ";
                    }
                }
            }
            //if(DropDownList1.Items[1].Selected == true)
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.zukuzuku = 1 ";
            //}
            //if (DropDownList1.Items[2].Selected == true)
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.children = 1 ";
            //}
            //if (DropDownList1.Items[3].Selected == true)
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.ov_member = 1 ";
            //}
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
            //if (drplstClub.SelectedValue.ToUpper() == "GOLF")
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.golf = 1 ";
            //}
            //else if (drplstClub.SelectedValue.ToUpper() == "BOARD")
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.board = 1 ";
            //}
            //else if (drplstClub.SelectedValue.ToUpper() == "LADY")
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.lady = 1 ";
            //}
            //else if (drplstClub.SelectedValue.ToUpper() == "CHILDREN")
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.children = 1 ";
            //}
            //else if (drplstClub.SelectedValue.ToUpper() == "SUKUSUKU")
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.zukuzuku = 1 ";
            //}
            //else if (drplstClub.SelectedValue.ToUpper() == "MEIJINKAI")
            //{
            //    SQLStatement = SQLStatement + " AND PrivateClub.meijinkai = 1 ";
            //}

            if (drplstMemberType.SelectedItem.Value.ToUpper() != "0")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail.membertype = '" + drplstMemberType.SelectedItem.Value.ToUpper() + "' ";
            }

            if (drplstHasFamily.SelectedItem.Value.Trim() == "1") // yes
            {
                SQLStatement = SQLStatement + " AND 0 < ( SELECT COUNT(PrivateDetail_02.memberid) " +
                    "           FROM PrivateDetail PrivateDetail_02 " +
                    "           WHERE PrivateDetail_02.firstMemberid <> PrivateDetail_02.memberid " +
                    "           AND PrivateDetail_02.firstMemberid = PrivateDetail.firstMemberid ) ";
            }
            else if (drplstHasFamily.SelectedItem.Value.Trim() == "2") // no
            {
                SQLStatement = SQLStatement + " AND 0 = ( SELECT COUNT(PrivateDetail_02.memberid) " +
                    "           FROM PrivateDetail PrivateDetail_02 " +
                    "           WHERE PrivateDetail_02.firstMemberid <> PrivateDetail_02.memberid " +
                    "           AND PrivateDetail_02.firstMemberid = PrivateDetail.firstMemberid ) ";
            }
            else
            {
                SQLStatement = SQLStatement + " ";
            }

            if (appliedDateFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail.appliedDate >= '" + DateTime.ParseExact(appliedDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (appliedDateTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail.appliedDate <= '" + DateTime.ParseExact(appliedDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if ((txtQuitDateFrom.Disabled == true) && (txtQuitDateFrom.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( PrivateDetail.cancelledDate >= '" + DateTime.ParseExact(txtQuitDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND PrivateDetail.memberStatus = 'NA' )";
            }

            if ((txtQuitDateTo.Disabled == true) && (txtQuitDateTo.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( PrivateDetail.cancelledDate <= '" + DateTime.ParseExact(txtQuitDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND PrivateDetail.memberStatus = 'NA' )";
            }

            if (address.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND ( PrivateAddressH.addressType = '1' and PrivateAddressH.address like '%" + address.Value + "%' ) ";
            }

            //SQLStatement = SQLStatement + " ORDER BY PrivateDetail.NameJ, PrivateDetail.NameE ";
            SQLStatement = SQLStatement + " ORDER BY PrivateAddressC.companyNm, PrivateDetail.NameE ";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "PrivateDetail");
            return myDataSet;
        }
        protected void rblMemberStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMemberStatus.SelectedItem.Text.Trim() == "NA")
            {
                txtQuitDateFrom.Disabled = false;
                txtQuitDateTo.Disabled = false;
            }
            else
            {
                txtQuitDateFrom.Value = "";
                txtQuitDateTo.Value = "";
                txtQuitDateFrom.Disabled = true;
                txtQuitDateTo.Disabled = true;
            }
        }

        protected void rblFieldFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblFieldFormat.Items[0].Selected)
            {
                setColumnAll();
                CheckBoxList1.Enabled = false;
                CheckBoxList2.Enabled = false;
                CheckBoxList3.Enabled = false;
            }
            else if (rblFieldFormat.Items[1].Selected)
            {
                CheckBoxList1.Enabled = true;
                CheckBoxList2.Enabled = true;
                CheckBoxList3.Enabled = true;
                setCheckbox();
                setColumnSelection();
            }
        }
        protected void setColumnAll()
        {
            int numberOfColumns = grdCompany.Columns.Count;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                grdCompany.Columns[indexCount].Visible = true;
            }
            Binddata();

        }
        protected void setCheckbox()
        {
            int numberOfColumns1 = CheckBoxList1.Items.Count;
            for (int indexCount = 0; indexCount < numberOfColumns1; indexCount++)
            {
                CheckBoxList1.Items[indexCount].Selected = true;
            }
            int numberOfColumns2 = CheckBoxList2.Items.Count;
            for (int indexCount = 0; indexCount < numberOfColumns2; indexCount++)
            {
                if (CheckBoxList2.Items[indexCount].Text == "")
                {
                    CheckBoxList2.Items[indexCount].Selected = false;
                }
                else
                {
                    CheckBoxList2.Items[indexCount].Selected = true;
                }
            }
            int numberOfColumns3 = CheckBoxList3.Items.Count;
            for (int indexCount = 0; indexCount < numberOfColumns3; indexCount++)
            {
                if (CheckBoxList3.Items[indexCount].Text == "")
                {
                    CheckBoxList3.Items[indexCount].Selected = false;
                }
                else
                {
                    CheckBoxList3.Items[indexCount].Selected = true;
                }
            }
        }
        protected void setColumnSelection()
        {
            int numberOfColumns = CheckBoxList1.Items.Count;
            bool columnSelect;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                columnSelect = CheckBoxList1.Items[indexCount].Selected;
                grdCompany.Columns[indexCount].Visible = columnSelect;
            }
            checkHeader();
        }
        protected void setColumnSelection2()
        {
            int numberOfColumns2 = CheckBoxList2.Items.Count;
            bool columnSelect2;
            for (int indexCount = 0; indexCount < numberOfColumns2; indexCount++)
            {
                columnSelect2 = CheckBoxList2.Items[indexCount].Selected;
                grdCompany.Columns[24 + indexCount].Visible = columnSelect2;
            }
            //Binddata();
            checkHeader();
        }
        protected void setColumnSelection3()
        {
            int numberOfColumns3 = CheckBoxList3.Items.Count;
            bool columnSelect3;
            for (int indexCount = 0; indexCount < numberOfColumns3; indexCount++)
            {
                columnSelect3 = CheckBoxList3.Items[indexCount].Selected;
                grdCompany.Columns[31 + indexCount].Visible = columnSelect3;
            }
            //Binddata();
            checkHeader();
        }
        protected void view_Click(object sender, EventArgs e)
        {
            Binddata();
        }

        protected void grdCompany_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            grdCompany.CurrentPageIndex = e.NewPageIndex;
            Binddata();
        }

        protected void checkHeader()
        {
            string sqlName = "", sqlName2 = "";
            string sqlValue = "", sqlValue2 = "";
            string sqlCheck = "", sqlCheck2 = "";
            //----------------------------------------------------
            DataTable dtt = SelectSqlTable("SELECT itemNm,itemVal, CASE WHEN itemVal = '' THEN itemNm END AS itemCheck " +
                "FROM privateClubDetail  " +
                "where itemNm like 'ev_%' ");
            int j = 0;
            foreach (DataRow tmprow in dtt.Rows)
            {
                sqlName2 = tmprow["itemNm"].ToString();
                sqlValue2 = tmprow["itemVal"].ToString();
                sqlCheck2 = tmprow["itemCheck"].ToString();

                var field = grdCompany.Columns[28 + j] as BoundColumn;
                if (sqlName2 != sqlCheck2)
                {
                    field.DataField = sqlValue2;
                    field.HeaderText = sqlValue2;
                }
                else
                {
                    field.DataField = sqlName2;
                    field.HeaderText = sqlName2;
                    field.Visible = false;
                }
                j++;

            }
            //----------------------------------------------------
            DataTable dt = SelectSqlTable("SELECT itemNm,itemVal, CASE WHEN itemVal = '' THEN itemNm END AS itemCheck " +
                "FROM privateClubDetail  " +
                "where itemNm like 'sub_%' ");
            int i = 0;
            foreach (DataRow tmprow in dt.Rows)
            {
                sqlName = tmprow["itemNm"].ToString();
                sqlValue = tmprow["itemVal"].ToString();
                sqlCheck = tmprow["itemCheck"].ToString();

                var field = grdCompany.Columns[38 + i] as BoundColumn;
                if (sqlName != sqlCheck)
                {
                    field.DataField = sqlValue;
                    field.HeaderText = sqlValue;
                }
                else
                {
                    field.DataField = sqlName;
                    field.HeaderText = sqlName;
                    field.Visible = false;
                }
                i++;

            }
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setColumnSelection();
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            //rblMemberStatus.SelectedValue = "Both";
            //drplstClub.SelectedIndex = 0;
            //drplstMemberType.SelectedValue = "0";
            //drplstSendType.SelectedValue = "0";
            //companyNm.Value = "";
            //drplstHasFamily.SelectedValue = "";
            //appliedDateFrom.Value = "";
            //appliedDateTo.Value = "";
            //txtQuitDateFrom.Value = "";
            //txtQuitDateTo.Value = "";
            //address.Value = "";
            rblMemberStatus.SelectedValue = "Both";

            drplstClub.SelectedIndex = 0;
            drpEvent.SelectedIndex = 0;
            drpOther.SelectedIndex = 0;

            drplstMemberType.SelectedIndex = 0;
            drplstSendType.SelectedIndex = 0;
            companyNm.Value = "";
            drplstHasFamily.SelectedIndex = 0;
            appliedDateFrom.Value = "";
            appliedDateTo.Value = "";
            txtQuitDateFrom.Value = "";
            txtQuitDateTo.Value = "";
            address.Value = "";
            rblFieldFormat.SelectedIndex = 0;

            if (rblFieldFormat.Items[0].Selected)
            {
                setColumnAll();
                CheckBoxList1.Enabled = false;
                CheckBoxList2.Enabled = false;
                CheckBoxList3.Enabled = false;
            }
            //rblFieldFormat.SelectedIndex = 0;
            //Binddata();
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setColumnSelection2();
        }

        protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            setColumnSelection3();
        }
    }
}