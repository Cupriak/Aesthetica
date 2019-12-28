using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that can move objects harmonic way
/// </summary>
public class MovingObjectController2D : MonoBehaviour
{
    /// <summary>
    /// Object collider reference
    /// </summary>
    private Collider2D objectCollidder;

    /// <summary>
    /// object controller reference to be able to move object
    /// </summary>
    public ObjectController2D controller;

    /// <summary>
    /// Vertical speed
    /// </summary>
    [SerializeField] private float speedX;
    /// <summary>
    /// Horizontal speed
    /// </summary>
    [SerializeField] private float speedY;

    /// <summary>
    /// First bound in between object would move
    /// </summary>
    [SerializeField] private Collider2D firstBound;
    /// <summary>
    /// Second bound in between object would move
    /// </summary>
    [SerializeField] private Collider2D secondBound;

    /// <summary>
    /// LayerMask that define what can be carried by object
    /// </summary>
    [SerializeField] private LayerMask whatCanBeCarried;

    /// <summary>
    /// Horizontal direction where object is moving. 
    /// -1 to move left, 1 to move right
    /// </summary>
    public int MovingDirectionX { get; private set; }
    /// <summary>
    /// Horizontal direction where object is moving. 
    /// -1 to move down, 1 to move up
    /// </summary>
    public int MovingDirectionY { get; private set; }

    /// <summary>
    /// Call on object creation. Initialization of basic variables
    /// </summary>
    private void Awake()
    {
        MovingDirectionX = 1;
        MovingDirectionY = 1;
        objectCollidder = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Called fixed times per second.
    /// Keep moving object vertically and horizontally
    /// </summary>
    void FixedUpdate()
    {
        controller.MoveHorizontal(MovingDirectionX * speedX);
        controller.MoveVertical(MovingDirectionY * speedY);
    }

    /// <summary>
    /// Changes diraction of horizontal movement
    /// </summary>
    public void ChangeDirectionX()
    {
        MovingDirectionX = -MovingDirectionX;
    }

    /// <summary>
    /// Changes diraction of horizontal movement
    /// </summary>
    public void ChangeDirectionY()
    {
        MovingDirectionY = -MovingDirectionY;
    }

    /// <summary>
    /// Call on trigger enter. Check if object touch one of its bounds. If so change diraction of movement
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change way of object
        if (Physics2D.IsTouching(objectCollidder, firstBound) || Physics2D.IsTouching(objectCollidder, secondBound))
        {
            ChangeDirectionX();
            ChangeDirectionY();
        }
    }

    /// <summary>
    /// Move object using its rigidbody
    /// </summary>
    /// <param name="rb2d">Rigidbody2D of object that would be moved</param>
    void MoveObject(Rigidbody2D rb2d)
    {
        float paramX = speedX == 0 ? rb2d.velocity.x : MovingDirectionX * speedX;
        //float paramY = speedY == 0 ? rb2d.velocity.y : MovingDirectionY * speedY;

        rb2d.velocity = new Vector2(paramX, rb2d.velocity.y);
    }

    /// <summary>
    /// Call on collision enter. Detects object that collided can be carried. In that case that object is moved
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanBeCarried))
        {
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                MoveObject(rb2d);
            }
        }
    }

    /// <summary>
    /// Call on collision enter. Detects object that collided can be carried. In that case that object is moved
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
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
