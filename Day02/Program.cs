const string inputFile = @"../../../../input02.txt";

Console.WriteLine("Day 02 - Rock Paper Scissors");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

int totalPoints = lines.Select(TranslateGuide1).Select(x => GetPoints(x.elf, x.me)).Sum();

Console.WriteLine($"The answer is: {totalPoints}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int totalPoints2 = lines.Select(TranslateGuide2).Select(x => GetPoints(x.elf, x.me)).Sum();

Console.WriteLine($"The answer is: {totalPoints2}");

Console.WriteLine();
Console.ReadKey();

static (RPS elf, RPS me) TranslateGuide1(string line)
{
    RPS elfStrat = line[0] switch
    {
        'A' => RPS.Rock,
        'B' => RPS.Paper,
        'C' => RPS.Scissors,
        _ => throw new Exception()
    };

    RPS myStrat = line[2] switch
    {
        'X' => RPS.Rock,
        'Y' => RPS.Paper,
        'Z' => RPS.Scissors,
        _ => throw new Exception()
    };

    return (elfStrat, myStrat);
}

static (RPS elf, RPS me) TranslateGuide2(string line)
{
    RPS elfStrat = line[0] switch
    {
        'A' => RPS.Rock,
        'B' => RPS.Paper,
        'C' => RPS.Scissors,
        _ => throw new Exception()
    };

    RPS myStrat = line[2] switch
    {
        'X' => (RPS)((int)(elfStrat + (int)RPS.MAX - 1)%(int)RPS.MAX),
        'Y' => elfStrat,
        'Z' => (RPS)((int)(elfStrat + 1) % (int)RPS.MAX),
        _ => throw new Exception()
    };

    return (elfStrat, myStrat);
}

static int GetPoints(RPS elfStrat, RPS myStrat)
{
    int rawPoints = (int)myStrat + 1;

    if (elfStrat == myStrat) return rawPoints + 3;
    if (myStrat == (RPS)((int)(elfStrat + 1) % (int)RPS.MAX)) return rawPoints + 6;
    return rawPoints;
}



enum RPS
{
    Rock = 0,
    Paper,
    Scissors,
    MAX
}