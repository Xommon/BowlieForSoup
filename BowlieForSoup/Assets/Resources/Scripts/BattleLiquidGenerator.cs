using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLiquidGenerator : MonoBehaviour
{
    public int liquidCount;
    public GameObject liquidParticle;
    public Color liquidColour;
    public Material liquidMaterial;
    public RecipeManager recipeManager;
    public GameManager gameManager;
    public Transform liquidParent;
    public bool initialFill;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
        gameManager = FindObjectOfType<GameManager>();
        initialFill = true;
    }

    private void Update()
    {
        if (liquidCount < gameManager.playerFill)
        {
            Instantiate(liquidParticle, transform.position, Quaternion.identity, liquidParent);
            liquidCount++;
        }
        else if (liquidCount == gameManager.playerFill)
        {
            initialFill = false;
        }

        // Change colour of broth depending on recipe
        if (recipeManager.currentRecipe == "Creamy Tomato Soup")
        {
            liquidMaterial.color = new Color(1, 0.130889f, 0, 1);
        }
        else if (recipeManager.currentRecipe == "Creamy Carrot Soup")
        {
            liquidMaterial.color = new Color(1, 0.5680981f, 0, 1);
        }
    }
}
