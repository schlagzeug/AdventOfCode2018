<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day3_Input.txt")).ToList();
	var claimList = GetClaims(inputs);
	
	Part1(claimList).Dump("Part 1 Answer");
	Part2(claimList).Dump("Part 2 Answer");
}

public List<Claim> GetClaims(List<string> list)
{
	var claimList = new List<Claim>();
	
	foreach (var entry in list)
	{
		var x = new Claim(entry);
		claimList.Add(x);
	}
	
	return claimList;
}

public int Part1(List<Claim> claims)
{
	var coordinateCounts = new Dictionary<string, int>();
	foreach (var claim in claims)
	{
		foreach (var coordinate in claim.CoordinateList)
		{
			if (coordinateCounts.ContainsKey(coordinate))
			{
				coordinateCounts[coordinate]++;
			}
			else
			{
				coordinateCounts.Add(coordinate, 1);
			}
		}
	}
	
	var count = 0;
	foreach (var coordinateCount in coordinateCounts)
	{
		if (coordinateCount.Value > 1) count++;
	}

	return count;
}

public int Part2(List<Claim> claims)
{
	var claimOwnersByCoordinate = new Dictionary<string, List<int>>();
	foreach (var claim in claims)
	{
		foreach (var coordinate in claim.CoordinateList)
		{
			if (claimOwnersByCoordinate.ContainsKey(coordinate))
			{
				claimOwnersByCoordinate[coordinate].Add(claim.ID);
			}
			else
			{
				claimOwnersByCoordinate.Add(coordinate, new List<int>());
				claimOwnersByCoordinate[coordinate].Add(claim.ID);
			}
		}
	}
	
	var list = new HashSet<int>();
	foreach (var element in claimOwnersByCoordinate)
	{
		if (element.Value.Count == 1)
		{
			foreach (var ID in element.Value)
			{
				if (!list.Contains(ID)) list.Add(ID);
			}
		}
	}
	
	foreach (var element in claimOwnersByCoordinate)
	{
		if (element.Value.Count > 1)
		{
			foreach (var value in element.Value)
			{
				if (list.Contains(value)) list.Remove(value);
			}
		}
	}
	
	if (list.Count > 1)
	{
		list.Dump("Error - List");
		return -1;
	}
	else
	{
		return list.First();	
	}
}

public class Claim
{
	public int ID { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }
	
	public HashSet<int> OverlappedClaims = new HashSet<int>();

	public List<string> CoordinateList
	{
		get
		{
			var retVal = new List<string>();
			for (int i = X; i < X + Width; i++)
			{
				for (int j = Y; j < Y + Height; j++)
				{
					retVal.Add($"{i},{j}");
				}
			}
			return retVal;
		}
	}

	public Claim(string claim)
	{
		var list = claim.Split(' ');
		ID = int.Parse(list[0].Replace('#',' '));
		
		var xy = list[2].Split(',');
		X = int.Parse(xy[0]);
		Y = int.Parse(xy[1].Replace(':',' '));
		
		var wh = list[3].Split('x');
		Width = int.Parse(wh[0]);
		Height = int.Parse(wh[1]);
	}
}
