using System.Collections.Generic;

namespace OldPlace.Graph.Interfaces
{
    public interface IEdgePath : IEnumerable<IDirectedEdge>
    {
        void Push(IDirectedEdge edge);
        IDirectedEdge Pop();
        IDirectedEdge Peek();
        int Length { get; }
    }

}
