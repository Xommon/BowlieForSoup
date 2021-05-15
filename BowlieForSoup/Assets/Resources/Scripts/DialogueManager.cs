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
    public GameObject answerArrow;
    public bool canChat;
    public RecipeManager recipeManager;
    private bool answer;
    public LiquidGenerator liquidGenerator;
    public bool skip;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
        gameManager = FindObjectOfType<GameManager>();
        liquidGenerator = FindObjectOfType<LiquidGenerator>();
        canChat = true;
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) && !yesBox.activeInHierarchy && (textArrow.activeInHierarchy || textCircle.activeInHierarchy) && !answerArrow.activeInHierarchy)
        {
            DisplayNextSentence();
        }
        else if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) && !yesBox.activeInHierarchy && (!textArrow.activeInHierarchy && !textCircle.activeInHierarchy) && !answerArrow.activeInHierarchy)
        {
            // Skip to end of sentence
            skip = true;
        }

        if (answerArrow.activeInHierarchy)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                answerArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 150);
                answer = true;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                answerArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 95);
                answer = false;
            }
        }

        if (Input.GetButtonDown("Fire1") && answerArrow.activeInHierarchy)
        {
            textCircle.SetActive(false);
            answerArrow.SetActive(false);
            noBox.SetActive(false);
            yesBox.SetActive(false);
            stoppedAtCharIndex = 0;

            if (answer)
            {
                // YES
                if (recipeManager.currentRecipe == "Creamy Tomato Soup")
                {
                    if (recipeManager.RateSoup() > 3)
                    {
                        sentenceIndex = 3;
                    }
                    else if (recipeManager.RateSoup() == 3)
                    {
                        sentenceIndex = 4;
                    }
                    else
                    {
                        sentenceIndex = 5;
                    }
                }
                else if (recipeManager.currentRecipe == "Creamy Carrot Soup")
                {
                    if (recipeManager.RateSoup() > 3)
                    {
                        sentenceIndex = 8;
                    }
                    else if (recipeManager.RateSoup() == 3)
                    {
                        sentenceIndex = 9;
                    }
                    else
                    {
                        sentenceIndex = 10;
                    }
                }

                recipeManager.ClearRecipe();
            }
            else
            {
                // NO
                sentenceIndex = 2;
            }

            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        answer = false; 
        answerArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 95);
        textArrow.SetActive(false);
        textCircle.SetActive(false);
        stoppedAtCharIndex = 0;
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.nameOfSpeaker;
        dialogueOpen = true;
        sentences = dialogue.sentences;
        gameManager.freezeOverworld = true;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        skip = false;
        if (textCircle.activeInHierarchy)
        {
            EndDialogue();
        }
        else if (!answerArrow.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(PrintSentence(sentences[sentenceIndex]));
        }
    }

    IEnumerator PrintSentence(string sentence)
    {
        textArrow.SetActive(false);
        textCircle.SetActive(false);
        dialogueText.text = "";

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
                    answerArrow.SetActive(true);
                    break;
                }
                else
                {
                    dialogueText.text += sentence.ToCharArray()[stoppedAtCharIndex + i];
                    if (!skip)
                    {
                        yield return null;
                    }
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

    private void EndDialogue()
    {
        yesBox.SetActive(false);
        noBox.SetActive(false);
        answerArrow.SetActive(false);
        dialogueText.text = "";
        animator.SetBool("isOpen", false);
        textCircle.SetActive(false);
        dialogueOpen = false;
        canChat = false;
        StartCoroutine(ChatCooldown());
        gameManager.freezeOverworld = false;

        if (sentenceIndex == 0)
        {
            // Introduce the creamy tomato soup
            recipeManager.UploadNewRecipe("Creamy Tomato Soup", new string[] { "Tomato Cube", "Onion Cube", "Garlic Cube" }, new int[] { 4, 1, 1 });
            sentenceIndex++;
        }
        else if (sentenceIndex == 2)
        {
            if (recipeManager.currentRecipe == "Creamy Tomato Soup")
            {
                sentenceIndex = 1;
            }
            else if (recipeManager.currentRecipe == "Creamy Carrot Soup")
            {
                sentenceIndex = 7;
            }
        }
        else if (sentenceIndex == 5)
        {
            sentenceIndex = 0;
        }
        else if (sentenceIndex == 4 || sentenceIndex == 3)
        {
            sentenceIndex = 6;
        }
        else if (sentenceIndex == 6)
        {
            // Introduce the creamy carrot soup
            recipeManager.UploadNewRecipe("Creamy Carrot Soup", new string[] { "Carrot Cube", "Onion Cube", "Garlic Cube" }, new int[] { 4, 1, 1 });
            sentenceIndex++;
        }
        else if (sentenceIndex == 7)
        {

        }
    }

    IEnumerator ChatCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        canChat = true;
    }
}
