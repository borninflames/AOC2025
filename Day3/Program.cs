Console.WriteLine("Day 3");
Console.WriteLine();

string filePath = "input.txt";
var answer = 0;

foreach (string line in File.ReadLines(filePath))
{
    Console.WriteLine(line);

    var maxPos = 0;
    var dec1 = '0';
    for (int i = 0; i < line.Length-1; i++)
    {
        if (line[i] > dec1)
        {
            dec1 = line[i];
            maxPos = i;
        }
    }
    var dec2 = '0';
    for (int i = maxPos+1; i < line.Length; i++)
    {
        if (line[i] > dec2)
        {
            dec2 = line[i];
        }
    }

    var result = int.Parse($"{dec1}{dec2}");
    answer += result;
}

Console.WriteLine($"Day 3 answer: {answer}");
