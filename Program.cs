using Maze;
using Maze.Nodes;
using Maze.Solvers;
using Maze.Utlis;

using Stone_Red_Utilities.ConsoleExtentions;

MazeParser parser = new MazeParser();
MazePrinter printer = new MazePrinter();
IMazeSolver<DfsNode> solver = new DepthFirstSearchMazeSolver();

try
{
    DfsNode[,] maze = parser.FromFile("input.txt", out DfsNode startNode);

    if (Console.WindowHeight < maze.GetLength(0) || Console.WindowWidth < maze.GetLength(1))
    {
        ConsoleExt.WriteLine("Console window size too small. Visualizing the maze may cause errors!", ConsoleColor.Red);
        ConsoleExt.Pause(true, "Press ENTER to continue or CTRL+C to exit the program.");
        Console.Clear();
    }

    List<DfsNode> solution = solver.Solve(startNode, maze, GraphicsMode.None);

    printer.PrintColored(maze, solution, redrawAll: true);

    Console.SetCursorPosition(0, maze.GetLength(0));
    ConsoleExt.WriteLine("Done", ConsoleColor.Green);
}
catch (MazeException e)
{
    ConsoleExt.WriteLine(e.Message, ConsoleColor.Red);
}