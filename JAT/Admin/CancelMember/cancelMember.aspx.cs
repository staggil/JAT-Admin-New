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

namespace JAT.Admin.CancelMember
{
    public partial class cancelMember : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;
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
            BindData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void BindData()
        {
            DataTable td;
            var status = DropDownList1.SelectedValue.ToString();
            td = SelectSqlTable("SELECT cancel_id,FORMAT(cancel_date,'dd/MM/yyyy') as canceldate,cancel_member,cancel_type,CASE  WHEN cancel_status = 'AP' THEN 'Approved' WHEN cancel_status = 'RJ' THEN 'Rejected' ELSE  'Waiting Admin' END AS cancel_status,staff_id,FORMAT(staff_date,'dd/MM/yyyy') AS staff_date FROM CancelPrivate WHERE cancel_status " + status + "Order by cancel_date ");
            GridView1.DataSource = td;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}