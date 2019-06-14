using CD67.ModeleMVC.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD67.ModeleMVC.Factory
{
    public partial class TypeVikingFactory
    {
        ///// <summary>
        ///// Constructeur public lié au constructeur de base
        ///// </summary>
        ///// <param name="dbContext">Context Entity Framework utilisé dans la classe</param>
        //public TypeVikingFactory(ModeleMVCEntities dbContext) : base(dbContext) { }

        /// <summary>
        /// Retourne une liste d'objets selon des paramètres
        /// </summary>
        /// <returns>Liste d'objets</returns>
        public Dictionary<int, string> getManyBy(int TypeId)
        {
            //cas de sortie directe
            if (TypeId == -1) return new Dictionary<int, string>();

            //liste fictive utilisant le type passé en entrée
            return new Dictionary<int, string>()
            {
                { 1, $"Type-{TypeId} Toto" },
                { 2, $"Type-{TypeId} Tata" },
                { 3, $"Type-{TypeId} Tutu" }
            };
        }
    }
}
