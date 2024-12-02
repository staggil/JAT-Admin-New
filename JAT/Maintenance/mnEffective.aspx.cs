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
    public class userDetails
    {
        public string effdate;
        public string expdate;

    }
    public partial class mnEffective : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
		protected void Page_Load(object sender, EventArgs e)
        {
            save.Visible = false;
            cancel.Visible = false;
            if (!IsPostBack)
            {
                LoadSubject();
                Loadpage();
                PopulateGridview2();
                PopulateGridview();
                PopulateGridview3();
            }
            //if (drpEffID.SelectedValue == "0") 
            //{
            //    if (!IsPostBack)
            //    {
            //        LoadSubject();
            //        PopulateGridview2();
            //        PopulateGridview();
            //        PopulateGridview3();
            //    }
            //}

        }
        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }
        //protected override void InitializeCulture()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("");
        //}
        private void Loadpage()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    if (drpEffID.SelectedIndex >= 0)
                    {
                        con.Open();
                        int id = Convert.ToInt32(drpEffID.SelectedValue);
                        SqlCommand cmd = new SqlCommand("Select EffectiveDate,ExpireDate FROM tblEffective where EffectiveID = " + id, con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        effdate.Value = dt.Rows[0]["EffectiveDate"].ToString().Split(' ')[0];
                        expdate.Value = dt.Rows[0]["ExpireDate"].ToString().Split(' ')[0];
                        PopulateGridview2();
                        PopulateGridview();
                        PopulateGridview3();
                        con.Close();
                    }
                    else
                        PopulateGridview2();
                    PopulateGridview();
                    PopulateGridview3();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
        }
        private void LoadSubject()
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    SqlDataAdapter adapter2 = new SqlDataAdapter("select EffectiveID, EffectiveID AS EffectiveIDVal from tblEffective order by EffectiveID", con);
                    adapter2.Fill(subjects);

                    drpEffID.DataSource = subjects;
                    //drplstMemberType.DataTextField = "MemberType";
                    drpEffID.DataValueField = "EffectiveID";
                    drpEffID.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            //drpEffID.Items.Insert(0, new ListItem("--Select--", "0"));
            //drpEffID.Items.Insert(1, new ListItem("1", "1"));
        }

        protected void drpEffID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable subjects = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {

                try
                {
                    if (drpEffID.SelectedIndex >= 0)
                    {
                        con.Open();
                        int id = Convert.ToInt32(drpEffID.SelectedValue);
                        SqlCommand cmd = new SqlCommand("Select EffectiveDate,ExpireDate FROM tblEffective where EffectiveID = " + id, con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        effdate.Value = dt.Rows[0]["EffectiveDate"].ToString().Split(' ')[0];
                        expdate.Value = dt.Rows[0]["ExpireDate"].ToString().Split(' ')[0];
                        PopulateGridview2();
                        PopulateGridview();
                        PopulateGridview3();
                        con.Close();
                    }
                    else
                        PopulateGridview2();
                    PopulateGridview();
                    PopulateGridview3();
                }
                catch (Exception ex)
                {
                    // Handle the error
                }

            }
            //drpEffID.Items.Insert(0, new ListItem("--Any--", "0"));
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {
                con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM SCompanyFee where EffectiveID = '" + drpEffID.SelectedValue.ToString() + "'", con);
                //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM SCompanyFee where EffectiveID = '1'", con);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                gvPhoneBook.DataSource = dtbl;
                gvPhoneBook.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                gvPhoneBook.DataSource = dtbl;
                gvPhoneBook.DataBind();
                gvPhoneBook.Rows[0].Cells.Clear();
                gvPhoneBook.Rows[0].Cells.Add(new TableCell());
                gvPhoneBook.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvPhoneBook.Rows[0].Cells[0].Text = "No Data Found ..!";
                gvPhoneBook.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }
        void PopulateGridview2()
        {
            DataTable dtbl = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {
                con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT distinct * FROM sMemberEntranceFee where EffectiveID = '" + drpEffID.SelectedValue.ToString() + "' ORDER BY EffectiveID", con);
                //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM SCompanyFee where EffectiveID = '1'", con);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                gvEntr.DataSource = dtbl;
                gvEntr.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                gvEntr.DataSource = dtbl;
                gvEntr.DataBind();
                gvEntr.Rows[0].Cells.Clear();
                gvEntr.Rows[0].Cells.Add(new TableCell());
                gvEntr.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvEntr.Rows[0].Cells[0].Text = "No Data Found ..!";
                gvEntr.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }
        void PopulateGridview3()
        {
            DataTable dtbl = new DataTable();
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
            {
                con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM SMemberType where EffectiveID = '" + drpEffID.SelectedValue.ToString() + "'", con);
                //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM SCompanyFee where EffectiveID = '1'", con);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                memfeeDes.DataSource = dtbl;
                memfeeDes.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                memfeeDes.DataSource = dtbl;
                memfeeDes.DataBind();
                memfeeDes.Rows[0].Cells.Clear();
                memfeeDes.Rows[0].Cells.Add(new TableCell());
                memfeeDes.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                memfeeDes.Rows[0].Cells[0].Text = "No Data Found ..!";
                memfeeDes.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        //protected void gvPhoneBook_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName.Equals("AddNew"))
        //        {
        //            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
        //            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
        //            {
        //                con.Open();
        //                string query = "INSERT INTO SCompanyFee (CFeeID,CTotalPerMonth,mem_fee,news_fee,EffectiveID) VALUES (@CFeeID,@CTotalPerMonth,@mem_fee,@news_fee,'"+ drpEffID.SelectedIndex + "')";
        //                SqlCommand sqlCmd = new SqlCommand(query, con);
        //                sqlCmd.Parameters.AddWithValue("@CTotalPerMonth", (gvPhoneBook.FooterRow.FindControl("txtCTotalPerMonthFooter") as TextBox).Text.Trim());
        //                sqlCmd.Parameters.AddWithValue("@mem_fee", (gvPhoneBook.FooterRow.FindControl("txtmem_feeFooter") as TextBox).Text.Trim());
        //                sqlCmd.Parameters.AddWithValue("@news_fee", (gvPhoneBook.FooterRow.FindControl("txtnews_feeFooter") as TextBox).Text.Trim());
        //                sqlCmd.Parameters.AddWithValue("@CFeeID", (gvPhoneBook.FooterRow.FindControl("txtCFeeIDFooter") as TextBox).Text.Trim());
        //                sqlCmd.ExecuteNonQuery();
        //                PopulateGridview();
        //                //lblSuccessMessage.Text = "New Record Added";
        //                //lblErrorMessage.Text = "";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblSuccessMessage.Text = "";
        //        //lblErrorMessage.Text = ex.Message;
        //    }
        //}

        protected void gvPhoneBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPhoneBook.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void gvPhoneBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPhoneBook.EditIndex = -1;
            PopulateGridview();
        }

        protected void gvPhoneBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
                var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
                using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
                {
                    con.Open();
                    string query = "UPDATE SCompanyFee SET CTotalPerMonth=@CTotalPerMonth,mem_fee=@mem_fee,news_fee=@news_fee,CFeeID=@CFeeID WHERE CFeeID = @id AND EffectiveID = '" + drpEffID.SelectedValue.ToString() + "'";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.Parameters.AddWithValue("@CTotalPerMonth", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtCTotalPerMonth") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@mem_fee", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtmem_fee") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@news_fee", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtnews_fee") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CFeeID", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtCFeeID") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                    try
                    {
						sqlCmd.ExecuteNonQuery();
						string activityDetail = $"Changed value in a table 'SCompanyFee' where CFeeID is {Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString())} AND EffectiveID is {drpEffID.SelectedValue} successful (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}

					catch (SqlException ex)
                    {
						string activityDetail = $"Changed value in a table 'SCompanyFee' where CFeeID is {Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString())} AND EffectiveID is {drpEffID.SelectedValue} unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}

					catch (Exception ex)
					{
						string activityDetail = $"Changed value in a table 'SCompanyFee' where CFeeID is {Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString())} AND EffectiveID is {drpEffID.SelectedValue} unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}

					gvPhoneBook.EditIndex = -1;
                    PopulateGridview();
                    //lblSuccessMessage.Text = "Selected Record Updated";
                    //lblErrorMessage.Text = "";
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                //lblSuccessMessage.Text = "";
                //lblErrorMessage.Text = ex.Message;
            }
        }

        //protected void gvPhoneBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
        //        using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
        //        {
        //            con.Open();
        //            string query = "DELETE FROM SCompanyFee WHERE CFeeID = @id";
        //            SqlCommand sqlCmd = new SqlCommand(query, con);
        //            sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
        //            sqlCmd.ExecuteNonQuery();
        //            PopulateGridview();
        //            //lblSuccessMessage.Text = "Selected Record Deleted";
        //            //lblErrorMessage.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblSuccessMessage.Text = "";
        //        //lblErrorMessage.Text = ex.Message;
        //    }
        //}

        protected void gvEntr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        //protected void gvEntr_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName.Equals("AddNew"))
        //        {
        //            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
        //            using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
        //            {
        //                con.Open();
        //                string query = "INSERT INTO sMemberEntranceFee (EffectiveID,FirstMember,SecondMember) VALUES ('" + drpEffID.SelectedIndex + "',@FirstMember,@SecondMember)";
        //                SqlCommand sqlCmd = new SqlCommand(query, con);
        //                sqlCmd.Parameters.AddWithValue("@FirstMember", (gvEntr.FooterRow.FindControl("txtFirstMemberFooter") as TextBox).Text.Trim());
        //                sqlCmd.Parameters.AddWithValue("@SecondMember", (gvEntr.FooterRow.FindControl("txtSecondMemberFooter") as TextBox).Text.Trim());
        //                sqlCmd.ExecuteNonQuery();
        //                PopulateGridview2();
        //                //lblSuccessMessage.Text = "New Record Added";
        //                //lblErrorMessage.Text = "";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblSuccessMessage.Text = "";
        //        //lblErrorMessage.Text = ex.Message;
        //    }
        //}

        protected void gvEntr_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEntr.EditIndex = e.NewEditIndex;
            PopulateGridview2();
        }

        protected void gvEntr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEntr.EditIndex = -1;
            PopulateGridview2();
        }

        protected void gvEntr_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
                var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
                using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
                {
                    con.Open();
                    string query = "UPDATE sMemberEntranceFee SET EffectiveID=@EffectiveID,FirstMember=@FirstMember,SecondMember=@SecondMember WHERE EffectiveID = @id AND EffectiveID = '" + drpEffID.SelectedValue.ToString() + "'";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.Parameters.AddWithValue("@EffectiveID", (gvEntr.Rows[e.RowIndex].FindControl("txtEffectiveID") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@FirstMember", (gvEntr.Rows[e.RowIndex].FindControl("txtFirstMember") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@SecondMember", (gvEntr.Rows[e.RowIndex].FindControl("txtSecondMember") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvEntr.DataKeys[e.RowIndex].Value.ToString()));
                    //sqlCmd.ExecuteNonQuery();
					try
					{
						sqlCmd.ExecuteNonQuery();
						string activityDetail = $"Changed value in a table 'sMemberEntranceFee' where EffectiveID is {Convert.ToInt32(gvEntr.DataKeys[e.RowIndex].Value.ToString())} AND EffectiveID is {drpEffID.SelectedValue.ToString()} successful (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Changed value in a table 'sMemberEntranceFee' where EffectiveID is {Convert.ToInt32(gvEntr.DataKeys[e.RowIndex].Value.ToString())} AND EffectiveID is {drpEffID.SelectedValue.ToString()} unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Changed value in a table 'sMemberEntranceFee' where EffectiveID is {Convert.ToInt32(gvEntr.DataKeys[e.RowIndex].Value.ToString())} AND EffectiveID is {drpEffID.SelectedValue.ToString()} unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					gvEntr.EditIndex = -1;
                    PopulateGridview2();
                    //lblSuccessMessage.Text = "Selected Record Updated";
                    //lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                //lblSuccessMessage.Text = "";
                //lblErrorMessage.Text = ex.Message;
            }
        }

        protected void add_Click(object sender, EventArgs e)
        {
            add.Visible = false;
            save.Visible = true;
            cancel.Visible = true;
            effdate.Disabled = false;
            expdate.Disabled = false;
            drpEffID.Enabled = false;
            drpEffID.SelectedValue = "1";
            effdate.Value = "";
            expdate.Value = "";
            PopulateGridview();
            PopulateGridview2();
            PopulateGridview3();

        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            PopulateGridview();
            PopulateGridview2();
            PopulateGridview3();
            add.Visible = true;
            save.Visible = false;
            cancel.Visible = false;

            effdate.Value = "";
            expdate.Value = "";
            effdate.Disabled = true;
            expdate.Disabled = true;
            drpEffID.Enabled = true;
        }

        protected void memfeeDes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            memfeeDes.EditIndex = e.NewEditIndex;
            PopulateGridview3();
        }

        protected void memfeeDes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            memfeeDes.EditIndex = -1;
            PopulateGridview3();
        }

        protected void memfeeDes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
                var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
                using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
                {
                    con.Open();
                    string query = "UPDATE SMemberType SET MemberType=@MemberType,Description=@Description,Newsletter=@Newsletter WHERE MemberType = @id AND EffectiveID = '" + drpEffID.SelectedValue.ToString() + "'";
                    //string query = "update SMemberType set SMemberType.MemberType = '97' from(select ROW_NUMBER() over(order by MemberType) as row,* from SMemberType where EffectiveID = '2') r  where SMemberType.MemberType = r.MemberType and SMemberType.EffectiveID = r.EffectiveID and r.row = @id; ";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.Parameters.AddWithValue("@MemberType", (memfeeDes.Rows[e.RowIndex].FindControl("txtMemberType") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Description", (memfeeDes.Rows[e.RowIndex].FindControl("txtDescription") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Newsletter", (memfeeDes.Rows[e.RowIndex].FindControl("txtNewsletter") as TextBox).Text.Trim());
                    //sqlCmd.Parameters.AddWithValue("@EffectiveID", (memfeeDes.Rows[e.RowIndex].FindControl("txtEffectiveID") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", (memfeeDes.DataKeys[e.RowIndex].Value.ToString()));
					//sqlCmd.ExecuteNonQuery();
					try
					{
						sqlCmd.ExecuteNonQuery();
						string activityDetail = $"Changed value in a table 'SMemberType' where MemberType is {memfeeDes.DataKeys[e.RowIndex].Value.ToString()} and EffectiveID is {drpEffID.SelectedValue.ToString()} successful (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (SqlException ex)
					{
						string activityDetail = $"Changed value in a table 'SMemberType' where MemberType is {memfeeDes.DataKeys[e.RowIndex].Value.ToString()} and EffectiveID is {drpEffID.SelectedValue.ToString()} unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					catch (Exception ex)
					{
						string activityDetail = $"Changed value in a table 'SMemberType' where MemberType is {memfeeDes.DataKeys[e.RowIndex].Value.ToString()} and EffectiveID is {drpEffID.SelectedValue.ToString()} unsuccessful [{ex.Message}] (User id = '{staffID}')";
						logActivity.LogStaffActivity(staffID, activityDetail);
					}
					memfeeDes.EditIndex = -1;
                    PopulateGridview3();
                    //lblSuccessMessage.Text = "Selected Record Updated";
                    //lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                //lblSuccessMessage.Text = "";
                //lblErrorMessage.Text = ex.Message;
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;
			try
            {
                connection();
                string max = "(SELECT MAX( EffectiveID )+1 FROM tblEffective)";
                cmd = new SqlCommand("SET dateformat dmy INSERT INTO dbo.tblEffective VALUES (" + max + ", '" + effdate.Value.ToString() + "', '" + expdate.Value.ToString() + "')", conn);
                conn.Open();
                int k = cmd.ExecuteNonQuery();
                conn.Close();

                string InsertSQL = "INSERT INTO dbo.SCompanyFee (CFeeID, CTotalPerMonth, mem_fee, news_fee, EffectiveID) VALUES (@CFeeID,@CTotalPerMonth,@mem_fee,@news_fee,(SELECT MAX( EffectiveID ) FROM tblEffective))";
                SqlCommand vlozSQL = new SqlCommand(InsertSQL, conn);
                string activityDetail1 = "";
                conn.Open();
                for (int i = 1; i <= 15; i++)
                {
                    string val = "0";
                    vlozSQL.Parameters.AddWithValue("@CFeeID", i);
                    vlozSQL.Parameters.AddWithValue("@CTotalPerMonth", val);
                    vlozSQL.Parameters.AddWithValue("@mem_fee", val);
                    vlozSQL.Parameters.AddWithValue("@news_fee", val);
                    
                    try
                    {
						vlozSQL.ExecuteNonQuery();
                        activityDetail1 = $"Added new value into a table 'SCompanyFee' successful (user id = '{staffID}')";
					}
                    catch (SqlException ex)
                    {
						activityDetail1 = $"Added new value into a table 'SCompanyFee' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						break;
					}
					catch (Exception ex)
					{
						activityDetail1 = $"Added new value into a table 'SCompanyFee' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						break;
					}

					vlozSQL.Parameters.Clear();
                }
				logActivity.LogStaffActivity(staffID, activityDetail1);
				conn.Close();

                string activityDetail2 = "";
                string InsertSQL2 = "INSERT INTO dbo.SMemberEntranceFee (EffectiveID, FirstMember, SecondMember) VALUES ((SELECT MAX( EffectiveID ) FROM tblEffective),@FirstMember,@SecondMember)";
                SqlCommand vlozSQL2 = new SqlCommand(InsertSQL2, conn);
                conn.Open();
                string val2 = "0";
                vlozSQL2.Parameters.AddWithValue("@FirstMember", val2);
                vlozSQL2.Parameters.AddWithValue("@SecondMember", val2);
                try
                {
					vlozSQL2.ExecuteNonQuery();
                    activityDetail2 = $"Added new value into a table 'SMemberEntranceFee' successful (user id = '{staffID}')";
                    
				}
                catch (SqlException ex)
                {
                    activityDetail2 = $"Added new value into a table 'SMemberEntranceFee' unsuccessful [{ex.Message}] (user id = '{staffID}')";
					
				}
				catch (Exception ex)
				{
					activityDetail2 = $"Added new value into a table 'SMemberEntranceFee' unsuccessful [{ex.Message}] (user id = '{staffID}')";

				}
				logActivity.LogStaffActivity(staffID, activityDetail2);
				conn.Close();

                string InsertSQL3 = "INSERT INTO dbo.SMemberType (MemberType, Description, Newsletter, EffectiveID) VALUES (@MemberType,@Description,@Newsletter,(SELECT MAX( EffectiveID ) FROM tblEffective))";
                SqlCommand vlozSQL3 = new SqlCommand(InsertSQL3, conn);

                conn.Open();
                string activityDetail3 = "";
                string[] member = { "1", "3A", "3B", "4", "6A","6B", "7" };
                //string[] des = { "普通会員", "準会員", "準会員", "名誉会員", "会友会員", "会友会員" };
                string[] des = { "普通会員", "準会員", "準会員", "名誉会員", "", "", "会友会員" };
                var numbersAndWords = member.Zip(des, (n, w) => new { Member = n, Des = w });
                foreach (var i in numbersAndWords)
                {
                    string val3 = "0";
                    vlozSQL3.Parameters.AddWithValue("@MemberType", i.Member);
                    vlozSQL3.Parameters.AddWithValue("@Description", i.Des);
                    vlozSQL3.Parameters.AddWithValue("@Newsletter", val3);
					try
					{
						vlozSQL3.ExecuteNonQuery();
						activityDetail3 = $"Added new value into a table 'SMemberType' successful (user id = '{staffID}')";
						
					}
					catch (SqlException ex)
					{
						activityDetail3 = $"Added new value into a table 'SMemberType' unsuccessful [{ex.Message}] (user id = '{staffID}')";
                        break;
					}
					catch (Exception ex)
					{
						activityDetail3 = $"Added new value into a table 'SMemberType' unsuccessful [{ex.Message}] (user id = '{staffID}')";
						break;
					}
					vlozSQL3.Parameters.Clear();
                }
				logActivity.LogStaffActivity(staffID, activityDetail3);
				conn.Close();
                string activityDetail4 = "";
                if (k != 0)
                {
                    add.Visible = true;
                    effdate.Value = "";
                    expdate.Value = "";
                    effdate.Disabled = true;
                    expdate.Disabled = true;
                    drpEffID.Enabled = true;
                    string script1 = "alert(\"Record Inserted Succesfully into the Database\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script1, true);
                    activityDetail4 = $"Added new value into a table 'tblEffective' successful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail4);
					Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    cancel.Visible = true;
                    save.Visible = true;
                    string script2 = "alert(\"Noo\");";
					activityDetail4 = $"Added new value into a table 'tblEffective' unsuccessful (User id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail4);
					ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script2, true);
					
				}

            }
            catch (Exception ex)
            {

            }
        }


        //protected void gvEntr_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
        //        using (SqlConnection con = new SqlConnection(connectionStr.ConnectionString))
        //        {
        //            con.Open();
        //            string query = "DELETE FROM sMemberEntranceFee WHERE EffectiveID = @id";
        //            SqlCommand sqlCmd = new SqlCommand(query, con);
        //            sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvEntr.DataKeys[e.RowIndex].Value.ToString()));
        //            sqlCmd.ExecuteNonQuery();
        //            PopulateGridview2();
        //            //lblSuccessMessage.Text = "Selected Record Deleted";
        //            //lblErrorMessage.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblSuccessMessage.Text = "";
        //        //lblErrorMessage.Text = ex.Message;
        //    }
        //}
    }
}