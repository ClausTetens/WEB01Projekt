using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Voresjazzklub.Filters;
using Voresjazzklub.Models;


namespace Voresjazzklub.Controllers
{
    [AdgangskontrolAttribute]
    public class UsersTableModelsController : Controller {

        private MySQLConnection db = new MySQLConnection();

        private string sessiondUserId() {
            return Session["userId"] == null?null:Session["userId"].ToString();
        }
        private void sessiondUserId(string userId) {
            Session["userId"] = userId;
        }


        [HttpGet]
        public ActionResult logIn() {
            /*UsersTableModel usersTableModel = new UsersTableModel();
            return View(usersTableModel);*/
            return View();
        }

        private void createLogon(string userId) {
            new LogonModel(userId).create();
        }

        private DateTime updateLogon(string userId) {
            LogonModel logonModel = new LogonModel().read(userId);
            if(logonModel==null) {
                createLogon(userId);
                logonModel = new LogonModel().read(userId);
            }
            logonModel.logonCnt++;
            logonModel.update();
            return logonModel.lastlogon;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logIn(UsersTableModel usersTableModel) {
            if(!ModelState.IsValid) {
                return View(usersTableModel);
            }
            if(usersTableModel.isUserValid()) {
                sessiondUserId(usersTableModel.brugerId);
                DateTime lastlogonDt=updateLogon(usersTableModel.brugerId);
                Session["lastlogon"] = string.Format("{0:dd.MM.yyyy HH:mm}", lastlogonDt);
                return RedirectToAction("Index", "Jazzhome");
            }
            ModelState.AddModelError("","Hmm .. kan ikke genkende dig");
            return View(usersTableModel);
        }
        /*

        public ActionResult Jazzhome() {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn");
            }
            DateTime lastlogonDt = updateLogon(sessiondUserId());
            ViewBag.lastlogon = string.Format("{0:dd.MM.yyyy HH:mm}", lastlogonDt);
            ViewBag.brugerId = sessiondUserId();
            return View();
        }
        */

        [AllowAnonymous]
        // GET: UsersTableModels
        public ActionResult Index()
        {
            if(sessiondUserId()==null || sessiondUserId()=="") {
                return RedirectToAction("logIn");
            }

            List<UsersTableModel> utms = new UsersTableModel().getUserList();
            return View(utms);
            /*
            UsersTableModel utm = new UsersTableModel();
            utm.brugerId = "Abc";
            utm.createTs = DateTime.Now;
            utm.email = "q@q.dk";
            utm.erAdmin = false;
            utm.password = "HejMor";
            utm.salt = "";

            var utms = new List<UsersTableModel>();
            utms.Add(utm);
            return View(utms);
            */
           // return View(db.UsersTableModel.ToList());
        }

        // GET: UsersTableModels/Details/5
        public ActionResult Details(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn");
            }

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UsersTableModel usersTableModel = db.UsersTableModel.Find(id);
            UsersTableModel usersTableModel = new UsersTableModel();
            usersTableModel.getUser(id);
            if (usersTableModel == null)
            {
                return HttpNotFound();
            }
            return View(usersTableModel);
        }

        // GET: UsersTableModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersTableModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "brugerId,createTs,password,salt,email,prim,erAdmin")] UsersTableModel usersTableModel)
        {
            if (ModelState.IsValid)
            {
                /*
                db.UsersTableModel.Add(usersTableModel);
                db.SaveChanges();
                */

                if(usersTableModel.userExists()) {
                    ModelState.AddModelError("", "Brugeren eksisterer allerede - prøv at hedde noget andet");
                } else {
                    usersTableModel.createUser();
                    new LogonModel().create(usersTableModel.brugerId);
                    return RedirectToAction("Index");
                }
            }

            return View(usersTableModel);
        }

        // GET: UsersTableModels/Edit/5
        public ActionResult Edit(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn");
            }

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UsersTableModel usersTableModel = db.UsersTableModel.Find(id);
            UsersTableModel usersTableModel = new UsersTableModel();
            usersTableModel.getUser(id);
            if (usersTableModel == null)
            {
                return HttpNotFound();
            }
            return View(usersTableModel);
        }

        // POST: UsersTableModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "brugerId,createTs,password,salt,email,prim,erAdmin")] UsersTableModel usersTableModel) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn");
            }

            if(ModelState.IsValid)
            {
                /*
                db.Entry(usersTableModel).State = EntityState.Modified;
                db.SaveChanges();
                */

                usersTableModel.updateUser();
                return RedirectToAction("Index");
            }
            return View(usersTableModel);
        }

        // GET: UsersTableModels/Delete/5
        public ActionResult Delete(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn");
            }

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //UsersTableModel usersTableModel = db.UsersTableModel.Find(id);
            UsersTableModel usersTableModel = new UsersTableModel();
            usersTableModel.getUser(id);
            //usersTableModel.deleteUser();
            if (usersTableModel == null)
            {
                return HttpNotFound();
            }
            return View(usersTableModel);
        }

        // POST: UsersTableModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn");
            }

            /*
            UsersTableModel usersTableModel = db.UsersTableModel.Find(id);
            db.UsersTableModel.Remove(usersTableModel);
            db.SaveChanges();
            */
            UsersTableModel usersTableModel = new UsersTableModel();
            usersTableModel.deleteUser(id);
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
