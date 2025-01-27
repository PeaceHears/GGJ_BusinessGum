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

    private void Walk()
    {
        transform.position = new Vector2(10.0f, 250.0f);
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
            Walk();
        }
    }

    public void BubbleGumTouchedToRoofTop()
    {
        isReachedToRoofTop = true;
        isBubbleGumReachedToRoofTop = true;

        StartCoroutine(WalkAfterBubbleGumBurst());
    }

    IEnumerator WalkAfterBubbleGumBurst()
    {
        yield return new WaitForSeconds(0.7f);
        Walk();
    }
}
