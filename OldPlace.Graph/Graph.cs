using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    /********************************************/

    public class Graph : IGraph
    {

        public IEnumerable<IEdge> Edges => _edges;
        public IEnumerable<INode> Nodes => _nodes;


        public Graph(IEnumerable<INode> nodes, string edgeScript)
        {
            _nodes = nodes.ToList();
            _edges = new List<IEdge>();
            var entries = edgeScript.Split(Environment.NewLine.ToCharArray()).Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Trim());
            foreach (var line in entries)
            {
                bool isOrdered = false;
                switch (line[0])
                {
                    case '{': // Unordered
                        isOrdered = false;
                        break;
                    case '(': // Ordered
                        isOrdered = true;
                        break;
                }
                var names = line.Substring(1, line.Length - 2).Split(',').Select(l => l.Trim());
                var left = Nodes.First(n => n.Name == names.First());
                var right = Nodes.First(n => n.Name == names.Last());
                IEdge edge = isOrdered ?
                    new DirectedEdge(left, right) :
                    new Edge(left, right);
                _edges.Add(edge);
            }
        }

        public Graph(IEnumerable<INode> nodes, IEnumerable<IEdge> edges)
        {
            _nodes = nodes.ToList();
            _edges = edges.ToList();
        }

        public Graph(params IElement[] elements)
        {
            _edges = new List<IEdge>();
            _nodes = new List<INode>();
            foreach (var e in elements)
            {
                if (e is INode)
                {
                    _nodes.Add(e as INode);
                }
                else
                {
                    _edges.Add(e as IEdge);
                }
            }
        }

        public IEnumerable<IDirectedEdge> GetDestinations(INode node)
        {
            return DirectedEdges.Where(de => de.Origin == node);
        }

        public IEnumerable<IDirectedEdge> GetOrigins(INode node)
        {
            return DirectedEdges.Where(de => de.Destination == node);
        }

        public IEnumerable<IEdge> GetReferences(INode node, bool undirectedOnly = false)
        {
            if (undirectedOnly)
            {
                return Edges.Where(i => !(i is IDirectedEdge) && (i.Left == node || i.Right == node));
            }
            return Edges.Where(i => i.Left == node || i.Right == node);
        }

        public bool IsDigraph() => Edges.All(e => e is IDirectedEdge);

        public bool IsPOSet()
        {
            if (!IsDigraph()) return false;
            throw new NotImplementedException();
        }

        public IEdgePath GetPath(INode origin, INode destination)
        {
            var ctx = FindPaths(origin, destination.Name);
            return ctx.BestPath;
        }

        public IEnumerable<IEdgePath> GetPaths(INode origin, INode destination)
        {
            var ctx = FindPaths(origin, destination.Name);
            return ctx.Paths;
        }

        public IEdgePath GetPath(INode origin, string destinationName)
        {
            var ctx = FindPaths(origin, destinationName);
            return ctx.BestPath;
        }

        public IEdgePath GetPath(string originName, string destinationName)
        {
            var ctx = FindPaths(originName, destinationName);
            return ctx.BestPath;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("NODES\n");
            foreach (var n in Nodes)
            {
                sb.AppendLine($"   {n.Name}");
            }
            sb.AppendLine("\n\nEDGES\n");
            foreach (var e in Edges)
            {
                sb.AppendLine($"   {e}");
            }
            return sb.ToString();
        }

        #region Supporting Methods

        protected enum State
        {
            Searching,
            Found,
            NotFound
        }

        protected List<IEdge> _edges;
        protected List<INode> _nodes;

        protected IPathFinderContext FindPaths(string originName, string destinationName)
        {
            var origin = Nodes.First(n => n.Name == originName);
            return FindPaths(origin, destinationName);
        }

        protected IPathFinderContext FindPaths(INode origin, string destinationName)
        {
            var ctx = new PathFinderContext();
            InternalFinder(ctx, origin, destinationName);
            Console.WriteLine(ctx);
            return ctx;
        }

        protected State InternalFinder(IPathFinderContext ctx, string originName, string destinationName)
        {
            var origin = Nodes.First(n => n.Name == originName);
            return InternalFinder(ctx, origin, destinationName);
        }

        protected virtual State InternalFinder(IPathFinderContext ctx, INode origin, string destinationName)
        {
            var returnState = State.Searching;
            if (origin.Name == destinationName)
            {
                //ctx.Commit();
                return State.Found;
            }

            var from = GetDestinations(origin);

            if (!from.Any())
            {
                //ctx.Abandon();
                return State.NotFound;
            }

            foreach (var l in from)
            {
                ctx.CurrentPath.Push(l);
                var state = InternalFinder(ctx, l.Destination, destinationName);
                switch (state)
                {
                    case State.Found:
                        ctx.Commit();
                        break;
                    case State.NotFound:
                        break;
                    case State.Searching:
                        break;
                }
                ctx.CurrentPath.Pop();
            }
            return returnState;
        }

        protected IEnumerable<IDirectedEdge> DirectedEdges => Edges.Where(e => e is IDirectedEdge).Cast<IDirectedEdge>();

        #endregion
    }

}
