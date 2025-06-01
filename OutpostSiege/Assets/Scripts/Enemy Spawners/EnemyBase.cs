using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health = 100;
    private Enemy_Spawn_Manager spawnManager;

    private void Start()
    {
        spawnManager = FindFirstObjectByType<Enemy_Spawn_Manager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            spawnManager.NotifyBaseDestroyed(gameObject);
            Destroy(transform.parent.gameObject); // Destroys the full base (parent of collider)
        }
    }
}
