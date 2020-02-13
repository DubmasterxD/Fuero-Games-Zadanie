using System.Collections;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] Asteroid asteroidPrefab = null;
    //must be divisible by 2
    [SerializeField] int gridSize = 160;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int asteroidsSpawned = 0;
        for (int i = 1; i < gridSize; i += 2)
        {
            for (int j = 1; j <= i; j += 2)
            {
                Instantiate(asteroidPrefab, new Vector3(i, j, 0), new Quaternion(0, 0, 0, 1), transform);
                Instantiate(asteroidPrefab, new Vector3(-i, j, 0), new Quaternion(0, 0, 0, 1), transform);
                Instantiate(asteroidPrefab, new Vector3(-i, -j, 0), new Quaternion(0, 0, 0, 1), transform);
                Instantiate(asteroidPrefab, new Vector3(i, -j, 0), new Quaternion(0, 0, 0, 1), transform);
                asteroidsSpawned += 4;
                if (asteroidsSpawned % 1000 == 0)
                {
                    yield return null;
                }
                if (j == i)
                {
                    for (int k = i - 2; k > 0; k -= 2)
                    {
                        Instantiate(asteroidPrefab, new Vector3(k, j, 0), new Quaternion(0, 0, 0, 1), transform);
                        Instantiate(asteroidPrefab, new Vector3(-k, j, 0), new Quaternion(0, 0, 0, 1), transform);
                        Instantiate(asteroidPrefab, new Vector3(-k, -j, 0), new Quaternion(0, 0, 0, 1), transform);
                        Instantiate(asteroidPrefab, new Vector3(k, -j, 0), new Quaternion(0, 0, 0, 1), transform);
                        asteroidsSpawned += 4;
                        if (asteroidsSpawned % 1000 == 0)
                        {
                            yield return null;
                        }
                    }
                }
            }
        }
    }
}
