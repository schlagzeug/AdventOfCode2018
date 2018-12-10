<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day7_Input.txt")).ToList();
	var steps = new Steps(inputs);

steps.Dump();
	Part1(steps).Dump("Part 1 Answer");
	//Part2(steps).Dump("Part 2 Answer");
}

string Part1(Steps steps)
{
	var tempSteps = new Steps(steps);
	var charList = new List<char>();
	
	var x = tempSteps.RunCurrentSteps();
	while (x.Count > 0)
	{
		charList.AddRange(x);
		charList.Add(' ');
		x = tempSteps.RunCurrentSteps();
	}
	
	return new string(charList.ToArray());
}

object Part2(Steps steps)
{
	throw new NotImplementedException();
}

public class Steps
{
	public List<Step> StepList = new List<Step>();

	public Steps(List<string> rawStepList)
	{
		foreach (var rawStep in rawStepList)
		{
			var stepA = FindStep(rawStep[5]);
			var stepB = FindStep(rawStep[36]);

			stepA.NextSteps.Add(stepB.StepName);
			stepB.PrevSteps.Add(stepA.StepName);
		}
	}

	public Steps(Steps steps)
	{
		StepList = steps.StepList;
	}

	public Step FindStep(char stepName)
	{
		foreach (var step in StepList)
		{
			if (step.StepName == stepName)
			{
				return step;
			}
		}

		var newStep = new Step(stepName);
		StepList.Add(newStep);
		return newStep;
	}

	public List<char> RunCurrentSteps()
	{
		var charList = new List<char>();
		foreach (var step in StepList)
		{
			if (!step.Finished && step.PrevSteps.Count == 0)
			{
				charList.Add(step.StepName);
				step.Finished = true;
			}
		}

		charList.Sort();
		if (charList.Count != 0)
		{
			foreach (var step in StepList)
			{
				foreach (var c in charList)
				{
					step.PrevSteps.Remove(c);
				}
			}
		}		
		return charList;
	}
}

public class Step
{
	public char StepName;
	public List<char> PrevSteps = new List<char>();
	public List<char> NextSteps = new List<char>();
	public bool Finished = false;
	
	public Step(char stepName)
	{
		StepName = stepName;
	}
}