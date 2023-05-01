using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public float R;
    public float G;
    public float B;

    bool IsDead = false;
    public float TimeAlive = 0;

    float TimeSpawned = 0f;
    float TimeDead = 0f;

    //SpriteRenderer sRenderer;
    //Collider2D sCollider;

    //private void OnMouseDown()
    //{
    //    IsDead = true;
    //    TimeAlive = PopulationManager.elapsed; // Unit alive time
    //    Debug.Log("Died At: " + TimeAlive);
    //    //sRenderer.enabled = false; //Disable rendering of unit (Needs working out for 3d version)
    //    //sCollider.enabled = false; //Disable collisions of unit (Needs working out for 3d version)
    //}

    public void dead()
    {
        TimeDead = Time.deltaTime;
        IsDead = true;
        TimeAlive = Time.timeSinceLevelLoad - TimeSpawned;
        Debug.Log("Died At: " + TimeAlive);
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeSpawned = Time.timeSinceLevelLoad;
        // sRenderer = GetComponent<SpriteRenderer>();
        //sCollider = GetComponent<Collider2D>();
        // sRenderer.color = new Color(R, G, B); // sonehow send this to boids
    }

    // Update is called once per frame
    void Update()
    {

    }
}
