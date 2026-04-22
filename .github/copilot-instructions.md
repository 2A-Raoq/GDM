# Instructions GitHub Copilot — ProjetGMP

Si une demande contredit ces règles ou manque d’informations, poser 1–3 questions
courtes avant d’implémenter.

## Règles non négociables
- **Sécurité** : ne jamais écrire/commiter de secrets (tokens, mots de passe,
  clés API, credentials). Utiliser des variables d’environnement, des fichiers
  d’exemple (`.env.example`) et `.gitignore`.
- **Git** : Git obligatoire. **GitFlow** (1 branche par feature). Interdiction de
  push sur `master`/`main` (PR obligatoire).
- **TDD 100%** : appliquer *Red → Green → Refactor* pour toute fonctionnalité ou
  correction de bug.
- **Architecture** : appliquer une **Clean Architecture**.
- **Qualité** : code professionnel et maintenable ; pas de « code moche ».

## Clean Architecture (attendus)
- Séparer clairement :
  - **Domain** : règles métier pures (aucune dépendance framework/IO).
  - **Application** : cas d’usage, orchestration, interfaces (ports).
  - **Infrastructure** : DB, réseau, fichiers, intégrations (adapters).
  - **Interfaces/UI** : contrôleurs, handlers, présentation.
- Dépendances orientées vers l’intérieur (Domain n’importe rien de l’extérieur).
- Favoriser l’injection de dépendances et les interfaces aux singletons.

## TDD (attendus)
- Commencer par écrire un test qui échoue (Red), implémenter le minimum (Green),
  puis refactor (Refactor) en gardant les tests verts.
- Tests : nommage explicite, arrangement clair (AAA), assertions utiles,
  pas de tests fragiles (éviter de tester des détails d’implémentation).

## Standards de code
- **Fonctions** : maximum **50 lignes**.
- **Largeur** : maximum **120 colonnes**.
- **Complexité** : **CCN ≤ 5** (cyclomatic complexity).
- **CRAP score ≤ 25**.
- Préférer des fonctions petites, pures quand possible, et un code lisible.
- Pas d’émoticônes dans le code.

## Framework & documentation
- Respecter la **documentation officielle** des frameworks/outils.
- Cibler **la version majeure 11.x** du framework du projet :
  - ne pas introduire d’APIs/features nécessitant une version > 11.x.
  - si un doute existe sur le framework ou ses versions, demander clarification.

## Commandes / scripts
- Les commandes et scripts d’outillage doivent être en **Python** lorsque
  l’automatisation est nécessaire (compatibles Windows).

## Documentation
- Mettre à jour la documentation **au fur et à mesure** (README, docs, ADR si
  pertinent). Toute nouvelle feature doit avoir sa doc d’usage.

## Commits
- Respecter le standard de commit de l’équipe.
- À défaut de standard explicite, utiliser **Conventional Commits** :
  `feat: ...`, `fix: ...`, `test: ...`, `refactor: ...`, `docs: ...`, `chore: ...`.

## Direction artistique (DA)
- Respecter la DA : **thème futuriste 2D vectoriel**.
- Ne pas introduire de nouvelles palettes/couleurs/shadows arbitraires.

## Comportement attendu de Copilot
- Ne pas inventer des exigences ; proposer la solution la plus simple conforme.
- Minimiser les dépendances ; privilégier standard library/solutions éprouvées.
- En cas de conflit entre objectifs (ex. vitesse vs qualité), privilégier les
  règles de qualité et TDD.
