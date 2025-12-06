Console.WriteLine("Day 6");
Console.WriteLine();

string filePath = "input.txt";
var rawInput = await File.ReadAllLinesAsync(filePath);
List<KeyValuePair<char, List<long>>> columns = [];

var ops = rawInput[rawInput.Length - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToList();

for (var i = 0; i < ops.Count; i++)
{
    columns.Add(new KeyValuePair<char, List<long>>(ops[i], []));
}

for (var i = 0; i < rawInput.Length - 1; i++)
{
    Console.WriteLine(rawInput[i]);
    var line = rawInput[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

    for (var j = 0; j < line.Count; j++)
    {
        columns[j].Value.Add(line[j]);
    }
}

var answer = 0L;

foreach (var col in columns)
{
    switch (col.Key)
    {
        case '+':
            answer += col.Value.Sum();
            break;
        case '*':
            var prod = 1L;
            foreach (var val in col.Value)
            {
                prod *= val;
            }
            answer += prod;
            break;
    }
}



//answer = await Part1(filePath, answer, ranges);

Console.WriteLine($"Day 6 answer: {answer}");


static async Task<long> Part1(string filePath, long answer, List<(long, long)> ranges)
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