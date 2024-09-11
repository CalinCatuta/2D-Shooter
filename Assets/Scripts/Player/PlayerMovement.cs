using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 4f;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 mousePos;     // Store mouse position in world space
    public Camera cam;            // Reference to the Main Camera to get the mouse position

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update() {
        InputManager();

        // Get mouse position in world coordinates
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate() {
        Move();
        // Move the player
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);

        // Calculate the direction vector between player and mouse position
        Vector2 lookDir = mousePos - rb.position;

        // Calculate the angle in radians and convert to degrees
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // Set the rotation of the player (rotate the z-axis)
        rb.rotation = angle;
    }

    void InputManager() {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;
    }
    void Move() {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
