using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public TextMeshProUGUI CurrencyTMP;
    private string currencytext = "Currency: ";
    public int currency = 10;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild() { return turretToBuild; }

    public GameObject Turret;

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

    public void AddCurrency(int currencyToAdd)
    {
        currency += currencyToAdd;
    }


    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        turretToBuild = Turret;
    }

    // Update is called once per frame
    void Update()
    {
        CurrencyTMP.SetText(currencytext + currency.ToString());
    }
}
