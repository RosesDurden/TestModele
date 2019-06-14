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
    [MetadataType(typeof(Viking_Metadata))]
    public partial class Viking
    {
        //Peut contenir une extension utile à la classe (méthode static ou non, nouvelles propriétés, propriétés construites dynamiquement selon d'autres de la classe, etc.)
        
        //Exemples de propriété booléenne qui renverra toujours 1
        public Nullable<int> costaud
        {
            get { return 1; }
            set { this.costaud = value; }
        }

        //Exemple d'un sous-type fictif pour les listes imbriquées
        public int IdSousType { get; set; }
    }

    /// <summary>
    /// Classe contenant les DataAnnotations pour chaque champ
    /// </summary>
    public class Viking_Metadata
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        [StringLength(255)]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int TypeVikingId { get; set; }

        [Display(Name = "Sous type", Description = "Valeur non enregistrée")]
        public int IdSousType { get; set; }

        //Utilise un affichage customisé MVC "YesNo", qui se trouve ici : "Views\Shared\DisplayTemplates" et "Views\Shared\EditorTemplates"
        [Display(Name = "Casque Cornu")]
        //[Range(0, 1)]
        //[UIHint("YesNoInt")]
        public bool? CasqueCornu;

        [Required]
        [Display(Name = "Nombre de victoires")]
        public int NombreVictoires { get; set; }

        [Display(Name = "Costaud", Description = "Toujours oui")]
        [Range(0, 1)]
        [UIHint("YesNoInt")]
        public Nullable<int> costaud;

        [Display(Name = "Description")]
        //[StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Date de création")]
        [DataType(DataType.Date)]
        public DateTime? DateCreation { get; set; }

        [Display(Name = "Date de dernière édition")]
        [DataType(DataType.Date)]
        public DateTime? DateEdition { get; set; }

        ////Propriétés ajoutées dans la classe partielle
        //[Display(Name = "Costaud", Description = "Toujours oui")]
        //[Range(0, 1)]
        //[UIHint("YesNoInt")]
        //public Nullable<int> costaud;

    }
}
