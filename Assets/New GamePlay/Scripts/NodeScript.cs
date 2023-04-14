using UnityEngine;

public class NodeScript : MonoBehaviour
{
    private Renderer rend;
    private GameObject turret;

    private void OnMouseEnter()
    {
        rend.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        rend.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Cant Build There!");
            return;
        }

        if (GameManager.Instance.currency >= 5)
        {
            GameObject turretToBuild = GameManager.Instance.GetTurretToBuild();
            Quaternion rotateOffset = new Quaternion();
            rotateOffset.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            turret = Instantiate(turretToBuild, transform.position, rotateOffset);
            GameManager.Instance.AddCurrency(-5);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
