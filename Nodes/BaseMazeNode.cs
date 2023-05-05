namespace Maze.Nodes;

internal abstract class BaseMazeNode<T> where T : BaseMazeNode<T>
{
    public bool Visited { get; set; }
    public bool Wall { get; set; }
    public bool Start { get; set; }
    public bool Goal { get; set; }
    public bool Updated { get; set; } = true;
    public List<T> Neighbors { get; set; } = new List<T>();
}