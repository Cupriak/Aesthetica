using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is responsible for movement in every other classed. Movement is based on rididbody2D component.
/// </summary>
public class ObjectController2D : MonoBehaviour
{
    /// <summary>
    /// LayerMask that determine what is ground
    /// </summary>
    [SerializeField] private LayerMask whatIsGround;

    /// <summary>
    /// Collider that check if object is grounded
    /// </summary>
    [SerializeField] private Collider2D groundCheck;

    /// <summary>
    /// Flag that determine if object is grounded
    /// </summary>
    public bool IsGrounded { get; private set; }
    /// <summary>
    /// Flag that determine if object is currently facing right
    /// </summary>
    public bool IsFacingRight { get; set; }

    /// <summary>
    /// Rigidbody2D reference. Used for movement and physics
    /// </summary>
    private Rigidbody2D rb2d;

    /// <summary>
    /// Called on object creation. 
    /// </summary>
    private void Awake()
    {
        IsFacingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Call every frame. Check if object is on ground
    /// </summary>
    private void Update()
    {
        GroundCheck();
    }

    /// <summary>
    /// Method that checks if player is on ground and set isGrounded flag
    /// </summary>
    private void GroundCheck()
    {
        if (Physics2D.IsTouchingLayers(groundCheck, whatIsGround))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    /// <summary>
    /// Check if object is facing right basing on its horizontal speed
    /// </summary>
    /// <param name="horizontalSpeed">horizontal speed value</param>
    private void FacingRightCheck(float horizontalSpeed)
    {
        if (horizontalSpeed > 0)
        {
            IsFacingRight = true;
        }
        else if (horizontalSpeed < 0)
        {
            IsFacingRight = false;
        }
    }

    /// <summary>
    /// Stop movement of object
    /// </summary>
    /// <param name="horizontal">if true stop horizontal movement</param>
    /// <param name="vertical">if true stop vertical movement</param>
    public void Stop(bool horizontal, bool vertical)
    {
        float multiplierX = horizontal ? 0 : 1;
        float miltiplierY = vertical ? 0 : 1;

        rb2d.velocity = new Vector2(rb2d.velocity.x * multiplierX, rb2d.velocity.y * miltiplierY);
    }

    /// <summary>
    /// Move object horizontally
    /// </summary>
    /// <param name="speed">speed of movement</param>
    public void MoveHorizontal(float speed)
    {
        FacingRightCheck(speed);

        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    /// <summary>
    /// Move object vertically taking side as point of reference
    /// </summary>
    /// <param name="side">point of reference</param>
    /// <param name="speed">speed of movement</param>
    public void MoveHorizontal(Vector3 side, float speed)
    {
        FacingRightCheck(speed);

        rb2d.velocity = side * speed;
    }

    /// <summary>
    /// Move object vertically
    /// </summary>
    /// <param name="speed">speed of movement</param>
    public void MoveVertical(float speed)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
    }

    /// <summary>
    /// Adding velocity to object
    /// </summary>
    /// <param name="horizontalSpeed">horizontal speed to be added</param>
    /// <param name="verticalSpeed">vertical speed to be added</param>
    public void AddMove(float horizontalSpeed, float verticalSpeed)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x + horizontalSpeed, rb2d.velocity.y + verticalSpeed);
    }

    /// <summary>
    /// Teleport object to given location
    /// </summary>
    /// <param name="positionX">x coordinate</param>
    /// <param name="positionY">y coordinate</param>
    /// <param name="positionZ">z coordinate</param>
    public void Teleport(float positionX, float positionY, float positionZ)
    {
        transform.position = new Vector3(positionX, positionY, positionZ);
    }

    /// <summary>
    /// Teleport object to given location
    /// </summary>
    /// <param name="position">coordinates</param>
    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// Teleport object to given location
    /// </summary>
    /// <param name="positionTransform">target position</param>
    public void Teleport(Transform positionTransform)
    {
        transform.position = positionTransform.position;
    }

    /// <summary>
    /// Seting drag of object
    /// </summary>
    /// <param name="drag">dragvalue</param>
    public void SetDrag(float drag)
    {
        rb2d.drag = drag;
    }

    /// <summary>
    /// Move object vertically if it is grounded
    /// </summary>
    /// <param name="jumpSpeed">speed of jump</param>
    public void Jump(float jumpSpeed)
    {
        if (IsGrounded)
        {
            MoveVertical(jumpSpeed);
        }
    }

    /// <summary>
    /// Rotating object right or left depending on IsFacingRight flag
    /// </summary>
    public void Rotate()
    {
        if (IsFacingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
