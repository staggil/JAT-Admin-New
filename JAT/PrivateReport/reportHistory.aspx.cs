using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Globalization;

namespace JAT.PrivateReport
{
    public partial class reportHistory : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                datepicker1Input.Value = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                datepicker2Input.Value = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));

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

        protected void Print_Click(object sender, EventArgs e)
        {
            var DateFrom = datepicker1Input.Value.ToString();
            var DateTo = datepicker2Input.Value.ToString();
            var payMeth = MemberType.SelectedValue.ToString();
            connection();
            conn.Open();
            if (MemberType.SelectedValue == "Japan")
            {
                string sql = "SET dateformat dmy select  firstmemberid, memberid, nameEng, membertype, companynm as companyNm, appliedDate, expiredDate, paymethod, max(bankcode) as bankAccount, max(accno) as receiptNo, nameE, newsletter, annualfee, payname, max(paymentdate) as paymentdate from ( " +
                    "select *, case when newsletter is null then(select newsletter from tbleffective a inner join smembertype b on a.effectiveid = b.effectiveid where membertype = bb.membertype and EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate >= floor(cast(getdate() as float))) else newsletter end annualFee " +
                    ", (select nameE from privatedetail where memberid = bb.firstmemberid) as payname " +
                    "from( " +
                    "select *, (select newsletter from tblEffective a inner join smembertype b on a.EffectiveID = b.EffectiveID and aa.expiredDate between a.EffectiveDate and a.ExpireDate and b.membertype = aa.membertype  where a.EffectiveID = 2) as newsletter  from( " +
                    "select d.firstmemberId, d.memberId, (d.prefixNm + ' ' + d.nameE) as nameEng, d.memberType, companyNm, d.appliedDate, expireddate, paymentdate, paymethod, pa.bankcode, pa.accno, d.nameE " +
                    "from privateDetail d inner join privateAddress a on d.firstmemberId = a.memberId and addressType = 2 " +
                    "inner join privateDetail dM on d.firstmemberId = dm.memberId " +
                    "left join(select pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate " +
                    "from privatepayment pp inner join (select memberId, payBy, max(expiredate) as expiredDate " +
                    "from privatepayment " +
                    "group by memberId, payBy) as pEx on pp.memberId = pEx.memberId and pp.payBy = pEx.payBy and pp.expiredate = pEx.expireddate) pTran on d.memberId = pTran.payBy " +
                    "left join privatePayAccount ppa on(ppa.tranId = pTran.tranid) " +
                    "left join privateAccount pa on(pa.accId = ppa.accId and pa.memberId = dm.memberId) " +
                    "where d.memberStatus = 'A'  and d.appliedDate between '"+ DateFrom + "' and '"+ DateTo + "' and dm.memberType not in ('3A', '3B')) aa) bb " +
                    ") as iiio " +
                    "group by firstmemberid, memberid, nameEng, membertype, companynm, appliedDate, expiredDate, paymethod, nameE, newsletter, annualfee, payname " +
                    "order by companyNm, payname,membertype, nameE ";

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateHistory.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.CommandTimeout = 600;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("payMethod", payMeth));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            else if (MemberType.SelectedValue == "Thai") 
            {
                string sql = "SET dateformat dmy select  firstmemberid, memberid, nameEng, membertype, companynm as companyNm, appliedDate, expiredDate, paymethod, max(bankcode) as bankcode, max(accno) as accno, nameE, newsletter, annualfee, payname, max(paymentdate) as paymentdate from ( " +
                    "select *, case when newsletter is null then(select newsletter from tbleffective a inner join smembertype b on a.effectiveid = b.effectiveid where membertype = bb.membertype and EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate >= floor(cast(getdate() as float))) else newsletter end annualFee " +
                    ", (select nameE from privatedetail where memberid = bb.firstmemberid) as payname " +
                    "from( " +
                    "select *, (select newsletter from tblEffective a inner join smembertype b on a.EffectiveID = b.EffectiveID and aa.expiredDate between a.EffectiveDate and a.ExpireDate and b.membertype = aa.membertype  where a.EffectiveID = 2) as newsletter  from( " +
                    "select d.firstmemberId, d.memberId, (d.prefixNm + ' ' + d.nameE) as nameEng, d.nameE, d.memberType, companyNm, d.appliedDate, expireddate, paymentdate, paymethod, pa.bankcode, pa.accno " +
                    "from privateDetail d inner join privateAddress a on d.firstmemberId = a.memberId and addressType = 2 " +
                    "inner join privateDetail dM on d.firstmemberId = dm.memberId " +
                    "left join(select pp.tranId, pp.memberId, pp.payBy, pp.paymethod, paymentdate, expiredDate " +
                    "from privatepayment pp inner join (select memberId, payBy, max(expiredate) as expiredDate " +
                    "from privatepayment " +
                    "group by memberId, payBy) as pEx on pp.memberId = pEx.memberId and pp.payBy = pEx.payBy and pp.expiredate = pEx.expireddate) pTran on d.memberId = pTran.payBy " +
                    "left join privatePayAccount ppa on(ppa.tranId = pTran.tranid) " +
                    "left join privateAccount pa on(pa.accId = ppa.accId and pa.memberId = dm.memberId) " +
                    "where d.memberStatus = 'A'  and d.appliedDate between '" + DateFrom + "' and '" + DateTo + "' and dm.memberType  in ('3A', '3B')) aa) bb " +
                    ") as iiio " +
                    "group by firstmemberid, memberid, nameEng, membertype, companynm, appliedDate, expiredDate, paymethod, nameE, newsletter, annualfee, payname " +
                    "order by companyNm, payname,membertype, nameE ";

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateHistory.rdlc");
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromDate", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("toDate", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("payMethod", payMeth));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

        }
    }
}