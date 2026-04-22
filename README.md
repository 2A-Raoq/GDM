# GDM — V1 locale offline

Implémentation V1 d'un gestionnaire de mots de passe local conforme aux documents de référence dans `/Doc`.

## Stack

- C# / .NET 10 (cible)
- Avalonia UI 11.x
- MVVM
- Clean Architecture (`Domain`, `Application`, `Infrastructure`, `UI`)

## Périmètre implémenté (V1)

- création d'un coffre local chiffré ;
- déverrouillage par mot de passe maître ;
- refus sur mot de passe incorrect ;
- ajout / liste / recherche / détail / modification d'entrées ;
- génération de mot de passe fort ;
- verrouillage manuel et à la fermeture (verrouillage explicite du service de session) ;
- changement du mot de passe maître ;
- fonctionnement 100% local, sans réseau.

## Hors périmètre V1 (non implémenté)

- fonctionnalités hybrides / cloud / synchronisation ;
- multi-utilisateur ;
- biométrie ;
- import / export ;
- suppression, tags, catégories, favoris, auto-lock inactivité.

## Démarrage

```bash
# une fois le SDK .NET 10 disponible
 dotnet restore
 dotnet build
 dotnet test
 dotnet run --project src/Gdm.UI/Gdm.UI.csproj
```

## Sécurité

- PBKDF2-SHA256 pour dérivation de clé ;
- AES-GCM pour chiffrement authentifié ;
- format de coffre versionné ;
- écriture disque atomique via fichier temporaire + remplacement.


## Dépannage restauration (NU1903)

Si `dotnet restore` remonte `NU1903` sur `Tmds.DBus.Protocol 0.16.0`,
la solution inclut désormais une référence directe vers une version corrigée.
Fais ensuite :

```bash
dotnet clean
dotnet restore
dotnet build
```

