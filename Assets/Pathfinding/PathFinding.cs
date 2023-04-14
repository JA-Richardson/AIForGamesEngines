using System;
using System.Collections.Generic;
using UnityEngine;


public class PathFinding : MonoBehaviour
{
    PathGrid grid;
    void Awake()
    {
        grid = GetComponent<PathGrid>();
    }

    public void FindPath(PathReq request, Action<PathResult> callback)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        PathNode startNode = grid.NodeFromWorldPoint(request.pathStart);
        PathNode targetNode = grid.NodeFromWorldPoint(request.pathEnd);
        if (startNode.walkable && targetNode.walkable)
        {
            PathHeap<PathNode> openSet = new(grid.MaxSize);
            HashSet<PathNode> closedSet = new();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                PathNode node = openSet.RemoveFirst();
                closedSet.Add(node);
                if (node == targetNode)
                {
                    pathSuccess = true;
                    break;
                }
                foreach (PathNode neighbour in grid.GetNeighbours(node))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour) + neighbour.movementPenalty;
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
        }

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
            pathSuccess = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints, pathSuccess, request.callback));
    }

    Vector3[] RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    Vector3[] SimplifyPath(List<PathNode> path)
    {
        List<Vector3> waypoints = new();
        Vector2 dirOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 dirNew = new(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (dirNew != dirOld)
            {
                waypoints.Add(path[i].worldPosition);
                dirOld = dirNew;
            }
        }
        return waypoints.ToArray();
    }

    int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

}