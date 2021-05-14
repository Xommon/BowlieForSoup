using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidGenerator : MonoBehaviour
{
    public int liquidCount;
    public int maxLiquidCount;
    public GameObject liquidParticle;
    public Transform liquidParent;
    public Color liquidColour;
    public Material liquidMaterial;
    public RecipeManager recipeManager;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
    }

    private void Update()
    {
        if (liquidCount < maxLiquidCount && recipeManager.currentRecipe != "")
        {
            Instantiate(liquidParticle, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity, liquidParent);
            liquidCount++;
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
