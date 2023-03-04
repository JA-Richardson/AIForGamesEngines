using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    PathGrid grid;

    public Transform seeker, target;

    private void Awake()
    {
        grid = GetComponent<PathGrid>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        findPath(seeker.position, target.position);
    }

    void findPath(Vector3 start, Vector3 end)
    {
        PathNode startNode = grid.NodeFromWorldpoint(start);
        PathNode endNode = grid.NodeFromWorldpoint(end);

        List<PathNode> openNode = new List<PathNode>();
        HashSet<PathNode> closedNode = new HashSet<PathNode>();
        openNode.Add(startNode);

        while(openNode.Count > 0)
        {
            PathNode currentNode = openNode[0];
            for (int i = 1; i < openNode.Count; i++)
            {
                if (openNode[i].fCost < currentNode.fCost || openNode[i].fCost == currentNode.fCost && openNode[i].hCost < currentNode.hCost)
                {
                    currentNode = openNode[i];
                }
            }

            openNode.Remove(currentNode);
            closedNode.Add(currentNode);

            if (currentNode == endNode)
            {
                RetracePath(startNode, endNode);
            }

            foreach (PathNode neighbour in grid.GetNeighbourNodes(currentNode))
            {
                if (!neighbour.pWalkable || closedNode.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistanceBetweenNodes(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openNode.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistanceBetweenNodes(neighbour, endNode);
                    neighbour.pParentNode = currentNode;

                    if (!openNode.Contains(neighbour))
                    {
                        openNode.Add(neighbour);
                    }
                }
            }
        }
    }
    
    void RetracePath(PathNode start, PathNode end)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.pParentNode;
        }
        path.Reverse();

        grid.pPath = path;
    }

    int GetDistanceBetweenNodes(PathNode nodeA, PathNode nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.pGridX - nodeB.pGridX);
        int distanceY = Mathf.Abs(nodeA.pGridY - nodeB.pGridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
