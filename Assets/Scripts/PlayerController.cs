using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f; // moving speed of the player
    [SerializeField] private float jumpForce = 4.0f;
    //[Space(10)]
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    const float rayLength = 1.6f;
    Animator animator;
    private bool isWalking = false;
    private bool isFacingRight = true;
    private int score = 0;
    private bool isLadder = false;
    private bool isClimbing = false;
    private float vertical;
    int lives = 3;
    private Vector2 startPosition;
    int keysFound = 0;
    int keysNumber = 3;
    private void Awake()
    {
        startPosition=transform.position;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Jump()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus"))
        {
            score++;
            Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
        }else if (other.CompareTag("Ladder"))
        {
            isLadder=true;
        } else if (other.CompareTag("Enemy"))
        {
            if (transform.position.y > other.gameObject.transform.position.y)
            {
                score = +1;
                Debug.Log("Killed an enemy");
            }
            else
            {
                transform.position = startPosition;
                if (lives > 0)
                {
                    lives = -1;
                    Debug.Log("You lost a heart. You have " + lives + " hearts remaining");
                } else
                {
                    Debug.Log("You lost the game");
                }
            }
        } else if (other.CompareTag("Key"))
        {
            keysFound++;
            Debug.Log("Key found");
            other.gameObject.SetActive(false);
        } else if (other.CompareTag("Heart"))
        {
            lives++;
            Debug.Log("Life added");
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Home"))
        {
            if(keysFound == keysNumber)
            {
                Debug.Log("Wygrana");
            }else
            {
                Debug.Log("Not enough keys");

            }
        }
        else if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder=false;
            isClimbing = false;
        }
        else if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate()
    {
        if(isClimbing)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, vertical * moveSpeed);
        }
        else
        {
            rigidBody.gravityScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Math.Abs(vertical) > 0)
        {
            isClimbing = true;
        }

        isWalking = false;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking= true;
            if (!isFacingRight) {
                Flip();
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking= true;
            if (isFacingRight) {
                Flip();
            }
        }
        Jump();

        animator.SetBool("isGrounded", isGrounded());
        animator.SetBool("isWalking", isWalking);

        Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 1, false);
    }
}
