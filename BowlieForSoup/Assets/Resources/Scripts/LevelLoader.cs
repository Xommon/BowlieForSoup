using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.1f;
    public GameManager gameManager;
    public BattleManager battleManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Skip the loading screen
            SceneManager.LoadScene(1);
            gameManager.freezeOverworld = true;
        }
        else
        {
            // Destroy enemy in overworld
            if (SceneManager.GetActiveScene().buildIndex == 1 && battleManager.battleInstanceFromOverworld != "")
            {
                Destroy(GameObject.Find(battleManager.battleInstanceFromOverworld));
                battleManager.battleInstanceFromOverworld = "";
            }
            StartCoroutine(UnfreezeOverworld());
            transition.gameObject.SetActive(true);
            transition.Play("blocktransition_end");
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevelIEN(sceneIndex));
    }

    IEnumerator LoadLevelIEN(int sceneIndex)
    {
        transition.SetTrigger("Start");
        gameManager.freezeOverworld = true;

        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(UnfreezeOverworld());
        transition.gameObject.SetActive(true);
        transition.Play("blocktransition_end");

        SceneManager.LoadSceneAsync(sceneIndex);
    }

    IEnumerator UnfreezeOverworld()
    {
        yield return new WaitForSeconds(transitionTime);
        gameManager.freezeOverworld = false;
    }
}
