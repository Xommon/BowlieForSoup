using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleHUD : MonoBehaviour
{
    public BattleManager battleManager;
    public Animator animator;
    public GameObject battleMenu;
    public GameObject battleMenuArrow;
    public GameObject attackButton;
    public GameObject shieldButton;
    public GameObject itemButton;
    public GameObject fleeButton;
    public GameObject selection;
    public GameObject weaponMenu;
    public RectTransform weaponMenuArrow;
    public int weaponIndex;
    public List<string> attacks = new List<string>();
    public Battler playerBattle;
    public bool reset;

    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        battleManager.battleHUD = this;
        animator = GetComponent<Animator>();
        battleManager.animatorHUD = animator;
        animator.StopPlayback();
        selection = attackButton;

        // Populate attacks
        attacks.Add("KnifeAttack");
        attacks.Add("SpoonAttack");
        attacks.Add("ForkAttack");
    }

    private void Update()
    {
        // Choose option
        if (battleManager.turn != null)
        {
            // Main battle menu
            if (battleManager.turn.name == "PlayerBattle" && !weaponMenu.activeInHierarchy)
            {
                if (Input.GetAxisRaw("Horizontal") == 1) // Right
                {
                    if (selection == attackButton)
                    {
                        selection = itemButton;
                    }
                    else if (selection == shieldButton)
                    {
                        selection = fleeButton;
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") == -1) // Left
                {
                    if (selection == itemButton)
                    {
                        selection = attackButton;
                    }
                    else if (selection == fleeButton)
                    {
                        selection = shieldButton;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") == -1) // Down
                {
                    if (selection == attackButton)
                    {
                        selection = shieldButton;
                    }
                    else if (selection == itemButton)
                    {
                        selection = fleeButton;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") == 1) // Up
                {
                    if (selection == shieldButton)
                    {
                        selection = attackButton;
                    }
                    else if (selection == fleeButton)
                    {
                        selection = itemButton;
                    }
                }
            }
            else if (battleManager.turn.name == "PlayerBattle" && weaponMenu.activeInHierarchy && battleManager.turn.target == null) 
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    // Once the player chooses and attack, move to chosing a target
                    float shortestDistance = 10000;
                    foreach (Battler enemy in battleManager.turnOrder)
                    {
                        if (enemy.name != "PlayerBattle")
                        {
                            float currentCalculation = Vector2.Distance(battleManager.playerBattle.transform.position, enemy.transform.position);
                            if (currentCalculation < shortestDistance)
                            {
                                shortestDistance = currentCalculation;
                                battleManager.playerBattle.target = enemy;
                            }
                        }
                    }
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    weaponMenu.SetActive(false);
                }
            }
            else if (battleManager.turn.name == "PlayerBattle" && weaponMenu.activeInHierarchy && battleManager.turn.target != null)
            {
                // Choose target
                if (Input.GetAxisRaw("Horizontal") == 1) // Right
                {

                }
                else if (Input.GetAxisRaw("Horizontal") == -1) // Left
                {

                }
                else if (Input.GetAxisRaw("Vertical") == -1) // Down
                {

                }
                else if (Input.GetAxisRaw("Vertical") == 1) // Up
                {

                }

                if (Input.GetButtonDown("Fire1"))
                {
                    // Attack target
                    if (weaponIndex == 0)
                    {
                        battleManager.playerBattle.KnifeAttack();
                    }
                    else if (weaponIndex == 1)
                    {
                        battleManager.playerBattle.SpoonAttack();
                    }
                    else if (weaponIndex == 2)
                    {
                        battleManager.playerBattle.ForkAttack();
                    }

                    weaponMenuArrow.gameObject.SetActive(false);
                    weaponMenu.SetActive(false);
                    battleMenu.SetActive(false);
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    // Go back to choose attack
                    battleManager.playerBattle.target = null;
                }
            }
        }

        // Select atack
        if (weaponMenu.activeInHierarchy && battleManager.playerBattle.target == null)
        {
            weaponMenuArrow.anchoredPosition = new Vector3(-325, (weaponIndex * 55) - 50, 0);
            if (Input.GetAxisRaw("Vertical") == 1 && reset)
            {
                reset = false;
                if (weaponIndex + 1 == attacks.Count)
                {
                    weaponIndex = 0;
                }
                else
                {
                    weaponIndex++;
                }
            }
            else if(Input.GetAxisRaw("Vertical") == -1 && reset)
            {
                reset = false;
                if (weaponIndex == 0)
                {
                    weaponIndex = attacks.Count - 1;
                }
                else
                {
                    weaponIndex--;
                }
            }
            else if(Input.GetAxisRaw("Vertical") == 0)
            {
                reset = true;
            }
        }

        if (weaponMenu.activeInHierarchy)
        {
            battleMenuArrow.SetActive(false);
        }
        else
        {
            battleMenuArrow.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (selection == attackButton)
            {
                weaponIndex = 0;
                weaponMenu.SetActive(true);
                weaponMenuArrow.gameObject.SetActive(true);
            }

        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (weaponMenu.activeInHierarchy)
            {
                weaponMenu.SetActive(false);
                weaponMenuArrow.gameObject.SetActive(false);
            }
        }

        // Move arrow
        battleMenuArrow.transform.position = selection.transform.position - new Vector3(70, 0);
    }
}
