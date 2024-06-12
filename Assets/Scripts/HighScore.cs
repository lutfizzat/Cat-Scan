using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this line
using UnityEngine.UI;
using System.IO;
using System;

public class LeaderboardDisplay : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        // Load the text file from the persistent data path
        string path = Path.Combine(Application.persistentDataPath, "scores.txt");
        string json = File.ReadAllText(path);

        // Remove the type declaration here to avoid shadowing
        ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);

        // Sort the scores in descending order
        scoreList.scores.Sort((a, b) => b.score.CompareTo(a.score));

        Debug.Log(json);

        float templateHeight = 60f;

        // Iterate over the scores, but not more than 8 times
        for (int i = 0; i < Math.Min(scoreList.scores.Count, 8); i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            // Set the score and player name from the JSON data
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = scoreList.scores[i].score.ToString();
            entryTransform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = scoreList.scores[i].playerName;
        }
    }
}


