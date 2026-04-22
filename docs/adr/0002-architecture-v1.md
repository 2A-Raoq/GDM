# ADR 0002 — Architecture V1

## Statut
Accepté

## Décision
Adopter Clean Architecture avec 4 couches :
- `Gdm.Domain` : entités et invariants métier ;
- `Gdm.Application` : cas d'usage et ports ;
- `Gdm.Infrastructure` : implémentations crypto / persistance ;
- `Gdm.UI` : Avalonia MVVM et composition root.

## Raison
Maximiser testabilité, maintenabilité et séparation métier/technique.
