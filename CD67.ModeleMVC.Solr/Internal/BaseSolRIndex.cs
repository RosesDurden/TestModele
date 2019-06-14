using CD67.ModeleMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolrTools;
using System.Xml;

namespace CD67.ModeleMVC.Solr
{
    /// <summary>
    /// Classe de base pour la gestion des index SolR
    /// </summary>
    public class BaseSolRIndex<T> : IDisposable where T : class
    {
        protected ModeleMVCEntities dbContext;
        public string SolRUrl { get; set; }
        public string SolRCore { get; set; }

        /// <summary>
        /// Constructeur avec initialisation du core et du context
        /// </summary>
        /// <param name="SolRUrl">URL du moteur de recherche SolR</param>
        /// <param name="SolRCore">Nom du core SolR</param>
        /// <param name="dbContext">Context entity nécessaire pour les classes dépendantes du context</param>
        public BaseSolRIndex(string SolRUrl, string SolRCore, ModeleMVCEntities dbContext = null)
        {
            this.SolRUrl = SolRUrl;
            this.SolRCore = SolRCore;
            if (dbContext != null) this.dbContext = dbContext;
        }

        #region Ajout
        /// <summary>
        /// Ajoute une liste d'objets à l'index SolR
        /// </summary>
        /// <param name="items">Liste d'éléments</param>
        /// <param name="version">Token</param>
        /// <param name="autoCommit">Commit à la fin ou non</param>
        /// <param name="chunk">Commit par paquet</param>
        public void Add(List<T> items, string version = null, bool autoCommit = true, int chunk = 0)
        {
            int i = 0;
            foreach (var item in items)
            {
                Add(item, version, false);
                if (chunk != 0 && ++i % chunk == 0) Commit();
            }

            //Auto-commit
            if (autoCommit) Commit();
        }

        public virtual void Add(T item, string version = null, bool autoCommit = true) { }
        #endregion

        #region Pilotage SolrEngine
        /// <summary>
        /// Requête SolR de sélection
        /// </summary>
        /// <param name="query">Requête SolR</param>
        /// <returns>Résultats au format XML</returns>
        public XmlNode SolrRequest(string query)
        {
            return SolrEngine.SolrRequest(SolRUrl, SolRCore, query);
        }

        /// <summary>
        /// Effectue un commit sur l'index
        /// </summary>
        public void Commit()
        {
            SolrEngine.SolrUpdate(SolRUrl, SolRCore, "<commit/>");
        }

        /// <summary>
        /// Mise à jour de l'index
        /// </summary>
        /// <param name="doc">Document XML de mise à jour</param>
        public void Update(XmlNode doc)
        {
            SolrEngine.SolrUpdate(SolRUrl, SolRCore, doc.OuterXml);
        }

        /// <summary>
        /// Optimise le core SolR
        /// </summary>
        public void Optimize()
        {
            SolrEngine.SolrUpdate(SolRUrl, SolRCore, "<optimize/>");
        }

        /// <summary>
        /// Permet la suppression via un id
        /// </summary>
        /// <param name="id">Identifiant dans le core SolR</param>
        public void Delete(string id)
        {
            SolrEngine.SolrUpdate(SolRUrl, SolRCore, "<delete><query>id:" + id + "</query></delete>");
        }

        /// <summary>
        /// Supprime toutes les versions autres que celle passée en paramètre
        /// </summary>
        /// <param name="version">Version à conserver</param>
        public void DeleteVersion(string version)
        {
            SolrEngine.SolrUpdate(SolRUrl, SolRCore, "<delete><query>-data_version:" + version + "</query></delete>");
        }

        /// <summary>
        /// Classe permettant de créer une nouvelle entrée field dans le fichier XML d'indexation SolR
        /// </summary>
        /// <param name="doc">Document XML en cours de création</param>
        /// <param name="name">Nom du paramètre</param>
        /// <param name="value">Valeur du paramètre</param>
        /// <param name="updateAttribute">Option : none, set ou add</param>
        public void AddFieldNode(ref XmlNode doc, string name, string value, updateAttributeValues updateAttribute = updateAttributeValues.none)
        {
            XmlNode node = doc.AppendChild(doc.OwnerDocument.CreateElement("field")); // description d'un champs
            node.Attributes.Append(doc.OwnerDocument.CreateAttribute("name")).Value = name;
            if (updateAttribute != updateAttributeValues.none) node.Attributes.Append(doc.OwnerDocument.CreateAttribute("update")).Value = updateAttribute.ToString();
            node.InnerText = value;
        }

        public string GetQueryStringFromParam(XmlNode param)
        {
            return SolrEngine.GetQueryStringFromParam(param);
        }

        /// <summary>
        /// Option de mise à jour pour une champ SolR
        /// </summary>
        public enum updateAttributeValues
        {
            /// <summary>
            /// Sans option
            /// </summary>
            none,
            /// <summary>
            /// Assignation d'une valeur
            /// </summary>
            set,
            /// <summary>
            /// Ajout d'une nouvelle valeur
            /// </summary>
            add
        }
        #endregion

        public string CleanString(string s)
        {
            if (s == null) return null;

            s = s.Replace(".", " ");
            s = s.Replace(",", " ");
            s = s.Replace(";", " ");
            s = s.Replace("/", " ");
            s = s.Replace(@"\", " ");
            s = s.Replace("(", " ");
            s = s.Replace(")", " ");
            s = s.Replace("{", " ");
            s = s.Replace("}", " ");
            s = s.Replace("[", " ");
            s = s.Replace("]", " ");
            s = s.Replace("<", " ");
            s = s.Replace(">", " ");
            s = s.Replace("-", " ");
            s = s.Replace(":", " ");
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " "); //Suppression des doubles espaces et saut de lignes

            return s;
        }

        /// <summary>
        /// Formate la date dans le format attendu par SolR
        /// </summary>
        /// <param name="dateValue">Date en entrée</param>
        /// <returns>Chaine reconnue par SolR</returns>
        public string FormatDate(DateTime dateValue)
        {
            return dateValue.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
