namespace OldPlace.Graph.Interfaces
{
    public interface IDirectedEdge : IEdge
    {
        Direction Direction { get; }
        INode Origin { get; }
        INode Destination { get; }
    }

    public interface IDirectedEdge<T> : IDirectedEdge
    {
        T Value { get; }
    }

}
