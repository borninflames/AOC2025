Console.WriteLine("Day 2");
Console.WriteLine();

string filePath = "input.txt";

var input = await File.ReadAllTextAsync(filePath);
var ranges = input.Split(',').Select(r => r.Split('-').Select(long.Parse).ToArray()).ToArray();

var invalidIdSum = 0L;
foreach (var range in ranges)
{
    for (long id = range[0]; id <= range[1]; id++)
    {
        if (IsInvalid(id.ToString()))
        {
             invalidIdSum += id;
        }
    }
}

Console.WriteLine($"Day 2 answer = {invalidIdSum}");

bool IsInvalid(string id)
{
    if (id.Length % 2 != 0) return false;
    var middleLength = id.Length / 2;
    for (int i = 0; i < middleLength; i++)
    {
        if (id[i] != id[i + middleLength]) return false;
    }

    return true;
}