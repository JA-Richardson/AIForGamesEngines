using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Transform target;
    //public Transform target;

    public float speed = 5f;
    public float turnDist = 5f;
    Path path;
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

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDist);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            UnityEngine.Debug.Log("Path Found");
        }
    }

    IEnumerator FollowPath()
    {
       
        while(true)
        {
            
            yield return null;
        }
        
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
