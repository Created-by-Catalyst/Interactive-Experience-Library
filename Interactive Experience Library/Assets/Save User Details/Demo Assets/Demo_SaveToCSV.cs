using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Demo_SaveToCSV : MonoBehaviour
{

    SaveToCSV saveToCSV;

    private string email;

    [SerializeField] Toggle marketingOptIn;

    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        saveToCSV = gameObject.GetComponent<SaveToCSV>();
    }

    public void SaveInputToCSV()
    {
        string[] row = new string[2];

        row[0] = email;

        row[1] = marketingOptIn.isOn.ToString();

        saveToCSV.Save(row);
    }


}
