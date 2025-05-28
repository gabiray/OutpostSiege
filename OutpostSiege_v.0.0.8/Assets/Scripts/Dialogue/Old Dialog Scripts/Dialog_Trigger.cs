using UnityEngine;

public class Dialog_Trigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
        FindFirstObjectByType<Dialog_Manager>().StartDialogue(dialogue);
    }
}
