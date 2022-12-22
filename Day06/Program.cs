const string inputFile = @"../../../../input06.txt";

Console.WriteLine("Day 06 - Tuning Trouble");
Console.WriteLine("Star 1");
Console.WriteLine();

string line = File.ReadAllText(inputFile);

int value = GetFirstStarPosition(line) + 1;

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int value2 = GetSecondStarPosition(line) + 1;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

int GetFirstStarPosition(string input)
{
    for (int i = 0; i < input.Length - 4; i++)
    {
        if (
            input[i] != input[i + 1] &&
            input[i] != input[i + 2] &&
            input[i] != input[i + 3] &&
            input[i + 1] != input[i + 2] &&
            input[i + 1] != input[i + 3] &&
            input[i + 2] != input[i + 3])
        {
            return i + 3;
        }
    }

    throw new Exception();
}


int GetSecondStarPosition(string input)
{
    Dictionary<char, int> charCount = new Dictionary<char, int>();

    for (int i = 0; i < 13; i++)
    {
        charCount[input[i]] = charCount.GetValueOrDefault(input[i]) + 1;
    }


    for (int i = 0; i < input.Length - 14; i++)
    {
        charCount[input[i + 13]] = charCount.GetValueOrDefault(input[i + 13]) + 1;

        if (!charCount.Values.Any(x => x > 1))
        {
            return i + 13;
        }

        charCount[input[i]] = charCount[input[i]] - 1;
    }

    throw new Exception();
}