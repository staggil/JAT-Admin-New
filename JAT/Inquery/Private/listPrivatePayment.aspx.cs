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

namespace JAT.Inquery.Private
{
    public partial class listPrivatePayment : System.Web.UI.Page
    {
        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Binddata();
            if (!Page.IsPostBack)
            {
                LoadSubjects();
                Binddata();
            }

            //GetDropDown();
            txtQuitDateFrom.Disabled = true;
            txtQuitDateTo.Disabled = true;
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

            // Add the initial item - you can add this even if the options from the
            // db were not successfully loaded
            drplstMemberType.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        private void Binddata()
        {
            //DataTable subjects = new DataTable();
            //var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            //using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            //{

            //    try
            //    {
            //        SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct MemberType FROM SMemberType", con);
            //        adapter.Fill(subjects);

            //        drplstMemberType.DataSource = subjects;
            //        //drplstMemberType.DataTextField = "MemberType";
            //        drplstMemberType.DataValueField = "MemberType";
            //        drplstMemberType.DataBind();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Handle the error
            //    }

            //}

            //// Add the initial item - you can add this even if the options from the
            //// db were not successfully loaded
            //drplstMemberType.Items.Insert(0, new ListItem("<Select Subject>", "0"));

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


            // debug print to Output window
            System.Diagnostics.Debug.WriteLine("Selected Member Type = " + drplstMemberType.SelectedItem?.Value);

            // หรือถ้าจะ Response.Write
            Response.Write("Selected Member Type = [" + drplstMemberType.SelectedItem?.Value + "]<br/>");

            ///this is sql statement which returns records
            string SQLStatement = " SELECT 'Member Id' = PrivateDetail_01.memberid,PrivateDetail_01.firstMemberid, " +
                " 'Company Name' = PrivateAddressC.companyNm, 'Prefix Name' = PrivateDetail_01.prefixNm, " +
                " 'Member Name' = PrivateDetail_01.nameE, 'Member Type' = PrivateDetail_01.membertype, " +
                " 'Payment Method' = PrivatePayment.paymethod, 'Account No.' = PrivateAccount.accno, " +
                " 'Bank Code' = PrivateAccount.bankcode, 'Payment Date' = PrivatePayment.paymentdate, " +
                " 'Pay Duration' = PrivatePayment.payduration, 'Send Type' = Private.sendtype, " +
                " 'Company Address' = PrivateAddressC.Address, 'Company Phone' = PrivateAddressC.Phone, " +
                " 'Company Fax' = PrivateAddressC.Fax, 'Home Address' = PrivateAddressH.Address, " +
                " 'Home Phone' = PrivateAddressH.Phone, 'Mobile Phone' = PrivateAddressH.Mobile, " +
                " 'Member Status' = PrivateDetail_01.memberStatus ";

            if ((drplstHasFamily.SelectedItem.Value.Trim() == "1") || (drplstHasFamily.SelectedItem.Value.Trim() == "2"))
            {
                SQLStatement = SQLStatement + ", 'Family Name' = PrivateDetail_02.nameE ";
            }
            else
            {
                SQLStatement = SQLStatement + ", 'Family Name' = null ";
            }

            SQLStatement = SQLStatement + " FROM PrivateDetail PrivateDetail_01 " +
                " LEFT OUTER JOIN Private " +
                " ON ( PrivateDetail_01.memberid = Private.memberid ) ";

            if ((drplstHasFamily.SelectedItem.Value.Trim() == "1") || (drplstHasFamily.SelectedItem.Value.Trim() == "2"))
            {
                SQLStatement = SQLStatement + " LEFT OUTER JOIN PrivateDetail PrivateDetail_02 " +
                    " ON ( PrivateDetail_01.firstMemberid = PrivateDetail_02.firstMemberid " +
                    " AND PrivateDetail_02.firstMemberid <> PrivateDetail_02.memberid ) ";
            }
            else
            {
                SQLStatement = SQLStatement + " ";
            }

            SQLStatement = SQLStatement + " LEFT OUTER JOIN PrivateAddress PrivateAddressC " +
                " ON ( PrivateDetail_01.memberid = PrivateAddressC.memberid " +
                " AND PrivateAddressC.addressType = 2) " +
                " LEFT OUTER JOIN PrivateAddress PrivateAddressH " +
                " ON ( PrivateDetail_01.memberid = PrivateAddressH.memberid " +
                " AND PrivateAddressH.addressType = 1) " +
                " INNER JOIN PrivatePayment " +
                " ON ( PrivateDetail_01.memberid = PrivatePayment.memberid ) " +
                " LEFT OUTER JOIN PrivatePayAccount " +
                " ON ( PrivatePayment.tranid = PrivatePayAccount.tranid ) " +
                " LEFT OUTER JOIN PrivateAccount " +
                " ON ( PrivatePayAccount.accId = PrivateAccount.accId " +
                " AND PrivateDetail_01.memberid = PrivateAccount.memberId ) " +
                " WHERE " +

                //comment for debugging 10:57 11/06/2025 uncommentted when you want to flitering membertype 7
                // "PrivateDetail_01.firstMemberid = PrivateDetail_01.memberid " +
                //end line

                //" AND " +
                "PrivatePayment.tranid = ( SELECT MAX(PrivatePayment_2.tranid) " +
                "                               FROM PrivatePayment PrivatePayment_2 " +
                "                               WHERE PrivatePayment.memberId = PrivatePayment_2.memberId ) ";

            if (memberStatus.SelectedValue.ToUpper() != "BOTH")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail_01.memberStatus = '" + memberStatus.SelectedValue.Trim() + "' ";
            }

            if (drplstMemberType.SelectedItem.Value.ToUpper() != "0")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail_01.membertype = '" + drplstMemberType.SelectedItem.Value.ToUpper() + "' ";
            }

            if (drplstPayMethod.SelectedValue.ToUpper() == "CASH")
            {
                SQLStatement = SQLStatement + " AND ( PrivatePayment.payMethod = 'K' " +
                    " OR PrivatePayment.payMethod = 'J' " +
                    " OR PrivatePayment.payMethod = 'P' ) ";
            }
            else if (drplstPayMethod.SelectedValue.ToUpper() == "BANK")
            {
                SQLStatement = SQLStatement + " AND ( PrivatePayment.payMethod = 'T' " +
                    " OR PrivatePayment.payMethod = 'S' " +
                    " OR PrivatePayment.payMethod = 'B' ) ";
            }

            if (drplstHasFamily.SelectedItem.Value.Trim() == "1") // yes
            {
                SQLStatement = SQLStatement + " AND PrivateDetail_02.NameE IS NOT NULL " +
                    " AND PrivateDetail_02.NameE <> '' ";
            }
            else if (drplstHasFamily.SelectedItem.Value.Trim() == "2") // no
            {
                SQLStatement = SQLStatement + " AND ( PrivateDetail_02.NameE IS NULL " +
                    " OR PrivateDetail_02.NameE = '' ) ";
            }
            else
            {
                SQLStatement = SQLStatement + " ";
            }


            if (drplstOverdue.SelectedValue.Trim() == "1") // Yes
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.expiredate <= '" + DateTime.Today.ToString("M/d/yyyy") + "' ";
            }
            else if (drplstOverdue.SelectedValue.Trim() == "0") // No
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.expiredate > '" + DateTime.Today.ToString("M/d/yyyy") + "' ";
            }

            if (drplstDuration.SelectedValue.Trim() == "1") // 6 Month
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.payDuration = 6 ";
            }
            else if (drplstDuration.SelectedValue.Trim() == "2") // > 6 Month
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.payDuration > 6 ";
            }

            if (appliedDateFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail_01.appliedDate >= '" + DateTime.ParseExact(appliedDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (appliedDateTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivateDetail_01.appliedDate <= '" + DateTime.ParseExact(appliedDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if ((txtQuitDateFrom.Disabled == true) && (txtQuitDateFrom.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( PrivateDetail_01.cancelledDate >= '" + DateTime.ParseExact(txtQuitDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND PrivateDetail_01.memberStatus = 'NA' )";
            }

            if ((txtQuitDateTo.Disabled == true) && (txtQuitDateTo.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( PrivateDetail_01.cancelledDate <= '" + DateTime.ParseExact(txtQuitDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND PrivateDetail_01.memberStatus = 'NA' )";
            }

            if (payMethodFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.paymentDate >= '" + DateTime.ParseExact(payMethodFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (payMethodTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.paymentDate <= '" + DateTime.ParseExact(payMethodTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (expiredDateFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.expiredate >= '" + DateTime.ParseExact(expiredDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (expiredDateTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND PrivatePayment.expiredate <= '" + DateTime.ParseExact(expiredDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (rblPayAt.SelectedItem.Text.ToUpper() == "ANNEX")
            {
                SQLStatement = SQLStatement + " AND Privatepayment.payRemark Like '%Annex%' ";
            }

            SQLStatement = SQLStatement + " ORDER BY PrivateAddressC.companyNm, PrivateDetail_01.nameE ";



            // แสดง SQL สุดท้ายที่ใช้จริง (สำหรับ debug)
            //Response.Write("<pre>SQL Statement: " + Server.HtmlEncode(SQLStatement) + "</pre>");
            //end line

            //เขียนแล้ว ออก
            // หรือถ้าจะ Response.Write
            //Response.Write("Selected Member Type = [" + drplstMemberType.SelectedItem?.Value + "]<br/>");
            //


            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "PrivateDetail");

            /*
            // ✅ แทรกจุด debug ตรงนี้
            foreach (DataRow row in myDataSet.Tables["PrivateDetail"].Rows)
            {
                Response.Write("<br>memberid: " + row["memberid"]);
                Response.Write(" | membertype: " + row["membertype"]);
            }
            //for debugging end line
            */



            return myDataSet;
        }

        protected void memberStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (memberStatus.SelectedItem.Text.Trim() == "NA")
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
            }
            else if (rblFieldFormat.Items[1].Selected)
            {
                CheckBoxList1.Enabled = true;
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
        }
        protected void setCheckbox()
        {
            int numberOfColumns = CheckBoxList1.Items.Count;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                CheckBoxList1.Items[indexCount].Selected = true;
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
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setColumnSelection();
        }

        protected void grdCompany_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            grdCompany.CurrentPageIndex = e.NewPageIndex;
            Binddata();
        }

        protected void view_Click(object sender, EventArgs e)
        {
            Binddata();
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}