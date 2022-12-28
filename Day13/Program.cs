const string inputFile = @"../../../../input13.txt";
//const string inputFile = @"../../../../input13test.txt";

Console.WriteLine("Day 13 - Distress Signal");
Console.WriteLine("Star 1");
Console.WriteLine();

//Sum of indices of pairs in the right order

//string[] lines = File.ReadAllLines(inputFile);
string[] pairData = File.ReadAllText(inputFile).Replace("10", "A").Split("\r\n\r\n");


//Construct

List<(ListNode left, ListNode right)> pairs =
    new List<(ListNode left, ListNode right)>();

foreach (string pairDatum in pairData)
{
    string[] elementData = pairDatum.Split("\r\n");


    IEnumerator<char> characterEnumerator = elementData[0].GetEnumerator();
    characterEnumerator.MoveNext();
    ListNode left = new ListNode(null, characterEnumerator);

    characterEnumerator = elementData[1].GetEnumerator();
    characterEnumerator.MoveNext();
    ListNode right = new ListNode(null, characterEnumerator);

    pairs.Add((left, right));
}


int value = pairs
    .Select((x, i) => (x.left, x.right, i+1))
    .Where(x => TestOrder(x.left, x.right) == Result.Correct)
    .Sum(x => x.Item3);

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

List<string> allData = File.ReadAllText(inputFile)
    .Replace("10", "A")
    .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
    .ToList();

//Adding specified elements
allData.Add("[[2]]");
allData.Add("[[6]]");

List<ListNode> allNodes = allData.Select(x =>
{
    IEnumerator<char> characterEnumerator = x.GetEnumerator();
    characterEnumerator.MoveNext();
    return new ListNode(null, characterEnumerator);
}).ToList();

ListNode firstInsert = allNodes[^2];
ListNode secondInsert = allNodes[^1];

allNodes.Sort((ListNode left, ListNode right) => (TestOrder(left, right) == Result.Correct) ? -1 : 1);

int value2 = (allNodes.IndexOf(firstInsert) + 1) * (allNodes.IndexOf(secondInsert) + 1);

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();


Result TestOrder(Node left, Node right)
{
    if (left is ValueNode leftValue && right is ValueNode rightValue)
    {
        if (leftValue.value < rightValue.value)
        {
            return Result.Correct;
        }
        else if (leftValue.value == rightValue.value)
        {
            return Result.Continue;
        }
        else
        {
            return Result.Incorrect;
        }
    }

    ListNode leftListNode = left.AsListNode;
    ListNode rightListNode = right.AsListNode;

    int comparableNodeCount = Math.Min(leftListNode.nodes.Count, rightListNode.nodes.Count);

    for (int i = 0; i < comparableNodeCount; i++)
    {
        Result innerResult = TestOrder(leftListNode[i], rightListNode[i]);

        if (innerResult != Result.Continue)
        {
            return innerResult;
        }
    }

    if (leftListNode.nodes.Count < rightListNode.nodes.Count)
    {
        return Result.Correct;
    }
    else if (leftListNode.nodes.Count == rightListNode.nodes.Count)
    {
        return Result.Continue;
    }
    else
    {
        return Result.Incorrect;
    }
}

enum Result
{
    Correct,
    Incorrect,
    Continue
}

abstract class Node
{
    public readonly Node parent;

    public Node(Node parent)
    {
        this.parent = parent;
    }

    public abstract ListNode AsListNode { get; }
}

class ListNode : Node
{
    public readonly List<Node> nodes = new List<Node>();

    /// <summary>
    /// Invoked to create a virtual single-item list
    /// </summary>
    public ListNode(Node parent, int value)
        : base(parent)
    {
        nodes.Add(new ValueNode(this, value));
    }

    public ListNode(Node parent, IEnumerator<char> characterEnumerator)
        : base(parent)
    {
        while (characterEnumerator.MoveNext())
        {
            switch (characterEnumerator.Current)
            {
                case ']': return;
                case ',': continue;

                case '[':
                    nodes.Add(new ListNode(this, characterEnumerator));
                    continue;

                default:
                    nodes.Add(new ValueNode(
                        parent: this,
                        value: int.Parse($"{characterEnumerator.Current}", System.Globalization.NumberStyles.HexNumber)));
                    break;
            }
        }
    }

    public Node this[int i] => nodes[i];

    public override ListNode AsListNode => this;
}

class ValueNode : Node
{
    public readonly int value;

    public ValueNode(Node parent, int value)
        : base(parent)
    {
        this.value = value;
    }

    public override ListNode AsListNode => new ListNode(parent, value);
}