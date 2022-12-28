using System.Numerics;
using System.Text.RegularExpressions;

const string inputFile = @"../../../../input11.txt";

Console.WriteLine("Day 11 - Monkey in the Middle");
Console.WriteLine("Star 1");
Console.WriteLine();

//After each monkey inspects an item but before it tests your worry level,
//your relief that the monkey's inspection didn't damage the item causes
//your worry level to be divided by three and rounded down to the nearest integer.

const int TOTAL_ROUND_COUNT = 20;

string lines = File.ReadAllText(inputFile);

Regex monkeyParser = new Regex(
@"Monkey (\d+)\:
  Starting items: ([0-9, ]+)
  Operation: ([a-z*+\/\-0-9 =]+)
  Test: divisible by ([0-9]+)
    If true: throw to monkey ([0-9]+)
    If false: throw to monkey ([0-9]+)");

List<Monkey> monkeys = monkeyParser
    .Matches(lines)
    .Select(x => new Monkey(x, 0))
    .ToList();

Dictionary<int, Monkey> monkeyLookup = monkeys.ToDictionary(x => x.Number, x => x);

long product = monkeys.Aggregate(1L, (value, monkey) => value * monkey.testDivisor);

for (int i = 0; i < TOTAL_ROUND_COUNT; i++)
{
    foreach (Monkey monkey in monkeys)
    {
        monkey.HandleItems(monkeyLookup, product);
    }
}

long value1 = monkeys
    .OrderByDescending(x => x.InspectionCount)
    .Take(2)
    .Aggregate(1L, (value, monkey) => value * monkey.InspectionCount);

Console.WriteLine($"The answer is: {value1}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

List<Monkey> newMonkeys = monkeyParser
    .Matches(lines)
    .Select(x => new Monkey(x, 1))
    .ToList();

Dictionary<int, Monkey> newMonkeyLookup = newMonkeys.ToDictionary(x => x.Number, x => x);


long newProduct = newMonkeys.Aggregate(1L, (value, monkey) => value * monkey.testDivisor);

const int NEW_TOTAL_ROUND_COUNT = 10_000;


for (int i = 0; i < NEW_TOTAL_ROUND_COUNT; i++)
{
    foreach (Monkey monkey in newMonkeys)
    {
        monkey.HandleItems(newMonkeyLookup, newProduct);
    }
}

long value2 = newMonkeys
    .OrderByDescending(x => x.InspectionCount)
    .Take(2)
    .Aggregate(1L, (value, monkey) => value * monkey.InspectionCount);

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

class Monkey
{
    public int Number { get; init; }
    public List<long> Items { get; } = new List<long>();

    private readonly OperationType operation;
    private readonly int operationValue = 0;

    public readonly int testDivisor;
    private readonly int trueMonkey;
    private readonly int falseMonkey;

    public long InspectionCount { get; private set; } = 0;

    private readonly int phase;

    public Monkey(Match match, int phase)
    {
        this.phase = phase;

        Number = int.Parse(match.Groups[1].Value);

        Items.AddRange(match.Groups[2].Value.Split(", ").Select(long.Parse));

        //old * old
        //old + X
        //old * X

        string operationText = match.Groups[3].Value[6..];

        if (operationText == "old * old")
        {
            operation = OperationType.Squared;
        }
        else if (operationText[4] == '+')
        {
            //Addition
            operation = OperationType.Addition;
            operationValue = int.Parse(operationText[6..]);
        }
        else
        {
            //Multiplication
            operation = OperationType.Multiplication;
            operationValue = int.Parse(operationText[6..]);
        }

        testDivisor = int.Parse(match.Groups[4].Value);
        trueMonkey = int.Parse(match.Groups[5].Value);
        falseMonkey = int.Parse(match.Groups[6].Value);
    }

    public void AddItem(long value) => Items.Add(value);

    public void HandleItems(Dictionary<int, Monkey> monkeyLookup, long product)
    {
        foreach (long item in Items.ToArray())
        {
            //Track the inspection
            InspectionCount++;

            long newValue;

            //Handle the item
            switch (operation)
            {
                case OperationType.Squared:
                    newValue = item * item;
                    break;

                case OperationType.Multiplication:
                    newValue = item * operationValue;
                    break;

                case OperationType.Addition:
                    newValue = item + operationValue;
                    break;

                default: throw new Exception();
            }


            if (phase == 0)
            {
                newValue /= 3;
            }

            newValue %= product;

            if (newValue % testDivisor == 0)
            {
                //Give to trueMonkey
                monkeyLookup[trueMonkey].AddItem(newValue);
            }
            else
            {
                //Give to falseMonkey
                monkeyLookup[falseMonkey].AddItem(newValue);
            }
        }

        Items.Clear();
    }


    private enum OperationType
    {
        Squared,
        Multiplication,
        Addition
    }
}
