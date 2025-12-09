Console.WriteLine("Day 9");
Console.WriteLine();
string filePath = "input.txt";
var input = await File.ReadAllLinesAsync(filePath);
var tiles = new List<Tile>();
foreach (var line in input)
{
    var parts = line.Split(',');
    tiles.Add(new Tile(long.Parse(parts[0]), long.Parse(parts[1])));
}

var maxArea = 0L;
for (int i = 0; i < tiles.Count; i++)
{
    for (int j = i + 1; j < tiles.Count; j++)
    {
        var area = GetArea(tiles[i], tiles[j]);
        if (area > maxArea)
        {
            maxArea = area;
        }
    }
}

Console.WriteLine($"Max Area: {maxArea}");

long GetArea(Tile t1, Tile t2)
{
    return Math.Abs(t1.X - t2.X + 1) * Math.Abs(t1.Y - t2.Y + 1);
}

record Tile(long X, long Y);
