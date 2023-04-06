using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Transform target;
    //public Transform target;

    float speed = 5f;
    Vector3[] path;
    int targetIndex;
    Stopwatch timer = new();

    void Start()
    {
        target = (GameObject.FindGameObjectWithTag("Base")).transform;
        PathManager.ReqPath(transform.position, target.position, OnPathFound);
        timer.Start();
    }

    void Update()
    {

        if (timer.ElapsedMilliseconds > 1000)
        {
            timer.Reset();
            timer.Start();
            PathManager.ReqPath(transform.position, target.position, OnPathFound);
        }
        
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            UnityEngine.Debug.Log("Path Found");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while(true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
        
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
