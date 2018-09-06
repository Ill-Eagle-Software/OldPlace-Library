using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    public class DirectedEdge : Edge, IDirectedEdge
    {
        public DirectedEdge(INode origin, INode destination, Direction direction = Direction.LeftToRight) :
        base(direction == Direction.LeftToRight ? origin : destination, direction == Direction.RightToLeft ? origin : destination)
        {
            Direction = direction;
        }

        public DirectedEdge(string name, INode origin, INode destination, Direction direction = Direction.LeftToRight) :
                       base(name, direction == Direction.LeftToRight ? origin : destination, direction == Direction.RightToLeft ? origin : destination)
        {
            Direction = direction;
        }

        public Direction Direction { get; }

        public INode Origin => Direction == Direction.LeftToRight ? Left : Right;

        public INode Destination => Direction == Direction.RightToLeft ? Left : Right;

        public override string ToString() => $"({Origin.Name}, {Destination.Name})";
    }

    public class DirectedEdge<T> : DirectedEdge, IDirectedEdge<T>
    {
        public DirectedEdge(string name, INode origin, INode destination, T value, Direction direction = Direction.LeftToRight) : base(name, origin, destination, direction)
        {
            Value = value;
        }

        public T Value { get; protected set; }
    }

}
