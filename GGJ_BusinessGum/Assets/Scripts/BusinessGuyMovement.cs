using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessGuyMovement : MonoBehaviour
{
    public GameObject character;
    public GameObject roofTop;
    public Animator animator;


    public int horizontalSpeed = 10;
    public int verticalSpeed = 2;

    private Rigidbody2D rigidBody;
    private Vector2 inputMovement;
    private bool isReachedToRoofTop = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isReachedToRoofTop)
        {
            inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 1);
        }
    }

    private void FixedUpdate()
    {
        inputMovement.x *= horizontalSpeed;

        if (isReachedToRoofTop)
        {
            inputMovement.y = 0;
        }
        else
        {
            inputMovement.y *= verticalSpeed;
        }

        Vector2 delta = inputMovement * Time.fixedDeltaTime;
        Vector2 newPosition = rigidBody.position + delta;
        rigidBody.MovePosition(newPosition);
    }

    private void Walk()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 4.0f);
        animator.SetTrigger("isWalking");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RoofTop")
        {
            isReachedToRoofTop = true;
            Walk();
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
