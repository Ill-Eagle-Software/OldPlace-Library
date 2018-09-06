using System;
using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    public class ReadOnlyNode<T> : Node, IReadOnlyNode<T>
    {
        public ReadOnlyNode(string name, T value) : base(name)
        {
            Value = value;
        }

        public T Value { get; protected set; }

        public override void Dispose() => (Value as IDisposable)?.Dispose();
    }

}
