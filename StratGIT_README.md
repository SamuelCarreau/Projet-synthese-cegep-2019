# Stratégie GIT avec Team Explorer

### Premièrement, allez dans les "settings" du Repository sur Team explorer

> 1. Project
> 2. Settings
> 3. Repository Settings
> 4. Prune Remote Branch during fetch = True
>    **Les branches du remote reflèteront maintenant ce qui se retrouve réellement sur DevOps**

### Deuxièmement, créez une branche correspondant à votre User Story sur Azure DevOps

> 1. Azure DevOps
> 2. Repos
> 3. Branches
> 4. New Branch
> 5. Normalement basée sur Master
> 6. Link work items (Story & Tasks)

### Troisièmement, retournez sur Visual Studio Team Explorer

> 1. Dans Team Explorer -> Home
> 2. Onglet SYNC
> 3. Sync / Fetch
> 4. Checkout sur votre nouvelle branche
> 5. Vous êtes maintenant prêts à travailler et faire des push

# IMPORTANT, consultez ce lien pour comprendre la commande **git rebase**

> https://www.atlassian.com/git/tutorials/rewriting-history/git-rebase

#### Pour être certain d'avoir les dernières modifications se retrouvant sur master

#### Il est important d'utiliser _git rebase_ afin de garder un historique clean

#### et par conséquent drastiquement améliorer les recherches dans le log (Bonnes pratiques svp)
