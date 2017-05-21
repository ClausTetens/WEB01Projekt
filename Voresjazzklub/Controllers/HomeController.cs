using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Voresjazzklub.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            //HttpContext.Session.Add("Test", "Så kører det");
            //ViewBag.isDebugging = HttpContext.IsDebuggingEnabled?"Debugging":null;

#if DEBUG
            ViewBag.isDebugging = "Hey! We're debugging!!";
#endif
            string title = ViewBag.Title;
            //System.Web.HttpContext.Current.Session["sessionString"] = "Så kører det";
            return View();
        }

        public ActionResult About() {
            //ViewBag.Message = System.Web.HttpContext.Current.Session["sessionString"];
            ViewBag.Message = "IT's all about jazz";
            string title = ViewBag.Title;
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Upload() {
            ViewBag.Message = "Uploading????";

            return View();
        }
    }
}