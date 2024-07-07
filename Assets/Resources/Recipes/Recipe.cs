using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class IngredientEntry
{
    public Recipe.Ingredient ingredient;
    public int quantity;
}

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
    BlackBeans,
    Bread,
    Broccoli,
    Butter,
    Carrot,
    Celery,
    Cheddar,
    Chili,
    Chives,
    Coconut,
    Corn,
    Garlic,
    Ginger,
    GreenOnion,
    Gruyère,
    Jalapeño,
    Lime,
    Mushroom,
    Noodles,
    Onion,
    Plantain,
    Potato,
    Squash,
    SweetPotato,
    Tofu,
    Tomato,
    Ube,
    Zucchini
}

    [SerializeField]
    private List<IngredientEntry> ingredientEntries = new List<IngredientEntry>();

    public Dictionary<Ingredient, int> Ingredients { get; private set; } = new Dictionary<Ingredient, int>();

    private void OnEnable()
    {
        Ingredients = new Dictionary<Ingredient, int>();
        foreach (var entry in ingredientEntries)
        {
            Ingredients[entry.ingredient] = entry.quantity;
        }
    }

    // Public method to synchronize dictionary and list
    public void SynchronizeDictionary()
    {
        ingredientEntries.Clear();
        foreach (var kvp in Ingredients)
        {
            ingredientEntries.Add(new IngredientEntry { ingredient = kvp.Key, quantity = kvp.Value });
        }
    }
}