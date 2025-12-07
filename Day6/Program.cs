Console.WriteLine("Day 6");
Console.WriteLine();

string filePath = "input.txt";
var rawInput = await File.ReadAllLinesAsync(filePath);
var rotatedInput = new string[rawInput[0].Length];
List<KeyValuePair<char, List<long>>> columns = [];
List<long> nums = [];
for (int i = rawInput[0].Length - 1; i >= 0; i--)
{    
    for (int j = 0; j < rawInput.Length; j++)
    {
        if ( j < rawInput.Length -1)
        {
            rotatedInput[i] += rawInput[j][i];
        }
        else 
        {
            nums.Add(long.Parse(rotatedInput[i].Trim()));
            if (rawInput[j][i] != ' ')
            {
                columns.Add(new KeyValuePair<char, List<long>>(rawInput[j][i], nums));
                i--;
                nums = [];
            }
        }
        
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

Console.WriteLine($"Day 6 answer: {answer}");