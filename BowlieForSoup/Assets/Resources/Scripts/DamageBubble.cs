using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBubble : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
