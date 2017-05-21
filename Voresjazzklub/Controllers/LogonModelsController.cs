using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Models;

namespace Voresjazzklub.Controllers
{
    public class LogonModelsController : Controller {
        private string sessiondUserId() {
            return Session["userId"] == null ? null : Session["userId"].ToString();
        }

        private MySQLConnection db = new MySQLConnection();

        // GET: LogonModels
        public ActionResult Index() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            //return View(db.LogonModels.ToList());
            return View(new LogonModel().readList());
        }

        // GET: LogonModels/Details/5
        public ActionResult Details(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LogonModel logonModel = db.LogonModels.Find(id);
            LogonModel logonModel = new LogonModel().read(id);
            if (logonModel == null)
            {
                return HttpNotFound();
            }
            return View(logonModel);
        }

        // GET: LogonModels/Create
        public ActionResult Create() {
            if(sessiondUserId() == null || sessiondUserId() == string.Empty) {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            return View();
        }

        // POST: LogonModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "brugerId,lastlogon,logonCnt")] LogonModel logonModel) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(ModelState.IsValid)
            {
                logonModel.create();
                //db.LogonModels.Add(logonModel);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logonModel);
        }

        // GET: LogonModels/Edit/5
        public ActionResult Edit(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogonModel logonModel = new LogonModel().read(id);
            //LogonModel logonModel = db.LogonModels.Find(id);
            if (logonModel == null)
            {
                return HttpNotFound();
            }
            return View(logonModel);
        }

        // POST: LogonModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "brugerId,lastlogon,logonCnt")] LogonModel logonModel) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(ModelState.IsValid)
            {
                logonModel.update();
                //db.Entry(logonModel).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logonModel);
        }

        // GET: LogonModels/Delete/5
        public ActionResult Delete(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogonModel logonModel = new LogonModel().read(id);
            //LogonModel logonModel = db.LogonModels.Find(id);
            if (logonModel == null)
            {
                return HttpNotFound();
            }
            return View(logonModel);
        }

        // POST: LogonModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            new LogonModel().delete(id);
            //LogonModel logonModel = db.LogonModels.Find(id);
            //db.LogonModels.Remove(logonModel);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
