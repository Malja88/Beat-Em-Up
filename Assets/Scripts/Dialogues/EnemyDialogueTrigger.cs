using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private EnemyDialogueManager enemyDialogueManager;
    void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        enemyDialogueManager.StartDialogue(dialogue);
    }
}
