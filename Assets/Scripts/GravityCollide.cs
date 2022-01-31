using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCollide : MonoBehaviour
{
    public Rigidbody2D player;

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            player.gravityScale *= -1;
        }
        Destroy(gameObject);
    }
}
