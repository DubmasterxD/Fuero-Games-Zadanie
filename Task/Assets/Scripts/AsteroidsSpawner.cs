using System.Collections;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] Asteroid asteroidPrefab = null;
    [SerializeField] float asteroidRespawnTime = 1;
    [Tooltip("Has to be divisible by 2")]
    [SerializeField] int gridSize = 160;

    Asteroid[,] asteroids;
    float[,] deathTimers;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                deathTimers[i, j] += deltaTime;
                if (deathTimers[i, j] - deltaTime < asteroidRespawnTime && deathTimers[i, j] >= asteroidRespawnTime)
                {
                    //asteroids[i, j].Spawn();
                }
            }
        }
    }

    private IEnumerator Spawn()
    {
        asteroids = new Asteroid[gridSize, gridSize];
        deathTimers = new float[gridSize, gridSize];
        int asteroidsSpawned = 0;
        for (int i = 1; i < gridSize; i += 2)
        {
            for (int j = 1; j <= i; j += 2)
            {
                asteroids[(gridSize + i - 1) / 2, (gridSize + j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3( i,  j, 0), new Quaternion(0, 0, 0, 1));
                asteroids[(gridSize - i - 1) / 2, (gridSize + j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3(-i,  j, 0), new Quaternion(0, 0, 0, 1));
                asteroids[(gridSize - i - 1) / 2, (gridSize - j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3(-i, -j, 0), new Quaternion(0, 0, 0, 1));
                asteroids[(gridSize + i - 1) / 2, (gridSize - j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3( i, -j, 0), new Quaternion(0, 0, 0, 1));
                asteroidsSpawned += 4;
                if (asteroidsSpawned % 100 == 0)
                {
                    yield return null;
                }
                if (j == i)
                {
                    for (int k = i - 2; k > 0; k -= 2)
                    {
                        asteroids[(gridSize + k - 1) / 2, (gridSize + j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3( k,  j, 0), new Quaternion(0, 0, 0, 1));
                        asteroids[(gridSize - k - 1) / 2, (gridSize + j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3(-k,  j, 0), new Quaternion(0, 0, 0, 1));
                        asteroids[(gridSize - k - 1) / 2, (gridSize - j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3(-k, -j, 0), new Quaternion(0, 0, 0, 1));
                        asteroids[(gridSize + k - 1) / 2, (gridSize - j - 1) / 2] = Instantiate(asteroidPrefab, new Vector3( k, -j, 0), new Quaternion(0, 0, 0, 1));
                        asteroidsSpawned += 4;
                        if (asteroidsSpawned % 100 == 0)
                        {
                            yield return null;
                        }
                    }
                }
            }
        }
    }
}
