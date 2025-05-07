using UnityEngine;

public class Main_Base_Generator : MonoBehaviour
{
    [Header("Base Configuration")]
    [SerializeField] private GameObject[] baseLevels; // All base level prefabs (Level 0, Level 1, etc.)
    [SerializeField] private float yPosition = -3f;    // Y position of the base

    private GameObject currentBase;
    private int currentLevel = 0;

    void Start()
    {
        SpawnBaseLevel(currentLevel); // Spawn the first level at the start
    }

    void Update()
    {
        // Check if the player presses space and if the base can be upgraded
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Only show the upgrade UI if the player can upgrade
            if (currentLevel + 1 < baseLevels.Length)
            {
                UpgradeBase();
            }
        }
    }

    private void SpawnBaseLevel(int level)
    {
        if (level >= 0 && level < baseLevels.Length)
        {
            if (currentBase != null)
            {
                Destroy(currentBase);
            }

            Vector3 spawnPosition = new Vector3(0f, yPosition, 0f);
            currentBase = Instantiate(baseLevels[level], spawnPosition, Quaternion.identity, transform);

            // Optional: Add the upgrade script and configure it
            /*
            UpgradeMainBaseGenerator upgradeScript = currentBase.AddComponent<UpgradeMainBaseGenerator>();
            upgradeScript.mainBase = this;
            upgradeScript.player = GameObject.FindWithTag("Player");
            upgradeScript.pressSpaceUI = GameObject.Find("Canvas/PressSpaceUI");

            if (upgradeScript.player == null)
                Debug.LogWarning("Player not found with tag 'Player'.");

            if (upgradeScript.pressSpaceUI == null)
                Debug.LogWarning("PressSpaceUI not found in the scene.");
            */
        }
        else
        {
            Debug.LogWarning("Invalid level index or no more base upgrades.");
        }
    }

    public void UpgradeBase()
    {
        if (currentLevel + 1 < baseLevels.Length)
        {
            currentLevel++;
            SpawnBaseLevel(currentLevel);
            Debug.Log("Base upgraded to level " + currentLevel);
        }
        else
        {
            Debug.Log("Base is already at the maximum level.");
        }
    }
}
