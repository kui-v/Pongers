using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Function to determine which direction to start moving ball
    void GoBall() {
        float rand = Random.Range(0, 2);
        // AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
        if (rand < 1) {
            rb2d.AddForce(new Vector2(20, -15));    // Vector2: x,y components
        } else {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);    // calls the GoBall function, and how long to wait before invokation
    }

    // Helper funciton to put ball to starting position after each round
    void ResetBall() {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Kicks off the round
    void RestartGame(float wait_time = 1f) {
        ResetBall();
        Invoke("GoBall", wait_time);
    }

    // UNITY RESERVED FUNCTION
    // SENT WHEN AN INCOMING COLLIDER MAKES CONTACT WITH THIS OBJECT'S COLLIDER
    // Adjusts velocity of ball appropriately using speed of the ball and the paddle
    void OnCollisionEnter2D(Collision2D coll) {     // coll is paddle
        if (coll.collider.CompareTag("Player")) {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            //Debug.Log("X: " + vel.x.ToString() + "\t Y: " + vel.y.ToString());
            rb2d.velocity = vel;
        }
    }
}
