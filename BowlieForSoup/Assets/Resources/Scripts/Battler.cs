using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private Battler enemyTrigger;
    public bool walkHome;
    //public GameObject damageBubble;
    //public TextMesh damageText;
    public GameManager gameManager;
    public bool damaged;
    public GameObject targetArrow;
    public Battler target;
    public GameObject playerShadow;
    public GameObject knifeObject;
    public GameObject spoonObject;
    public GameObject forkObject;
    public bool moreRed;
    public bool captured;
    public PlayerIngredients playerIngredients;
    public bool fleeing;

    // ATTACKS
    // Roll
    public float momentum;
    public float launchSpeed;

    private void Start()
    {
        // References
        fleeing = false;
        sr = GetComponent<SpriteRenderer>();
        battleManager = FindObjectOfType<BattleManager>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        playerIngredients = FindObjectOfType<PlayerIngredients>();
        battleManager.battleCamera = GameObject.Find("Battle Camera");

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
        // Shadow follows
        if (name == "PlayerBattle")
        {
            if (battleManager.turn == battleManager.playerBattle)
            {
                playerShadow.transform.position = new Vector3(-5, -0.5f, 0);
            }
            else
            {
                playerShadow.transform.position = new Vector3(transform.position.x, playerShadow.transform.position.y, playerShadow.transform.position.z);
            }
        }

        // Captured enemies spin slightly
        if (captured)
        {
            transform.Rotate(0, 0, 2);
        }

        // Indicate weakness
        if (name != "PlayerBattle" && currentHealth <= (health / 4))
        {
            if (!moreRed)
            {
                sr.color -= new Color(0, 0.05f, 0.05f, 0);
            }
            else
            {
                sr.color += new Color(0, 0.05f, 0.05f, 0);
            }
            if (sr.color.b >= 1)
            {
                moreRed = false;
            }
            else if (sr.color.b <= 0)
            {
                moreRed = true;
            }
        }

        // Give player gravity briefly
        if (name == "PlayerBattle")
        {
            if (battleManager.turn != this && battleManager.turn != null)
            {
                //transform.position = new Vector3(home.x, transform.position.y, transform.position.z);
                if (Input.GetButtonDown("Fire1") && transform.position == home)
                {
                    // Jump
                    rb.velocity = new Vector2(0, 800) * Time.deltaTime;
                    rb.gravityScale = 4;
                }
            }

            if (transform.position.y < home.y && state == "")
            {
                rb.gravityScale = 0;
                rb.velocity = Vector3.zero;
                transform.position = home;
            }
            else if (state == "")
            {
                rb.gravityScale = 4;
            }
            else if (state != "")
            {
                rb.gravityScale = 0;
            }
        }

        // Walk home
        if (walkHome && transform.position != home)
        {
            transform.position = Vector2.MoveTowards(transform.position, home, 9.0f * Time.deltaTime);
        }

        // Indicate target
        if (name != "PlayerBattle" && battleManager.playerBattle.target == this && battleManager.battleHUD.weaponMenu.activeInHierarchy)
        {
            targetArrow.SetActive(true);
        }
        else if (name != "PlayerBattle")
        {
            targetArrow.SetActive(false);
        }

        // Walk up to enemy
        if (state == "Knife" || state == "Spoon" || state == "Fork")
        {
            if (phase == 0)
            {
                if (transform.position != target.transform.position - new Vector3(2, 0, 0))
                {
                    // Get into position to attack
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position - new Vector3(2, 0, 0), 9.0f * Time.deltaTime);
                }
                else
                {
                    // Position ready
                    phase = 1;
                }
            }
            else if (phase == 1)
            {
                if (state == "Knife")
                {
                    knifeObject.SetActive(true);
                    StartCoroutine(KnifeDamage());
                }
                else if (state == "Spoon")
                {
                    spoonObject.SetActive(true);
                    StartCoroutine(SpoonDamage());
                }
                else if (state == "Fork")
                {
                    forkObject.SetActive(true);
                    StartCoroutine(ForkCapture());
                }
                phase = 2;
            }
            else if (phase == 2 && transform.position == home)
            {
                battleManager.playerBattle.target = null;
                state = "";
                walkHome = false;
                battleManager.EndTurn();
            }
        }

        // Roll attack
        if (state == "Roll")
        {
            if (phase == 0)
            {
                if (transform.position != new Vector3(-1, 0, 0))
                {
                    // Get into position to attack
                    transform.position = Vector2.MoveTowards(transform.position, new Vector3(-1, 0, 0), 9.0f * Time.deltaTime);
                }
                else
                {
                    // Position ready
                    phase = 1;
                    momentum = 2.0f;
                }
            }
            else if (phase == 1)
            {
                // Start rotating
                momentum *= 1.04f;
                transform.Rotate(0, 0, momentum);
            }
            else if (phase == 2)
            {
                // Launch at player
                if (transform.position.x > -10.5f)
                {
                    transform.Rotate(0, 0, momentum);
                    transform.position += new Vector3(-launchSpeed, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(10.5f, home.y, 0);
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
                    walkHome = false;
                    momentum = 0;
                    battleManager.EndTurn();
                }
            }
        }

        // Run away
        if (fleeing)
        {
            if (name == "PlayerBattle")
            {
                transform.position += new Vector3(-0.1f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        // Die if health is depleted
        if (name != "PlayerBattle" && currentHealth <= 0 && health > 0)
        {
            // Create children if cut up
            if (battleManager.battleHUD.weaponIndex == 0)
            {
                foreach (Ingredient childIngredient in ingredient.children)
                {
                    Battler newEnemy = Instantiate(battleManager.enemyPrefab);
                    newEnemy.CreateEnemyStats(childIngredient, level);
                    battleManager.turnOrder.Add(newEnemy);
                    battleManager.enemies.Add(newEnemy);
                    newEnemy.walkHome = true;
                }
            }

            battleManager.turnOrder.Remove(this);
            Destroy(gameObject);
        }
    }

    public void CreateEnemyStats(Ingredient ingredient, int level)
    {
        sr.sprite = ingredient.sprite;
        this.level = level;
        this.ingredient = ingredient;
        health = 5 + (int)((level + ingredient.health) * 1.5f);
        attack = (int)(level * ingredient.attack);
        defence = (int)(level * ingredient.defence);
        speed = (int)(level * ingredient.speed);
        luck = (int)(level * ingredient.luck);

        currentHealth = health;
        currentAttack = attack;
        currentDefence = defence;
        currentSpeed = speed;
        currentLuck = luck;
    }

    // Player Attacks
    public void KnifeAttack()
    {
        state = "Knife";
        phase = 0;
    }

    IEnumerator KnifeDamage()
    {
        yield return new WaitForSeconds(0.25f);

        // Do damage to enemy
        int damage = (int)Random.Range(battleManager.playerBattle.currentAttack * 1.0f, battleManager.playerBattle.currentAttack * 1.35f) - (target.currentDefence / 2);
        GameObject newDamage = Instantiate(battleManager.damagePrefab, target.transform.position, Quaternion.identity, null);
        TextMesh newDamageText = newDamage.GetComponentInChildren<TextMesh>();
        if (damage < 0)
        {
            damage = 0;
        }
        if (Random.Range(0, 101) < battleManager.playerBattle.currentLuck)
        {
            damage *= 2;
            newDamageText.text = "Critical Hit!\n" + damage.ToString();
        }
        else
        {
            newDamageText.text = damage.ToString();
        }

        if (target.ingredient.name == "Corn")
        {
            target.HaveChild();
        }

        target.currentHealth -= damage;
        StartCoroutine(WalkHome());
    }

    IEnumerator SpoonDamage()
    {
        yield return new WaitForSeconds(0.25f);

        // Do damage to enemy
        int damage = (int)Random.Range(battleManager.playerBattle.currentAttack * 0.9f, battleManager.playerBattle.currentAttack * 1.1f) - (target.currentDefence / 2);
        GameObject newDamage = Instantiate(battleManager.damagePrefab, target.transform.position, Quaternion.identity, null);
        TextMesh newDamageText = newDamage.GetComponentInChildren<TextMesh>();
        if (damage < 0)
        {
            damage = 0;
        }
        if (Random.Range(0, 101) < battleManager.playerBattle.currentLuck)
        {
            damage *= 2;
            newDamageText.text = "Critical Hit!\n" + damage.ToString();
        }
        else
        {
            newDamageText.text = damage.ToString();
        }

        if (target.ingredient.name == "Corn")
        {
            target.HaveChild();
        }

        target.currentHealth -= damage;
        StartCoroutine(WalkHome());
    }

    public void HaveChild()
    {
        Battler newEnemy = Instantiate(battleManager.enemyPrefab);
        newEnemy.CreateEnemyStats(ingredient.children[0], level);
        battleManager.turnOrder.Add(newEnemy);
        battleManager.enemies.Add(newEnemy);
        newEnemy.walkHome = true;
    }

    IEnumerator ForkCapture()
    {
        yield return new WaitForSeconds(0.25f);

        // Capture enemy if theyre weak enough
        if (target.currentHealth <= target.health / 4)
        {
            target.captured = true;
            target.rb.gravityScale = 3;
            target.rb.velocity = new Vector2(-200 * Time.deltaTime, 550 * Time.deltaTime);
        }

        yield return new WaitForSeconds(0.125f);
        StartCoroutine(WalkHome());
    }

    public void SpoonAttack()
    {
        state = "Spoon";
        phase = 0;
    }

    public void ForkAttack()
    {
        state = "Fork";
        phase = 0;
    }

    // Enemy Attacks
    public IEnumerator Roll()
    {
        phase = 0; // Get into position
        state = "Roll";

        float launchTime = Random.Range(1.5f, 2.5f);
        if (level < 10)
        {
            launchSpeed = Random.Range(0.1f, 0.1f);
        }
        else if (level < 20)
        {
            launchSpeed = Random.Range(0.1f, 0.2f);
        }
        else
        {
            launchSpeed = Random.Range(0.04f, 0.3f);
        }
        yield return new WaitForSeconds(launchTime);
        phase = 2; // Launch toward player
    }

    public void Tackle()
    {
        phase = 0; // Get into position
        state = "Tackle";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (name == "PlayerBattle")
        {
            enemyTrigger = collision.GetComponent<Battler>();
        }

        // Player hit by attack
        if (enemyTrigger != null)
        {
            if (name == "PlayerBattle" && battleManager.turn == enemyTrigger && enemyTrigger.state == "Roll" && !damaged)
            {
                // Do damage to player
                battleManager.ShakeScreen(2.0f, 1.5f);
                damaged = true;
                int damage = (int)Random.Range(enemyTrigger.currentAttack * 0.8f, enemyTrigger.currentAttack * 1.2f) - (currentDefence / 2);
                GameObject newDamage = Instantiate(battleManager.damagePrefab, battleManager.playerBattle.transform.position, Quaternion.identity, null);
                TextMesh newDamageText = newDamage.GetComponentInChildren<TextMesh>();
                if (Random.Range(0, 101) < enemyTrigger.currentLuck)
                {
                    damage *= 2;
                    newDamageText.text = "Critical Hit!\n" + damage.ToString();
                }
                else
                {
                    newDamageText.text = damage.ToString();
                }
                if (damage < 0)
                {
                    damage = 0;
                }
                else if (damage > 100)
                {
                    damage = 100;
                }
                gameManager.playerFill -= damage;

                //rb.gravityScale = 4;
                //rb.velocity = new Vector2(0, 300) * Time.deltaTime;
                StartCoroutine(WalkHome());
            }
        }

        // Destroy captured enemy
        if (name == "PlayerBattle" && enemyTrigger != null)
        {
            if (enemyTrigger.captured)
            {
                playerIngredients.inventory.Add(enemyTrigger.ingredient);
                battleManager.turnOrder.Remove(enemyTrigger);
                Destroy(enemyTrigger.gameObject);
            }
        }
    }

    public IEnumerator WalkHome()
    {
        yield return new WaitForSeconds(1.0f);

        if (name == "PlayerBattle")
        {
            knifeObject.SetActive(false);
            spoonObject.SetActive(false);
            forkObject.SetActive(false);
        }

        if (battleManager.turn == this)
        {
            walkHome = true;
        }
    }
}
