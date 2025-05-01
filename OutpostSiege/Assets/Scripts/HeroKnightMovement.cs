using UnityEngine;

public class HeroKnightMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Viteza de mișcare a personajului
    private float horizontalInput; // Input pe axa X (stânga/dreapta)

    void Update()
    {
        // Preia inputul de la tastatură
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Mută personajul pe axa X
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
    }
}
