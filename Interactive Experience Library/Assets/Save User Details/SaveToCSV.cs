using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveToCSV : MonoBehaviour
{

    [SerializeField]
    string fileHeader = "userEmail,marketingConsent";

    [SerializeField]
    string filePath = @"c:\UserDataFolder\";

    [SerializeField]
    string fileName = "UserData";

    
    string fileType = ".csv";

    public void Save(string[] row)
    {
        bool writeHeaders = false;

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            writeHeaders = true;
        }

        using (StreamWriter strWriter = new StreamWriter(filePath + fileName + fileType, append: true))
        {
            if (writeHeaders)
            {
                strWriter.WriteLine(fileHeader);
            }
            strWriter.WriteLine(string.Join(", ", row));
        }


    }
}
