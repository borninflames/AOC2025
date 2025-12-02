Console.WriteLine("Day 1");
Console.WriteLine();

string filePath = "inputa.txt";

var pos = 50;
var zeroPositions = 0;

foreach (string line in File.ReadLines(filePath))
{
    Console.WriteLine($"Position: {pos}");
    Console.WriteLine(line);

    var dir = line[0];
    var dist = int.Parse(line[1..]);
    
    zeroPositions += dist / 100;
    Console.WriteLine($"Full Circles: {dist / 100}");
    dist %= 100;

    if (dir == 'R')
    {        
        pos += dist;
        if (pos >= 100)
        {            
            zeroPositions++;
            Console.WriteLine($"Full Circles++ {zeroPositions}");
        }
    }
    else if (dir == 'L')
    {
        var origPos = pos;
        pos -= dist;
        if (origPos != 0 && pos <= 0)
        {            
            zeroPositions++;
            Console.WriteLine($"Full Circles++ {zeroPositions}");
        }
    }

    pos %= 100;
    pos = pos >= 0 ? pos : pos + 100;

    //Day 1 a calculations
    //if (pos == 0)
    //{
    //    zeroPositions++;
    //}   
}

Console.WriteLine($"Day 1a answer; Zero positions: {zeroPositions}");
Console.ReadLine();