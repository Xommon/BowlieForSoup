using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiquidParticle : MonoBehaviour
{
    public GameManager gameManager;
    public DialogueManager dialogueManager;
    public LiquidGenerator liquidGenerator;
    public Rigidbody2D rb;
    public bool inBowl;
    public int life;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (SceneManager.GetActiveScene().name != "Battle")
        {
            liquidGenerator = FindObjectOfType<LiquidGenerator>();
        }
        life = 30;
    }

    void Update()
    {
        // Freeze
        if (gameManager.freezeOverworld && SceneManager.GetActiveScene().name != "Battle")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        // Adjust gravity
        if (SceneManager.GetActiveScene().name == "Battle")
        {
            rb.gravityScale = 1;
        }
        else
        {
            rb.gravityScale = 1;
        }

        if (inBowl)
        {
            life = 60;
        }
        else if (!gameManager.freezeOverworld)
        {
            life--;
        }

        if (life <= 0)
        {
            if (SceneManager.GetActiveScene().name != "Battle")
            {
                liquidGenerator.liquidCount--;
            }
            else
            {
                gameManager.playerFill--;
            }
            Destroy(gameObject);
        }
    }
}
