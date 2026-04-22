# GDD — Gestionnaire de mots de passe local et hybride

## 1. Présentation du projet

### 1.1 Nom du projet

**Gestionnaire de mots de passe local et hybride**

### 1.2 Résumé

L’application est un gestionnaire de mots de passe permettant à un utilisateur de stocker, consulter, modifier et sécuriser ses identifiants dans un coffre-fort chiffré protégé par un mot de passe maître.

Le produit est conçu selon une approche :

* **local-first**,
* d’abord **hors ligne** pour le MVP,
* puis évolutif vers des **fonctionnalités en ligne optionnelles** dans une version hybride.

### 1.3 Contexte

Le besoin provient d’un backlog de user stories définissant une application centrée sur :

* la sécurité du coffre-fort ;
* la gestion des entrées de connexion ;
* l’ergonomie d’utilisation quotidienne ;
* la sauvegarde et la maintenance des données ;
* une évolution possible vers la synchronisation et la gestion multi-appareils.

### 1.4 Type de document

Ce document sert de **GDD** au sens de **document de conception globale** du produit.

Il décrit :

* la vision ;
* le périmètre ;
* les fonctionnalités ;
* les contraintes ;
* les flux utilisateurs ;
* les choix de conception attendus.

---

## 2. Vision produit

### 2.1 Problème à résoudre

Les utilisateurs ont besoin de mémoriser ou stocker de nombreux identifiants pour différents services.

Cette gestion devient rapidement :

* complexe ;
* peu ergonomique ;
* risquée lorsqu’elle repose sur des notes non chiffrées, des fichiers texte ou la réutilisation de mots de passe faibles.

### 2.2 Proposition de valeur

L’application doit permettre à l’utilisateur de :

* centraliser ses identifiants dans un coffre-fort unique ;
* protéger ses données par chiffrement ;
* accéder rapidement à ses comptes grâce à une interface claire ;
* rester maître de ses données en usage local ;
* évoluer vers un usage multi-appareils si les fonctions en ligne sont activées.

### 2.3 Objectifs produit

* Fournir un coffre-fort local chiffré.
* Garantir un accès protégé par mot de passe maître.
* Permettre une gestion simple et rapide des entrées.
* Assurer une bonne expérience utilisateur au quotidien.
* Rendre possible la sauvegarde et la restauration des données.
* Préparer une évolution vers un mode hybride sans casser le mode local.

### 2.4 Non-objectifs du MVP

Le MVP ne vise pas à inclure immédiatement :

* la synchronisation cloud ;
* le partage multi-utilisateur ;
* l’autoremplissage navigateur ;
* l’authentification biométrique ;
* la synchronisation mobile ;
* la gestion complexe de conflits multi-appareils.

---

## 3. Public cible

### 3.1 Utilisateur principal

Un utilisateur individuel souhaitant gérer localement ses mots de passe personnels ou professionnels.

### 3.2 Profils d’usage

* **Utilisateur personnel** : stocke ses comptes de réseaux sociaux, messagerie, banque, achats en ligne.
* **Utilisateur étudiant** : centralise ses accès aux plateformes d’école, outils collaboratifs et services en ligne.
* **Utilisateur professionnel** : garde ses accès outils, VPN, portails internes ou comptes clients dans un coffre local.

### 3.3 Besoins clés

* sécurité forte ;
* simplicité d’utilisation ;
* rapidité d’accès ;
* fonctionnement hors ligne ;
* fiabilité des sauvegardes ;
* évolutivité maîtrisée vers le multi-appareil.

---

## 4. Plateforme et hypothèses de réalisation

### 4.1 Plateforme cible

Application desktop locale.

### 4.2 Hypothèse technique recommandée

Une implémentation en **C# / .NET avec Avalonia UI** est cohérente pour proposer une application multiplateforme :

* Windows
* Linux
* macOS

### 4.3 Mode de fonctionnement

* fonctionnement principalement hors ligne ;
* stockage local des données ;
* aucune dépendance obligatoire à un service distant pour le MVP.

### 4.4 Évolution cible

Le produit pourra évoluer vers un mode **hybride** où :

* le coffre reste utilisable localement ;
* des fonctions en ligne optionnelles sont ajoutées ;
* le réseau devient un complément, pas une dépendance absolue.

---

## 5. Périmètre fonctionnel

Le périmètre est structuré en cinq grands domaines.

### 5.1 Sécurité

* création d’un coffre-fort local ;
* déverrouillage par mot de passe maître ;
* refus d’accès en cas de mot de passe incorrect ;
* chiffrement des données ;
* verrouillage manuel ;
* verrouillage automatique à la fermeture ;
* verrouillage automatique après inactivité ;
* changement du mot de passe maître.

### 5.2 Gestion des entrées

* ajout d’une entrée ;
* consultation de la liste des entrées ;
* recherche d’une entrée ;
* consultation du détail ;
* modification d’une entrée ;
* suppression d’une entrée.

### 5.3 Ergonomie

* masquage des mots de passe par défaut ;
* affichage temporaire d’un mot de passe ;
* copie identifiant / mot de passe dans le presse-papiers ;
* organisation par catégories ou tags ;
* gestion des favoris ;
* interface simple et lisible.

### 5.4 Sauvegarde et maintenance

* sauvegarde locale chiffrée ;
* export d’une sauvegarde ;
* import d’une sauvegarde ;
* gestion des erreurs ;
* gestion des fichiers corrompus.

### 5.5 Fonctionnalités hybrides futures

* compte en ligne optionnel ;
* association d’appareils ;
* synchronisation ;
* résolution de conflits ;
* sauvegarde distante chiffrée ;
* visibilité de l’état de synchronisation ;
* révocation d’appareils.

---

## 6. Fonctionnalités détaillées

## 6.1 Création du coffre-fort

### Description

Lors du premier lancement, l’utilisateur peut créer un coffre-fort local protégé par un mot de passe maître.

### Règles

* le mot de passe maître est requis ;
* une confirmation du mot de passe maître est recommandée ;
* le coffre est persisté localement ;
* les données du coffre doivent être chiffrées avant écriture disque.

### Critères d’acceptation

* le coffre est créé si les champs sont valides ;
* un message d’erreur est affiché si les champs sont vides ou incohérents.

---

## 6.2 Déverrouillage du coffre

### Description

L’utilisateur déverrouille le coffre-fort en saisissant le mot de passe maître.

### Règles

* aucun accès aux données tant que le coffre est verrouillé ;
* l’accès est refusé si le mot de passe est incorrect.

### Critères d’acceptation

* l’utilisateur accède à l’écran principal après authentification réussie ;
* un message d’erreur clair est affiché en cas d’échec.

---

## 6.3 Gestion des entrées

### Description

Une entrée représente un compte ou un identifiant enregistré dans le coffre.

### Champs minimaux

* nom du service ;
* identifiant ou email ;
* mot de passe ;
* note optionnelle.

### Champs complémentaires recommandés

* catégorie ;
* tags ;
* URL du service ;
* statut favori ;
* date de création ;
* date de dernière modification.

### Actions disponibles

* créer ;
* lire ;
* mettre à jour ;
* supprimer ;
* rechercher.

---

## 6.4 Affichage sécurisé du mot de passe

### Description

Le mot de passe n’est jamais affiché en clair par défaut.

### Comportement attendu

* champ masqué par défaut ;
* bouton pour révéler temporairement le mot de passe ;
* retour automatique à l’état masqué après quelques secondes.

---

## 6.5 Copie dans le presse-papiers

### Description

L’utilisateur peut copier l’identifiant ou le mot de passe d’une entrée.

### Règles recommandées

* confirmation visuelle de la copie ;
* effacement automatique du presse-papiers après un délai configurable dans une version ultérieure.

---

## 6.6 Générateur de mot de passe

### Description

L’application peut générer un mot de passe fort.

### Paramètres possibles

* longueur ;
* lettres minuscules ;
* lettres majuscules ;
* chiffres ;
* caractères spéciaux.

### Résultat attendu

* mot de passe aléatoire conforme aux critères choisis ;
* insertion directe possible dans une nouvelle entrée ou une entrée existante.

---

## 6.7 Sauvegarde et restauration

### Description

L’utilisateur peut exporter et réimporter une sauvegarde chiffrée de son coffre.

### Règles

* l’export doit produire un fichier exploitable localement ;
* l’import doit vérifier la validité du format et la cohérence des données ;
* les erreurs doivent être explicites si le fichier est invalide ou corrompu.

---

## 6.8 Changement du mot de passe maître

### Description

L’utilisateur peut modifier son mot de passe maître sans perdre ses données.

### Règles

* l’ancien mot de passe doit être demandé ;
* le coffre doit être rechiffré avec le nouveau mot de passe ;
* l’opération doit être atomique autant que possible.

---

## 7. Backlog de référence

## 7.1 User stories MVP

Les user stories suivantes constituent le socle fonctionnel minimal :

* US-001 à US-011
* US-013
* US-015
* US-016
* US-017
* US-019
* US-024
* US-027

## 7.2 User stories hors MVP mais prévues

* US-012
* US-014
* US-018
* US-020
* US-021
* US-022
* US-023
* US-025
* US-026
* US-028

## 7.3 User stories hybrides futures

* US-029 à US-042

---

## 8. Flux utilisateur principaux

## 8.1 Premier lancement

1. L’utilisateur ouvre l’application.
2. L’application détecte qu’aucun coffre n’existe.
3. L’utilisateur crée un mot de passe maître.
4. Le coffre-fort est créé et chiffré.
5. L’utilisateur accède à l’interface principale.

## 8.2 Connexion à un coffre existant

1. L’utilisateur ouvre l’application.
2. L’écran de déverrouillage s’affiche.
3. L’utilisateur saisit son mot de passe maître.
4. Si le mot de passe est valide, le coffre est déverrouillé.
5. Sinon, un message d’erreur s’affiche.

## 8.3 Ajout d’une entrée

1. L’utilisateur clique sur « Ajouter ».
2. Il remplit le formulaire.
3. Il valide.
4. L’application vérifie les champs obligatoires.
5. L’entrée est enregistrée localement et apparaît dans la liste.

## 8.4 Consultation et copie

1. L’utilisateur recherche ou sélectionne une entrée.
2. Il ouvre le détail.
3. Il peut afficher temporairement le mot de passe.
4. Il peut copier l’identifiant ou le mot de passe.

## 8.5 Verrouillage

1. L’utilisateur clique sur « Verrouiller » ou quitte l’application.
2. Le coffre est verrouillé.
3. Les données ne sont plus visibles sans nouvelle authentification.

---

## 9. Structure des données

## 9.1 Entité Coffre

* identifiant du coffre ;
* métadonnées de création ;
* version de format ;
* paramètres de chiffrement ;
* contenu chiffré.

## 9.2 Entité Entrée

* id ;
* serviceName ;
* login ;
* password ;
* notes ;
* category ;
* tags ;
* isFavorite ;
* createdAt ;
* updatedAt ;
* websiteUrl.

## 9.3 Paramètres applicatifs recommandés

* durée d’inactivité avant verrouillage ;
* durée d’affichage temporaire du mot de passe ;
* thème clair / sombre ;
* emplacement du fichier coffre.

---

## 10. Contraintes de sécurité

## 10.1 Exigences minimales

* les données doivent être chiffrées au repos ;
* le mot de passe maître ne doit jamais être stocké en clair ;
* les mots de passe doivent être masqués par défaut ;
* l’accès au contenu doit être impossible sans déverrouillage du coffre.

## 10.2 Bonnes pratiques recommandées

* dérivation de clé robuste à partir du mot de passe maître ;
* gestion mémoire prudente pour les données sensibles ;
* effacement des données temporaires quand c’est possible ;
* limitation de l’exposition du mot de passe à l’écran ;
* validation stricte des imports.

## 10.3 Points sensibles

* corruption du fichier coffre ;
* oubli du mot de passe maître ;
* persistance involontaire dans le presse-papiers ;
* erreurs d’implémentation cryptographique.

### Décision produit

Le projet doit indiquer clairement qu’en cas de perte du mot de passe maître, la récupération des données peut être impossible si aucune solution de récupération n’est prévue.

---

## 11. UX / UI

## 11.1 Principes d’interface

* interface sobre et claire ;
* navigation rapide ;
* mise en avant des actions principales ;
* feedback immédiat sur les actions critiques.

## 11.2 Écrans principaux

### Écran 1 — Déverrouillage / création du coffre

* création du coffre au premier lancement ;
* saisie du mot de passe maître ;
* messages d’erreur et d’état.

### Écran 2 — Tableau de bord / liste des entrées

* barre de recherche ;
* liste des comptes ;
* filtres éventuels ;
* bouton d’ajout ;
* action de verrouillage.

### Écran 3 — Détail d’une entrée

* affichage des champs ;
* bouton afficher / masquer mot de passe ;
* bouton copier identifiant ;
* bouton copier mot de passe ;
* bouton modifier ;
* bouton supprimer.

### Écran 4 — Édition / création d’une entrée

* formulaire des champs ;
* générateur de mot de passe ;
* validation / annulation.

### Écran 5 — Paramètres

* changement du mot de passe maître ;
* réglage du verrouillage automatique ;
* export / import ;
* préférences d’affichage.

---

## 12. Exigences non fonctionnelles

### 12.1 Performance

* temps de déverrouillage raisonnable ;
* affichage fluide de la liste des entrées ;
* sauvegarde rapide après modification.

### 12.2 Fiabilité

* pas de perte de données lors des opérations standards ;
* sauvegarde cohérente en cas de fermeture normale ;
* gestion claire des fichiers corrompus.

### 12.3 Maintenabilité

* architecture claire et modulaire ;
* séparation UI / logique métier / persistance ;
* code testable.

### 12.4 Portabilité

* compatibilité multiplateforme souhaitée ;
* comportement cohérent sur Windows, Linux et macOS.

### 12.5 Accessibilité

* libellés clairs ;
* navigation clavier minimale ;
* contrastes suffisants.

---

## 13. Architecture logique recommandée

## 13.1 Couches

* **UI** : vues Avalonia ;
* **Présentation** : ViewModels MVVM ;
* **Domaine** : entités, règles métier, validation ;
* **Services applicatifs** : orchestration des cas d’usage ;
* **Infrastructure** : chiffrement, persistance, import/export, presse-papiers.

## 13.2 Services principaux

* `VaultService`
* `EncryptionService`
* `AuthenticationService`
* `PasswordGeneratorService`
* `ClipboardService`
* `BackupService`
* `SettingsService`

## 13.3 Principes de conception

* architecture MVVM ;
* dépendances injectables si nécessaire ;
* services découplés de l’interface ;
* validations centralisées ;
* séparation stricte des responsabilités.

---

## 14. Règles métier

* Une entrée ne peut pas être enregistrée sans nom de service.
* Une entrée doit contenir au minimum un identifiant et un mot de passe pour être considérée complète dans le MVP.
* Tant que le coffre est verrouillé, aucune donnée sensible n’est accessible.
* Le mot de passe maître conditionne tout accès au coffre.
* La fermeture de l’application doit entraîner le verrouillage du coffre.
* Les mots de passe sont masqués par défaut.
* Toute modification doit être persistée localement.
* Une opération d’import invalide ne doit pas écraser les données existantes sans confirmation.

---

## 15. Gestion des erreurs

### Cas à traiter

* mot de passe maître incorrect ;
* champ obligatoire manquant ;
* coffre-fort introuvable ;
* coffre-fort corrompu ;
* échec d’import ;
* échec d’export ;
* tentative d’ouverture d’une entrée supprimée ou inexistante.

### Comportement attendu

* afficher un message utilisateur clair ;
* journaliser l’erreur de manière exploitable côté application ;
* éviter les pertes de données ;
* proposer une action corrective quand cela est possible.

---

## 16. Roadmap produit

## Version 1 — MVP local

* création du coffre ;
* déverrouillage ;
* ajout / lecture / modification d’entrées ;
* recherche ;
* masquage / affichage temporaire ;
* copie presse-papiers ;
* génération de mot de passe ;
* chiffrement local ;
* verrouillage manuel et à la fermeture ;
* changement du mot de passe maître.

## Version 1.1

* suppression d’entrées ;
* verrouillage après inactivité ;
* catégories et tags ;
* messages d’erreur enrichis.

## Version 1.2

* favoris ;
* export / import chiffré ;
* options d’interface supplémentaires.

## Version 2+ locale

* amélioration du presse-papiers ;
* filtres avancés ;
* meilleure personnalisation ;
* personnalisation du stockage local.

---

## 17. Critères de réussite

Le produit sera considéré comme conforme si :

* un utilisateur peut créer puis ouvrir un coffre-fort local sécurisé ;
* les données sont chiffrées et non lisibles en clair sur disque ;
* l’utilisateur peut gérer ses entrées simplement ;
* les mots de passe restent masqués par défaut ;
* l’application fonctionne sans connexion Internet ;
* les erreurs principales sont gérées proprement.

---

## 18. Risques projet

* complexité de l’implémentation cryptographique ;
* mauvaise gestion du cycle de vie des données sensibles ;
* corruption du fichier de coffre ;
* UX trop complexe pour une application censée être simple ;
* dette technique si l’architecture n’est pas séparée dès le début.

### Réponses recommandées

* s’appuyer sur des bibliothèques cryptographiques éprouvées ;
* définir un format de coffre versionné ;
* prévoir des tests automatisés sur les cas critiques ;
* limiter le scope du MVP ;
* documenter les décisions structurantes.

---

## 19. Annexes

## 19.1 Mapping simplifié user stories → modules

* **Sécurité** : US-001, US-002, US-003, US-015, US-016, US-017, US-018, US-024, US-027
* **Gestion des entrées** : US-004, US-005, US-006, US-007, US-011, US-012, US-019
* **Ergonomie** : US-008, US-009, US-010, US-014, US-020, US-021, US-026, US-028
* **Sauvegarde et maintenance** : US-022, US-023, US-025
* **Hybride** : US-029 à US-042

## 19.2 Référence source

Ce document a été rédigé à partir du backlog de user stories et de la vision produit du projet.

---

## 20. Extension produit — version hybride avec fonctionnalités en ligne

### 20.1 Positionnement

La version 1 du produit reste un gestionnaire de mots de passe **local** et **hors ligne**.

À partir d’une évolution post-MVP, le produit pourra proposer un mode **hybride** :

* le coffre reste exploitable localement ;
* les fonctionnalités en ligne sont **optionnelles** ;
* l’absence de réseau ne doit pas empêcher l’accès aux données locales déjà disponibles ;
* les fonctions distantes servent à la synchronisation, à la sauvegarde distante chiffrée et à la gestion d’appareils.

### 20.2 Objectifs de la version hybride

* Permettre l’usage du même coffre sur plusieurs appareils.
* Conserver un fonctionnement local-first.
* Rendre la synchronisation compréhensible et maîtrisable par l’utilisateur.
* Conserver un haut niveau de sécurité pour les données sensibles.
* Ne pas dégrader l’expérience offline existante.

### 20.3 Non-objectifs du premier incrément hybride

Le premier incrément hybride ne vise pas immédiatement :

* le partage collaboratif avancé ;
* la web app complète ;
* l’autoremplissage navigateur ;
* la synchronisation temps réel complexe ;
* la récupération de compte reposant sur l’accès au contenu du coffre en clair.

### 20.4 Principes produit

* **Local-first** : les opérations principales doivent rester possibles localement.
* **Online optionnel** : l’utilisateur peut rester en mode 100% local.
* **Clarté** : l’utilisateur doit comprendre ce qui est local, distant, synchronisé ou en conflit.
* **Sécurité** : les secrets utilisateur ne doivent pas être exposés inutilement côté serveur.
* **Réversibilité** : la désactivation des fonctions en ligne doit être possible.

### 20.5 Fonctionnalités hybrides principales

* création d’un compte en ligne optionnel ;
* association d’un ou plusieurs appareils ;
* synchronisation des données du coffre ;
* consultation de l’état de synchronisation ;
* reprise après travail hors ligne ;
* détection et résolution des conflits ;
* révocation d’un appareil ;
* sauvegarde distante chiffrée.

### 20.6 Flux utilisateur hybrides

#### 20.6.1 Activation du mode hybride

1. L’utilisateur ouvre les paramètres.
2. Il active les fonctionnalités en ligne.
3. Il crée ou connecte un compte.
4. L’application associe l’appareil courant.
5. Une première synchronisation est proposée.

#### 20.6.2 Usage hors ligne puis reprise

1. L’utilisateur modifie son coffre sans connexion réseau.
2. Les changements sont conservés localement.
3. À la reconnexion, l’application propose ou lance la synchronisation.
4. En cas de conflit, l’utilisateur est informé clairement.

#### 20.6.3 Révocation d’un appareil

1. L’utilisateur consulte la liste des appareils connectés.
2. Il révoque un appareil.
3. L’appareil révoqué perd l’accès aux fonctions en ligne.
4. L’utilisateur reçoit une confirmation claire de l’action.

### 20.7 Données et synchronisation

Les métadonnées suivantes peuvent être nécessaires pour la version hybride :

* identifiant unique d’appareil ;
* identifiant de version du coffre ;
* date de dernière synchronisation ;
* état de synchronisation ;
* état de conflit éventuel.

Le système de synchronisation doit :

* détecter les modifications concurrentes ;
* éviter l’écrasement silencieux des données ;
* conserver une cohérence entre local et distant ;
* rendre visibles les erreurs réseau ou d’authentification.

### 20.8 Contraintes de sécurité hybrides

* Les données sensibles ne doivent pas être exposées inutilement côté serveur.
* Le modèle de chiffrement doit être documenté avant développement backend.
* Les appareils autorisés doivent pouvoir être identifiés et révoqués.
* Les erreurs d’authentification et de synchronisation doivent être journalisées sans fuite de secrets.
* La rotation ou révocation d’un appareil compromis doit être prévue.

### 20.9 UX / UI complémentaire

Ajouter les écrans ou états suivants :

* connexion / création de compte ;
* liste des appareils autorisés ;
* état de synchronisation ;
* dernière synchronisation réussie ;
* erreur réseau ;
* session expirée ;
* résolution de conflit.

### 20.10 Exigences non fonctionnelles complémentaires

* la perte réseau ne doit pas casser l’usage local courant ;
* l’échec de synchronisation ne doit pas corrompre les données locales ;
* les conflits doivent être détectés explicitement ;
* les actions sensibles liées aux appareils doivent être confirmées ;
* la journalisation doit rester exploitable sans exposer les secrets ;
* l’activation du mode hybride ne doit pas dégrader les performances locales de manière significative.

### 20.11 Architecture logique complémentaire

Services supplémentaires recommandés :

* `SyncService`
* `RemoteVaultRepository`
* `DeviceManagementService`
* `OnlineAuthService`
* `ConflictResolutionService`

Principes supplémentaires :

* séparation stricte entre domaine local et transport réseau ;
* DTO réseau distincts des entités métier ;
* gestion des erreurs réseau découplée de l’UI ;
* orchestration de synchronisation testable sans interface graphique.

### 20.12 Roadmap mise à jour

#### Version 2 — Hybride V1

* compte en ligne optionnel ;
* association d’appareils ;
* synchronisation de base ;
* état de synchronisation ;
* gestion des erreurs réseau ;
* révocation d’appareils.

#### Version 2.1

* résolution de conflits améliorée ;
* sauvegarde distante chiffrée ;
* historique de synchronisation ;
* UX enrichie des états hybrides.

---