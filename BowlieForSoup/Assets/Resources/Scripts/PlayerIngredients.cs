using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    public List<Ingredient> inventory = new List<Ingredient>();

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
