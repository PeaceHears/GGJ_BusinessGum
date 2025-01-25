using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessGuyMovement : MonoBehaviour
{
    public int horizontalSpeed = 10;
    public int verticalSpeed = 2;

    private Rigidbody2D characterBody;
    private Vector2 inputMovement;

    void Start()
    {
        characterBody = GetComponent<Rigidbody2D>();
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
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }
}
