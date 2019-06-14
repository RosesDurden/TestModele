using System.Data.Entity.Validation;

namespace CD67.ModeleMVC.Entity
{
    public partial class ModeleMVCEntities
    {
        /// <summary>
        /// Ajout d'une meilleure gestion des exceptions Entity pour la méthode "SaveChanges"
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new Internal.FormattedDbEntityValidationException(e);
                throw newException;
            }
        }
    }
}
