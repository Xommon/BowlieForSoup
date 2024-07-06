using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Language
    public enum Language { English, Spanish, French };
    public Language language;

    // Dialogue
    public TextMeshProUGUI text;
    private int messageIndex;
    private string messageToPush;

    [ContextMenu("TestDialogue")]
    public void StartDialogue()
    {
        messageToPush = Dialogue.english[0];
        messageIndex = 0;
        text.text = "";
        StartCoroutine(PushLetter());
    }

    IEnumerator PushLetter()
    {
        text.text += messageToPush[messageIndex];

        // Add pauses for punctuation
        if (messageToPush[messageIndex] == '.' || messageToPush[messageIndex] == ',' || messageToPush[messageIndex] == '!' || messageToPush[messageIndex] == '?' || messageToPush[messageIndex] == ':' || messageToPush[messageIndex] == ';')
        {
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.025f);

        messageIndex++;

        if (messageToPush.Length > messageIndex)
        {
            StartCoroutine(PushLetter());
        }
    }
}
