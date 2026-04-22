# User Stories — Gestionnaire de mots de passe local et hybride

## 1. Contexte

Ce document regroupe les user stories du produit.

Il couvre :
- le **socle MVP local hors ligne** ;
- les évolutions locales post-MVP ;
- les futures fonctionnalités **hybrides** avec options en ligne.

L’objectif est de disposer d’un backlog structuré, priorisé et exploitable pour guider le développement.

---

## 2. Backlog principal des user stories

| ID | En tant que | Je veux | Afin de | Priorité |
|---|---|---|---|---|
| US-001 | Utilisateur | créer un coffre-fort local protégé par un mot de passe maître | sécuriser l’ensemble de mes identifiants dans un espace unique | Haute |
| US-002 | Utilisateur | déverrouiller mon coffre-fort avec mon mot de passe maître | accéder à mes données sensibles de manière sécurisée | Haute |
| US-003 | Utilisateur | être bloqué si le mot de passe maître saisi est incorrect | empêcher tout accès non autorisé à mes mots de passe | Haute |
| US-004 | Utilisateur | ajouter une nouvelle entrée contenant un nom de service, un identifiant, un mot de passe et une note | enregistrer facilement un nouveau compte | Haute |
| US-005 | Utilisateur | voir la liste de toutes mes entrées enregistrées | retrouver rapidement un compte existant | Haute |
| US-006 | Utilisateur | rechercher une entrée par nom de service ou identifiant | accéder plus vite à l’information dont j’ai besoin | Haute |
| US-007 | Utilisateur | consulter le détail d’une entrée | vérifier ou récupérer les informations d’un compte | Haute |
| US-008 | Utilisateur | masquer les mots de passe par défaut | éviter qu’ils soient visibles à l’écran sans action volontaire | Haute |
| US-009 | Utilisateur | révéler temporairement un mot de passe | consulter sa valeur uniquement quand j’en ai besoin | Haute |
| US-010 | Utilisateur | copier un identifiant ou un mot de passe dans le presse-papiers | gagner du temps lors de la connexion à un service | Haute |
| US-011 | Utilisateur | modifier une entrée existante | garder mes identifiants à jour | Haute |
| US-012 | Utilisateur | supprimer une entrée | retirer les comptes que je n’utilise plus | Moyenne |
| US-013 | Utilisateur | générer automatiquement un mot de passe fort | créer des mots de passe plus sécurisés | Haute |
| US-014 | Utilisateur | choisir la longueur et certains critères du mot de passe généré | adapter le mot de passe aux exigences du site concerné | Moyenne |
| US-015 | Utilisateur | sauvegarder mes données localement de façon chiffrée | protéger mes informations même si le fichier est consulté directement | Haute |
| US-016 | Utilisateur | verrouiller manuellement le coffre-fort | sécuriser rapidement mes données lorsque je m’absente | Haute |
| US-017 | Utilisateur | que le coffre-fort se reverrouille automatiquement à la fermeture de l’application | éviter qu’il reste accessible après usage | Haute |
| US-018 | Utilisateur | que le coffre-fort se verrouille après une période d’inactivité | réduire le risque d’accès non autorisé | Moyenne |
| US-019 | Utilisateur | être informé lorsqu’un champ obligatoire est manquant lors de la création ou de la modification d’une entrée | éviter d’enregistrer des données incomplètes | Haute |
| US-020 | Utilisateur | organiser mes entrées par catégories ou tags | mieux classer mes comptes personnels, professionnels ou bancaires | Moyenne |
| US-021 | Utilisateur | marquer certaines entrées comme favorites | accéder plus rapidement à mes comptes les plus utilisés | Basse |
| US-022 | Utilisateur | exporter une sauvegarde chiffrée de mon coffre-fort | conserver une copie de secours de mes données | Moyenne |
| US-023 | Utilisateur | importer une sauvegarde précédemment exportée | restaurer mes données en cas de perte ou de changement de poste | Moyenne |
| US-024 | Utilisateur | changer mon mot de passe maître | maintenir un bon niveau de sécurité dans le temps | Haute |
| US-025 | Utilisateur | voir un message clair en cas de coffre-fort introuvable ou corrompu | comprendre le problème et éviter une mauvaise manipulation | Moyenne |
| US-026 | Utilisateur | afficher une interface simple et lisible | utiliser l’application facilement au quotidien | Moyenne |
| US-027 | Utilisateur | utiliser l’application sans connexion Internet | garder le contrôle local sur mes données | Haute |
| US-028 | Utilisateur | fermer automatiquement le mode d’affichage du mot de passe après quelques secondes | limiter l’exposition visuelle des données sensibles | Moyenne |

---

## 3. User stories complémentaires — version hybride / fonctionnalités en ligne

| ID | En tant que | Je veux | Afin de | Priorité |
|---|---|---|---|---|
| US-029 | Utilisateur | activer un compte en ligne optionnel | synchroniser mon coffre entre plusieurs appareils sans perdre le mode local | Haute |
| US-030 | Utilisateur | connecter un nouvel appareil à mon compte | retrouver mon coffre sur un second poste de travail | Haute |
| US-031 | Utilisateur | synchroniser mes modifications avec un service distant | garder mes données cohérentes entre mes appareils | Haute |
| US-032 | Utilisateur | continuer à utiliser l’application hors ligne puis resynchroniser plus tard | ne pas être bloqué en absence de réseau | Haute |
| US-033 | Utilisateur | voir l’état de synchronisation de mon coffre | comprendre si mes données sont à jour ou non | Haute |
| US-034 | Utilisateur | être informé lorsqu’un conflit de modification existe | éviter d’écraser une version plus récente ou concurrente | Haute |
| US-035 | Utilisateur | choisir la version à conserver en cas de conflit | garder le contrôle sur mes données | Haute |
| US-036 | Utilisateur | révoquer un appareil connecté à mon compte | protéger mes données si un appareil est perdu ou compromis | Haute |
| US-037 | Utilisateur | être averti si ma session a expiré ou si mon appareil n’est plus autorisé | comprendre pourquoi la synchronisation ne fonctionne plus | Moyenne |
| US-038 | Utilisateur | voir un message clair lorsqu’une erreur réseau empêche la synchronisation | éviter de croire à tort que mes données sont synchronisées | Haute |
| US-039 | Utilisateur | lancer une synchronisation manuelle | reprendre le contrôle en cas de doute sur l’état distant | Moyenne |
| US-040 | Utilisateur | consulter la date de dernière synchronisation réussie | savoir si mon coffre distant est à jour | Moyenne |
| US-041 | Utilisateur | désactiver les fonctionnalités en ligne et revenir à un usage 100% local | garder la maîtrise de mon mode d’utilisation | Moyenne |
| US-042 | Utilisateur | sauvegarder à distance une copie chiffrée de mon coffre | disposer d’une sécurité supplémentaire en cas de perte locale | Moyenne |

---

## 4. MVP recommandé

Les user stories prioritaires pour une première version locale sont :

- US-001 à US-011
- US-013
- US-015
- US-016
- US-017
- US-019
- US-024
- US-027

---

## 5. Évolutions locales post-MVP recommandées

Pour enrichir la version locale après le MVP :

- US-012
- US-014
- US-018
- US-020
- US-021
- US-022
- US-023
- US-025
- US-026
- US-028

---

## 6. Périmètre recommandé de la première version hybride

Les user stories hybrides à envisager en premier sont :

- US-029
- US-030
- US-031
- US-032
- US-033
- US-034
- US-036
- US-038
- US-039
- US-040
- US-041

---

## 7. Découpage fonctionnel

### 7.1 Sécurité locale
- création du coffre-fort ;
- déverrouillage par mot de passe maître ;
- refus d’accès en cas d’erreur ;
- chiffrement local ;
- verrouillage manuel ;
- verrouillage automatique à la fermeture ;
- changement du mot de passe maître.

### 7.2 Gestion des entrées
- ajout d’une entrée ;
- consultation de la liste ;
- recherche ;
- consultation du détail ;
- modification ;
- suppression.

### 7.3 Ergonomie
- masquage par défaut des mots de passe ;
- affichage temporaire ;
- copie dans le presse-papiers ;
- générateur de mot de passe ;
- catégories, tags et favoris ;
- interface simple et lisible.

### 7.4 Sauvegarde et maintenance
- export de sauvegarde ;
- import de sauvegarde ;
- gestion des erreurs ;
- messages explicites ;
- robustesse des opérations.

### 7.5 Fonctionnalités hybrides
- activation du compte en ligne ;
- association et révocation d’appareils ;
- synchronisation locale / distante ;
- résolution de conflits ;
- erreurs réseau ;
- visibilité de l’état de synchronisation ;
- retour à un mode 100% local.

---

## 8. Format recommandé pour détailler les user stories

Chaque user story implémentée devrait idéalement être enrichie avec :

- un scénario nominal ;
- un ou plusieurs scénarios d’échec ;
- les préconditions ;
- les données minimales requises ;
- les validations attendues ;
- le comportement UI attendu ;
- le comportement métier attendu ;
- les messages utilisateur attendus ;
- les impacts sécurité éventuels ;
- les tests associés.

---

## 9. Définition des priorités

### Haute
Fonction essentielle au MVP local ou à la cohérence de la version hybride.

### Moyenne
Fonction importante mais non bloquante pour une première livraison.

### Basse
Fonction utile, mais non nécessaire au démarrage du produit.

---

## 10. Remarques produit

- Le produit doit rester pleinement exploitable en **mode local**.
- Les fonctionnalités en ligne doivent rester **optionnelles**.
- Le passage à la version hybride ne doit pas remettre en cause le fonctionnement hors ligne.
- Les user stories hybrides nécessitent un cadrage sécurité et synchronisation avant implémentation.

---