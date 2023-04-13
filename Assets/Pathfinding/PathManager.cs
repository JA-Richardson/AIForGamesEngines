using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PathManager : MonoBehaviour
{
    Queue<PathResult> result = new Queue<PathResult>();
    PathFinding pathfinding;



    static PathManager instance;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<PathFinding>();
    }

    void Update()
    {
        if (result.Count > 0)
        {
            int itemsInQueue = result.Count;
            lock (result)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult pathResult = result.Dequeue();
                    pathResult.callback(pathResult.path, pathResult.success);
                }
            }
        }
    }

    public static void ReqPath(PathReq request)
    {
        ThreadStart startThread = delegate
        {
            instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
        };
        startThread.Invoke();
    }




    public void FinishedProcessingPath(PathResult pathResult)
    {
        
        lock (result)
        {
            instance.result.Enqueue(pathResult);
        }
        

    }

   
}
    public struct PathReq
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathReq(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }

public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }


}

