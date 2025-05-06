using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tree_Interactions : MonoBehaviour
{
    public TextMeshProUGUI interactionText; // Assign in Inspector
    private bool playerInRange = false;

    private void Start()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CutTree();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionText != null)
                interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionText != null)
                interactionText.gameObject.SetActive(false);
        }
    }

    private void CutTree()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}