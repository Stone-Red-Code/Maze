﻿using Maze.Nodes;
using Maze.Utlis;

namespace Maze.Solvers;

internal class DepthFirstSearchMazeSolver : IMazeSolver
{
    public List<MazeNode> Solve(MazeNode node, MazeNode[,] maze, GraphicsMode graphicsMode = GraphicsMode.None)
    {
        if (!node.Start)
        {
            throw new MazeException("Node is not a start node.");
        }

        MazeNode? currentNode = node;
        MazeNode? tempNode = null;
        Stack<MazeNode> path = new Stack<MazeNode>();

        while (!currentNode.Goal)
        {
            currentNode.Visited = true;

            if (currentNode.Neighbors.Count == 0)
            {
                throw new MazeException("Node has no neighbors.");
            }

            if (path.TryPeek(out MazeNode? prevNode))
            {
                prevNode.Updated = true;
            }

            currentNode = currentNode.Neighbors.FirstOrDefault(n => !n.Visited && !n.Wall);

            if (currentNode is null)
            {
                if (!path.TryPop(out currentNode))
                {
                    throw new MazeException("No path found.");
                }

                if (path.TryPeek(out MazeNode? prevNode2))
                {
                    prevNode2.Updated = true;
                }

                tempNode = currentNode;
            }
            else
            {
                // Add the previous node to the path
                if (tempNode is not null)
                {
                    tempNode.Updated = true;
                    path.Push(tempNode);
                    tempNode = null;
                }
                path.Push(currentNode);
            }

            currentNode.Updated = true;

            if (graphicsMode != GraphicsMode.None)
            {
                Thread.Sleep(30);
                Console.SetCursorPosition(0, 0);
            }

            if (graphicsMode == GraphicsMode.BlackWhite)
            {
                new MazePrinter().Print(maze, path.ToList());
            }
            else if (graphicsMode == GraphicsMode.Colored)
            {
                new MazePrinter().PrintColored(maze, path.ToList());
            }
        }

        List<MazeNode> solution = path.ToList();
        solution.ForEach(n => n.Updated = true);
        return solution;
    }
}