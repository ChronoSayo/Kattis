namespace TheKingOfTheNorth
{
    class Edge
    {
        public int From, To;
        public long Capacity, Flow;

        public Edge(int from, int to, long cap)
        {
            From = from;
            To = to;
            Capacity = cap;
            Flow = 0;
        }
    }
}