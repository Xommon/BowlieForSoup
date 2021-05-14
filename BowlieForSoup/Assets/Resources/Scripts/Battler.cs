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

    private void Start()
    {
        // References
        sr = GetComponent<SpriteRenderer>();
        battleManager = FindObjectOfType<BattleManager>();

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

        // Start Battle
        if (gameObject.name == "PlayerBattle")
        {
            StartCoroutine(battleManager.BattleStart());
        }
    }

    private void Update()
    {
        if (transform.position != home && !attacking)
        {
            Vector3.MoveTowards(transform.position, home, 1.0f);
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
}
