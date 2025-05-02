using Unity.VisualScripting;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // Prefaburi pentru diferitele tipuri de tile-uri de teren
    public GameObject Ground_Dirt;
    public GameObject Ground_Grass_Left;
    public GameObject Ground_Grass_Right;
    public GameObject Ground_Grass_Between;

    // Numărul de tile-uri care vor fi generate într-o direcție
    public int groundLength = 10;

    // Dimensiunea fiecărui tile pe axa X
    public float tileSize = 1.0f;

    // Înălțimea la care va fi plasat terenul (axa Y)
    public float groundHeight = -2f;

    // Permite reglarea dimensiunii coliderului din Inspector
    [Range(0.1f, 4f)] public float colliderScale = 1.0f;

    void Start()
    {
        // Pornește generarea terenului când jocul începe
        GenerateGroundRight();
        GenerateGroundLeft();
    }

    // Funcția pentru generarea terenului spre dreapta
    void GenerateGroundRight()
    {
        // Iterăm prin numărul de tile-uri specificat
        for (int i = 0; i < groundLength;)
        {
            float xPos = i * tileSize;

            // Cu o anumită probabilitate, adăugăm un grup de iarbă
            if (i <= groundLength - 3 && Random.value > 0.5f)
            {
                bool ok = false;

                // Plasează tile-ul de început Grass_Left
                GameObject grassLeft = Instantiate(Ground_Grass_Left, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(grassLeft);

                xPos += tileSize;

                // Alege un număr aleator de tile-uri Grass_Between
                int grassBetweenCount = Random.Range(2, 7);
                for (int j = 0; j < grassBetweenCount; j++)
                {
                    GameObject grassBetween = Instantiate(Ground_Grass_Between, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(grassBetween);
                    xPos += tileSize;
                    ok = true;
                }

                // Plasează tile-ul de final Grass_Right
                if (ok)
                {
                    GameObject grassRight = Instantiate(Ground_Grass_Right, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(grassRight);
                    xPos += tileSize;
                }

                // Creștem indexul în funcție de câte tile-uri am adăugat
                i += (2 + grassBetweenCount);

                // Adaugă câteva tile-uri Dirt între grupuri de iarbă
                int dirtCount = Random.Range(1, 3);
                for (int j = 0; j < dirtCount && i < groundLength; j++)
                {
                    GameObject dirt = Instantiate(Ground_Dirt, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(dirt);
                    xPos += tileSize;
                    i++;
                }
            }
            else
            {
                // Dacă nu s-a generat grup de iarbă, adaugă un simplu tile Dirt
                GameObject dirt = Instantiate(Ground_Dirt, new Vector3(xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(dirt);
                i++;
            }
        }
    }

    // Funcția pentru generarea terenului spre stânga
    //se inverseaza stanga cu dreapta deoarece e in oglinda si daca nu se inverseaza se barmbureste tot
    void GenerateGroundLeft()
    {
        // Iterăm prin numărul de tile-uri specificat
        for (int i = 0; i < groundLength;)
        {
            float xPos = i * tileSize;

            // Cu o anumită probabilitate, adăugăm un grup de iarbă
            if (i <= groundLength - 3 && Random.value > 0.5f)
            {
                bool ok = false;

                // Plasează tile-ul de început Grass_right
                
                GameObject grassRight = Instantiate(Ground_Grass_Right, new Vector3(-xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(grassRight);

                xPos += tileSize;

                // Alege un număr aleator de tile-uri Grass_Between
                int grassBetweenCount = Random.Range(2, 7);
                for (int j = 0; j < grassBetweenCount; j++)
                {
                    GameObject grassBetween = Instantiate(Ground_Grass_Between, new Vector3(-xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(grassBetween);
                    xPos += tileSize;
                    ok = true;
                }

                // Plasează tile-ul de final Grass_left
                if (ok)
                {
                    GameObject grassLeft = Instantiate(Ground_Grass_Left, new Vector3(-xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(grassLeft);
                    xPos += tileSize;
                }

                // Creștem indexul în funcție de câte tile-uri am adăugat
                i += (2 + grassBetweenCount);

                // Adaugă câteva tile-uri Dirt între grupuri de iarbă
                int dirtCount = Random.Range(1, 3);
                for (int j = 0; j < dirtCount && i < groundLength; j++)
                {
                    GameObject dirt = Instantiate(Ground_Dirt, new Vector3(-xPos, groundHeight, 0), Quaternion.identity, transform);
                    AddBoxCollider(dirt);
                    xPos += tileSize;
                    i++;
                }
            }
            else
            {
                // Dacă nu s-a generat grup de iarbă, adaugă un simplu tile Dirt
                GameObject dirt = Instantiate(Ground_Dirt, new Vector3(-xPos, groundHeight, 0), Quaternion.identity, transform);
                AddBoxCollider(dirt);
                i++;
            }
        }
    }

    void AddBoxCollider(GameObject obj)
    {
        // Verifică dacă obiectul are deja un BoxCollider2D, dacă nu, adaugă unul
        BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = obj.AddComponent<BoxCollider2D>();
        }

        // Ajustează dimensiunea coliderului în funcție de valoarea setată în Inspector
        collider.size = new Vector2(1.0f * colliderScale, 1.0f * colliderScale);
    }
}
