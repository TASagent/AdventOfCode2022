using AoCTools;

const string inputFile = @"../../../../input09.txt";

Console.WriteLine("Day 09 - Rope Bridge");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);

IEnumerable<Point2D> steps = lines.SelectMany(TranslateString);

{
    Point2D head = (0, 0);
    Point2D tail = (0, 0);

    HashSet<Point2D> tailVisited = new HashSet<Point2D>() { tail };

    foreach (Point2D step in steps)
    {
        //Enact step
        head += step;

        if (Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1)
        {
            continue;
        }

        //We take a step

        Point2D stepDirection = (0, 0);

        if (head.x > tail.x)
        {
            stepDirection += Point2D.XAxis;
        }
        else if (head.x < tail.x)
        {
            stepDirection -= Point2D.XAxis;
        }

        if (head.y > tail.y)
        {
            stepDirection += Point2D.YAxis;
        }
        else if (head.y < tail.y)
        {
            stepDirection -= Point2D.YAxis;
        }

        tail += stepDirection;

        tailVisited.Add(tail);
    }

    Console.WriteLine($"The answer is: {tailVisited.Count}");
}

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

{
    List<Point2D> knots = new List<Point2D>()
    {
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0)
    };

    HashSet<Point2D> tailVisited = new HashSet<Point2D>() { (0, 0) };

    foreach (Point2D step in steps)
    {
        //Enact step
        knots[0] += step;

        for (int i = 1; i < knots.Count; i++)
        {
            if (Math.Abs(knots[i - 1].x - knots[i].x) <= 1 && Math.Abs(knots[i - 1].y - knots[i].y) <= 1)
            {
                continue;
            }

            //We take a step

            Point2D stepDirection = (0, 0);

            if (knots[i - 1].x > knots[i].x)
            {
                stepDirection += Point2D.XAxis;
            }
            else if (knots[i - 1].x < knots[i].x)
            {
                stepDirection -= Point2D.XAxis;
            }

            if (knots[i - 1].y > knots[i].y)
            {
                stepDirection += Point2D.YAxis;
            }
            else if (knots[i - 1].y < knots[i].y)
            {
                stepDirection -= Point2D.YAxis;
            }

            knots[i] += stepDirection;
        }

        tailVisited.Add(knots[^1]);
    }

    Console.WriteLine($"The answer is: {tailVisited.Count}");
}

Console.WriteLine();
Console.ReadKey();

IEnumerable<Point2D> TranslateString(string line)
{
    int steps = int.Parse(line[2..]);
    Point2D direction;
    switch (line[0])
    {
        case 'R':
            direction = Point2D.XAxis;
            break;

        case 'L':
            direction = -Point2D.XAxis;
            break;

        case 'U':
            direction = Point2D.YAxis;
            break;

        case 'D':
            direction = -Point2D.YAxis;
            break;

        default: throw new Exception();
    }

    for (int i = 0; i < steps; i++)
    {
        yield return direction;
    }
}
