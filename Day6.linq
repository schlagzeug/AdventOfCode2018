<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day6_Input.txt")).ToList();
	
	// I'll have to come back to this
}


class Map
{
	Coordinate upperBound = new Coordinate(-1, -1);
	List<Area> areas = new List<Area>();
	
	Dictionary<Coordinate, string> OwnersByCoordinates
	{
		get
		{
			var dict = new Dictionary<Coordinate, string>();
			foreach (var area in areas)
			{
				var center = area.center.ToString();
				foreach (var coordinate in area.coordinatesInArea)
				{
					if (dict.ContainsKey(coordinate))
					{
						dict[coordinate] = $"{dict[coordinate]}:{center}";
					}
					else
					{
						dict.Add(coordinate, center);
					}
				}
			}
			return dict;
		}
	}
	
	public Map(List<string> rawCoordinates)
	{
		foreach (var raw in rawCoordinates)
		{
			var split = raw.Split(',');
			var x = int.Parse(split[0]);
			var y = int.Parse(split[1]);
			var area = new Area(x, y);
			areas.Add(area);
			
			if (x > upperBound.X)
			{
				upperBound.X = x;
			}
			
			if (y > upperBound.Y)
			{
				upperBound.Y = y;
			}
		}
	}
}

class Area
{
	public Coordinate center;
	public List<Coordinate> coordinatesInArea;
	public bool isInfinite;

	public Area(int x, int y)
	{
		center = new Coordinate(x, y);
		coordinatesInArea = new List<Coordinate>();
		isInfinite = false;
	}
}

class Coordinate
{
	public int X;
	public int Y;
	
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