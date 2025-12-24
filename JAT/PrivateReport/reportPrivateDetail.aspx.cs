//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Configuration;
//using System.Data;
//using System.Drawing;
//using System.Web;
//using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
//using System.Data.SqlClient;
//using System.Web.Configuration;
//using System.Globalization;
//using System.Threading;
//using System.Diagnostics;
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

using Microsoft.Reporting.WebForms;
//using Microsoft.ApplicationBlocks.Data;
using JAT.Core;


namespace JAT.PrivateReport
{
    public partial class reportPrivateDetail : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();

		//private ReportDocument repSource = new ReportDocument();

		//private ReportDocument reportDocument;

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

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                Box1.Value = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                Box2.Value = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			var DateFrom = Box1.Value.ToString();
            var DateTo = Box2.Value.ToString();
            var displayCho1 = RadioButtonList1.SelectedValue.ToString();
            if (displayCho1 == "New Member")
            {
                connection();
                conn.Open();

                //string sql = "SELECT t1.memberid, t1.nameJ, t1.nameE, t2.birthPlace, t3.address, t3.phone AS PhoneCom,  (SELECT CASE WHEN fax = '' THEN '' ELSE 'F/ ' + fax END AS faxCom FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS faxCom, t3.companyNm, (SELECT address FROM privateAddress WHERE memberid = t1.memberid AND addressType = '1') AS HomeAdd, (SELECT CASE WHEN phone = '' THEN '' ELSE 'R. ' + phone END AS PhoneName FROM privateAddress WHERE memberid = t1.memberid AND addressType = '1') AS HomePhone " +
                //             "FROM PrivateDetail t1 " +
                //             "INNER JOIN Private t2 ON t1.memberid  = t2.memberid INNER JOIN privateAddress t3 ON t1.memberid = t3.memberid WHERE t1.appliedDate >= '" + DateFrom + "'" + "AND t1.appliedDate <= '" + DateTo + "'" + "AND addressType = '2'" +
                //             "ORDER BY t1.nameE ASC";
                string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, d.applieddate, d.nameJ, (d.nameE) as nameE, " +
                    "ha.address as HomeAdd, ha.phone as homePhone, ca.companyNm, ca.address as address, " +
                    "ca.phone as PhoneCom, ca.fax as faxCom, checkCompanyNm, checkCompanyAddress, " +
                    "checkCompanyPhone, checkCompanyFax, checkHomeAddress, checkHomePhone, case when d.memberid = d.firstmemberid then birthPlace else '' end as birthPlace, checkMainName, checkBirthPlace, checkFamilyName, checkMember, sendType " +
                    ", d.prefixNm, (select count(*) from privatedetail where firstmemberid = d.firstmemberid) as cntRec " +
                    "from private p " +
                    "left join privateDetail d on  p.memberid = d.firstmemberid " +
                    "left join privateDetail dM on d.firstmemberId = dM.memberId " +
                    "left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1 " +
                    "left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2 " +
                    "where d.memberStatus = 'A' and d.memberType not in ('3A', '3B') " +
                    "and dM.memberType not in ('3A', '3B') " +
                    "and d.appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "order by  dM.nameE, d.firstmemberid, d.memberType, (dM.nameE + d.firstmemberid),d.spouse, dM.nameJ, d.prefixnm ";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.CommandTimeout = 600;
                //adapter.Fill(ds, "NewMember");
                DataTable dt = new DataTable();                
                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateList.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();

            }
            else if (displayCho1 == "Cancel Member")
            {
                connection();
                conn.Open();

                //string sql = "SELECT t1.memberid, t1.nameJ, CONCAT(t1.prefixNm, ' ' + t1.nameE) AS fullNameE " +
                //             "FROM PrivateDetail t1 " +
                //             "WHERE CONVERT(DATE,t1.cancelledDate) >= '" + DateFrom + "'" + "AND CONVERT(DATE,t1.cancelledDate) <= '" + DateTo + "'" +
                //             "ORDER BY t1.nameE ASC";
                string sql = "SET dateformat dmy select d.firstmemberId, d.memberId, nameJ, (prefixNm + ' ' + nameE) as fullNameE, CONVERT(DATE, cancelledDate) as processDate, memberType " +
                    "from privateDetail d " +
                    "where memberStatus = 'NA' and CONVERT(DATE, cancelledDate) between '" + DateFrom + "' and '" + DateTo + "'" +
                    "order by nameE, nameJ ";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.SelectCommand.CommandTimeout = 600;
                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateCancel.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (displayCho1 == "Honor Member")
            {
                connection();
                conn.Open();

                //string sql = "SELECT t1.nameJ, t1.nameE, (SELECT companyNm FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS companyNm, (SELECT address FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS address, (SELECT address FROM privateAddress WHERE memberid = t1.memberid AND addressType = '1') AS HomeAdd, (SELECT phone FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS PhoneCom, (SELECT CASE WHEN fax = '' THEN '' ELSE 'F/ ' + fax END AS faxCom FROM privateAddress WHERE memberid = t1.memberid AND addressType = '2') AS faxCom, (SELECT CASE WHEN phone = '' THEN '' ELSE 'R. ' + phone END AS PhoneName FROM privateAddress WHERE memberid = t1.memberid AND addressType = '1') AS HomePhone " +
                //             "FROM PrivateDetail t1 " +
                //             "WHERE memberType = '4' and memberStatus = 'A' " +
                //             "ORDER BY t1.nameE ASC";
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
                adapter.SelectCommand.CommandTimeout = 600;
                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateHorner.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (displayCho1 == "Family Member")
            {
                connection();
                conn.Open();
                //string sql = "select firstmemberId, memberId, nameJ, (prefixNm + ' '+ nameE) as nameEng, appliedDate as processDate, memberType " +
                //             "from privateDetail " +
                //             "where firstmemberId <> memberId and memberStatus = 'A' and applieddate between '" + DateFrom + "' and '" + DateTo + "' " +
                //             "order by nameE, nameJ ";
                string sql = "SET dateformat dmy BEGIN " +
                    "DROP TABLE IF EXISTS #temp4; " +
                    "CREATE TABLE #temp4 " +
                    "( " +
                    "RowNum int NOT NULL, " +
                    "nameJ nvarchar(100) NOT NULL, " +
                    ") " +
                    "INSERT INTO #temp4 " +
                    "select ROW_NUMBER() OVER(ORDER BY memberStatus) AS RowNum, nameJ " +
                    "from privateDetail " +
                    "where firstmemberId <> memberId and memberStatus = 'A' and applieddate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "order by nameE, nameJ " +
                    "END ";

                try
                {
					SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
					DataTable dt = new DataTable();
					adapter.SelectCommand.CommandTimeout = 600;
					adapter.Fill(dt);
                    string activityDetail = $"Created, Insert data and drop table #temp4 successful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch
                {
					string activityDetail = $"Created, Insert data and drop table #temp4 unsuccessful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
                

                string sql2 = "select RowNum % 7 as RowNum,nameJ " +
                    "from #temp4 ";

                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateMag.rdlc");
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn);
                DataTable dt2 = new DataTable();
                adapter2.Fill(dt2);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt2);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateFamily.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();

                //SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                //DataTable dt = new DataTable();

                //adapter.Fill(dt);

                //ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateFamily.rdlc");
                //ReportViewer1.LocalReport.DataSources.Clear();
                //ReportViewer1.LocalReport.DataSources.Add(rds);

                //conn.Close();
            }
            else if (displayCho1 == "Change Address")
            {
                connection();
                conn.Open();

                string sql = "SET dateformat dmy select d.memberId, nameJ, (prefixNm + ' ' + nameE) as nameE, ha.address as HomeAdd, ha.phone as homePhone, ca.companyNm, ca.address as address, ca.phone as companyPhone, ca.fax as companyFax " +
                    "from private p " +
                    "inner join privateDetail d on p.memberid = d.memberid " +
                    "left join privateAddress ha on p.memberId = ha.memberId and ha.addressType = 1 " +
                    "left join privateAddress ca on p.memberId = ca.memberId and ca.addressType = 2 " +
                    "where memberStatus = 'A' and changeAddressDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "order by d.nameE, d.nameJ ";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.SelectCommand.CommandTimeout = 600;
                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateListChangeAddress.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (displayCho1 == "Election Label")
            {
                connection();
                conn.Open();

                //string sql = "BEGIN " +
                //    "DROP TABLE IF EXISTS #temp5; " +
                //    "CREATE TABLE #temp5 " +
                //    "( " +
                //    "RowNum int NOT NULL, " +
                //    "firstmemberId nvarchar(10) NOT NULL, " +
                //    "memberId nvarchar(10) NOT NULL, " +
                //    "nameJ nvarchar(100) NOT NULL, " +
                //    "nameE nvarchar(100) NOT NULL, " +
                //    "memberType nvarchar(10) NOT NULL " +
                //    ") " +
                //    "INSERT INTO #temp5 " +
                //    "select ROW_NUMBER() OVER(ORDER BY nameE, nameJ, memberStatus) AS RowNum, firstmemberId, memberId, nameJ, (prefixNm + ' ' + nameE) as nameE ,memberType " +
                //    "from privateDetail d " +
                //    "where memberStatus = 'A' and memberType not in('3B','3A') and appliedDate between '" + DateFrom + "' and '" + DateTo + "'" +
                //    "order by nameE, nameJ " +
                //    "END ";
                string sql = "SET dateformat dmy BEGIN " +
                    "DROP TABLE IF EXISTS #temp5; " +
                    "CREATE TABLE #temp5 (RowNum int,GroupType int,firstmemberId nvarchar(10) NOT NULL, memberId nvarchar(10) NOT NULL, nameJ nvarchar(100) NOT NULL, nameE nvarchar(100) NOT NULL, memberType nvarchar(10) NOT NULL ) " +
                    "INSERT INTO #temp5 select ROW_NUMBER() OVER(ORDER BY nameE, nameJ) AS RowNum,GroupType=1,firstmemberId, memberId, nameJ, (prefixNm + ' ' + nameE) as nameE ,memberType " +
                    "from privateDetail d where memberStatus = 'A' " +
                    "and memberType not in('3B', '3A') and memberid = firstmemberid " +
                    "and appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "INSERT INTO #temp5 select ROW_NUMBER() OVER(ORDER BY nameE, nameJ) AS RowNum,GroupType=2,firstmemberId, memberId, nameJ, (prefixNm + ' ' + nameE) as nameE ,memberType " +
                    "from privateDetail d where memberStatus = 'A' " +
                    "and memberType not in('3B', '3A') and memberid <> firstmemberid " +
                    "and appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "END ";

                try
                {
					SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
					DataTable dt = new DataTable();
					adapter.SelectCommand.CommandTimeout = 600;
					adapter.Fill(dt);
					string activityDetail = $"Created, Insert data and drop table #temp5 successful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch
				{
					string activityDetail = $"Created, Insert data and drop table #temp5 unsuccessful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}


				//string sql2 = "select case " +
				//    "when firstmemberid<> memberid then '1' " +
				//    "else '2' " +
				//    "end as GroupType,RowNum % 5 as RowNum,firstmemberId,memberId,nameJ,nameE ,memberType " +
				//    "from #temp5 ";

				string sql2 = "select GroupType,RowNum % 5 as RowNum," +
                    "firstmemberId,memberId,nameJ,nameE ,memberType " +
                    "from #temp5 ";

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateElection.rdlc");
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn);
                DataTable dt2 = new DataTable();
                adapter2.Fill(dt2);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt2);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (displayCho1 == "List Receiver")
            {
                connection();
                conn.Open();

                string sql = "SET dateformat dmy select ooo.memberId, ooo.nameEng, fee, payMethod, expiredDate, payDuration, companyNm, fullNameE from ( " +
                    "select memberId, max(expiredDate) as expireCheck, nameEng, nameE from( " +
                    "select d.memberId, (d.prefixNm +' ' + d.nameE) as nameEng, f.fee, payMethod, max(expireDate) as expiredDate, payDuration, a.companyNm, (dF.prefixNm + ' ' + dF.nameE) as fullNameE, d.nameE " +
                    "from privateDetail d " +
                    "left join privatePayment p on d.memberId = p.memberId " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateDetail dF on d.memberId = dF.firstmemberId and dF.firstmemberId<> dF.memberId and dF.memberType = '7' and dF.memberStatus = 'A' " +
                    "left join(select firstmemberid, sum(newsletter) as fee " +
                    "from privatedetail d inner join smemberType sm on (d.membertype = sm.memberType and sm.EffectiveID = '1') and d.memberStatus = 'A' " +
                    "group by firstmemberid) f on d.memberId = f.firstmemberId " +
                    "where d.firstmemberId = d.memberId and d.applieddate between '" + DateFrom + "' and '" + DateTo + "' and d.memberstatus = 'A'  and d.memberType <> '3B' " +
                    "group by d.memberId, d.prefixNm, d.nameE, f.fee, payMethod, payDuration, a.companyNm, dF.prefixNm, dF.nameE) as ooo group by memberId, nameEng, nameE ) as ooo left join( " +
                    "select d.memberId, (d.prefixNm +' ' + d.nameE) as nameEng, f.fee, payMethod, max(expireDate) as expiredDate, payDuration, a.companyNm, (dF.prefixNm + ' ' + dF.nameE) as fullNameE, d.nameE " +
                    "from privateDetail d " +
                    "left join privatePayment p on d.memberId = p.memberId " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateDetail dF on d.memberId = dF.firstmemberId and dF.firstmemberId<> dF.memberId and dF.memberType = '7' and dF.memberStatus = 'A' " +
                    "left join(select firstmemberid, sum(newsletter) as fee " +
                    "from privatedetail d inner join smemberType sm on (d.membertype = sm.memberType and sm.EffectiveID = '1') and d.memberStatus = 'A'  and d.memberType <> '3B' " +
                    "group by firstmemberid) f on d.memberId = f.firstmemberId " +
                    "where d.firstmemberId = d.memberId and d.applieddate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "group by d.memberId, d.prefixNm, d.nameE, f.fee, payMethod, payDuration, a.companyNm, dF.prefixNm, dF.nameE) as ppp on ooo.memberId = ppp.memberId and ooo.expireCheck = ppp.expiredDate " +
                    "order by companyNm, ooo.nameE ";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.SelectCommand.CommandTimeout = 600;
                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateListReceiver.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (displayCho1 == "KrungThep Magazine")
            {
                connection();
                conn.Open();

                string sql = "SET dateformat dmy BEGIN DROP TABLE IF EXISTS #temp3; CREATE TABLE #temp3 (Fee int,RowNum int,nameJ nvarchar(100), nameEng nvarchar(100), companyNm nvarchar(150), nameE nvarchar(50)) " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 1, ROW_NUMBER() OVER(ORDER BY d.nameE, d.nameJ)%5 AS RowNum, d.nameJ, (d.prefixNm + ' ' + d.nameE) as nameEng, a.companyNm, d.nameE " +
                    "from privateDetail d " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateRefer r on d.memberId = r.memberId " +
                    "left join privateDetail dR on r.refermemberId = dR.memberId " +
                    "where d.memberstatus = 'A' and companyNm = '' and d.appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "order by nameE, nameJ " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 2 ,ROW_NUMBER() OVER(ORDER BY d.nameE, d.nameJ) AS RowNum, d.nameJ, (d.prefixNm + ' ' + d.nameE) as nameEng, a.companyNm, d.nameE " +
                    "from privateDetail d " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateRefer r on d.memberId = r.memberId " +
                    "left join privateDetail dR on r.refermemberId = dR.memberId " +
                    "where d.memberStatus = 'A' and d.memberid = d.firstmemberid and(d.appliedDate between '" + DateFrom + "' and '" + DateTo + "') and companyNm <> '' " +
                    "order by nameE, nameJ " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 3 ,ROW_NUMBER() OVER(ORDER BY memberStatus) AS RowNum, nameJ, (prefixNm + ' ' + nameE) as nameEng, isnull('', '') as companyNm, nameE " +
                    "from privateDetail " +
                    "where memberStatus = 'NA' and CONVERT(DATE, cancelledDate) between '" + DateFrom + "' and '" + DateTo + "'" +
                    "INSERT INTO #temp3 " +
                    "select Fee = 11, COUNT(*) as RowNum, isnull('', '') as nameJ, isnull('', '') as nameEng, isnull('', '') as companyNm, isnull('', '') as nameE " +
                    "from privateDetail " +
                    "where memberStatus = 'NA' and CONVERT(DATE, cancelledDate) between '" + DateFrom + "' and '" + DateTo + "'" +
                    "INSERT INTO #temp3 " +
                    "select Fee = 4, COUNT(*) as RowNum, isnull('', '') as nameJ, isnull('', '') as nameEng, isnull('', '') as companyNm, isnull('', '') as nameE " +
                    "from privateDetail " +
                    "where memberStatus = 'A' " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 5, COUNT(*) as RowNum, isnull('', '') as nameJ, isnull('', '') as nameEng, isnull('', '') as companyNm, isnull('', '') as nameE " +
                    "from privateDetail " +
                    "where memberType in ('3', '3A', '3B') and memberStatus = 'A' " +
                    "INSERT INTO #temp3 " +
                    //"select Fee = 6, ROW_NUMBER() OVER(ORDER BY memberStatus)% 7 AS RowNum, nameJ, isnull('', '') as nameEng, isnull('', '') as companyNm, isnull('', '') as nameE " +
                    //"from privateDetail " +
                    //"where memberid <> firstmemberid and appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "select Fee = 6, ROW_NUMBER() OVER(ORDER BY d.nameE, d.nameJ)% 7 AS RowNum, d.nameJ, isnull('', '') as nameEng, isnull('', '') as companyNm, isnull('', '') as nameE " +
                    "from privateDetail d " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateRefer r on d.memberId = r.memberId " +
                    "left join privateDetail dR on r.refermemberId = dR.memberId " +
                    "where d.firstmemberid <> d.memberid and d.memberStatus = 'A' and(CONVERT(DATE, d.appliedDate) between '" + DateFrom + "' and '" + DateTo + "' or CONVERT(DATE, d.cancelledDate) between '" + DateFrom + "' and '" + DateTo + "') " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 7, ROW_NUMBER() OVER(ORDER BY d.nameE, d.nameJ)%5 AS RowNum, d.nameJ, (d.prefixNm + ' ' + d.nameE) as nameEng, a.companyNm, d.nameE " +
                    "from privateDetail d " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateRefer r on d.memberId = r.memberId " +
                    "left join privateDetail dR on r.refermemberId = dR.memberId " +
                    "where d.memberstatus = 'A' and d.appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 8, ROW_NUMBER() OVER(ORDER BY d.nameE, d.nameJ)%5 AS RowNum, d.nameJ, (d.prefixNm + ' ' + d.nameE) as nameEng, a.companyNm, d.nameE " +
                    "from privateDetail d " +
                    "left join privateAddress a on d.memberId = a.memberId and addressType = 2 " +
                    "left join privateRefer r on d.memberId = r.memberId " +
                    "left join privateDetail dR on r.refermemberId = dR.memberId " +
                    "where d.memberid = d.firstmemberid and d.memberstatus = 'A' and companyNm = '' and d.appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "order by nameE, nameJ " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 9 ,ROW_NUMBER() OVER(ORDER BY nameE, nameJ)%6 AS RowNum, nameJ, (prefixNm + ' ' + nameE) as nameEng, isnull('', '') as companyNm, nameE " +
                    "from privateDetail " +
                    "where memberStatus = 'NA' and memberid = firstmemberid and CONVERT(DATE, cancelledDate) between '" + DateFrom + "' and '" + DateTo + "'" +
                    "order by nameE, nameJ " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 10 ,ROW_NUMBER() OVER(ORDER BY nameE, nameJ)%6 AS RowNum, nameJ, (prefixNm + ' ' + nameE) as nameEng, isnull('', '') as companyNm, nameE " +
                    "from privateDetail " +
                    "where memberStatus = 'NA' and memberid <> firstmemberid and CONVERT(DATE, cancelledDate) between '" + DateFrom + "' and '" + DateTo + "'" +
                    "order by nameE, nameJ " +
                    "INSERT INTO #temp3 " +
                    "select Fee = 12, COUNT(*) as RowNum, isnull('', '') as nameJ, isnull('', '') as nameEng, isnull('', '') as companyNm, isnull('', '') as nameE  from privateDetail d left join privateAddress a on d.memberId = a.memberId and addressType = 2 left join privateRefer r on d.memberId = r.memberId left join privateDetail dR on r.refermemberId = dR.memberId " +
                    "where d.memberstatus = 'A' and d.appliedDate between '" + DateFrom + "' and '" + DateTo + "' " +
                    "END ";

                try
                {
					SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
					DataTable dt = new DataTable();
					adapter.SelectCommand.CommandTimeout = 600;
					adapter.Fill(dt);
					string activityDetail = $"Created, Insert data and drop table #temp3 successful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch
				{
					string activityDetail = $"Created, Insert data and drop table #temp3 unsuccessful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}



				string sql2 = "select Fee,RowNum,nameJ,companyNm " +
                              "from #temp3 ";


                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateMag.rdlc");
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn);
                DataTable dt2 = new DataTable();
                adapter2.SelectCommand.CommandTimeout = 600;
                adapter2.Fill(dt2);
                ReportDataSource rds = new ReportDataSource("DataSet1", dt2);
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("DateFrom", DateFrom));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);
                reportParameters.Add(new ReportParameter("DateTo", DateTo));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else if (displayCho1 == "Withdrawal=>Re-enrollment")
            {
                connection();
                conn.Open();
                //DateFrom += " 00:00:00";
                //DateTo += " 23:59:59";
                string sql = "SET dateformat dmy SELECT firstmemberid,memberid,prefixNm,nameJ,nameE," +
                        "date_do_status_na_to_a AS 'appliedDate',memberType " +
                        //"FROM PrivateDetail WHERE date_do_status_na_to_a IS NOT NULL " +
                        //"AND date_do_status_na_to_a BETWEEN '" + DateFrom + "' AND '" + DateTo + "'" +
                        "FROM PrivateDetail WHERE date_do_status_na_to_a BETWEEN '" + DateFrom + " 00:00:00' AND '" + DateTo + " 23:59:59'" +
                        "AND date_do_status_na_to_a IS NOT NULL " +
                        "ORDER BY date_do_status_na_to_a";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.SelectCommand.CommandTimeout = 600;
                adapter.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateReenrollment.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);

                conn.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert()", true);

            }




        }

    }
}