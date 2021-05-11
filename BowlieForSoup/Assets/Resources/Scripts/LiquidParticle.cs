using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        liquidGenerator = FindObjectOfType<LiquidGenerator>();
        life = 60;
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
            liquidGenerator.liquidCount--;
            Destroy(gameObject);
        }
    }
}
