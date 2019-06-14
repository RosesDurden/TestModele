using CD67.ModeleMVC.Entity;
using System.Linq;

namespace CD67.ModeleMVC.Factory
{
	/// <summary>
    /// NE PAS MODIFIER
    /// C'est une classe partielle, elle peut être complétée avec une classe partielle du même nom
    /// Factory générée automatiquement à l'aide du fichier GenericFactories.tt
    /// pour toutes les entités du fichier entity : /CD67.ModeleMVC.Entity/EntityModel.edmx
    /// </summary>
	public partial class TypeVikingFactory : Internal.BaseFactory<Entity.TypeViking>
	{
		/// <summary>
        /// Constructeur public lié au constructeur de base
        /// </summary>
        /// <param name="dbContext">Context Entity Framework utilisé dans la classe</param>
		/// <param name="AsNoTracking">Supprime la détection de modification pour des factories utilisées pour de la lecture</param>
        /// <param name="EnableMultipleOptimization">Optimise les mises à jour multiples mais n'utilise plus en boucle les ajout/modification/suppression unitaires</param>
		public TypeVikingFactory(ModeleMVCEntities dbContext, bool AsNoTracking = false, bool EnableMultipleOptimization = false) : base(dbContext, AsNoTracking, EnableMultipleOptimization) { }
	}
}
	