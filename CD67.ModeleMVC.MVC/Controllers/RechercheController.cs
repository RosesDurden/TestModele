using CD67.ModeleMVC.Entity;
using CD67.ModeleMVC.Solr;
using SolrTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;

namespace CD67.ModeleMVC.MVC.Controllers
{
    public class RechercheController : Controller
    {
        private ModeleMVCEntities db = new ModeleMVCEntities();

        /// <summary>
        /// Recherche via SOLR
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            //######## Decommenter cette partie pour que la page soit vide au premier affichage
            // Si cette partie reste commentée, tous les résultats seront affichés au premier affichage

            //if (!(Request.QueryString.AllKeys.Count() > 0))
            //{
            //    ViewData["xmlData"] = "";
            //    return View();
            //}
            // ####### 

            // Construit la requete solr ("q=...")
            string requeteSolr = BuildSolrRequest(Request);
            VikingsIndexer vikingsIndexer = new VikingsIndexer(db);
            XmlNode xmldata = vikingsIndexer.SolrRequest(requeteSolr);

            // Passe les arguments à la XSLT
            XsltArgumentList argsList = new XsltArgumentList();
            argsList.AddParam("recherche", "", this.Request["recherche"] == null ? "*" : this.Request["recherche"]);
            argsList.AddParam("mode", "", "modele-mvc");
            argsList.AddParam("paramrecherche", "", "recherche");
            argsList.AddParam("configFile", "", "../solr.config.xml");

            ViewData["xmlData"] = ApplyXslt(xmldata, "/xslt/resultat-recherche.xslt", argsList);

            return View();
        }

        /// <summary>
        /// Construit une requête Solr à partir de la requête Http et des appSettings (Web.config)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildSolrRequest(HttpRequestBase request)
        {
            List<string> build = new List<string>();

            // 1. recherche (from HttpRequest)
            string recherche = this.Request["recherche"];
            if (string.IsNullOrEmpty(recherche))
            {
                recherche = "*";
            }
            build.Add(string.Format("q=recherche:{0}", recherche.ToLower()));

            // 2. facettes (from Web.Config)
            string facette_fields = System.Configuration.ConfigurationManager.AppSettings["facette_fields"];
            if (facette_fields.Length > 0)
            {
                build.Add("facet=true");
                foreach (string facette_field in System.Configuration.ConfigurationManager.AppSettings["facette_fields"].Split(' '))
                {
                    build.Add(String.Format("facet.field={0}", facette_field));
                }
                build.Add(String.Format("facet.mincount={0}", System.Configuration.ConfigurationManager.AppSettings["facette_mincount"]));
            }

            // 3. Facettes héritées (from HttpRequest)s
            if (!string.IsNullOrEmpty(request["fq"]))
            {
                var fqs = request["fq"].Split(new[] { ',' });
                foreach (string fq in fqs)
                {
                    var fqval = fq.Split(new[] { ':' });

                    if(fqval[1].ToString().StartsWith("[")) build.Add(String.Format("fq={0}:{1}", fqval[0], fqval[1]));
                    else build.Add(String.Format("fq={0}:\"{1}\"", fqval[0], fqval[1].Replace("\"", "")));
                }
            }

            //// 4. sort
            //build.Add(string.Format("sort={0}", System.Configuration.ConfigurationManager.AppSettings["trierPar"]));

            // 5. nombre de resultats par page
            build.Add(string.Format("rows={0}", System.Configuration.ConfigurationManager.AppSettings["resultatParPage"]));
            
            // 6. Starting point (from HttpRequest)
            try
            {
                build.Add(string.Format("start={0}", int.Parse(request["start"])));
            }
            catch
            {
                build.Add("start=0");
            }

            //7. Facette range
            build.Add(System.Configuration.ConfigurationManager.AppSettings["facette_ranges"]);

            build.Add("echoParams=explicit");

            return string.Join("&", build);
        }

        /// <summary>
        /// Transforme le résultat XML à l'aide d'une feuille de style xslt
        /// </summary>
        /// <param name="xmldata">Resultat de la requete solr au format XML</param>
        /// <param name="xslt">Chemin vers la feuille xslt</param>
        /// <returns></returns>
        private string ApplyXslt(XmlNode xmldata, string xslt, XsltArgumentList args)
        {
            
            // StringBuilder: chaines mutables
            StringBuilder sb = new StringBuilder();

            // Creation et paramétrage d'un XmlWriter
            XmlWriterSettings writer_options = new XmlWriterSettings();
            writer_options.Indent = false;
            writer_options.IndentChars = "\t";
            writer_options.ConformanceLevel = ConformanceLevel.Auto;
            XmlWriter writer = XmlWriter.Create(sb, writer_options);

            // Transformation XSL
            XslCompiledTransform transfo = new XslCompiledTransform();
            // new XsltSettings(true/false, true/false) : chargement des documents et des scripts (à activer au besoin)
            transfo.Load(Server.MapPath(xslt), new XsltSettings(true, false), new XmlUrlResolver());

            using (XmlReader reader = new XmlNodeReader(xmldata))
                transfo.Transform(reader, args, writer);

            writer.Close();
            return sb.ToString();
        }


        /// <summary>
        /// Méthode appelée dynamiquement pour l'autocomplétion
        /// </summary>
        /// <param name="terms">Premiers termes tapés</param>
        /// <returns>Liste de suggestions</returns>
        [HttpGet]
        public string AutoComplete(string terms)
        {
            Uri SolRUrl = new Uri(WebConfigurationManager.AppSettings["SolrUrl"]);

            //if (HttpContext.Request.IsAuthenticated)
            {
                HttpContext.Response.ContentType = "application/json";
                HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                return SolrEngine.SolrAutoCompleteMultiTerm(SolRUrl.AbsoluteUri, "*:*", "recherche", "count", terms.ToLower(), 10);
            }
            //else
            //{
            //    HttpContext.Response.ClearContent();
            //    HttpContext.Response.StatusCode = 401;
            //    HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    return null;
            //}
        }


    }
}