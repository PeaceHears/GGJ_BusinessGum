using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int horizontalSpeed = 10;
    public int verticalSpeed = 2;

    private Rigidbody2D characterBody;
    private Vector2 velocity;
    private Vector2 inputMovement;

    void Start()
    {
        velocity = new Vector2(horizontalSpeed, verticalSpeed);
        characterBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 1);

        Vector2 newCameraPosition = characterBody.position;
        newCameraPosition.y = characterBody.position.y;
        characterBody.position = newCameraPosition;
    }

    private void FixedUpdate()
    {
        inputMovement.x *= velocity.x;
        inputMovement.y *= velocity.y;

        Vector2 delta = inputMovement * Time.fixedDeltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }
}
