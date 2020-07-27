using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 10.0f;     // paddle speed
    public float boundY = 2.25f;     // highest position paddle can go
    private Rigidbody2D rb2d;       // reference to Rigidbody object

    // Start is called before the first frame update
    // Init function
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // Will be used to tell what button pressed, and then move paddle
    // Or if no button pressed, then paddle should stay still
    // Is condition checking every frame even efficient???
    // Movement is based on setting velocity in y axis to positive SPEED or negative
    void Update() {
        var vel = rb2d.velocity;
        if (Input.GetKey(moveUp)) {             // set velocity of y-axis to speed
            vel.y = speed;
        } else if (Input.GetKey(moveDown)) {    // set velocity of y-axis to negative speed
            vel.y = -speed;
        } else {                                // no movement
            vel.y = 0;
        }
        rb2d.velocity = vel;

        // bound the paddle between +boundY and -boundY
        var pos = transform.position;
        if (pos.y > boundY) {
            pos.y = boundY;
        } else if (pos.y < -boundY) {
            pos.y = -boundY;
        }
        transform.position = pos;
    }
}
