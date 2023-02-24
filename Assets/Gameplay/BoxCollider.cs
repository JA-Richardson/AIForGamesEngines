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
    public char area;


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

            wallCountscript.turretCount += 1;
        }
        else
        {
            return;
        }
    }

    void Start()
    {
        wallCountscript = PublicVariablesHolder.GetComponent<PublicVariables>();
        
        if(transform.position.x < 0)
        {
            area = 'a';
        }
        else if(transform.position.x > 0)
        {
            area = 'b';
        }
    }

    void ChangeToTurret()
    {
        turret = true;
        wall = false;
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
