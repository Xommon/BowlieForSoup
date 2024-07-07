using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameManager gameManager;
    public int messageIndex;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        if (!gameManager.speechBubble.activeInHierarchy)
        {
            // Start reading sign if no dialogue is happening
            gameManager.StartDialogue(messageIndex);
        }
    }
}
