const string inputFile = @"../../../../input07.txt";

Console.WriteLine("Day 07 - No Space Left On Device");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

List<AoCDirectory> directories = new List<AoCDirectory>();

AoCDirectory root = new AoCDirectory("/", null);

directories.Add(root);

AoCDirectory currentDirectory = root;

for (int i = 1; i < lines.Length; i++)
{
    if (lines[i] == "$ ls")
    {
        continue;
    }
    else if (lines[i].StartsWith("$ cd "))
    {
        //Handle changing directory
        string target = lines[i][5..];

        if (target == "..")
        {
            currentDirectory = currentDirectory.Parent;
        }
        else
        {
            currentDirectory = currentDirectory.GetOrAddChildDir(target, directories);
        }
    }
    else
    {
        if (char.IsNumber(lines[i][0]))
        {
            //It's a file

            string[] splitLine = lines[i].Split(' ', 2);
            currentDirectory.AddChild(new AoCFile(splitLine[1], long.Parse(splitLine[0]), currentDirectory));
        }
        else
        {
            //It's a directory
            currentDirectory.GetOrAddChildDir(lines[i][4..], directories);
        }
    }
}

long value = directories.Where(x => x.Size < 100_000).Sum(x => x.Size);

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

long freeSpace = 70_000_000 - root.Size;

long neededSpace = 30_000_000 - freeSpace;

long value2 = directories
    .Select(x => x.Size)
    .Where(x => x >= neededSpace)
    .Order()
    .First();

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

abstract class AoCNode
{
    public AoCDirectory Parent { get; }
    public string Name { get; set; }
    public abstract long Size { get; }

    public AoCNode(string name, AoCDirectory parent)
    {
        Name = name;
        Parent = parent;
    }

}

class AoCDirectory : AoCNode
{
    public List<AoCNode> Children { get; } = new List<AoCNode>();

    public override long Size => Children.Sum(x=> x.Size);

    public AoCDirectory(string name, AoCDirectory parent)
        : base(name, parent)
    {
    }

    public void AddChild(AoCNode child) => Children.Add(child);

    public AoCDirectory GetOrAddChildDir(string name, List<AoCDirectory> directories)
    {
        AoCDirectory target = Children.FirstOrDefault(x => x.Name == name) as AoCDirectory;

        if (target is null)
        {
            target = new AoCDirectory(name, this);
            directories.Add(target);
            AddChild(target);
        }

        return target;
    }
}

class AoCFile : AoCNode
{
    public override long Size { get; }

    public AoCFile(string name, long size, AoCDirectory parent)
        : base(name, parent)
    {
        Size = size;
    }
}