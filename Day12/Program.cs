using AoCTools;

const string inputFile = @"../../../../input12.txt";

Console.WriteLine("Day 12 - Hill Climbing Algorithm");
Console.WriteLine("Star 1");
Console.WriteLine();

//string line = File.ReadAllText(inputFile);
string[] lines = File.ReadAllLines(inputFile);


Point2D min = (0, 0);
Point2D max = (lines[0].Length - 1, lines.Length - 1);

Point2D start = Point2D.Zero;
Point2D end = Point2D.Zero;

for (int y = 0; y < lines.Length; y++)
{
    bool found = false;
    for (int x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] == 'S')
        {
            start = (x, y);
            found = true;
        }
        else if (lines[y][x] == 'E')
        {
            end = (x, y);
            found = true;
        }
    }

    if (found)
    {
        lines[y] = lines[y].Replace('S', 'a').Replace('E', 'z');
    }
}

Dictionary<Point2D, int> heightMap = lines
    .Select(x => x.Select(x => (int)(x - 'a')))
    .ToDictionaryGrid();

Queue<Point2D> searchQueue = new Queue<Point2D>();
Dictionary<Point2D, int> gridDistance = new Dictionary<Point2D, int>();

searchQueue.Enqueue(start);
gridDistance[start] = 0;


while (searchQueue.Count > 0)
{
    Point2D nextPoint = searchQueue.Dequeue();

    int maxHeight = heightMap[nextPoint] + 1;
    int distance = gridDistance[nextPoint] + 1;

    if (gridDistance.ContainsKey(end) && gridDistance[end] <= distance)
    {
        continue;
    }

    foreach (Point2D neighbor in nextPoint.GetAdjacent())
    {
        if (neighbor.x < min.x || neighbor.y < min.y || neighbor.x > max.x || neighbor.y > max.y)
        {
            //out of bounds
            continue;
        }

        if (heightMap[neighbor] > maxHeight)
        {
            //too high
            continue;
        }

        if (gridDistance.ContainsKey(neighbor) && gridDistance[neighbor] <= distance)
        {
            //too far
            continue;
        }

        gridDistance[neighbor] = distance;

        searchQueue.Enqueue(neighbor);
    }
}


Console.WriteLine($"The answer is: {gridDistance[end]}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();


searchQueue.Clear();
gridDistance.Clear();

searchQueue.Enqueue(end);
gridDistance[end] = 0;


while (searchQueue.Count > 0)
{
    Point2D nextPoint = searchQueue.Dequeue();

    int minHeight = heightMap[nextPoint] - 1;
    int distance = gridDistance[nextPoint] + 1;

    foreach (Point2D neighbor in nextPoint.GetAdjacent())
    {
        if (neighbor.x < min.x || neighbor.y < min.y || neighbor.x > max.x || neighbor.y > max.y)
        {
            //out of bounds
            continue;
        }

        if (heightMap[neighbor] < minHeight)
        {
            //too high
            continue;
        }

        if (gridDistance.ContainsKey(neighbor) && gridDistance[neighbor] <= distance)
        {
            //too far
            continue;
        }

        gridDistance[neighbor] = distance;

        searchQueue.Enqueue(neighbor);
    }
}

Console.WriteLine($"The answer is: {gridDistance.Where(x => heightMap[x.Key] == 0).Min(x => x.Value)}");

Console.WriteLine();
Console.ReadKey();
