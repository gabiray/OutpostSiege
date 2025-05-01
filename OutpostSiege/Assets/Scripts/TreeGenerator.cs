using UnityEngine;

public class RandomTreeSpawner : MonoBehaviour
{
    public GameObject tree01;
    public GameObject tree02;
    public float startDistance = 10f;
    public float stopDistance = 50f;
    public int minTrees = 2;
    public int maxTrees = 5;
    public float treeYPosition = 0f;
    public float minTreeSpacing;
    public float maxTreeSpacing;

    // Funcția Start este apelată la începutul jocului și setează distanța între copaci în funcție de maxTrees
    // După aceea, creează copaci pe ambele părți ale punctului de start
    void Start()
    {
        if (maxTrees <= 20)
        {
            minTreeSpacing = 3f;
            maxTreeSpacing = 6f;
        }
        else
        {
            minTreeSpacing = 2f;
            maxTreeSpacing = 5f;
        }

        // Plasează copacii pe partea dreaptă și pe partea stângă a punctului de start
        SpawnTrees(1);  // Plasare copaci în dreapta
        SpawnTrees(-1); // Plasare copaci în stânga
    }

    // Funcția SpawnTrees generează un număr aleatoriu de copaci între startDistance și stopDistance, în direcția specificată
    // De asemenea, plasează copacii la distanțe aleatorii între ei în intervalul specificat
    void SpawnTrees(int direction)
    {
        float randomDistance = Random.Range(startDistance, stopDistance);
        Vector3 startPosition = transform.position;
        Vector3 finishPosition = startPosition + new Vector3(randomDistance * direction, 0, 0);

        int treeCount = Random.Range(minTrees, maxTrees + 1);
        float currentX = startPosition.x + (startDistance * direction);

        for (int i = 0; i < treeCount; i++)
        {
            float treeSpacing = Random.Range(minTreeSpacing, maxTreeSpacing);
            if (i > 0)
            {
                currentX += treeSpacing * direction;
            }
            SpawnTree(currentX, startPosition.z, i);
        }
    }

    // Funcția SpawnTree instanțiază un copac în poziția (x, y, z) aleatorie
    // Alege aleatoriu între cele două tipuri de copaci (tree01 și tree02)
    void SpawnTree(float x, float z, int index)
    {
        GameObject treePrefab = (Random.value > 0.5f) ? tree01 : tree02;
        if (treePrefab != null)
        {
            float randomZ = Random.Range(z - 5f, z + 5f);
            Vector3 treePosition = new Vector3(x, treeYPosition, randomZ);
            GameObject tree = Instantiate(treePrefab, treePosition, Quaternion.identity);
            tree.name = "Tree" + (index + 1);
        }
    }
}
