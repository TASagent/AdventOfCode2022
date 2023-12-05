using AoCTools;
using System.Text.RegularExpressions;

const string inputFile = @"../../../../input15.txt";

Console.WriteLine("Day 15 - Beacon Exclusion Zone");
Console.WriteLine("Star 1");
Console.WriteLine();

const int targetY = 10;

string[] lines = File.ReadAllLines(inputFile);

HashSet<LongPoint2D> sensors = new();
Dictionary<LongPoint2D, LongPoint2D> closestBeacons = new();

Regex captureCoordinates = new Regex(@"x=([-0-9]+), y=([-0-9]+)");

foreach (string line in lines)
{
    string[] splitLine = line.Split(':');

    LongPoint2D sensorLocation = new LongPoint2D(
        x: long.Parse(captureCoordinates.Match(splitLine[0]).Groups[1].Value),
        y: long.Parse(captureCoordinates.Match(splitLine[0]).Groups[2].Value));

    LongPoint2D beaconLocation = new LongPoint2D(
        x: long.Parse(captureCoordinates.Match(splitLine[1]).Groups[1].Value),
        y: long.Parse(captureCoordinates.Match(splitLine[1]).Groups[2].Value));

    sensors.Add(sensorLocation);

    closestBeacons.Add(sensorLocation, beaconLocation);
}

HashSet<LongPoint2D> excludedBeaconPositions = new();

foreach (LongPoint2D sensor in sensors)
{
    long totalDistance = (sensor - closestBeacons[sensor]).TaxiCabLength;

    long deltaY = Math.Abs(targetY - sensor.y);

    if (deltaY > totalDistance)
        continue;

    excludedBeaconPositions.Add((sensor.x, targetY));

    for (int i = 1; i <= (totalDistance - deltaY); i++)
    {
        excludedBeaconPositions.Add((sensor.x + i, targetY));
        excludedBeaconPositions.Add((sensor.x - i, targetY));
    }
}



int value = excludedBeaconPositions.Except(closestBeacons.Values).Count();

Console.WriteLine($"The answer is: {value}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

int value2 = 0;

Console.WriteLine($"The answer is: {value2}");

Console.WriteLine();
Console.ReadKey();
