Console.WriteLine("Day 4");
Console.WriteLine();

string filePath = "input.txt";
var answer = 0;
var rollOfPapers = await File.ReadAllLinesAsync(filePath);
var boundaryRow = new string('.', rollOfPapers.First().Length);
rollOfPapers = [.. rollOfPapers.Prepend(boundaryRow)];
rollOfPapers = [.. rollOfPapers.Append(boundaryRow)];

rollOfPapers = rollOfPapers.Select(line => $".{line}.").ToArray();

for (int row = 1; row < rollOfPapers.Length - 1; row++)
{
    for (int col = 1; col < rollOfPapers.First().Length - 1; col++)
    {
        Console.Write(rollOfPapers[row][col]);
        if (rollOfPapers[row][col] == '@')
        {
            var adjacentRolls = rollOfPapers[row - 1][(col - 1)..(col + 2)].Count(c => c == '@');
            adjacentRolls += (rollOfPapers[row][(col - 1)..(col + 2)].Count(c => c == '@') - 1);
            adjacentRolls += rollOfPapers[row + 1][(col - 1)..(col + 2)].Count(c => c == '@');

            if (adjacentRolls < 4)
            {
                answer++;
            }
        }
    }
    Console.WriteLine();
}



Console.WriteLine($"Day 4 answer: {answer}");
