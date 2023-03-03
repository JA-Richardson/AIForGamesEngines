using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    PathGrid grid;

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
        
    }

    void findPath(Vector3 start, Vector3 end)
    {
        PathNode startNode = grid.NodeFromWorldpoint(start);
        PathNode endNode = grid.NodeFromWorldpoint(end);
    }
}
