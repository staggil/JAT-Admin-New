using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace JAT
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        private SqlConnection conn;
        private SqlCommand cmd;
        internal Assembly assembly = Assembly.GetExecutingAssembly();
        internal AssemblyFileVersionAttribute fileVersion;

        private void connection()
        {
            var connectionStr = WebConfigurationManager.ConnectionStrings["DefaultConnection"];
            conn = new SqlConnection(connectionStr.ConnectionString);
        }

        public System.Data.DataTable SelectSqlTable(string Sqlcmd)
        {
            connection();
            var table = new System.Data.DataTable();
            string sql = Sqlcmd;
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(table);
            conn.Close();
            return table;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;

            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }

            
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.fileVersion = this.assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            this.LblVersion.Text = this.fileVersion.Version;

            CultureInfo cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            

            if (GetCurrentPageName() == "Login")
            {
                //hide sidebar
                sidebarmenu.Visible = false;
            }
            else
            {
                permissionCheck();
            }
            if (Session["requestpassword"] != null)
            {
                HyperLinkPopUp.Visible = false;
                HyperLinkFinish.Visible = true;
                PanelList.Visible = true;

            }

            //if (Session["language"] != null && !IsPostBack)
            //{
            //    DropDownList_Language.ClearSelection();
            //    DropDownList_Language.Items.FindByValue(Session["language"].ToString()).Selected = true;
            //}

        }
        protected void LinkButtonJP_Click(object sender, EventArgs e)
        {
            this.SetMyNewCulture("ja-JP");
            var posturl = "";
            if (Request.QueryString != null)
            {
                posturl = Request.Path + "?" + Request.QueryString;
            }
            else
            {
                posturl = Request.Path;
            }
            Response.Redirect(posturl);
        }

        protected void LinkButtonEN_Click(object sender, EventArgs e)
        {
            this.SetMyNewCulture("en-US");
            var posturl = "";
            if (Request.QueryString != null)
            {
                posturl = Request.Path + "?" + Request.QueryString;
            }
            else
            {
                posturl = Request.Path;
            }
            Response.Redirect(posturl);
        }
        //protected void DropDownList_Language_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (DropDownList_Language.SelectedValue)
        //    {
        //        case "en-US":
        //            this.SetMyNewCulture("en-US");
        //            break;
        //        case "ja-JP":
        //            this.SetMyNewCulture("ja-JP");
        //            break;
        //        default:
        //            break;
        //    }
        //    Response.Redirect(Request.Path);
        //}

        private void SetMyNewCulture(string culture)
        {
            Session["language"] = culture;
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
        protected void permissionCheck()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (Session["User"].ToString().Trim() == "")
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString().Trim() == "Administrator")
                {
                    //show menu
                    testhid.Disabled = false;
                    testhid.Visible = true;
                }
                else
                {
                    //normal role -> hide menu
                    testhid.Disabled = true;
                    testhid.Visible = false;
                }
            }
        }
        public string GetCurrentPageName()
        {
            string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo Info = new System.IO.FileInfo(Path);
            string pageName = Info.Name;
            return pageName;
        }
        /*   commentted for project
        protected void ButtonPopupConfirm_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Session["requestpassword"] == null)
            {
                string username = Session["User"].ToString();
                string password = TextBoxRequestPassword.Text;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + password + "')", true);
                //DataTable dt = SelectSqlTable("SELECT staffName,staffPass FROM SStaff WHERE staffName = '" + username + "' AND staffPass = '" + password + "'");
                //if (dt.Rows.Count > 0)
                //{

                //    Session["requestpassword"] = "password";
                //    HyperLinkPopUp.Visible = true;
                //    HyperLinkFinish.Visible = false;
                //    PanelList.Visible = false;
                //    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                //}
                //else
                //{
                //    LabelCheckPassword.Visible = true;
                //}

                DataTable dt = SelectSqlTable("SELECT * FROM Maintenance_Password WHERE password = '" + password + "' ");
                if (dt.Rows.Count > 0)
                {

                    Session["requestpassword"] = "password";
                    HyperLinkPopUp.Visible = true;
                    HyperLinkFinish.Visible = false;
                    PanelList.Visible = false;
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    LabelCheckPassword.Visible = true;
                }
            }
            else
            {
                HyperLinkPopUp.Visible = false;
                HyperLinkFinish.Visible = true;
                PanelList.Visible = true;
            }

        }
        */
    }

}