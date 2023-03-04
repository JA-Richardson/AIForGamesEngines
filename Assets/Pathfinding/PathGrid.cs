using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    public Transform pPlayer;
    public LayerMask pUnwalkableMask;
    public Vector2 pGridWorldSize;
    public float pNodeRadius;
    PathNode[,] pGrid;

    float pNodeDiam;
    int pGridSizeX, pGridSizeY;
    public List<PathNode> pPath;

    // Start is called before the first frame update
    void Start()
    {
        pNodeDiam = pNodeRadius * 2;
        pGridSizeX = Mathf.RoundToInt(pGridWorldSize.x / pNodeDiam);
        pGridSizeY = Mathf.RoundToInt(pGridWorldSize.y / pNodeDiam);
        CreateGrid();
    }

    void CreateGrid()
    {
        pGrid = new PathNode[pGridSizeX, pGridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * pGridWorldSize.x / 2 - Vector3.forward * pGridWorldSize.y / 2;
        for (int x = 0; x < pGridSizeX; x++)
        {
            for (int y = 0; y < pGridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * pNodeDiam + pNodeRadius) + Vector3.forward * (y * pNodeDiam + pNodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, pNodeRadius, pUnwalkableMask));
                pGrid[x, y] = new PathNode(walkable, worldPoint, x, y);
            }
        }
    }
    public List<PathNode> GetNeighbourNodes(PathNode node)
    {
        List<PathNode> neighbour = new List<PathNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int checkX = node.pGridX + x;
                int checkY = node.pGridY + y;
                if (checkX >= 0 && checkX < pGridSizeX && checkY >= 0 && checkY < pGridSizeY)
                {
                    neighbour.Add(pGrid[checkX, checkY]);
                }
            }
        }
        return neighbour;
    }
    public PathNode NodeFromWorldpoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + pGridWorldSize.x / 2) / pGridWorldSize.x;
        float percentY = (worldPos.z + pGridWorldSize.y / 2) / pGridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((pGridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((pGridSizeY - 1) * percentY);

        return pGrid[x, y];

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(pGridWorldSize.x, 1, pGridWorldSize.y));
        if (pGrid != null)
        {
            PathNode playerNode = NodeFromWorldpoint(pPlayer.position);

            foreach (PathNode p in pGrid)
            {
                Gizmos.color = (p.pWalkable) ? Color.white : Color.red;
                if (pPath != null)
                {
                    //Debug.Log("pPath is not null");
                    if (pPath.Contains(p))
                    {
                        Debug.Log("pPath contains p");
                        Gizmos.color = Color.black;
                    }
                }
                //if (playerNode.pWorldPos == p.pWorldPos)
                //{
                    
                //    Gizmos.color = Color.blue;
                //}
                Gizmos.DrawCube(p.pWorldPos, Vector3.one * (pNodeDiam - 0.1f));

            }
        }
    }

    
  
    // Update is called once per frame
    void Update()
    {

    }

}