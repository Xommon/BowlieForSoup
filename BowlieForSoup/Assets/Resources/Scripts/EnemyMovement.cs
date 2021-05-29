using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Ingredient ingredient;
    public int level;
    public SpriteRenderer sr;
    public Vector3 spawnPoint;
    public float walkingSpeed;
    public bool wandering;
    public Rigidbody2D rb;
    public Vector2 direction;
    public Ingredient[] ingredientsList;

    private void Awake()
    {
        ingredient = ingredientsList[Random.Range(0, ingredientsList.Length)];
    }

    private void Start()
    {
        spawnPoint = transform.position;
        sr.sprite = ingredient.sprite;
        rb = GetComponent<Rigidbody2D>();

        // Start wandering around
        //StartCoroutine(Wander(walkingSpeed, Random.Range(2.0f, 6.0f)));
        Debug.Log("Start walking");
    }

    private void Update()
    {
        rb.MovePosition(new Vector2(0.05f, 0));
        //transform.position += new Vector3(0.1f, 0, 0);
        /*if (wandering)
        {
            if (Vector3.Distance(transform.position, spawnPoint) < 6)
            {
                transform.position += new Vector3(direction.x, direction.y, 0);
            }
            else
            {
                transform.position += Vector3.MoveTowards(transform.position, spawnPoint, walkingSpeed * Time.deltaTime);
            }
        }*/
    }

    IEnumerator Wander(float speed, float time)
    {
        // Close to spawn point, move away
        wandering = true;
        int multiplierX = Random.Range(-1, 2);
        int multiplierY = 0;
        if (multiplierX == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                multiplierY = -1;
            }
            else
            {
                multiplierY = 1;
            }
        }
        else
        {
            multiplierY = Random.Range(-1, 2);
        }
        direction = new Vector2(speed * multiplierX * Time.deltaTime, speed * multiplierY * Time.deltaTime);

        yield return new WaitForSeconds(time);
        wandering = false;
        StartCoroutine(Rest(Random.Range(0.5f, 2.5f)));
    }

    IEnumerator Rest(float time)
    {
        Debug.Log("Start resting");
        yield return new WaitForSeconds(time);
        StartCoroutine(Wander(walkingSpeed * Time.deltaTime, Random.Range(4.0f, 7.0f)));
    }
}
