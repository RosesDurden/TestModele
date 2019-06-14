using CD67.ModeleMVC.Entity;
using CD67.ModeleMVC.Solr;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD67.ModeleMVC.Factory
{
    public partial class VikingFactory : Internal.BaseFactory<Entity.Viking>
    {
        ///// <summary>
        ///// Constructeur public lié au constructeur de base
        ///// </summary>
        ///// <param name="dbContext">Context Entity Framework utilisé dans la classe</param>
        //public VikingFactory(ModeleMVCEntities dbContext) : base(dbContext) { }

        /// <summary>
        /// Retourne tous les objets (en surchargeant la méthode standard)
        /// </summary>
        /// <returns>Liste d'objets</returns>
        public override IQueryable<Viking> GetAll()
        {
            //Ce n'est pas nécessaire ici (un lazy loading est présent par défaut), mais j'ai ajouté un include explicite pour charger les sous-objets "TypeViking"
            return dbContext.Vikings.Include(item => item.TypeViking);
        }

        public override void Add(ref Viking entity)
        {
            base.Add(ref entity);

            //Mise à jour du core SolR
            VikingsIndexer vikingIndexer = new VikingsIndexer(this.dbContext);
            vikingIndexer.Add(entity);
        }

        public override void AddMany(ref List<Viking> entities)
        {
            base.AddMany(ref entities);

            //Mise à jour du core SolR
            VikingsIndexer vikingIndexer = new VikingsIndexer(this.dbContext);
            entities.ForEach(i => vikingIndexer.Add(i));
        }

        public override void Update(ref Viking entity)
        {
            base.Update(ref entity);

            //Mise à jour du core SolR
            VikingsIndexer vikingIndexer = new VikingsIndexer(this.dbContext);
            vikingIndexer.Add(entity);
        }

        public override void UpdateMany(ref List<Viking> entities)
        {
            base.UpdateMany(ref entities);

            //Mise à jour du core SolR
            VikingsIndexer vikingIndexer = new VikingsIndexer(this.dbContext);
            entities.ForEach(i => vikingIndexer.Add(i));
        }

        public override void Delete(ref Viking entity)
        {
            //Mise à jour du core SolR
            VikingsIndexer vikingIndexer = new VikingsIndexer(this.dbContext);
            vikingIndexer.delete(entity);

            base.Delete(ref entity);
        }

        public override void DeleteMany(ref List<Viking> entities)
        {
            //Mise à jour du core SolR
            VikingsIndexer vikingIndexer = new VikingsIndexer(this.dbContext);
            entities.ForEach(i => vikingIndexer.delete(i));

            base.DeleteMany(ref entities);
        }
    }
}
