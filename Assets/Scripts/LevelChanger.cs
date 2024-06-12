using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Destroyer destroyer;
    public Spawner spawner;
    public Conveyor conveyor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Level2()
    {
        conveyor.speed = 0.5f;
        spawner.minTime = 4f; // Minimum time between spawns
        spawner.maxTime = 5f;
        spawner.minNumberItems = 1; // Minimum number of items to spawn
        spawner.maxNumberItems = 3; // Maximum number of items to spawn
        spawner.shapeNumber = 2; // Number of shapes to spawn
    }
    public void Level3()
    {
        conveyor.speed = 0.75f;
        spawner.minTime = 4f; // Minimum time between spawns
        spawner.maxTime = 5f;
        spawner.minNumberItems = 1; // Minimum number of items to spawn
        spawner.maxNumberItems = 3; // Maximum number of items to spawn
        spawner.shapeNumber = 2; // Number of shapes to spawn
    }
    public void Level4()
    {
        conveyor.speed = 1f;
        spawner.minTime = 4f; // Minimum time between spawns
        spawner.maxTime = 5f;
        spawner.minNumberItems = 1; // Minimum number of items to spawn
        spawner.maxNumberItems = 3; // Maximum number of items to spawn
        spawner.shapeNumber = 2; // Number of shapes to spawn
    }
    public void Level5()
    {
        conveyor.speed = 0.75f;
        spawner.minTime = 3.5f; // Minimum time between spawns
        spawner.maxTime = 5f;
        spawner.minNumberItems = 2; // Minimum number of items to spawn
        spawner.maxNumberItems = 4; // Maximum number of items to spawn
        spawner.shapeNumber = 2; // Number of shapes to spawn
    }
    public void Level6()
    {
        conveyor.speed = 1f;
        spawner.minTime = 5f; // Minimum time between spawns
        spawner.maxTime = 6f;
        spawner.minNumberItems = 2; // Minimum number of items to spawn
        spawner.maxNumberItems = 4; // Maximum number of items to spawn
        spawner.shapeNumber = 2; // Number of shapes to spawn
    }
    public void Level7()
    {
        conveyor.speed = 1f;
        spawner.minTime = 4f; // Minimum time between spawns
        spawner.maxTime = 5f;
        spawner.minNumberItems = 2; // Minimum number of items to spawn
        spawner.maxNumberItems = 4; // Maximum number of items to spawn
        spawner.shapeNumber = 3; // Number of shapes to spawn
    }
    public void Level8()
    {
        conveyor.speed = 1f;
        spawner.minTime = 5f; // Minimum time between spawns
        spawner.maxTime = 6f;
        spawner.minNumberItems = 3; // Minimum number of items to spawn
        spawner.maxNumberItems = 5; // Maximum number of items to spawn
        spawner.shapeNumber = 4; // Number of shapes to spawn
    }
    public void Level9()
    {
        conveyor.speed = 1.1f;
        spawner.minTime = 5f; // Minimum time between spawns
        spawner.maxTime = 6f;
        spawner.minNumberItems = 3; // Minimum number of items to spawn
        spawner.maxNumberItems = 5; // Maximum number of items to spawn
        spawner.shapeNumber = 4; // Number of shapes to spawn
    }
}
