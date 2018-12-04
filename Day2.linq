<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day2_Input.txt")).ToList();
	
	Part1(inputs).Dump("Part 1 Answer");
	Part2(inputs).Dump("Part 2 Answer");
}

public int Part1(List<string> inputs)
{
	var twos = 0;
	var threes = 0;

	foreach (var input in inputs)
	{
		var has2 = false;
		var has3 = false;

		CheckInput(input, out has2, out has3);

		if (has2) twos++;
		if (has3) threes++;
	}

	return (twos * threes);
}

private void CheckInput(string input, out bool has2, out bool has3)
{
	var chars = input.ToCharArray();
	var letterCounts = new Dictionary<char, int>();
	has2 = false;
	has3 = false;
	
	foreach (var letter in chars)
	{
		if (letterCounts.ContainsKey(letter))
		{
			letterCounts[letter]++;
		}
		else
		{
			letterCounts.Add(letter, 1);
		}
	}
	
	foreach (var letterCount in letterCounts)
	{
		if (letterCount.Value == 2)
		{
			has2 = true;
		}

		if (letterCount.Value == 3)
		{
			has3 = true;
		}
	}
}

public string Part2(List<string> inputs)
{
	for (int i = 0; i < inputs.Count; i++)
	{
		for (int j = i + 1; j < inputs.Count; j++)
		{
			if (Compare(inputs[i], inputs[j]))
			{
				return FindCommon(inputs[i], inputs[j]);
			}
		}
	}
	return string.Empty;
}

private bool Compare(string string1, string string2)
{
	var letters1 = string1.ToList();
	var letters2 = string2.ToList();
	var diffCount = 0;
	
	for (int i = 0; i < letters1.Count; i++)
	{
		if (letters1[i] != letters2[i])
		{
			diffCount++;
			if (diffCount > 1) return false;
		}
	}
	
	return true;
}

private string FindCommon(string string1, string string2)
{
	var letters1 = string1.ToList();
	var letters2 = string2.ToList();
	var output = string.Empty;

	for (int i = 0; i < letters1.Count; i++)
	{
		if (letters1[i] == letters2[i])
		{
			output += letters1[i];
		}
	}
	return output;
}