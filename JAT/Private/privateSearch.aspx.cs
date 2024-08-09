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
    public partial class privateSearch : System.Web.UI.Page
    {
		private LogActivity logActivity = new LogActivity();
		private SqlConnection conn;
        private SqlCommand cmd;
        private string textInput;
        private int textInputInt;
        private int valOfTable;
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
            cmd.CommandTimeout = 6000;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Clear();

            Label3.Visible = false;

            ImageButton1.Visible = false;
            ImageButton2.Visible = false;


        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "2")
            {
                RadioButtonList1.SelectedValue = "1";
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BindData();
            }
        }

        protected void BindData()
        {
			var uid = Session["UID"];
			int staffID = uid != null ? Convert.ToInt32(uid) : 0;

			DataTable td;

            var memSatCho = DropDownList1.SelectedValue.ToString();
            var memberTypeCho = RadioButtonList1.SelectedValue.ToString();
            var textInputOpt = DropDownList2.SelectedValue.ToString();
            textInput = Text1.Value.ToString();
            var sortOpt = DropDownList3.SelectedValue.ToString();
            var sortCouOpt = DropDownList4.SelectedValue.ToString();
            string query = "";
			int queryCheck;
			string activityDetail;

			if (RadioButtonList1.SelectedValue != "4")
            {
                query = "EXEC searchPrivate '" + memSatCho + "','" + memberTypeCho + "','" + textInputOpt + "','%" + textInput + "%','" + sortOpt + "','" + sortCouOpt + "'";
                queryCheck = 1;
                
                switch (textInputOpt)
                {
                    case "":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        };break;
                    case "1":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        };break;
                    case "2":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        };break;
                    case "3":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        }; break;
                    case "4":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        }; break;
                    case "5":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        }; break;
                    case "6":
                        {
                            GridView1.Columns[4].Visible = true;
                            GridView1.Columns[5].Visible = false;
                            GridView1.Columns[6].Visible = true;
                            GridView1.Columns[7].Visible = true;
                            GridView1.Columns[8].Visible = true;
                            GridView1.Columns[9].Visible = true;
                        }; break;
                }
            }
            else
            {
                query = "IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '#TEMP1')) " +
                    "DROP TABLE #TEMP1 " +
                    "IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '#TEMP2')) " +
                    "DROP TABLE #TEMP2 " +
                    "IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '#TEMP3')) " +
                    "DROP TABLE #TEMP3 " +
                    "IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '#TEMP')) " +
                    "DROP TABLE #TEMP " +
                    "CREATE TABLE #TEMP1 (firstmemberid nvarchar(100),memberid nvarchar(100),nameEng nvarchar(100),nameJ nvarchar(100),membertype nvarchar(100),companyNm nvarchar(100),nameKidJ nvarchar(100),nameKidE nvarchar(100),familyNameJ  nvarchar(100),familyNameE nvarchar(100)) " +
                    "CREATE TABLE #TEMP2 (firstmemberid nvarchar(100),memberid nvarchar(100),nameEng nvarchar(100),nameJ nvarchar(100),membertype nvarchar(100),companyNm nvarchar(100),nameKidJ nvarchar(100),nameKidE nvarchar(100),familyNameJ  nvarchar(100),familyNameE nvarchar(100)) " +
                    "CREATE TABLE #TEMP3 (firstmemberid nvarchar(100),memberid nvarchar(100),nameEng nvarchar(100),nameJ nvarchar(100),membertype nvarchar(100),companyNm nvarchar(100),nameKidJ nvarchar(100),nameKidE nvarchar(100),familyNameJ  nvarchar(100),familyNameE nvarchar(100)) " +
                    "INSERT INTO #TEMP1 EXEC searchPrivate '" + memSatCho + "','1','" + textInputOpt + "','%" + textInput + "%','" + sortOpt + "','" + sortCouOpt + "' " +
                    "INSERT INTO #TEMP2 EXEC searchPrivate '" + memSatCho + "','2','" + textInputOpt + "','%" + textInput + "%','" + sortOpt + "','" + sortCouOpt + "' " +
                    "INSERT INTO #TEMP3 EXEC searchPrivate '" + memSatCho + "','3','" + textInputOpt + "','%" + textInput + "%','" + sortOpt + "','" + sortCouOpt + "' " +
                    "SELECT * INTO #TEMP FROM #TEMP1 UNION SELECT * FROM #TEMP2 UNION SELECT * FROM #TEMP3 " +
                    "SELECT * FROM #TEMP " +
                    "DROP TABLE #TEMP1,#TEMP2,#TEMP3,#TEMP";
                queryCheck = 2;
                GridView1.Columns[4].Visible = true;
                GridView1.Columns[5].Visible = false;
                GridView1.Columns[6].Visible = true;
                GridView1.Columns[7].Visible = true;
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[9].Visible = true;
				
			}
            
            if (queryCheck == 1)
            {
                try
                {
					td = SelectSqlTable(query);
					GridView1.DataSource = td;
					valOfTable = td.Rows.Count;
					activityDetail = $"Executed procedure name 'searchPrivate' successful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
                catch
                {
					activityDetail = $"Executed procedure name 'searchPrivate' unsuccessful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);

				}
				
			}
            else if (queryCheck == 2)
            {
                try
                {
					td = SelectSqlTable(query);
					GridView1.DataSource = td;
					valOfTable = td.Rows.Count;
					activityDetail = $"Created new table name '#TEMP1,#TEMP2,#TEMP3,#TEMP' then added a procedure result name 'searchPrivate' then get data from table '#TEMP1,#TEMP2,#TEMP3,#TEMP' before removed table '#TEMP1,#TEMP2,#TEMP3,#TEMP' successful (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
                catch (SqlException ex)
                {
					activityDetail = $"Created new table name '#TEMP1,#TEMP2,#TEMP3,#TEMP' then added a procedure result name 'searchPrivate' then get data from table '#TEMP1,#TEMP2,#TEMP3,#TEMP' before removed table '#TEMP1,#TEMP2,#TEMP3,#TEMP' unsuccessful [{ex.Message}] (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}
				catch (Exception ex)
				{
					activityDetail = $"Created new table name '#TEMP1,#TEMP2,#TEMP3,#TEMP' then added a procedure result name 'searchPrivate' then get data from table '#TEMP1,#TEMP2,#TEMP3,#TEMP' before removed table '#TEMP1,#TEMP2,#TEMP3,#TEMP' unsuccessful [{ex.Message}] (user id = '{staffID}')";
					logActivity.LogStaffActivity(staffID, activityDetail);
				}

			}

            
			

			
            
            Label1.Text = "Searching for " + "<b>" + textInput + "</b>" + " returned " + "<b>" + valOfTable + "</b>" + " people(s).";
            GridView1.DataBind();
            ImageButton2.Visible = true;
            if (GridView1.PageIndex != 0)
            {
                ImageButton1.Visible = true;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "rblchildren")
            {
                if (DropDownList2.SelectedValue == "companyNm")
                {
                    RadioButtonList1.SelectedValue = "firstMem";
                }
            }else if (RadioButtonList1.SelectedValue == "familyMem")
            {
                if (DropDownList2.SelectedValue == "companyNm")
                {
                    RadioButtonList1.SelectedValue = "firstMem";
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            GridView1.PageIndex--;
            BindData();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            GridView1.PageIndex++;
            BindData();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "firstMem")
            {
                DropDownList2.SelectedValue = "memberid";
            }
            else if (RadioButtonList1.SelectedValue == "familyMem")
            {
                DropDownList2.SelectedValue = "memberid";
            }
            else if (RadioButtonList1.SelectedValue == "rblchildren")
            {
                DropDownList2.SelectedValue = "memberid";
                DropDownList1.SelectedValue = "IN ('A', 'NA')";
            }
            else if (RadioButtonList1.SelectedValue == "rblall")
            {
                DropDownList2.SelectedValue = "memberid";
            }
        }
        protected void checkKeyValue(object sender, ServerValidateEventArgs args)
        {
            try
            {
                if (DropDownList2.SelectedValue.ToString() == "1")
                {
                    int i = int.Parse(args.Value);
                    args.IsValid = true;
                }

            }
            catch (Exception e)
            {
                args.IsValid = false;
            }

        }
    }
}