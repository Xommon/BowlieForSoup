using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float damping = 0.9f; // Damping factor to control sliding
    public Rigidbody2D rb;
    private Vector2 movementInput;

    void FixedUpdate()
    {
        // Move player based on input
        movementInput = ControlsManager.Stick(0);
        if (movementInput.magnitude > 1)
        {
            movementInput.Normalize();
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
