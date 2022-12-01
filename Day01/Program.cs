const string inputFile = @"../../../../input01.txt";

Console.WriteLine("Day 01 - Calorie Counting");
Console.WriteLine("Star 1");
Console.WriteLine();

//string[] lines = File.ReadAllLines(inputFile);
IEnumerable<int> elfCalorieTotal = File.ReadAllText(inputFile)
    .Split("\r\n\r\n")
    .Select(x => x.Split("\r\n").Select(int.Parse).Sum())
    .ToList();

Console.WriteLine($"The answer is: {elfCalorieTotal.Max()}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Console.WriteLine($"The answer is: {elfCalorieTotal.OrderByDescending(x=>x).Take(3).Sum()}");

Console.WriteLine();
Console.ReadKey();
