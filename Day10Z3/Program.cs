using Microsoft.Z3;
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
            answer += ConfigureJoltagesZ3(line);
        }

        Console.WriteLine($"Day 10 answer: {answer}");
    }

    private static int ConfigureJoltagesZ3(string line)
    {
        var machine = new Machine(line);
        Console.WriteLine(machine);
        var solution = machine.SolveWithZ3();
        if (solution != null)
        {
            Console.WriteLine($"Solution: {string.Join(", ", solution.Select((x, i) => $"Button{i}={x}"))}");
            return solution.Sum();
        }
        Console.WriteLine("No solution found");
        return 0;
    }
}

internal partial class Machine
{
    public Machine(string line)
    {
        List<List<int>> buttons = [.. ButtonsRegex().Matches(line).Select(m => m.Groups[1].Value.Split(",").Select(int.Parse).ToList())];
        Joltages = [.. JoltagesRegex().Match(line).Groups[1].Value.Split(',').Select(int.Parse).ToList()];
        Buttons = buttons;
    }

    public List<List<int>> Buttons { get; set; }

    public List<int> Joltages { get; set; }

    public int[]? SolveWithZ3()
    {
        using Context ctx = new();
        Optimize optimizer = ctx.MkOptimize(); // Solver helyett Optimize!

        // Minden gombhoz létrehozunk egy egész változót
        IntExpr[] buttonVars = new IntExpr[Buttons.Count];
        for (int i = 0; i < Buttons.Count; i++)
        {
            buttonVars[i] = ctx.MkIntConst($"button_{i}");
            // A gombnyomások száma nem-negatív
            optimizer.Assert(ctx.MkGe(buttonVars[i], ctx.MkInt(0)));
        }

        // Minden joltage indexhez felírunk egy egyenletet
        for (int joltageIdx = 0; joltageIdx < Joltages.Count; joltageIdx++)
        {
            List<ArithExpr> terms = new();

            // Összegyűjtjük, hogy melyik gombok érintik ezt az indexet
            for (int buttonIdx = 0; buttonIdx < Buttons.Count; buttonIdx++)
            {
                if (Buttons[buttonIdx].Contains(joltageIdx))
                {
                    terms.Add(buttonVars[buttonIdx]);
                }
            }

            // Ha van olyan gomb, ami érinti ezt az indexet
            if (terms.Count > 0)
            {
                ArithExpr sum = terms.Count == 1
                    ? terms[0]
                    : ctx.MkAdd(terms.ToArray());

                optimizer.Assert(ctx.MkEq(sum, ctx.MkInt(Joltages[joltageIdx])));
            }
            else if (Joltages[joltageIdx] != 0)
            {
                // Ha nincs gomb, ami érinti, de a cél nem 0, nincs megoldás
                return null;
            }
        }

        // MINIMALIZÁLÁS: A gombnyomások összegét minimalizáljuk
        ArithExpr totalPresses = buttonVars.Length == 1
            ? buttonVars[0]
            : ctx.MkAdd(buttonVars);
        optimizer.MkMinimize(totalPresses);

        // Megoldjuk
        if (optimizer.Check() == Status.SATISFIABLE)
        {
            Model model = optimizer.Model;
            int[] solution = new int[Buttons.Count];

            for (int i = 0; i < Buttons.Count; i++)
            {
                var eval = model.Evaluate(buttonVars[i], true);
                solution[i] = ((IntNum)eval).Int;
            }

            return solution;
        }

        return null; // Nincs megoldás
    }

    public override string ToString()
    {
        return $"{string.Join(" ", Buttons.Select(b => $"({string.Join(",", b)})"))} {string.Join(",", Joltages.Select(j => $"{{{j}}}"))}";
    }

    [GeneratedRegex(@"\(([^)]+)\)")]
    private static partial Regex ButtonsRegex();

    [GeneratedRegex(@"\{([^}]+)\}")]
    private static partial Regex JoltagesRegex();
}