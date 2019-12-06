using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatformController2D : MonoBehaviour
{
    Collider2D platformCollider;

    [SerializeField] ObjectController2D controller;

    [SerializeField] float horizontalSpeed;

    [SerializeField] Collider2D leftBound;
    [SerializeField] Collider2D rightBound;

    [SerializeField] LayerMask whatCanBeTakenByPlatform;

    public int MovingDirection { get; private set; }

    private void Awake()
    {
        MovingDirection = 1;
        platformCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate ()
    {
        controller.MoveHorizontal(MovingDirection * horizontalSpeed);
    }

    public void ChangeDirection()
    {
        MovingDirection = -MovingDirection;
    }

    public void ChangeDirection(bool moveRight)
    {
        if(moveRight)
        {
            MovingDirection = 1;
        }
        else
        {
            MovingDirection = -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change way of platform
        if (Physics2D.IsTouching(platformCollider, leftBound) || Physics2D.IsTouching(platformCollider, rightBound))
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HERE TRY TO GRAB OBJECTS THAT STOMP ON PLATFORM
        if(LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanBeTakenByPlatform))
        {



            Debug.Log("Horizontal Platform grabbed layer = " + LayerMask.LayerToName(collision.gameObject.layer));
        }
    }
}
