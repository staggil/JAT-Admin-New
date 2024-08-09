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

namespace JAT.Admin.RegisterMember
{
    public partial class regCheck : System.Web.UI.Page
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
            string member = "";
            if(Text1.Value.ToString().Trim() != "")
            {
                
                member = "AND register_member LIKE '%" + Text1.Value.ToString().Trim() + "%'";
            }
            System.Diagnostics.Debug.WriteLine("SELECT register_id,register_type,FORMAT(register_date,'dd/MM/yyyy') as regisdate,register_member,FORMAT(register_approve,'dd/MM/yyyy') as register_approve,register_status  FROM RegisterPrivate WHERE register_status " + status + member + "  Order by register_date ");
            td = SelectSqlTable("SELECT register_id,register_type,FORMAT(register_date,'dd/MM/yyyy') as regisdate,register_member,FORMAT(register_approve,'dd/MM/yyyy') as register_approve,register_status  FROM RegisterPrivate WHERE register_status " + status + member +"  Order by register_date ");
            
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