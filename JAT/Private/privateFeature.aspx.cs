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

namespace JAT.Private
{
    public partial class privateFeature : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
		//public string postBackId;

		private string showfristMem;
        private string showMem;
        string addValue;
        protected void Page_Load(object sender, EventArgs e)
        {
            showfristMem = Request.QueryString["firstmemberid"];
            showMem = Request.QueryString["memberid"];

            addValue = Request.QueryString["mode"];

            if (showMem != null || showfristMem != null)
            {
                if (!Page.IsPostBack)
                {
                    lastEditor();
                    BindData();
                }

            }
        }
        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT nameJ, CONCAT(prefixNm, nameE) " +
                            "FROM PrivateDetail " +
                            "WHERE firstmemberid = '" + showfristMem + "'" + "AND memberid = firstmemberid";
            //string sqlBrithPlace = "SELECT birthPlace FROM PrivateDetail WHERE memberid = '" + showMem +"'";
            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    //show in member information
                    Label1.Text = rd.GetValue(0).ToString();
                    Label2.Text = rd.GetValue(1).ToString();
                }

            }
            catch { }

            //Box2.Value = showVal;


            conn.Close();
        }

        private void lastEditor()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT top 1 staffFName " +
                "FROM PrivateDetail t1 " +
                "left outer join SStaff ss on t1.updatedBy = ss.staffID " +
                "WHERE firstmemberid = '" + showfristMem + "' ";

            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();

                while (rd.Read())
                {
                    updateBy.Text = rd.GetValue(0).ToString();
                }

            }
            catch { }
            finally
            {
                if (updateBy.Text.Trim() == "" || updateBy.Text.Trim() == null)
                {
                    updateBy.Text = "N/A";
                }
            }
        }

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
        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void ChildrenTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void paymentTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void cancelTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateCancel.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string radVal = RadioButtonList1.SelectedValue.ToString();
            if (radVal == "Merge Family")
            {
                setMergeInputVisible(1);
            }
            else
            {
                MergeFamilyInput.Text = "";
                setMergeInputVisible(0);
            }
        }

        protected void processBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			String tmp = "";
            string radVal = RadioButtonList1.SelectedValue.ToString();
            if (radVal == "Merge Family")
            {
                clrGridView();
                if (MergeFamilyInput.Text != "")
                {
                    MergeFamily();
                    tmp = "Merge Family Completed";
                }
            }
            else if (radVal == "Divide Family")
            {
                showInGridCanMem();
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[1].Visible = false;
            }
            else if (radVal == "Activate Member")
            {
                connection();
                string sql = "EXEC psActivateMember '" + showfristMem + "'";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                try
                {
					cmd.ExecuteNonQuery();
					string activityDetail = $"Executed procedure name 'psActivateMember' successful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
                catch (SqlException ex)
                {
					string activityDetail = $"Executed procedure name 'psActivateMember' unsuccessful [{ex.Message}] (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Executed procedure name 'psActivateMember' unsuccessful [{ex.Message}] (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				conn.Close();
                tmp = "Activate Member Executed";
                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
            }
            else
            {
                showInGridCanMem();
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = true;
            }
            if (tmp != "")
            {
                string strJavaScript = "<script language='javascript'>alert('" + tmp + "');</script>";
                Page.RegisterStartupScript("Msgbox", strJavaScript);
            }
        }
        protected void clrGridView()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        protected void showInGridCanMem()
        {
            DataTable td;

            //var memSatCho = DropDownList1.SelectedValue.ToString();
            //var memberTypeCho = RadioButtonList1.SelectedValue.ToString();


            td = SelectSqlTable("SELECT memberid, nameJ, CONCAT(prefixNm, nameE) AS nameE, FORMAT(birthDate, 'yyyy-MMM-dd') AS birthDate, FORMAT(appliedDate, 'yyyy-MMM-dd') AS appliedDate, FORMAT(cancelledDate, 'yyyy-MMM-dd') AS cancelledDate, memberType, memberStatus " +
                                "FROM PrivateDetail " +
                                "WHERE firstmemberid =" + "'" + showfristMem + "'" + "AND firstmemberid != memberid" + " AND cancelledDate IS NULL");

            GridView1.DataSource = td;

            GridView1.DataBind();

            conn.Close();

        }
        protected void GridView_Button_Divide(object sender, EventArgs e)
        {
            DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			try
			{
				td = SelectSqlTable("exec psDivideFamily '" + row.Cells[2].Text + "'");
				string activityDetail = $"Executed procedure name 'psDivideFamily' successful (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
				string activityDetail = $"Executed procedure name 'psDivideFamily' unsuccessful [{ex.Message}] (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Executed procedure name 'psDivideFamily' unsuccessful [{ex.Message}] (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			Response.Redirect("privateEntry.aspx?firstmemberid=" + row.Cells[2].Text);

        }
        protected void GridView_Button_ChangeFirst(object sender, EventArgs e)
        {
            DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			//Label3.Text = row.Cells[1].Text;

			//td = SelectSqlTable("UPDATE PrivateDetail " +
			//    "SET firstMemberID = '" + row.Cells[2].Text + "'" +
			//    "WHERE firstMemberID = '" + showfristMem + "'");

			try
			{
				td = SelectSqlTable("exec psChangeFirstMember '" + showfristMem + "','" + row.Cells[2].Text + "','3'");
				string activityDetail = $"Executed procedure name 'psChangeFirstMember' successful (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (SqlException ex)
			{
				string activityDetail = $"Executed procedure name 'psChangeFirstMember' unsuccessful [{ex.Message}] (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				string activityDetail = $"Executed procedure name 'psChangeFirstMember' unsuccessful [{ex.Message}] (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}

			Response.Redirect("privateEntry.aspx?firstmemberid=" + row.Cells[2].Text);

        }
        protected void setMergeInputVisible(int x)
        {
            if (x == 0)
            {
                MergeFamilyInput.Visible = false;
                MergeFamilyLabel.Visible = false;
                MergeFamilySpan.Visible = false;
            }
            else
            {
                MergeFamilyInput.Visible = true;
                MergeFamilyLabel.Visible = true;
                MergeFamilySpan.Visible = true;
            }
        }
        protected void MergeFamily()
        {
            DataTable td;
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
                td = SelectSqlTable("UPDATE PrivateDetail " +
                "SET firstmemberid = '" + showfristMem + "' " +
                "WHERE firstmemberid = '" + MergeFamilyInput.Text + "'"
                );
				string activityDetail = $"Changed value in a table 'PrivateDetail' where firstmemberid is '{MergeFamilyInput.Text}' successful (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
                MergeFamilyInput.Text = "Error! Merge family incomplete. Member ID may be duplicated.";
                MergeFamilyInput.Visible = true;
				string activityDetail = $"Changed value in a table 'PrivateDetail' where firstmemberid is '{MergeFamilyInput.Text}' unsuccessful [{ex.Message}] (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			catch (Exception ex)
			{
				MergeFamilyInput.Text = $"Error! Merge family incomplete. {ex.Message}.";
				MergeFamilyInput.Visible = true;
				string activityDetail = $"Changed value in a table 'PrivateDetail' where firstmemberid is '{MergeFamilyInput.Text}' unsuccessful [{ex.Message}] (user id = '{staffID}')";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
			Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);

        }
    }
}