using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    public GameManager gameManager;
    public DialogueManager dialogueManager;
    public Rigidbody2D rb;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (gameManager.freezeOverworld)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
