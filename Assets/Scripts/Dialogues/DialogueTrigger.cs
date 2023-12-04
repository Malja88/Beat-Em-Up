using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueManager dialogueManager;
    void Start()
    {
        TriggerDialogue();       
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
