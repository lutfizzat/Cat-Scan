using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLeaderboard : MonoBehaviour
{
    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
