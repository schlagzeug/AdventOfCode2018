<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var inputs = File.ReadLines(Path.Combine(folder, "Day6_Input.txt")).ToList();
}


class Area
{
	int X;
	int Y;

	Area(int x, int y)
	{
		X = x;
		Y = y;
	}
}