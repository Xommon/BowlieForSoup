using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public Recipe.Ingredient ingredientType;
    public int[] healths;
    public int mass;
    public float movementSpeed;
    public float sightRadius;
    public Rigidbody2D rb;
    private Player player;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        // Uncomment and adjust the following lines if needed
        /*GameObject carrotObject = new GameObject("Carrot");
        Veggies carrot = carrotObject.AddComponent<Veggies>();

        Debug.Log("Veggie Details:");
        Debug.Log("Name: " + carrot.veggieName);
        Debug.Log("Health: " + carrot.health);
        Debug.Log("Movement Speed: " + carrot.movementSpeed);
        Debug.Log("Sight Range: " + carrot.sightRange);*/
    }

    private void FixedUpdate() 
    {
        // Chase the player if they're in sight
        if (Vector3.Distance(transform.position, player.transform.position) <= sightRadius)
        {
            direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * movementSpeed; // Set velocity directly instead of adding force
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving if player is out of sight
        }
    }
}

[CustomEditor(typeof(Enemy))]
//[ExecuteAlways]
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
