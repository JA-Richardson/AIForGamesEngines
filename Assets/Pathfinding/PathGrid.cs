using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    public LayerMask pUnwalkableMask;
    public Vector2 pGridWorldSize;
    public float pNodeRadius;
    PathNode[,] pGrid;

    float pNodeDiam;
    int pGridSizeX, pGridSizeY;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(pGridWorldSize.x, 1, pGridWorldSize.y));
        if (pGrid != null)
        {
            foreach (PathNode p in pGrid)
            {
                Gizmos.color = (p.pWalkable) ? Color.white : Color.red;
                Gizmos.DrawCube(p.pWorldPos, Vector3.one * (pNodeDiam - 0.1f));

            }
        }
    }
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
        Vector3 worldBottomLEft = transform.position - Vector3.right * pGridSizeX / 2 - Vector3.forward * pGridSizeY / 2;
        for (int x = 0; x < pGridSizeX; x++)
        {
            for (int y = 0; y < pGridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLEft + Vector3.right * (x * pNodeDiam + pNodeRadius) + Vector3.forward*(y * pNodeDiam + pNodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, pNodeRadius,pUnwalkableMask));
                pGrid[x,y] = new PathNode(walkable, worldPoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
