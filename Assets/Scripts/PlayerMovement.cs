using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D body;
    // For better jump
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    // For checking if player is grounded
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    // For checking if player is near ledge
    public float rememberGroundedFor;
    float lastTimeGrounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }
    private void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flips player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

    }
    private void Jump() {
        if (Input.GetKey(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor)) {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }
    // Credit to Board to Bits Games on youtube and his tutorial:
    // "Better Jumping in Unity With Four Lines of Code"
    private void BetterJump() {
        if (body.velocity.y < 0) {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    // Creates a circle at player's feet to check if player is on the ground
    private void CheckIfGrounded() {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null) {
            isGrounded = true;
        }
        else {
            if (isGrounded) {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }
}
