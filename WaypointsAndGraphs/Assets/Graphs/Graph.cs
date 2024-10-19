using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();

    List<Node> nodes = new List<Node>();

    List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);

        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if(from != null && to != null)
        {
            Edge e = new Edge(from, to);
            edges.Add(e);
            from.edgeList.Add(e);
        }
    }

    Node FindNode(GameObject id)
    {
        foreach(Node n in nodes)
        {
            if (n.getId() == id)
                return n;
        }

        return null;
    }

    public bool AStar(GameObject startID, GameObject endID)
    {
        Node start = FindNode(startID);
        Node end = FindNode(endID);

        if(start == null || end == null)
        {
            return false;
        }

        List<Node> open = new List<Node>();
        List<Node> close = new List<Node>();
        float tentativeGScore = 0;
        bool tentativeIsBetter;

        start.g = 0;
        start.h = Distance(start, end);
        start.f = start.h;

        open.Add(start);
        while(open.Count > 0)
        {
            int i = LowestF(open);
            Node thisNode = open[i];
            if(thisNode.getId() == endID)
            {
                ReconstructPath(start,end);
                return true;
            }

            open.RemoveAt(i);
            close.Add(thisNode);
            Node neighbour;
            foreach(Edge e in thisNode.edgeList)
            {
                neighbour = e.endNode;

                if (close.IndexOf(neighbour) > -1)
                    continue;

                tentativeGScore = thisNode.g + Distance(thisNode, neighbour);
                if (open.IndexOf(neighbour) == -1)
                {
                    open.Add(neighbour);
                    tentativeIsBetter = true;
                }
                else if (tentativeGScore < neighbour.g)
                {
                    tentativeIsBetter = true;
                }
                else
                    tentativeIsBetter = false;

                if (tentativeIsBetter)
                {
                    neighbour.cameFrom = thisNode;
                    neighbour.g = tentativeGScore;
                    neighbour.h = Distance(thisNode, end);
                    neighbour.f = neighbour.g + neighbour.h;
                }
            }
        }

        return false;
    }

    public void ReconstructPath(Node startId, Node endId)
    {
        pathList.Clear();
        pathList.Add(endId);

        var p = endId.cameFrom;
        while (p!= startId && p != null)
        {
            pathList.Insert(0, p);
            p = p.cameFrom;
        }

        pathList.Insert(0, startId);
    }

    float Distance(Node a, Node b)
    {
        return (Vector3.SqrMagnitude(a.getId().transform.position - b.getId().transform.position));
    }

    int LowestF(List<Node> l)
    {
        float lowestf = 0;

        int count = 0;

        int iteratorCount = 0;

        lowestf = l[0].f;

        for(int i = 1; i < l.Count; i++)
        {
            if (l[i].f < lowestf)
            {
                lowestf = l[i].f;
                iteratorCount = count;
            }
            count++;
        }
        return iteratorCount;
    }
}
