using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject Ground_Dirt;
    public GameObject Ground_Grass_Left;
    public GameObject Ground_Grass_Right;
    public GameObject Ground_Grass_Between;
    public int groundLength = 10;
    public float tileSize = 1.0f;
    public float groundHeight = -2f; // Poziția pe axa Y

    [Range(0.1f, 4f)] public float colliderScale = 1.0f; // Valoare ajustabilă pentru dimensiunea coliderului

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        GenerateSide(-1); // Generare la stânga
        GenerateSide(1);  // Generare la dreapta
    }

    void GenerateSide(int direction)
    {
        for (int i = 0; i < groundLength;)
        {
            float xPos = i * tileSize * direction;

            // Probabilitate de a pune grupul Grass sau doar Dirt
            if (i <= groundLength - 3 && Random.value > 0.5f)
            {
                GameObject grassLeft = Instantiate(Ground_Grass_Left, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(grassLeft);

                xPos += tileSize * direction;

                int grassBetweenCount = Random.Range(2, 7);
                for (int j = 0; j < grassBetweenCount; j++)
                {
                    GameObject grassBetween = Instantiate(Ground_Grass_Between, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(grassBetween);
                    xPos += tileSize * direction;
                }

                GameObject grassRight = Instantiate(Ground_Grass_Right, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(grassRight);
                xPos += tileSize * direction;

                i += (2 + grassBetweenCount);

                int dirtCount = Random.Range(1, 3);
                for (int j = 0; j < dirtCount && i < groundLength; j++)
                {
                    GameObject dirt = Instantiate(Ground_Dirt, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(dirt);
                    xPos += tileSize * direction;
                    i++;
                }
            }
            else
            {
                GameObject dirt = Instantiate(Ground_Dirt, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(dirt);
                i++;
            }
        }
    }

    void AddBoxCollider(GameObject obj)
    {
        BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = obj.AddComponent<BoxCollider2D>();
        }

        // Ajustează dimensiunea coliderului în funcție de colliderScale
        collider.size = new Vector2(1.0f * colliderScale, 1.0f * colliderScale);
    }
}
