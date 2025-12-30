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
using JAT.Core;
using Microsoft.Ajax.Utilities;



namespace JAT.Private
{
    public partial class privateFeature : System.Web.UI.Page
    {
        private SqlConnection conn;
        private SqlCommand cmd;

		private LogActivity logActivity = new LogActivity();
        //public string postBackId;
        private PrivateFeatureRepository _repo = new PrivateFeatureRepository();

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


        private void lastEditor()
        {
            try
            {
                updateBy.Text = _repo.GetLastEditor(showfristMem);
            }
            catch
            {
                updateBy.Text = "N/A";
            }
        }


        protected void BindData()
        {
            string sql = "SELECT nameJ, CONCAT(prefixNm, nameE) AS fullName " +
                         "FROM PrivateDetail " +
                         "WHERE firstmemberid = @firstMemberId AND memberid = firstmemberid";

            var parameters = new Dictionary<string, object>
    {
        { "@firstMemberId", showfristMem }
    };

            try
            {
                DataTable dt = _repo.ExecuteQueryWithParams(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = dt.Rows[0]["nameJ"].ToString();
                    Label2.Text = dt.Rows[0]["fullName"].ToString();
                }
            }
            catch (Exception ex)
            {
                // log error หรือแสดงข้อความ (option)
            }
        }




        /* commentted 
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
        */



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

            // Show/hide MergeFamilyInput based on the selected radio button
            if (radVal == "Merge Family")
            {
                setMergeInputVisible(1); // Show MergeFamilyInput
            }
            else
            {
                MergeFamilyInput.Text = "";
                setMergeInputVisible(0); // Hide MergeFamilyInput
            }

            // Update the grid view visibility based on the selected radio button
            if (radVal == "Divide Family")
            {
                clrGridView(); // Clear the grid view
                showInGridCanMem();

                GridView1.Columns[0].Visible = true; // Show "divide view" column
                GridView1.Columns[1].Visible = false; // Hide "change first member" column
            }
            else if (radVal == "Change First Member")
            {
                clrGridView(); // Clear the grid view
                showInGridCanMem();

                GridView1.Columns[0].Visible = false; // Hide "divide view" column
                GridView1.Columns[1].Visible = true; // Show "change first member" column
            }
            else
            {
                clrGridView(); // Clear the grid view
            }
        }


        protected void processBtn_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            string radVal = RadioButtonList1.SelectedValue;
            string tmp = "";

            if (radVal == "Merge Family")
            {
                clrGridView();

                if (!string.IsNullOrWhiteSpace(MergeFamilyInput.Text))
                {
                    MergeFamily(); // อันนี้ค่อย refactor ต่อทีหลังได้
                    tmp = "Merge Family Completed";
                }
            }
            else if (radVal == "Divide Family")
            {
                clrGridView();
                showInGridCanMem();

                GridView1.Columns[0].Visible = true;
                GridView1.Columns[1].Visible = false;
            }
            else if (radVal == "Activate Member")
            {
                try
                {
                    _repo.ActivateMember(showfristMem);

                    logActivity.LogStaffActivity(
                        staffID,
                        $"Executed procedure name 'psActivateMember' successful (user id = '{staffID}')"
                    );

                    tmp = "Activate Member Executed";
                    Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
                    return;
                }
                catch (SqlException ex)
                {
                    logActivity.LogStaffActivity(
                        staffID,
                        $"Executed procedure name 'psActivateMember' unsuccessful [{ex.Message}] (user id = '{staffID}')"
                    );
                }
                catch (Exception ex)
                {
                    logActivity.LogStaffActivity(
                        staffID,
                        $"Executed procedure name 'psActivateMember' unsuccessful [{ex.Message}] (user id = '{staffID}')"
                    );
                }
            }
            else if (radVal == "Change First Member")
            {
                clrGridView();
                showInGridCanMem();

                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = true;
            }

            if (!string.IsNullOrEmpty(tmp))
            {
                ClientScript.RegisterStartupScript(
                this.GetType(),
                "Msgbox",
                $"alert('{tmp}');",
                true
                );
            }
        }

        protected void clrGridView()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void showInGridCanMem()
        {
            try
            {
                var dt = _repo.GetCandidateMembers(showfristMem);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                // TODO: handle error
            }

        }

        protected void GridView_Button_Divide(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            string memberId = row.Cells[2].Text.Trim();

            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            try
            {
                _repo.DivideFamily(memberId);

                logActivity.LogStaffActivity(
                    staffID,
                    $"Executed procedure psDivideFamily successful (memberId={memberId})"
                );
            }
            catch (Exception ex)
            {
                logActivity.LogStaffActivity(
                    staffID,
                    $"Executed procedure psDivideFamily failed [{ex.Message}]"
                );
            }

            Response.Redirect("privateEntry.aspx?firstmemberid=" + memberId);
        }


        protected void GridView_Button_ChangeFirst(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            string newFirstMemberId = row.Cells[2].Text.Trim();
             string memberId = row.Cells[2].Text.Trim();
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            try
            {
                // 1️⃣ เปลี่ยนหัวหน้าครอบครัว
                _repo.ChangeFirstMember(showfristMem, memberId, "3");

                // 2️⃣ ตรวจสอบ member ใน Private
                if (_repo.PrivateMemberExists(newFirstMemberId))
                {
                    _repo.UpdateMemberType(newFirstMemberId, 1);
                    _repo.UpdateSendType(newFirstMemberId, "#");
                }
                else
                {
                    logActivity.LogStaffActivity(
                        staffID,
                        $"[Warning] Cannot update sendType: memberid {newFirstMemberId} not found in Private table."
                    );
                }

                logActivity.LogStaffActivity(
                    staffID,
                    $"Executed procedure 'psChangeFirstMember' successful (new first member = {newFirstMemberId})"
                );
            }
            catch (SqlException ex)
            {
                logActivity.LogStaffActivity(
                    staffID,
                    $"Executed procedure 'psChangeFirstMember' failed (SQL): {ex.Message}"
                );
            }
            catch (Exception ex)
            {
                logActivity.LogStaffActivity(
                    staffID,
                    $"Executed procedure 'psChangeFirstMember' failed: {ex.Message}"
                );
            }

            Response.Redirect("privateEntry.aspx?firstmemberid=" + newFirstMemberId);
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
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            try
            {
                _repo.MergeFamily(showfristMem, MergeFamilyInput.Text);

                logActivity.LogStaffActivity(
                    staffID,
                    $"Merged family from '{MergeFamilyInput.Text}' to '{showfristMem}'"
                );

                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
            }
            catch (SqlException ex)
            {
                MergeFamilyInput.Text = "Error! Merge family incomplete. Member ID may be duplicated.";
                MergeFamilyInput.Visible = true;

                logActivity.LogStaffActivity(
                    staffID,
                    $"Merge family failed (SQL): {ex.Message}"
                );
            }
            catch (Exception ex)
            {
                MergeFamilyInput.Text = "Error! Merge family incomplete.";
                MergeFamilyInput.Visible = true;

                logActivity.LogStaffActivity(
                    staffID,
                    $"Merge family failed: {ex.Message}"
                );
            }
        }

    }
}