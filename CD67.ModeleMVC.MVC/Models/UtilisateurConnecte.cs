using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD67.ModeleMVC.MVC.Models
{
    /// <summary>
    /// Classe correspondant à l'utilisateur actuelement connecté à l'application
    /// </summary>
    public class UtilisateurConnecte
    {
        public string nom {get; set; }
        public string prenom { get; set; }
        public string libelle { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public int? employeeID { get; set; }
        public Guid? guid { get; set; }
        public string sid { get; set; }
        public string structure { get; set; }
    }
}