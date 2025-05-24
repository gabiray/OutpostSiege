using System.Collections.Generic;
using UnityEngine;

public class TowerWalls_Generation : MonoBehaviour
{
    [Header("Towers and Walls")]
    [SerializeField] private List<GameObject> towerPrefabs;  // Lista pentru towers/outposts
    [SerializeField] private List<GameObject> wallPrefabs;   // Lista pentru walls
    public List<GameObject> TowerPrefabs => towerPrefabs;

    public List<GameObject> WallPrefabs => wallPrefabs; // Getter public

    [Header("Distances")]
    [SerializeField] private float startDistance = 15f;
    [SerializeField] private float endDistance = 70f;

    [Header("Random distance between prefabs (min/max)")]
    [SerializeField] private float minDistance = 8f;
    [SerializeField] private float maxDistance = 12f;

    [Header("OY coordinates")]
    [SerializeField] private float towerYPosition = -1.2f;
    [SerializeField] private float wallYPosition = -2.720f;

    void Start()
    {
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        if (towerPrefabs.Count == 0 || wallPrefabs.Count == 0)
        {
            Debug.LogError("List is empty: Please assign at least one tower and one wall prefab.");
            return;
        }

        PlacePrefabsInDirection(1);

        PlacePrefabsInDirection(-1);
    }

    private void PlacePrefabsInDirection(int direction)
    {
        float currentPosition = (direction > 0) ? startDistance : -startDistance;
        float endPos = (direction > 0) ? endDistance : -endDistance;

        bool placeTowerNext = true; 

        while ((direction > 0 && currentPosition <= endPos) || (direction < 0 && currentPosition >= endPos))
        {
            GameObject prefabToPlace;
            float yPos;

            if (placeTowerNext)
            {
                prefabToPlace = towerPrefabs[0];  // Doar primul tower
                yPos = towerYPosition;
            }
            else
            {
                prefabToPlace = wallPrefabs[0];   // Doar primul wall
                yPos = wallYPosition;
            }

            Instantiate(prefabToPlace, new Vector3(currentPosition, yPos, 0f), Quaternion.identity, transform);

            currentPosition += Random.Range(minDistance, maxDistance) * direction;
            placeTowerNext = !placeTowerNext;
        }
    }
}
