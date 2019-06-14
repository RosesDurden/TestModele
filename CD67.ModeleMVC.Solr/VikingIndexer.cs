using CD67.ModeleMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CD67.ModeleMVC.Solr
{
    public class VikingsIndexer : BaseSolRIndex<Viking>, ISolRDataIndex<Viking>
    {
        public static string SolrUrl = "http://t-lunr4:8080/solr/";
        public static string SolrCore = "modele-mvc";

        /// <summary>
        /// Constructeur public lié au constructeur de base
        /// </summary>
        /// <param name="dbContext">Context Entity Framework utilisé dans la classe</param>
        public VikingsIndexer(ModeleMVCEntities dbContext) : base(SolrUrl, SolrCore, dbContext) { }

        /// <summary>
        /// Ajoute le viking à l'index solr
        /// </summary>
        /// <param name="viking"> Un objet Viking </param>
        /// <param name="version"> Un GUID utilisé pour identifier la version des données indéxées </param>
        /// <param name="autocommit"> Si autocommit est vrai, les modifications apportées à l'index seront commitées automatiquement </param>
        public override void Add(Viking viking, string version = "", bool autocommit = true)
        {
            // Si aucun GUID version n'a été donné en paramètre, en génère un pour cet ajout.
            if (String.IsNullOrEmpty(version))
            {
                version = Guid.NewGuid().ToString();
            }

            // Génère le document XML qui servira à transmettre les données à SolR
            XmlDocument xmlSolrDocument = new XmlDocument();
            xmlSolrDocument.AppendChild(xmlSolrDocument.CreateElement("add"));
            XmlNode docNode = xmlSolrDocument.DocumentElement.AppendChild(xmlSolrDocument.CreateElement("doc"));

            // Ajoute les noeuds au document XML.
            // >> Cette partie est à modifier selon le type d'objet indéxé (ici: Viking)

            // Id unique Solr
            base.AddFieldNode(ref docNode, "id", viking.Id.ToString());
            // Version de la donnée
            base.AddFieldNode(ref docNode, "version", version);

            // Donnees
            base.AddFieldNode(ref docNode, "nom", viking.Nom);
            base.AddFieldNode(ref docNode, "description", viking.Description);

            // Facettes
            dbContext.Entry(viking).Reference("TypeViking").Load();
            base.AddFieldNode(ref docNode, "type_libelle", viking.TypeViking.Libelle);
            base.AddFieldNode(ref docNode, "casque_cornu", (viking.CasqueCornu ? "Oui" : "Non"));
            base.AddFieldNode(ref docNode, "nb_victoires", (viking.NombreVictoires == null) ? "0" : viking.NombreVictoires.ToString());
            base.AddFieldNode(ref docNode, "annee_creation", viking.DateCreation.Year.ToString());

            // Tri
            base.AddFieldNode(ref docNode, "date_edition", String.Format("{0:yyyyMMddhhmmss}", viking.DateEdition));

            // contruit le champ recherche par concaténation
            List<string> recherche_strings = new List<string>() { viking.Nom, viking.TypeViking.Libelle, viking.Description };
            string recherche = String.Join(" ", recherche_strings);
            base.AddFieldNode(ref docNode, "recherche", recherche.ToLower());

            //Mise à jour du corps Solr
            base.Update(xmlSolrDocument);

            // Commit les modifications si besoin
            if (autocommit) base.Commit();
        }

        public void delete(Viking item, bool autoCommit = true)
        {
            base.Delete(item.Id.ToString());

            //Auto-commit
            if (autoCommit) base.Commit();
        }
    }
}
