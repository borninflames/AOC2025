using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Day 10");
        Console.WriteLine();
        string filePath = "input0.txt";
        var input = await File.ReadAllLinesAsync(filePath);

        var answer = 0;

        foreach (var line in input)
        {
            //answer += ConfigureLights(line);
            answer += ConfigureJoltages(line);
        }

        Console.WriteLine($"Day 10 answer: {answer}");
    }

    private static int ConfigureLights(string line)
    {
        var machineButtonPresses = new Machine(line).Find();

        Console.WriteLine(machineButtonPresses);
        return machineButtonPresses;
    }

    private static int ConfigureJoltages(string line)
    {
        var machine = new Machine(line);
        Console.WriteLine(machine);
        var machineButtonPresses = machine.Find2();
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

        List<List<int>> buttons = [.. ButtonsRegex().Matches(line).Select(m => m.Groups[1].Value.Split(",").Select(int.Parse).ToList())];

        Joltages = [.. JoltagesRegex().Match(line).Groups[1].Value.Split(',').Select(int.Parse).ToList()];

        LightDiagram = lightDiagram;
        Buttons = buttons;
        Lights = lightDiagram.Replace("#", ".").ToCharArray();
    }

    public string LightDiagram { get; set; }

    public char[] Lights { get; set; }

    public List<List<int>> Buttons { get; set; }

    public List<int> Joltages { get; set; }

    private readonly List<List<char[]>> lightStates = [];
    private readonly List<Level> levels = [];

    public int Find2()
    {
        //Calulate the max number of times each button can be pressed maximum
        List<int> maxPressableButtons = [.. Enumerable.Repeat(int.MaxValue, Buttons.Count)];
        for (int i = 0; i < Buttons.Count; i++)
        {
            List<int> button = Buttons[i];
            foreach (var index in button)
            {
                maxPressableButtons[i] = Math.Min(maxPressableButtons[i], Joltages[index]);
            }
        }

        //Initialize level #0
        levels.Add(new Level
        {
            CounterStates =
            [new CounterState {
                Counters = [.. Enumerable.Repeat(0, Joltages.Count)],
                MaxPressableButtons = maxPressableButtons }]
        });

        Console.WriteLine($"Initial level: {levels[0]}");

        while (true)
        {
            levels.Add(new Level());
            foreach (var counterState in levels[^2].CounterStates)
            {
                for (int i = 0; i < Buttons.Count; i++)
                {
                    //check if this button can still be pressed
                    if (counterState.MaxPressableButtons[i] == 0)
                    {
                        continue;
                    }

                    //Create a copy of the current counter state
                    CounterState newCounterState = new()
                    {
                        Counters = [.. counterState.Counters],
                        MaxPressableButtons = [.. counterState.MaxPressableButtons]
                    };

                    var isNewCounterStateOK = true;
                    List<int>? button = Buttons[i];
                    //Increase the counters for the pressed button
                    foreach (var index in button)
                    {
                        newCounterState.Counters[index]++;
                        if (newCounterState.Counters[index] > Joltages[index])
                        {
                            isNewCounterStateOK = false;
                            break;
                        }
                    }

                    if (isNewCounterStateOK)
                    {
                        newCounterState.MaxPressableButtons[i]--;

                        if (newCounterState.Counters.SequenceEqual(Joltages))
                        {
                            return levels.Count - 1;
                        }

                        levels[^1].CounterStates.Add(newCounterState);
                        //Console.WriteLine($"Level {levels.Count - 1}: counters: {string.Join(",", newCounterState.Counters.Select(c => c.ToString()))} | max pressable: {string.Join(",", newCounterState.MaxPressableButtons.Select(c => c.ToString()))}");
                    }
                }
            }
        }
    }

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
                        return lightStates.Count - 1;
                    }
                }
            }
        }
    }

    public override string ToString()
    {
        return $"[{LightDiagram}] {string.Join(" ", Buttons.Select(b => $"({string.Join(",", b)})"))} {string.Join(",", Joltages.Select(j => $"{{{j}}}"))}";
    }

    internal class Level
    {
        public List<CounterState> CounterStates { get; set; } = [];

        public override string ToString()
        {
            return $"Level with {CounterStates.Count} counter states";
        }
    }

    internal class CounterState
    {
        public List<int> Counters { get; set; } = [];
        public List<int> MaxPressableButtons { get; set; } = [];
    }

    [GeneratedRegex(@"\[(.+?)\]")]
    private static partial Regex LightsRegex();

    [GeneratedRegex(@"\(([^)]+)\)")]
    private static partial Regex ButtonsRegex();

    [GeneratedRegex(@"\{([^}]+)\}")]
    private static partial Regex JoltagesRegex();
}