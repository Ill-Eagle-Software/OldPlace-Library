using System.Collections.Generic;
using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    /********************************************/

    public class Node : INode
    {
        public static IEnumerable<INode> Create(params string[] names)
        {
            foreach (var n in names) yield return new Node(n);
        }

        public Node(string name) => Name = name;

        public string Name { get; }

        public virtual void Dispose()
        { }
    }

    public class Node<T> : Node, INode<T>
    {
        public Node(string name, T value) : base(name)
        {
            Value = value;
        }

        public T Value { get; set; }

        public static implicit operator ReadOnlyNode<T>(Node<T> value) => new ReadOnlyNode<T>(value.Name, value.Value);
        public static implicit operator Node<T>(ReadOnlyNode<T> value) => new Node<T>(value.Name, value.Value);
    }

}
