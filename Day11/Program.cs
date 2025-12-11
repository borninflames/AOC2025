Console.WriteLine("Day 11");
Console.WriteLine();
string filePath = "input.txt";
var input = await File.ReadAllLinesAsync(filePath);

var graph = new Dictionary<string, List<string>>();

foreach (var line in input)
{
    var parts = line.Split(':', StringSplitOptions.TrimEntries);
    var node = parts[0];
    var neighbors = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    
    graph[node] = neighbors;
}

int CountPaths(string current, string target, HashSet<string> pathVisited)
{
    if (current == target)
        return 1;

    if (!graph.ContainsKey(current))
        return 0;

    int totalPaths = 0;
    pathVisited.Add(current);

    foreach (var neighbor in graph[current])
    {
        if (!pathVisited.Contains(neighbor))
        {
            totalPaths += CountPaths(neighbor, target, pathVisited);
        }
    }

    pathVisited.Remove(current); // Backtrack
    return totalPaths;
}

var answer = CountPaths("you", "out", new HashSet<string>());
Console.WriteLine($"Day 11 answer: {answer}");

