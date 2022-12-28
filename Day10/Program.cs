const string inputFile = @"../../../../input10.txt";

Console.WriteLine("Day 10 - Cathode-Ray Tube");
Console.WriteLine("Star 1");
Console.WriteLine();

string[] lines = File.ReadAllLines(inputFile);
//string line = File.ReadAllText(inputFile);

// X starts at 1
//signal strength is cycle number times registerX

int cycleTest = 20;
int cycleNum = 0;

int TOTAL_CYCLE_COUNT = 221;

int registerX = 1;

List<long> signalStrengths = new List<long>();
int instr = 0;
bool wait = false;
int pendingAdd = 0;

while (++cycleNum < TOTAL_CYCLE_COUNT)
{
    if (cycleNum == cycleTest)
    {
        cycleTest += 40;

        signalStrengths.Add(cycleNum * registerX);
    }

    //Execute here

    if (wait)
    {
        wait = false;
        registerX += pendingAdd;
        pendingAdd = 0;
    }
    else if (lines[instr] == "noop")
    {
        instr++;
    }
    else
    {
        //Add Instr
        wait = true;

        pendingAdd = int.Parse(lines[instr][5..]);

        instr++;
    }
}

Console.WriteLine($"The answer is: {signalStrengths.Sum()}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

//the sprite is 3 pixels wide
//the X register sets the horizontal position of the middle of that sprite.
//40x6
//Draws left-to-right


//Reset VM
cycleNum = 0;
wait = false;
instr = 0;
registerX = 1;

Console.WriteLine($"The console output:");

TOTAL_CYCLE_COUNT = 241;

while (++cycleNum < TOTAL_CYCLE_COUNT)
{
    //Handle Drawing

    int column = (cycleNum - 1) % 40;

    if (column == 0)
    {
        Console.WriteLine();
    }

    if (Math.Abs(registerX - column) <= 1)
    {
        Console.Write("#");
    }
    else
    {
        Console.Write(" ");
    }

    //Execute Instr
    if (wait)
    {
        wait = false;
        registerX += pendingAdd;
        pendingAdd = 0;
    }
    else if (lines[instr] == "noop")
    {
        instr++;
    }
    else
    {
        //Add Instr
        wait = true;

        pendingAdd = int.Parse(lines[instr][5..]);

        instr++;
    }
}







Console.WriteLine();
Console.ReadKey();
