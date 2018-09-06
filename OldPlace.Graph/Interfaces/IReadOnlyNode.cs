namespace OldPlace.Graph.Interfaces
{
    public interface IReadOnlyNode<T> : INode
    {
        T Value { get; }
    }

}
