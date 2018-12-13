<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllText(Path.Combine(folder, "Day9_Input.txt"));
	
	
}



public class MarbleCircle
{
	public Marble CurrentMarble;
}

public class Marble
{
	public int Value;
	public Marble ClockwiseMarble;
	public Marble CounterClockwiseMarble;
	
	public Marble(int val)
	{
		Value = val;
		ClockwiseMarble = this;
		CounterClockwiseMarble = this;
	}
}