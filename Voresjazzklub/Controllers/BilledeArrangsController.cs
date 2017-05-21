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
    public class BilledeArrangsController : Controller
    {
        private MySQLConnection db = new MySQLConnection();
        BilledeArrang billedeArrang = new BilledeArrang();

        private void theViewBag(long? id) {
            if(id != null) {
                ViewBag.arrangementId = id;
                ViewBag.arrangementBeskrivelse = new ArrangementModel().read((long)id).arrangementBeskrivelse;
                //new BilledeArrang().read((long)id).arrangementBeskrivelse;
                //new ArrangementModel().read((long)id).arrangementBeskrivelse;
            } else {
                ViewBag.arrangementBeskrivelse = "koncerter";
            }
        }

        // GET: BilledeArrangs
        public ActionResult Index(long? id)
        {
            //return View(db.BilledeArrangs.ToList());
            theViewBag(id);
            return (id == null) ? View(billedeArrang.readList()) : View(billedeArrang.readList((long)id));
        }

        // GET: BilledeArrangs/Details/5
        public ActionResult Details(long? arran, long? id)
        {
            if (arran==null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BilledeArrang billedeArrang = db.BilledeArrangs.Find(id);
            BilledeArrang billede=billedeArrang.read((long)arran, (long)id);
            if (billedeArrang == null)
            {
                return HttpNotFound();
            }
            theViewBag(id);
            return View(billede);
        }

        // GET: BilledeArrangs/Create
        public ActionResult Create(long? id)
        {
            if(id==null)
                return View();
            BilledeArrang billedArrang = new BilledeArrang();
            billedeArrang.arrangementId = (long)id;
            //billedeArrang.brugerId=
            return View(billedeArrang);
        }

        // POST: BilledeArrangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "billedId,billedAdresse,arrangementId,brugerId,createTs,kommentar,arrangementBeskrivelse")] BilledeArrang billedeArrang)
        {
            if (ModelState.IsValid)
            {
                //db.BilledeArrangs.Add(billedeArrang);
                //db.SaveChanges();
                billedeArrang.create();
                return RedirectToAction("Index/"+billedeArrang.arrangementId);
            }
            theViewBag(billedeArrang.arrangementId);
            return View(billedeArrang);
        }

        // GET: BilledeArrangs/Edit/5
        public ActionResult Edit(long?arran, long? id)
        {
            if (arran==null ||id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BilledeArrang billedeArrang = db.BilledeArrangs.Find(id);
            BilledeArrang billede=billedeArrang.read((long)arran, (long)id);
            if (billedeArrang == null)
            {
                return HttpNotFound();
            }
            theViewBag(arran);
            return View(billede);
        }

        // POST: BilledeArrangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "billedId,billedAdresse,arrangementId,brugerId,createTs,kommentar,arrangementBeskrivelse")] BilledeArrang billedeArrang)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(billedeArrang).State = EntityState.Modified;
                //db.SaveChanges();
                billedeArrang.update();
                return RedirectToAction("Index/"+billedeArrang.arrangementId);
            }
            theViewBag(billedeArrang.arrangementId);
            return View(billedeArrang);
        }

        // GET: BilledeArrangs/Delete/5
        public ActionResult Delete(long?arran, long? id)
        {
            if (arran==null ||id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BilledeArrang billedeArrang = db.BilledeArrangs.Find(id);
            billedeArrang=billedeArrang.read((long)arran, (long)id);
            if (billedeArrang == null)
            {
                return HttpNotFound();
            }
            theViewBag(id);
            return View(billedeArrang);
        }

        // POST: BilledeArrangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long arran, long id)
        {
            //BilledeArrang billedeArrang = db.BilledeArrangs.Find(id);
            //db.BilledeArrangs.Remove(billedeArrang);
            //db.SaveChanges();
            theViewBag(id);
            billedeArrang.delete(id);
            return RedirectToAction("Index/"+arran);
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
