using UnityEngine;
using System.Collections;

public class BoxCollider : MonoBehaviour
{
    public GameObject WallClass;
    public bool wall = false;
    public bool turret = false;

    void OnMouseDown()
    {
        if (wall)
        {
            Instantiate(WallClass, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        else if (turret)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = this.transform.position;
            cube.transform.localScale = new Vector3(5, 5, 5);
            var cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.blue);
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

    void Update()
    {
        if (Input.GetKey("a"))
        {
            turret = false;
            wall = true;
        }

        if (Input.GetKey("d"))
        {
            turret = true;
            wall = false;
        }
    }

}
