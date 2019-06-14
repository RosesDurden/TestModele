using System.Net;
using System.Web.Mvc;
using CD67.ModeleMVC.Entity;
using CD67.ModeleMVC.Factory;
using System.Collections.Generic;
using System;
using System.Linq;
using CD67.ModeleMVC.MVC.Internal;
using CD67.ModeleMVC.Solr;

namespace CD67.ModeleMVC.MVC.Controllers
{
    public class VikingController : Controller
    {
        private ModeleMVCEntities db = new ModeleMVCEntities();

        // GET: Viking
        public ActionResult Index()
        {
            VikingFactory vikingsFactory = new VikingFactory(db);
            return View(vikingsFactory.GetAll());
        }

        // GET: Viking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VikingFactory vikingsFactory = new VikingFactory(db);
            Viking viking = vikingsFactory.GetById(id.Value);
            if (viking == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(viking);
        }

        // GET: Viking/Create
        public ActionResult Create()
        {
            ViewBag.ListeTypesViking = getListeTypesViking(string.Empty);

            Viking viking = new Viking();
            FillSelect(viking);
            return View(viking);
        }

        // POST: Viking/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,TypeVikingId,CasqueCornu,NombreVictoires,Description")] Viking viking)
        {
            if (ModelState.IsValid)
            {
                VikingFactory vikingsFactory = new VikingFactory(db);

                viking.DateEdition = DateTime.Now;
                viking.DateCreation = DateTime.Now;

                vikingsFactory.Add(ref viking);

                db.Entry(viking).Reference(i => i.TypeViking).Load();

                // Ajout d'un message flash
                this.Success("Viking créé avec succès.");

                return RedirectToAction("Index");
            }
            FillSelect(viking);
            return View(viking);
        }

        // GET: Viking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ListeTypesViking = getListeTypesViking(string.Empty);

            VikingFactory vikingsFactory = new VikingFactory(db);
            Viking viking = vikingsFactory.GetById(id.Value);
            if (viking == null)
            {
                return HttpNotFound();
            }
            FillSelect(viking);
            return View(viking);
        }

        // POST: Viking/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,TypeVikingId,CasqueCornu,NombreVictoires,Description,DateCreation")] Viking viking)
        {
            if (ModelState.IsValid)
            {
                VikingFactory vikingsFactory = new VikingFactory(db);

                viking.DateEdition = DateTime.Now;

                vikingsFactory.Update(ref viking);

                db.Entry(viking).Reference(i => i.TypeViking).Load();

                // Ajout d'un message flash
                this.Success("Viking édité avec succès.");

                return RedirectToAction("Index");
            }
            FillSelect(viking);
            return View(viking);
        }

        // GET: Viking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VikingFactory vikingsFactory = new VikingFactory(db);
            Viking viking = vikingsFactory.GetById(id.Value);
            if (viking == null)
            {
                return HttpNotFound();
            }
            return View(viking);
        }

        // POST: Viking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VikingFactory vikingsFactory = new VikingFactory(db);
            Viking viking = vikingsFactory.GetById(id);
            vikingsFactory.Delete(ref viking);

            // Ajout d'un message flash
            this.Success("Viking supprimé avec succès.");

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

        private void FillSelect(Viking viking)
        {
            TypeVikingFactory typeVikingFactory = new TypeVikingFactory(db);
            ViewBag.ID_TYPE = new SelectList(typeVikingFactory.GetAll(), "Id", "TypeVikingId", viking.TypeViking);

            //Chargement d'une liste vide à la création
            TypeVikingFactory sousTypeFactory = new TypeVikingFactory(db);
            ViewBag.ID_SOUS_TYPE = new SelectList(sousTypeFactory.getManyBy(viking.TypeVikingId).OrderBy(i => i.Value), "Key", "Value", viking.TypeViking);
        }

        //Mise à jour Ajax de la liste imbriquée
        public JsonResult listeSousType(int Id, int? defaultSelected)
        {
            TypeVikingFactory sousTypeFactory = new TypeVikingFactory(db);
            List<SelectListItem> listType = new List<SelectListItem>();

            Dictionary<int, string> infoType = sousTypeFactory.getManyBy(Id); //le dictionnaire doit devenir une liste de types
            
            List<Tuple<int, string>> listeType = new List<Tuple<int, string>>();
            foreach (var type in infoType)
                listeType.Add(new Tuple<int, string>(int.Parse(type.Key.ToString()), type.Value));

            foreach (Tuple<int, string> liste in listeType)
                listType.Add(new SelectListItem { Text = liste.Item2, Value = liste.Item1.ToString() });

            return Json(new SelectList(listType, "Value", "Text", defaultSelected), JsonRequestBehavior.AllowGet);
        }

        public SelectList getListeTypesViking(string defaultSelected)
        {
            TypeVikingFactory typeVikingfactory = new TypeVikingFactory(db);
            List<TypeViking> getlist = typeVikingfactory.GetAll().ToList();
            List<SelectListItem> listTypeViking = new List<SelectListItem>();

            foreach (TypeViking item in getlist)
            {
                listTypeViking.Add(new SelectListItem { Text = item.Id + " - " + item.Libelle, Value = item.Id.ToString() });
            }

            if (defaultSelected == string.Empty)
                defaultSelected = "";

            return new SelectList(listTypeViking, "Value", "Text", defaultSelected);
        }

    }
}
