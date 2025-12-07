Console.WriteLine("Day 7");
Console.WriteLine();

string filePath = "input.txt";
using StreamReader sr = new(filePath);
string? line = await sr.ReadLineAsync();
int splitCount = 0;
List<int> beamCols = [
    line!.IndexOf('S')
];

Console.WriteLine(line);

while ((line = await sr.ReadLineAsync()) != null)
{
    List<int> newBeamCols = [.. beamCols];
    for (int i = 0; i < beamCols.Count; i++)
    {
        if (line[beamCols[i]] == '^')
        {
            splitCount++;
            newBeamCols.Remove(beamCols[i]);
            if (!newBeamCols.Contains(beamCols[i] - 1))
            {
                newBeamCols.Add(beamCols[i] - 1);
            }
            if (!newBeamCols.Contains(beamCols[i] + 1))
            {
                newBeamCols.Add(beamCols[i] + 1);
            }            
        }
    }

    beamCols = newBeamCols;
}

Console.WriteLine($"Day 7 answer: {splitCount}");