using System.Collections.Concurrent;

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

var pathCache = new ConcurrentDictionary<(string, string), long>();

long CountPaths(string current, string target, HashSet<string> pathVisited)
{
    if (pathCache.TryGetValue((current, target), out long cachedResult))
    {
        return cachedResult;
    }

    if (current == target) { return 1; }
    if (!graph.ContainsKey(current)) { return 0; }

    long totalPaths = 0;
    pathVisited.Add(current);
    foreach (var neighbor in graph[current])
    {
        if (!pathVisited.Contains(neighbor))
        {
            totalPaths += CountPaths(neighbor, target, pathVisited);
        }
    }

    pathVisited.Remove(current); // Backtrack

    // Store result in cache before returning
    pathCache[(current, target)] = totalPaths;

    return totalPaths;
}

//Part1
//var answer = CountPaths("you", "out", new HashSet<string>());

//Part2
var svrToFftPathsTask = Task.Run(() =>
{
    var svrToFftPaths = CountPaths("svr", "fft", []);
    Console.WriteLine($"svrToFftPaths: {svrToFftPaths}");
    return svrToFftPaths;
});
var fftToDacPathsTask = Task.Run(() =>
{
    var fftToDacPaths = CountPaths("fft", "dac", []);
    Console.WriteLine($"fftToDacPaths: {fftToDacPaths}");
    return fftToDacPaths;
});
var dacToOutPathsTask = Task.Run(() =>
{
    var dacToOutPaths = CountPaths("dac", "out", []);
    Console.WriteLine($"dacToOutPaths: {dacToOutPaths}");
    return dacToOutPaths;
});

await Task.WhenAll(svrToFftPathsTask, fftToDacPathsTask, dacToOutPathsTask);

var svrToFftPaths = svrToFftPathsTask.Result;
var fftToDacPaths = fftToDacPathsTask.Result;
var dacToOutPaths = dacToOutPathsTask.Result;

var answer = svrToFftPaths * fftToDacPaths * dacToOutPaths;
Console.WriteLine($"Day 11 answer: {answer}");