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
    public partial class reportPayment : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

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
            connection();
            conn.Open();
            var DateFrom = datepicker1Input.Value.ToString();
            var DateTo = datepicker2Input.Value.ToString();
            var payMeth = payMethod.Text.ToString();
            if (PayAt.SelectedValue == "1")
            {
                if (payMethod.SelectedValue == "C")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "where pp.paymethod in ('J', 'K', 'P') and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "A")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate as expireDate, " +
                             "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                             "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                             "inner join privatePayment pp on d.memberId = pp.payBy " +
                             "where pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') " +
                             "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "T")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "left join privatePayShort ps on pp.tranId = ps.tranId " +
                                 //"where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') and ps.shortfrom is null " +
                                 "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') and pp.checkShort = '0' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "TS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                                 //"where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') " +
                                 "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') and pp.checkShort = '1' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "S")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "left join privatePayShort ps on pp.tranId = ps.tranId " +
                                 //"where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') and ps.shortfrom is null " +
                                 "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') and pp.checkShort = '0' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "SS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                        //"where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') " +
                        "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat not in ('Rec.', 'Annex', 'Transfer') and pp.checkShort = '1' " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }

            }
            else if (PayAt.SelectedValue == "2")
            {
                if (payMethod.SelectedValue == "C")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymethod in ('J', 'K', 'P') and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Annex' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "A")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "where pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Annex' " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "T")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "left join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Annex' and ps.shortfrom is null " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "TS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Annex' " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "S")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "left join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Annex' and ps.shortfrom is null " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "SS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Annex' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
            }
            else if (PayAt.SelectedValue == "3")
            {
                if (payMethod.SelectedValue == "C")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymethod in ('J', 'K', 'P') and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Rec.' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "A")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Rec.' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "T")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "left join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Rec.' and ps.shortfrom is null " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "TS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Rec.' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "S")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "left join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Rec.' and ps.shortfrom is null " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "SS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Rec.' " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
            }
            else if (PayAt.SelectedValue == "4")
            {
                if (payMethod.SelectedValue == "C")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymethod in ('J', 'K', 'P') and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Transfer' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "A")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Transfer' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "T")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "left join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Transfer' and ps.shortfrom is null " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "TS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Transfer' " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "S")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "left join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Transfer' and ps.shortfrom is null " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "SS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Transfer' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
            }
            else if (PayAt.SelectedValue == "5") //******credit
            {
                if (payMethod.SelectedValue == "C")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymethod in ('J', 'K', 'P') and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Credit card' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "A")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "where pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Credit card' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "T")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "left join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Credit' and ps.shortfrom is null " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "TS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'T' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Credit card' " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "S")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                        "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                        "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                        "inner join privatePayment pp on d.memberId = pp.payBy " +
                        "left join privatePayShort ps on pp.tranId = ps.tranId " +
                        "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Credit card' and ps.shortfrom is null " +
                        "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                else if (payMethod.SelectedValue == "SS")
                {
                    string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, (prefixNm + ' ' + nameE) as nameEng, companyNm, pp.paymentdate, pp.expiredate, " +
                                 "pp.entranceFee, pp.newsletterfee, pp.receiptNo, pp.payremark, pp.paymethod " +
                                 "from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                                 "inner join privatePayment pp on d.memberId = pp.payBy " +
                                 "inner join privatePayShort ps on pp.tranId = ps.tranId " +
                                 "where pp.paymethod = 'S' and pp.paymentDate between '" + DateFrom + "' and '" + DateTo + "' and pp.payat = 'Credit Card' " +
                                 "order by pp.paymentDate, d.nameE ";

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivatepayment.rdlc");
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("dateFrom", DateFrom));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("dateTo", DateTo));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                    reportParameters.Add(new ReportParameter("paymethod", payMeth));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
            }
        }
    }
}