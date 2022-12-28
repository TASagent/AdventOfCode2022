using AoCTools;

const string inputFile = @"../../../../input14.txt";

Console.WriteLine("Day 14 - Regolith Reservoir");
Console.WriteLine("Star 1");
Console.WriteLine();

Dictionary<Point2D, State> cave = new Dictionary<Point2D, State>();

foreach (string line in File.ReadAllLines(inputFile))
{
    List<Point2D> points = line.Split(" -> ").Select(ParsePoint).ToList();

    cave[points[0]] = State.Rock;

    for (int i = 0; i < points.Count - 1; i++)
    {
        Point2D diff;

        if (points[i].x > points[i + 1].x)
        {
            diff = (-1, 0);
        }
        else if (points[i].x < points[i + 1].x)
        {
            diff = (1, 0);
        }
        else if (points[i].y > points[i + 1].y)
        {
            diff = (0, -1);
        }
        else
        {
            diff = (0, 1);
        }

        Point2D lastPoint = points[i];

        while (lastPoint != points[i + 1])
        {
            lastPoint += diff;
            cave[lastPoint] = State.Rock;
        }
    }
}

int maxY = cave.Keys.Max(x => x.y);


List<Point2D> potentialSteps = new List<Point2D>()
{
    (0, 1),
    (-1, 1),
    (1, 1)
};


while (HandleGrain())
{
    //Wheeeeee
}


int value = cave.Values.Count(x => x == State.Sand);

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();


int newFloor = maxY + 2;

for (int x = 500 - maxY - 5; x <= 500 + maxY + 5; x++)
{
    cave[(x, newFloor)] = State.Rock;
}


while (HandleGrain2())
{
    //Wheeeeee
}

int value2 = cave.Values.Count(x => x == State.Sand); ;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();

Point2D ParsePoint(string input)
{
    string[] splitLine = input.Split(',');
    return (int.Parse(splitLine[0]), int.Parse(splitLine[1]));
}

bool HandleGrain()
{
    Point2D grainCoordinate = (500, 0);

    while (true)
    {
        Point2D newPosition = HandleStep(grainCoordinate);

        if (newPosition == grainCoordinate)
        {
            cave[newPosition] = State.Sand;
            return true;
        }

        grainCoordinate = newPosition;

        if (grainCoordinate.y >= maxY)
        {
            //Completely done
            return false;
        }
    }
}

bool HandleGrain2()
{
    Point2D grainCoordinate = (500, 0);

    while (true)
    {
        Point2D newPosition = HandleStep(grainCoordinate);

        if (newPosition == grainCoordinate)
        {
            cave[newPosition] = State.Sand;
            return newPosition != (500, 0);
        }

        grainCoordinate = newPosition;
    }
}

Point2D HandleStep(Point2D grainCoordinate)
{
    for (int i = 0; i < potentialSteps.Count; i++)
    {
        if (!cave.ContainsKey(grainCoordinate + potentialSteps[i]))
        {
            return grainCoordinate + potentialSteps[i];
        }
    }

    return grainCoordinate;
}

enum State
{
    Rock,
    Sand
}
