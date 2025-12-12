Advent Of Code 2025 - Day 12 Ideas
===============================

Overview
--------

Shape Transformations
-----
All present shapes are 3x3 grids that can be transformed using the following operations:

- Flip vertically: Exchange left and right columns.
- Flip horizontally: Exchange top and bottom rows.

- Rotate 90 degrees clockwise: Move top row to right column, right column to bottom row, bottom row to left column, and left column to top row.
- Rotate 90 degrees counterclockwise: Move top row to left column, left column to bottom row, bottom row to right column, and right column to top row.
- Rotate 180 degrees: Exchange top and bottom rows, and reverse them.

Egy area állapota (node)
-----

- Area egy char matrix (ajándékokkal és üres helyekkel)
- Maradék hely mérete 
- Maradék ajándékok listája (darabszámok shapenként)
- CanFit, rekurzív hívás eredménye

Ajándék tipusok listája (fix)
-----

- Minden ajándék típushoz tartozik egy shape (3x3 char matrix)
- Mérete
- De minden transzformációt megcsinálunk egyszer és azt eltároljuk
  - FlipVert
  - FlipHoriz
  - Rotated90
  - Rotated180
  - Rotated270

DFS Search
-----

- üres areaval kezdjük
- a maradék ajándékok méretét és számát összegezve megnézzük, hogy befér-e egyáltalán a megmaradó helyre, ha nem akkor ez egy halott ág

### Elágazások

- veszünk egy ajándéktípust a maradék ajándékok közül
- minden lehetséges transzformációján végigmegyünk
- minden lehetséges pozícióba megpróbáljuk elhelyezni az adott transzformált ajándékot
- ha elfér, akkor létrehozunk egy új állapotot (node-ot) az új area-val, frissített maradék ajándékokkal, és rekurzívan hívjuk a DFS-t az új állapotra

### Check present in area 
- Adott area 
- adott pozícióban 
- elfér-e egy adott shape 
- adott transzformációja

### Clone node

- char mátrixot lemásoljuk
- maradék elhelyezendő ajándékok számait lemásoljuk, list int clone