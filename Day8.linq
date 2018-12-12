<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllText(Path.Combine(folder, "Day8_Input.txt")).ToList();
}


public static class NodeFactory
{
	
}

public class Node
{
	public Header Header;
	public List<Node> ChildNodes = new List<Node>();
	public List<int> MetaData = new List<int>();
	
	public int SumMyMetaData()
	{
		int sum = 0;
		foreach (var data in MetaData)
		{
			sum += data;
		}
		return sum;
	}
	
	public int SumAllMetaData()
	{
		int sum = SumMyMetaData();
		foreach (var child in ChildNodes)
		{
			sum += child.SumAllMetaData();
		}
		return sum;
	}
}

public class Header
{
	public int NumChildNodes;
	public int NumMetaData;
}