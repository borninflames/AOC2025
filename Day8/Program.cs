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


var isAnswerFound = false;
for (int i = 0; i < distances.Count && !isAnswerFound; i++)
{
    Console.WriteLine($"Distance {distances[i].Distance:0.00} between {distances[i].A} and {distances[i].B}");

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

            if (circuit.Count == jBoxes.Count)
            {
                isAnswerFound = true;
                Console.WriteLine($"Merged into one circuit... Answer part 2 is: {d.A.X * d.B.X}");
            }
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

static double GetDistance(JBox a, JBox b)
{
    double dx = a.X - b.X;
    double dy = a.Y - b.Y;
    double dz = a.Z - b.Z;
    return Math.Sqrt(dx * dx + dy * dy + dz * dz);
}

record JBox(int X, int Y, int Z);

record JBoxDistance(double Distance, JBox A, JBox B);