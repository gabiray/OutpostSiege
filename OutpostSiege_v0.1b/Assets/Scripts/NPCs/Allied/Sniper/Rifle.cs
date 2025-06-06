using System.Collections;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public Transform firePoint;
    [SerializeField] private int damage = 20;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask raycastLayers;

    public IEnumerator Shoot(Vector2 direction)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, direction, Mathf.Infinity, raycastLayers);

        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            Infantry_Enemy enemy = hitInfo.transform.GetComponent<Infantry_Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + (Vector3)direction * 100);
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}