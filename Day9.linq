<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllText(Path.Combine(folder, "Day9_Input.txt"));
	
	var gc = new GameController(input);
	
	Part1(gc).Dump("Part 1 Answer");
	Part2(gc).Dump("Part 2 Answer");
}

long Part1(GameController gc)
{
	return gc.PlayGame();
}

long Part2(GameController gc)
{
	gc.Reset();
	gc.HighestMarbleValue = gc.HighestMarbleValue * 100; 
	return gc.PlayGame();
}


public class GameController
{
	public MarbleCircle MarbleCircle = new MarbleCircle();
	public int NumberOfPlayers;
	public int HighestMarbleValue;
	public List<Player> PlayersScores;
	int CurrentPlayer;
	
	public GameController(string inputString)
	{
		var split = inputString.Split(' ');
		NumberOfPlayers = int.Parse(split[0]);
		HighestMarbleValue = int.Parse(split[6]);
		PlayersScores = new List<Player>();
		for (int i = 0; i < NumberOfPlayers; i++)
		{
			PlayersScores.Add(new Player());
		}
		CurrentPlayer = 0;
	}

	public long PlayGame()
	{
		for (int i = 1; i <= HighestMarbleValue; i++)
		{
			TakeTurn(i);
			NextPlayer();
		}

		var max = 0L;
		foreach (var player in PlayersScores)
		{
			if (player.Score > max)
			{
				max = player.Score;
			}
		}
		
		return max;
	}
	
	public void TakeTurn(int i)
	{
		if (i % 23 == 0)
		{
			PlayersScores[CurrentPlayer].Score += i;
			for (int j = 0; j < 7; j++)
			{
				MarbleCircle.Previous();
			}
			PlayersScores[CurrentPlayer].Score += MarbleCircle.CurrentMarble.Value.Value;
			MarbleCircle.RemoveCurrent();
		}
		else
		{
			MarbleCircle.Next();
			MarbleCircle.Insert(new Marble(i));
		}
	}
	
	public void NextPlayer()
	{
		CurrentPlayer++;
		if (CurrentPlayer >= NumberOfPlayers)
		{
			CurrentPlayer = 0;
		}
	}
	
	public void Reset()
	{
		MarbleCircle = new MarbleCircle();
		PlayersScores = new List<Player>();
		for (int i = 0; i < NumberOfPlayers; i++)
		{
			PlayersScores.Add(new Player());
		}
		CurrentPlayer = 0;
	}
}

public class MarbleCircle
{
	public LinkedList<Marble> Marbles;
	public LinkedListNode<Marble> CurrentMarble;
	
	
	public MarbleCircle()
	{
		Marbles = new LinkedList<Marble>();
		CurrentMarble = Marbles.AddFirst(new Marble(0));
	}
		
	public void Next()
	{
		if (CurrentMarble.Next == null)
		{
			CurrentMarble = Marbles.First;
		}
		else
		{
			CurrentMarble = CurrentMarble.Next;
		}
	}
	
	public void Previous()
	{
		if (CurrentMarble.Previous == null)
		{
			CurrentMarble = Marbles.Last;
		}
		else
		{
			CurrentMarble = CurrentMarble.Previous;
		}
	}
	
	public void Insert(Marble newMarble)
	{
		Marbles.AddAfter(CurrentMarble, newMarble);
		Next();
	}
	
	public void RemoveCurrent()
	{
		var mc = CurrentMarble;
		Next();
		Marbles.Remove(mc);
	}
	
}

public class Marble
{
	public int Value;
	
	public Marble(int val)
	{
		Value = val;
	}
}

public class Player
{
	public long Score { get; set; }
}