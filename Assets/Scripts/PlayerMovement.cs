using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D body;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
    }
    private void Move() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
    }
    private void Jump() {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
            body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
    // Creates a circle at player's feet to check if player is on the ground
    private void CheckIfGrounded() {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }
}
