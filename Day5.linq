<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllText(Path.Combine(folder, "Day5_Input.txt")).Trim().ToList();

	Part1(input).Dump("Part 1 Answer");
	Part2(input).Dump("Part 2 Answer");
}

int Part1(List<char> input)
{
	return RunString(input).Count;
}

int Part2(List<char> input) //4944
{
	var charDictionary = new Dictionary<char, int>();

	for (int i = 0; i < input.Count; i++)
	{
		var tempChar = input[i];
		if (!charDictionary.ContainsKey(tempChar))
		{
			var tempInput = new List<char>(input);
			for (int j = 0; j < tempInput.Count; j++)
			{
				if (tempInput[j].ToString().ToUpper() == tempChar.ToString().ToUpper())
				{
					tempInput.RemoveAt(j);
					j--;
				}
			}
			
			var count = RunString(tempInput).Count;
			charDictionary.Add(tempChar, count);
		}
	}
	
	int lowest = charDictionary.First().Value;
	foreach (var entry in charDictionary)
	{
		if (entry.Value < lowest) lowest = entry.Value;
	}
	
	return lowest;
}

List<char> RunString(List<char> input)
{
	bool runAgain = false;
	for (int i = 0; i < input.Count - 1; i++)
	{
		var a = input[i].ToString();
		var b = input[i + 1].ToString();
		
		if ((a.ToUpper() == b && b.ToLower() == a) || (b.ToUpper() == a && a.ToLower() == b))
		{
			input.RemoveAt(i + 1);
			input.RemoveAt(i);
			i--;
			runAgain = true;
		}
	}
	
	if (runAgain) return RunString(input);
	
	return input;
}