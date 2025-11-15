using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 4f;

    public float xMin = -8f;
    public float xMax = 8f;
    public float yMin = -4f;
    public float yMax = 4f;

    void Start()
    {
        InvokeRepeating("SpawnCoin", 2f, spawnInterval);
    }

    void SpawnCoin()
    {
        Vector3 pos = new Vector3(
            Random.Range(xMin, xMax),
            Random.Range(yMin, yMax),
            0f
        );

        Instantiate(coinPrefab, pos, Quaternion.identity);
    }
}