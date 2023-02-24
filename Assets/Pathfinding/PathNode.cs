using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{

    public bool pWalkable;
    public Vector3 pWorldPos;

    public PathNode(bool walkable, Vector3 worldPos)
    {
        pWalkable = walkable;
        pWorldPos = worldPos;
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
