using Maze.Nodes;

namespace Maze.Solvers;

internal interface IMazeSolver<T> where T : BaseMazeNode<T>, new()
{
    public List<T> Solve(T node, T[,] maze, GraphicsMode graphicsMode = GraphicsMode.None);
}