using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{

    public bool pWalkable;
    public Vector3 pWorldPos;

    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public int pGridX;
    public int pGridY;
    public PathNode pParentNode;

    public PathNode(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        pWalkable = walkable;
        pWorldPos = worldPos;
        pGridX = gridX;
        pGridY = gridY;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
