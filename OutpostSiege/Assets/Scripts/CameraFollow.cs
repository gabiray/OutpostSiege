using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Obiectul urmărit
    public float smoothSpeed = 5f; // Viteza de urmărire
    public Vector3 offset = new Vector3(5, 0, -10); // Poziția camerei față de obiect

    void LateUpdate()
    {
        if (target != null)
        {
            // Păstrăm Y și Z inițiale, dar X se actualizează după target
            Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
