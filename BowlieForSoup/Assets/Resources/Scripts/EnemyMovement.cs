using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Ingredient ingredient;
    public int level;
    public SpriteRenderer sr;

    private void Start()
    {
        sr.sprite = ingredient.sprite;
    }
}
