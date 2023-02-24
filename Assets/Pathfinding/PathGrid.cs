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
                pGrid[x, y] = new PathNode(walkable, worldPoint);
            }
        }
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
                if(playerNode.pWorldPos == p.pWorldPos) 
                {
                    print("x1 : " + playerNode.pWorldPos.x + " y1 : " + playerNode.pWorldPos.y);
                    print("x2 : " + p.pWorldPos.x + " y2 : " + p.pWorldPos.y);
                    Gizmos.color = Color.blue; 
                }
                Gizmos.DrawCube(p.pWorldPos, Vector3.one * (pNodeDiam - 0.1f));

            }
        }
    }

    
    

    // Update is called once per frame
    void Update()
    {

    }
}
