<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllLines(Path.Combine(folder, "Day12_Input.txt")).ToList();

	Part1().Dump("Part 1 Answer");
	Part2().Dump("Part 2 Answer");
}

string Part1()
{
	return string.Empty;
}

string Part2()
{
	return string.Empty;
}

void Alternate()
{
	HashSet<int> currentPlants = new HashSet<int>();
	Dictionary<int, bool> plantRules = new Dictionary<int, bool>();
	StreamReader file = new StreamReader(@"C:\Work\LINQPad\AdventOfCode2018\Day12_Input.txt");

	string line = file.ReadLine();
	line.Skip(15).Select((x, i) => new { x, i }).Where(c => c.x == '#').Select(c => c.i).ToList().ForEach(x => currentPlants.Add(x));
	line = file.ReadLine();
	while (!file.EndOfStream)
	{
		line = file.ReadLine();
		int binary = line.Take(5).Select((x, i) => new { x, i }).Where(c => c.x == '#').Sum(c => (int)Math.Pow(2, c.i));
		plantRules.Add(binary, line[9] == '#' ? true : false);
	}

	long iterations = 50000000000;
	long totalSum = 0;
	HashSet<int> newPlants = new HashSet<int>();

	for (int iter = 1; iter <= iterations; iter++)
	{
		newPlants = new HashSet<int>();
		int min = currentPlants.Min() - 3;
		int max = currentPlants.Max() + 3;

		for (int pot = min; pot <= max; pot++)
		{
			int sum = 0;
			for (int i = 0; i < 5; i++)
			{
				if (currentPlants.Contains(pot + i - 2)) sum += (int)Math.Pow(2, i);
			}
			if (plantRules[sum]) newPlants.Add(pot);
		}
		// the simulation converged to a stable point


		currentPlants = newPlants;
		totalSum = currentPlants.Sum();
	}

	//Console.WriteLine(totalSum);
	currentPlants.Sum().Dump();


}


public class StateManager
{
	List<char> State;
	List<SpreadLogic> SpreadLogics;
}

public class SpreadLogic
{
	List<char> Pattern;
	char Outcome
}