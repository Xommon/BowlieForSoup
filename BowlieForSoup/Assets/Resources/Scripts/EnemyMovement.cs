using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Ingredient ingredient;
    public int level;
    public SpriteRenderer sr;
    public Vector3 spawnPoint;
    public float walkingSpeed;
    public bool wandering;

    private void Start()
    {
        spawnPoint = transform.position;
        sr.sprite = ingredient.sprite;
    }

    private void Update()
    {
        
    }
}
