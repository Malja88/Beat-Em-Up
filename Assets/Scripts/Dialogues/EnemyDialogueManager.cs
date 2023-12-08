using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDialogueManager : MonoBehaviour
{
    [SerializeField] private Queue<string> sentences = new Queue<string>();
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject dialogueBox;

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
            await Task.Delay(100);
        }
        if (nextButton != null)
        {
            nextButton.interactable = true;
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
