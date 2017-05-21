using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Models;

namespace Voresjazzklub.Controllers
{
    public class JazzHomeController : Controller
    {
        private string sessiondUserId() {
            return Session["userId"] == null ? "" : Session["userId"].ToString();
        }
        private string lastLogon() {
            return Session["lastlogon"] == null ? "???" : Session["lastlogon"].ToString();
        }
  
        // GET: JazzHome
        public ActionResult Index()
        {
            if(sessiondUserId().Length == 0) {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            ViewBag.userId = sessiondUserId();
            ViewBag.LastLogon = lastLogon();
            //LogonModel logonmodel=new LogonModel().read(sessiondUserId());
            //ViewBag.LastLogon=logonmodel.lastlogon.ToString("dd.MM.yyyy HH:mm");


            return View();
        }

        // GET: JazzHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JazzHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JazzHome/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JazzHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JazzHome/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JazzHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JazzHome/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
