using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
public enum Ingredient
{
    Avocado,
    Basil,
    Beans,
    Beet,
    BellPepper,
    Bread,
    Broccoli,
    Butter,
    Carrot,
    Celery,
    Cheese,
    Chili,
    Coconut,
    Corn,
    Garlic,
    Ginger,
    GreenOnion,
    Jalapeño,
    Lime,
    Mushroom,
    Egg,
    Onion,
    Plantain,
    Potato,
    Squash,
    SweetPotato,
    Tofu,
    Tomato,
    VeggieSausage,
    Zucchini
}

    public Ingredient[] ingredients;
    public int[] quantities;
}