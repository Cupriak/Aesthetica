using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController2D : MonoBehaviour
{
    private Collider2D objectCollidder;

    public ObjectController2D controller;

    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

    [SerializeField] private Collider2D firstBound;
    [SerializeField] private Collider2D secondBound;

    [SerializeField] private LayerMask whatCanBeCarried;

    public int MovingDirectionX { get; private set; }
    public int MovingDirectionY { get; private set; }

    private void Awake()
    {
        MovingDirectionX = 1;
        MovingDirectionY = 1;
        objectCollidder = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        controller.MoveHorizontal(MovingDirectionX * speedX);
        controller.MoveVertical(MovingDirectionY * speedY);
    }

    public void ChangeDirectionX()
    {
        MovingDirectionX = -MovingDirectionX;
    }

    public void ChangeDirectionY()
    {
        MovingDirectionY = -MovingDirectionY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change way of object
        if (Physics2D.IsTouching(objectCollidder, firstBound) || Physics2D.IsTouching(objectCollidder, secondBound))
        {
            ChangeDirectionX();
            ChangeDirectionY();
        }
    }

    void MoveObject(Rigidbody2D rb2d)
    {
        float paramX = speedX == 0 ? rb2d.velocity.x : MovingDirectionX * speedX;
        //float paramY = speedY == 0 ? rb2d.velocity.y : MovingDirectionY * speedY;

        rb2d.velocity = new Vector2(paramX, rb2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HERE TRY TO GRAB OBJECTS THAT STOMP ON PLATFORM
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanBeCarried))
        {
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                MoveObject(rb2d);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //HERE TRY TO GRAB OBJECTS THAT STOMP ON PLATFORM
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanBeCarried))
        {
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                MoveObject(rb2d);
            }
        }
    }
}
