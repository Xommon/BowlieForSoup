using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float movementSpeed;
    public float damping = 0.9f; // Damping factor to control sliding
    public Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 direction;
    public GameObject hitBoxPrefab;
    private Hitbox activeHitBox;
    public UnityEvent OnDone, OnBegin;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Only allow these things with speech bubbles aren't open
        if (!gameManager.speechBubble.activeInHierarchy && activeHitBox == null)
        {
            // Move player based on input if dialogue isn't happening
            movementInput = ControlsManager.Stick(0);
            if (movementInput.magnitude > 1)
            {
                movementInput.Normalize();
            }

            // Set direction based on stick input
            if (movementInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;

                // Convert angle to positive value
                if (angle < 0)
                {
                    angle += 360;
                }

                // Determine direction based on angle
                if (angle >= 45 && angle < 135)
                {
                    direction = Vector2.up;
                }
                else if (angle >= 135 && angle < 225)
                {
                    direction = Vector2.left;
                }
                else if (angle >= 225 && angle < 315)
                {
                    direction = Vector2.down;
                }
                else
                {
                    direction = Vector2.right;
                }
            }

            // Interact with objects next to the player
            if (ControlsManager.ButtonDown("A"))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1);

                if (hit.collider != null && hit.collider.CompareTag("Dialogue"))
                {
                    GameObject dialogueObject = hit.collider.gameObject;
                    gameManager.StartDialogue(hit.collider.gameObject.GetComponent<Sign>().messageIndex);
                }
                else
                {
                    // Swing the knife
                    activeHitBox = Instantiate(hitBoxPrefab, (Vector2)transform.position + direction, Quaternion.identity).GetComponent<Hitbox>();
                    activeHitBox.strength = 16;
                }
            }
        }
        else
        {
            movementInput = Vector2.zero;
        }

        // Apply movement directly to velocity for smoother collision handling
        rb.velocity = movementInput * movementSpeed;

        // Apply damping only if no input is received to prevent sliding
        if (movementInput == Vector2.zero)
        {
            rb.velocity *= damping;
        }
    }
}
