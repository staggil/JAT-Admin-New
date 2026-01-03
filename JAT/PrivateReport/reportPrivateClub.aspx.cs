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
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WebForms;
using JAT.Core;

namespace JAT.PrivateReport
{
    public partial class reportPrivateClub : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();

		private SqlConnection conn;
        private SqlCommand cmd;

        //private ReportDocument repSource = new ReportDocument();

        //private ReportDocument reportDocument;


        /*
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
            // เพิ่มเวลา timeout (หน่วย: วินาที)
            cmd.CommandTimeout = 180; // เช่น 120 วินาที (2 นาที)


            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        */


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                DataTable td;
                td = SelectSqlTable("SELECT * FROM privateClubDetail");
                */

                var repo = new PrivateClubReportRepository();

                DataTable td = repo.SelectSqlTable("SELECT * FROM privateClubDetail");


                foreach (DataRow tmprow in td.Rows)
                {
                    switch (tmprow["itemNm"].ToString().Trim())
                    {
                        //case "ev_tmp1": RadioButtonList1.Items[4].Text = tmprow["itemVal"].ToString(); break;
                        //case "ev_tmp2": RadioButtonList1.Items[5].Text = tmprow["itemVal"].ToString(); break;
                        //case "ev_tmp3": RadioButtonList1.Items[6].Text = tmprow["itemVal"].ToString(); break;
                        //case "sub_tmp1": RadioButtonList2.Items[8].Text = tmprow["itemVal"].ToString(); break;
                        //case "sub_tmp2": RadioButtonList2.Items[9].Text = tmprow["itemVal"].ToString(); break;
                    }
                }
                initLabel();
            }
        }



        protected void initLabel()
        {
            /*
            if (RadioButtonList1.Items[4].Text.Trim() == "")
            {
                RadioButtonList1.Items[4].Enabled = false;
                RadioButtonList1.Items[4].Selected = false;
            }
            if (RadioButtonList1.Items[5].Text.Trim() == "")
            {
                RadioButtonList1.Items[5].Enabled = false;
                RadioButtonList1.Items[5].Selected = false;
            }
            if (RadioButtonList1.Items[6].Text.Trim() == "")
            {
                RadioButtonList1.Items[6].Enabled = false;
                RadioButtonList1.Items[6].Selected = false;
            }

           
            if (RadioButtonList2.Items[8].Text.Trim() == "")
            {
                RadioButtonList2.Items[8].Enabled = false;
                RadioButtonList2.Items[8].Selected = false;
            }
            if (RadioButtonList2.Items[9].Text.Trim() == "")
            {
                RadioButtonList2.Items[9].Enabled = false;
                RadioButtonList2.Items[9].Selected = false;
            }
            */


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue != "")
            {
                var selVal = RadioButtonList1.SelectedValue;
                bindReportData(selVal);
            }
            else if (RadioButtonList2.SelectedValue != "")
            {
                var selVal = RadioButtonList2.SelectedValue;
                bindReportData(selVal);
            }
            else if (RadioButtonList3.SelectedValue != "")
            {
                var selVal = RadioButtonList3.SelectedValue;
                bindReportData(selVal);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select club')", true);
            }
        }

        protected void bindReportData(string clubCho)
        {
            //string startAge = DropDownList1.SelectedValue;
            //var clubCho = RadioButtonList1.SelectedValue;
            if (clubCho == "golf")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId as memberid, 
                ltrim(nameJ) as nameJ, 
                (prefixNm + ' ' + nameE) as nameEng, 
                ltrim(ha.phone) as homePhone, 
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm, 
                ltrim(ca.fax) as companyFax, 
                ltrim(ca.phone) as companyPhone, 
                d.memberType, 
                convert(nvarchar(12), expireDate, 111) as expireDate, 
                'Golf' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate 
                from privatePayment 
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where c.golf = 1 and d.memberStatus = 'A'
                order by nameE";

                // ใช้ repository แทน connection ตรง ๆ
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Golf"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "children_library")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId as memberid, 
                ltrim(nameJ) as nameJ, 
                (prefixNm + ' ' + nameE) as nameEng, 
                ltrim(ha.phone) as homePhone, 
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm, 
                ltrim(ca.fax) as companyFax, 
                ltrim(ca.phone) as companyPhone, 
                d.memberType, 
                convert(nvarchar(12), expireDate, 111) as expireDate, 
                'Children' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate 
                from privatePayment 
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where c.children = 1 and d.memberStatus = 'A'
                order by nameE";

                // ใช้ repository แทน connection ตรง ๆ
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Children"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "sukusuku")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                // SQL query สำหรับ Sukusuku
                string sql = @"
                select d.memberId as memberid, 
                ltrim(nameJ) as nameJ, 
                (prefixNm + ' ' + nameE) as nameEng, 
                ltrim(ha.phone) as homePhone, 
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm, 
                ltrim(ca.fax) as companyFax, 
                ltrim(ca.phone) as companyPhone, 
                d.memberType, 
                convert(nvarchar(12), expireDate, 111) as expireDate, 
                'Sukusuku' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate 
                from privatePayment 
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where c.zukuzuku = 1 and d.memberStatus = 'A'
                order by nameE";

                // ดึงข้อมูลจาก repository
                DataTable dt = repo.SelectSqlTable(sql);

                // ใส่ข้อมูลให้ ReportViewer
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Sukusuku"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "board_list")
            {
                var uid = Session["UID"];
                int staffID = uid != null ? Convert.ToInt32(uid) : 0;

                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                // SQL command เรียก stored procedure
                string sql = "exec psPrivateReportClub @reportType=7";

                try
                {
                    // ใช้ repository ดึงข้อมูล
                    DataTable dt = repo.SelectSqlTable(sql);

                    // log ว่าทำสำเร็จ
                    string activityDetail = $"Executed procedure name 'psPrivateReportClub' successful (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);

                    // ใส่ข้อมูลให้ ReportViewer
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateBoardList.rdlc");

                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("showClub", "役員名簿 "));
                    ReportViewer1.LocalReport.SetParameters(reportParameters);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                catch (SqlException ex)
                {
                    string activityDetail = $"Executed procedure name 'psPrivateReportClub' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                catch (Exception ex)
                {
                    string activityDetail = $"Executed procedure name 'psPrivateReportClub' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
            }

            else if (clubCho == "board")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId, 
                nameJ, 
                nameE, 
                boardPosition, 
                p.email as address, 
                ha.phone as HomePhone, 
                ha.mobile as MobilePhone, 
                ca.companyNm, 
                ca.phone as PhoneCom, 
                ca.fax as faxCom, 
                '' as sortLady
                from private p
                inner join privateBoard b on p.memberId = b.memberId
                inner join privateDetail d on b.memberId = d.memberId
                left join privateAddress ha on b.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on b.memberId = ca.memberId and ca.addressType = 2
                where memberStatus = 'A' and sortBoard is not null and sortBoard > 0
                order by sortBoard
                ";

                // ใช้ repository แทน connection
                DataTable dt = repo.SelectSqlTable(sql);

                // สร้าง ReportDataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateBoard.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "理事・監事・オブザーバー名簿"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "lady")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
        select d.memberId, 
               nameJ, 
               (prefixNm + ' ' + nameE) as nameE, 
               boardPosition, 
               p.email as address, 
               ha.phone as HomePhone, 
               ha.mobile as MobilePhone, 
               ca.companyNm, 
               ca.phone as PhoneCom, 
               ca.fax as faxCom, 
               sortLady
        from private p
        inner join privateBoard b on p.memberId = b.memberId
        inner join privateDetail d on b.memberId = d.memberId
        left join privateAddress ha on b.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on b.memberId = ca.memberId and ca.addressType = 2
        where memberStatus = 'A' and sortLady is not null and sortLady > 0

        union

        select d.memberId, 
               nameJ, 
               (prefixNm + ' ' + nameE) as nameE, 
               '' as boardPosition, 
               '' as address, 
               ha.phone as HomePhone, 
               ha.mobile as MobilePhone, 
               '' as companyNm, 
               '' as PhoneCom, 
               '' as faxCom, 
               99 as sortLady
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        where lady = 1 and memberStatus = 'A' and d.firstmemberId <> d.memberId
        order by sortLady, d.memberId
    ";

                // ใช้ repository แทน connection
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateBoard.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "理事・監事・オブザーバー名簿"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }


            else if (clubCho == "club_secretary")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId as memberid,
                ltrim(nameJ) as nameJ,
                (prefixNm + ' ' + nameE) as nameEng,
                ltrim(ha.phone) as homePhone,
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm,
                ltrim(ca.fax) as companyFax,
                ltrim(ca.phone) as companyPhone,
                d.memberType,
                convert(nvarchar(12), expireDate, 111) as expireDate,
                'Club secretary' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate
                from privatePayment
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where sub_secretary = 1 and memberStatus = 'A'
                order by nameE
                ";

                // ใช้ repository แทน connection
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Club Secretary"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "bazaar_volunteer")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId as memberid,
                ltrim(nameJ) as nameJ,
                (prefixNm + ' ' + nameE) as nameEng,
                ltrim(ha.phone) as homePhone,
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm,
                ltrim(ca.fax) as companyFax,
                ltrim(ca.phone) as companyPhone,
                d.memberType,
                convert(nvarchar(12), expireDate, 111) as expireDate,
                'Bazaar volunteer' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate
                from privatePayment
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where sub_volunteer = 1 and memberStatus = 'A'
                order by nameE
                ";

                // ใช้ repository แทน connection
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Bazaar volunteer"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "social_gathering_members")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId as memberid,
                ltrim(nameJ) as nameJ,
                (prefixNm + ' ' + nameE) as nameEng,
                ltrim(ha.phone) as homePhone,
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm,
                ltrim(ca.fax) as companyFax,
                ltrim(ca.phone) as companyPhone,
                d.memberType,
                convert(nvarchar(12), expireDate, 111) as expireDate,
                'Social gathering members' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate
                from privatePayment
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where sub_social = 1 and memberStatus = 'A'
                order by nameE
                ";

                // ใช้ repository แทน connection ตรง ๆ
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Social gathering members"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "youth_circle_members")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
                select d.memberId as memberid, 
                ltrim(nameJ) as nameJ, 
                (prefixNm + ' ' + nameE) as nameEng, 
                ltrim(ha.phone) as homePhone, 
                ltrim(ha.mobile) as MobilePhone,
                ca.companyNm as companyNm, 
                ltrim(ca.fax) as companyFax, 
                ltrim(ca.phone) as companyPhone, 
                d.memberType, 
                convert(nvarchar(12), expireDate, 111) as expireDate, 
                'Youth circle members' as mode
                from privateDetail d
                inner join privateClub c on d.memberId = c.memberId
                left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
                left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
                left join (
                select memberid, max(expireDate) as expireDate 
                from privatePayment 
                group by memberId
                ) as pm on d.firstmemberId = pm.memberId
                where sub_member = 1 and memberStatus = 'A'
                order by nameE";

                // ใช้ repository แทน connection ตรง ๆ
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Youth circle members"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "overseas_resident_members")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate, 111) as expireDate, 
               'Overseas resident members' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where ov_member = 1 and memberStatus = 'A'
        order by nameE";

                // ใช้ repository แทน connection ตรง ๆ
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Overseas resident members"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "english_test")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate, 111) as expireDate, 
               'English test' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where ev_1 = 1 and memberStatus = 'A'
        order by nameE";

                // ใช้ repository แทน connection ตรง ๆ
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "English test"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "online_event")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate, 111) as expireDate, 
               'Online Event' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where ev_2 = 1 and memberStatus = 'A'
        order by nameE";

                // เรียกครั้งเดียวและเก็บ DataTable
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Online Event"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "softball")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate,111) as expireDate, 
               'SoftBall' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where ev_3 = 1 and memberStatus = 'A'
        order by nameE";

                // เรียกครั้งเดียวและเก็บ DataTable
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "SoftBall"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "yoga")
            {
                // สร้าง instance ของ repository
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string sql = @"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate,111) as expireDate, 
               'Yoga' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where ev_4 = 1 and memberStatus = 'A'
        order by nameE";

                // เรียกครั้งเดียวและเก็บ DataTable
                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", "Yoga"));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }




            /*
            else if (clubCho == "ev_tmp1")
            {
                string text = RadioButtonList1.SelectedItem.Text;
                string sql = "select d.memberId as memberid, ltrim(nameJ) as nameJ, (prefixNm + ' '+ nameE) as nameEng, ltrim(ha.phone) as homePhone, ltrim(ha.mobile) as MobilePhone, " +
                    "ca.companyNm as companyNm, ltrim(ca.fax) as companyFax, ltrim(ca.phone) as companyPhone , d.memberType, " +
                    "convert(nvarchar(12),expireDate,111) as expireDate, '" + text + "' as mode " +
                    "from privateDetail d inner join privateClub c on d.memberId = c.memberId " +
                    "left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1 " +
                    "left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2 " +
                    "left join (select memberid, max(expireDate) as expireDate from privatePayment group by memberId) as pm on d.firstmemberId = pm.memberId " +
                    "where ev_tmp1 = 1 and memberStatus = 'A' order by nameE";
                SelectSqlTable(sql);
                DataTable dt = SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", text));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            else if (clubCho == "ev_tmp2")
            {
                string text = RadioButtonList1.SelectedItem.Text;
                string sql = "select d.memberId as memberid, ltrim(nameJ) as nameJ, (prefixNm + ' '+ nameE) as nameEng, ltrim(ha.phone) as homePhone, ltrim(ha.mobile) as MobilePhone, " +
                    "ca.companyNm as companyNm, ltrim(ca.fax) as companyFax, ltrim(ca.phone) as companyPhone , d.memberType, " +
                    "convert(nvarchar(12),expireDate,111) as expireDate, '" + text + "' as mode " +
                    "from privateDetail d inner join privateClub c on d.memberId = c.memberId " +
                    "left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1 " +
                    "left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2 " +
                    "left join (select memberid, max(expireDate) as expireDate from privatePayment group by memberId) as pm on d.firstmemberId = pm.memberId " +
                    "where ev_tmp2 = 1 and memberStatus = 'A' order by nameE";
                SelectSqlTable(sql);
                DataTable dt = SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", text));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            else if (clubCho == "ev_tmp3")
            {
                string text = RadioButtonList1.SelectedItem.Text;
                string sql = "select d.memberId as memberid, ltrim(nameJ) as nameJ, (prefixNm + ' '+ nameE) as nameEng, ltrim(ha.phone) as homePhone, ltrim(ha.mobile) as MobilePhone, " +
                    "ca.companyNm as companyNm, ltrim(ca.fax) as companyFax, ltrim(ca.phone) as companyPhone , d.memberType, " +
                    "convert(nvarchar(12),expireDate,111) as expireDate, '" + text + "' as mode " +
                    "from privateDetail d inner join privateClub c on d.memberId = c.memberId " +
                    "left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1 " +
                    "left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2 " +
                    "left join (select memberid, max(expireDate) as expireDate from privatePayment group by memberId) as pm on d.firstmemberId = pm.memberId " +
                    "where ev_tmp3 = 1 and memberStatus = 'A' order by nameE";
                SelectSqlTable(sql);
                DataTable dt = SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", text));
                this.ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }
            */


            else if (clubCho == "sub_tmp1")
            {
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string text = RadioButtonList2.SelectedItem.Text;

                string sql = $@"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate,111) as expireDate, 
               '{text}' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where sub_tmp1 = 1 and memberStatus = 'A'
        order by nameE";

                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", text));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }

            else if (clubCho == "sub_tmp2")
            {
                PrivateClubReportRepository repo = new PrivateClubReportRepository();

                string text = RadioButtonList2.SelectedItem.Text;

                string sql = $@"
        select d.memberId as memberid, 
               ltrim(nameJ) as nameJ, 
               (prefixNm + ' ' + nameE) as nameEng, 
               ltrim(ha.phone) as homePhone, 
               ltrim(ha.mobile) as MobilePhone,
               ca.companyNm as companyNm, 
               ltrim(ca.fax) as companyFax, 
               ltrim(ca.phone) as companyPhone, 
               d.memberType, 
               convert(nvarchar(12), expireDate,111) as expireDate, 
               '{text}' as mode
        from privateDetail d
        inner join privateClub c on d.memberId = c.memberId
        left join privateAddress ha on d.memberId = ha.memberId and ha.addressType = 1
        left join privateAddress ca on d.memberId = ca.memberId and ca.addressType = 2
        left join (
            select memberid, max(expireDate) as expireDate 
            from privatePayment 
            group by memberId
        ) as pm on d.firstmemberId = pm.memberId
        where sub_tmp2 = 1 and memberStatus = 'A'
        order by nameE";

                DataTable dt = repo.SelectSqlTable(sql);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PrivateReport/ReportPage/printPrivateClub.rdlc");

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("showClub", text));
                ReportViewer1.LocalReport.SetParameters(reportParameters);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
            }



        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList2.ClearSelection();
            RadioButtonList3.ClearSelection();
        }
        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.ClearSelection();
            RadioButtonList3.ClearSelection();
        }
        protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.ClearSelection();
            RadioButtonList2.ClearSelection();
        }
    }
}