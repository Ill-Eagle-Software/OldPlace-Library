using System.Collections.Generic;
using System.Linq;
using System.Text;
using OldPlace.Graph.Interfaces;

namespace OldPlace.Graph
{
    public class PathFinderContext : IPathFinderContext
    {

        public PathFinderContext()
        {
            CurrentPath = new EdgePath();
        }

        public IEnumerable<IEdgePath> Paths { get; } = new List<IEdgePath>();

        public IEdgePath CurrentPath { get; protected set; }

        public virtual IEdgePath BestPath => GetBestPath();

        public void Abandon()
        {
            CurrentPath = new EdgePath();
        }

        public void Commit()
        {
            PathList.Add(new EdgePath(CurrentPath));
        }

        protected List<IEdgePath> PathList => Paths as List<IEdgePath>;        

        protected virtual IEdgePath GetBestPath() => PathList.OrderBy(p => p.Length).FirstOrDefault();

        public override string ToString()
        {
            var bestPath = BestPath?.ToString() ?? "NO VIABLE PATH";
            var s = new StringBuilder();
            s.AppendLine("[VIABLE PATHS]");
            foreach (var p in Paths)
            {
                var path = p.ToString();
                var isBest = path == bestPath;
                s.AppendLine($"  {(isBest ? "*" : " ")} {path}");
            }
            return s.ToString();
        }

    }

}
