using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public List<Battler> enemies = new List<Battler>();
    public GameObject battleInstanceFromOverworld;
    public Animator animatorHUD;
    public GameObject battleMenuArrow;
    public List<Battler> turnOrder = new List<Battler>();
    public Battler firstEnemy;
    public Battler playerBattle;
    public Battler turn;

    // Enemy battle stats
    public Ingredient enemyIngredient;
    public int enemyLevel;

    // Player battle stats
    public int level;
    public int attack;
    public int defence;
    public int speed;
    public int luck;
    public int exp;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // Select standing position for each enemy
        if (enemies.Count == 1)
        {
            enemies[0].home = new Vector3(6, 0, 0);
        }
        else if (enemies.Count == 2)
        {
            enemies[0].home = new Vector3(6, 1.5f, 0);
            enemies[1].home = new Vector3(6, -1.5f, 0);
        }
        else if (enemies.Count == 3)
        {
            enemies[0].home = new Vector3(6, 1.5f, 0);
            enemies[1].home = new Vector3(6, -1.5f, 0);
            enemies[2].home = new Vector3(4, 0, 0);
        }
        else if (enemies.Count == 4)
        {
            enemies[0].home = new Vector3(6, 1.5f, 0);
            enemies[1].home = new Vector3(6, -1.5f, 0);
            enemies[2].home = new Vector3(4, 1.5f, 0);
            enemies[3].home = new Vector3(4, -1.5f, 0);
        }
        else if (enemies.Count == 5)
        {
            enemies[0].home = new Vector3(6, 2.5f, 0);
            enemies[1].home = new Vector3(6, 0, 0);
            enemies[2].home = new Vector3(6, -2.5f, 0);
            enemies[3].home = new Vector3(4, 1.5f, 0);
            enemies[4].home = new Vector3(4, -1.5f, 0);
        }
        else if (enemies.Count == 6)
        {
            enemies[0].home = new Vector3(6, 2.5f, 0);
            enemies[1].home = new Vector3(6, 0, 0);
            enemies[2].home = new Vector3(6, -2.5f, 0);
            enemies[3].home = new Vector3(4, 2.5f, 0);
            enemies[4].home = new Vector3(4, 0, 0);
            enemies[5].home = new Vector3(4, -2.5f, 0);
        }
        else if (enemies.Count == 7)
        {
            enemies[0].home = new Vector3(6, 3, 0);
            enemies[1].home = new Vector3(6, 1, 0);
            enemies[2].home = new Vector3(6, -1, 0);
            enemies[3].home = new Vector3(6, -3, 0);
            enemies[4].home = new Vector3(4, 2.5f, 0);
            enemies[5].home = new Vector3(4, 0, 0);
            enemies[6].home = new Vector3(4, -2.5f, 0);
        }
        else if (enemies.Count == 8)
        {
            enemies[0].home = new Vector3(6, 3, 0);
            enemies[1].home = new Vector3(6, 1, 0);
            enemies[2].home = new Vector3(6, -1, 0);
            enemies[3].home = new Vector3(6, -3, 0);
            enemies[4].home = new Vector3(4, 3, 0);
            enemies[5].home = new Vector3(4, 1, 0);
            enemies[6].home = new Vector3(4, -1, 0);
            enemies[7].home = new Vector3(4, -3, 0);
        }
        else if (enemies.Count == 9)
        {
            enemies[0].home = new Vector3(6, 2, 0);
            enemies[1].home = new Vector3(6, 0, 0);
            enemies[2].home = new Vector3(6, -2, 0);
            enemies[3].home = new Vector3(4, 2, 0);
            enemies[4].home = new Vector3(4, 0, 0);
            enemies[5].home = new Vector3(4, -2, 0);
            enemies[6].home = new Vector3(2, 2, 0);
            enemies[7].home = new Vector3(2, 0, 0);
            enemies[8].home = new Vector3(2, -2, 0);
        }
        else if (enemies.Count == 10)
        {
            enemies[0].home = new Vector3(6, 2, 0);
            enemies[1].home = new Vector3(6, 0, 0);
            enemies[2].home = new Vector3(6, -2, 0);
            enemies[3].home = new Vector3(4, 3, 0);
            enemies[4].home = new Vector3(4, 1, 0);
            enemies[5].home = new Vector3(4, -1, 0);
            enemies[6].home = new Vector3(4, -3, 0);
            enemies[7].home = new Vector3(2, 2, 0);
            enemies[8].home = new Vector3(2, 0, 0);
            enemies[9].home = new Vector3(2, -2, 0);
        }
        else if (enemies.Count == 12)
        {
            enemies[0].home = new Vector3(6, 3, 0);
            enemies[1].home = new Vector3(6, 1, 0);
            enemies[2].home = new Vector3(6, -1, 0);
            enemies[3].home = new Vector3(6, -3, 0);
            enemies[4].home = new Vector3(4, 3, 0);
            enemies[5].home = new Vector3(4, 1, 0);
            enemies[6].home = new Vector3(4, -1, 0);
            enemies[7].home = new Vector3(4, -3, 0);
            enemies[8].home = new Vector3(2, 2, 0);
            enemies[9].home = new Vector3(2, 0, 0);
            enemies[10].home = new Vector3(2, -2, 0);
        }
        else if (enemies.Count == 11)
        {
            enemies[0].home = new Vector3(6, 3, 0);
            enemies[1].home = new Vector3(6, 1, 0);
            enemies[2].home = new Vector3(6, -1, 0);
            enemies[3].home = new Vector3(6, -3, 0);
            enemies[4].home = new Vector3(4, 3, 0);
            enemies[5].home = new Vector3(4, 1, 0);
            enemies[6].home = new Vector3(4, -1, 0);
            enemies[7].home = new Vector3(4, -3, 0);
            enemies[8].home = new Vector3(2, 3, 0);
            enemies[9].home = new Vector3(2, 1, 0);
            enemies[10].home = new Vector3(2, -1, 0);
            enemies[11].home = new Vector3(2, -3, 0);
        }
    }

    public IEnumerator BattleStart()
    {
        Debug.Log("Battle Start");
        // Compile list of enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.GetComponent<Battler>());
        }

        // Calculate enemy stats
        GameObject.Find("EnemyBattle").GetComponent<Battler>().CreateEnemyStats(enemyIngredient, enemyLevel);

        // Upload player stats
        playerBattle = GameObject.Find("PlayerBattle").GetComponent<Battler>();
        playerBattle.level = level;
        playerBattle.attack = attack;
        playerBattle.defence = defence;
        playerBattle.speed = speed;
        playerBattle.luck = luck;

        // Sort battlers from quickest to slowest
        foreach (Battler battler in FindObjectsOfType<Battler>())
        {
            turnOrder.Add(battler);
        }
        turnOrder = turnOrder.OrderBy(w => w.GetComponent<Battler>().speed).ToList();
        turnOrder.Reverse();

        yield return new WaitForSeconds(1.0f);
        turn = turnOrder[0];
        Debug.Log(turnOrder[0].name + "'s turn");
        StartCoroutine(GameObject.Find("EnemyBattle").GetComponent<Battler>().Roll());
    }

    public void EndTurn()
    {
        // Start Battler's next turn
        if (turnOrder.IndexOf(turn) == turnOrder.Count - 1)
        {
            turn = turnOrder[0];
        }
        else
        {
            turn = turnOrder[turnOrder.IndexOf(turn) + 1];
        }
    }

    public void StartTurn(Battler battler)
    {
        if (battler.name == "PlayerBattle")
        {
            //BattleHUD.
        }
    }
}
