<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day6_Input.txt")).ToList();
	var map = new Map(inputs);
	
	Part1(map).Dump("Part 1 Answer");
	Part2(map).Dump("Part 2 Answer");
}

public int Part1(Map map)
{
	var biggestArea = -1;
	var countsByIndex = new Dictionary<int, int>();
	var excluded = new List<int>();
	
	foreach (var point in map.MapPoints)
	{
		if (excluded.Contains(point.Owner) || point.OnEdge)
		{
			if (!excluded.Contains(point.Owner))
			{
				excluded.Add(point.Owner);
			}
			continue;
		}
		
		if (countsByIndex.ContainsKey(point.Owner))
		{
			countsByIndex[point.Owner]++;
		}
		else
		{
			countsByIndex.Add(point.Owner, 1);
		}
	}
	
	foreach (var count in countsByIndex)
	{
		if (excluded.Contains(count.Key))
		{
			continue;
		}
		
		if (count.Value > biggestArea)
		{
			biggestArea = count.Value;
		}
	}
	
	return biggestArea;
}

public int Part2(Map map)
{
	var safeArea = 0;
	
	foreach (var point in map.MapPoints)
	{
		int totalDistance = 0;
		foreach (var input in map.inputs)
		{
			totalDistance += map.ManhattanDistance(input, point);
		}
		
		if (totalDistance < 10000) safeArea++;
	}
	
	return safeArea;
}

public class Map
{
	public Coordinate upperBound = new Coordinate(-1, -1);
	public List<Coordinate> inputs = new List<Coordinate>();
	public List<Coordinate> MapPoints = new List<Coordinate>();
	
	public Map(List<string> rawCoordinates)
	{
		// set up Inputs
		foreach (var raw in rawCoordinates)
		{
			var split = raw.Split(',');
			var x = int.Parse(split[0]);
			var y = int.Parse(split[1]);
			var coordinate = new Coordinate(x, y);
			inputs.Add(coordinate);
			
			if (x > upperBound.X)
			{
				upperBound.X = x;
			}
			
			if (y > upperBound.Y)
			{
				upperBound.Y = y;
			}
		}
		
		// set up MapPoints
		for (int x = 0; x <= upperBound.X; x++)
		{
			for (int y = 0; y <= upperBound.Y; y++)
			{
				var xy = new Coordinate(x, y);
				if (x == 0 || y == 0 || x == upperBound.X || y == upperBound.Y)
				{
					xy.OnEdge = true;
				}
				
				int distance = ManhattanDistance(new Coordinate(0, 0), upperBound);
				for (int i = 0; i < inputs.Count; i++)
				{
					var tempDistance = ManhattanDistance(inputs[i], xy);
					if (tempDistance < distance)
					{
						xy.Owner = i;
						distance = tempDistance;
					}
					else if (tempDistance == distance)
					{
						xy.Owner = -1;
					}
				}
				
				MapPoints.Add(xy);
			}
		}
	}

	public int ManhattanDistance(Coordinate c1, Coordinate c2)
	{
		return (Math.Abs(c1.X - c2.X) + Math.Abs(c1.Y - c2.Y));
	}
}

public class Coordinate
{
	public int X;
	public int Y;
	public int Owner = -1;
	public bool OnEdge = false;

	public Coordinate(int x, int y)
	{
		X = x;
		Y = y;
	}

	public override string ToString()
	{
		return $"{X},{Y}";
	}
}