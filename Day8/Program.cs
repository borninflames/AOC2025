Console.WriteLine("Day 8");
Console.WriteLine();
string filePath = "input.txt";
var input = await File.ReadAllLinesAsync(filePath);
var jBoxes = input.Select(l =>
{
    var coords = l.Split(',').Select(int.Parse).ToArray();
    return new JBox(coords[0], coords[1], coords[2]);
}).ToList();

List<JBoxDistance> distances = [];

List<List<JBox>> circuits = [];
for (int i = 0; i < jBoxes.Count - 1; i++)
{
    for (int j = i + 1; j < jBoxes.Count; j++)
    {
        var distance = GetDistance(jBoxes[i], jBoxes[j]);
        distances.Add(new JBoxDistance(distance, jBoxes[i], jBoxes[j]));
    }
}

distances = [.. distances.OrderBy(d => d.Distance)];

for (int i = 0; i < 1000; i++)
{
    Console.WriteLine($"Distance {distances[i].Distance:0.##} between {distances[i].A} and {distances[i].B}");

    var d = distances[i];
    var foundCircuits = circuits.Where(c => c.Contains(d.A) || c.Contains(d.B)).ToList();
    if (foundCircuits.Count > 0)
    {
        if (foundCircuits.Count == 1)
        {
            var circuit = foundCircuits[0];
            if (!circuit.Contains(d.A))
                circuit.Add(d.A);
            if (!circuit.Contains(d.B))
                circuit.Add(d.B);
        }
        else if (foundCircuits.Count == 2)
        {
            foundCircuits[0].AddRange(foundCircuits[1]);
            circuits.Remove(foundCircuits[1]);
        }        
    }
    else
    {
        circuits.Add([d.A, d.B]);
    }
}

var circuitSizes = circuits.OrderByDescending(c => c.Count).Select(c => c.Count).Take(3).ToList();

var answer = 1;
foreach (var size in circuitSizes)
{    
    answer *= size;
}


Console.WriteLine($"Day 8 part 1 answer: {answer}");
//Console.WriteLine($"Day 8 part 2 answer: {beams.Values.Sum()}");

static double GetDistance(JBox a, JBox b)
{
    double dx = a.X - b.X;
    double dy = a.Y - b.Y;
    double dz = a.Z - b.Z;
    return Math.Sqrt(dx * dx + dy * dy + dz * dz);
}

record JBox(int X, int Y, int Z);

record JBoxDistance(double Distance, JBox A, JBox B);