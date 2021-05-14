using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    public BattleManager battleManager;
    public Animator animator;
    public GameObject battleMenuArrow;

    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        animator = GetComponent<Animator>();
        battleManager.animatorHUD = animator;
        animator.StopPlayback();
    }
}
