Console.WriteLine("Day 7");
Console.WriteLine();
string filePath = "input.txt";
using StreamReader sr = new(filePath);
string? line = await sr.ReadLineAsync();
int splitCount = 0;
Dictionary<int, long> beams = [];
beams.Add(line!.IndexOf('S'), 1);
while ((line = await sr.ReadLineAsync()) != null)
{
    Dictionary<int, long> newBeams = [];
    foreach (var (pos, count) in beams)
    {
        if (line[pos] == '^')
        {
            splitCount++;
            if (!newBeams.ContainsKey(pos - 1))
            {
                newBeams.Add(pos - 1, 0);
            }
            if (!newBeams.ContainsKey(pos + 1))
            {
                newBeams.Add(pos + 1, 0);
            }
            newBeams[pos - 1] += count;
            newBeams[pos + 1] += count;
        }
        else 
        {
            if (!newBeams.ContainsKey(pos))
            {
                newBeams.Add(pos, 0);
            }
            newBeams[pos] += count;
        }
    }
    beams = newBeams;
}

Console.WriteLine($"Day 7 part 1 answer: {splitCount}");
Console.WriteLine($"Day 7 part 2 answer: {beams.Values.Sum()}");