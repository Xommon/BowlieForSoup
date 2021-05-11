using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidInBowl : MonoBehaviour
{
    public PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            player.fill++;
            collision.GetComponent<LiquidParticle>().inBowl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            player.fill--;
            collision.GetComponent<LiquidParticle>().inBowl = false;
        }
    }
}
