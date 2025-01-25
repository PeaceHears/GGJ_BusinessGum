using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessGuyMovement : MonoBehaviour
{
    public GameObject character;

    public int horizontalSpeed = 10;
    public int verticalSpeed = 2;

    private Rigidbody2D rigidBody;
    private Vector2 inputMovement;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 1);
    }

    private void FixedUpdate()
    {
        inputMovement.x *= horizontalSpeed;
        inputMovement.y *= verticalSpeed;

        Vector2 delta = inputMovement * Time.fixedDeltaTime;
        Vector2 newPosition = rigidBody.position + delta;
        rigidBody.MovePosition(newPosition);
    }
}
