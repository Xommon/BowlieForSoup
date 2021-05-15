using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool freezeOverworld;
    public TextMeshProUGUI fpsText;
    public float deltaTime;
    public float fps;
    public Vector3 savedPlayerPosition;
    public int playerFill;
    public PlayerMovement player;
    public TextMeshProUGUI fillDisplay;
    public BattleManager battleManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
        savedPlayerPosition = new Vector3(7.84f, 6.94f, 0.2f);
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void Update()
    {
        // Update fill display
        fillDisplay.text = playerFill + "%";

        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fpsText.text = ((int)(1.0f / deltaTime)).ToString();
    }
}
