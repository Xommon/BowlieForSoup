using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidGenerator : MonoBehaviour
{
    public int liquidCount;
    public int maxLiquidCount;
    public GameObject liquidParticle;
    public Transform liquidParent;

    private void Update()
    {
        if (liquidCount < maxLiquidCount)
        {
            Instantiate(liquidParticle, transform.position, Quaternion.identity, liquidParent);
            liquidCount++;
        }
    }
}
