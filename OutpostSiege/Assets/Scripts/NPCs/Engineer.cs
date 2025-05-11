using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Engineer : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stopDistance = 0.2f;

    [Header("Tree Cutting")]
    [SerializeField] private float cutDuration = 2f; // configurable in Inspector
    [SerializeField] private int minCoins = 1;
    [SerializeField] private int maxCoins = 2;

    private Vector3 basePosition;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private readonly Queue<(GameObject tree, Action<GameObject> callback)> taskQueue = new();

    private void Start()
    {
        basePosition = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.SetBool("running", false);
        animator.SetBool("engineering", false);

        StartCoroutine(HandleQueue());

        // Check for pending tasks
        if (Tree_Task_Manager.Instance.TryGetTask(out var task))
        {
            RequestTreeCut(task.Item1, task.Item2);
        }
    }

    public void RequestTreeCut(GameObject tree, Action<GameObject> onTreeCut)
    {
        if (!IsTreeAlreadyQueued(tree))
        {
            // Enqueue the tree cutting task
            taskQueue.Enqueue((tree, onTreeCut));
            Debug.Log($"Tree added to queue: {tree.name}");

            // Update the tree's visual state to indicate it has been paid for
            tree.GetComponent<Tree_Interactions>()?.ForcePaidVisual();
        }
        else
        {
            Debug.Log($"Tree {tree.name} is already in the queue.");
        }
    }

    private bool IsTreeAlreadyQueued(GameObject tree)
    {
        foreach (var item in taskQueue)
        {
            if (item.tree == tree) return true;
        }
        return false;
    }

    private IEnumerator HandleQueue()
    {
        while (true)
        {
            if (taskQueue.Count == 0)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            while (taskQueue.Count > 0)
            {
                var (tree, callback) = taskQueue.Dequeue();

                if (tree == null) continue;

                // Move to tree and wait until we touch its collider
                yield return MoveTo(tree);

                // Start engineering animation
                animator.SetBool("engineering", true);

                yield return new WaitForSeconds(cutDuration);

                animator.SetBool("engineering", false);

                // Spawn coins before destroying the tree
                int coins = UnityEngine.Random.Range(minCoins, maxCoins + 1);
                callback?.Invoke(tree); // let Player_Interactions handle coin adding

                Destroy(tree);
            }

            // Move back to base position
            yield return MoveTo(basePosition);
            animator.SetBool("running", false);
        }
    }

    private IEnumerator MoveTo(Vector3 targetPos)
    {
        animator.SetBool("running", true);
        FlipToFace(targetPos.x);

        // Keep Y and Z the same as current to prevent vertical movement
        Vector3 targetFlat = new Vector3(targetPos.x, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, targetFlat) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFlat, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("running", false);
    }

    private IEnumerator MoveTo(GameObject targetObject)
    {
        Collider2D targetCollider = targetObject.GetComponent<Collider2D>();
        Collider2D myCollider = GetComponent<Collider2D>();

        if (targetCollider == null || myCollider == null)
        {
            Debug.LogWarning("Missing colliders on Engineer or Tree.");
            yield break;
        }

        animator.SetBool("running", true);
        FlipToFace(targetObject.transform.position.x);

        // Move until colliders overlap
        while (!myCollider.IsTouching(targetCollider))
        {
            Vector3 targetPos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("running", false);
    }

    private void FlipToFace(float targetX)
    {
        spriteRenderer.flipX = targetX < transform.position.x;
    }

    public bool IsBusy() => taskQueue.Count > 0;
}