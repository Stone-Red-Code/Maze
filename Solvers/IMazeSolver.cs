using Maze.Nodes;

namespace Maze.Solvers;

internal interface IMazeSolver
{
    public List<MazeNode> Solve(MazeNode node, MazeNode[,] maze, GraphicsMode graphicsMode = GraphicsMode.None);
}