using System.Net;
using System.Web.Mvc;
using System.Web.SessionState;
using Voresjazzklub.Models;
namespace Voresjazzklub.Controllers {
    public class TilmeldingersController : Controller {
        private string sessiondUserId() {
            var vd = ViewData;
            var vc = ViewEngineCollection;

            return Session["userId"] == null ? null : Session["userId"].ToString();
        }

        private MySQLConnection db = new MySQLConnection();
        private Tilmeldinger tilmeld = new Tilmeldinger();

        private void theViewBag(long? id) {
            if(id != null) {
                ViewBag.arrangementId = id;
                ViewBag.arrangementBeskrivelse = new ArrangementModel().read((long)id).arrangementBeskrivelse;
            }
        }

        // GET: Tilmeldingers
        public ActionResult Index(long?id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            theViewBag(id);
            
            return (id==null) ? View(new Tilmeldinger().readList()) : View(new Tilmeldinger().readList((long)id));
            //return View(db.Tilmeldingers.ToList());
        }


        public ActionResult Tilmeldte(long? arran) { // arran skal også hedde arran i RouteConfig
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            theViewBag(arran);
            return (arran == null) ? View(new Tilmeldinger().readList()) : View(new Tilmeldinger().readList((long)arran));
            //return View(db.Tilmeldingers.ToList());
        }

        /*public ActionResult Index(long arran) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            return View(new Tilmeldinger().readList(arran));
            //return View(db.Tilmeldingers.ToList());
        }*/
        // GET: Tilmeldingers/Details/5
        public ActionResult Details(long? id, string bruger) {  // "string" is nullable, hence "string?" makes no sense
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(id == null || bruger==null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tilmeldinger tilmeldinger = db.Tilmeldingers.Find(id);
            //  (sessiondUserId() == bruger) ==> må godt Edit
            Tilmeldinger tilmeldinger = new Tilmeldinger().read((long)id, (string)bruger);
            if (tilmeldinger == null)
            {
                return HttpNotFound();
            }
            theViewBag(id);
            return View(tilmeldinger);
        }

        // GET: Tilmeldingers/Create
        public ActionResult Create(long?id) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            theViewBag(id);

            if(id!=null) {
                Tilmeldinger tilmeldinger = new Tilmeldinger();
                tilmeldinger.arrangementId = (long)id;
                return View(tilmeldinger);
            }
            
            return View();
        }

        // POST: Tilmeldingers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "arrangementId,brugerId,createTs,erTilmeldt,spiser")] Tilmeldinger tilmeldinger) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }
            if(sessiondUserId() != null) {
                tilmeldinger.brugerId = sessiondUserId();
            }
            theViewBag(tilmeldinger.arrangementId);
            if(ModelState.IsValid) {
                tilmeldinger.create();
                //db.Tilmeldingers.Add(tilmeldinger);
                //db.SaveChanges();
                return RedirectToAction("Index/"+ tilmeldinger.arrangementId);
            }
            return View(tilmeldinger);
        }

        // GET: Tilmeldingers/Edit/5
        public ActionResult Edit(long? id, string bruger) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(id == null || bruger==null)  {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // sessiondUserId()
            Tilmeldinger tilmeldinger = new Tilmeldinger().read((long)id, bruger);
            //Tilmeldinger tilmeldinger = db.Tilmeldingers.Find(id);
            if (tilmeldinger == null)
            {
                return HttpNotFound();
            }

            theViewBag(id);
            return View(tilmeldinger);
        }

        // POST: Tilmeldingers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "arrangementId,brugerId,createTs,erTilmeldt,spiser")] Tilmeldinger tilmeldinger) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(ModelState.IsValid)
            {
                tilmeldinger.update();
                //db.Entry(tilmeldinger).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index/"+tilmeldinger.arrangementId);
            }
            theViewBag(tilmeldinger.arrangementId);
            return View(tilmeldinger);
        }

        // GET: Tilmeldingers/Delete/5
        public ActionResult Delete(long? id, string bruger) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            if(id == null || bruger==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tilmeldinger tilmeldinger = db.Tilmeldingers.Find(id);
            Tilmeldinger tilmeldinger =new Tilmeldinger().read((long)id, bruger/*sessiondUserId()*/);
            if (tilmeldinger == null)
            {
                return HttpNotFound();
            }
            theViewBag(id);
            return View(tilmeldinger);
        }

        // POST: Tilmeldingers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id, string bruger) {
            if(sessiondUserId() == null || sessiondUserId() == "") {
                return RedirectToAction("logIn", "UsersTableModels");
            }

            new Tilmeldinger().read(id, sessiondUserId()).delete(id, bruger/*sessiondUserId()*/);
            //Tilmeldinger tilmeldinger = db.Tilmeldingers.Find(id);
            //db.Tilmeldingers.Remove(tilmeldinger);
            //db.SaveChanges();
            theViewBag(id);
            return RedirectToAction("Index/"+id);
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
