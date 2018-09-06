using System.Collections.Generic;

namespace OldPlace.Graph.Interfaces
{
    public interface IGraph
    {
        IEnumerable<IEdge> Edges { get; }
        IEnumerable<INode> Nodes { get; }

        IEnumerable<IEdge> GetReferences(INode node, bool undirectedOnly = false);
        IEnumerable<IDirectedEdge> GetOrigins(INode node);
        IEnumerable<IDirectedEdge> GetDestinations(INode node);

        IEdgePath GetPath(INode origin, INode destination);
        IEdgePath GetPath(INode origin, string destinationName);
        IEdgePath GetPath(string originName, string destinationName);
        IEnumerable<IEdgePath> GetPaths(INode origin, INode destination);

        bool IsDigraph();
        bool IsPOSet();
    }

}
