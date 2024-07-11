using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class BarUI : MonoBehaviour
{
    [Range(0, 100)]
    public int value;
    public Color[] colours;
    public Image container;
    public Image bar;
    private RectTransform barRT;
    private RectTransform containerRT;
    private Gradient gradient;

    private void Start()
    {
        // Initialize components and gradient
        barRT = bar.GetComponent<RectTransform>();
        containerRT = container.GetComponent<RectTransform>();
        
    }

    private void CreateGradient()
    {
        gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colours.Length];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colours.Length];

        for (int i = 0; i < colours.Length; i++)
        {
            colorKeys[i].color = colours[i];
            colorKeys[i].time = i / (float)(colours.Length - 1); // Distribute color keys evenly
            alphaKeys[i].alpha = 1f;
            alphaKeys[i].time = i / (float)(colours.Length - 1); // Distribute alpha keys evenly
        }

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    private void Update()
    {
        CreateGradient();
        // Ensure components are initialized
        if (barRT == null || containerRT == null)
        {
            // Reinitialize components if null
            barRT = bar.GetComponent<RectTransform>();
            containerRT = container.GetComponent<RectTransform>();
            return;
        }

        int finalValue = 0;
        if (value > 0)
        {
            finalValue = (int)(containerRT.sizeDelta.x * value / 100);
        }

        // Change the size of the fill of the bar
        barRT.sizeDelta = new Vector2(finalValue, containerRT.sizeDelta.y);

        // Adapt the colours using the gradient
        bar.color = gradient.Evaluate(value / 100f);
    }
}
