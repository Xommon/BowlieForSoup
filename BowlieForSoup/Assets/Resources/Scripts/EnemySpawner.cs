using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public PlayerMovement player;
    public Vector3 viewPos;
    public GameObject enemyPrefab;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy = null;
        int choice = Random.Range(1, 5);
        if (choice == 1)
        {
            // Top
            newEnemy = Instantiate(enemyPrefab, mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), 1.1f, 10.0f)), Quaternion.identity);
        }
        else if (choice == 2)
        {
            // Bottom
            newEnemy = Instantiate(enemyPrefab, mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), -0.1f, 10.0f)), Quaternion.identity);
        }
        else if (choice == 3)
        {
            // Right
            newEnemy = Instantiate(enemyPrefab, mainCamera.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(-0.1f, 1.1f), 10.0f)), Quaternion.identity);
        }
        else if (choice == 4)
        {
            // Left
            newEnemy = Instantiate(enemyPrefab, mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(-0.1f, 1.1f), 10.0f)), Quaternion.identity);
        }
        newEnemy.GetComponent<EnemyMovement>().level = Random.Range(7, 10);
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(SpawnEnemy());
    }
}
