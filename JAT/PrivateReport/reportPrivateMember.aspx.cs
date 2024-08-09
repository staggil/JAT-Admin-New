using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using Microsoft.Reporting.WebForms;
namespace JAT.PrivateReport
{
    public partial class reportPrivateMember : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

        //private ReportDocument repSource = new ReportDocument();

        //private ReportDocument reportDocument;

        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            chkRadio();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var tmpdatefr = date_fr.Text;
            var tmpdateto = date_to.Text;
            if (RadioButtonList1.SelectedValue == "Honor Member")
            {
                connection();
                conn.Open();


                //string sql = "SELECT t1.nameJ, t1.nameE, (SELECT companyNm FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS companyNm, (SELECT address FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS address, (SELECT address FROM privateAddress WHERE memberid = t1.memberid AND addressType = '1') AS HomeAdd, (SELECT phone FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS PhoneCom, (SELECT CASE WHEN fax = '' THEN '' ELSE 'F/ ' + fax END AS faxCom FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS faxCom, (SELECT CASE WHEN phone = '' THEN '' ELSE 'R. ' + phone END AS PhoneName FROM privateAddress WHERE memberid = t1.memberid AND addressType = '1') AS HomePhone " +
                //            "FROM PrivateDetail t1 " +
                //            "WHERE memberType = '4' " +
                //            "ORDER BY t1.nameE ASC";
                string sql = "select d.firstmemberId, d.memberId, applieddate, nameJ, (nameE) as nameE, " +
                    "ha.address as HomeAdd, ha.phone as HomePhone, ca.companyNm, ca.address as address, " +
                    "ca.phone as PhoneCom, ca.fax as faxCom, checkCompanyNm, checkCompanyAddress, " +
                    "checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, birthPlace " +
                    "from private p " +
                    "left join privateDetail d on  p.memberid = d.firstmemberid " +
                    "left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1 " +
                    "left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2 " +
                    "where memberStatus = 'A' and memberType = '4' " +
                    "order by nameE, nameJ ";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateHorner.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (RadioButtonList1.SelectedValue == "Thai Main Member")
            {
                connection();
                conn.Open();


                string sql = "select d.memberType as prefixNm, " +
                        "d.firstmemberId, d.memberId, d.applieddate, " +
                        "ltrim(d.nameJ) as nameJ, " +
                        "ltrim(d.nameE) as nameEng, " +
                        "ltrim(d2.nameJ) as nameJ2, " +
                        "ltrim(d2.nameE) as nameEng2, " +
                        "ha.address as homeAddress, ha.phone as homePhone, ca.companyNm, ca.address as companyAddress, " +
                        "ca.phone as companyPhone, ca.fax as companyFax, checkCompanyNm, checkCompanyAddress, " +
                        "checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, birthPlace, " +
                        "checkFamilyName " +
                        "from private p " +
                        "left join privateDetail d on  p.memberid = d.firstmemberid " +
                        "left join privateDetail d2 on p.memberid = d2.firstmemberid and d2.membertype = '7' and d2.memberStatus = 'A' " +
                        "left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1 " +
                        "left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2 " +
                        "where d.memberStatus = 'A' and " +
                        "d.memberType in ('3A', '3B') " +
                        //"and p.checkmember != 1  and d.appliedDate between '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                        "and p.checkmember != 1  and convert(varchar, d.appliedDate, 3) between '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                        "order by d.nameE, d.nameJ ";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateTMMember.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (RadioButtonList1.SelectedValue == "Japanese Member List")
            {
                if (chkBox.Checked == true)
                {
                    connection();
                    conn.Open();
                    string sql = "select d.firstmemberId,(SELECT nameJ FROM PrivateDetail WHERE memberid=d.firstmemberid) as fmNameJ, " +
                            "(SELECT nameE FROM PrivateDetail WHERE memberid=d.firstmemberid) as fmNameEng, " +
                            "d.memberId, d.applieddate, d.nameJ, (d.nameE) as nameEng, " +
                            "ha.address as homeAddress, ha.phone as homePhone, ca.companyNm, ca.address as companyAddress, " +
                            "ca.phone as companyPhone, ca.fax as companyFax, checkCompanyNm, checkCompanyAddress, " +
                            "checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, birthPlace, checkMainName, checkBirthPlace, checkFamilyName, checkMember, sendType, " +
                            "d.prefixNm, (select count(*) from privatedetail where firstmemberid = d.firstmemberid) as cntRec " +
                            "from private p " +
                            "left join privateDetail d on  p.memberid = d.firstmemberid " +
                            "left join privateDetail dM on d.firstmemberId = dM.memberId " +
                            "left join privateAddress ha on d.firstmemberId = ha.memberId and ha.addressType = 1 " +
                            "left join privateAddress ca on d.firstmemberId = ca.memberId and ca.addressType = 2 " +
                            "where d.memberStatus = 'A' and d.memberType not in ('3A', '3B') " +
                            "and dM.memberType not in ('3A', '3B') " +
                            //"and d.appliedDate between '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                            "and convert(varchar, d.appliedDate, 3) between '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                            "order by  dM.nameE,d.firstmemberid,d.memberType,(dM.nameE + d.firstmemberid),d.spouse, dM.nameJ, d.prefixnm";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateJMember_chk.rdlc");
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    var jdate = tmpdateto.Split('/')[0].ToString() + "年" + tmpdateto.Split('/')[1].ToString() + "月" + tmpdateto.Split('/')[2].ToString() + "日";
                    //must validate date format
                    reportParameters.Add(new ReportParameter("dateto", jdate));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);

                    conn.Close();
                }
                else
                {
                    connection();
                    conn.Open();
                    string sql = "select ROW_NUMBER() OVER(PARTITION BY d.firstmemberid ORDER BY d.memberid) as boardPosition,d.firstmemberId, d.memberId, d.applieddate, d.nameJ, (d.nameE) as nameEng, d.memberType, d.spouse, " +
                            "case " +
                            "when checkHomeAddress = 1 then '' " +
                            "else ha.address " +
                            "end as homeAddress," +
                            "case " +
                            "when checkHomePhone = 1 then '' " +
                            "else ha.phone " +
                            "end as homePhone,  " +
                            "ca.companyNm, ca.address as companyAddress, " +
                            "ca.phone as companyPhone, ca.fax as companyFax, checkCompanyNm, checkCompanyAddress, " +
                            "checkCompanyPhone, checkCompanyFax as faxCom, checkHomeAddress, checkHomePhone as address, birthPlace, checkMainName, " +
                            "checkBirthPlace, checkFamilyName, checkMember, sendType, " +
                            "d.prefixNm, (select count(*) from privatedetail where firstmemberid = d.firstmemberid) as RowNum " +
                            "from private p " +
                            "left join privateDetail d on  p.memberid = d.firstmemberid " +
                            "left join privateDetail dM on d.firstmemberId = dM.memberId " +
                            "left join privateAddress ha on d.firstmemberId = ha.memberId and ha.addressType = 1 " +
                            "left join privateAddress ca on d.firstmemberId = ca.memberId and ca.addressType = 2 " +
                            "where d.memberStatus = 'A' and d.memberType not in ('3A', '3B') " +
                            "and dM.memberType not in ('3A', '3B') " +
                            "and p.checkmember != 1 " +
                            //"and d.appliedDate between '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                            "and convert(varchar, d.appliedDate, 3) between '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                            "and ((select count(*) from privatedetail where firstmemberid = d.firstmemberid) < 3 or d.memberid = d.firstmemberid) " +
                            "order by  dM.nameE,d.firstmemberid,d.memberType,(dM.nameE + d.firstmemberid),d.spouse, dM.nameJ, d.prefixnm ";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateJMember.rdlc");
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);

                    conn.Close();
                }
            }
            else if (RadioButtonList1.SelectedValue == "Company List")
            {
                if (chkBox.Checked == true)
                {
                    connection();
                    conn.Open();


                    string sql = "SELECT companyMember.companyNmJ, companyMember.companyNmE, companyMember.busType, " +
                            "companyMember.memberStatus, companyMember.address, companyMember.phone, " +
                            "companyMember.fax, " +
                            "case when companyMember.represNmE = '' " +
                            "then companyMember.represNm " +
                            "else companyMember.represNmE end as represNm, " +
                            "companyMember.represPosition, companyMember.represNm as represNmJapan " +
                            "FROM companyMember " +
                            "WHERE companyMember.memberStatus = 'A' " +
                            "AND companyMember.companyNmE <> 'Deleted' " +
                            //"AND appliedDate BETWEEN '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                            "AND convert(varchar, appliedDate, 3) BETWEEN '" + tmpdatefr + "' and '" + tmpdateto + "' " +
                            "ORDER BY companyMember.companyNmE ASC";


                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateCompanyList_chk.rdlc");
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    //var jdate = tmpdateto.Split('/')[2].ToString() + "年" + tmpdateto.Split('/')[1].ToString() + "月" + tmpdateto.Split('/')[0].ToString() + "日";
                    //must validate date format
                    //reportParameters.Add(new ReportParameter("dateto", jdate));
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);

                    conn.Close();
                }
                else
                {
                    connection();
                    conn.Open();

                    string sql = $"select companyId, case when left(companyNmJ,7) = 'Default' then '' else companyNmJ end as companyNmJ, " +
                                 $"companyNmE, represNm, represPosition, email, fax, phone, busType, address from companyMember " +
                                 $"where memberStatus = 'A' and appliedDate BETWEEN convert(datetime, '{tmpdatefr}', 103) " +
                                 $"and convert(datetime, '{tmpdateto}',103) order by companyNmE";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateCompanyList.rdlc");
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);

                    conn.Close();
                }
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkRadio();
        }
        protected void chkRadio()
        {
            if (RadioButtonList1.SelectedValue == "Honor Member")
            {
                inputdiv1.Visible = false;
                inputdiv2.Visible = false;
            }
            else if (RadioButtonList1.SelectedValue == "Thai Main Member")
            {
                inputdiv1.Visible = false;
                inputdiv2.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "Japanese Member List")
            {
                inputdiv1.Visible = true;
                chkBoxLabel.Text = "Member Check";
                inputdiv2.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "Company List")
            {
                inputdiv1.Visible = true;
                chkBoxLabel.Text = "Company Check";
                inputdiv2.Visible = true;
            }
            else
            {
                inputdiv1.Visible = false;
                inputdiv2.Visible = false;
            }
        }
    }
}