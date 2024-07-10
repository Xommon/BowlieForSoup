using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RecipeDisplay : MonoBehaviour
{
    public Recipe recipe;
    public GameObject ingredientSlotPrefab;
    private Recipe previousRecipe;
    public Sprite[] ingredientIcons;
    private RectTransform rt;

    public Vector2 debugValues;
    
    void Update()
    {
        if (recipe != previousRecipe)
        {
            // Store references to children before destroying them
            Transform[] children = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i);
            }
            
            // Destroy stored children
            foreach (Transform child in children)
            {
                DestroyImmediate(child.gameObject);
            }

            // Populate new ingredients list
            if (recipe != null)
            {
                for (int i = 0; i < recipe.ingredients.Length; i++)
                {
                    IngredientSlot newEntry = Instantiate(ingredientSlotPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<IngredientSlot>();
                    newEntry.icon.sprite = ingredientIcons[(int)recipe.ingredients[i]];
                    newEntry.text.text = recipe.quantities[i].ToString();
                }
            }

            // Update height of the display
            if (rt == null)
            {
                rt = GetComponent<RectTransform>();
            }
            
            
            previousRecipe = recipe;
        }

        int ingredientLength = 0;
        if (recipe != null)
        {
            ingredientLength = recipe.ingredients.Length;
        }
        rt.anchoredPosition = new Vector2(300, debugValues.x - (2 - Mathf.Ceil(ingredientLength / 2f)) * debugValues.y);

    }
}
