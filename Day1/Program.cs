Console.WriteLine("Day 1");
Console.WriteLine();

string filePath = "inputa.txt";

var pos = 50;
var zeroPositions = 0;

foreach (string line in File.ReadLines(filePath))
{
    Console.WriteLine(line);

    var dir = line[0];
    var dist = int.Parse(line[1..]);
    if (dir == 'R')
    {
        pos += dist;
        
    }
    else if (dir == 'L')
    {
        pos -= dist;
    }

    pos %= 100;
    //pos = pos > 0 ? pos : pos + 100;

    if (pos == 0)
    {
        zeroPositions++;
    }

    Console.WriteLine($"Position: {pos}");
}

Console.WriteLine($"Day 1a answer; Zero positions: {zeroPositions}");
Console.ReadLine();