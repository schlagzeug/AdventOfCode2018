<Query Kind="Program" />

void Main()
{
	var folder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var input = File.ReadAllText(Path.Combine(folder, "Day8_Input.txt"));
	
	var root = NodeFactory.CreateNodeTree(input);
	root.Dump();
	
	Part1(root).Dump("Part 1 Answer");
	Part2(root).Dump("Part 2 Answer");
}

int Part1(Node rootNode)
{
	return rootNode.SumAllMetaData();
}

int Part2(Node rootNode)
{
	return rootNode.GetValue();
}


public static class NodeFactory
{
	public static Node CreateNodeTree(string rawData)
	{
		var dataSplit = rawData.Split(' ');
		Queue<int> dataQueue = new Queue<int>();
		foreach (var data in dataSplit)
		{
			dataQueue.Enqueue(int.Parse(data));
		}
		
		var rootNode = CreateNode(dataQueue);
		
		return rootNode;
	}

	private static Node CreateNode(Queue<int> queue)
	{
		var node = new Node();
		node.Header.NumChildNodes = queue.Dequeue();
		node.Header.NumMetaData = queue.Dequeue();

		for (int i = 0; i < node.Header.NumChildNodes; i++)
		{
			var child = CreateNode(queue);
			node.ChildNodes.Add(child);
		}

		for (int i = 0; i < node.Header.NumMetaData; i++)
		{
			node.MetaData.Add(queue.Dequeue());
		}

		return node;
	}
}

public class Node
{
	public Header Header = new Header();
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
	
	public int GetValue()
	{
		if (ChildNodes.Count == 0)
		{
			return SumMyMetaData();
		}
		else
		{
			int value = 0;
			foreach (var data in MetaData)
			{
				if (data > 0 && data <= ChildNodes.Count)
				{
					value += ChildNodes[data - 1].GetValue();
				}
			}
			return value;
		}
	}
}

public class Header
{
	public int NumChildNodes;
	public int NumMetaData;
}