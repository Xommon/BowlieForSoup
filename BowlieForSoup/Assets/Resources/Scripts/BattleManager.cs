using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public List<Battler> enemies = new List<Battler>();
    public string battleInstanceFromOverworld;
    public Animator animatorHUD;
    public GameObject battleMenuArrow;
    public List<Battler> turnOrder = new List<Battler>();
    public Battler firstEnemy;
    public Battler playerBattle;
    public Battler turn;
    public BattleHUD battleHUD;
    public LevelLoader levelLoader;
    public Battler enemyPrefab;
    public GameObject damagePrefab;
    public GameObject battleCamera;

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
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Update()
    {
        // Select standing position for each enemy
        if (enemies.Count == 1)
        {
            enemies[0].home = new Vector3(5, 0, 0);
        }
        else if (enemies.Count == 2)
        {
            enemies[0].home = new Vector3(5, 1.5f, 0);
            enemies[1].home = new Vector3(5, -1.5f, 0);
        }
        else if (enemies.Count == 3)
        {
            enemies[0].home = new Vector3(5, 1.5f, 0);
            enemies[1].home = new Vector3(5, -1.5f, 0);
            enemies[2].home = new Vector3(3, 0, 0);
        }
        else if (enemies.Count == 4)
        {
            enemies[0].home = new Vector3(5, 1.5f, 0);
            enemies[1].home = new Vector3(5, -1.5f, 0);
            enemies[2].home = new Vector3(3, 1.5f, 0);
            enemies[3].home = new Vector3(3, -1.5f, 0);
        }
        else if (enemies.Count == 5)
        {
            enemies[0].home = new Vector3(5, 2.5f, 0);
            enemies[1].home = new Vector3(5, 0, 0);
            enemies[2].home = new Vector3(5, -2.5f, 0);
            enemies[3].home = new Vector3(3, 1.5f, 0);
            enemies[4].home = new Vector3(3, -1.5f, 0);
        }
        else if (enemies.Count == 6)
        {
            enemies[0].home = new Vector3(5, 2.5f, 0);
            enemies[1].home = new Vector3(5, 0, 0);
            enemies[2].home = new Vector3(5, -2.5f, 0);
            enemies[3].home = new Vector3(3, 2.5f, 0);
            enemies[4].home = new Vector3(3, 0, 0);
            enemies[5].home = new Vector3(3, -2.5f, 0);
        }
        else if (enemies.Count == 7)
        {
            enemies[0].home = new Vector3(5, 3, 0);
            enemies[1].home = new Vector3(5, 1, 0);
            enemies[2].home = new Vector3(5, -1, 0);
            enemies[3].home = new Vector3(5, -3, 0);
            enemies[4].home = new Vector3(3, 2.5f, 0);
            enemies[5].home = new Vector3(3, 0, 0);
            enemies[6].home = new Vector3(3, -2.5f, 0);
        }
        else if (enemies.Count == 8)
        {
            enemies[0].home = new Vector3(5, 3, 0);
            enemies[1].home = new Vector3(5, 1, 0);
            enemies[2].home = new Vector3(5, -1, 0);
            enemies[3].home = new Vector3(5, -3, 0);
            enemies[4].home = new Vector3(3, 3, 0);
            enemies[5].home = new Vector3(3, 1, 0);
            enemies[6].home = new Vector3(3, -1, 0);
            enemies[7].home = new Vector3(3, -3, 0);
        }
        else if (enemies.Count == 9)
        {
            enemies[0].home = new Vector3(5, 2, 0);
            enemies[1].home = new Vector3(5, 0, 0);
            enemies[2].home = new Vector3(5, -2, 0);
            enemies[3].home = new Vector3(3, 2, 0);
            enemies[4].home = new Vector3(3, 0, 0);
            enemies[5].home = new Vector3(3, -2, 0);
            enemies[6].home = new Vector3(1, 2, 0);
            enemies[7].home = new Vector3(1, 0, 0);
            enemies[8].home = new Vector3(1, -2, 0);
        }
        else if (enemies.Count == 10)
        {
            enemies[0].home = new Vector3(5, 2, 0);
            enemies[1].home = new Vector3(5, 0, 0);
            enemies[2].home = new Vector3(5, -2, 0);
            enemies[3].home = new Vector3(3, 3, 0);
            enemies[4].home = new Vector3(3, 1, 0);
            enemies[5].home = new Vector3(3, -1, 0);
            enemies[6].home = new Vector3(3, -3, 0);
            enemies[7].home = new Vector3(1, 2, 0);
            enemies[8].home = new Vector3(1, 0, 0);
            enemies[9].home = new Vector3(1, -2, 0);
        }
        else if (enemies.Count == 12)
        {
            enemies[0].home = new Vector3(5, 3, 0);
            enemies[1].home = new Vector3(5, 1, 0);
            enemies[2].home = new Vector3(5, -1, 0);
            enemies[3].home = new Vector3(5, -3, 0);
            enemies[4].home = new Vector3(3, 3, 0);
            enemies[5].home = new Vector3(3, 1, 0);
            enemies[6].home = new Vector3(3, -1, 0);
            enemies[7].home = new Vector3(3, -3, 0);
            enemies[8].home = new Vector3(1, 2, 0);
            enemies[9].home = new Vector3(1, 0, 0);
            enemies[10].home = new Vector3(1, -2, 0);
        }
        else if (enemies.Count == 11)
        {
            enemies[0].home = new Vector3(5, 3, 0);
            enemies[1].home = new Vector3(5, 1, 0);
            enemies[2].home = new Vector3(5, -1, 0);
            enemies[3].home = new Vector3(5, -3, 0);
            enemies[4].home = new Vector3(3, 3, 0);
            enemies[5].home = new Vector3(3, 1, 0);
            enemies[6].home = new Vector3(3, -1, 0);
            enemies[7].home = new Vector3(3, -3, 0);
            enemies[8].home = new Vector3(1, 3, 0);
            enemies[9].home = new Vector3(1, 1, 0);
            enemies[10].home = new Vector3(1, -1, 0);
            enemies[11].home = new Vector3(1, -3, 0);
        }
    }

    public IEnumerator BattleStart()
    {
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
        StartTurn(turn);
    }

    public void EndTurn()
    {
        // Reset state and phase
        turn.state = "";
        turn.phase = 0;

        // Disable walking home
        foreach (Battler battler in turnOrder)
        {
            battler.walkHome = false;
        }

        // Start Battler's next turn
        if (turnOrder.IndexOf(turn) == turnOrder.Count - 1)
        {
            turn = turnOrder[0];
        }
        else
        {
            turn = turnOrder[turnOrder.IndexOf(turn) + 1];
        }
        BetweenTurns();
    }

    public void BetweenTurns()
    {
        // Disable damage bubbles
        foreach (Battler battler in turnOrder)
        {
            battler.damaged = false;
            battler.rb.gravityScale = 0;
            battler.transform.position = battler.home;
        }

        if (turnOrder.Count == 1)
        {
            // All enemies vanquished
            BattleEnd();
        }
        else
        {
            StartTurn(turn);
        }
    }

    public void StartTurn(Battler battler)
    {
        if (battler.name == "PlayerBattle")
        {
            // Player's turn
            battleHUD.battleMenu.SetActive(true);
            battleHUD.selection = battleHUD.attackButton;
            battleHUD.weaponMenu.SetActive(false);
            battleHUD.weaponMenuArrow.gameObject.SetActive(false);
            battleHUD.animator.SetBool("PlayerTurn", true);
        }
        else
        {
            // Enemy's turn
            if (Random.Range(0, 2) == 0)
            {
                StartCoroutine(turn.Roll());
            }
            else
            {
                turn.Tackle();
            }
        }
    }

    public void BattleEnd()
    {
        enemies.Clear();
        turnOrder.Clear();
        levelLoader.LoadLevel(1);
    }

    public IEnumerator ShakeScreen(float time, float intensity)
    {
        Vector3 startingPosition = battleCamera.transform.position;
        for (int i = 0; i < (int)(Time.deltaTime * time); i++)
        {
            battleCamera.transform.position = new Vector3(Random.Range(0, intensity), Random.Range(0, intensity), startingPosition.z);
            yield return new WaitForSeconds(intensity / 10);
            battleCamera.transform.position = startingPosition;
            yield return new WaitForSeconds(intensity / 10);
        }
    }
}
