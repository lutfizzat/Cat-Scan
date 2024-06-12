using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static Spawner;
using System;
using System.Linq;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    public Spawner spawner; // Reference to the Spawner script
    public UnityEngine.UI.Text scoreUI;
    public LevelChanger levelChanger;

    // Score field
    private int score = 0;
    private int lastMilestoneReached = 0;

    void Start()
    {
    }

    public void DestroyPrefab(string shapeName)
    {
        // Find the SpawnedItemInfo instances with the specified shape name
        List<SpawnedItemInfo> itemsToModify = spawner.spawnedItems.FindAll(item => Array.Exists(item.shapeNames, name => name == shapeName));

        // If itemsToModify are found
        if (itemsToModify.Count > 0)
        {
            foreach (SpawnedItemInfo itemToModify in itemsToModify)
            {
                // Check if the spawnedItem is not null before trying to access it
                if (itemToModify.spawnedItem != null)
                {
                    // Find the shape sprite GameObject with the specified shape name
                    Transform shapeSprite = itemToModify.spawnedItem.transform.Find("ShapeSprite_" + Array.IndexOf(itemToModify.shapeNames, shapeName));

                    if (shapeSprite != null)
                    {
                        // Start the coroutine to shrink and destroy the shape sprite GameObject
                        StartCoroutine(ShrinkAndDestroy(shapeSprite.gameObject, itemToModify));
                    }
                    else
                    {
                        Debug.Log("No shape sprite found with the shape name: " + shapeName);
                    }

                    // Remove the shapeName from the shapeNames array
                    int indexToRemove = Array.IndexOf(itemToModify.shapeNames, shapeName);
                    if (indexToRemove != -1)
                    {
                        itemToModify.shapeNames = itemToModify.shapeNames.Where((name, index) => index != indexToRemove).ToArray();
                    }
                }
            }
        }
        else
        {
            Debug.Log("No spawned item found with the shape name: " + shapeName);
        }
    }

    void Update()
    {
        // Check if all child GameObjects have been destroyed for each SpawnedItemInfo
        foreach (SpawnedItemInfo item in spawner.spawnedItems)
        {
            if (item.shapeNames.Length == 0)
            {
                StartCoroutine(ShrinkAndDestroyParent(item.spawnedItem));
            }
        }
    }

    IEnumerator ShrinkAndDestroy(GameObject obj, SpawnedItemInfo itemToModify)
    {
        // Shrink the GameObject over 1 second
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            if (obj != null)
            {
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, Vector3.zero, t);
                yield return null;
            }
            else
            {
                yield break;
            }
        }

        // Destroy the GameObject
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    IEnumerator ShrinkAndDestroyParent(GameObject obj)
    {
        // Shrink the GameObject over 1 second
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            if (obj != null)
            {
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, Vector3.zero, t);
                yield return null;
            }
            else
            {
                yield break;
            }
        }

        // Destroy the GameObject
        if (obj != null)
        {
            spawner.spawnedItems.RemoveAll(item => item.spawnedItem == obj);
            Destroy(obj);
            AddScore(10);
            UpdateScore();
        }
    }


    

    public void UpdateScore(){
        scoreUI.text = "Score : " + GetScore();
    }

    // Function to add score
    public void AddScore(int points)
    {
        score += points;

        // Check if a new 100 point milestone has been reached
        int currentMilestone = score / 150;
        if (currentMilestone > lastMilestoneReached)
        {
            lastMilestoneReached = currentMilestone;

            // Stop the SpawnItems coroutine
            StopCoroutine(spawner.SpawnItems());

            // Call a different method for each milestone
            switch (currentMilestone)
            {
                case 1:
                    levelChanger.Level2();
                    break;
                case 2:
                    levelChanger.Level3();
                    break;
                case 3:
                    levelChanger.Level4();
                    break;
                case 4:
                    levelChanger.Level5();
                    break;
                case 5:
                    levelChanger.Level6();
                    break;
                case 6:
                    levelChanger.Level7();
                    break;
                case 7:
                    levelChanger.Level8();
                    break;
                case 8:
                    levelChanger.Level9();
                    break;
                default:
                    // Do nothing or perform some default action
                    break;
            }

            // Start the SpawnItems coroutine again after a delay
            StartCoroutine(StartSpawnItemsAfterDelay(1.5f));
        }
    }

    private IEnumerator StartSpawnItemsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(spawner.SpawnItems());
    }

    // Getter for the score
    public int GetScore()
    {
        return score;
    }
}
