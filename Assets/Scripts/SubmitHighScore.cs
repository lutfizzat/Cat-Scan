using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class SubmitHighScore : MonoBehaviour
{

    public UnityEngine.UI.Text score;
    public TMP_InputField mainInputField;


    public void Submit()
    {
        string playerName = mainInputField.text;
        int score = PlayerPrefs.GetInt("Score", 0);

        if (playerName != "")
        {
            // Create a new player score
            PlayerScore newScore = new PlayerScore();
            newScore.playerName = playerName;
            newScore.score = score;

            // Load the existing scores
            string path = Path.Combine(Application.persistentDataPath, "scores.txt");
            string json = File.ReadAllText(path);
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);

            // Add the new score to the list
            scoreList.scores.Add(newScore);

            // Sort the list
            scoreList.scores.Sort((x, y) => y.score.CompareTo(x.score));

            // Save the scores
            string newJson = JsonUtility.ToJson(scoreList, true); // Format the JSON string with indents
            PlayerPrefs.SetString("HighScores", newJson);
            PlayerPrefs.Save();

            // Write the scores back to the text file in the persistent data path
            File.WriteAllText(path, newJson);

            Debug.Log("Score submitted! HERES THE RESUKLTS");
            Debug.Log(newJson);

            // Go back to the main menu
            UnityEngine.SceneManagement.SceneManager.LoadScene("Leaderboard");
        }
    }



    void Start()
    {
        score.text = "Score : " + PlayerPrefs.GetInt("Score", 0);
    }
}
