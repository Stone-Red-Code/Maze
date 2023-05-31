using Maze;
using Maze.Nodes;
using Maze.Solvers;
using Maze.Utlis;

using Stone_Red_Utilities.ConsoleExtentions;

MazeParser parser = new MazeParser();
MazePrinter printer = new MazePrinter();
PathFinder pathFinder = new PathFinder(new DepthFirstSearchMazeSolver());

try
{
    MazeNode[,] maze = parser.FromFile("input.txt", out MazeNode startNode);

    if (Console.WindowHeight < maze.GetLength(0) || Console.WindowWidth < maze.GetLength(1))
    {
        ConsoleExt.WriteLine("Console window size too small. Visualizing the maze may cause errors!", ConsoleColor.Red);
        ConsoleExt.Pause(true, "Press ENTER to continue or CTRL+C to exit the program.");
        Console.Clear();
    }

    Console.WriteLine("Choose an algorithm:");
    Type type = typeof(IMazeSolver);
    IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t => type.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

    int index = 0;

    foreach (Type t in types)
    {
        Console.WriteLine($"[{index++}] {t.Name}");
    }

    int algorithmIndex = -1;

    do
    {
        if (!int.TryParse(Console.ReadLine(), out algorithmIndex))
        {
            ConsoleExt.WriteLine("Invalid input. Please try again.", ConsoleColor.Red);
            algorithmIndex = -1;
        }
    }
    while (algorithmIndex < 0 || algorithmIndex >= types.Count());

    pathFinder.MazeSolver = (IMazeSolver)Activator.CreateInstance(types.ElementAt(algorithmIndex))!;

    List<MazeNode> solution = pathFinder.FindPath(startNode, maze, GraphicsMode.Colored);

    printer.PrintColored(maze, solution, redrawAll: true);

    Console.SetCursorPosition(0, maze.GetLength(0));
    ConsoleExt.WriteLine("Done", ConsoleColor.Green);
}
catch (MazeException e)
{
    ConsoleExt.WriteLine(e.Message, ConsoleColor.Red);
}