//maxArea = GetMaxAreaPart1(tiles, maxArea);









//void PaintInside(List<Tile> tiles)
//{
//    var minX = tiles.Min(t => t.X);
//    var maxX = tiles.Max(t => t.X);
//    var minY = tiles.Min(t => t.Y);
//    var maxY = tiles.Max(t => t.Y);
//    for (long col = minX; col <= maxX; col++)
//    {
//        for (long row = minY; row <= maxY; row++)
//        {
//            if (tiles.Any(t => t.X < col && t.Y == row) &&
//                tiles.Any(t => t.X > col && t.Y == row) &&
//                tiles.Any(t => t.Y < row && t.X == col) &&
//                tiles.Any(t => t.Y > row && t.X == col))
//            {
//                if (!tiles.Any(t => t.X == col && t.Y == row && t.Color == Color.Red))
//                {
//                    PaintItToGreen(col, row);
//                }
//            }
//        }
//    }
//}

//void PaintItToGreen(long col, long row)
//{
//    tilesWithGreen.Add(new Tile(col, row, Color.Green));
//}

//long GetMaxAreaPart1(List<Tile> tiles, long maxArea)
//{
//    for (int i = 0; i < tiles.Count; i++)
//    {
//        for (int j = i + 1; j < tiles.Count; j++)
//        {
//            var area = GetArea(tiles[i], tiles[j]);
//            if (area > maxArea)
//            {
//                maxArea = area;
//            }
//        }
//    }

//    return maxArea;
//}

record Tile(long X, long Y, Color Color);
