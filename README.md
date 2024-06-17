# P7CreateRestApi

Installez .Net Core 8 sur votre machine.

Restaurez les packages NuGet dans la Console du gestionnaire de package avec: 

Update-Package -reinstall

Dans l'onglet Générer faite Générer la solution, cette commande compile le projet et vérifie s’il y a des erreurs de compilation.

Pour créer correctement la base de données, modifier les chaînes de connexion pour qu'elles pointent vers le serveur MSSQL fonctionnant sur votre PC local.

**Prérequis** : MSSQL Developer 2019 ou Express 2019 a été installé ainsi que Microsoft SQL Server Management Studio (SSMS).

MSSQL : https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads

SSMS : https://docs.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16

*Remarque : les versions antérieures de MSSQL Server devraient fonctionner sans problèmes, mais elles n’ont pas été testées.

*Dans le projet P7CreateRestApi, ouvrez le fichier appsettings.json file.*

Vous verrez la section ConnectionStrings qui définit les chaînes de connexion pour la base de données utilisée dans cette application.

      "ConnectionStrings":
      {
        "DefaultConnection": "Server=.;Database=VOTRE BASE DE DONNÉES;Trusted_Connection=True;MultipleActiveResultSets=true"
      }

Ouvrez la console du gestionnaire de package puis tapez la commande suivante pour ajouter une migration initiale : 

Add-Migration "Votre message"

Update-Database

Exécuter le projet, Automatiquement et uniquement un User "Admin" avec le mot de passe "Sy4oSfGDBWZJ8hcwOG?h$V&" et le rôle "Admin" seras créé.

Notez que le projet est configuré avec un logging dans le Program.cs et qui créer un fichier nommé “logs{année mois}” dans le répertoire “Logs” donc le niveau de logging minimum et "information" et tous les logs de niveau Information, Warning, Error et Critical sont également enregistrés, et que ce nouveau fichier sera créé chaque mois.



