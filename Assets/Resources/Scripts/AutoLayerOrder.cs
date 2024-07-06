using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class AutoLayerOrder : MonoBehaviour
{
    private SpriteRenderer sr;

    void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }
        else if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        else
        {
            sr.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
        }
    }
}
