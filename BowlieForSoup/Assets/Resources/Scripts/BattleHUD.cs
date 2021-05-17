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
    public GameObject weaponMenuArrow;
    public int weaponIndex;
    public List<Action> attacks = new List<Action>();

    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        battleManager.battleHUD = this;
        animator = GetComponent<Animator>();
        battleManager.animatorHUD = animator;
        animator.StopPlayback();
        selection = attackButton;
    }

    private void Update()
    {
        // Choose option
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

        // Select atack
        if (weaponMenu.activeInHierarchy)
        {
            weaponMenuArrow.transform.position = new Vector3(-325, (weaponIndex * 60) - 50, 0);
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                if (weaponIndex + 1 == attacks.Count)
                {
                    weaponIndex = 0;
                }
                else
                {
                    weaponIndex++;
                }
            }
            else if(Input.GetAxisRaw("Vertical") == -1)
            {
                if (weaponIndex == 0)
                {
                    weaponIndex = attacks.Count - 1;
                }
                else
                {
                    weaponIndex--;
                }
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
                weaponMenuArrow.SetActive(true);
            }

        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (weaponMenu.activeInHierarchy)
            {
                weaponMenu.SetActive(false);
                weaponMenuArrow.SetActive(false);
            }
        }

        // Move arrow
        battleMenuArrow.transform.position = selection.transform.position - new Vector3(70, 0);
    }
}
