using Maze.Nodes;
using Maze.Utlis;

namespace Maze.Solvers;

internal class BreadthFirstSearchMazeSolver : IMazeSolver<BfsNode>
{
    public List<BfsNode> Solve(BfsNode node, BfsNode[,] maze, GraphicsMode graphicsMode = GraphicsMode.None)
    {
        if (!node.Start)
        {
            throw new MazeException("Node is not a start node.");
        }

        BfsNode currentNode;
        Queue<BfsNode> queue = new Queue<BfsNode>();

        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            currentNode = queue.Dequeue();
            currentNode.Updated = true;
            if (currentNode.Parent is not null)
            {
                currentNode.Parent.Updated = true;
            }

            if (currentNode.Goal)
            {
                List<BfsNode> path = new List<BfsNode>();
                BfsNode? pathNode = currentNode;

                while (pathNode is not null && !pathNode.Start)
                {
                    pathNode.Updated = true;
                    path.Add(pathNode);
                    pathNode = pathNode.Parent;
                }

                return path;
            }

            foreach (BfsNode child in currentNode.Neighbors.Where(n => !n.Visited && !n.Wall))
            {
                child.Parent = currentNode;
                child.Visited = true;
                child.Updated = true;
                queue.Enqueue(child);
            }

            if (graphicsMode != GraphicsMode.None)
            {
                Thread.Sleep(50);
                Console.SetCursorPosition(0, 0);
            }

            if (graphicsMode == GraphicsMode.BlackWhite)
            {
                new MazePrinter().Print(maze, queue.ToList());
            }
            else if (graphicsMode == GraphicsMode.Colored)
            {
                new MazePrinter().PrintColored(maze, queue.ToList());
            }
        }

        return new List<BfsNode>();
    }
}