namespace Maze.Nodes;

internal class MazeNode
{
    public bool Visited { get; set; }
    public bool Wall { get; set; }
    public bool Start { get; set; }
    public bool Goal { get; set; }
    public bool Updated { get; set; } = true;
    public MazeNode? Parent { get; set; }
    public List<MazeNode> Neighbors { get; set; } = new List<MazeNode>();
}