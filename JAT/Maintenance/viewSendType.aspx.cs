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
	public partial class viewSendType : System.Web.UI.Page
	{
		private SqlConnection conn;

		private void connection()
		{
			var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
			conn = new SqlConnection(connectionStr.ConnectionString);
		}

		private void Binddata()
		{
			DataGrid1.DataSource = GetStaffData().Tables["SSendType"].DefaultView;
			DataGrid1.DataBind();
		}

		private DataSet GetStaffData()
		{
			connection();
			String SQLStatement = "SELECT sendID,sendtype,description " +
			"FROM SSendType order by sendID";
			SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLStatement, conn);
			DataSet myDataSet;
			dataAdapter.SelectCommand.CommandType = CommandType.Text;
			myDataSet = new DataSet();
			dataAdapter.Fill(myDataSet, "SSendType");
			return myDataSet;
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				Binddata();
		}
	}
}