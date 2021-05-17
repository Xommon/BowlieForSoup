using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public string currentRecipe;
    public Dictionary<string, int> ingredientsList = new Dictionary<string, int>();
    public PlayerMovement player;
    public GameManager gameManager;

    // HUD
    public TextMeshProUGUI recipeText;
    public List<string> ingredients = new List<string>();
    public TextMeshProUGUI ingredientsText;
    public GameObject downArrow;
    public GameObject upArrow;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    /*private void Update()
    {
        if (Input.GetButtonDown("RightShoulder") && ingredients.Count > 2)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredientsText.text == ingredients[i] + "\n" + ingredients[i + 1] && (i < ingredients.Count - 2))
                {
                    ingredientsText.text = ingredients[i + 1] + "\n" + ingredients[i + 2];

                    if (i < ingredients.Count - 3)
                    {
                        downArrow.SetActive(true);
                    }
                    else
                    {
                        downArrow.SetActive(false);
                    }

                    break;
                }
            }
        }
        else if (Input.GetButtonDown("LeftShoulder") && ingredients.Count > 2)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredientsText.text == ingredients[i] + "\n" + ingredients[i + 1] && (i > ingredients.Count - 1))
                {
                    ingredientsText.text = ingredients[i - 1] + "\n" + ingredients[i - 2];

                    if (i > ingredients.Count - 3)
                    {
                        upArrow.SetActive(true);
                    }
                    else
                    {
                        upArrow.SetActive(false);
                    }

                    break;
                }
            }
        }
    }*/

    public void UploadNewRecipe(string recipeName, string[] ingredientsNeeded, int[] amountOfIngredientsNeeded)
    {
        recipeText.text = recipeName;
        currentRecipe = recipeName;
        ingredients.Clear();
        int i = 0;
        foreach (string ingredient in ingredientsNeeded)
        {
            ingredientsList.Add(ingredient, amountOfIngredientsNeeded[i]);
            ingredients.Add(ingredient + " - 0/" + ingredientsList[ingredient] + "\n");
            ingredientsText.text += ingredient + " - 0/" + ingredientsList[ingredient] + "\n";
            i++;
        }
        if (ingredients.Count == 1)
        {
            ingredientsText.text = ingredients[0];
        }
        else if (ingredients.Count == 2)
        {
            ingredientsText.text = ingredients[0] + "\n" + ingredients[1];
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
        if (gameManager.playerFill >= 95)
        {
            return 5;
        }
        else if (gameManager.playerFill >= 90)
        {
            return 4;
        }
        else if (gameManager.playerFill >= 80)
        {
            return 3;
        }
        else if (gameManager.playerFill >= 70)
        {
            return 2;
        }

        return 1;
    }
}
