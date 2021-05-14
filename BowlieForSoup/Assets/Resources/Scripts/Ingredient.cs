using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite sprite;
    public Ingredient[] children;
    public float health;
    public float attack;
    public float defence;
    public float speed;
    public float luck;
}
