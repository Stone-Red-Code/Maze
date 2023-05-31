using Maze.Nodes;

namespace Maze.Utlis;

internal class MazeParser
{
    public MazeNode[,] FromFile(string filePath, out MazeNode startNode)
    {
        return FromString(File.ReadAllText(filePath), out startNode);
    }

    public MazeNode[,] FromString(string input, out MazeNode startNode)
    {
        string[] lines = input.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        startNode = null!;
        MazeNode? goalNode = null;
        MazeNode[,] nodes = new MazeNode[lines.Length, lines[0].Length];

        // Create nodes from input
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                char c = lines[y][x];

                MazeNode node = new MazeNode
                {
                    Wall = c == '+',
                    Start = c == 'S',
                    Goal = c == 'G'
                };

                if (node.Start)
                {
                    if (startNode is not null)
                    {
                        throw new MazeException("Multiple start nodes found.");
                    }

                    startNode = node;
                }

                if (node.Goal)
                {
                    if (goalNode is not null)
                    {
                        throw new MazeException("Multiple goal nodes found.");
                    }
                    goalNode = node;
                }

                nodes[y, x] = node;
            }
        }

        if (startNode is null)
        {
            throw new MazeException("No start node found.");
        }

        if (goalNode is null)
        {
            throw new MazeException("No goal node found.");
        }

        // Add neighbors to nodes based on their position in the maze
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                MazeNode node = nodes[y, x];

                if (x < lines[0].Length - 1)
                {
                    node.Neighbors.Add(nodes[y, x + 1]);
                }
                if (x > 0)
                {
                    node.Neighbors.Add(nodes[y, x - 1]);
                }
                if (y < lines.Length - 1)
                {
                    node.Neighbors.Add(nodes[y + 1, x]);
                }
                if (y > 0)
                {
                    node.Neighbors.Add(nodes[y - 1, x]);
                }
            }
        }

        return nodes;
    }
}