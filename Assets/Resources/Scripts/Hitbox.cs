using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Rigidbody2D rb;
    public float strength;

    private void Start() 
    {
        Invoke("DestroySelf", 0.25f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
