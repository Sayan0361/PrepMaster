using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Dapper;
using PrepMaster.Models;

namespace PrepMaster
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            var httpException = ex as HttpException;

            int statusCode = httpException?.GetHttpCode() ?? 500;
            try
            {
                var db = new DapperConn();
                var proc = "sp_LogError";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ErrorMessage", ex.Message);
                param.Add("@StackTrace", ex.StackTrace);
                param.Add("@ErrorProcedure", HttpContext.Current.Request.Url.ToString());
                db.ExecuteWithoutReturn(proc, param);
            }
            catch
            {
                // Prevent recursive crash if logging fails
            }

            Server.ClearError();
            if (statusCode == 404)
            {
                Response.Redirect("~/Error/NotFound");
            }
            else
            {
                Response.Redirect("~/Error/Index");
            }
        }
    }
}