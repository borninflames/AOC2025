Console.WriteLine("Day 3");
Console.WriteLine();

string filePath = "input.txt";
var answer = 0L;

foreach (string line in File.ReadLines(filePath))
{
    Console.WriteLine(line);

    var maxPos = 0;
    var maxNum = new char[12];

    for (int d = 11; d >= 0; d--)
    {
        for (int i = maxPos; i < line.Length - d; i++)
        {
            if (line[i] > maxNum[d])
            {
                maxNum[d] = line[i];
                maxPos = i + 1;
            }
        }
    }

    var result = long.Parse(new string([.. maxNum.Reverse()]));
    answer += result;
}

Console.WriteLine($"Day 3 answer: {answer}");
