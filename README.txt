README pour la section Infrastructure

Pour Faire une migration :

Ouvrir le Package Manager Console.
Se d�placer dans ParentEspoir.Persistence : cd ParentEspoir.Persistence
Saisir : 
dotnet ef --startup-project ../ParentEspoir.WebUI migrations add "myMigrations"

Pour faire Update-database :

Ouvrir le Package Manager Console.
S'assurer que que le projet par d�faut est bien Presentation/ParentEspoir.WebUI (liste d�roulante en haut de la console)
Saisir : update-database 
