using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    // Level
    public int level;

    // Attack
    public int attack;
    public int currentAttack;

    // Defence
    public int defence;
    public int currentDefence;

    // Speed
    public int speed;
    public int currentSpeed;

    // Luck
    public int luck;
    public int currentLuck;

    // Experience
    public int exp;
    public int currentExp;

    // Misc
    public int health;
    public int currentHealth;
    public Ingredient ingredient;
    public int positionOnField;
    public SpriteRenderer sr;
    public Vector3 home;
    public bool attacking;
    public BattleManager battleManager;
    public Rigidbody2D rb;
    public string state;
    public int phase;

    // ATTACKS
    
    // Roll
    public float momentum;
    public float launchSpeed;

    private void Start()
    {
        // References
        sr = GetComponent<SpriteRenderer>();
        battleManager = FindObjectOfType<BattleManager>();
        rb = GetComponent<Rigidbody2D>();

        // Create the player's stats
        if (gameObject.name == "PlayerBattle")
        {
            level = battleManager.level;
            attack = battleManager.attack;
            defence = battleManager.defence;
            speed = battleManager.speed;
            luck = battleManager.luck;
            exp = battleManager.exp;

            currentAttack = attack;
            currentDefence = defence;
            currentSpeed = speed;
            currentLuck = luck;
        }

        // Set stats
        phase = 0;
        state = "";

        // Start Battle
        if (gameObject.name == "PlayerBattle")
        {
            StartCoroutine(battleManager.BattleStart());
        }
    }

    private void Update()
    {
        // Give player gravity briefly
        if (name == "PlayerBattle" && battleManager.turn != this)
        {
            rb.gravityScale = 4;

            if (Input.GetButtonDown("Jump") && transform.position == home)
            {
                Debug.Log("Jump");
                rb.velocity = new Vector2(0, 800) * Time.deltaTime;
            }

            if (transform.position.y < 0)
            {
                transform.position = home;
            }
        }
        else
        {
            rb.gravityScale = 0;
        }

        // Roll attack
        if (state == "Roll")
        {
            if (phase == 0)
            {
                if (transform.position != Vector3.zero)
                {
                    // Get into position to attack
                    transform.position = Vector2.MoveTowards(transform.position, Vector3.zero, 4.0f * Time.deltaTime);
                }
                else
                {
                    // Position ready
                    phase = 1;
                    momentum = 0.5f;
                }
            }
            else if (phase == 1)
            {
                // Start rotating
                momentum *= 1.025f;
                transform.Rotate(0, 0, momentum);
            }
            else if (phase == 2)
            {
                // Launch at player
                if (transform.position.x > -11.0f)
                {
                    transform.Rotate(0, 0, momentum);
                    transform.position += new Vector3(-launchSpeed, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(11.0f, home.y, 0);
                    phase = 3; // Return home
                }
            }
            else if (phase == 3)
            {
                if (transform.position.x > home.x)
                {
                    // Return home
                    transform.Rotate(0, 0, momentum / 4);
                    transform.position += new Vector3(-0.15f, 0, 0);
                }
                else
                {
                    // End attack
                    transform.position = home;
                    transform.rotation = Quaternion.identity;
                    state = "";
                    phase = 0;
                    battleManager.EndTurn();
                }
            }
        }
    }

    public void CreateEnemyStats(Ingredient ingredient, int level)
    {
        sr.sprite = ingredient.sprite;
        this.level = level;
        this.ingredient = ingredient;
        health = 5 + (int)(level * ingredient.health * 1.5f);
        attack = (int)(level * ingredient.attack);
        defence = (int)(level * ingredient.defence);
        speed = (int)(level * ingredient.speed);
        luck = (int)(level * ingredient.luck);

        currentAttack = attack;
        currentDefence = defence;
        currentSpeed = speed;
        currentLuck = luck;
    }

    // Player Attacks

    // Enemy Attacks
    public IEnumerator Roll()
    {
        Debug.Log("Phase 0");
        phase = 0; // Get into position
        state = "Roll";

        float launchTime = Random.Range(2.5f, 5.0f);
        launchSpeed = Random.Range(0.1f, 0.3f);
        yield return new WaitForSeconds(launchTime);
        phase = 2; // Launch toward player
    }

    public void Tackle()
    {
        phase = 0; // Get into position
        state = "Tackle";
    }
}
