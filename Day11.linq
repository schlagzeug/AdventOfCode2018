<Query Kind="Program" />

public static int GRID_SERIAL_NUMBER = 3463;

void Main()
{
	var fc = new FuelCellGrid();
	
	Part1(fc).Dump("Part 1 Answer");
	Part2(fc).Dump("Part 2 Answer"); // takses a while, first run took 7.5 minutes
}

string Part1(FuelCellGrid fcg)
{
	int val;
	var fc = fcg.FindBest(3, out val);
	return $"{fc.X},{fc.Y}";
}

string Part2(FuelCellGrid fcg)
{
	var bestTotal = 0;
	var bestGridSize = -1;
	FuelCell bestCell = null;
	
	for (int i = 1; i <= 300; i++)
	{
		int high = 0;
		var fc = fcg.FindBest(i, out high);
		if (high > bestTotal)
		{
			bestTotal = high;
			bestGridSize = i;
			bestCell = fc;
		}
	}

	return $"{bestCell.X},{bestCell.Y},{bestGridSize}";
}

public class FuelCellGrid
{
	public FuelCell[,] Grid;
	
	public FuelCellGrid()
	{
		Grid = new FuelCell[300,300];
		for (int x = 0; x < 300; x++)
		{
			for (int y = 0; y < 300; y++)
			{
				var fc = new FuelCell(x + 1, y + 1);
				Grid[x,y] = fc;
			}
		}
	}
	
	public FuelCell FindBest(int size, out int highest)
	{
		highest = 0;
		FuelCell retCell = null;
		for (int x = 0; x < 300 - size; x++)
		{
			for (int y = 0; y < 300 - size; y++)
			{
				var cellTot = GetSquareTotal(x, y, size);
				if (cellTot > highest)
				{
					highest = cellTot;
					retCell = Grid[x, y];
				}
			}
		}

		return retCell;
	}

	private int GetSquareTotal(int x, int y, int size)
	{
		var total = 0;
		for (int a = x; a < x + size; a++)
		{
			for (int b = y; b < y + size; b++)
			{
				total += Grid[a, b].PowerLevel;
			}
		}
		return total;
	}
}

public class FuelCell
{
	public int X;
	public int Y;
	public int PowerLevel;
	
	public FuelCell(int x, int y)
	{
		X = x;
		Y = y;
		SetPowerLevel();
	}
	
	public void SetPowerLevel()
	{
		var rackID = X + 10;
		var pwrLvl = rackID * Y;
		pwrLvl += GRID_SERIAL_NUMBER;
		pwrLvl *= rackID;
		pwrLvl = (int)Math.Abs(pwrLvl / 100 % 10);
		pwrLvl -= 5;
		
		PowerLevel = pwrLvl;
	}
}