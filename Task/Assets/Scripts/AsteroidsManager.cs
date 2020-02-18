using System.Collections;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    [SerializeField] Asteroid asteroidPrefab = null;
    [SerializeField] float asteroidRespawnTime = 1;
    [Tooltip("Has to be divisible by 2")]
    [SerializeField] int spawnPerFrame = 1000;
    public int gridSize = 160;

    Asteroid[,] asteroids;
    Transform[,] transforms;
    float[,] deathTimers;
    bool areAllCreated = false;

    GameObject player;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager.onGameStart += RestartAsteroids;
    }

    private void Start()
    {
        StartCoroutine(CreateAsteroids());
    }

    private void Update()
    {
        if (areAllCreated)
        {
            Vector3 playerPosition = player.transform.position;
            float deltaTime = Time.deltaTime;
            float boundsRadius = gridSize * Mathf.Sqrt(2);
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    deathTimers[i, j] += deltaTime;
                    if (deathTimers[i, j] - deltaTime < asteroidRespawnTime && deathTimers[i, j] >= asteroidRespawnTime)
                    {
                        //asteroids[i, j].Spawn();
                    }
                    if (Vector3.Distance(transforms[i, j].position, playerPosition) >= boundsRadius)
                    {
                        transforms[i, j].position = transforms[i, j].position + transforms[i, j].position - playerPosition;
                    }
                }
            }
        }
    }

    private IEnumerator CreateAsteroids()
    {
        asteroids = new Asteroid[gridSize, gridSize];
        deathTimers = new float[gridSize, gridSize];
        transforms = new Transform[gridSize, gridSize];
        int asteroidsCreated = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                asteroids[i, j] = Instantiate(asteroidPrefab);
                asteroids[i, j].gameObject.SetActive(false);
                deathTimers[i, j] = float.MinValue;
                transforms[i, j] = asteroids[i, j].transform;
                asteroidsCreated++;
                if (asteroidsCreated % spawnPerFrame == 0)
                {
                    yield return null;
                }
            }
        }
        gameManager.StartGame();
        areAllCreated = true;
        RestartAsteroids();
    }

    private void RestartAsteroids()
    {
        StopAllCoroutines();
        HideAllAsteroids();
        StartCoroutine(SpawnAllAsteroids());
    }

    private void HideAllAsteroids()
    {
        for (int i = 0; i< gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++)
            {
                asteroids[i, j].gameObject.SetActive(false);
                deathTimers[i, j] = float.MinValue;
            }
        }
    }

    private IEnumerator SpawnAllAsteroids()
    {
        int asteroidsSpawned = 0;
        for (int i = 1; i < gridSize; i += 2)
        {
            for (int j = 1; j <= i; j += 2)
            {
                Spawn4MirroredAsteroidsAt(i, j);
                asteroidsSpawned += 4;
                if (asteroidsSpawned % spawnPerFrame == 0)
                {
                    yield return null;
                }
                if (j == i)
                {
                    for (int k = i - 2; k > 0; k -= 2)
                    {
                        Spawn4MirroredAsteroidsAt(k, j);
                        asteroidsSpawned += 4;
                        if (asteroidsSpawned % spawnPerFrame == 0)
                        {
                            yield return null;
                        }
                    }
                }
            }
        }
    }

    private void Spawn4MirroredAsteroidsAt(int i, int j)
    {
        SpawnAsteroidAt(i, j);
        SpawnAsteroidAt(-i, j);
        SpawnAsteroidAt(-i, -j);
        SpawnAsteroidAt(i, -j);
    }

    private void SpawnAsteroidAt(int i, int j)
    {
        asteroids[(gridSize + i - 1) / 2, (gridSize + j - 1) / 2].SpawnAt(player.transform.position.x + i, player.transform.position.y + j);
    }
}
