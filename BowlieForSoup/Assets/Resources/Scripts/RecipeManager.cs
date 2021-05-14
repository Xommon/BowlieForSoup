using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public string currentRecipe;
    public Dictionary<string, int> ingredientsList = new Dictionary<string, int>();
    public PlayerMovement player;

    // HUD
    public TextMeshProUGUI recipeText;
    public TextMeshProUGUI ingredientsText;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public void UploadNewRecipe(string recipeName, string[] ingredientsNeeded, int[] amountOfIngredientsNeeded)
    {
        recipeText.text = recipeName;
        currentRecipe = recipeName;
        int i = 0;
        foreach (string ingredient in ingredientsNeeded)
        {
            ingredientsList.Add(ingredient, amountOfIngredientsNeeded[i]);
            ingredientsText.text += ingredient + " - 0/" + ingredientsList[ingredient] + "\n";
            i++;
        }
    }

    public void ClearRecipe()
    {
        recipeText.text = "";
        currentRecipe = "";
        ingredientsList.Clear();
        ingredientsText.text = "";
    }

    public int RateSoup()
    {
        if (player.fill >= 95)
        {
            return 5;
        }
        else if (player.fill >= 90)
        {
            return 4;
        }
        else if (player.fill >= 80)
        {
            return 3;
        }
        else if (player.fill >= 70)
        {
            return 2;
        }

        return 1;
    }
}
