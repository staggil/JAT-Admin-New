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

namespace JAT.Inquery.Company
{
    public partial class listCompanyStat : System.Web.UI.Page
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
        private void Binddata()
        {
            if (rblViewType.SelectedItem.Value.Trim() != "2")
            {
                if (rblViewType.SelectedItem.Value.Trim().Equals("1"))
                {
                    grdCompany.Columns[2].Visible = false;
                    grdCompany.Columns[3].Visible = false;
                    grdCompany.Columns[4].Visible = false;
                    grdCompany.Columns[5].Visible = false;
                    grdCompany.Columns[6].Visible = true;
                }
                else
                {
                    grdCompany.Columns[2].Visible = true;
                    grdCompany.Columns[3].Visible = true;
                    grdCompany.Columns[4].Visible = false;
                    grdCompany.Columns[5].Visible = false;
                    grdCompany.Columns[6].Visible = false;
                }
            }
            else
            {
                grdCompany.Columns[2].Visible = false;
                grdCompany.Columns[3].Visible = false;
                grdCompany.Columns[4].Visible = true;
                grdCompany.Columns[5].Visible = true;
                grdCompany.Columns[6].Visible = false;
            }
            grdCompany.DataSource = GetMemberTypeData().Tables["companyMember"].DefaultView;
            grdCompany.DataBind();
        }

        private DataSet GetMemberTypeData()
        {
            connection();
            /////this is sql statement which returns records
            //string SQLStatement = "select *, (MemberFee * NumberOfPersons) as TotalMemberFee, (NewsFee * NumberOfPersons) as TotalNewsFee from (select TotalPerMonth, sum(NumberOfPersons) as NumberOfPersons, max(MemberFee) as MemberFee , max(NewsFee) as NewsFee, " +
            //    " sum((TotalPerMonth * NumberOfPersons)) as totalfee  from ( SELECT " +
            //    " (case when SCompanyFee.CTotalPerMonth >= 300 and SCompanyFee.CTotalPerMonth <= 800 then 1000 else " +
            //    " case when SCompanyFee.CTotalPerMonth >= 850 and SCompanyFee.CTotalPerMonth <= 1000 then 1500 else " +
            //    //  " case when companypayment.companyid = '000805' then 1300 else  " +
            //    " case when SCompanyFee.CTotalPerMonth >= 1000 and SCompanyFee.CTotalPerMonth <= 1500 and SCompanyFee.CTotalPerMonth <> 1200 then 2000 else SCompanyFee.CTotalPerMonth  end end end) as TotalPerMonth " +
            //    " ,  Count(companyPayment.TotalPerMonth) AS NumberOfPersons,  " +
            //    " AVG(SCompanyFee.mem_fee) AS MemberFee,  Sum(SCompanyFee.mem_fee) AS TotalMemberFee,  AVG(SCompanyFee.news_fee) AS NewsFee,  " +
            //    " Sum(SCompanyFee.news_fee) AS TotalNewsFee  " +
            //    " FROM SCompanyFee inner join tbleffective aa on scompanyfee.Effectiveid = '1' " +
            //    " LEFT OUTER JOIN companyPayment ON ( SCompanyFee.CTotalPerMonth = companyPayment.TotalPerMonth )  " +
            //    " LEFT OUTER JOIN companyMember ON ( companyPayment.companyId = companyMember.companyId )  " +
            //    " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment.tranid) " +
            //    " FROM companyPayment companyPayment_2 " +
            //    " WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
            //    " and aa.effectiveid = '1' ";

            //if (cboStatus.Value == "A")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'A' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '1' ";
            //}
            //else if (cboStatus.Value == "NA")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'NA' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '1' ";
            //}

            //SQLStatement = SQLStatement + "  and TotalPerMonth <> 1200 and cast(companyMember.companyId as int) not in (select cast(companyid as int) from companyCheck) GROUP BY SCompanyFee.CTotalPerMonth ";

            //SQLStatement = SQLStatement + "union SELECT " +
            //   " (case when SCompanyFee.CTotalPerMonth >= 300 and SCompanyFee.CTotalPerMonth <= 800 then 1000 else  " +
            //   " case when SCompanyFee.CTotalPerMonth >= 850 and SCompanyFee.CTotalPerMonth <= 1000 then 1500 else " +
            //   // " case when companypayment.companyid = '000805' then 1300 else  " +
            //   " case when SCompanyFee.CTotalPerMonth >= 1000 and SCompanyFee.CTotalPerMonth <= 1500 and SCompanyFee.CTotalPerMonth <> 1200 then 2000 else SCompanyFee.CTotalPerMonth end end end) as CTotalPerMonth " +
            //   " ,  Count(companyPayment.TotalPerMonth) AS NumberOfPersons,  AVG(SCompanyFee.mem_fee) AS MemberFee,  " +
            //   " Sum(SCompanyFee.mem_fee) AS TotalMemberFee,  AVG(SCompanyFee.news_fee) AS NewsFee,  Sum(SCompanyFee.news_fee) AS TotalNewsFee  " +
            //   " FROM SCompanyFee inner join tbleffective aa on scompanyfee.Effectiveid = '2' " +
            //   " LEFT OUTER JOIN companyPayment ON ( SCompanyFee.CTotalPerMonth = companyPayment.TotalPerMonth )  " +
            //   " LEFT OUTER JOIN companyMember ON ( companyPayment.companyId = companyMember.companyId )  " +
            //   " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
            //   " FROM companyPayment companyPayment_2 " +
            //   " WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
            //   " and aa.effectiveid = '2'  ";

            //if (cboStatus.Value == "A")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'A' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '2' ";
            //}
            //else if (cboStatus.Value == "NA")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'NA' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '2' ";
            //}

            //SQLStatement = SQLStatement + " and TotalPerMonth <> 1200 and cast(companyMember.companyId as int) not in (select cast(companyid as int) from companyCheck) GROUP BY SCompanyFee.CTotalPerMonth  ";




            //SQLStatement = SQLStatement + " union SELECT SCompanyFee.CTotalPerMonth AS TotalPerMonth,  Count(companyPayment.TotalPerMonth) AS NumberOfPersons,  " +
            //        " AVG(SCompanyFee.mem_fee) AS MemberFee,  Sum(SCompanyFee.mem_fee) AS TotalMemberFee,  AVG(SCompanyFee.news_fee) AS NewsFee,  " +
            //        " Sum(SCompanyFee.news_fee) AS TotalNewsFee  " +
            //        " FROM SCompanyFee inner join tbleffective aa on scompanyfee.Effectiveid = '1' " +
            //        " LEFT OUTER JOIN companyPayment ON ( SCompanyFee.CTotalPerMonth = companyPayment.TotalPerMonth )  " +
            //        " LEFT OUTER JOIN companyMember ON ( companyPayment.companyId = companyMember.companyId )  " +
            //        " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
            //        " FROM companyPayment companyPayment_2 " +
            //        " WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
            //        " and aa.effectiveid = '1' ";

            //if (cboStatus.Value == "A")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'A' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '1' ";
            //}
            //else if (cboStatus.Value == "NA")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'NA' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '1' ";
            //}

            //SQLStatement = SQLStatement + " and cast(companyMember.companyId as int) in (select cast(companyid as int) from companyCheck) GROUP BY SCompanyFee.CTotalPerMonth ";

            //SQLStatement = SQLStatement + " union SELECT SCompanyFee.CTotalPerMonth AS TotalPerMonth,  Count(companyPayment.TotalPerMonth) AS NumberOfPersons,  AVG(SCompanyFee.mem_fee) AS MemberFee,   " +
            //        " Sum(SCompanyFee.mem_fee) AS TotalMemberFee,  AVG(SCompanyFee.news_fee) AS NewsFee,  Sum(SCompanyFee.news_fee) AS TotalNewsFee  " +
            //        " FROM SCompanyFee inner join tbleffective aa on scompanyfee.Effectiveid = '2' " +
            //        " LEFT OUTER JOIN companyPayment ON ( SCompanyFee.CTotalPerMonth = companyPayment.TotalPerMonth )  " +
            //        " LEFT OUTER JOIN companyMember ON ( companyPayment.companyId = companyMember.companyId )  " +
            //        " WHERE companyPayment.tranid = ( SELECT MAX(companyPayment_2.tranid) " +
            //        " FROM companyPayment companyPayment_2 " +
            //        " WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
            //        " and aa.effectiveid = '2' ";

            //if (cboStatus.Value == "A")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'A' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '2' ";
            //}
            //else if (cboStatus.Value == "NA")
            //{
            //    SQLStatement = SQLStatement + " AND companyMember.memberStatus = 'NA' ";
            //    SQLStatement = SQLStatement + " AND scompanyfee.Effectiveid = '2' ";
            //}

            //SQLStatement = SQLStatement + " and TotalPerMonth <> 1500 and cast(companyMember.companyId as int) in (select cast(companyid as int) from companyCheck) GROUP BY SCompanyFee.CTotalPerMonth ) as iii ";
            string memberstatus = "";
            if (cboStatus.Value == "A")
            {
                memberstatus =  " companyMember.memberStatus = 'A'  AND ";

            }
            else if (cboStatus.Value == "NA")
            {
                memberstatus =  " companyMember.memberStatus = 'NA'  AND ";

            }
            string SQLStatement = "SET dateformat dmy  " +
                "SELECT totalPerMonth AS TotalPerMonth, " +
                "COUNT(totalPerMonth) as NumberOfPersons,  " +
                "(totalPerMonth * 30) / 100 as MemberFee , " +
                "(totalPerMonth * 70) / 100 as Newsfee, " +
                "totalPerMonth * COUNT(totalPerMonth) as totalfee, " +
                "((totalPerMonth * 30) / 100) * COUNT(totalPerMonth) as TotalMemberFee, " +
                "((totalPerMonth * 70) / 100) * COUNT(totalPerMonth) as TotalNewsFee " +
                "FROM companyMember " +
                "LEFT OUTER JOIN companyPayment ON(companyMember.companyId = companyPayment.companyId) " +
                "LEFT OUTER JOIN companyAccount ON(companyMember.companyId = companyAccount.companyId AND companyPayment.accId = companyAccount.accId) " +
                "WHERE  " + memberstatus + 
                " companyPayment.tranid = (SELECT MAX(companyPayment_2.tranid) FROM companyPayment companyPayment_2 " +
				"WHERE companyPayment.companyId = companyPayment_2.companyId ) " +
                //"WHERE companyPayment.companyId = companyPayment_2.companyId ) AND CompanyPayment.Deleted_at IS NULL " +
                "GROUP BY totalPerMonth " +
                "ORDER BY totalPerMonth ASC";

            

             

           

            SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
            DataSet myDataSet;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            myDataSet = new DataSet();
            dataAdapter.SelectCommand.CommandTimeout = 600;
            dataAdapter.Fill(myDataSet, "companyMember");
            return myDataSet;
        }

        protected void view_Click(object sender, EventArgs e)
        {
            Binddata();
        }
    }
}