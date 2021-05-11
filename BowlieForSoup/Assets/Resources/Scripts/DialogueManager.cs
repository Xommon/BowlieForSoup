using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject textBox;
    private string[] sentences;
    public int sentenceIndex;
    public bool dialogueOpen;
    public GameManager gameManager;
    public Animator animator;
    public GameObject textArrow;
    public GameObject textCircle;
    public int stoppedAtCharIndex;
    public int remainingSentences;
    public GameObject yesBox;
    public GameObject noBox;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //sentences = new Queue<string>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !yesBox.activeInHierarchy && (textArrow.activeInHierarchy || textCircle.activeInHierarchy))
        {
            DisplayNextSentence();
        }

        gameManager.freezeOverworld = dialogueOpen;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        textArrow.SetActive(false);
        textCircle.SetActive(false);
        stoppedAtCharIndex = 0;
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.nameOfSpeaker;
        dialogueOpen = true;
        sentences = dialogue.sentences;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (textCircle.activeInHierarchy)
        {
            EndDialogue();
        }
        else
        {
            //string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(PrintSentence(sentences[sentenceIndex]));
        }
    }

    IEnumerator PrintSentence(string sentence)
    {
        textArrow.SetActive(false);
        textCircle.SetActive(false);
        dialogueText.text = "";

        //StartCoroutine(TypeSentence(sentence));
        for (int i = 0; i < sentence.Length; i++)
        {
            if (i == sentence.Length - 1)
            {
                textCircle.SetActive(true);
                break;
            }

            if (stoppedAtCharIndex + i < sentence.Length)
            {
                if (sentence.ToCharArray()[stoppedAtCharIndex + i] == '|')
                {
                    textCircle.SetActive(true);
                    stoppedAtCharIndex += i + 2;
                    yesBox.SetActive(true);
                    noBox.SetActive(true);
                    break;
                }
                else
                {
                    dialogueText.text += sentence.ToCharArray()[stoppedAtCharIndex + i];
                    yield return null;
                }
            }

            if (dialogueText.preferredWidth >= 555)
            {
                textArrow.SetActive(true);
                stoppedAtCharIndex += i + 1;
                break;
            }
        }
    }

    /*IEnumerator TypeSentence(string sentence)
    {
        for (int i = 0; i < sentence.Length; i++)
        {
            if (dialogueText.preferredWidth < 560)
            {
                if (i == sentence.Length)
                {
                    if (sentences.Count == 0)
                    {
                        textCircle.SetActive(true);
                    }
                    else
                    {
                        textArrow.SetActive(true);
                    }

                    PrintSentence(sentence);
                }
                else
                {
                    dialogueText.text += sentence.Substring(stoppedAtCharIndex + i);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                stoppedAtCharIndex += i;
                textArrow.SetActive(true);
                break;
            }
        }
        // Determine whether the word will fit or not
        if (dialogueText.preferredWidth < 560)
        {
            // Word will fit
            foreach (char letter in measureSentenceText.text.ToCharArray())
            {
                dialogueText.text += letter;
                stoppedAtCharIndex++;
                yield return null;
            }
            PrintSentence(sentence.Substring(stoppedAtCharIndex, sentence.Length));
        }
        else
        {
            // Word will NOT fit
            Debug.Log("Word will not fit");
            textArrow.SetActive(true);
        }
    }*/

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        dialogueOpen = false;
    }
}
