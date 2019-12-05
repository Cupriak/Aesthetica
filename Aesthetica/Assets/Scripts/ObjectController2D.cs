using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController2D : MonoBehaviour
{
    Rigidbody2D rb2d;

    public bool isGrounded { get; set; }
    public bool isFacingRight { get; set; }

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatCanCarryObject;

    [SerializeField] Collider2D groundCheck;

    private void Awake()
    {
        isFacingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        if (Physics2D.IsTouchingLayers(groundCheck, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void facingRightCheck(float horizontalSpeed)
    {
        if (horizontalSpeed > 0)
        {
            isFacingRight = true;
        }
        else if (horizontalSpeed < 0)
        {
            isFacingRight = false;
        }
    }

    public void MoveHorizontal(float speed)
    {
        facingRightCheck(speed);

        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    public void MoveVertical(float speed)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
    }

    public void AddMove(float horizontalSpeed, float verticalSpeed)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x + horizontalSpeed, rb2d.velocity.y + verticalSpeed);
    }

    ////START TEST SETPARENTA
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (Physics2D.IsTouchingLayers(groundCheck, whatCanCarryObject))
    //    {
    //        transform.SetParent(collision.gameObject.transform, false);
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (!Physics2D.IsTouchingLayers(groundCheck, whatCanCarryObject))
    //    {
    //        transform.SetParent(transform, false);
    //    }
    //}
    ////END TEST SETPARENTA

    public void Jump(float jumpSpeed)
    {
        if (isGrounded)
        {
            MoveVertical(jumpSpeed);
        }
    }

    public void PreventRotation()
    {
        if (isFacingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

}
