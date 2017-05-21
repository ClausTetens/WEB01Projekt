using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Voresjazzklub.Controllers {
    public class ErrorController : Controller {
        private string sessiondUserId() {
            return Session["userId"] == null ? null : Session["userId"].ToString();
        }

        // GET: Error
        public string Index() {
            return "Error Index - Voresjazzklub";
        }

        public ActionResult NotFound() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            Response.StatusCode = 404; // så smider serveren sin egen 404-side hvis system.web redirect. Brug system.webServer
            return View();
            //return "Error NotFund - Voresjazzklub";
        }

        public ActionResult Forbidden() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            Response.StatusCode = 403;
            return View();
            //return "Error Forbidden - Voresjazzklub";
        }

        public ActionResult BadRequest() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            Response.StatusCode = 400;
            return View();
            //return "Error Forbidden - Voresjazzklub";
        }
    }
}