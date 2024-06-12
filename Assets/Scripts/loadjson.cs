using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class loadjson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Define the path in the persistent data path
        string path = Path.Combine(Application.persistentDataPath, "scores.txt");

        // Check if the file exists
        if (!File.Exists(path))
        {
            // Load the text file from the Resources folder
            TextAsset file = Resources.Load("scores") as TextAsset;
            string content = file.ToString();

            // Write the content to the new location
            File.WriteAllText(path, content);
        }
        Debug.Log("hi mom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


