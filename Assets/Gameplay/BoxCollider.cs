using UnityEngine;
using System.Collections;

public class BoxCollider : MonoBehaviour
{
    public GameObject WallClass;
    public GameObject TurretClass;

    public GameObject PublicVariablesHolder;
    private PublicVariables wallCountscript;

    public bool wall = false;
    public bool turret = false;


    void OnMouseDown()
    {
        if (wall)
        {
            Instantiate(WallClass, transform.position, transform.rotation);
            Destroy(gameObject);
            wallCountscript.wallCount += 1;
        }
        else if (turret)
        {
            Instantiate(TurretClass, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

    void Start()
    {
        wallCountscript = PublicVariablesHolder.GetComponent<PublicVariables>();
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

        Debug.Log(wallCountscript.wallCount);
    }

}
