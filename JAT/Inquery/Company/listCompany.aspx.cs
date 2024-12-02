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
    public partial class listCompany : System.Web.UI.Page
    {
        private SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Binddata();
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        //}
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
        private DataSet GetData()
        {
            IFormatProvider culture = new CultureInfo("en-US", true);
            connection();
            //String SQLStatement = "SELECT * " +
            //"FROM  CompanyMember";

            //string SQLStatement = " SELECT companyMember.companyId, companyMember.companyNmJ, " +
            //    " companyMember.companyNmE, companyMember.companyNmEE, " +
            //    " companyMember.busType, companyMember.appliedDate, " +
            //    " companyMember.establishedDate, companyMember.memberStatus, " +
            //    " companyMember.sendType, companyMember.address, " +
            //    " companyMember.phone, companyMember.fax, " +
            //    " companyMember.email, companyMember.represNm, " +
            //    " companyMember.represNmE, companyMember.represPosition, " +
            //    " companyMember.contNm, companyMember.contPosition, " +
            //    " companyMember.remark, companyMember.payMethod, " +
            //    " companyMember.payPeriod, companyMember.payDuration, " +
            //    " companyMember.getInvoice, companyMember.withHolding, " +
            //    " companyMember.updatedDate " +
            //    " FROM companyMember";

            //if (companyNmE.Value.Trim() != "")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.companyNmE LIKE '%" + companyNmE.Value.Trim() + "%' ";
            //}

            //if (memberStatus.SelectedItem.Value.ToUpper() != "BOTH")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = '" + memberStatus.SelectedValue.Trim() + "' ";
            //}

            //if (drplstHasPhone.Value.Trim() == "1") // Yes
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.phone IS NOT NULL " +
            //        " AND companyMember.phone <> '' ";
            //}
            //else if (drplstHasPhone.Value.Trim() == "0") // No
            //{
            //    SQLStatement = SQLStatement + " AND ( companyMember.phone IS NULL " +
            //        " OR companyMember.phone = '' ) ";
            //}

            //if (drplstInBkk.Value.Trim() == "1") // Yes
            //{
            //    SQLStatement = SQLStatement + " AND ( companyMember.address LIKE '%BANGKOK%' " +
            //        " OR companyMember.address LIKE '%BKK%' ) ";
            //}
            //else if (drplstInBkk.Value.Trim() == "0") // No
            //{
            //    SQLStatement = SQLStatement + " AND ( companyMember.address NOT LIKE '%BANGKOK%' " +
            //        " AND companyMember.address NOT LIKE '%BKK%' ) ";
            //}

            //if (sendType.Value.Trim() != "-- Any --")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.sendType = '" + sendType.Value.Trim() + "' ";
            //}

            //if (appliedDateFrom.Value.Trim() != "")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.appliedDate >= '" + DateTime.ParseExact(appliedDateFrom.Value.Trim(), "yyyy/M/d", culture).ToString("M/d/yyyy") + "' ";
            //}

            //if (appliedDateTo.Value.Trim() != "")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.appliedDate <= '" + DateTime.ParseExact(appliedDateTo.Value.Trim(), "yyyy/M/d", culture).ToString("M/d/yyyy") + "' ";
            //}

            //if ((txtQuitDateFrom.Disabled == true) && (txtQuitDateFrom.Value.Trim() != ""))
            //{
            //    SQLStatement = SQLStatement + " AND ( companyMember.updatedDate >= '" + DateTime.ParseExact(txtQuitDateFrom.Value.Trim(), "yyyy/M/d", culture).ToString("M/d/yyyy") + "' " +
            //        " AND company.memberStatus = 'NA' )";
            //}

            //if ((txtQuitDateTo.Disabled == true) && (txtQuitDateTo.Value.Trim() != ""))
            //{
            //    SQLStatement = SQLStatement + " AND ( companyMember.updatedDate <= '" + DateTime.ParseExact(txtQuitDateTo.Value.Trim(), "yyyy/M/d", culture).ToString("M/d/yyyy") + "' " +
            //        " AND company.memberStatus = 'NA' )";
            //}
            //if ((address.Disabled == true) && (address.Value.Trim() != ""))
            //{
            //    SQLStatement = SQLStatement + " AND ( companyMember.address like '%" + address.Value + "%'  ) ";
            //}

            //SQLStatement = SQLStatement + " ORDER BY companyMember.companyNmJ, companyMember.companyNmE ";

            //        string SQLStatement = " SELECT companyMember.companyId, companyMember.companyNmJ, " +
            //            " companyMember.companyNmE, companyMember.companyNmEE, " +
            //            " companyMember.busType, companyMember.appliedDate, " +
            //            " companyMember.establishedDate, companyMember.memberStatus, " +
            //            " companyMember.sendType, companyMember.address, " +
            //            " companyMember.phone, companyMember.fax, " +
            //            " companyMember.email, companyMember.represNm, " +
            //            " companyMember.represNmE, companyMember.represPosition, " +
            //            " companyMember.contNm, companyMember.contPosition, " +
            //            " companyMember.remark, companyMember.payMethod, " +
            //            " companyMember.payPeriod, companyMember.payDuration, " +
            //            " companyMember.getInvoice, companyMember.withHolding, " +
            //            " companyMember.updatedDate " +
            //            " FROM companyMember LEFT OUTER JOIN companyPayment ON ( companyMember.companyId = companyPayment.companyId ) " +
            //            " LEFT OUTER JOIN companyAccount ON ( companyPayment.accId = companyAccount.accId and companyPayment.companyId = companyAccount.companyId) " +
            //            " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
            //            "                               FROM companyPayment companyPayment_2 " +
            //"                               WHERE companyPayment.companyId = companyPayment_2.companyId ) AND companyPayment.Deleted_at IS NULL ";

            string SQLStatement = " SELECT companyMember.companyId, companyMember.companyNmJ, " +
                " companyMember.companyNmE, companyMember.companyNmEE, " +
                " companyMember.busType, companyMember.appliedDate, " +
                " companyMember.establishedDate, companyMember.memberStatus, " +
                " companyMember.sendType, companyMember.address, " +
                " companyMember.phone, companyMember.fax, " +
                " companyMember.email, companyMember.represNm, " +
                " companyMember.represNmE, companyMember.represPosition, " +
                " companyMember.contNm, companyMember.contPosition, " +
                " companyMember.remark, companyMember.payMethod, " +
                " companyMember.payPeriod, companyMember.payDuration, " +
                " companyMember.getInvoice, companyMember.withHolding, " +
                " companyMember.updatedDate " +
                " FROM companyMember LEFT OUTER JOIN companyPayment ON ( companyMember.companyId = companyPayment.companyId ) " +
                " LEFT OUTER JOIN companyAccount ON ( companyPayment.accId = companyAccount.accId and companyPayment.companyId = companyAccount.companyId) " +
                " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
                "                               FROM companyPayment companyPayment_2 " +
                "                               WHERE companyPayment.companyId = companyPayment_2.companyId ) ";

            if (companyNmE.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND companyMember.companyNmE LIKE '%" + companyNmE.Value.Trim() + "%' ";
            }

            if (memberStatus.SelectedValue.ToUpper() != "BOTH")
            {
                SQLStatement = SQLStatement + " AND companyMember.memberStatus = '" + memberStatus.SelectedValue.Trim() + "' ";
            }

            if (drplstHasPhone.Value.Trim() == "1") // Yes
            {
                SQLStatement = SQLStatement + " AND companyMember.phone IS NOT NULL " +
                    " AND companyMember.phone <> '' ";
            }
            else if (drplstHasPhone.Value.Trim() == "2") // No
            {
                SQLStatement = SQLStatement + " AND ( companyMember.phone IS NULL " +
                    " OR companyMember.phone = '' ) ";
            }

            if (drplstInBkk.Value.Trim() == "1") // Yes
            {
                SQLStatement = SQLStatement + " AND ( companyMember.address LIKE '%BANGKOK%' " +
                    " OR companyMember.address LIKE '%BKK%' ) ";
            }
            else if (drplstInBkk.Value.Trim() == "2") // No
            {
                SQLStatement = SQLStatement + " AND ( companyMember.address NOT LIKE '%BANGKOK%' " +
                    " AND companyMember.address NOT LIKE '%BKK%' ) ";
            }

            if (sendType.Value.Trim() != "-- Any --")
            {
                SQLStatement = SQLStatement + " AND companyMember.sendType = '" + sendType.Value.Trim() + "' ";
            }

            if (appliedDateFrom.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND companyMember.appliedDate >= '" + DateTime.ParseExact(appliedDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if (appliedDateTo.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND companyMember.appliedDate <= '" + DateTime.ParseExact(appliedDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' ";
            }

            if ((txtQuitDateFrom.Disabled == true) && (txtQuitDateFrom.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( companyMember.updatedDate >= '" + DateTime.ParseExact(txtQuitDateFrom.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND company.memberStatus = 'NA' )";
            }

            if ((txtQuitDateTo.Disabled == true) && (txtQuitDateTo.Value.Trim() != ""))
            {
                SQLStatement = SQLStatement + " AND ( companyMember.updatedDate <= '" + DateTime.ParseExact(txtQuitDateTo.Value.Trim(), "dd/MM/yyyy", culture).ToString("M/d/yyyy") + "' " +
                    " AND company.memberStatus = 'NA' )";
            }
            if (address.Value.Trim() != "")
            {
                SQLStatement = SQLStatement + " AND ( companyMember.address like '%" + address.Value + "%'  ) ";
            }

            SQLStatement = SQLStatement + " ORDER BY companyMember.companyNmJ, companyMember.companyNmE ";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.Fill(myDataSet, "companyMember");
            return myDataSet;
        }

        protected void rblFieldFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rblFieldFormat.SelectedValue == "Selection")
            //{
            //    CheckBoxList1.Enabled = true;
            //}
            //else
            //    CheckBoxList1.Enabled = false;
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
            //if (CheckBoxList1.SelectedValue== "Id")
            //{
            //    this.GridView1.Columns[0].Visible = false;
            //}
            //if(CheckBoxList1.SelectedValue == "Name (Remark)")
            //{
            //    this.GridView1.Columns[1].Visible = false;
            //}
        }

        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            //if (DataGrid1.EditIndex != -1)
            //{
            //    // Use the Cancel property to cancel the paging operation.
            //    e.Cancel = true;

            //    // Display an error message.
            //    int newPageNumber = e.NewPageIndex + 1;
            //    //Message.Text = "Please update the record before moving to page " +
            //    //  newPageNumber.ToString() + ".";
            //}
            //else
            //{
            //    // Clear the error message.
            //    // Message.Text = "";
            //    grdCompany.PageIndex = e.NewPageIndex;
            //    BindGridCompany();
            //    //grdCompany.DataBind();
            //}

            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            //GridView1.PageIndex = e.NewPageIndex;
            // Rebind the data. 
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