using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum : MonoBehaviour
{
    private Vector2 scale;
    private float scaleFactor = 0.1f;
    private int growthSpeed = 2;

    void Start()
    {
        scale = transform.localScale;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (scale.x > 3.0f)
            {
                return;
            }

            scale.x += scaleFactor;
            scale.y += scaleFactor;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(scale.x < 0.5f)
            {
                return;
            }

            scale.x -= scaleFactor;
            scale.y -= scaleFactor;
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, scale, growthSpeed * Time.deltaTime);
    }
}
