using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct Step
{
    public Node last;
    public float distance;
    public bool visited;

    public static Step Init;

    static Step()
    {
        Init.last = null;
        Init.distance = float.MaxValue;
        Init.visited = false;
    }

    public Step(Node l, float dist)
    {
        last = null;
        distance = float.MaxValue;
        visited = true;
    }
}

public class Graph
{
    public Dictionary<int, Node> Nodes { get; private set; }
    public Dictionary<int, Road> Roads { get; private set; }
    public float[,] MapTable;

    public Graph(Dictionary<int, Node> nodes, Dictionary<int, Road> rodes)
    {
        Nodes = nodes;
        Roads = rodes;
    }

    //public void BuildTable(Army )
    //{
    //    MapTable = Array.CreateInstance(typeof(float), Nodes.Count, Nodes.Count) as float[,];

    //    for (int i = 0; i < Nodes.Count; i++)
    //        for (int j = 0; j < Nodes.Count; j++)
    //            MapTable[i,j] = float.MaxValue;

    //    foreach (var r in Roads.Values)
    //    {
    //        r.
    //    }
    //}

    public Dictionary<Node, float> GetAccessibleNodes(Army army)
    {
        Queue<Node> queue = new Queue<Node>();
        Dictionary<Node,float> nodes = new Dictionary<Node, float>();
        HashSet<Node> hasarmy = new HashSet<Node>();

        queue.Enqueue(army.Node);
        nodes[army.Node] = 0;
        hasarmy.Add(army.Node);

        while (queue.Count != 0)
        {
            Node cur = queue.Dequeue();
            foreach (var r in cur.Roads)
            {
                Node next = r.Node1 == cur ? r.Node2 : r.Node1;
                float dist = nodes[cur] + r.GetCost(army, cur);

                if (nodes.ContainsKey(next)&&dist<nodes[next])
                    continue;
                if (dist > army.velocity)
                    continue;
                if (next.Army != null && next.Army.Owner != army.Owner)
                    continue;
                if (next.Army != null && next.Army.Owner == army.Owner)
                    hasarmy.Add(next);

                queue.Enqueue(next);
                nodes[next]= dist;
            }
        }

        foreach (var n in hasarmy)
            nodes.Remove(n);

        return nodes;
    }

    public HashSet<Node> GetAttackableNodes(Army army)
    {
        Queue<Node> queue = new Queue<Node>();
        Dictionary<Node, float> nodes = new Dictionary<Node, float>();
        HashSet<Node> hasarmy = new HashSet<Node>();

        queue.Enqueue(army.Node);
        nodes[army.Node] = 0;

        while (queue.Count != 0)
        {
            Node cur = queue.Dequeue();
            foreach (var r in cur.Roads)
            {
                Node next = r.Node1 == cur ? r.Node2 : r.Node1;
                float dist = nodes[cur] + r.GeAttacktCost(army, cur);

                if (nodes.ContainsKey(next) && dist < nodes[next])
                    continue;
                if (dist > army.attackRange)
                    continue;
                if (next.Army != null && next.Army.Owner != army.Owner)
                {
                    hasarmy.Add(next);
                    continue;
                }

                queue.Enqueue(next);
                nodes[next] = dist;
            }
        }

        return hasarmy;
    }

    public Dictionary<Node,Step> Dijkstra(Node source)
    {
        Dictionary<Node, Step> ans = new Dictionary<Node, Step>(Nodes.Count);
        List<Node> unvisited = new List<Node>(Nodes.Values);

        foreach (var n in Nodes.Values)
            ans[n] = new Step();

        ans[source] = new Step(source, 0);

        for (int i = 0; i < ans.Count; i++)
        {
            Node min = unvisited[0];
            foreach (var n in unvisited)
            {
                if (ans[n].distance < ans[min].distance)
                    min = n;
            }

            unvisited.Remove(min);

            foreach (var r in min.Roads)
            {
                
            }
        }

        return ans;
    }
}
