using Maze.Nodes;

using Stone_Red_Utilities.ConsoleExtentions;

namespace Maze.Utlis;

internal class MazePrinter
{
    public void Print(MazeNode[,] nodes, List<MazeNode>? solution = null, bool redrawAll = false)
    {
        for (int y = 0; y < nodes.GetLength(0); y++)
        {
            for (int x = 0; x < nodes.GetLength(1); x++)
            {
                MazeNode node = nodes[y, x];

                if (!node.Updated && !redrawAll)
                {
                    continue;
                }

                node.Updated = false;

                Console.SetCursorPosition(x, y);

                if (node.Wall)
                {
                    Console.Write('+');
                }
                else if (node.Start)
                {
                    Console.Write('S');
                }
                else if (node.Goal)
                {
                    Console.Write('G');
                }
                else if (solution?.Contains(node) == true)
                {
                    Console.Write(GetDirection(x, y, node, nodes, solution));
                }
                else if (node.Visited)
                {
                    Console.Write('-');
                }
                else
                {
                    Console.Write(' ');
                }
            }
        }
    }

    public void PrintColored(MazeNode[,] nodes, List<MazeNode>? solution = null, bool redrawAll = false)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;

        for (int y = 0; y < nodes.GetLength(0); y++)
        {
            for (int x = 0; x < nodes.GetLength(1); x++)
            {
                MazeNode node = nodes[y, x];

                if (!node.Updated && !redrawAll)
                {
                    continue;
                }

                node.Updated = false;

                Console.SetCursorPosition(x, y);

                if (node.Wall)
                {
                    ConsoleExt.Write('+', ConsoleColor.White);
                }
                else if (node.Start)
                {
                    ConsoleExt.Write('S', ConsoleColor.Magenta);
                }
                else if (node.Goal)
                {
                    ConsoleExt.Write('G', ConsoleColor.Red);
                }
                else if (solution?.Contains(node) == true)
                {
                    ConsoleExt.Write(GetDirection(x, y, node, nodes, solution), ConsoleColor.Green);
                }
                else if (node.Visited)
                {
                    ConsoleExt.Write('-', ConsoleColor.DarkGray);
                }
                else
                {
                    Console.Write(' ');
                }
            }
        }

        Console.ResetColor();
        Console.CursorVisible = false;
    }

    // Get the direction of the next node in the solution list
    private static char GetDirection<T>(int x, int y, T node, T[,] nodes, List<T> solution)
    {
        int index = solution.IndexOf(node);
        T? nextNode = solution.ElementAtOrDefault(index - 1);

        if (y > 0 && nextNode?.Equals(nodes[y - 1, x]) == true)
        {
            return '^';
        }
        else if (y < nodes.GetLength(0) - 1 && nextNode?.Equals(nodes[y + 1, x]) == true)
        {
            return 'v';
        }
        else if (x > 0 && nextNode?.Equals(nodes[y, x - 1]) == true)
        {
            return '<';
        }
        else if (x < nodes.GetLength(1) - 1 && nextNode?.Equals(nodes[y, x + 1]) == true)
        {
            return '>';
        }
        else
        {
            return 'O';
        }
    }
}