using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Game Manager Is Null!");
            }
            return _instance;
        }
    }

    public int baseHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        _instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
