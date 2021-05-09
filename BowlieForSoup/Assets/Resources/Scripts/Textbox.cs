using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Textbox : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string sampleText;

    // Start is called before the first frame update
    void Start()
    {
        sampleText = "Okay, so your first task is to assemble the ingredients for a tomato soup. You'll need 4 tomato cubes, 1 onion cube, and 1 garlic cube.";
        sampleText.Split('\n');

        StartCoroutine(PrintText(sampleText, 0.025f));
    }

    public IEnumerator PrintText(string textToPrint, float textSpeed)
    {
        for (int i = 0; i < textToPrint.Length; i++)
        {
            textDisplay.text += textToPrint.Substring(i, 1);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
