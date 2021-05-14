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
        // Calculate enemy stats
        GameObject.Find("EnemyBattle").GetComponent<Battler>().CreateEnemyStats(enemyIngredient, enemyLevel);
        // Upload Enemy's stats
        /*firstEnemy = GameObject.Find("EnemyBattle").GetComponent<Battler>();
        firstEnemy.level = enemyStats[0];
        firstEnemy.attack = enemyStats[1];
        firstEnemy.defence = enemyStats[2];
        firstEnemy.speed = enemyStats[3];
        firstEnemy.luck = enemyStats[4];*/

        // Sort battlers from quickest to slowest
        foreach (Battler battler in FindObjectsOfType<Battler>())
        {
            turnOrder.Add(battler);
        }
        turnOrder = turnOrder.OrderBy(w => w.GetComponent<Battler>().speed).ToList();

        yield return new WaitForSeconds(1.5f);
        turn = turnOrder[0];
    }
}
