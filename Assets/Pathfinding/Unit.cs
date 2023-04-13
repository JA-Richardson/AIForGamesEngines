using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minUpdateTime = .2f;
    const float pathUpdateThreshold = .5f;
    Transform target;
    //public Transform target;

    public float speed = 5f;
    public float turnDist = 1f;
    public float turnSpeed = 1f;
    Path path;
    Stopwatch timer = new();
    public float stoppingDist = 5f;

    void Start()
    {
        target = (GameObject.FindGameObjectWithTag("Base")).transform;
        PathManager.ReqPath(new PathReq (transform.position, target.position, OnPathFound));
        timer.Start();
    }

    void Update()
    {

        if (timer.ElapsedMilliseconds > 1000)
        {
            timer.Reset();
            timer.Start();
            PathManager.ReqPath(new PathReq(transform.position, target.position, OnPathFound));
        }

    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDist, stoppingDist);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            UnityEngine.Debug.Log("Path Found");
        }
    }

    IEnumerator FollowPath()
    {
        float speedPercent = 1;
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);
        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].CrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }
            if (followingPath)
            {
                if (pathIndex >= path.slowDownIndex && stoppingDist > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceToEnd(pos2D) / stoppingDist);
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }
                
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward  * Time.deltaTime * speed * speedPercent, Space.Self);
            }
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

    //coroutine for updating path if target moves, not used due to be a tower defense game
    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathManager.ReqPath(new PathReq(transform.position, target.position, OnPathFound));
        float sqrMoveThreshol = pathUpdateThreshold * pathUpdateThreshold;
        Vector3 targetPosOld = target.position;
        while (true)
        {
            yield return new WaitForSeconds(minUpdateTime);
            if ((target.position = targetPosOld).sqrMagnitude > sqrMoveThreshol)
            {
                PathManager.ReqPath(new PathReq(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }
}
