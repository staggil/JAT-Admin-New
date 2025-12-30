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
using JAT.Core;


namespace JAT.Private
{
    public partial class privateSearch : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();
		private SqlConnection conn;
        private SqlCommand cmd;
        private string textInput;
        private int textInputInt;
        private int valOfTable;
        private PrivateSearchRepository _repo = new PrivateSearchRepository();
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
            cmd.CommandTimeout = 6000;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        */


        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Clear();

            Label3.Visible = false;

            ImageButton1.Visible = false;
            ImageButton2.Visible = false;


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "2")
            {
                RadioButtonList1.SelectedValue = "1";
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            var repo = new PrivateSearchRepository();
            DataTable td;
            string activityDetail;

            string memSatCho = DropDownList1.SelectedValue;
            string memberTypeCho = RadioButtonList1.SelectedValue;
            string textInputOpt = DropDownList2.SelectedValue;
            string textInput = Text1.Value;
            string sortOpt = DropDownList3.SelectedValue;
            string sortCouOpt = DropDownList4.SelectedValue;

            string query;
            Dictionary<string, object> parameters = new Dictionary<string, object>();


            if (RadioButtonList1.SelectedValue != "4")
            {
                query = "EXEC searchPrivate @memSatCho, @memberTypeCho, @textInputOpt, @textInput, @sortOpt, @sortCouOpt";
                parameters.Add("@memSatCho", memSatCho);
                parameters.Add("@memberTypeCho", memberTypeCho);
                parameters.Add("@textInputOpt", textInputOpt);
                parameters.Add("@textInput", "%" + textInput + "%");
                parameters.Add("@sortOpt", sortOpt);
                parameters.Add("@sortCouOpt", sortCouOpt);
            }
            else
            {
                query = @"
            IF OBJECT_ID('tempdb..#TEMP1') IS NOT NULL DROP TABLE #TEMP1
            IF OBJECT_ID('tempdb..#TEMP2') IS NOT NULL DROP TABLE #TEMP2
            IF OBJECT_ID('tempdb..#TEMP3') IS NOT NULL DROP TABLE #TEMP3
            IF OBJECT_ID('tempdb..#TEMP') IS NOT NULL DROP TABLE #TEMP

            CREATE TABLE #TEMP1 (...columns...)
            CREATE TABLE #TEMP2 (...columns...)
            CREATE TABLE #TEMP3 (...columns...)

            INSERT INTO #TEMP1 EXEC searchPrivate @memSatCho, '1', @textInputOpt, @textInput, @sortOpt, @sortCouOpt
            INSERT INTO #TEMP2 EXEC searchPrivate @memSatCho, '2', @textInputOpt, @textInput, @sortOpt, @sortCouOpt
            INSERT INTO #TEMP3 EXEC searchPrivate @memSatCho, '3', @textInputOpt, @textInput, @sortOpt, @sortCouOpt

            SELECT * INTO #TEMP FROM #TEMP1
            UNION SELECT * FROM #TEMP2
            UNION SELECT * FROM #TEMP3

            SELECT * FROM #TEMP

            DROP TABLE #TEMP1, #TEMP2, #TEMP3, #TEMP";
                parameters.Add("@memSatCho", memSatCho);
                parameters.Add("@textInputOpt", textInputOpt);
                parameters.Add("@textInput", "%" + textInput + "%");
                parameters.Add("@sortOpt", sortOpt);
                parameters.Add("@sortCouOpt", sortCouOpt);
            }

            try
            {
                td = repo.ExecuteQuery(query, parameters);
                GridView1.DataSource = td;
                GridView1.DataBind();
                Label1.Text = $"Searching for <b>{textInput}</b> returned <b>{td.Rows.Count}</b> people(s).";
                activityDetail = "Executed searchPrivate successfully (user id = '" + staffID + "')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
            catch (Exception ex)
            {
                activityDetail = "Executed searchPrivate unsuccessfully [" + ex.Message + "] (user id = '" + staffID + "')";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }

            ImageButton2.Visible = true;
            ImageButton1.Visible = GridView1.PageIndex != 0;
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "rblchildren")
            {
                if (DropDownList2.SelectedValue == "companyNm")
                {
                    RadioButtonList1.SelectedValue = "firstMem";
                }
            }else if (RadioButtonList1.SelectedValue == "familyMem")
            {
                if (DropDownList2.SelectedValue == "companyNm")
                {
                    RadioButtonList1.SelectedValue = "firstMem";
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            GridView1.PageIndex--;
            BindData();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            GridView1.PageIndex++;
            BindData();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "firstMem")
            {
                DropDownList2.SelectedValue = "memberid";
            }
            else if (RadioButtonList1.SelectedValue == "familyMem")
            {
                DropDownList2.SelectedValue = "memberid";
            }
            else if (RadioButtonList1.SelectedValue == "rblchildren")
            {
                DropDownList2.SelectedValue = "memberid";
                DropDownList1.SelectedValue = "IN ('A', 'NA')";
            }
            else if (RadioButtonList1.SelectedValue == "rblall")
            {
                DropDownList2.SelectedValue = "memberid";
            }
        }
        protected void checkKeyValue(object sender, ServerValidateEventArgs args)
        {
            try
            {
                if (DropDownList2.SelectedValue.ToString() == "1")
                {
                    int i = int.Parse(args.Value);
                    args.IsValid = true;
                }

            }
            catch (Exception e)
            {
                args.IsValid = false;
            }

        }
    }
}