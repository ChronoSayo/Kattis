using System;
using System.Collections.Generic;

namespace TheKingOfTheNorth
{
    class Dinic
    {
        private int _numNode;
        private List<Edge> _edges;
        private List<List<int>> _adjacencyList;
        private int[] _distance, _currentEdgeIndex;

        public Dinic(int numNode)
        {
            _numNode = numNode;
            _edges = new List<Edge>();
            _adjacencyList = new List<List<int>>(numNode);
            for (int i = 0; i < numNode; i++)
            {
                _adjacencyList.Add(new List<int>());
            }
            _distance = new int[numNode];
            _currentEdgeIndex = new int[numNode];
        }

        public void AddEdge(int from, int to, long capacity)
        {
            if (from != to)
            {
                _edges.Add(new Edge(from, to, capacity));
                _adjacencyList[from].Add(_edges.Count - 1);
                _edges.Add(new Edge(to, from, 0));
                _adjacencyList[to].Add(_edges.Count - 1);
            }
        }

        bool BFS(int source, int sink)
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(source);
            Array.Fill(_distance, _numNode + 1);
            _distance[source] = 0;
            while (q.Count > 0)
            {
                int currentNode = q.Dequeue();
                if (currentNode == sink) 
                    break;
                foreach (int k in _adjacencyList[currentNode])
                {
                    Edge currentEdge = _edges[k];
                    if (currentEdge.Flow < currentEdge.Capacity && _distance[currentEdge.To] > _distance[currentEdge.From] + 1)
                    {
                        _distance[currentEdge.To] = _distance[currentEdge.From] + 1;
                        q.Enqueue(currentEdge.To);
                    }
                }
            }
            return _distance[sink] != _numNode + 1;
        }

        long DFS(int currentNode, int sink, long flow = -1)
        {
            if (currentNode == sink || flow == 0) return flow;
            for (; _currentEdgeIndex[currentNode] < _adjacencyList[currentNode].Count; _currentEdgeIndex[currentNode]++)
            {
                Edge currentEdge = _edges[_adjacencyList[currentNode][_currentEdgeIndex[currentNode]]];
                Edge reverseEdge = _edges[_adjacencyList[currentNode][_currentEdgeIndex[currentNode]] ^ 1];
                if (_distance[currentEdge.To] == _distance[currentEdge.From] + 1)
                {
                    long amt = currentEdge.Capacity - currentEdge.Flow;
                    if (flow != -1 && amt > flow) 
                        amt = flow;
                    long pushed = DFS(currentEdge.To, sink, amt);
                    if (pushed > 0)
                    {
                        currentEdge.Flow += pushed;
                        reverseEdge.Flow -= pushed;
                        return pushed;
                    }
                }
            }
            return 0;
        }

        public long MaxFlow(int source, int sink)
        {
            long total = 0;
            while (BFS(source, sink))
            {
                Array.Fill(_currentEdgeIndex, 0);
                long flow;
                while ((flow = DFS(source, sink)) > 0)
                    total += flow;
            }
            return total;
        }
    }
}
