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



namespace JAT.Inquery.Company
{
    public partial class listCompanyPayment : System.Web.UI.Page
    {
        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
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
            if (!IsPostBack)
                Binddata();
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        private void Binddata()
        {
            //au_Wirte_10_MAY_2021
            try
            {
                DataGrid1.DataSource = GetData().Tables["companyMember"].DefaultView;
                DataGrid1.DataBind();
            }
            catch (Exception)
            {
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataBind();
            }
}
        protected override void InitializeCulture()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        }
        private DataSet GetData()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
            IFormatProvider culture = new CultureInfo("en-US", true);
            connection();

            //        string SQLStatement = " SELECT companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
            //            " companyMember.payMethod, companyAccount.accNumber, companyAccount.bankCode, " +
            //            " companyPayment.TotalPerMonth, companyPayment.paymentDate, companyMember.address, " +
            //            " companyPayment.payRemark " +
            //            " FROM companyMember LEFT OUTER JOIN companyPayment ON ( companyMember.companyId = companyPayment.companyId and companyPayment.tranid = (select max(tranid) from companyPayment aa where aa.companyid = companyPayment.companyid)) " +
            //            " LEFT OUTER JOIN companyAccount ON ( companyPayment.accId = companyAccount.accId and companyaccount.companyid = companyMember.companyid ) " +
            //            " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
            //            "                               FROM companyPayment companyPayment_2 " +
            //"                               WHERE companyPayment.companyId = companyPayment_2.companyId ) AND CompanyPayment.Deleted_at IS NULL";

            string SQLStatement = " SELECT companyMember.companyId, companyMember.companyNmJ, companyMember.companyNmE, " +
                " companyMember.payMethod, companyAccount.accNumber, companyAccount.bankCode, " +
                " companyPayment.TotalPerMonth, companyPayment.paymentDate, companyMember.address, " +
                " companyPayment.payRemark " +
                " FROM companyMember LEFT OUTER JOIN companyPayment ON ( companyMember.companyId = companyPayment.companyId and companyPayment.tranid = (select max(tranid) from companyPayment aa where aa.companyid = companyPayment.companyid)) " +
                " LEFT OUTER JOIN companyAccount ON ( companyPayment.accId = companyAccount.accId and companyaccount.companyid = companyMember.companyid ) " +
                " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
                "                               FROM companyPayment companyPayment_2 " +
                "                               WHERE companyPayment.companyId = companyPayment_2.companyId ) ";

            if (memberStatus.SelectedItem.Value.ToUpper() != "BOTH")
            {
                SQLStatement = SQLStatement + " AND companyMember.memberStatus = '" + memberStatus.SelectedValue.Trim() + "' ";
            }

            if (payMethod.Value.ToUpper() == "CASH")
            {

                SQLStatement = SQLStatement + " AND ( companyMember.payMethod = 'K' " +
                    " OR companyMember.payMethod = 'J' " +
                    " OR companyMember.payMethod = 'P' ) ";
            }
            else if (payMethod.Value.ToUpper() == "BANK")
            {

                SQLStatement = SQLStatement + " AND ( companyMember.payMethod = 'T' " +
                    " OR companyMember.payMethod = 'S' " +
                    " OR companyMember.payMethod = 'B' ) ";
            }

            if (drplstOverdue.SelectedValue.Trim() == "1") // Yes DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            {
                SQLStatement = SQLStatement + " AND DATEADD(Month, companyPayment.noPayMonth, companyPayment.effectiveDate) <= '" + DateTime.Now.ToString("M/d/yyyy", new CultureInfo("en-US")) + "' ";
            }
            else if (drplstOverdue.SelectedValue.Trim() == "0") // No
            {
                SQLStatement = SQLStatement + " AND DATEADD(Month, companyPayment.noPayMonth, companyPayment.effectiveDate) > '" + DateTime.Now.ToString("M/d/yyyy", new CultureInfo("en-US")) + "' ";
            }

            if (drplstDuration.SelectedValue.Trim() == "1") // 1 Year
            {
                SQLStatement = SQLStatement + " AND payDuration = 12 ";
            }
            else if (drplstDuration.SelectedValue.Trim() == "2") // < 1 Year
            {
                SQLStatement = SQLStatement + " AND payDuration < 12 ";
            }

            if (appliedDateFrom.Value.Trim() != "")
            {
                //SQLStatement = SQLStatement + " AND companyMember.appliedDate >= '" + DateTime.ParseExact(appliedDateFrom.Value.Trim(), "yyyy/M/d", culture).ToString("M/d/yyyy") + "' ";
                SQLStatement = SQLStatement + " AND companyMember.appliedDate >= '" + DateTime.ParseExact(appliedDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (appliedDateTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND companyMember.appliedDate <= '" + DateTime.ParseExact(appliedDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if ((txtQuitDateFrom.Disabled == false) && (txtQuitDateFrom.Value.Trim() != "")) //((txtQuitDateFrom.Disabled == true) && (txtQuitDateFrom.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( companyMember.updatedDate >= '" + DateTime.ParseExact(txtQuitDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND memberStatus = 'NA' )";
            }

            if ((txtQuitDateTo.Disabled == false) && (txtQuitDateTo.Value.Trim() != "")) //((txtQuitDateTo.Disabled == true) && (txtQuitDateTo.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( companyMember.updatedDate <= '" + DateTime.ParseExact(txtQuitDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND memberStatus = 'NA' )";
            }

            if (payMethodFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND companyPayment.paymentDate >= '" + DateTime.ParseExact(payMethodFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (payMethodTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND companyPayment.paymentDate <= '" + DateTime.ParseExact(payMethodTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (expiredDateFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND DATEADD(Month, companyPayment.noPayMonth, companyPayment.effectiveDate) >= '" + DateTime.ParseExact(expiredDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (expiredDateTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND DATEADD(Month, companyPayment.noPayMonth, companyPayment.effectiveDate) <= '" + DateTime.ParseExact(expiredDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            SQLStatement = SQLStatement + " ORDER BY companyMember.companyNmJ, companyMember.companyNmE ";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            dataAdapter.SelectCommand.CommandTimeout = 1800;
			myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "companyMember");
            return myDataSet;
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
            int numberOfColumns = DataGrid1.Columns.Count;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                DataGrid1.Columns[indexCount].Visible = true;
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
                DataGrid1.Columns[indexCount].Visible = columnSelect;
            }
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setColumnSelection();
        }

        protected void rblMemberStatus_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void view_Click(object sender, EventArgs e)
        {
            Binddata();
        }

        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;

            // Rebind the data. 

            Binddata();
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}