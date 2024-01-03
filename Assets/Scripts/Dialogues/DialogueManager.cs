using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Queue<string> sentences = new Queue<string>();
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject enemyDialogueBox;
    [SerializeField] private CharacterMovement movement;
    private void Update()
    {
        if (nextButton != null && nextButton.interactable)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            //enemyDialogueBox.SetActive(true);
            return;
        }

        if (nextButton != null)
        {
            nextButton.interactable = false;
        }
        string sentence = sentences.Dequeue();
        TypeSentence(sentence);
    }

    async void TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            await Task.Delay(50);
        }
        if (nextButton != null)
        {
            nextButton.interactable = true;
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        movement.isMoving = true;
        movement.isAttacking = true;
    }
}
