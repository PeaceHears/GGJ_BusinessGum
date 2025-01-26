using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusinessGuyMovement : MonoBehaviour
{
    public Animator animator;

    public int horizontalSpeed = 10;
    public int verticalSpeed = 2;

    private Rigidbody2D rigidBody;
    private Vector2 inputMovement;
    private bool isReachedToRoofTop = false;
    private bool isBubbleGumReachedToRoofTop = false;
    private bool isDeath = false;

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
            inputMovement.x = horizontalSpeed;
            inputMovement.y = 0;
        }
        else if(isDeath)
        {
            inputMovement.x = 0;
            inputMovement.y *= verticalSpeed * -2.0f;
        }
        else
        {
            inputMovement.y *= verticalSpeed;
        }

        Vector2 delta = inputMovement * Time.fixedDeltaTime;
        Vector2 newPosition = rigidBody.position + delta;
        rigidBody.MovePosition(newPosition);
    }

    private void Walk(float yPositionFactor)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + yPositionFactor);
        animator.SetTrigger("Walk");
        StartCoroutine(LoadScene(2, "WinScene"));
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        isDeath = true;

        StartCoroutine(LoadScene(1, "GameOverScene"));
    }

    IEnumerator LoadScene(int interval, string sceneName)
    {
        yield return new WaitForSeconds(interval);
        SceneManager.LoadScene(sceneName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isBubbleGumReachedToRoofTop)
        {
            return;
        }

        if (collision.gameObject.tag == "RoofTop")
        {
            isReachedToRoofTop = true;
            Walk(4.0f);
        }
    }

    public void BubbleGumTouchedToRoofTop()
    {
        isReachedToRoofTop = true;
        isBubbleGumReachedToRoofTop = true;
        Walk(8.0f);
    }
}
