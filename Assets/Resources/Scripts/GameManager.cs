using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Language
    public enum Language { English, Spanish, French };
    public Language language;

    // Dialogue
    public TextMeshProUGUI text;
    public GameObject speechBubble;
    private int charIndex;
    private int lineIndex;
    private string messageToPush;
    public GameObject nextIcon;

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && nextIcon.activeInHierarchy)
        {
            if (messageToPush.Length > charIndex)
            {
                ContinueDialogue();
            }
            else
            {
                // End of chat
                speechBubble.SetActive(false);
            }
        }
    }

    [ContextMenu("TestDialogue")]
    public void StartDialogue()
    {
        lineIndex = 0;
        messageToPush = Dialogue.english[lineIndex];
        charIndex = 0;
        text.text = "";
        speechBubble.SetActive(true);
        nextIcon.SetActive(false);
        StartCoroutine(PushLetter());
    }

    public void ContinueDialogue()
    {
        text.text = "";
        nextIcon.SetActive(false);
        StartCoroutine(PushLetter());
    }

    IEnumerator PushLetter()
    {
        while (charIndex < messageToPush.Length)
        {
            // Add the next letter to the text
            text.text += messageToPush[charIndex];

            // Add pauses for punctuation
            if (char.IsPunctuation(messageToPush[charIndex]) && messageToPush[charIndex] != '\'' && messageToPush[charIndex] != '\"')
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.02f);
            }
            charIndex++;

            // Check if the text has overflowed to a new page
            if (PageFull())
            {
                // Remove the last added letter and wait for the user to proceed
                text.text = text.text.Remove(text.text.Length - 1);
                charIndex--;
                nextIcon.SetActive(true);
                yield break; // Exit the coroutine and wait for user input
            }
        }
        // Show the next icon if all letters are displayed
        nextIcon.SetActive(true);
    }

    public bool PageFull()
    {
        return !(text.pageToDisplay >= text.textInfo.pageCount);
    }
}
