using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlaneMovement : MonoBehaviour
{
    public int flightSpeed = 5;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Vector2 inputMovement;
    private bool isCollidedToSkyscraper = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        GameObject businessGuy = GameObject.FindWithTag("BusinessGuy");

        int randomXPositionControl = Random.Range(0, 2);
        float randomNumberForXPosition = randomXPositionControl == 0 ? - 9.9f : 10.5f;
        float randomNumberForYPosition = Random.Range(businessGuy.transform.position.y + 10.0f, 
            businessGuy.transform.position.y + 20.0f);

        velocity = new Vector2(flightSpeed, flightSpeed);
        inputMovement = new Vector2(-1, 0);
        transform.position = new Vector2(randomNumberForXPosition, randomNumberForYPosition);

        if (randomNumberForXPosition < 0)
        {
            inputMovement.x *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
    }

    void FixedUpdate()
    {
        velocity.x = flightSpeed;
        velocity.y = flightSpeed;

        if(isCollidedToSkyscraper)
        {
            inputMovement.y = -1;
        }

        Vector2 delta = inputMovement * velocity * Time.fixedDeltaTime;
        Vector2 newPosition = rigidBody.position + delta;
        rigidBody.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Skyscraper")
        {
            isCollidedToSkyscraper = true;
            rigidBody.freezeRotation = false;
        }
        else if (collision.gameObject.tag == "Bird")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "PaperPlane")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
