using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    Animator animator;
    private bool isFacingRight = false;
    private float moveSpeed = 5.0f; // moving speed of the player
    float startPositionX;
    float moveRange = 10.0f;
    bool isMovingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {

            if (this.transform.position.x < startPositionX+moveRange){
                moveRight();
                if (!isFacingRight)
                {
                    Flip();
                }

            } else
            {
                isMovingRight = false;
                moveLeft();
                if (isFacingRight)
                {
                    Flip();
                }
            }
        } else
        {
            
            if (this.transform.position.x > startPositionX - moveRange)
            {
                moveLeft();
                if (isFacingRight)
                {
                    Flip();
                }
            } else
            {
                isMovingRight=true;
                moveRight();
                if (!isFacingRight)
                {
                    Flip();
                }
            }
        }
    }
    private void Awake()
    {
       
        startPositionX =this.transform.position.x;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void moveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
    private void moveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.position.y < other.gameObject.transform.position.y)
            {
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
            }
            
        }
    }
    IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

}
