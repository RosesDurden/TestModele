using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD67.ModeleMVC.Solr
{
    /// <summary>
    /// Interface pour les classes de gestion d'index SolR
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISolRDataIndex<T>
    {
        /// <summary>
        /// Ajout (ou mise à jour) d'un élément à l'index SolR
        /// </summary>
        /// <param name="item">Elément à ajouter</param>
        /// <param name="version">Identifiant de la nouvelle version</param>
        /// <param name="autoCommit">Commit à l'issue de l'ajout</param>
        void Add(T item, string version = null, bool autoCommit = true);

        /// <summary>
        /// Suppression d'un élément de l'index SolR
        /// </summary>
        /// <param name="item">Elément à supprimer</param>
        /// <param name="autoCommit">Commit à l'issue de l'ajout</param>
        void delete(T item, bool autoCommit = true);
    }
}
