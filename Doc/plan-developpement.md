# Plan de développement — Gestionnaire de mots de passe local et hybride

## 1. Objet du document

Ce document décrit le plan de développement du produit, depuis le **MVP local hors ligne** jusqu’à une **évolution hybride** avec fonctionnalités en ligne optionnelles.

L’objectif est de construire une application desktop sécurisée, maintenable, testable et évolutive, en respectant :

* une architecture propre,
* une séparation stricte des responsabilités,
* une approche TDD,
* une priorité donnée à la sécurité et à la robustesse.

---

## 2. Vision de développement

Le développement est organisé en deux grandes étapes :

### Étape 1 — MVP local

Construire une application desktop **hors ligne**, capable de :

* créer un coffre local chiffré,
* déverrouiller le coffre avec un mot de passe maître,
* ajouter, consulter, rechercher et modifier des entrées,
* générer des mots de passe,
* verrouiller le coffre,
* sauvegarder localement les données de façon sécurisée.

### Étape 2 — Évolution hybride

Faire évoluer le produit vers un mode **local-first avec fonctionnalités en ligne optionnelles**, sans casser le fonctionnement hors ligne :

* compte en ligne optionnel,
* synchronisation entre appareils,
* sauvegarde distante chiffrée,
* gestion des appareils autorisés,
* gestion des conflits de synchronisation,
* maintien d’un usage local même sans réseau.

---

## 3. Hypothèse technique

### Stack cible

* **Langage principal** : C#
* **Plateforme** : .NET 10 (LTS)
* **Framework UI** : Avalonia UI 11.x
* **Pattern UI** : MVVM

### Principes techniques

* Clean Architecture
* séparation `Domain / Application / Infrastructure / UI`
* TDD obligatoire
* tests unitaires et d’intégration
* persistance locale chiffrée
* évolution future vers un backend hybride découplé

---

## 4. Règles de qualité à appliquer dès le départ

Avant tout développement, les règles suivantes sont considérées comme obligatoires :

* code professionnel et maintenable ;
* architecture claire ;
* Git obligatoire ;
* branche par fonctionnalité ;
* aucun push direct sur `main` ou `master` ;
* PR obligatoire avant fusion ;
* documentation mise à jour au fur et à mesure ;
* aucun secret ou credential dans le dépôt ;
* TDD obligatoire : Red → Green → Refactor ;
* fonctions limitées à 50 lignes maximum ;
* 120 colonnes maximum ;
* CCN ≤ 5 ;
* CRAP score ≤ 25 ;
* respect de la direction artistique : thème futuriste 2D vectoriel ;
* respect des standards de commit.

---

## 5. Phases de développement

## Phase 0 — Cadrage, qualité et décisions initiales

### Objectif

Fixer le cadre de développement du projet avant la première ligne de code.

### Actions

* Confirmer le périmètre MVP.
* Confirmer la stack technique.
* Formaliser la Definition of Done.
* Formaliser les règles Git et qualité.
* Définir les premiers ADR :

  * choix cryptographiques,
  * format du coffre,
  * stratégie d’écriture atomique,
  * structure de solution.

### Livrables

* dépôt Git initialisé ;
* structure de documentation ;
* premier ADR sécurité ;
* premier ADR architecture ;
* convention de commits ;
* checklist de PR.

---

## Phase 1 — Bootstrap de la solution

### Objectif

Créer la structure de base du projet.

### Actions

* Créer la solution .NET.
* Créer les projets :

  * `Domain`
  * `Application`
  * `Infrastructure`
  * `UI`
  * `Tests.Domain`
  * `Tests.Application`
  * `Tests.Infrastructure`
* Ajouter les outils de qualité :

  * analyzers,
  * EditorConfig,
  * conventions de style.
* Configurer la composition root dans l’UI.
* Préparer la base MVVM Avalonia.

### Livrables

* solution compilable ;
* architecture vide mais cohérente ;
* pipeline de build local ;
* pipeline de tests local.

---

## Phase 2 — Modèle de domaine

### Objectif

Définir le cœur métier du produit sans dépendances techniques.

### Actions

* Définir les entités et objets de valeur principaux :

  * `Vault`
  * `VaultEntry`
  * `VaultMetadata`
  * `EntryId`
* Définir les invariants métier :

  * `serviceName` obligatoire,
  * `login` obligatoire dans le MVP,
  * `password` obligatoire dans le MVP,
  * accès impossible aux données si le coffre est verrouillé,
  * gestion des dates `createdAt` / `updatedAt`.
* Définir les erreurs métier structurées.

### Livrables

* modèle de domaine testé ;
* règles métier explicites ;
* objets de domaine sans dépendance framework.

---

## Phase 3 — Cas d’usage applicatifs du MVP

### Objectif

Mettre en place les premiers cas d’usage du MVP autour du coffre.

### Actions

* Implémenter :

  * création du coffre,
  * déverrouillage,
  * refus si mot de passe incorrect,
  * verrouillage manuel,
  * verrouillage à la fermeture,
  * changement du mot de passe maître.
* Définir les ports applicatifs nécessaires.
* Centraliser les validations métier applicatives.

### Livrables

* cas d’usage testés ;
* contrats d’application stables ;
* gestion claire des erreurs utilisateur.

---

## Phase 4 — Crypto et format du coffre

### Objectif

Définir une base cryptographique robuste pour le stockage local.

### Actions

* Choisir une stratégie de dérivation de clé robuste.
* Choisir un chiffrement authentifié.
* Définir un format de coffre versionné.
* Définir les métadonnées non sensibles.
* Prévoir la détection de corruption et de falsification.

### Recommandations

* KDF robuste avec paramètres stockés dans les métadonnées ;
* chiffrement authentifié ;
* format versionné ;
* sérialisation stable ;
* stratégie d’évolution future compatible migration.

### Tests attendus

* round-trip chiffrement / déchiffrement ;
* échec sur mauvais mot de passe ;
* échec sur altération des données ;
* stabilité du format sérialisé ;
* robustesse face aux erreurs de lecture.

### Livrables

* service de chiffrement ;
* format de coffre documenté ;
* tests d’intégration de sécurité.

---

## Phase 5 — Persistance locale

### Objectif

Permettre la lecture et l’écriture sécurisées du coffre sur disque.

### Actions

* Définir `IVaultRepository`.
* Implémenter un repository fichier local.
* Gérer :

  * création du fichier,
  * lecture,
  * sauvegarde,
  * remplacement atomique,
  * erreurs d’IO,
  * corruption,
  * coffre absent.
* Définir l’emplacement par défaut du coffre.
* Prévoir l’évolution future vers un emplacement configurable.

### Livrables

* persistance locale testée ;
* écriture atomique ;
* erreurs de lecture / écriture correctement traitées.

---

## Phase 6 — Gestion des entrées

### Objectif

Permettre à l’utilisateur de gérer ses identifiants.

### Actions

* Implémenter :

  * ajout d’entrée,
  * consultation liste,
  * recherche,
  * détail,
  * modification.
* Gérer les validations :

  * champs obligatoires,
  * erreurs structurées,
  * dates de modification.
* Préparer l’évolution future :

  * suppression,
  * tags,
  * catégories,
  * favoris,
  * URL.

### Livrables

* cas d’usage CRUD du MVP ;
* recherche simple et robuste ;
* cohérence des données persistées.

---

## Phase 7 — Générateur de mot de passe

### Objectif

Ajouter une génération de mot de passe fort utilisable dans le formulaire.

### Actions

* Implémenter un générateur basé sur un RNG cryptographique.
* Permettre au minimum :

  * longueur configurable,
  * minuscules,
  * majuscules,
  * chiffres,
  * caractères spéciaux.
* Garder l’UI simple au MVP si besoin.

### Tests

* longueur correcte ;
* conformité aux critères choisis ;
* robustesse fonctionnelle.

### Livrables

* service de génération ;
* intégration à l’écran d’édition.

---

## Phase 8 — UI Avalonia MVP

### Objectif

Construire les écrans principaux du produit.

### Écrans

* écran de création / déverrouillage ;
* tableau de bord / liste des entrées ;
* détail d’une entrée ;
* création / édition d’une entrée ;
* paramètres.

### Actions

* Mettre en place ViewModels.
* Gérer l’état global :

  * `Locked`
  * `Unlocked`
* Gérer navigation et feedback utilisateur.
* Afficher des messages d’erreur clairs.

### Livrables

* interface MVP fonctionnelle ;
* navigation simple ;
* UX cohérente avec le périmètre.

---

## Phase 9 — Ergonomie et sécurité UI

### Objectif

Renforcer la sécurité d’usage et le confort utilisateur.

### Actions

* Masquer les mots de passe par défaut.
* Révéler temporairement les mots de passe.
* Copier identifiant / mot de passe dans le presse-papiers.
* Afficher un feedback de copie.
* Prévoir une évolution future pour effacement automatique du presse-papiers.

### Livrables

* UX sécurisée minimale ;
* intégration des services UI adaptés.

---

## Phase 10 — Gestion d’erreurs et robustesse

### Objectif

Sécuriser le comportement global du produit en cas de problème.

### Cas à traiter

* mot de passe incorrect ;
* coffre absent ;
* coffre corrompu ;
* champ obligatoire manquant ;
* erreur d’IO ;
* tentative d’ouverture d’une entrée introuvable.

### Actions

* messages utilisateur clairs ;
* logging technique exploitable ;
* interdiction de fuite de secrets dans les logs ;
* stratégie claire de non-récupération si le mot de passe maître est perdu.

### Livrables

* robustesse fonctionnelle ;
* erreurs explicites ;
* journalisation propre.

---

## Phase 11 — Version 1.1+ locale

### Objectif

Compléter progressivement le produit local après le MVP.

### Fonctionnalités possibles

* suppression d’entrées ;
* verrouillage après inactivité ;
* catégories et tags ;
* favoris ;
* export / import chiffrés ;
* messages enrichis ;
* options d’interface ;
* emplacement du coffre configurable ;
* thème et préférences.

### Livrables

* backlog local enrichi ;
* améliorations incrémentales stables.

---

## Phase 12 — Préparation de la version hybride

### Objectif

Préparer l’évolution du produit vers un mode **local-first avec fonctionnalités en ligne optionnelles**.

### Principes à figer

* le local reste prioritaire ;
* les fonctionnalités en ligne sont optionnelles ;
* l’absence de réseau ne bloque pas l’accès local ;
* la synchronisation ne doit pas corrompre les données ;
* la sécurité du modèle hybride doit être définie avant implémentation.

### Livrables attendus

* ADR produit hybride ;
* ADR sécurité hybride ;
* ADR synchronisation ;
* contrat d’API V1 ;
* schéma d’architecture cible ;
* stratégie de gestion des conflits ;
* scénarios de test E2E hybrides.

---

## 6. Sous-phases de la préparation hybride

## 12.1 — Cadrage produit hybride

Définir :

* ce que signifie “hybride” dans le produit ;
* ce qui reste local ;
* ce qui devient synchronisable ;
* ce qui est optionnel ;
* ce qui est explicitement hors périmètre initial.

## 12.2 — Architecture hybride

Introduire des ports applicatifs dédiés :

* `ISyncService`
* `IRemoteVaultRepository`
* `IDeviceRegistryService`
* `IAuthenticationProvider`

Séparer clairement :

* persistance locale,
* transport réseau,
* orchestration de synchronisation,
* gestion des conflits.

## 12.3 — Sécurité hybride

Définir :

* le modèle de menace ;
* la place du chiffrement client / serveur ;
* la gestion des clés ;
* la stratégie de révocation d’appareils ;
* les métadonnées autorisées côté serveur.

## 12.4 — Synchronisation

Définir :

* l’unité de synchronisation ;
* les états de sync ;
* la détection de conflit ;
* la stratégie de résolution ;
* la reprise après hors ligne.

## 12.5 — UX hybride

Prévoir les écrans / états :

* connexion / création de compte ;
* gestion d’appareils ;
* état de synchronisation ;
* conflit ;
* erreur réseau ;
* dernière synchronisation réussie.

## 12.6 — Vérification

Prévoir :

* tests unitaires d’orchestration ;
* tests d’intégration client / backend ;
* tests de perte réseau ;
* tests de conflit ;
* tests de révocation d’appareil.

---

## 7. Vérification globale

## Tests automatisés

* tests unitaires `Domain`
* tests unitaires `Application`
* tests d’intégration `Infrastructure`
* tests de crypto
* tests de persistance

## Vérifications manuelles MVP

* premier lancement ;
* création du coffre ;
* ajout d’entrée ;
* recherche ;
* détail ;
* affichage temporaire ;
* copie ;
* modification ;
* verrouillage ;
* déverrouillage ;
* changement de mot de passe maître.

## Vérifications de robustesse

* coffre corrompu ;
* mot de passe incorrect ;
* fichier absent ;
* permission refusée ;
* champ obligatoire manquant.

## Vérifications hybrides futures

* synchronisation initiale ;
* modification hors ligne puis reprise ;
* conflit ;
* appareil révoqué ;
* session expirée ;
* erreur réseau.

---

## 8. Fichiers de référence

* `gdd.md`
* `user Story Gestionnaire de mot de passe.md`
* `constitution.md`
* `docs/adr/`
* `src/`

---

## 9. Décisions structurantes

* Stack : C# / .NET 10 (LTS)
* UI : Avalonia UI 11.x
* Pattern : MVVM
* Architecture : Clean Architecture
* Persistance locale chiffrée
* Mode MVP : hors ligne
* Évolution : hybride optionnel, local-first
* Aucune dépendance réseau obligatoire pour le MVP
* Documentation continue obligatoire

---

## 10. Risques principaux

* complexité cryptographique ;
* mauvaise gestion des données sensibles ;
* corruption de coffre ;
* dette technique si l’architecture dérive ;
* confusion produit entre mode local et mode hybride ;
* complexité excessive de synchronisation.

### Réponses recommandées

* ADR précoces ;
* tests automatisés ;
* limitation du scope ;
* séparation stricte des responsabilités ;
* documentation continue ;
* sécurité prioritaire.

---

## 11. Critères de passage au mode hybride

Le développement de la partie hybride ne commence que si :

* le MVP local est stable ;
* le modèle de sécurité hybride est documenté ;
* la stratégie de synchronisation est décidée ;
* les user stories hybrides sont formalisées ;
* l’architecture cible est validée ;
* les responsabilités local / online sont clairement séparées.

---