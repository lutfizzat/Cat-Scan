using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{

    public UnityEngine.UI.Text score;
    public void Back()
    {
        SceneManager.LoadScene("Start Screen");
    }

    void Start()
    {
        score.text = "Score : " + PlayerPrefs.GetInt("Score", 0);
    }
}

