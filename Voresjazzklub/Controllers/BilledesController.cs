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


    public class BilledesController : Controller
    {
        private BilledeArrang billedeArrang = new BilledeArrang();
        private MySQLConnection db = new MySQLConnection();

        // GET: Billedes
        public ActionResult Index()
        {   
            //db.Database.Connection.Close(); ved close kommer ingen exception
            //return View(db.Billedes.ToList());

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //return View(billedeArrang.readList());
        }

        public ActionResult IndexId(long? id) {
            //db.Database.Connection.Close(); ved close kommer ingen exception
            //return View(db.Billedes.ToList());

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //return (id == null) ? View(billedeArrang.readList()) : View(billedeArrang.readList((long)id));
        }


        // GET: Billedes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Billede billede = db.Billedes.Find(id);
            Billede billede = null;
            if (billede == null)
            {
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //return View(billede);
        }

        // GET: Billedes/Create
        public ActionResult Create()
        {
            //return View();
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Billedes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "billedAdresse,arrangementId,brugerId,createTs,kommentar")] Billede billede)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            /*
            if (ModelState.IsValid)
            {
                //db.Billedes.Add(billede);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billede);*/
        }

        // GET: Billedes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Billede billede = db.Billedes.Find(id);
            Billede billede = null;
            if (billede == null)
            {
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //return View(billede);
        }

        // POST: Billedes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "billedAdresse,arrangementId,brugerId,createTs,kommentar")] Billede billede)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            /*
            if (ModelState.IsValid)
            {
                //db.Entry(billede).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billede);*/
        }

        // GET: Billedes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Billede billede = db.Billedes.Find(id);
            Billede billede = null;
            if (billede == null)
            {
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //return View(billede);
        }

        // POST: Billedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Billede billede = db.Billedes.Find(id);
            //db.Billedes.Remove(billede);
            //db.SaveChanges();
            //return RedirectToAction("Index");
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
