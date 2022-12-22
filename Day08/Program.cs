using AoCTools;

const string inputFile = @"../../../../input08.txt";
//const string inputFile = @"../../../../input08test.txt";

Console.WriteLine("Day 08 - Treetop Tree House");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] inputLines = File.ReadAllLines(inputFile);

Dictionary<Point2D, char> treeHeightTemp = inputLines
    .ToDictionaryGrid();

//Dictionary<Point2D, int> forest = new Dictionary<Point2D, int>();

Dictionary<Point2D, int> treeHeight = treeHeightTemp
    .Select(x => (x.Key, int.Parse(x.Value.ToString())))
    .ToDictionary(x => x.Key, x => x.Item2);

HashSet<Point2D> visibleTrees = new HashSet<Point2D>();

for (int x = 0; x < inputLines[0].Length; x++)
{
    //Y-tests

    //Test Top
    visibleTrees.Add((x, 0));
    int maxHeight = treeHeight[(x, 0)];
    Point2D testValue = (x, 1);

    while (maxHeight < 9 && testValue.y < inputLines.Length)
    {
        if (treeHeight[testValue] > maxHeight)
        {
            maxHeight = treeHeight[testValue];
            visibleTrees.Add(testValue);
        }

        testValue += Point2D.YAxis;
    }

    //Test Bottom
    visibleTrees.Add((x, inputLines.Length - 1));
    testValue = (x, inputLines.Length - 2);
    maxHeight = treeHeight[(x, inputLines.Length - 1)];

    while (maxHeight < 9 && testValue.y >= 0)
    {
        if (treeHeight[testValue] > maxHeight)
        {
            maxHeight = treeHeight[testValue];
            visibleTrees.Add(testValue);
        }

        testValue -= Point2D.YAxis;
    }
}

for (int y = 0; y < inputLines.Length; y++)
{
    //X-tests

    //Test Left
    visibleTrees.Add((0, y));
    Point2D testValue = (1, y);
    int maxHeight = treeHeight[(0, y)];

    while (maxHeight < 9 && testValue.x < inputLines[0].Length)
    {
        if (treeHeight[testValue] > maxHeight)
        {
            maxHeight = treeHeight[testValue];
            visibleTrees.Add(testValue);
        }
        testValue += Point2D.XAxis;
    }

    //Test Right
    visibleTrees.Add((inputLines[0].Length - 1, y));
    testValue = (inputLines[0].Length - 2, y);
    maxHeight = treeHeight[(inputLines[0].Length - 1, y)];

    while (maxHeight < 9 && testValue.x >= 0)
    {
        if (treeHeight[testValue] > maxHeight)
        {
            maxHeight = treeHeight[testValue];
            visibleTrees.Add(testValue);
        }

        testValue -= Point2D.XAxis;
    }
}

int value = visibleTrees.Count;

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Point2D max = (inputLines[0].Length, inputLines.Length);

int maxScore = 0;

for (int x = 1; x < max.x - 1; x++)
{
    for (int y = 1; y < max.y - 1; y++)
    {
        Point2D start = (x, y);
        int maxHeight = treeHeight[start];
        int score = 1;
        int currentDistance = 0;

        Point2D current = start + Point2D.XAxis;
        while (current.x < max.x)
        {
            currentDistance++;

            if (treeHeight[current] >= maxHeight)
            {
                break;
            }

            current += Point2D.XAxis;
        }

        score *= currentDistance;

        currentDistance = 0;
        current = start - Point2D.XAxis;
        while (current.x >= 0)
        {
            currentDistance++;

            if (treeHeight[current] >= maxHeight)
            {
                break;
            }

            current -= Point2D.XAxis;
        }


        score *= currentDistance;
        currentDistance = 0;

        current = start + Point2D.YAxis;
        while (current.y < max.y)
        {
            currentDistance++;

            if (treeHeight[current] >= maxHeight)
            {
                break;
            }

            current += Point2D.YAxis;
        }

        score *= currentDistance;

        currentDistance = 0;
        current = start - Point2D.YAxis;
        while (current.y >= 0)
        {
            currentDistance++;

            if (treeHeight[current] >= maxHeight)
            {
                break;
            }

            current -= Point2D.YAxis;
        }

        score *= currentDistance;

        maxScore = Math.Max(score, maxScore);
    }
}

Console.WriteLine($"The answer is: {maxScore}");

Console.WriteLine();
Console.ReadKey();
