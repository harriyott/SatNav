using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    public class Map
    {
        private readonly IList<CalcNode> _priorities = new List<CalcNode>();
        private readonly IList<Edge> _route = new List<Edge>();

        public Map()
        {
            Nodes = new Dictionary<string, Node>
            {
                {"A", new Node("A", 5, 5)},
                {"B", new Node("B", 4, 5)},
                {"C", new Node("C", 3, 5)},
                {"D", new Node("D", 2, 5)},
                {"E", new Node("E", 1, 4)},
                {"F", new Node("F", 1, 3)},
                {"G", new Node("G", 2, 1)},
                {"H", new Node("H", 3, 4)},
                {"I", new Node("I", 3, 3)},
                {"J", new Node("J", 2, 3)},
                {"K", new Node("K", 5, 3)},
                {"L", new Node("L", 5, 1)},
                {"M", new Node("M", 3, 1)},
                {"N", new Node("N", 1, 1)}
            };

            Edges = new List<Edge>
            {
                new Edge(Nodes["A"], Nodes["B"]),
                new Edge(Nodes["A"], Nodes["K"]),
                new Edge(Nodes["B"], Nodes["C"]),
                new Edge(Nodes["C"], Nodes["D"]),
                new Edge(Nodes["C"], Nodes["H"]),
                new Edge(Nodes["D"], Nodes["E"]),
                new Edge(Nodes["E"], Nodes["F"]),
                new Edge(Nodes["F"], Nodes["G"]),
                new Edge(Nodes["G"], Nodes["N"]),
                new Edge(Nodes["H"], Nodes["I"]),
                new Edge(Nodes["I"], Nodes["J"]),
                new Edge(Nodes["J"], Nodes["F"]),
                new Edge(Nodes["K"], Nodes["L"]),
                new Edge(Nodes["L"], Nodes["M"]),
                new Edge(Nodes["M"], Nodes["N"])
            };
        }

        public IList<Edge> GetRoute(string start, string end)
        {
            var startNode = Nodes[start];
            var endNode = Nodes[end];

            _priorities.Clear();
            _priorities.Add(new CalcNode(startNode, 0, new Edge(startNode, endNode).Length));

            FindNextNode(startNode, endNode);

            return _route;
        }

        private void FindNextNode(Node currentNode, Node endNode)
        {
            while (_priorities.Any())
            {
                _priorities.Remove(_priorities.FirstOrDefault(p => p.Node == currentNode));

                var availableEdges = GetAvailableEdges(currentNode);
                foreach (var edge in availableEdges)
                {
                    if (edge.End == endNode)
                    {
                        _route.Add(edge);
                        return;
                    }
                    var g = new Edge(currentNode, edge.End).Length;
                    var h = new Edge(edge.End, endNode).Length;
                    _priorities.Add(new CalcNode(edge.End, g, h));
                }
                if (LastNodeIsEndOfRoute(endNode)) return;

                var priority = GetNextBestPoint();
                var start = _route.Any() ? _route.Last().End : currentNode;

                AddEdgeMatchingTheseNodes(start, priority.Node);
                FindNextNode(priority.Node, endNode);
            }
        }

        private bool LastNodeIsEndOfRoute(Node endNode)
        {
            return _route.Any() && _route.Last().End == endNode;
        }

        private void AddEdgeMatchingTheseNodes(Node start, Node end)
        {
            _route.Add(Edges.Single(e => e.Start.Name == start.Name && e.End.Name == end.Name));
        }

        private CalcNode GetNextBestPoint()
        {
            var minF = _priorities.Min(p => p.F);
            var priority = _priorities.First(p => p.F == minF);
            return priority;
        }

        private IEnumerable<Edge> GetAvailableEdges(Node currentNode)
        {
            return Edges.Where(e => e.Start == currentNode);
        }

        private IList<Edge> Edges { get; }
        private IDictionary<string, Node> Nodes { get; }
    }
}