using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
namespace JAT
{
    public partial class procSendMail : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private string strMailServer;
        private string strMailUser;
        private string strMailPassword;
        private string strMailFrom;
        private string strEmail;
        private string tmpUser;
        string toDayDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        //protected void DisableControls(Control parent, bool State)
        //{
        //    foreach (Control c in parent.Controls)
        //    {
        //        if (c is DropDownList)
        //        {
        //            ((DropDownList)(c)).Enabled = State;
        //        }

        //        DisableControls(c, State);
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            initLabel();
            if (!IsPostBack)
            {
                DataTable td;
                td = SelectSqlTable("SELECT * FROM privateClubDetail");
                foreach(DataRow tmprow in td.Rows)
                {
                    switch (tmprow["itemNm"].ToString().Trim())
                    {
                        case "ev_tmp1": ev_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                        case "ev_tmp2": ev_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                        case "ev_tmp3": ev_tmp3.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                        case "sub_tmp1": sub_tmp1.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                        case "sub_tmp2": sub_tmp2.Text = "&nbsp;" + tmprow["itemVal"].ToString(); break;
                    }
                }
                initLabel();
            }
            initMail();
        }
        public DataTable SelectSqlTable(string Sqlcmd)
        {
            connection();
            var table = new DataTable();
            string sql = Sqlcmd;
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 600;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        protected void sendEmail_Click(object sender, EventArgs e)
        {
            //no check or subject/content is blank
            if (sendMailCheck())
            {
                connection();
                conn.Open();
                string sql = "SELECT email,'' as 'emailrep','' as 'emailtre', '' as 'emailper' FROM PrivateDetail pd " +
                        "INNER JOIN PrivateClub pc ON pd.memberid = pc.memberid " +
                        "INNER JOIN Private p ON pd.memberid = p.memberid " +
                        "WHERE (email is not null and email <> '') ";
                string whereclause = getWhereClause();
                sql += whereclause;
                //sql = "SELECT 'temporary0253@gmail.com'";
                cmd = new SqlCommand(sql, conn);
                SqlDataReader myRead = cmd.ExecuteReader();
                while (myRead.Read())
                {
                    for(int i = 0; i < 4; i++)
                    {
                        var val = myRead.GetValue(i).ToString();
                        if (val != null && val != "")
                        {
                            mailSend(val);
                        }
                    }
                }
                lblText.Text = "Message Sent Successfully";
                conn.Close();
            }
            else
            {
                lblText.Text = "Please check input again";
            }
        }
        protected void initLabel()
        {
            if (ev_tmp1.Text.Trim() == "&nbsp;")
            {
                ev_tmp1.Checked = false;
                ev_tmp1.Enabled = false;
            }
            if (ev_tmp2.Text.Trim() == "&nbsp;")
            {
                ev_tmp2.Checked = false;
                ev_tmp2.Enabled = false;
            }
            if (ev_tmp3.Text.Trim() == "&nbsp;")
            {
                ev_tmp3.Checked = false;
                ev_tmp3.Enabled = false;
            }
            if (sub_tmp1.Text.Trim() == "&nbsp;")
            {
                sub_tmp1.Checked = false;
                sub_tmp1.Enabled = false;
            }
            if (sub_tmp2.Text.Trim() == "&nbsp;")
            {
                sub_tmp2.Checked = false;
                sub_tmp2.Enabled = false;
            }
        }
        protected void setActiveEvent(bool chk)
        {
            head_ev.Checked = false;
            head_ev.Enabled = chk;
            ev_1.Checked = false;
            ev_1.Enabled = chk;
            ev_2.Checked = false;
            ev_2.Enabled = chk;
            ev_3.Checked = false;
            ev_3.Enabled = chk;
            ev_4.Checked = false;
            ev_4.Enabled = chk;
            ev_tmp1.Checked = false;
            ev_tmp1.Enabled = chk;
            ev_tmp2.Checked = false;
            ev_tmp2.Enabled = chk;
            ev_tmp3.Checked = false;
            ev_tmp3.Enabled = chk;
        }
        protected void setActiveSubcommittee(bool chk)
        {
            head_sub.Checked = false;
            head_sub.Enabled = chk;
            sub_1.Checked = false;
            sub_1.Enabled = chk;
            sub_2.Checked = false;
            sub_2.Enabled = chk;
            sub_3.Checked = false;
            sub_3.Enabled = chk;
            sub_4.Checked = false;
            sub_4.Enabled = chk;
            sub_5.Checked = false;
            sub_5.Enabled = chk;
            sub_6.Checked = false;
            sub_6.Enabled = chk;
            sub_7.Checked = false;
            sub_7.Enabled = chk;
            sub_8.Checked = false;
            sub_8.Enabled = chk;
            sub_tmp1.Checked = false;
            sub_tmp1.Enabled = chk;
            sub_tmp2.Checked = false;
            sub_tmp2.Enabled = chk;
        }
        protected void setActiveSukusuku(bool chk)
        {
            head_sk.Checked = false;
            head_sk.Enabled = chk;
            sk_1.Checked = false;
            sk_1.Enabled = chk;
        }
        protected void setActiveChildrenLibrary(bool chk)
        {
            head_ch.Checked = false;
            head_ch.Enabled = chk;
            ch_1.Checked = false;
            ch_1.Enabled = chk;
        }
        protected void setActiveOverseasResidentMembers(bool chk)
        {
            head_ov.Checked = false;
            head_ov.Enabled = chk;
            ov_1.Checked = false;
            ov_1.Enabled = chk;
        }
        protected void setActiveSupportingMemberCompanies(bool chk)
        {
            head_sup.Checked = false;
            head_sup.Enabled = chk;
            sup_1.Checked = false;
            sup_1.Enabled = chk;
            sup_2.Checked = false;
            sup_2.Enabled = chk;
            sup_3.Checked = false;
            sup_3.Enabled = chk;
        }
        protected void selectOne(string chkboxid)
        {
            //chkboxid split '_' into array and specific where column name is, use it as variable for switch case
            //an other case is "All_"
            var tmparray = chkboxid.Split('_');
            var tmpid = "All";
            if (tmparray[0] == "head")
            {
                tmpid = tmparray[1];
            }
            else
            {
                tmpid = tmparray[0];
            }
            switch (tmpid)
            {
                case "ev":
                    setActiveSubcommittee(false);
                    setActiveSukusuku(false);
                    setActiveChildrenLibrary(false);
                    setActiveOverseasResidentMembers(false);
                    setActiveSupportingMemberCompanies(false);
                    if (tmparray[0] != "head")
                    {
                        selectOneEvent(chkboxid);
                    }
                    break;
                case "sub":
                    setActiveEvent(false);
                    setActiveSukusuku(false);
                    setActiveChildrenLibrary(false);
                    setActiveOverseasResidentMembers(false);
                    setActiveSupportingMemberCompanies(false);
                    if (tmparray[0] != "head")
                    {
                        selectOneSubCommittee(chkboxid);
                    }
                    break;
                case "sk":
                    setActiveEvent(false);
                    setActiveSubcommittee(false);
                    setActiveChildrenLibrary(false);
                    setActiveOverseasResidentMembers(false);
                    setActiveSupportingMemberCompanies(false);
                    if (tmparray[0] != "head")
                    {
                        selectOneSukusuku(chkboxid);
                    }
                    break;
                case "ch":
                    setActiveEvent(false);
                    setActiveSubcommittee(false);
                    setActiveSukusuku(false);
                    setActiveOverseasResidentMembers(false);
                    setActiveSupportingMemberCompanies(false);
                    if (tmparray[0] != "head")
                    {
                        selectOneChildrenLibrary(chkboxid);
                    }
                    break;
                case "ov":
                    setActiveEvent(false);
                    setActiveSubcommittee(false);
                    setActiveSukusuku(false);
                    setActiveChildrenLibrary(false);
                    setActiveSupportingMemberCompanies(false);
                    if (tmparray[0] != "head")
                    {
                        selectOneOverseasResidentMembers(chkboxid);
                    }
                    break;
                case "sup":
                    setActiveEvent(false);
                    setActiveSubcommittee(false);
                    setActiveSukusuku(false);
                    setActiveChildrenLibrary(false);
                    setActiveOverseasResidentMembers(false);
                    if (tmparray[0] != "head")
                    {
                        selectOneSupportingMemberCompanies(chkboxid);
                    }
                    break;
                default:selectNone();break;
            }
        }
        protected void selectNone()
        {
            setActiveEvent(true);
            setActiveSubcommittee(true);
            setActiveSukusuku(true);
            setActiveChildrenLibrary(true);
            setActiveOverseasResidentMembers(true);
            setActiveSupportingMemberCompanies(true);
        }

        protected void groupboxCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (CheckBox)sender;
            if (chkbox.Checked == true)
            {
                selectOne(chkbox.ID);
            }
            else
            {
                var tmpdesel = chkbox.ID.Split('_');
                if (tmpdesel[0] == "head")
                {
                    //Deselect head
                    selectOne("All_");
                }
                else
                {
                    //Deselect item
                    //Enable other items in a group
                    switch (tmpdesel[0])
                    {
                        case "ev":
                            setActiveEvent(true);
                            head_ev.Checked = true;
                            break;
                        case "sub":
                            setActiveSubcommittee(true);
                            head_sub.Checked = true;
                            break;
                        case "sk":
                            setActiveSukusuku(true);
                            head_sk.Checked = true;
                            break;
                        case "ch":
                            setActiveChildrenLibrary(true);
                            head_ch.Checked = true;
                            break;
                        case "ov":
                            setActiveOverseasResidentMembers(true);
                            head_ov.Checked = true;
                            break;
                    }
                    initLabel();
                }
            }
            initLabel();
        }
        protected void selectOneEvent(string id)
        {
            setActiveEvent(false);
            head_ev.Checked = true;
            head_ev.Enabled = true;
            switch (id)
            {
                case "ev_1":
                    ev_1.Checked = true;
                    ev_1.Enabled = true;
                    break;
                case "ev_2":
                    ev_2.Checked = true;
                    ev_2.Enabled = true;
                    break;
                case "ev_3":
                    ev_3.Checked = true;
                    ev_3.Enabled = true;
                    break;
                case "ev_4":
                    ev_4.Checked = true;
                    ev_4.Enabled = true;
                    break;
                case "ev_tmp1":
                    ev_tmp1.Checked = true;
                    ev_tmp1.Enabled = true;
                    break;
                case "ev_tmp2":
                    ev_tmp2.Checked = true;
                    ev_tmp2.Enabled = true;
                    break;
                case "ev_tmp3":
                    ev_tmp3.Checked = true;
                    ev_tmp3.Enabled = true;
                    break;
            }
        }
        protected void selectOneSubCommittee(string id)
        {
            setActiveSubcommittee(false);
            head_sub.Checked = true;
            head_sub.Enabled = true;
            switch (id)
            {
                case "sub_1":
                    sub_1.Checked = true;
                    sub_1.Enabled = true;
                    break;
                case "sub_2":
                    sub_2.Checked = true;
                    sub_2.Enabled = true;
                    break;
                case "sub_3":
                    sub_3.Checked = true;
                    sub_3.Enabled = true;
                    break;
                case "sub_4":
                    sub_4.Checked = true;
                    sub_4.Enabled = true;
                    break;
                case "sub_5":
                    sub_5.Checked = true;
                    sub_5.Enabled = true;
                    break;
                case "sub_6":
                    sub_6.Checked = true;
                    sub_6.Enabled = true;
                    break;
                case "sub_7":
                    sub_7.Checked = true;
                    sub_7.Enabled = true;
                    break;
                case "sub_8":
                    sub_8.Checked = true;
                    sub_8.Enabled = true;
                    break;
                case "sub_tmp1":
                    sub_tmp1.Checked = true;
                    sub_tmp1.Enabled = true;
                    break;
                case "sub_tmp2":
                    sub_tmp2.Checked = true;
                    sub_tmp2.Enabled = true;
                    break;
            }
        }
        protected void selectOneSukusuku(string id)
        {
            setActiveSukusuku(false);
            head_sk.Checked = true;
            head_sk.Enabled = true;
            sk_1.Checked = true;
            sk_1.Enabled = true;
        }
        protected void selectOneChildrenLibrary(string id)
        {
            setActiveChildrenLibrary(false);
            head_ch.Checked = true;
            head_ch.Enabled = true;
            ch_1.Checked = true;
            ch_1.Enabled = true;
        }
        protected void selectOneOverseasResidentMembers(string id)
        {
            setActiveOverseasResidentMembers(false);
            head_ov.Checked = true;
            head_ov.Enabled = true;
            ov_1.Checked = true;
            ov_1.Enabled = true;
        }
        protected void selectOneSupportingMemberCompanies(string id)
        {
            head_sup.Checked = true;
        }
        protected string getWhereClause()
        {
            string clause = "";
            if (head_ev.Checked == true)
            {
                if (ev_1.Checked == true)
                {
                    //English test
                    clause = "AND pc.ev_1 = 1";
                }
                else if (ev_2.Checked == true)
                {
                    //online event
                    clause = "AND pc.ev_2 = 1";
                }
                else if (ev_3.Checked == true)
                {
                    //softball
                    clause = "AND pc.ev_3 = 1";
                }
                else if (ev_4.Checked == true)
                {
                    //yoga
                    clause = "AND pc.ev_4 = 1";
                }
                else if (ev_tmp1.Checked == true)
                {
                    //tmp1
                    clause = "AND pc.ev_tmp1 = 1";
                }
                else if (ev_tmp2.Checked == true)
                {
                    //tmp2
                    clause = "AND pc.ev_tmp2 = 1";
                }
                else if (ev_tmp3.Checked == true)
                {
                    //tmp3
                    clause = "AND pc.ev_tmp3 = 1";
                }
            }
            else if (head_sub.Checked == true)
            {
                if (sub_1.Checked == true)
                {
                    //board
                    clause = "AND pc.board = 1";
                }
                else if (sub_2.Checked == true)
                {
                    //board list
                    clause = "AND pc.sub_board_list = 1";
                }
                else if (sub_3.Checked == true)
                {
                    //golf
                    clause = "AND pc.golf = 1";
                }
                else if (sub_4.Checked == true)
                {
                    //lady
                    clause = "AND pc.lady = 1";
                }
                else if (sub_5.Checked == true)
                {
                    //club secretary

                    clause = "AND pc.sub_secretary = 1";
                }
                else if (sub_6.Checked == true)
                {
                    //bazaar volunteer
                    clause = "AND pc.sub_volunteer = 1";
                }
                else if (sub_7.Checked == true)
                {
                    //social gathering members

                    clause = "AND pc.sub_social = 1";
                }
                else if (sub_8.Checked == true)
                {
                    //youth circle member

                    clause = "AND pc.sub_member = 1";
                }
                else if (sub_tmp1.Checked == true)
                {
                    //tmp1
                    clause = "AND pc.sub_tmp1 = 1";
                }
                else if (sub_tmp2.Checked == true)
                {
                    //tmp2
                    clause = "AND pc.sub_tmp2 = 1";
                }
            }
            else if (head_sk.Checked == true)
            {
                if (sk_1.Checked == true)
                {
                    //sk
                    clause = "AND pc.zukuzuku = 1";
                }
            }
            else if (head_ch.Checked == true)
            {
                if (ch_1.Checked == true)
                {
                    //ch
                    clause = "AND pc.children = 1";
                }
            }
            else if (head_ov.Checked == true)
            {
                if (ov_1.Checked == true)
                {
                    //ov
                    clause = "AND pc.ov_member = 1";
                }
            }
            else if (head_sup.Checked == true)
            {
                clause = "AND 1=0 UNION ALL SELECT ";
                if(sup_1.Checked == true)
                {
                    if(sup_2.Checked == true)
                    {
                        if (sup_3.Checked == true)
                        {
                            //1,2,3
                            clause += "email,represEm as 'emailrep',AccEm as 'emailtre', personinchargeEm as 'emailper' FROM CompanyMember ";
                            clause += "WHERE (represEm is not null and represEm <> '') OR (AccEm is not null and AccEm <> '') OR (personinchargeEm is not null and personinchargeEm <> '')";
                        }
                        else
                        {
                            //1,2
                            clause += "email,represEm as 'emailrep',AccEm as 'emailtre', '' as 'emailper' FROM CompanyMember ";
                            clause += "WHERE (represEm is not null and represEm <> '') OR (AccEm is not null and AccEm <> '')";
                        }
                    }
                    else
                    {
                        if (sup_3.Checked == true)
                        {
                            //1,3
                            clause += "email,represEm as 'emailrep','' as 'emailtre', personinchargeEm as 'emailper' FROM CompanyMember ";
                            clause += "WHERE (represEm is not null and represEm <> '') OR (personinchargeEm is not null and personinchargeEm <> '')";
                        }
                        else
                        {
                            //1
                            clause += "email,represEm as 'emailrep','' as 'emailtre', '' as 'emailper' FROM CompanyMember ";
                            clause += "WHERE (represEm is not null and represEm <> '')";
                        }
                    }
                }
                else
                {
                    if (sup_2.Checked == true)
                    {
                        if (sup_3.Checked == true)
                        {
                            //2,3
                            clause += "email,'' as 'emailrep',AccEm as 'emailtre', personinchargeEm as 'emailper' FROM CompanyMember ";
                            clause += "WHERE (AccEm is not null and AccEm <> '') OR (personinchargeEm is not null and personinchargeEm <> '')";
                        }
                        else
                        {
                            //2
                            clause += "email,'' as 'emailrep',AccEm as 'emailtre', '' as 'emailper' FROM CompanyMember ";
                            clause += "WHERE (AccEm is not null and AccEm <> '')";
                        }
                    }
                    else
                    {
                        //3
                        clause += "email,'' as 'emailrep','' as 'emailtre', personinchargeEm as 'emailper' FROM CompanyMember ";
                        clause += "WHERE (personinchargeEm is not null and personinchargeEm <> '')";
                    }
                }
            }
            return clause;
        }
        protected bool sendMailCheck()
        {
            bool tmpchk = false;
            if (txtSubject.Value.Trim() == "")
            {
                return false;
            }
            else
            {
                if (txtBody.Value.Trim() == "")
                {
                    return false;
                }
                else
                {
                    if (head_ev.Checked == true)
                    {
                        if (ev_1.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (ev_2.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (ev_3.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (ev_4.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (ev_tmp1.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (ev_tmp2.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (ev_tmp3.Checked == true)
                        {
                            tmpchk = true;
                        }
                    }
                    else if (head_sub.Checked == true)
                    {
                        if (sub_1.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_2.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_3.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_4.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_5.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_6.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_7.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_8.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_tmp1.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else if (sub_tmp2.Checked == true)
                        {
                            tmpchk = true;
                        }
                    }
                    else if (head_sk.Checked == true)
                    {
                        if (sk_1.Checked == true)
                        {
                            tmpchk = true;
                        }
                    }
                    else if (head_ch.Checked == true)
                    {
                        if (ch_1.Checked == true)
                        {
                            tmpchk = true;
                        }
                    }
                    else if (head_ov.Checked == true)
                    {
                        if (ov_1.Checked == true)
                        {
                            tmpchk = true;
                        }
                    }
                    else if (head_sup.Checked == true)
                    {
                        if (sup_1.Checked == true)
                        {
                            tmpchk = true;
                        }
                        else
                        {
                            if (sup_2.Checked == true)
                            {
                                tmpchk = true;
                            }
                            else
                            {
                                tmpchk = true;
                            }
                        }
                    }
                }
            }
            return tmpchk;
        }
        private void initMail()
        {
            String userid;
            if (Session["UID"]!=null)
            {
                userid = Session["UID"].ToString();
            }
            else
            {
                userid = "";
            }
            DataTable dt = new DataTable();
            dt = SelectSqlTable("select staffemail from sstaff where staffid = '" + userid + "' ");
            if (dt.Rows.Count > 0)
                strEmail = dt.Rows[0][0].ToString();
            else
                strEmail = WebConfigurationManager.AppSettings["mailUser"];
            if (!isValidEmail(strEmail))
            {
                strEmail = WebConfigurationManager.AppSettings["mailUser"];
            }
            RadioButtonList1.Items[0].Text = "&nbsp;"+strEmail;
            RadioButtonList1.Items[0].Value = strEmail;
            strMailServer = WebConfigurationManager.AppSettings.Get("mailServer");
            strMailUser = WebConfigurationManager.AppSettings["mailUser"];
            strMailPassword = WebConfigurationManager.AppSettings["mailPassword"];
            strMailFrom = WebConfigurationManager.AppSettings["mailFrom"] + "<" + RadioButtonList1.SelectedValue + ">";
            if (!isValidEmail(RadioButtonList1.SelectedValue))
            {
                strMailFrom = WebConfigurationManager.AppSettings["mailFrom"] + "<" + WebConfigurationManager.AppSettings["mailUser"] + ">";
            }
        }
        public Boolean isValidEmail(string email)
        {
            string pattern = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;
            if (string.IsNullOrEmpty(email))
                valid = false;
            else
                valid = check.IsMatch(email);
            return valid;
        }

        protected void openLog_Click(object sender, EventArgs e)
        {
            var fileExist = File.Exists(Server.MapPath("~/LogFile.txt"));
            if (fileExist)//check file exist
            {
                Response.Redirect("~/LogFile.txt");
            }
            else
            {
                string script = "<script type=\"text/javascript\">alert('Log file not exist');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", script);
            }
        }
        protected void doLog(string tmp)
        {
            string filename = "~/LogFile.txt";
            var fileExist = File.Exists(Server.MapPath("~/LogFile.txt"));
            //file exist or not
            if (fileExist)
            {
                //read and write
                try
                {
                    using (StreamWriter _txtData = File.AppendText(Server.MapPath(filename)))
                    {
                        _txtData.WriteLine(tmp);
                    }
                }
                catch { }
            }
            else
            {
                //create and write
                try
                {
                    // Create a new file     
                    using (StreamWriter _txtData = new StreamWriter(Server.MapPath(filename), true))
                    {
                        _txtData.WriteLine(tmp); // Write the file.
                    }
                }
                catch { }
            }
        }
        protected void mailSend(string toVal)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential(strMailUser, strMailPassword);
            MailMessage myMail = new MailMessage();
            MailAddress fromAddress = new MailAddress(strMailFrom);

            smtpClient.Host = strMailServer;

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;
	    //smtpClient.EnableSsl = true;

            myMail.BodyEncoding = System.Text.Encoding.GetEncoding(932);
            //myMail.Bcc = vTo;
            myMail.From = fromAddress;
            myMail.Subject = txtSubject.Value;
            //myMail.BodyFormat = MailFormat.Text;
            myMail.IsBodyHtml = false;
            myMail.Body = txtBody.InnerText;
            myMail.To.Add(toVal);

            smtpClient.Send(myMail);
            //log
            string logtxt = toDayDate + " sender: " + strMailFrom + " to: " + toVal + "\nsubject: " + txtSubject.Value.ToString() + "\ncontent: " + txtBody.InnerText.ToString();
            doLog(logtxt);
            System.Threading.Thread.Sleep(250);
        }
    }
}