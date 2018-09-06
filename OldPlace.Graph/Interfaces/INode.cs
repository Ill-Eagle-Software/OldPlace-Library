namespace OldPlace.Graph.Interfaces
{
    public interface INode : IElement
    {
    }

    public interface INode<T> : IReadOnlyNode<T>
    {
        new T Value { get; set; }
    }

}
