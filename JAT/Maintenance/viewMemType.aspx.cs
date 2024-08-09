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
    public partial class viewMemType : System.Web.UI.Page
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
			DataTable td;
            //td = SelectSqlTable("SELECT distinct MemberType,Description,Newsletter " +
            //					"FROM SMemberType " +
            //					"WHERE EffectiveID = '2'");
            td = SelectSqlTable("select * from SMemberType where EffectiveID = (select TOP(1) EffectiveID from tblEffective where EffectiveDate <= floor(cast(getdate() as float)) and ExpireDate>= floor(cast(getdate() as float))) order by memberType");
            DataGrid1.DataSource = td;
            DataGrid1.DataBind();
		}

        protected void Radio_Change(object sender, EventArgs e)
        {
            if (RadioButtonList1.Items[0].Selected)
            {
                setColumnAll();
                CheckBoxList1.Enabled = false;
            }
            else if (RadioButtonList1.Items[1].Selected)
            {
                CheckBoxList1.Enabled = true;
                setCheckbox();
                setColumnSelection();
            }
        }

        protected void setColumnAll()
        {
            int numberOfColumns = DataGrid1.Columns.Count;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                DataGrid1.Columns[indexCount].Visible = true;
            }
        }

        protected void setCheckbox()
        {
            int numberOfColumns = CheckBoxList1.Items.Count;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                CheckBoxList1.Items[indexCount].Selected = true;
            }
        }
        protected void setColumnSelection()
        {
            int numberOfColumns = CheckBoxList1.Items.Count;
            bool columnSelect;
            for (int indexCount = 0; indexCount < numberOfColumns; indexCount++)
            {
                columnSelect = CheckBoxList1.Items[indexCount].Selected;
                DataGrid1.Columns[indexCount].Visible = columnSelect;
            }
        }
        protected void Checkbox_Change(object sender, EventArgs e)
        {
            setColumnSelection();
        }


        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    GridView1.Columns[0].Visible = false;
        //}

        //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    GridView1.Columns[1].Visible = true;
        //}

        //protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        //{
        //    GridView1.Columns[2].Visible = true;
        //}
    }
}