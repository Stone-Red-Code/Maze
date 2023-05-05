using System.Runtime.Serialization;

namespace Maze;

[Serializable]
public class MazeException : Exception
{
    public MazeException()
    {
    }

    public MazeException(string message) : base(message)
    {
    }

    public MazeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MazeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}