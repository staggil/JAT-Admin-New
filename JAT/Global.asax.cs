using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Globalization;

namespace JAT
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (HttpContext.Current.Session != null)
            {
                if (context.Session["language"] != null)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(context.Session["language"].ToString().Trim());
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(context.Session["language"].ToString().Trim());
                }
            }
        }
    }
}