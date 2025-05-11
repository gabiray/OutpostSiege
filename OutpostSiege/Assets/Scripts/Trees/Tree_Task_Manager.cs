using System;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Task_Manager : MonoBehaviour
{
    public static Tree_Task_Manager Instance { get; private set; }

    private readonly Queue<(GameObject, Action<GameObject>)> pendingTasks = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddTask(GameObject tree, Action<GameObject> callback)
    {
        if (!IsTreeAlreadyQueued(tree))
        {
            pendingTasks.Enqueue((tree, callback));
            Debug.Log($"Tree queued in Task Manager: {tree.name}");
        }
    }

    public bool TryGetTask(out (GameObject, Action<GameObject>) task)
    {
        if (pendingTasks.Count > 0)
        {
            task = pendingTasks.Dequeue();
            return true;
        }

        task = default;
        return false;
    }

    private bool IsTreeAlreadyQueued(GameObject tree)
    {
        foreach (var task in pendingTasks)
            if (task.Item1 == tree)
                return true;
        return false;
    }
}
