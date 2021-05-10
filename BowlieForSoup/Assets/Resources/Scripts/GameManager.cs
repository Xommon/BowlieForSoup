using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool freezeOverworld;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
