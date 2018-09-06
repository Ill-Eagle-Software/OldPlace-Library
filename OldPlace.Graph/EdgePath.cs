using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    /********************************************/

    public class EdgePath : IEdgePath
    {

        public EdgePath() { }
        public EdgePath(IEnumerable<IDirectedEdge> path)
        {
            foreach (var link in path) Push(link);
        }

        public void Push(IDirectedEdge link) => Segments.Push(link);
        public IDirectedEdge Pop() => Segments.Pop();
        public IDirectedEdge Peek() => Segments.Peek();
        public int Length => Segments.Count();

        protected Stack<IDirectedEdge> Segments { get; set; } = new Stack<IDirectedEdge>();
        IEnumerator<IDirectedEdge> IEnumerable<IDirectedEdge>.GetEnumerator() => Segments.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Segments.GetEnumerator();

        public override string ToString()
        {
            var segs = Segments.ToList();
            var results = string.Empty;
            foreach (var seg in segs)
            {
                results += seg.Origin.Name + " --> ";
            }
            results += segs.Last().Destination.Name;
            return results;
        }
    }

}
