using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum : MonoBehaviour
{
    public GameObject businessGuy;

    private Vector2 scale;
    private float scaleFactor = 0.1f;
    private int growthSpeed = 2;

    void Start()
    {
        scale = transform.localScale;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (scale.x > 1.5f)
            {
                return;
            }

            scale.x += scaleFactor;
            scale.y += scaleFactor;

            businessGuy.GetComponent<BusinessGuyMovement>().verticalSpeed += 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if(scale.x < 0.5f)
            {
                return;
            }

            scale.x -= scaleFactor;
            scale.y -= scaleFactor;

            businessGuy.GetComponent<BusinessGuyMovement>().verticalSpeed -= 1;
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, scale, growthSpeed * Time.deltaTime);
    }

    private void Burst()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Burst");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Burst();
    }
}
