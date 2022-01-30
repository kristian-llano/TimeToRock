using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseCollide : MonoBehaviour
{
    public PlayerMovement player;

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            player.inverseSpeed();
        }
        Destroy(gameObject);
    }
}
