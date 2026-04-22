# Constitution du projet

## 1. Objet

Cette constitution définit les règles obligatoires de développement du projet.

Elle a pour but de garantir :

* un code professionnel ;
* une architecture maintenable ;
* une qualité constante ;
* un haut niveau de sécurité ;
* une documentation à jour ;
* un workflow d’équipe clair et discipliné.

Toute contribution au projet doit respecter ce document.

---

## 2. Principes généraux

* Le projet doit produire un code lisible, maintenable, testable et professionnel.
* La simplicité est préférée à la complexité inutile.
* La sécurité prime sur la rapidité d’implémentation.
* Toute décision technique importante doit être explicitée.
* La qualité ne se négocie pas en fin de projet : elle s’applique dès le départ.
* Le code doit servir le produit, pas démontrer une complexité technique inutile.

---

## 3. Architecture

### 3.1 Architecture obligatoire

Le projet suit une **Clean Architecture**.

La séparation minimale attendue est :

* `Domain`
* `Application`
* `Infrastructure`
* `UI`

### 3.2 Règles d’architecture

* Le domaine ne dépend d’aucun framework.
* Le domaine ne dépend d’aucun accès disque, réseau ou UI.
* L’application orchestre les cas d’usage.
* L’infrastructure implémente les dépendances techniques.
* L’UI ne contient pas la logique métier.
* L’injection de dépendances est concentrée dans la composition root.

### 3.3 Objectifs

Cette organisation doit permettre :

* une forte testabilité ;
* une bonne lisibilité ;
* une meilleure évolutivité ;
* une séparation claire entre métier et technique.

---

## 4. Stack technique

### Stack cible

* **Langage principal** : C#
* **Plateforme** : .NET 10 (LTS)
* **Framework UI** : Avalonia UI 11.x
* **Pattern UI** : MVVM

### Règles complémentaires

* Le code applicatif principal est écrit en C#.
* Les scripts d’automatisation, de tooling ou d’analyse peuvent être écrits en Python.
* Toute dépendance externe importante doit être justifiée.
* Toute dépendance sensible doit être évaluée sous l’angle sécurité, maintenance et coût de complexité.

---

## 5. Qualité du code

### Règles obligatoires

* Une fonction ne doit pas dépasser **50 lignes**.
* Une ligne ne doit pas dépasser **120 colonnes**.
* **CCN max : 5**
* **CRAP score max : 25**

### Attendus

* Le code doit être clair, explicite et bien nommé.
* Les effets de bord cachés doivent être évités.
* Le code mort doit être supprimé.
* Le code opaque ou inutilement complexe est interdit.
* Les commentaires doivent expliquer le **pourquoi**, pas répéter le **quoi**.
* Les warnings de build doivent être traités ou justifiés explicitement.

### Interdits

* méthodes fourre-tout ;
* logique métier noyée dans l’UI ;
* dépendances circulaires ;
* duplication évitable ;
* hacks non documentés.

---

## 6. Tests

### Règle principale

Le projet suit le **TDD obligatoire** :

**Red → Green → Refactor**

### Règles de test

* Aucun développement métier ne doit être validé sans test automatisé.
* Les tests unitaires couvrent en priorité `Domain` et `Application`.
* Les composants critiques d’`Infrastructure` doivent être couverts par des tests d’intégration.
* Tout bug corrigé doit être accompagné d’un test reproduisant le bug.
* Les tests doivent rester lisibles, rapides et fiables.
* Les tests fragiles ou non déterministes doivent être évités.

### Objectif

Le TDD doit être un outil de conception, pas une formalité.

---

## 7. Git et workflow

### Règles obligatoires

* Git est obligatoire.
* Une branche par fonctionnalité, correction ou refactor important.
* Les pushes directs sur `main` ou `master` sont interdits.
* Une Pull Request est obligatoire avant fusion.
* Une revue de code est obligatoire avant merge.
* Les commits doivent suivre un standard cohérent, par exemple **Conventional Commits**.

### Workflow recommandé

* branche courte et ciblée ;
* PR petite et relisible ;
* description de PR claire ;
* lien vers la user story ou la tâche concernée ;
* tests passants avant revue.

---

## 8. Sécurité

### Principes

* Aucun secret ne doit être versionné.
* Aucun token, mot de passe, clé API ou fichier sensible ne doit être poussé sur Git.
* Les fichiers de secrets locaux doivent être ignorés.
* Aucun log ne doit contenir :

  * mot de passe,
  * secret,
  * clé dérivée,
  * donnée déchiffrée,
  * contenu de coffre en clair.

### Exigences

* Les choix cryptographiques doivent être documentés.
* Les décisions de sécurité importantes doivent faire l’objet d’un ADR.
* Les données sensibles doivent être manipulées avec prudence en mémoire et sur disque.
* Toute fonctionnalité réseau future doit être conçue avec un modèle de menace explicite.

### Interdits

* implémentation crypto improvisée sans documentation ;
* stockage en clair d’informations sensibles ;
* fuite involontaire de secrets dans les erreurs ou logs.

---

## 9. Documentation

### Règles

* La documentation est mise à jour au fur et à mesure.
* Toute décision d’architecture importante doit être documentée.
* Toute nouvelle fonctionnalité doit mettre à jour, si nécessaire :

  * le backlog,
  * le GDD,
  * le plan de développement,
  * les ADR concernés.

### Principe

Une fonctionnalité n’est pas terminée tant que sa documentation minimale n’est pas à jour.

---

## 10. Direction artistique et UX

### Direction artistique

Le projet doit respecter une identité :

* futuriste ;
* 2D vectorielle ;
* propre ;
* lisible ;
* cohérente.

### Règles

* La DA ne doit jamais nuire à la lisibilité.
* Les contrastes doivent rester suffisants.
* L’accessibilité minimale doit être prise en compte.
* La cohérence visuelle doit être maintenue entre les écrans.

---

## 11. Outils et automatisation

### Règles

* Les scripts d’automatisation peuvent être écrits en Python.
* Les scripts importants doivent être documentés.
* Les commandes récurrentes doivent être regroupées dans une documentation de contribution.
* L’automatisation doit aider à sécuriser le projet :

  * lint,
  * build,
  * tests,
  * contrôles de qualité.

---

## 12. Definition of Done

Une tâche est considérée terminée uniquement si :

* le code compile ;
* les tests passent ;
* les règles de qualité sont respectées ;
* la revue de code a été faite ;
* la documentation minimale a été mise à jour ;
* aucun secret n’est exposé ;
* les cas d’erreur principaux ont été traités ;
* les impacts sécurité éventuels ont été pris en compte.

---

## 13. Interdictions formelles

* push direct sur `main` / `master` ;
* commit de secrets ;
* fonctionnalité métier sans test ;
* couplage direct UI ↔ infrastructure sensible ;
* dépendance lourde ajoutée sans justification ;
* régression silencieuse non documentée ;
* contournement du workflow de revue ;
* documentation laissée incohérente volontairement.

---

## 14. Règle de cohérence future

Toute évolution du projet, y compris vers une version hybride avec fonctionnalités en ligne, doit respecter cette constitution.

Cela implique notamment :

* maintien de la séparation des couches ;
* maintien du TDD ;
* maintien des exigences de sécurité ;
* documentation préalable des décisions critiques ;
* refus d’introduire des fonctionnalités en ligne qui dégradent le mode local sans justification produit claire.

---