using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [Header("Tree Prefabs")]
    [SerializeField] private GameObject[] treePrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float minDistance = 4f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private int xLimit = 50;

    private void Start()
    {
        SpawnTrees();
    }

    private void SpawnTrees()
    {
        float xPos = -xLimit;

        while (xPos <= xLimit)
        {
            // Pick a random tree
            GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

            // Instantiate tree
            Vector3 position = new Vector3(xPos, yOffset, 0f);
            Instantiate(treePrefab, position, Quaternion.identity, transform);

            // Advance xPos by a random distance
            float step = Random.Range(minDistance, maxDistance);
            xPos += step;
        }
    }
}
