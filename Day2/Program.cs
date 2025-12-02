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
        if (IsInvalidPart2(id.ToString()))
        {
             invalidIdSum += id;
        }
    }
}

Console.WriteLine($"Day 2 answer = {invalidIdSum}");

bool IsInvalidPart2(string id)
{
    for (int chunkSize = 1; chunkSize < id.Length; chunkSize++)
    {
        if (id.Length % chunkSize == 0)
        {
            var chunks = id.Chunk(chunkSize);
            var pattern = new string(chunks.First());
            if (chunks.All(chunk => new string(chunk) == pattern)) 
            {
                return true;
            }
        }   
    }

    return false;
}

bool IsInvalidPart1(string id)
{
    if (id.Length % 2 != 0) return false;
    var middleLength = id.Length / 2;
    for (int i = 0; i < middleLength; i++)
    {
        if (id[i] != id[i + middleLength]) return false;
    }

    return true;
}