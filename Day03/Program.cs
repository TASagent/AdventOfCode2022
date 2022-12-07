const string inputFile = @"../../../../input03.txt";

Console.WriteLine("Day 03 - Rucksack Reorganization");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

List<Rucksack> rucksacks = File.ReadAllLines(inputFile).Select(x => new Rucksack(x)).ToList();

List<char> relevantChars = new List<char>();

for (char c = 'a'; c <= 'z'; c++)
{
    relevantChars.Add(c);
}

for (char c = 'A'; c <= 'Z'; c++)
{
    relevantChars.Add(c);
}


int prioritySum = 0;

foreach (char c in relevantChars)
{
    foreach (Rucksack deeznuts in rucksacks)
    {
        int deezsacks = deeznuts.GetPack(c);

        if (deezsacks == 3)
        {
            prioritySum += GetPriority(c);
        }
    }
}


Console.WriteLine($"The answer is: {prioritySum}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

List<ElfGroup> elfGroups = new List<ElfGroup>();

for (int i = 0; i < rucksacks.Count / 3; i++)
{
    elfGroups.Add(new ElfGroup(rucksacks, i));
}

int value2 = elfGroups.Select(x => x.GetBadge()).Select(GetPriority).Sum();

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

static int GetPriority(char c)
{
    if (c >= 'a' && c <= 'z')
    {
        return (c - 'a' + 1);
    }

    if (c >= 'A' && c <= 'Z')
    {
        return (c - 'A' + 27);
    }

    throw new Exception($"Bad value: {c}");
}

class ElfGroup
{
    private readonly List<Rucksack> rucksacks = new List<Rucksack>();


    public ElfGroup(List<Rucksack> rucksacks, int index)
    {
        for (int i = 0; i < 3; i++)
        {
            this.rucksacks.Add(rucksacks[3 * index + i]);
        }
    }

    public char GetBadge()
    {
        HashSet<char> sack1 = new HashSet<char>(rucksacks[0].contents);
        HashSet<char> sack2 = new HashSet<char>(rucksacks[1].contents);
        HashSet<char> sack3 = new HashSet<char>(rucksacks[2].contents);

        return sack1.Intersect(sack2).Intersect(sack3).First();
    }
}

class Rucksack
{
    private readonly List<char> bag1 = new List<char>();
    private readonly List<char> bag2 = new List<char>();

    public readonly string contents;

    public Rucksack(string line)
    {
        if (line.Length % 2 == 1)
        {
            throw new Exception($"Someone fucked something up: {line}");
        }

        bag1.AddRange(line.Substring(0, line.Length / 2));
        bag2.AddRange(line.Substring(line.Length / 2, line.Length / 2));

        contents = line;
    }

    /// <summary>
    /// returns 0 if not found
    /// </summary>
    public int GetPack(char c)
    {
        int bag = 0;

        if (bag1.Contains(c))
        {
            bag |= 1;
        }

        if (bag2.Contains(c))
        {
            bag |= (1 << 1);
        }

        return bag;
    }
}
