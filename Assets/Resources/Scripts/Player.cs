using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float movementSpeed;
    public float damping = 0.9f; // Damping factor to control sliding
    public Rigidbody2D rb;
    private Vector2 movementInput;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        // Move player based on input if dialogue isn't happening
        if (!gameManager.speechBubble.activeInHierarchy)
        {
            movementInput = ControlsManager.Stick(0);
            if (movementInput.magnitude > 1)
            {
                movementInput.Normalize();
            }
        }
        else
        {
            movementInput = Vector2.zero;
        }

        if (movementInput != Vector2.zero)
        {
            // Move the Rigidbody2D to the new position
            rb.MovePosition(rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Apply damping to reduce sliding
            rb.velocity *= damping;
        }
    }
}
