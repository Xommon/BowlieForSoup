using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Overworld Stats
    public float walkingSpeed;

    // Battle Stats

    // References
    public GameManager gameManager;
    public Rigidbody2D rb;
    public DialogueTrigger npc;
    public DialogueManager dialogueManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {

        // Stop moving if the overworld is frozen
        if (!gameManager.freezeOverworld)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkingSpeed * Time.deltaTime * 50, Input.GetAxis("Vertical") * walkingSpeed * Time.deltaTime * 50);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Start dialogue with NPC
        if (Vector3.Distance(transform.position, npc.transform.position) < 1.33f && Input.GetKeyDown(KeyCode.Space) && !dialogueManager.dialogueOpen)
        {
            npc.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
