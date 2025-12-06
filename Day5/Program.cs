Console.WriteLine("Day 5");
Console.WriteLine();

string filePath = "input.txt";
var answer = 0L;
List<(long, long)> ranges = [];


answer = await Part2(filePath, answer, ranges);

Console.WriteLine($"Day 5 answer: {answer}");

static async Task<long> Part2(string filePath, long answer, List<(long, long)> ranges)
{
    using (var reader = new StreamReader(filePath))
    {
        string? line;
        bool isFirststage = true; // collect all ranges
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
    }
    ranges.Sort();
    List<(long, long)> mergedRanges = MergeRanges(ranges);

    foreach (var range in mergedRanges)
    {
        answer += (range.Item2 - range.Item1 + 1);
    }


    return answer;
}

static List<(long, long)> MergeRanges(List<(long, long)> ranges)
{
    List<(long, long)> mergedRanges = [];

    while (ranges.Count > 0)
    {
        var currentRange = ranges[0];
        ranges.RemoveAt(0);

        var hasOverlappingRange = true;
        while (hasOverlappingRange)
        {
            var overlappingRange = ranges.FirstOrDefault(r =>
            r.Item1 >= currentRange.Item1 && r.Item1 <= currentRange.Item2 ||
            r.Item2 >= currentRange.Item1 && r.Item2 <= currentRange.Item2);
            if (overlappingRange == default)
            {
                hasOverlappingRange = false;
                break;
            }

            currentRange = (Math.Min(currentRange.Item1, overlappingRange.Item1), 
                Math.Max(currentRange.Item2, overlappingRange.Item2));
            ranges.Remove(overlappingRange);
        }

        mergedRanges.Add(currentRange);
    }

    return mergedRanges;
}

static async Task<int> Part1(string filePath, int answer, List<(long, long)> ranges)
{
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

    return answer;
}