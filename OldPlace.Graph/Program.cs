using System;

namespace OldPlace.Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var nodes = Node.Create("A", "B", "C", "D", "E", "F", "G", "H");
            var graph = new Graph(nodes, @"
(A, B)
(A, C)
(B, D)
(B, F)
(C, D)
(C, E)
(D, F)
(E, G)
(F, H)");
            Console.WriteLine(graph);

            var path = graph.GetPath("A", "H");
            Console.WriteLine($"Path [A, H] : {path}");

        }
    }

}
