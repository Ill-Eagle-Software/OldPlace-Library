namespace OldPlace.Graph.Interfaces
{
    public interface IEdge : IElement
    {
        INode Left { get; }
        INode Right { get; }
    }

    public interface IEdge<T> : IEdge
    {
        T Value { get; }
    }
}
