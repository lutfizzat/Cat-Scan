using System;
using System.IO;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed; // Speed of the conveyor belt
    public Spawner spawner; // Reference to the Spawner script
    public EndGameManager end;
    public Destroyer destroyer;
    public SpriteRenderer[] healthSprites; // The sprites that represent the player's health

    private ScoreList scoreList;

    void Start()
    {
        // Initialize health points
        PlayerPrefs.SetInt("Health", healthSprites.Length);
    }

    void Awake()
    {
        // Load the text file from the persistent data path
        string path = Path.Combine(Application.persistentDataPath, "scores.txt");

        // Check if the file exists before reading it
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            // Remove the type declaration here to avoid shadowing
            scoreList = JsonUtility.FromJson<ScoreList>(json);

            // Sort the scores in descending order
            scoreList.scores.Sort((a, b) => b.score.CompareTo(a.score));
        }
        else
        {
            Debug.Log("File does not exist.");
        }
    }


    void Update()
    {
        // Iterate over each spawned item
        for (int i = spawner.spawnedItems.Count - 1; i >= 0; i--)
        {
            GameObject item = spawner.spawnedItems[i].spawnedItem; // Access the spawnedItem field

            // Move the item along the x-axis
            item.transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);

            // If the item is out of bounds
            if (!GetComponent<Collider>().bounds.Contains(item.transform.position))
            {
                // Remove the item from the list first
                spawner.spawnedItems.RemoveAt(i);

                // Destroy the item afterward
                Destroy(item);

                // Decrease health points
                int health = PlayerPrefs.GetInt("Health");
                health--;
                PlayerPrefs.SetInt("Health", health);

                Handheld.Vibrate();

                // Disable the corresponding health sprite
                if (health >= 0 && health < healthSprites.Length)
                {
                    healthSprites[health].enabled = false;
                }

                // Check if health points have reached 0
                if (health <= 0)
                {
                    PlayerPrefs.SetInt("Score", destroyer.GetScore());

                    int playerScore = destroyer.GetScore();

                    end.EndGame();

                    // Check if the player's score is higher than the highest score in the list
                    // or if the scoreList has less than 8 scores
                    if (scoreList == null || scoreList.scores.Count == 0 || playerScore > scoreList.scores[Math.Min(scoreList.scores.Count, 8) - 1].score)
                    {
                        // Load the scene where the player can enter their initials
                        UnityEngine.SceneManagement.SceneManager.LoadScene("EnterHighScore");
                    }
                    else
                    {
                        // Load the scene where the leaderboard is displayed
                        end.EndGame();
                    }

                }
            }
        }
    }
}

