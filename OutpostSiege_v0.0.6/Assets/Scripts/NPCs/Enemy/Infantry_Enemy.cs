using UnityEngine;

public class Infantry_Enemy : MonoBehaviour
{
    [SerializeField] private int health = 20;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private Color hitColor = Color.white; // FF8080 will be assigned in Unity

    private SpriteRenderer sr;
    private Color originalColor;
    private Collider2D col;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashWhite());

        if (health <= 0)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        sr.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }

    private void Die()
    {
        if (col != null) col.enabled = false;
        StartCoroutine(FadeAndDestroy());
    }

    private System.Collections.IEnumerator FadeAndDestroy()
    {
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
        Destroy(gameObject);
    }
}
