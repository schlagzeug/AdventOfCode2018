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
}

public class Header
{
	public int NumChildNodes;
	public int NumMetaData;
}