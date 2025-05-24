using UnityEngine;

public class Wall_Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer sr; // Assign from Inspector
    [SerializeField] private Color hitColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private float fadeDuration = 0.5f;

    private Collider2D col;

    private void Awake()
    {
        currentHealth = maxHealth;
        col = GetComponent<Collider2D>();

        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashWhite());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (col != null) col.enabled = false;
        StartCoroutine(FadeAndDestroy());
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        if (sr == null) yield break;

        Color originalColor = sr.color;
        sr.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }

    private System.Collections.IEnumerator FadeAndDestroy()
    {
        if (sr == null) yield break;

        float timer = 0f;
        Color startColor = sr.color;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            sr.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, t));
            timer += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        Destroy(transform.parent.gameObject); // destroy the entire wall
    }
}
