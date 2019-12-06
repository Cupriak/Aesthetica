using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatformController2D : MonoBehaviour
{
    Collider2D platformCollider;

    [SerializeField] ObjectController2D controller;

    [SerializeField] float verticalSpeed;

    [SerializeField] Collider2D lowerBound;
    [SerializeField] Collider2D upperBound;

    [SerializeField] LayerMask whatCanBeTakenByPlatform;

    public int MovingDirection { get; private set; }

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        MovingDirection = 1;
    }

    void FixedUpdate()
    {
        controller.MoveVertical(MovingDirection * verticalSpeed);
    }

    public void ChangeDirection()
    {
        MovingDirection = -MovingDirection;
    }

    public void ChangeDirection(bool moveUp)
    {
        if (moveUp)
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
        if (Physics2D.IsTouching(platformCollider, lowerBound) || Physics2D.IsTouching(platformCollider, upperBound))
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HERE TRY TO GRAB OBJECTS THAT STOMP ON PLATFORM
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanBeTakenByPlatform))
        {



            Debug.Log("Vertical Platform grabbed layer = " + LayerMask.LayerToName(collision.gameObject.layer));
        }
    }
}
