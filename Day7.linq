<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day7_Input.txt")).ToList();
	
	var steps1 = new Steps(inputs);
	Part1(steps1).Dump("Part 1 Answer");
	var steps2 = new Steps(inputs);
	Part2(steps2).Dump("Part 2 Answer");
}

string Part1(Steps steps)
{
	var tempSteps = new Steps(steps);
	var charList = new List<char>();
	
	var x = tempSteps.RunNextStep();
	while (x != ' ')
	{
		charList.Add(x);
		x = tempSteps.RunNextStep();
	}
	
	return new string(charList.ToArray());
}

int Part2(Steps steps)
{
	var tempSteps = new Steps(steps);
	var elapsedTime = 0;
	var workers = new Workers(5, steps);
	while (true)
	{
		if (workers.AllDone()) break;
		
		workers.AssignWork();
		workers.Work();
		
		elapsedTime++;
	}
	
	return elapsedTime;
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
	
	public char RunNextStep()
	{
		var availableStepList = GetAvailableSteps();
		if (availableStepList.Count != 0)
		{
			var step = availableStepList[0];
			step.Finished = true;
			
			foreach (var s in StepList)
			{
				s.PrevSteps.Remove(step.StepName);
			}
			return step.StepName;
		}
		else
		{
			return ' ';
		}
	}
	
	public List<Step> GetAvailableSteps()
	{
		//var charList = new List<char>();
		var availableSteps = new List<Step>();
		foreach (var step in StepList)
		{
			if (!step.Finished && step.PrevSteps.Count == 0)
			{
				//charList.Add(step.StepName);
				availableSteps.Add(step);
			}
		}

		//charList.Sort();
		return availableSteps.OrderBy(x => x.StepName).ToList();
		//return charList;
	}
}

public class Step
{
	public char StepName;
	public List<char> PrevSteps = new List<char>();
	public bool Working = false;
	public bool Finished = false;
	public int WorkTime;
	
	public Step(char stepName)
	{
		StepName = stepName;
		SetInitialWorktime();
	}

	private void SetInitialWorktime()
	{
		WorkTime = (StepName - 'A') + 61;
	}
}

public class Workers
{
	public List<Worker> WorkerList = new List<Worker>();
	public Steps Steps;
	
	public Workers (int numWorkers, Steps steps)
	{
		for (int i = 0; i < numWorkers; i++)
		{
			WorkerList.Add(new Worker());
		}
		
		Steps = steps;
	}
	
	public bool AllDone()
	{
		var availableSteps = Steps.GetAvailableSteps();
		if (availableSteps.Count == 0) return true;
		return false;
	}
	
	public void Work()
	{
		foreach (var worker in WorkerList)
		{
			if (worker.Work())
			{
				foreach (var s in Steps.StepList)
				{
					s.PrevSteps.Remove(worker.CurrentStep.StepName);
				}
				worker.CurrentStep = null;
			}
		}		
	}
	
	public void AssignWork()
	{
		var availableWork = Steps.GetAvailableSteps();
		foreach (var work in availableWork)
		{
			if (work.Working) continue;
			
			foreach (var worker in WorkerList)
			{
				if (worker.CurrentStep == null)
				{
					worker.CurrentStep = work;
					worker.CurrentStep.Working = true;
					break;
				}
			}
		}
	}
}

public class Worker
{
	public Step CurrentStep;
	
	public bool Work()
	{
		if (CurrentStep != null)
		{
			CurrentStep.WorkTime--;

			if (CurrentStep.WorkTime == 0)
			{
				CurrentStep.Finished = true;
				return true;
			}
		}
		return false;
	}
}