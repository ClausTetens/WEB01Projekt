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
    public class ArranController : Controller
    {
        private MySQLConnection db = new MySQLConnection();
        private ArranModel arranModel = new ArranModel();
        private string sessiondUserId() {
            return Session["userId"] == null ? null : Session["userId"].ToString();
        }

        public ActionResult Afslut() {
            Session["userId"] = null;
            return RedirectToAction("logIn", "UsersTableModels");
        }

        // GET: Arrans
        public ActionResult Index() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            //return View(db.Arrans.ToList());
            return View(arranModel.readList());
        }

        // GET: Arrans/Details/5
        public ActionResult Details(long? id)
        {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ArranModel arran = db.Arrans.Find(id);
            ArranModel arran = new ArranModel().read((long)id);
            if (arran == null)
            {
                return HttpNotFound();
            }
            return View(arran);
        }

        // GET: Arrans/Create
        public ActionResult Create() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            return View();
        }

        // POST: Arrans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "arrangementId,arrangementBeskrivelse,createTs,arrangementWeb,arrangementDt,arrangemnetTid,spiseSted,spiseTid")] ArranModel arran) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            if (ModelState.IsValid)
            {
                arran.create();
                //db.Arrans.Add(arran);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arran);
        }

        // GET: Arrans/Edit/5
        public ActionResult Edit(long? id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ArranModel arran = db.Arrans.Find(id);
            ArranModel arran = new ArranModel().read((long)id);
            if (arran == null)
            {
                return HttpNotFound();
            }
            return View(arran);
        }

        // POST: Arrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "arrangementId,arrangementBeskrivelse,createTs,arrangementWeb,arrangementDt,arrangemnetTid,spiseSted,spiseTid")] ArranModel arran) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            if (ModelState.IsValid)
            {
                //db.Entry(arran).State = EntityState.Modified;
                //db.SaveChanges();
                arran.update();
                //return RedirectToAction("Index/"+arranModel.arrangementId); -- skal lige testes først 
                return RedirectToAction("Index");
            }
            return View(arran);
        }

        // GET: Arrans/Delete/5
        public ActionResult Delete(long? id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ArranModel arran = db.Arrans.Find(id);
            ArranModel arran = new ArranModel().read((long)id);
            //if (arran == null)
            if(arran.arrangementId<0)
            {
                return HttpNotFound();
            }
            return View(arran);
        }

        // POST: Arrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            //ArranModel arran = db.Arrans.Find(id);
            //db.Arrans.Remove(arran);
            //db.SaveChanges();
            new ArranModel().delete(id);
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
