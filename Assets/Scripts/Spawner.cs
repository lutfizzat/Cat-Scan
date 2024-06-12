using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;

public class SpawnedItemInfo
{
    public GameObject spawnedItem;
    public string type;
    public string[] shapeNames;
}

public class Spawner : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> pooledObjects = new Dictionary<string, Queue<GameObject>>(); // Object pool dictionary
    private Dictionary<string, List<GameObject>> prefabDictionary = new Dictionary<string, List<GameObject>>();

    public float minTime = 4f; // Minimum time between spawns
    public float maxTime = 5f;
    public int minNumberItems = 2; // Minimum number of items to spawn
    public int maxNumberItems = 3; // Maximum number of items to spawn
    public int shapeNumber; // Number of shapes to spawn
    public List<SpawnedItemInfo> spawnedItems = new List<SpawnedItemInfo>(); // List to keep track of spawned items and their types
    public Transform[] spawnPoints;
    public float constantScale = 1.0f; // Constant scale for all axes
    private string[] folderPaths = { "UsingAssets/Blue", "UsingAssets/Red", "UsingAssets/Green" };
    private string symbolFolderPath = "SymbolSprite"; // Folder path for symbol sprites


    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        InitializeShapeNumber(sceneName);
        StartCoroutine(SpawnItems());
        LoadPrefabs();
        LoadSymbols(symbolFolderPath); // Load symbols
    }

    private void InitializeShapeNumber(string sceneName)
    {
        if (sceneName == "Tutorial")
        {
            shapeNumber = 6;
        }
        else
        {
            shapeNumber = 2;
        }
    }

    // Rest of the code...

    public void LoadPrefabs()
    {
        foreach (string folderPath in folderPaths)
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>(folderPath);

            foreach (GameObject prefab in prefabs)
            {
                if (!prefabDictionary.ContainsKey(folderPath))
                {
                    prefabDictionary[folderPath] = new List<GameObject>();
                }
                prefabDictionary[folderPath].Add(prefab);
                // Initialize object pool for each prefab
                InitializeObjectPool(prefab, 5); // Adjust pool size as needed
            }
        }
    }

    private void InitializeObjectPool(GameObject prefab, int initialSize)
    {
        string key = prefab.name; // Using prefab name as key for the object pool
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        pooledObjects[key] = objectPool;
    }

    private GameObject GetPooledObject(GameObject prefab)
    {
        string key = prefab.name;
        if (pooledObjects.ContainsKey(key) && pooledObjects[key].Count > 0)
        {
            return pooledObjects[key].Dequeue();
        }
        else
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            return obj;
        }
    }

    private void ReturnPooledObject(GameObject obj)
    {
        string key = obj.name;
        if (pooledObjects.ContainsKey(key))
        {
            obj.SetActive(false);
            pooledObjects[key].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Trying to return an object that doesn't belong to any pool.");
        }
    }

    public IEnumerator SpawnItems()
    {
        while (true)
        {
            float delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);

            int numberOfItemsToSpawn = Random.Range(minNumberItems, maxNumberItems); // Randomly determine the number of items to spawn

            ArrayList availableSpawns = new ArrayList(); // This ArrayList will hold spawn points
            availableSpawns.AddRange(spawnPoints);


            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                string folderPath = folderPaths[Random.Range(0, folderPaths.Length)];
                GameObject[] prefabs = prefabDictionary[folderPath].ToArray();

                // Randomly select a prefab from the available prefabs
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                int randomIndex = Random.Range(0, availableSpawns.Count);
                Transform randomSpawn = (Transform)availableSpawns[randomIndex];//transform da randomspawn into randommspowner

                availableSpawns.RemoveAt(randomIndex); // remove the used spawn
                Vector3 randomPosition = randomSpawn.position;


                // Get the original rotation and scale from the prefab
                Quaternion originalRotation = prefab.transform.rotation;
                Vector3 originalScale = prefab.transform.localScale;

                // Instantiate the prefab with its original rotation and scale
                GameObject spawnedItem = GetPooledObject(prefab);
                spawnedItem.transform.position = randomPosition;
                spawnedItem.transform.rotation = originalRotation;
                spawnedItem.transform.localScale = originalScale * constantScale;
                spawnedItem.SetActive(true);

                // Determine the shape name based on the folder path
                string type = folderPath.Substring(folderPath.LastIndexOf('/') + 1);

                Sprite[] loadedSprites = LoadSymbols(symbolFolderPath);
                string[] shapeNames = DetermineShapeNames(loadedSprites, shapeNumber); // Change 5 to your desired count

                // Add the spawned item to the list
                spawnedItems.Add(new SpawnedItemInfo { spawnedItem = spawnedItem, type = type, shapeNames = shapeNames });



                // Instantiate 2D sprite as a child of the spawned item
                InstantiateShapeSprite(spawnedItem, shapeNames);
            }    
        }    
        
    }

    private GameObject GetPooledObject(object prefab)
    {
        throw new System.NotImplementedException();
    }

    private void InstantiateShapeSprite(GameObject parent, string[] shapeNames)
    {
        // Load the sprites from symbolFolderPath
        Sprite[] sprites = LoadSymbols(symbolFolderPath);

        // Calculate the total width of all sprites
        float totalWidth = 0f;
        foreach (string shapeName in shapeNames)
        {
            Sprite shapeSprite = System.Array.Find(sprites, s => s.name == shapeName);
            if (shapeSprite != null)
            {
                totalWidth += shapeSprite.bounds.size.x;
            }
        }

        // Calculate the center of the prefab
        Vector3 prefabCenter = CalculatePrefabCenter(parent);

        // Calculate the starting position for the first sprite
        float startX = prefabCenter.x - totalWidth / 2f;

        // Iterate through each shape name
        for (int i = 0; i < shapeNames.Length; i++)
        {
    
            Sprite shapeSprite = System.Array.Find(sprites, s => s.name == shapeNames[i]);

            if (shapeSprite != null)
            {
                // Create a new GameObject for the sprite and add a SpriteRenderer component
                GameObject shapeGameObject = new GameObject("ShapeSprite_" + i);

                SpriteRenderer spriteRenderer = shapeGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = shapeSprite;

                // Set the parent of the shape sprite GameObject to the spawned item
                shapeGameObject.transform.SetParent(parent.transform);

                // Calculate the position of the shape sprite
                float spriteWidth = shapeSprite.bounds.size.x;
                float offsetX = startX + spriteWidth / 2f;
                float posY = prefabCenter.y;

                // Set the position of the shape sprite
                shapeGameObject.transform.position = new Vector3(offsetX, posY, -10);

                // Update the startX for the next sprite
                startX += spriteWidth;
            }
            else
            {
                Debug.LogWarning("Sprite not found for shape: " + shapeNames[i]);
            }
        }
    }

    private Vector3 CalculatePrefabCenter(GameObject prefab)
    {
        Renderer renderer = prefab.GetComponent<Renderer>();

        if (renderer != null)
        {
            // Calculate the center based on the bounds of the renderer
            return renderer.bounds.center;
        }
        else
        {
            // If renderer is not found, return the position of the prefab
            return prefab.transform.position;
        }
    }

    private string[] DetermineShapeNames(Sprite[] sprites, int count)
    {
        List<string> shapeNames = new List<string>();

        // Loop to determine the shape names
        for (int i = 0; i < count; i++)
        {
            // Randomly select a sprite from the loaded sprites
            Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

            // Add the name of the selected sprite as the shape name
            shapeNames.Add(randomSprite.name);
        }

        return shapeNames.ToArray();
    }

    public Sprite[] LoadSymbols(string folderPath)
    {
        // Load all sprites from the specified folder path
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);

        // Check if any sprites are found
        if (sprites != null && sprites.Length > 0)
        { }
        else
        {
            Debug.LogWarning("No sprites found in folder path: " + folderPath);
        }

        return sprites;
    }
}




