namespace SatNav
{
    public class Node
    {
        public Node(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public string Name { get; }
        public int X { get; }
        public int Y { get; }
    }
}