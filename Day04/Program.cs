const string inputFile = @"../../../../input04.txt";

Console.WriteLine("Day 04 - Camp Cleanup");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

(Elf, Elf)[] groups = lines.Select(TranslateLine).ToArray();

int value = groups.Count(x => x.Item1.Contains(x.Item2) || x.Item2.Contains(x.Item1));

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int value2 = groups.Count(x => x.Item1.Overlaps(x.Item2) || x.Item2.Overlaps(x.Item1));

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

(Elf, Elf) TranslateLine(string line)
{
    string[] segments = line.Split(',');
    return (new Elf(segments[0]), new Elf(segments[1]));
}


public class Elf
{
    private readonly int lowerBound;
    private readonly int upperBound;

    public Elf(string line)
    {
        string[] segments = line.Split('-');

        lowerBound = int.Parse(segments[0]);
        upperBound = int.Parse(segments[1]);
    }

    public bool Contains(Elf other) =>
        lowerBound <= other.lowerBound &&
        upperBound >= other.upperBound;

    public bool Overlaps(Elf other) =>
        (lowerBound <= other.lowerBound && upperBound >= other.lowerBound) ||
        (upperBound >= other.upperBound && lowerBound <= other.upperBound);
}
