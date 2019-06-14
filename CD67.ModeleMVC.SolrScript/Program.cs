using CD67.ModeleMVC.Entity;
using CD67.ModeleMVC.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD67.ModeleMVC.Solr;

// Script d'indexation global
// > Utilise la DLL Solr
// Pour réutiliser, remplacer Viking et vikingFactory par les noms qui correspondent au projet

namespace CD67.ModeleMVC.SolrScript
{
    class Program
    {
        static string SolrUrl = "http://t-lunr4:8080/solr/modele-mvc";

        /// <summary>
        /// Lance l'indexation de tous les objets Vikings
        /// </summary>
        static void Main(string[] args)
        {
            // Instancie la base de données
            ModeleMVCEntities db = new ModeleMVCEntities();

            // Génère un GUID pour versionner les données issues de cette indexation
            string version = Guid.NewGuid().ToString();
            Console.WriteLine("Version index:" + version);

            // Parcourt les vikings de la base et les ajoute à l'index au fur et à mesure
            VikingFactory vikingsFactory = new VikingFactory(db);
            VikingsIndexer vikingsIndexer = new VikingsIndexer(db);
            vikingsIndexer.Add(vikingsFactory.GetAll().ToList(), version, false);

            // Supprime les anciennes données en se basant sur le numero de version
            Console.WriteLine("Nettoyage anciennes versions...");
            vikingsIndexer.DeleteVersion(version);

            // Envoie les requetes de commit et d'optimisation de l'index
            Console.WriteLine("Commit et optimisation");
            vikingsIndexer.Commit();
            vikingsIndexer.Optimize();

            Console.WriteLine("Indexation terminée, presser une touche pour fermer.");
            Console.ReadKey();
        }
    }
}
