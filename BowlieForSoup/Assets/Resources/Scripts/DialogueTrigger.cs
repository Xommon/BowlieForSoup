using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public LanguageManager languageManager;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Awake()
    {
        languageManager = FindObjectOfType<LanguageManager>();
    }

    private void Update()
    {
        dialogue.sentences = languageManager.allLanguages[languageManager.currentLanguage];
    }
}
