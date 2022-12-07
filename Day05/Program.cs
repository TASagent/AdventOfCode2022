const string inputFile = @"../../../../input05.txt";

Console.WriteLine("Day 05 - Supply Stacks");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

List<Stack<char>> stacks = new List<Stack<char>>();
List<Stack<char>> stacksCopy = new List<Stack<char>>();
List<Stack<char>> initialStacks = new List<Stack<char>>();


for (int i = 0; i < 9; i++)
{
    stacks.Add(new Stack<char>());
    stacksCopy.Add(new Stack<char>());
    initialStacks.Add(new Stack<char>());
}

List<StackMove> stackMoves = new List<StackMove>();

ParsingPhase phase = 0;

for (int i = 0; i < lines.Length; i++)
{
    string line = lines[i];

    switch (phase)
    {
        case ParsingPhase.Stacks:
            if (line[1] == '1')
            {
                goto case ParsingPhase.Labels;
            }

            for (int j = 0; j < 9; j++)
            {
                if (line[1 + 4 * j] != ' ')
                {
                    initialStacks[j].Push(line[1 + 4 * j]);
                }
            }
            break;

        case ParsingPhase.Labels:
            i++;
            phase = ParsingPhase.Moves;

            for (int j = 0; j < 9; j++)
            {
                while (initialStacks[j].Count > 0)
                {
                    char newChar = initialStacks[j].Pop();

                    stacks[j].Push(newChar);
                    stacksCopy[j].Push(newChar);
                }
            }

            continue;

        case ParsingPhase.Moves:
            stackMoves.Add(new StackMove(line));
            break;

        default:
            break;
    }
}


foreach (StackMove move in stackMoves)
{
    move.DoItNerd(stacks);
}


string output = "";

foreach (Stack<char> stack in stacks)
{
    output += stack.Peek();
}

Console.WriteLine($"The answer is: {output}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

foreach (StackMove move in stackMoves)
{
    move.DoItAgainNerd(stacksCopy);
}


string output2 = "";

foreach (Stack<char> stack in stacksCopy)
{
    output2 += stack.Peek();
}

Console.WriteLine($"The answer is: {output2}");

Console.WriteLine();
Console.ReadKey();


enum ParsingPhase
{
    Stacks,
    Labels,
    Moves
}

class StackMove
{
    public readonly int moveCount;
    public readonly int sourceStack;
    public readonly int destinationStack;

    public StackMove(string line)
    {
        string[] splitLine = line.Split(' ');

        moveCount = int.Parse(splitLine[1]);
        sourceStack = int.Parse(splitLine[3]) - 1;
        destinationStack = int.Parse(splitLine[5]) - 1;
    }

    public void DoItNerd(List<Stack<char>> stacks)
    {
        for (int i = 0; i < moveCount; i++)
        {
            stacks[destinationStack].Push(stacks[sourceStack].Pop());
        }
    }

    public void DoItAgainNerd(List<Stack<char>> stacks)
    {
        List<char> tempStack = new List<char>();
        for (int i = 0; i < moveCount; i++)
        {
            tempStack.Add(stacks[sourceStack].Pop());
        }

        for (int i = tempStack.Count - 1; i >= 0; i--)
        {
            stacks[destinationStack].Push(tempStack[i]);
        }
    }
}