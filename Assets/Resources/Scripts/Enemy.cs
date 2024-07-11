using UnityEngine;
using UnityEditor;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Recipe.Ingredient ingredientType;
    public int health;
    public int threshold;
    public GameObject[] offspring;
    public int mass;
    public float movementSpeed;
    public float sightRadius;
    public Rigidbody2D rb;
    private Player player;
    private Vector2 direction;
    private Hitbox hitbox;
    private bool hit;
    public enum Behaviour { Chase, Flee };
    public Behaviour behaviour;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate() 
    {
        // General behaviour
        if (!hit)
        {
            if (behaviour == Behaviour.Chase)
            {
                // Chase 
                if (Vector3.Distance(transform.position, player.transform.position) <= sightRadius)
                {
                    direction = (player.transform.position - transform.position).normalized;
                    rb.velocity = direction * movementSpeed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
            else if (behaviour == Behaviour.Flee)
            {
                // Flee 
                if (Vector3.Distance(transform.position, player.transform.position) <= sightRadius)
                {
                    direction = (player.transform.position - transform.position).normalized;
                    rb.velocity = direction * movementSpeed * -1;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }

    private void Update() 
    {
        if (!hit && health <= threshold)
        {
            // Break into smaller pieces
            if (offspring.Length > 0)
            {
                foreach (GameObject child in offspring)
                {
                    Instantiate(child, transform.position, Quaternion.identity, null);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hitbox" && !hit)
        {
            hitbox = other.GetComponent<Hitbox>();
            if (hitbox != null)
            {
                SendFlyingBack();
            }
        }
    }

    public void SendFlyingBack()
    {
        hit = true;
        health--;
        StopAllCoroutines();
        Vector2 direction = (transform.position - player.transform.position).normalized;
        float totalForce = hitbox.strength - mass;
        if (totalForce < 0)
        {
            totalForce = 0;
        }
        rb.AddForce(direction * totalForce, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.15f);
        rb.velocity = Vector2.zero;
        hit = false;
    }
}

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    void OnSceneGUI()
    {
        Enemy enemy = (Enemy)target;
        Handles.color = Color.green;
        Handles.DrawWireDisc(enemy.transform.position, Vector3.forward, enemy.sightRadius);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Enemy enemy = (Enemy)target;

        //enemy.sightRadius = EditorGUILayout.FloatField("Radius", enemy.sightRadius);
    }
}
