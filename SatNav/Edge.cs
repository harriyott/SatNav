namespace SatNav
{
    public class Edge
    {
        public Edge(Node start, Node end)
        {
            Start = start;
            End = end;
        }

        public Node Start { get; }
        public Node End { get; }

        public decimal Length => TapeMeasure.GetDistanceBetweenPoints(Start.X, End.X, Start.Y, End.Y);

        public string Name => Start.Name + End.Name;
    }
}