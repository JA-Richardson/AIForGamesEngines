using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHealth : MonoBehaviour
{
    public TextMeshProUGUI baseHealthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        baseHealthDisplay.text = GameManager._instance.baseHealth.ToString();
    }
}
