using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    Queue<PathReq> pathReqQueue = new Queue<PathReq>();
    PathReq currentPathReq;
    PathFinding pathfinding;

    bool isProcessingPath;

    static PathManager instance;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<PathFinding>();
    }

    public static void ReqPath(Vector3 pathstart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathReq newReq = new PathReq(pathstart, pathEnd, callback);
        instance.pathReqQueue.Enqueue(newReq);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathReqQueue.Count > 0)
        {
            currentPathReq = pathReqQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathReq.pathStart, currentPathReq.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathReq.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathReq
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
}
