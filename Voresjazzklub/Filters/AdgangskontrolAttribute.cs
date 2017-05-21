using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Voresjazzklub.Filters {
    public class AdgangskontrolAttribute : ActionFilterAttribute {
        private string sessiondUserId() {
            //return Session["userId"] == null ? null : Session["userId"].ToString();
            return string.Empty;
        }
        private void sessiondUserId(string userId) {
            //Session["userId"] = userId;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            string bruger = "";
            IIdentity ident = filterContext.HttpContext.User.Identity;
            if(filterContext.HttpContext.Session["userId"] != null) {
                bruger = filterContext.HttpContext.Session["userId"].ToString();
            }
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext) {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext) {
            Log("OnResultExecuting ", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData) {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0}- controller:{1} action:{2}", methodName,
                                                                        controllerName,
                                                                        actionName);
            Debug.WriteLine(message);
        }
    }
}