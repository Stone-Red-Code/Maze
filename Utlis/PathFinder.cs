using Maze.Nodes;
using Maze.Solvers;

namespace Maze.Utlis;

internal class PathFinder
{
    public IMazeSolver MazeSolver { get; set; }

    public PathFinder(IMazeSolver mazeSolver)
    {
        MazeSolver = mazeSolver;
    }

    public List<MazeNode> FindPath(MazeNode node, MazeNode[,] maze, GraphicsMode graphicsMode = GraphicsMode.None)
    {
        return MazeSolver.Solve(node, maze, graphicsMode);
    }
}