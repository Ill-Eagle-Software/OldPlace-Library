using System.Collections.Generic;

namespace OldPlace.Graph.Interfaces
{
    public interface IMutableGraph
    {
        IEnumerable<IEdge> Edges { get; }
        IEnumerable<INode> Nodes { get; }

        IEnumerable<IEdge> GetReferences(INode node, bool undirectedOnly = false);
        IEnumerable<IDirectedEdge> GetOrigins(INode node);
        IEnumerable<IDirectedEdge> GetDestinations(INode node);


        void Add(params IElement[] elements);
        void Remove(params IElement[] elements);

        bool IsDigraph();
        bool IsPOSet();
    }

}
