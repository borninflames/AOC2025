using Day12;

Console.WriteLine("Day 12");
Console.WriteLine();
string filePath = "input.txt";
var input = await File.ReadAllLinesAsync(filePath);

PresentType[] presentTypes = GetPresentTypes(input);
List<Area> areas = GetAreas(input);
int answer = 0;
foreach (var area in areas)
{
    var areaSize = area.Cols * area.Rows;
    var requiredSize = presentTypes.Select((pt, index) => pt.Size * area.RemainingPresentsToPlace[index]).Sum();
    
    if (areaSize >= requiredSize)
    {
        answer++;
    }
}

Console.WriteLine($"Day 12 answer: {answer}");

PresentType[] GetPresentTypes(string[] input)
{
    var presentTypes = new PresentType[6];
    var presentIndex = -1;
    for (int i = 0; i < 29; i++)
    {
        if (string.IsNullOrWhiteSpace(input[i])) { continue; }
        if (input[i].Length == 2)
        {
            presentIndex++;
            presentTypes[presentIndex] = new PresentType();
            continue;
        }

        if (input[i].Length == 3)
        {
            for (int j = 0; j < 3; i++, j++)
            {
                var lineSize = input[i].Count(c => c == '#');
                presentTypes[presentIndex].Size += lineSize;
            }
        }
    }
    
    return presentTypes;
}

List<Area> GetAreas(string[] input)
{
    List<Area> areas = [];
    for (int i = 30; i < input.Length; i++)
    {
        var parts = input[i].Split(':');
        var sizeParts = parts[0].Split('x').Select(int.Parse).ToArray();
        var remainingPresentPlaces = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
        var area = new Area
        {
            Cols = sizeParts[0],
            Rows = sizeParts[1],
            RemainingPresentsToPlace = remainingPresentPlaces
        };

        areas.Add(area);
    }

    return areas;
}