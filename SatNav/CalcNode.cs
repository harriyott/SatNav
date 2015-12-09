namespace SatNav
{
    public class CalcNode
    {
        public CalcNode(Node node, decimal g, decimal h)
        {
            Node = node;
            F = g + h;
        }

        public Node Node { get; }
        public decimal F { get; }
    }
}
