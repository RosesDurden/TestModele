using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;

namespace CD67.ModeleMVC.MVC.Internal
{
    public static class UtilisateurConnecteFactory
    {
        /// <summary>
        /// Retourne un objet UtilisateurConnecté avec les informations prnvenant de l'AD pour un utilisateur :
        /// nom , prenom , libellé (displayname), email , login, employéID (AstreRH), GUID, SID, Structure hiérarchique
        /// </summary>
        /// <param name="SID">SID de l'utilisateur souhaité</param>
        /// <returns>Utilisateur correspondant au SID</returns>  
        public static Models.UtilisateurConnecte getUtilisateur(string SID)
        {
            try
            {
                DirectoryEntry userEntry = getUserEntry(SID);
                Models.UtilisateurConnecte utilisateur = new Models.UtilisateurConnecte()
                {
                    nom = GetValue(userEntry.Properties["sn"]),
                    prenom = GetValue(userEntry.Properties["givenName"]),
                    libelle = GetValue(userEntry.Properties["displayName"]),
                    email = GetValue(userEntry.Properties["mail"]),
                    login = GetValue(userEntry.Properties["sAMAccountName"]),
                    employeeID = userEntry.Properties["employeeID"].Value == null ? (int?)null : int.Parse(userEntry.Properties["employeeID"].Value.ToString()),
                    guid = userEntry.Properties["objectGUID"].Value == null ? (Guid?)null : new Guid((byte[])userEntry.Properties["objectGUID"].Value),
                    sid = new SecurityIdentifier((byte[])userEntry.Properties["objectSid"].Value, 0).ToString(),
                    structure = String.Join(@"\", getOUs(userEntry))
                };

                return utilisateur;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'interrogation AD", ex);
            }
        }

        /// <summary>
        /// Retourne un objet UtilisateurConnecté avec les informations provenant de l'AD pour l'utilisateur courant :
        /// nom , prenom , libellé (displayname), email , login, employéID (AstreRH), GUID, SID, Structure hiérarchique
        /// Une fois récupéré, les valeurs sont stockées en variable de session, et ne seront récupérées à nouveau depuis l'AD que si nécessaire
        /// </summary>
        /// <returns>L'utilisateur connecté</returns>
        public static Models.UtilisateurConnecte getUtilisateurConnecte()
        {
            //On récupère l'utilisateur depuis le cache de session
            Models.UtilisateurConnecte utilisateurConnecte = HttpContext.Current.Session["UtilisateurConnecte"] as Models.UtilisateurConnecte;

            //S'il est vide, on le met à jour
            if (utilisateurConnecte == null)
            {
                try
                {
                    DirectoryEntry userEntry = getUserEntry();
                    utilisateurConnecte = new Models.UtilisateurConnecte()
                    {
                        nom = GetValue(userEntry.Properties["sn"]),
                        prenom = GetValue(userEntry.Properties["givenName"]),
                        libelle = GetValue(userEntry.Properties["displayName"]),
                        email = GetValue(userEntry.Properties["mail"]),
                        login = GetValue(userEntry.Properties["sAMAccountName"]),
                        employeeID = userEntry.Properties["employeeID"].Value == null ? (int?)null : int.Parse(userEntry.Properties["employeeID"].Value.ToString()),
                        guid = userEntry.Properties["objectGUID"].Value == null ? (Guid?)null : new Guid((byte[])userEntry.Properties["objectGUID"].Value),
                        sid = new SecurityIdentifier((byte[])userEntry.Properties["objectSid"].Value, 0).ToString(),
                        structure = String.Join(@"\", getOUs(userEntry))
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur lors de l'interrogation AD", ex);
                }
            }
            HttpContext.Current.Session["UtilisateurConnecte"] = utilisateurConnecte;

            return utilisateurConnecte;
        }

        /// <summary>
        /// Retourne l'entrée dans l'AD de l'utilisateur courant.
        /// Exemple d'accès aux propriétés : userEntry.Properties["givenName"].Value
        /// </summary>
        /// <returns>Entrée utilisateur</returns>
        private static DirectoryEntry getUserEntry(string SID = null)
        {
            try
            {
                DirectoryEntry ldap = new DirectoryEntry("LDAP://CG67.fr", "LectureAD", "Adlecture!");
                DirectorySearcher searcher = new DirectorySearcher(ldap);

                if (SID == null)
                {
                    string login = HttpContext.Current.User.Identity.Name.ToLower().Replace("cg67\\", "");
                    if (login == null) throw new Exception("Impossible de récupérer le login de l'utilisateur courant.");
                    searcher.Filter = $"(sAMAccountName={login})";
                }
                else searcher.Filter = $"(objectSid={SID})";
                SearchResult result = searcher.FindOne();

                DirectoryEntry userEntry = result.GetDirectoryEntry();
                return userEntry;
            }
            catch (NullReferenceException ex)
            {
                throw new Exception("Le nom d'utilisateur courant n'a pas été retrouvé dans l'AD.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur dans la récupération des infos de l'utilisateur.", ex);
            }
        }

        private static List<string> getOUs(DirectoryEntry dirEntry)
        {
            List<string> res = Regex.Matches(dirEntry.Path, @"OU=[^,]+").Cast<Match>().Select(match => match.Value.Substring(3)).ToList();
            if (res != null)
            {
                if (res.Count > 1) res.Reverse();
                if (res[0] == "Organisation") res.RemoveAt(0); //Suppression du premier niveau "Organisation"
            }
            else res.Add("");

            return res;
        }

        private static string GetValue(PropertyValueCollection item)
        {
            return item?.Value == null ? "" : item.Value.ToString();
        }
    }
}
