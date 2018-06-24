using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator Instance = null;

    public enum GameState { Playing, NotPlaying };
    private GameState gameState = GameState.Playing;

    public float startSpeed = 5;
    public int startDifficult = 1;
    public int startMaxConsecutiveObstacles = 1;
    public int startMaxConsecutivePoints = 3;
    public float startTimeToSpawnNext = 2;
    public float speed = 5;
    private int difficult = 1;
    private int maxConsecutiveObstacles = 1;
    private int maxConsecutivePoints = 3;
    private int spawnedObstalces = 0;
    private int spawnedPoints = 0;
    private float timeToSpawnNext = 2;
    private int spawnedObjects = 0;

    private float lastSpawnTime;

    /*move these events to a game events script*/
    public delegate void LevelStartAction();
    public static event LevelStartAction Retry;

    public delegate void LevelEndAction();
    public static event LevelEndAction EndLevel;

    public float[] positions = { -1.7f, 0, 1.7f };

    public enum SpawnableObjects {Obstacle, Point};

    public List<GameObject> obstaclePrefabs;
    public List<GameObject> pointsPrefabs;

    public static void EndLevelFirer()
    {
        Time.timeScale = 0;
        Instance.SetGameState(GameState.NotPlaying);
        if (EndLevel != null) EndLevel();
    }

    public static void RetryLevelFirer()
    {
        Time.timeScale = 1;
        Instance.SetGameState(GameState.Playing);
        Instance.RestartLevel();
        if (Retry != null) Retry();
    }

    // Use this for initialization
    void Awake () {
		if(Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 1;
        lastSpawnTime = Time.time;
	}
	
    public void SetGameState(GameState state)
    {
        gameState = state;
    }

	// Update is called once per frame
	void Update () {
        if(gameState == GameState.Playing)
        {
            if (Time.time - lastSpawnTime >= timeToSpawnNext)
            {
                lastSpawnTime = Time.time;
                timeToSpawnNext = Mathf.Max(0.8f, timeToSpawnNext - 0.05f);
                Spawn();
            }
        }
	}

    private void Spawn()
    {
        SpawnableObjects objectType = SpawnableObjects.Point;
        int rand = Random.Range(0, 2);
   
        spawnedObjects++;
        if(spawnedObjects > 10)
        {
            spawnedObjects = 0;
            speed++;
            maxConsecutiveObstacles++;
        }

        if(rand == 0)
        {
            if(spawnedObstalces > maxConsecutiveObstacles)
            {
                spawnedObstalces = 0;
                spawnedPoints++;
                objectType = SpawnableObjects.Point;
            } else {
                objectType = SpawnableObjects.Obstacle;
                spawnedObstalces++;
            }
        } else {
            if (spawnedPoints > maxConsecutivePoints)
            {
                spawnedPoints = 0;
                spawnedObstalces++;
                objectType = SpawnableObjects.Obstacle;
            }
            else
            {
                objectType = SpawnableObjects.Point;
                spawnedPoints++;
            }
        }

        if(objectType == SpawnableObjects.Obstacle)
        {
            int spwn = 0;//Random.Range(0, difficult);
            Instantiate(obstaclePrefabs[spwn]);
        } else {
            Instantiate(pointsPrefabs[0]);
        }
    }

    public void RiseSpeed()
    {
        speed++;
    }

    public void RestartLevel()
    {
        speed = startSpeed;
        difficult = startDifficult;
        maxConsecutiveObstacles = startMaxConsecutiveObstacles;
        maxConsecutivePoints = startMaxConsecutivePoints;
        spawnedObstalces = 0;
        spawnedPoints = 0;
        timeToSpawnNext = startTimeToSpawnNext;
        spawnedObjects = 0;
    }
}
