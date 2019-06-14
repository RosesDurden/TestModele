using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Attention à l'espace de nom, lors de la création d'une nouvelle classe celui-ci sera par défaut :
// namespace CD67.ModeleMVC.Entity.Extend
// Alors que pour étendre une classe il faut être dans le même namespace que l'original
namespace CD67.ModeleMVC.Entity
{
    /// <summary>
    /// Classe d'extension de celle d'Entity, nécessaire pour y associer les Metadata
    /// </summary>
    [MetadataType(typeof(TypeViking_Metadata))]
    public partial class TypeViking { }

    /// <summary>
    /// Classe contenant les DataAnnotations pour chaque champ
    /// </summary>
    public class TypeViking_Metadata
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Libelle")]
        [StringLength(100)]
        public string Libelle { get; set; }
    }
}
