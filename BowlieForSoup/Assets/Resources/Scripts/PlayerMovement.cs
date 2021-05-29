using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Overworld Stats
    public float walkingSpeed;

    // References
    public GameManager gameManager;
    public BattleManager battleManager;
    public Rigidbody2D rb;
    public DialogueTrigger npc;
    public DialogueManager dialogueManager;
    public LevelLoader levelLoader;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
        transform.position = gameManager.savedPlayerPosition;
        gameManager.player = this;
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
        if (Vector3.Distance(transform.position, npc.transform.position) < 1.33f && Input.GetButtonDown("Fire1") && !dialogueManager.dialogueOpen && dialogueManager.canChat)
        {
            npc.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !gameManager.freezeOverworld)
        {
            // Download the enemy's stats
            EnemyMovement collisionEnemy = collision.gameObject.GetComponent<EnemyMovement>();
            battleManager.battleInstanceFromOverworld = collision.gameObject.name;
            battleManager.enemyIngredient = collisionEnemy.ingredient;
            battleManager.enemyLevel = collisionEnemy.level;
            gameManager.savedPlayerPosition = transform.position;
            levelLoader.LoadLevel(2);
        }
    }
}
