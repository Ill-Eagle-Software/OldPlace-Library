using System.Collections.Generic;

namespace OldPlace.Graph.Interfaces
{
    public interface IPathFinderContext
    {
        IEnumerable<IEdgePath> Paths { get; }
        IEdgePath CurrentPath { get; }
        void Commit();
        void Abandon();
        IEdgePath BestPath { get; }
    }

}
