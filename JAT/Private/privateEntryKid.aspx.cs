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
using CrystalDecisions.Shared;

namespace JAT.Private
{
    public partial class privateEntryKid : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
		//public string postBackId;

		private string showfristMem;
        private string showMem;
        string addValue;

        string dateToCancel;

        string toDayDate = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss", new CultureInfo("en-US"));
        string toDayDateSh = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
        protected void Page_Load(object sender, EventArgs e)
        {
            dateToCancel = DateTime.Now.ToString("dd/MM/yyyy");

            updateBtn.Visible = false;


            showfristMem = Request.QueryString["firstmemberid"];
            showMem = Request.QueryString["memberid"];

            addValue = Request.QueryString["mode"];

            if (addValue == "add")
            {
                EnabledForm();
                saveBtn.Visible = true;

            }
            else
            {
                DisabledForm();
                saveBtn.Visible = false;

            }

            if (showMem != null || showfristMem != null)
            {
                if (!Page.IsPostBack)
                {
                    BindData();
                    showInGrid();
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

        protected void BindData()
        {
            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT nameJ, CONCAT(prefixNm, nameE) ,ss.staffFName " +
                "FROM PrivateDetail pd " +
                "inner join PrivateChild pc on pd.memberid = pc.memberid " +
                "LEFT OUTER JOIN SStaff ss ON pc.updatedBy = ss.staffID " +
                "WHERE pc.memberid = '"+ showfristMem + "' ";
            //string sqlBrithPlace = "SELECT birthPlace FROM PrivateDetail WHERE memberid = '" + companyId +"'";
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
                    updateBy.Text = rd.GetValue(2).ToString();
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

            //Box2.Value = showVal;


            conn.Close();
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

        protected void specialTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateFeature.aspx?firstmemberid=" + showfristMem);
            }
        }
        protected void showInGrid()
        {
            DataTable td;

            // *** 2024-09-09 10.04am : Toon Jiradej.K have revise code
            #region 'The old query dosn't soft delete support'
            //td = SelectSqlTable("SELECT t1.childid, t1.prefixKid, t1.nameKidJ, t1.nameKidE, FORMAT(t1.birthdate, 'dd/MM/yyyy') AS birthDate " +
            //                    "FROM PrivateChild t1 " +
            //                    "INNER JOIN PrivateDetail t2 ON t1.memberid = t2.memberid WHERE t2.firstmemberid =" + "'" + showfristMem + "'");
            #endregion

            #region 'The old query has soft delete supported'
            td = SelectSqlTable($"SELECT t1.childid, t1.prefixKid, t1.nameKidJ, t1.nameKidE, " +
                                $"FORMAT(t1.birthdate, 'dd/MM/yyyy') AS birthDate " +
                                $"FROM PrivateChild t1 " +
                                $"INNER JOIN PrivateDetail t2 ON t1.memberid = t2.memberid " +
                                $"WHERE t2.firstmemberid = '{showfristMem}' AND t1.Deleted_at IS NULL");
            #endregion

            // *** End Of Revised
            //GridView1.Columns[0].Visible = false;
            GridView1.DataSource = td;
            //ImageButton1.Visible = true;
            //ImageButton2.Visible = true;

            GridView1.DataBind();
            conn.Close();

        }

        private void EnabledForm()
        {
            addBTN.Visible = false;


            //saveBtn.Visible = true;
            cancelBtn.Visible = true;

            Box1.Attributes.Remove("disabled");
            Box2.Attributes.Remove("disabled");
            Box3.Attributes.Remove("disabled");
            RadioButtonList1.Enabled = true;

            RequiredFieldValidator1.Visible = true;
            RequiredFieldValidator2.Visible = true;
            RequiredFieldValidator3.Visible = true;

        }

        private void DisabledForm()
        {
            addBTN.Visible = true;

            saveBtn.Visible = false;
            cancelBtn.Visible = false;

            Box1.Attributes.Add("disabled", "disabled");
            Box2.Attributes.Add("disabled", "disabled");
            Box3.Attributes.Add("disabled", "disabled");
            RadioButtonList1.Enabled = false;

            RequiredFieldValidator1.Visible = false;
            RequiredFieldValidator2.Visible = false;
            RequiredFieldValidator3.Visible = false;

        }

        protected void addBTN_Click(object sender, EventArgs e)
        {
            EnabledForm();
            saveBtn.Visible = true;

            //Response.Redirect("privateEntryKid.aspx?mode=add");
            //Response.Redirect("privateEntryKid.aspx?mode=add&firstmemberid=" + showfristMem);
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            DisabledForm();
            Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);

        }

        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            EnabledForm();
            saveBtn.Visible = false;
            updateBtn.Visible = true;

            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;


            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            HiddenField1.Value = row.Cells[0].Text;

            connection();
            SqlCommand sc;
            SqlDataReader rd;

            string sql = "SELECT t1.childid, t1.prefixKid, t1.nameKidJ, t1.nameKidE, FORMAT(t1.birthdate, 'dd/MM/yyyy') AS birthDate " +
                            "FROM PrivateChild t1 " +
                            "INNER JOIN PrivateDetail t2 ON t1.memberid = t2.memberid WHERE t1.childid = " + "'" + row.Cells[0].Text + "'"  ;


            try
            {
                conn.Open();
                sc = new SqlCommand(sql, conn);
                rd = sc.ExecuteReader();


                while (rd.Read())
                {

                    string preNmChild = rd.GetValue(1).ToString();
                    if (preNmChild == "Boy")
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }
                    else if (preNmChild == "Girl")
                    {
                        RadioButtonList1.SelectedIndex = 1;
                    }
                    else
                    {
                        RadioButtonList1.ClearSelection();
                    }

                    Box1.Value = rd.GetValue(2).ToString();
                    Box2.Value = rd.GetValue(3).ToString();
                    Box3.Value = rd.GetValue(4).ToString();
                   
                }

            }
            catch { }

            conn.Close();

        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;

                //var chkBirth = Box7.Checked;
                var preFixCho = RadioButtonList1.SelectedValue.ToString();
                var nameKIDJinput = Box1.Value.ToString();
                var nameKIDEinput = Box2.Value.ToString();
                var birthDateinput = Box3.Value.ToString();

                //string removeDateDefault = "";

                //var preFixCho = preFixSel.SelectedValue.ToString();

                try
                {
					td = SelectSqlTable("SET dateformat dmy INSERT INTO PrivateChild(memberid, nameKidJ, nameKidE, birthdate, prefixKid,updatedBy,updatedDate)" +
										"VALUES('" + showfristMem + "'" + "," + "N'" + nameKIDJinput + "'" + "," + "N'" + nameKIDEinput + "'" + "," + "'" + birthDateinput + "'" + "," + "'" + preFixCho + "'" + "," + Session["UID"] + "," + "'" + toDayDate + "'" + ")");
					string activityDetail = $"Added new data into a table 'PrivateChild' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
                {
					string activityDetail = $"Added new data into a table 'PrivateChild' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Added new data into a table 'PrivateChild' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}

				//DisabledForm();
				Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
            else
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
        }

        protected void GridView_Delete_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;

            //ImageButton _myButton = (ImageButton)e.Item.FindControl("ibtn2");

            try
            {
                // *** 2024-09-06 01.49pm : Toon Jiradech.K Toon Jiradech.k have changed code from hard deleting to soft deleting
                #region 'Hard Deleting' 
                //         td = SelectSqlTable("DELETE FROM PrivateChild  " +
                //              "WHERE childid = " + "'" + row.Cells[0].Text + "'");
                #endregion

                #region 'Soft Deleting'
                td = SelectSqlTable($"UPDATE PrivateChild SET Deleted_at = GETDATE() " +
                                    $"WHERE childid = '{row.Cells[0].Text}'");
                #endregion
                // *** End of Revised

                string activityDetail = $"Soft deleted data in a table 'PrivateChild' where childid is '{row.Cells[0].Text}' successful (user id = {staffID})";
				logActivity.LogStaffActivity(staffID, activityDetail);
			}
            catch (SqlException ex)
            {
                //string activityDetail = $"Soft deleted data in a table 'PrivateChild' where childid is '{row.Cells[0].Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.LineNumber} {ex.StackTrace} " +
                    $"{ex.Message}");
            }
			catch (Exception ex)
			{
                //string activityDetail = $"Soft deleted data in a table 'PrivateChild' where childid is '{row.Cells[0].Text}' unsuccessful [{ex.Message}] (user id = {staffID})";
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
            }

			Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);

            conn.Close();

        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                DataTable td;

                //var chkBirth = Box7.Checked;
                var preFixCho = RadioButtonList1.SelectedValue.ToString();
                var nameKIDJinput = Box1.Value.ToString();
                var nameKIDEinput = Box2.Value.ToString();
                var birthDateinput = Box3.Value.ToString();


                //string removeDateDefault = "";

                //var preFixCho = preFixSel.SelectedValue.ToString();


                
				try
				{
					td = SelectSqlTable("SET dateformat dmy UPDATE PrivateChild " +
									"SET nameKidE = '" + nameKIDEinput + "'" + "," + "nameKidJ = N'" + nameKIDJinput + "'" + "," + "prefixKid = '" + preFixCho + "'" + "," + "birthdate = '" + birthDateinput + "'" + "," + "updatedDate = '" + dateToCancel + "'" + "," + "updatedBy = " + Session["UID"] +
									 "WHERE childid = '" + HiddenField1.Value + "'");
					string activityDetail = $"Changed new data into a table 'PrivateChild' where childid is '{HiddenField1.Value}' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (SqlException ex)
				{
					string activityDetail = $"Changed new data into a table 'PrivateChild' where childid is '{HiddenField1.Value}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					string activityDetail = $"Changed new data into a table 'PrivateChild' where childid is '{HiddenField1.Value}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}

				//DisabledForm();
				Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
            else
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            }
        }


    }
}