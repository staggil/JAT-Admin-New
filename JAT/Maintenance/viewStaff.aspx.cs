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


namespace JAT.Maintenance
{
    public partial class viewStaff : System.Web.UI.Page
    {
		private SqlConnection conn;

		private void connection()
		{
			var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
			conn = new SqlConnection(connectionStr.ConnectionString);
		}

		//public DataTable SelectSqlTable(string Sqlcmd)
		//{
		//	connection();
		//	var table = new DataTable();
		//	string sql = Sqlcmd;
		//	conn.Open();
		//	String SQLStatement = "SELECT  *, case when staffBranch = 1 then 'Sathorn' else 'Sukhumvit' end as branchNm " +
		//	"FROM  SStaff order by staffId";
		//	//cmd = new SqlCommand(sql, conn);
		//	SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
		//	//SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
		//	dataAdapter.Fill(table);
		//	conn.Close();
		//	return table;
		//}

		private void BindStaff()
		{
			DataGrid1.DataSource = GetStaffData().Tables["SStaff"].DefaultView;
			DataGrid1.DataBind();
		}

		private DataSet GetStaffData()
        {
			connection();
			String SQLStatement = "SELECT staffID,staffFName,staffEmail, case when staffBranch = 1 then 'Sathorn' else 'Sukhumvit' end as branchNm " +
			"FROM  SStaff order by staffId";
			SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
			DataSet myDataSet;
			dataAdapter.SelectCommand.CommandType = CommandType.Text;
			myDataSet = new DataSet();
			dataAdapter.Fill(myDataSet, "SStaff");
			return myDataSet;
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				BindStaff();
			//DataTable td;

			//td = SelectSqlTable("SELECT  *, case when staffBranch = 1 then 'Sathorn' else 'Sukhumvit' end as staffBranch " +
			//"FROM  SStaff order by staffId");


			//GridView1.DataSource = td;
			//GridView1.DataBind();
		}

        protected void add_Click(object sender, EventArgs e)
        {
			Response.Redirect("viewStaffAdd.aspx");
		}
    }
}