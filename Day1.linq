<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day1_Input.txt")).ToList();
	
	Part1(inputs).Dump("Part 1 Answer");
	Part2(inputs).Dump("Part 2 Answer");
}

public int Part1(List<string> inputs)
{
	var frequency = 0;

	foreach (var input in inputs)
	{
		int x = int.Parse(input);
		frequency += x;
	}

	return frequency;
}

public int Part2(List<string> inputs)
{
	return RunList(inputs, new HashSet<int>(), 0);
}

private int RunList(List<string> inputs, HashSet<int> frequencies, int frequency)
{
	foreach (var input in inputs)
	{
		if (frequencies.Contains(frequency))
		{
			return frequency;
		}
		else
		{
			frequencies.Add(frequency);
		}

		int x = int.Parse(input);
		frequency += x;
	}
	
	return RunList(inputs, frequencies, frequency);
}