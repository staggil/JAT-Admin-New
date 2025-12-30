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
using JAT.Core;


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

        private PrivateChildRepository _repository = new PrivateChildRepository();


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
        /* unused 2 methods cause PrivateChildRepository.cs did instead
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
        protected void BindData()
        {
            var repository = new PrivateChildRepository();
            var data = repository.GetPrivateMember(showfristMem);

            if (data != null && data.Rows.Count > 0)
            {
                Label1.Text = data.Rows[0][0].ToString(); // nameJ
                Label2.Text = data.Rows[0][1].ToString(); // CONCAT(prefixNm, nameE)
                updateBy.Text = string.IsNullOrEmpty(data.Rows[0][2].ToString()) ? "N/A" : data.Rows[0][2].ToString(); // staffFName
            }
            else
            {
                updateBy.Text = "N/A";
            }
        }



        protected void memberTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntry.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void familyTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryMember.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void paymentTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateEntryPayment.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void cancelTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateCancel.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void specialTab_Click(object sender, EventArgs e)
        {
            if (showMem != null || showfristMem != null)
            {
                Response.Redirect("privateFeature.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        protected void showInGrid()
        {
            var repository = new PrivateChildRepository();
            var td = repository.GetChildrenByMemberId(showfristMem);

            GridView1.DataSource = td;
            GridView1.DataBind();
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
            //Response.Redirect("privateEntryKid.aspx?mode=add&firstmemberid=" + showfirstMem);
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            DisabledForm();
            Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void GridView_Button_Click(object sender, EventArgs e)
        {
            EnabledForm();
            saveBtn.Visible = false;
            updateBtn.Visible = true;

            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            string childId = row.Cells[0].Text;
            HiddenField1.Value = childId;

            var repository = new PrivateChildRepository();
            var data = repository.GetChildrenByMemberId(showfristMem);

            // หา row เด็กที่ตรงกับ childId
            var childRow = data.AsEnumerable().FirstOrDefault(r => r["childid"].ToString() == childId);

            if (childRow != null)
            {
                string preNmChild = childRow["prefixKid"].ToString();
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

                Box1.Value = childRow["nameKidJ"].ToString();
                Box2.Value = childRow["nameKidE"].ToString();
                Box3.Value = childRow["birthDate"].ToString();
            }
        }


        protected void saveBtn_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                var preFixCho = RadioButtonList1.SelectedValue.ToString();
                var nameKIDJinput = Box1.Value.ToString();
                var nameKIDEinput = Box2.Value.ToString();
                var birthDateinput = Box3.Value.ToString();

                var repository = new PrivateChildRepository();

                try
                {
                    repository.AddChild(showfristMem, nameKIDJinput, nameKIDEinput, birthDateinput, preFixCho, staffID, toDayDate);

                    string activityDetail = $"Added new data into a table 'PrivateChild' successful (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                catch (Exception ex)
                {
                    string activityDetail = $"Added new data into a table 'PrivateChild' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }

                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }


        protected void GridView_Delete_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            GridViewRow row = (GridViewRow)(sender as ImageButton).NamingContainer;
            string childId = row.Cells[0].Text;

            var repository = new PrivateChildRepository();

            try
            {
                repository.SoftDeleteChild(childId);

                string activityDetail = $"Soft deleted data in a table 'PrivateChild' where childid is '{childId}' successful (user id = {staffID})";
                logActivity.LogStaffActivity(staffID, activityDetail);
            }
            catch (Exception ex)
            {
                logActivity.LogStaffActivity(staffID, $"ERROR at {ex.StackTrace} {ex.Message}");
            }

            Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
            Context.ApplicationInstance.CompleteRequest();
        }


        protected void updateBtn_Click(object sender, EventArgs e)
        {
            var uid = Session["UID"];
            int staffID = uid != null ? Convert.ToInt32(uid) : 0;

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                // ดึงค่าจากฟอร์ม
                var preFixCho = RadioButtonList1.SelectedValue.ToString();
                var nameKIDJinput = Box1.Value.ToString();
                var nameKIDEinput = Box2.Value.ToString();
                var birthDateinput = Box3.Value.ToString();
                string childId = HiddenField1.Value;

                var repository = new PrivateChildRepository();

                try
                {
                    repository.UpdateChild(childId, nameKIDJinput, nameKIDEinput, birthDateinput, preFixCho, staffID, dateToCancel);

                    string activityDetail = $"Changed new data into a table 'PrivateChild' where childid is '{childId}' successful (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }
                catch (Exception ex)
                {
                    string activityDetail = $"Changed new data into a table 'PrivateChild' where childid is '{childId}' unsuccessful [{ex.Message}] (User id = '{staffID}')";
                    logActivity.LogStaffActivity(staffID, activityDetail);
                }

                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Redirect("privateEntryKid.aspx?firstmemberid=" + showfristMem);
                Context.ApplicationInstance.CompleteRequest();
            }
        }



    }
}