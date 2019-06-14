using System.Net;
using System.Web.Mvc;
using CD67.ModeleMVC.Entity;
using CD67.ModeleMVC.Factory;
using CD67.ModeleMVC.MVC.Internal;

namespace CD67.ModeleMVC.MVC.Controllers
{
    public class TypeVikingController : Controller
    {
        private ModeleMVCEntities db = new ModeleMVCEntities();

        // GET: TypeViking
        public ActionResult Index()
        {
            TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
            return View(typeVikingFactory.GetAll());
        }

        // GET: TypeViking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
            TypeViking typeViking = typeVikingFactory.GetById(id.Value);
            if (typeViking == null)
            {
                return HttpNotFound();
            }
            return View(typeViking);
        }

        // GET: TypeViking/Create
        public ActionResult Create()
        {
            TypeViking typeViking = new TypeViking();
            return View(typeViking);
        }

        // POST: TypeViking/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Libelle")] TypeViking typeViking)
        {
            if (ModelState.IsValid)
            {
                TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
                typeVikingFactory.Add(ref typeViking);

                // Ajout d'un message flash
                this.Success("Type de viking créé avec succès.");

                return RedirectToAction("Index");
            }

            return View(typeViking);
        }

        // GET: TypeViking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
            TypeViking typeViking = typeVikingFactory.GetById(id.Value);
            if (typeViking == null)
            {
                return HttpNotFound();
            }
            return View(typeViking);
        }

        // POST: TypeViking/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Libelle")] TypeViking typeViking)
        {
            if (ModelState.IsValid)
            {
                TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
                typeVikingFactory.Update(ref typeViking);

                // Ajout d'un message flash
                this.Success("Type de viking edité avec succès.");

                return RedirectToAction("Index");
            }
            return View(typeViking);
        }

        // GET: TypeViking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
            TypeViking typeViking = typeVikingFactory.GetById(id.Value);
            if (typeViking == null)
            {
                return HttpNotFound();
            }
            return View(typeViking);
        }

        // POST: TypeViking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
            TypeViking typeViking = typeVikingFactory.GetById(id);
            typeVikingFactory.Delete(ref typeViking);

            // Ajout d'un message flash
            this.Success("Type de viking supprimé avec succès.");

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
