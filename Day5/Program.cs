Console.WriteLine("Day 5");
Console.WriteLine();

string filePath = "input.txt";
var answer = 0;
List<(long, long)> ranges = [];


using (var reader = new StreamReader(filePath))
{
    string? line;
    bool isFirststage = true;
    while (isFirststage)
    {
        line = await reader.ReadLineAsync();
        Console.WriteLine(line);

        if (string.IsNullOrEmpty(line))
        {
            isFirststage = false;
            continue;
        }

        var rangeParts = line.Split('-').Select(long.Parse).ToArray();
        ranges.Add((rangeParts[0], rangeParts[1]));
    }

    while ((line = await reader.ReadLineAsync()) != null)
    {
        var prodId = long.Parse(line);
        var isFresh = ranges.Any(r => r.Item1 <= prodId && r.Item2 >= prodId);
        if (isFresh)
        {
            Console.WriteLine($"Fresh product: {prodId}");
            answer++;
        }
    }
}

Console.WriteLine($"Day 5 answer: {answer}");