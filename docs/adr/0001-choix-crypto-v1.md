# ADR 0001 — Choix cryptographiques V1

## Statut
Accepté

## Contexte
Le MVP doit stocker un coffre local chiffré sans dépendance réseau, avec robustesse face au mauvais mot de passe
et à l'altération des données.

## Décision
- KDF : PBKDF2-SHA256 avec sel aléatoire par coffre et itérations stockées dans le format.
- Chiffrement : AES-GCM (authentifié) avec nonce aléatoire.
- Format : document JSON versionné contenant les métadonnées KDF et les champs base64.

## Conséquences
- Déchiffrement impossible sans mot de passe maître correct.
- Altération détectée par l'authentification GCM.
- Paramètres KDF évolutifs via versionnement.
