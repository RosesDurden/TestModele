using CD67.ModeleMVC.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CD67.ModeleMVC.Factory.Internal
{
    /// <summary>
    /// Classe de base pour toutes les classes spécialisées de la couche de service
    /// </summary>
    public class BaseFactory<T> : IDisposable where T : class
    {
        /// <summary>
        /// Active les optimisations pour les méthodes de mise à jour de plusieurs lignes à la fois : AddMany, UpdateMany, DeleteMany
        /// Par défaut, la valeur est fausse et celles ci appellent autant de fois la mise à jour unitaire ce qui est plus sûr
        /// L'activation peut poser des problèmes dans les cas ou les mises à jour unitaires sont redéfinies
        /// </summary>
        public bool EnableMultipleOptimization { get; set; }

        /// <summary>
        /// Utilise l'option AsNoTracking dans les méthodes GET pour obtenir des objets non suivi par entity
        /// C'est plus performant mais ne permet pas d'enregistrer les modifications
        /// Par défaut cette option est désactivée
        /// </summary>
        public bool AsNoTracking { get; set; }

        /// <summary>
        /// Context Entity Framework utilisé dans la classe
        /// </summary>
        protected DbContext dbContext;

        #region Constructeurs
        /// <summary>
        /// Constructeur sans argument pour les classes sans contexts Entity
        /// </summary>
        /// <param name="AsNoTracking">Supprime la détection de modification pour des factories utilisées pour de la lecture</param>
        /// <param name="EnableMultipleOptimization">Optimise les mises à jour multiples mais n'utilise plus en boucle les ajout/modification/suppression unitaires</param>
        public BaseFactory(bool AsNoTracking = false, bool EnableMultipleOptimization = false)
        {
            this.EnableMultipleOptimization = EnableMultipleOptimization;
            this.AsNoTracking = AsNoTracking;

            Setup();
        }

        /// <summary>
        /// Constructeur avec initialisation du context Entity Framework
        /// </summary>
        /// <param name="dbContext">Context Entity Framework</param>
        /// <param name="AsNoTracking">Supprime la détection de modification pour des factories utilisées pour de la lecture</param>
        /// <param name="EnableMultipleOptimization">Optimise les mises à jour multiples mais n'utilise plus en boucle les ajout/modification/suppression unitaires</param>
        public BaseFactory(DbContext dbContext, bool AsNoTracking = false, bool EnableMultipleOptimization = false)
        {
            this.EnableMultipleOptimization = EnableMultipleOptimization;
            this.AsNoTracking = AsNoTracking;
            this.dbContext = dbContext;

            Setup();
        }

        /// <summary>
        /// Fonction appelée à la fin du constructeur, elle peut être surchargée au besoin
        /// </summary>
        public virtual void Setup() { }
        #endregion

        #region Méthodes génériques
        private DbSet<T> ObjectSet
        {
            get
            {
                return this.dbContext.Set<T>();
            }
        }

        public virtual T GetById(params object[] keyValues)
        {
            return ObjectSet.Find(keyValues);
        }

        public virtual T GetBy(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = ObjectSet.Where(expression);
            if (this.AsNoTracking) query = query.AsNoTracking();
            return query.FirstOrDefault();
        }

        public virtual T GetBy(string dynamicExpression)
        {
            IQueryable<T> query = ObjectSet.Where(dynamicExpression);
            if (this.AsNoTracking) query = query.AsNoTracking();
            return query.FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll()
        {
            return GetAll(null);
        }

        public virtual IQueryable<T> GetAll(string sortParameter = null)
        {
            IQueryable<T> query = ObjectSet;
            if (sortParameter != null) query = query.OrderBy(sortParameter);
            if (this.AsNoTracking) query = query.AsNoTracking();
            return query;
        }

        public virtual IQueryable<T> GetManyBy(Expression<Func<T, bool>> expression, string sortParameter = null)
        {
            IQueryable<T> query = ObjectSet.Where(expression);
            if (sortParameter != null) query = query.OrderBy(sortParameter);
            if (this.AsNoTracking) query = query.AsNoTracking();
            return query;
        }

        public virtual IQueryable<T> GetManyBy(string dynamicExpression, string sortParameter = null)
        {
            IQueryable<T> query = ObjectSet.Where(dynamicExpression);
            if (sortParameter != null) query = query.OrderBy(sortParameter);
            if (this.AsNoTracking) query = query.AsNoTracking();
            return query;
        }

        public virtual bool Any(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.Any(expression);
        }

        public virtual int Count()
        {
            return ObjectSet.Count();
        }

        public virtual int CountManyBy(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.Count(expression);
        }

        public virtual void Add(ref T entity)
        {
            ObjectSet.Add(entity);
            dbContext.SaveChanges();
        }

        public virtual void AddMany(ref List<T> entities)
        {
            if (this.EnableMultipleOptimization)
            {
                ObjectSet.AddRange(entities);
                dbContext.SaveChanges();
            }
            else entities.ForEach(i => this.Add(ref i));
        }

        public virtual void Update(ref T entity)
        {
            // On attache l'objet en paramètre au contexte, on le note bien comme modifié pour qu'il soit mis à jour
            ObjectSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public virtual void UpdateMany(ref List<T> entities)
        {
            if (this.EnableMultipleOptimization)
            {
                foreach (var item in entities)
                {
                    // On attache l'objet en paramètre au contexte, on le note bien comme modifié pour qu'il soit mis à jour
                    ObjectSet.Attach(item);
                    dbContext.Entry(item).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            else entities.ForEach(i => this.Update(ref i));
        }

        public virtual void UpdateMany(Expression<Func<T, bool>> expression)
        {
            List<T> query = ObjectSet.Where(expression).ToList();
            this.UpdateMany(ref query);
        }

        public virtual void UpdateMany(string dynamicExpression)
        {
            List<T> query = ObjectSet.Where(dynamicExpression).ToList();
            this.UpdateMany(ref query);
        }

        public virtual void Delete(ref T entity)
        {
            ObjectSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public virtual void DeleteMany(ref List<T> entities)
        {
            if (this.EnableMultipleOptimization)
            {
                ObjectSet.RemoveRange(entities);
                dbContext.SaveChanges();
            }
            else entities.ForEach(i => this.Delete(ref i));
        }

        public virtual void DeleteMany(Expression<Func<T, bool>> expression)
        {
            List<T> query = ObjectSet.Where(expression).ToList();
            this.DeleteMany(ref query);
        }

        public virtual void DeleteMany(string dynamicExpression)
        {
            List<T> query = ObjectSet.Where(dynamicExpression).ToList();
            this.DeleteMany(ref query);
        }
        #endregion

        #region Dispose
        private bool disposed = false;

        /// <summary>
        /// Fonction pour détruire proprement la classe après utilisation
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Fonction pour détruire proprement la classe après utilisation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
