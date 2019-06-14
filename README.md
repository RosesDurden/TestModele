Mode d'emploi du projet modèle MVC-entity

# Historique
v0.1 19/07/2016 (Julien Legrand) : 1ère version


# Démarrage d'un nouveau projet avec le modèle : pas-à-pas

**0. Visual Studio**
* 0.1. Avant toute chose, vérifier que le gestionnaire de packages Nuget est à jour dans Visual Studio
* 0.2. Si nécessaire, executer "console manager nuget" et accepter avec "T" les messages dans l'écran console
* 0.3. Au besoin, changer la politique Powershell du poste quant à la signature du code comme expliqué ici : "http://codebox/snippets/4"

**1. GIT**
* 1.1. Cloner le projet modèle dans le dossier de votre choix : "git clone [adresse du projet]" à faire dans le dossier parent (le dossier CD67.ModeleMVC sera créé automatiquement)
* 1.2. Renommer le dossier du projet avec le nom de votre choix
* 1.3. Supprimer le sous-dossier caché ".git" à la racine du projet pour supprimer le lien avec le projet modèle
* 1.4. Initialiser un nouveau dépôt GIT : "git init" à faire dans le dossier du projet
* 1.5. C'est le bon moment pour faire un premier commit : "git add ." et "git commit -m "NEW Reprise du projet modèle #indiquez ici le dernier numéro de commit du projet modèle, pour identifier sa version#""
* 1.6. Créer un nouveau projet sur CodeBox et copier l'adresse pour GIT (SSH ou HTTP)
* 1.7. Ajout de la référence du repository en ligne : "git remote add origin [adresse du nouveau projet]"
* 1.8. 1er envoi sur le serveur : "git push --set-upstream origin master" (par la suite, "git push" suffira)

**2. Renommer et exécuter la maquette :**
* 2.1. Ouvrir le projet dans Visual Studio et renommer la solution et les projets en suivant ce schéma : "CD67.[nom appli].[Entity/Factory/MVC/Tests/Batchs]"
* 2.2. Renommer également les assemblies et espaces de nom de chaque projet (dans les propriétés des projets, onglet "Application")
* 2.3. Quitter Visual Studio
* 2.4. Supprimer si nécessaire tous les fichiers commencant par "CD67.ModeleMVC.*" dans les dossiers suivants : "\CD67.ModeleMVC.MVC\bin", "\CD67.ModeleMVC.Factory\bin\Debug", "\CD67.ModeleMVC.Entity\bin\Debug", "CD67.ModeleMVC.Tests\bin\Debug"
* 2.5. Modifier le noms des dossiers des projets en conséquence et modifier le chemin des fichiers projets dans le fichier solution *.sln
* 2.6. Ouvrir à nouveau la solution et faire un rechercher/remplacer global à la solution pour remplacer "CD67.ModeleMVC" par "CD67.[nom appli]"
* 2.7. Nettoyer, régénérer la solution (ce qui va restaurer les packages Nuget)
* 2.8. Définir le projet "CD67.ModeleMVC.MVC" comme projet de démarrage
* 2.9. Exécuter la pour contrôler qu'elle fonctionne correctement à ce stade avec les données de tests (noter dans le code les exemples à disposition)
* 2.10. Supprimer les anciennes classes d'exemples :
  * Entity : ouvrir avec l'éditeur graphique le model Entity : "CD67.ModeleMVC.Entity\EntityModel.edmx" et supprimer les tables d'exemples
  * MVC : faire un clic droit sur fichier "CD67.ModeleMVC.Factory\Internal\GenericFactories.tt" et sélectionner "Exécuter un outil personnalisé"
  * Entity : supprimer les classes partielles : "CD67.ModeleMVC.Entity\Extend\Viking.cs" et "CD67.ModeleMVC.Entity\Extend\TypeViking.cs"
  * Factory : supprimer les fichiers "Factory" : "CD67.ModeleMVC.Factory\VikingFactory.cs" et "CD67.ModeleMVC.Factory\TypeVikingFactory.cs"
  * MVC : supprimer les contrôleurs et vues "PrintTest", "TypeViking" et "Viking" dans : "CD67.ModeleMVC.MVC\Controllers" et "CD67.ModeleMVC.MVC\Views"
  * MVC : editer le "CD67.ModeleMVC.MVC\Views\shared\_Layout.cshtml" et supprimer les éléments de menu qui n'existent plus (lignes 92 à 109)
  * MVC : editer le fichier "Mvc.sitemap" pour supprimer les pages qui n'existent plus
  * MVC : editer le fichier "CD67.ModeleMVC.MVC\Views\Home\Index.cshtml" et supprimer le sous-menu (lignes 10 à 18)
* 2.11. Supprimer le fichier Readme et le dossier "Solution items" (le fichier Readme est aussi à supprimer directement dans le dossier)
* 2.12. Renommer le nom de l'application dans le layout ici : "CD67.ModeleMVC.MVC\Views\shared\_Layout.cshtml" (ligne 51)
* 2.13. Un petit commit avant de continuer : "git add ." et "git commit -m "NEW Renommage du projet modèle""

**3. Nouvelle connexion :**
* 3.1. Pour SQL server :
      * Mettre à jour la chaine de connexion SQLserver dans les fichiers : "CD67.ModeleMVC.Entity\App.config", "CD67.ModeleMVC.MVC\Web.config" et "CD67.ModeleMVC.Tests\App.config"
	  * Modifier le fichier "EntityModel.edmx" avec l'éditeur XML pour n'activer que le provider SQL server
* 3.2. Option (fortement recommandé pour SQL server) : il est possible d'ajouter une projet de type base de données pour gérer votre base de données dans la même solution (en vous connectant avec la chaine de connexion mis à jour au point précédent), nom : CD67.[nom appli].BDD
* 3.3. C'est le moment du commit : "git add .", "git commit -m "NEW Initialisation des connexions"" et push de cette branche "git push"
* 3.4. A ce stade le nouveau projet est prêt à réellement démarrer, on créer donc une nouvelle branche "develop" : "git branch develop" et "git checkout develop"
* 3.5. On pousse la nouvelle branche en ligne : "git push --set-upstream origin develop" (par la suite, "git push" suffira)

**4. Démarrage du nouveau projet :**
* 4.1. Mettre à jour le modèle par rapport à la base de données pour ajouter de nouvelles tables et vues nécessaire à l'application
* 4.2. Vérifier les types des entités générées depuis la base de données, particulièrement les chiffres et clés en numéro auto
* 4.3. Créer vos propres fichiers d'extension avec DataAnnotation ici : "CD67.ModeleMVC.Entity\Extend"
* 4.4. Des Factories génériques sont automatiquement générées pour toutes entités dans le modèle ici : "CD67.ModeleMVC.Factory\Internal"
* 4.5. Créer vos propres factory pour ajouter de nouvelles méthodes ou redéfinir les méthodes de base ici : "CD67.ModeleMVC.Factory"
* 4.6. Créer vos propres controller/vues ici : "CD67.ModeleMVC.MVC\Controllers" et "CD67.ModeleMVC.MVC\Views"
(NOTE : personellement j'utilise à la création du controller le modèle "Contrôleur MV5 avec vues, utilisant Entity Framework" en générant les vues et avec la page de disposition que je souhaite, ensuite je modifie le code du contrôleur pour utiliser les classes "Factory")
* 4.7. Modifier le fichier "CD67.ModeleMVC.MVC\App_Start\RouteConfig.cs" par rapport à vos contrôleurs et variables nécessaires
* 4.8. Editer le fichier "Mvc.sitemap" pour ajouter vos nouvelles pages (ne pas oublier les paramètres), cela permet d'ajouter automatiquement le fil d'Arianne
* 4.9. Ajout des tests unitaires pour tester les fonctions principales dans le projet dédié "CD67.ModeleMVC.Tests" (CRUD sur les classes, processus complet, etc.)
* 4.10. On fait des commits/push à chaque avancée majeure, on fait une fusion avec la branche "master" dès que l'outil est en production, puis son continue les développements sur "develop"


# Description générale
La solution est consituée de 4 projets :
- CD67.ModeleMVC.Entity : Projet qui contient les objets métiers, c'est à dire dans le cas de projets Entity : le modèle entity framework
- CD67.ModeleMVC.Factory : Projet qui contient les classes permettant la gestion des objets : les actions CRUD pour chacun à minima
- CD67.ModeleMVC.MVC : Projet qui comprend le site Web MVC, les packages Bootstrap et FontAwesome sont déjà installés
- CD67.ModeleMVC.Tests : Tests unitaires


# Description du projet : CD67.ModeleMVC.Entity
Le modèle se nomme par défaut "EntityModel".

Le dossier "Extend" contient les extensions éventuelles de classe Entity (qui se trouvent dans "EntityModel.edmx\EntityModel.tt\").
C'est notamment utile pour ajouter les DataAnnotations permettant de décrire le données en vue d'une génération de contrôles plus appropiés dans les vues MVC.
Attention à bien surveiller l'espace de noms lors de la création d'une classe dans ce sous-dossier (erreur fréquente) :
- par défaut ce sera : "CD67.ModeleMVC.Entity.Extend",
- mais il faut "CD67.ModeleMVC.Entity" (le même que les classes de bases Entity).

Le dossier "Internal" contient :
- "FormattedDbEntityValidationException.cs" : classe héritée de Exception qui permet un affichage plus complet dans les messages d'erreur pour les exceptions liées au format de données du modèle Entity
- "Entities.cs" : classe partielle permettant d'utiliser les nouvelles Exceptions FormattedDbEntityValidationException en surchargeant "SaveChanges()"
En cas d'ajout d'une nouvelle connexion avec Entity Framework, il faut ajouter une nouvelle classe d'extension du même type.


# Description du projet : CD67.ModeleMVC.Factory
Les classes Factory sont nommées ainsi : "[Nom objet]Factory"
Les factories permettent de gérer le cycle de vie des objets

Le dossier "Internal" contient :
- "GenericFactories.tt" : il s'agit d'un modèle T4, celui-ci permet la génération automatique de factories pour toutes les entités du modèle edmx situé ici : "../CD67.ModeleMVC.Entity/EntityModel.edmx", les classes générées sont des classes partielles qui peuvent être complété pour ajouter du code spécifique (méthodes supplementaires ou redéfinition des méthodes de bases)
- "baseFactory.cs" : Le dossier "Internal" contient la classe de base "baseFactory" dont chaque "Factory" hérite. Cette classe implémente pour toutes les factories générées les méthodes : getById, getBy, getAll, where, add, update, delete


# Description du projet : CD67.ModeleMVC.MVC
La partie Model du projet n'est pas nécessaire que pour définir des classes ne servant qu'à l'affichage, les classes mêtiers étant dans le projet Entity.
Celui-ci contient au départ le modèle d'un utilisateur connecté : "UtilisateurConnecte.cs".

Le dossier "Views" contient par défaut :
- "Shared\_Layout.cshtml" : une page de disposition pour l'application
- "Shared\_Flash.cshtml" : page de disposition partielle pour les messages flash
- "DisplayTemplates\YesNoBool.cshtml" et "EditorTemplates\YesNoBool.cshtml" ou "DisplayTemplates\YesNoInt.cshtml" et "EditorTemplates\YesNoInt.cshtml" : il s'agit d'exemples de personnalisation d'affichage pour un type de champ, ici "YesNo". On attribut un type avec la DataAnnotation "[UIHint("YesNoBool")]" dans la classe Metadata et ce sont les fichiers dans DisplayTemplates et EditorTemplates du même nom qui seront utilisés pour créer le contrôle à l'affichage.
- "DisplayTemplates\Date.cshtml" et "EditorTemplates\Date.cshtml" : affichages spécifiques pour ajouter un sélecteur de dates basé sur Jquery et homogène sur tous les navigateurs.

Le dossier "Content" contient :
- un sous dossier "images" destiné à contenir toutes les images utilisées dans la mise en page
- "cd67-custom.css" : feuille de style à modifier pour le projet courant
- "cd67-model.css" : feuille de style du modèle CD67 à ne pas modifier

Le dossier "Scripts" contient :
- "cd67-autofocus.js" : un script permettant le focus sur un contrôle automatiquement au chargement de la page
- "cd67-CancelAlert.js" : des fonctions pour ajouter des messages d'alertes lors de la sortie d'un écran
- "cd67-Jquery.UI.DatePicker.js" : fichier nécessaire aux sélecteur de dates basé sur Jquery
- "cd67-listes.js" : script nécessaire à l'utilisation de listes imbriquées

Les scripts et styles sont chargés à l'aide des commandes : "@Scripts.Render("~/bundles/jquery")", "@Scripts.Render("~/bundles/bootstrap")" et "@Styles.Render("~/Content/css")".
La configuration de ceci se trouve dans ce fichier : "App_Start\BundleConfig.cs"

Les Glyphicons Bootstrap sont ici : http://getbootstrap.com/components/#glyphicons

Et les icônes FontAwesome là : http://fontawesome.io/icons/

Le dossier "Internal" contient :
- "Navigation.cs" : code nécessaire à la création du fil d'Arianne
- "UtilisateurConnecteFactory.cs" : code permettant l'accès aux données AD de l'utilisateur courant
- "MvcHtmlHelpers.cs" : code permettant l'ajout de la commande "@Html.DescriptionFor" dans les vues
- "FlashMessageExtensions.cs" : code permettant l'ajout d'extension pour les controllers afin d'intégrer lé création de messages flash


# Description du projet : CD67.ModeleMVC.Tests
Projet le plus important de la solution :)
