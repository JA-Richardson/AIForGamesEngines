using TMPro;
using UnityEngine;

public class TurretText : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public GameObject PublicVariablesHolder;
    private PublicVariables wallCountscript;

    void Start()
    {
        wallCountscript = PublicVariablesHolder.GetComponent<PublicVariables>();
    }

    void Update()
    {
        textDisplay.text = wallCountscript.turretCount.ToString();
    }
}