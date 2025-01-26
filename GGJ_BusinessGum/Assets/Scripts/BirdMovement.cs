using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public int flightSpeed = 2;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Vector2 inputMovement;
    private GameObject businessGuy;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        businessGuy = GameObject.FindWithTag("BusinessGuy");

        float randomNumberForXPosition = Random.Range(-5.0f, 10.0f);
        float randomNumberForYPosition = Random.Range(businessGuy.transform.position.y + 20.0f, 
            businessGuy.transform.position.y + 50.0f);

        velocity = new Vector2(flightSpeed, flightSpeed);
        inputMovement = new Vector2(1, -0.5f); //TODO: Maybe up movement too?
        transform.position = new Vector2(randomNumberForXPosition, randomNumberForYPosition);
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
        else if(collision.gameObject.tag == "Bird")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
