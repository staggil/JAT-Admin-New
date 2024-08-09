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
using System.Web.Services;

namespace JAT.Maintenance
{
    public partial class itemEvent : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();

		protected void Page_Load(object sender, EventArgs e)
        {
            gridBinding(DataGrid1, "all");
            if (!IsPostBack)
            {
                div_datagrid1_edit.Visible = false;
            }
        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        protected void chngbtn_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //Get rowindex
            int rowindex = gvr.RowIndex;
            gridBinding(DataGrid1_Edit, DataGrid1.DataKeys[rowindex].Value.ToString());
            div_datagrid1_edit.Visible = true;
            div_datagrid1.Visible = false;
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
        protected void gridBinding(GridView grdref,string key)
        {
            DataTable td;
            string sqltmp = "";
            if (key == "all")
            {
                sqltmp = "SELECT ROW_NUMBER() OVER(ORDER BY itemNm) AS 'itemNo',* FROM PrivateClubDetail WHERE itemNm like 'ev_tmp%'";
            }
            else
            {
                //key=itemNm
                sqltmp = "SELECT * FROM PrivateClubDetail WHERE itemNm ='"+key+"'";
            }
            td = SelectSqlTable(sqltmp);
            grdref.DataSource = td;
            grdref.DataBind();
            conn.Close();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            div_datagrid1_edit.Visible = false;
            div_datagrid1.Visible = true;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			//hide datagrid
			div_datagrid1_edit.Visible = false;
            div_datagrid1.Visible = true;
            //exec sp_itemupd
            connection();
            conn.Open();
            cmd = new SqlCommand("spClubItemUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@itemNm", SqlDbType.VarChar).Value = DataGrid1_Edit.DataKeys[0].Value.ToString();
			var itemnamenew = DataGrid1_Edit.Rows[0].FindControl("itemname_new") as TextBox;
            var newval = itemnamenew.Text;
            cmd.Parameters.Add("@itemNew", SqlDbType.VarChar).Value = newval;
            
            try
            {
				cmd.ExecuteNonQuery();
                string activityDetail = $"Called stored procedure name 'spClubItemUpdate' successful (User id = '{staffID}')";
                logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
				string activityDetail = $"Called stored procedure name 'spClubItemUpdate' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Called stored procedure name 'spClubItemUpdate' unsuccessful [{ex.Message}] (User id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			conn.Close();
            gridBinding(DataGrid1, "all");
        }
    }
}