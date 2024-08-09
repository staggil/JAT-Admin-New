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
namespace JAT.Company
{
    public partial class companySearch : System.Web.UI.Page
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
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void BindData()
        {
            DataTable td;
            var companyNJ = companyNmJ.Text.ToString();
            var companyNE = companyNmE.Text.ToString();
            var memSatCho = DropDownList1.SelectedValue.ToString();

            string inputOpt = "";
            string inputOpt2 = "";

            if (companyNJ != "")
            {
                inputOpt = "AND companyNmJ LIKE  N'%" + companyNJ.Replace("'", "''") + "%'";
            }
            else if (companyNJ == "")
            {
                inputOpt = "";
            }
            if (companyNE != "")
            {
                inputOpt2 = "AND companyNmE LIKE '%" + companyNE.Replace("'", "''") + "%'";
            }
            else if (companyNE == "")
            {
                inputOpt2 = "";
            }
            td = SelectSqlTable("SELECT companyId,companyNmJ,companyNmE " +
                                "FROM CompanyMember " +
                                "WHERE memberStatus " + memSatCho + inputOpt2 + inputOpt +
                                " order by companyNmE, companyId");
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