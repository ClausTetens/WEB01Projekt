using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Models;

namespace Voresjazzklub.Controllers
{
    public class ArrangementController : Controller {
        private string sessiondUserId() {
            return Session["userId"] == null ? null : Session["userId"].ToString();
        }

        // GET: Arrangement
        public ActionResult Index() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            return View(new ArrangementModel().readList());
            //return View();
        }

        // GET: Arrangement/Details/5
        public ActionResult Details(int id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            return View();
        }

        // GET: Arrangement/Create
        public ActionResult Create() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            return View();
        }

        // POST: Arrangement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
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

        // GET: Arrangement/Edit/5
        public ActionResult Edit(int id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            return View();
        }

        // POST: Arrangement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
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

        // GET: Arrangement/Delete/5
        public ActionResult Delete(int id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            return View();
        }

        // POST: Arrangement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
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
