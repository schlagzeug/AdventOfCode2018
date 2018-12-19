<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllLines(Path.Combine(folder, "Day10_Input.txt")).ToList();
	
	var skyStepper = new SkyStepper(input);
	skyStepper.StepToLowestPoint();

	Part1(skyStepper).Dump("Part 1 Answer");
	Part2(skyStepper).Dump("Part 2 Answer");
}

string Part1(SkyStepper ss)
{
	return ss.Sky.ToString();
}

int Part2(SkyStepper ss)
{
	return ss.Sky.Increment;
}


public class SkyStepper
{
	public Sky Sky;
	
	public SkyStepper(List<string> dataPoints)
	{
		var pointList = new List<Point>();
		foreach (var data in dataPoints)
		{
			pointList.Add(new Point(data));
		}
		Sky = new Sky(pointList, 0);
	}
	
	public void StepPoints()
	{
		foreach (var point in Sky.Points)
		{
			point.Step();
		}
		Sky.Increment++;
		Sky.CalcHeight();
	}
	
	public void StepPointsBack()
	{
		foreach (var point in Sky.Points)
		{
			point.StepBack();
		}
		Sky.Increment--;
		Sky.CalcHeight();
	}
	
	public void StepToLowestPoint()
	{
		var previousHeight = Sky.Height;
		for (int i = 1; i < int.MaxValue; i++)
		{
			StepPoints();
			if (Sky.Height <= previousHeight)
			{
				previousHeight = Sky.Height;
			}
			else
			{
				StepPointsBack();
				break;
			}
		}
	}
}

public class Sky
{
	public List<Point> Points;
	public int Height;
	public int Increment;
	private int BiggestY;
	private int SmallestY;
	
	public Sky(List<Point> points, int increment)
	{
		Points = points;
		Increment = increment;
		CalcHeight();
	}
	
	public void CalcHeight()
	{
		BiggestY = 0;
		SmallestY = int.MaxValue;
		
		foreach (var point in Points)
		{
			if (point.Y > BiggestY) BiggestY = point.Y;
			if (point.Y < SmallestY) SmallestY = point.Y;
		}
		
		Height = BiggestY - SmallestY;
	}

	public override string ToString()
	{
		var retString = string.Empty;
		var biggestX = 0;
		var smallestX = int.MaxValue;

		foreach (var point in Points)
		{
			if (point.X > biggestX) biggestX = point.X;
			if (point.X < smallestX) smallestX = point.X;
		}

		//for (int y = BiggestY; y >= SmallestY; y--)
		for (int y = SmallestY; y <= BiggestY; y++)
		{
			var yString = string.Empty;
			for (int x = smallestX; x <= biggestX; x++)
			{
				var found = false;
				foreach (var point in Points)
				{
					if (point.X == x && point.Y == y)
					{
						found = true;
						break;
					}
				}

				if (found)
				{
					yString += '#';
				}
				else
				{
					yString += '.';
				}
			}
			retString += yString;
			retString += "\r\n";
		}
		
		return retString;
	}
}

public class Point 
{
	public int X;
	public int Y;
	public int Velocity_X;
	public int Velocity_Y;
	
	public Point(string data)
	{
		X = int.Parse(data.Substring(10, 6));
		Y = int.Parse(data.Substring(18, 6));
		Velocity_X = int.Parse(data.Substring(36, 2));
		Velocity_Y = int.Parse(data.Substring(40, 2));
	}
	
	public void Step()
	{
		X += Velocity_X;
		Y += Velocity_Y;
	}
	
	public void StepBack()
	{
		X -= Velocity_X;
		Y -= Velocity_Y;
	}
}