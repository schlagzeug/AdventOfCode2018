<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day4_Input.txt")).ToList();
	var guardList = GetData(inputs);
	
	Part1(guardList).Dump("Part 1 Answer");
	Part2(guardList).Dump("Part 2 Answer");
}

public GuardList GetData(List<string> inputs)
{
	inputs.Sort();
	
	var guardList = new GuardList();
	var currentGuardID = -1;
	var minuteFellAsleep = -1;
	
	foreach (var input in inputs)
	{
		if (input.Contains("begins shift"))
		{
			var stringNumber = input.Substring(input.IndexOf('#') + 1);
			stringNumber = stringNumber.Replace(" begins shift", string.Empty);
			currentGuardID = int.Parse(stringNumber);
		}
		else if (input.Contains("falls asleep"))
		{
			minuteFellAsleep = int.Parse(input.Substring(15, 2));
		}
		else if (input.Contains("wakes up"))
		{
			var minuteWokeUp = int.Parse(input.Substring(15, 2));
			var newGuard = new Guard();
			newGuard.ID = currentGuardID;
			for (int i = minuteFellAsleep; i < minuteWokeUp; i++)
			{
				newGuard.DaysAsleepByMinute.Add(i, 1);
			}
			guardList.AddGuard(newGuard);
		}
	}
	return guardList;
}

public int Part1(GuardList guardList)
{
	int guardId = -1;
	int maxTimeAsleep = -1;
	foreach (var guard in guardList.List)
	{
		int totalMinAsleep = guard.TotalMinutesAsleep;
		if (totalMinAsleep > maxTimeAsleep)
		{
			guardId = guard.ID;
			maxTimeAsleep = totalMinAsleep;
		}
	}

	var sleepyGuard = guardList.FindGuard(guardId);
	int minute = -1;
	int maxDaysAsleep = -1;
	foreach (var minuteDays in sleepyGuard.DaysAsleepByMinute)
	{
		if (minuteDays.Value > maxDaysAsleep)
		{
			minute = minuteDays.Key;
			maxDaysAsleep = minuteDays.Value;
		}
	}
	
	// return driverID * minuteWithMostsleep
	return (guardId * minute);
}

public int Part2(GuardList guardList)
{
	int guardId = -1;
	int minute = -1;
	int maxDaysAsleep = -1;
	
	foreach (var guard in guardList.List)
	{
		foreach (var minuteDays in guard.DaysAsleepByMinute)
		{
			if (minuteDays.Value > maxDaysAsleep)
			{
				minute = minuteDays.Key;
				maxDaysAsleep = minuteDays.Value;
				guardId = guard.ID;
			}
		}
	}
	
	// return driverID * minuteWithMostsleep
	return (guardId * minute);
}

public class GuardList
{
	public List<Guard> List = new List<Guard>();
	
	public Guard FindGuard(int ID)
	{
		foreach (var guard in List)
		{
			if (guard.ID == ID) return guard;
		}
		return null;
	}
	
	public void AddGuard(Guard newGuard)
	{
		foreach (var guard in List)
		{
			if (guard.ID == newGuard.ID)
			{
				// found the guard
				foreach (var minute in newGuard.DaysAsleepByMinute)
				{
					if (guard.DaysAsleepByMinute.ContainsKey(minute.Key))
					{
						guard.DaysAsleepByMinute[minute.Key] += minute.Value;
					}
					else
					{
						guard.DaysAsleepByMinute.Add(minute.Key, minute.Value);
					}
				}
				
				return;
			}
		}
		
		// didn't find him
		List.Add(newGuard);
	}
}
public class Guard
{
	public int ID;
	public Dictionary<int, int> DaysAsleepByMinute = new Dictionary<int, int>();
	public int TotalMinutesAsleep
	{
		get
		{
			var total = 0;
			foreach (var hour in DaysAsleepByMinute)
			{
				total += (hour.Key * hour.Value);
			}
			return total;
		}
	}
}