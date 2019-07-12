Veuillez vous référez au projet NordWind Trader afin de bien comprendre le document

Ceci est l'"application layer" du projet NordWind
Il dépend du reste du core mais ne devrez avoir aucune autre dépendance.

Cette partie permet de défénir les interfaces qui pourront être implémenter dans des
couches externes.

Pour le regex utiliser partout dans le Document, voici comment le comprendre:

^([\u00c0-\u01ffa-zA-Z'\-])+$   
	: Ça comprends tout les letters possibles de l'utf 8

^([\u00c0-\u01ffa-zA-Z'\-\s])+$
	: Même chose que le précédent mais accepte les espaces en plus