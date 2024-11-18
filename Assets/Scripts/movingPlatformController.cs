using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformController : MonoBehaviour
{
    private bool isMovingRight = false;
    private float moveSpeed = 2.71f; // moving speed of the player
    float startPositionX;
    float moveRange = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {

            if (this.transform.position.x < startPositionX + moveRange)
            {
                moveRight();
            }
            else
            {
                isMovingRight = false;
                moveLeft();
            }
        }
        else
        {

            if (this.transform.position.x > startPositionX - moveRange)
            {
                moveLeft();
            }
            else
            {
                isMovingRight = true;
                moveRight();
            }
        }
    }

    private void Awake()
    {
        startPositionX = this.transform.position.x;
        //rigidBody = GetComponent<Rigidbody2D>();
    }
    private void moveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
    private void moveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
}
