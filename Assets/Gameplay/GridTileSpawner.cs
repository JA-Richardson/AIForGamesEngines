using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileSpawner : MonoBehaviour
{

    public GameObject TileClass;
    public GameObject WallClass;
    private Vector3 TilePosition = new Vector3(-95,0,-95);

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < 20; j++)
        {
            for (int i = 0; i < 20; i++)
            {
                if (i <= 6 || i >= 13 || j <= 6 || j >= 13)
                {
                    
                    if (i == 0 || i == 19 || j == 0 || j == 19)
                    {
                        Instantiate(WallClass, TilePosition, transform.rotation);
                    }
                    else
                    {
                        Instantiate(TileClass, TilePosition, transform.rotation);
                    }
                }
               TilePosition.z  = TilePosition.z + 10;
            }
            TilePosition.z = -95;
            TilePosition.x = TilePosition. x + 10;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
