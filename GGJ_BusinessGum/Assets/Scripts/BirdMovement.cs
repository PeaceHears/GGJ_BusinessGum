using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public int flightSpeed = 2;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Vector2 inputMovement;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        velocity = new Vector2(flightSpeed, flightSpeed);
        inputMovement = new Vector2(1, -0.5f);
    }

    void FixedUpdate()
    {
        velocity.x = flightSpeed;
        velocity.y = flightSpeed;

        Vector2 delta = inputMovement * velocity * Time.fixedDeltaTime;
        Vector2 newPosition = rigidBody.position + delta;
        rigidBody.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Skyscraper")
        {
            if (inputMovement.x < 0)
            {
                inputMovement.x *= -1;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
            }
            else if (inputMovement.x > 0)
            {
                inputMovement.x *= -1;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
            }
        }
    }
}
