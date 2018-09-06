using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    public class MutableGraph : Graph, IMutableGraph
    {
        public void Add(params IElement[] elements)
        {
            foreach (var el in elements)
            {
                if (el is INode)
                {
                    _nodes.Add(el as INode);
                }
                else
                    _edges.Add(el as IEdge);

            }
        }

        public void Remove(params IElement[] elements)
        {
            foreach (var el in elements)
            {
                if (el is INode)
                {
                    _nodes.Remove(el as INode);
                }
                else
                    _edges.Remove(el as IEdge);

            }
        }
    }

}
