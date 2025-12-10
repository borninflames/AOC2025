
using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Day 10");
        Console.WriteLine();
        string filePath = "input.txt";
        var input = await File.ReadAllLinesAsync(filePath);

        var answer = 0;

        foreach (var line in input)
        {
            answer += ConfigureLights(line);
        }

        Console.WriteLine($"Day 10 answer: {answer}");
    }

    private static int ConfigureLights(string line)
    {
        var machineButtonPresses = new Machine(line).Find();

        Console.WriteLine(machineButtonPresses);
        return machineButtonPresses;
    }
}

internal partial class Machine
{
    public Machine(string line)
    {
        var match = LightsRegex().Match(line);
        string lightDiagram = match.Groups[1].Value;

        List<List<int>> buttons = [.. NumbersRegex().Matches(line).Select(m => m.Groups[1].Value.Split(",").Select(int.Parse).ToList())];

        LightDiagram = lightDiagram;
        Buttons = buttons;
        Lights = lightDiagram.Replace("#", ".").ToCharArray();
    }

    public string LightDiagram { get; set; }

    public char[] Lights { get; set; }

    public List<List<int>> Buttons { get; set; }

    private readonly List<List<char[]>> lightStates = [];

    public int Find()
    {
        lightStates.Add([Lights]);
        while (true)
        {
            lightStates.Add([]);
            foreach (var lightState in lightStates[^2])
            {
                foreach (var button in Buttons)
                {
                    var newLigthState = (char[])lightState.Clone();
                    foreach (var index in button)
                    {
                        newLigthState[index] = lightState[index] == '#' ? '.' : '#';                        
                    }
                    lightStates[^1].Add((char[])newLigthState.Clone());
                    if (new string(newLigthState) == LightDiagram)
                    {
                        return lightStates.Count -1;
                    }                    
                }
            }
        }
    }




    public override string ToString()
    {
        return $"[{LightDiagram}], Buttons: {string.Join(" ", Buttons.Select(b => $"({string.Join(",", b)})"))}";
    }

    [GeneratedRegex(@"\[(.+?)\]")]
    private static partial Regex LightsRegex();

    [GeneratedRegex(@"\(([^)]+)\)")]
    private static partial Regex NumbersRegex();
}