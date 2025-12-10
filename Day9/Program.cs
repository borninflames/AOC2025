Console.WriteLine("Day 9");
Console.WriteLine();
string filePath = "input.txt";
var input = await File.ReadAllLinesAsync(filePath);
var tiles = new List<Tile>();
var tilesWithGreen = new List<Tile>();

SetTiles(input, tiles, tilesWithGreen);

//ShowTiles(tilesWithGreen);

var maxArea = 0L;

maxArea = GetMaxAreaPart2(tiles, maxArea);

Console.WriteLine($"Max Area: {maxArea}");
Console.ReadLine();

long GetMaxAreaPart2(List<Tile> tiles, long maxArea)
{
    Console.WriteLine("Calculating max area...");

    for (int i = 0; i < tiles.Count; i++)
    {
        for (int j = i + 1; j < tiles.Count; j++)
        {
            var area = GetArea(tiles[i], tiles[j]);
            if (area > maxArea)
            {
                if (IsValidArea(tiles[i], tiles[j]))
                {
                    maxArea = area;
                    Console.WriteLine($"New max area: {maxArea} between tiles ({tiles[i].X},{tiles[i].Y}) and ({tiles[j].X},{tiles[j].Y})");
                }
            }
        }
    }

    return maxArea;
}

long GetArea(Tile t1, Tile t2)
{
    var xDiff = Math.Abs(t1.X - t2.X);
    var yDiff = Math.Abs(t1.Y - t2.Y);
    xDiff = xDiff == 0 ? 1 : xDiff + 1;
    yDiff = yDiff == 0 ? 1 : yDiff + 1;
    var area = xDiff * yDiff;
    return area;
}

bool IsValidArea(Tile tile1, Tile tile2)
{
    //Check corners
    if (!IsInside(tile1.X, tile1.Y) || 
        !IsInside(tile2.X, tile2.Y) || 
        !IsInside(tile1.X, tile2.Y) || 
        !IsInside(tile2.X, tile1.Y))
    {
        return false;
    }

    //Check inside area, if we have any red tile in it then it is not valid
    var fromX = Math.Min(tile1.X, tile2.X);
    var toX = Math.Max(tile1.X, tile2.X);
    var fromY = Math.Min(tile1.Y, tile2.Y);
    var toY = Math.Max(tile1.Y, tile2.Y);

    if (tiles.Any(t => t.X > fromX && t.X < toX && t.Y > fromY && t.Y < toY)) { return false; }

    //Check four edges for intersection with existing lines
    for (int i = 0; i < tiles.Count; i++)
    {
        var nextIndex = (i + 1) % tiles.Count;
        if (CheckIntersections(tile1.X, tile1.Y, tile2.X, tile1.Y, tiles[i].X, tiles[i].Y, tiles[nextIndex].X, tiles[nextIndex].Y))
        {
            return false;
        }
        if (CheckIntersections(tile2.X, tile1.Y, tile2.X, tile2.Y, tiles[i].X, tiles[i].Y, tiles[nextIndex].X, tiles[nextIndex].Y))
        {
            return false;
        }
        if (CheckIntersections(tile2.X, tile2.Y, tile1.X, tile2.Y, tiles[i].X, tiles[i].Y, tiles[nextIndex].X, tiles[nextIndex].Y))
        {
            return false;
        }
        if (CheckIntersections(tile1.X, tile2.Y, tile1.X, tile1.Y, tiles[i].X, tiles[i].Y, tiles[nextIndex].X, tiles[nextIndex].Y))
        {
            return false;
        }
    }

    return true;
}

bool CheckIntersections(long x1, long y1, long x2, long y2, long x3, long y3, long x4, long y4)
{
    long d1 = (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1);
    long d2 = (x2 - x1) * (y4 - y1) - (y2 - y1) * (x4 - x1);
    long d3 = (x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3);
    long d4 = (x4 - x3) * (y2 - y3) - (y4 - y3) * (x2 - x3);

    if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
        ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
    {
        return true;
    }

    return false;
}

bool IsInside(long col, long row)
{
    if (tilesWithGreen.Any(t => t.X <= col && t.Y == row) &&
        tilesWithGreen.Any(t => t.X >= col && t.Y == row) &&
        tilesWithGreen.Any(t => t.Y <= row && t.X == col) &&
        tilesWithGreen.Any(t => t.Y >= row && t.X == col))
    {
        return true;
    }
    else
    {
        return false;
    }
}

void SetTiles(string[] input, List<Tile> tiles, List<Tile> tilesWithGreen)
{
    for (var i = 0; i < input.Length; i++)
    {
        var parts = input[i].Split(',');
        tiles.Add(new Tile(long.Parse(parts[0]), long.Parse(parts[1]), Color.Red));
        tilesWithGreen.Add(new Tile(long.Parse(parts[0]), long.Parse(parts[1]), Color.Red));
        if (i > 0)
        {
            DrawGreenTiles(tiles[i - 1], tiles[i]);
        }

        if (i == input.Length - 1)
        {
            DrawGreenTiles(tiles[i], tiles[0]);
        }
    }
}

void DrawGreenTiles(Tile tile1, Tile tile2)
{
    if (tile1.X == tile2.X)
    {
        var fromY = Math.Min(tile1.Y, tile2.Y);
        var toY = Math.Max(tile1.Y, tile2.Y);
        for (long y = fromY + 1; y < toY; y++)
        {
            tilesWithGreen.Add(new Tile(tile1.X, y, Color.Green));
        }
    }

    if (tile1.Y == tile2.Y)
    {
        var fromX = Math.Min(tile1.X, tile2.X);
        var toX = Math.Max(tile1.X, tile2.X);
        for (long x = fromX + 1; x < toX; x++)
        {
            tilesWithGreen.Add(new Tile(x, tile1.Y, Color.Green));
        }
    }
}

void ShowTiles(List<Tile> tiles)
{
    Console.Clear();
    foreach (var tile in tiles)
    {
        Console.CursorLeft = (int)tile.X;
        Console.CursorTop = (int)tile.Y;
        switch (tile.Color)
        {
            case Color.Red:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Color.Green:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
        }

        Console.Write(tile.Color == Color.Red ? '#' : 'X');
    }

    Console.CursorLeft = 0;
    Console.CursorTop = (int)tiles.Max(t => t.Y) + 1;

    Console.ReadLine();

}