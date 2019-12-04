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

    /// <summary>
    /// TO BE DELETED PROBABLY DOESNT WORK CUZ OF COLISIONS READONLY
    /// </summary>
    /// <param name="reciver"></param>
    /// <param name="horizontalSpeed"></param>
    /// <param name="verticalSpeed"></param>
    public void TESTAddMove(Rigidbody2D reciver, float horizontalSpeed, float verticalSpeed)
    {
        reciver.velocity = new Vector2(reciver.velocity.x + horizontalSpeed, reciver.velocity.y + verticalSpeed);
        Debug.Log("X = " + reciver.velocity.x.ToString() + "Y = " + reciver.velocity.y.ToString());
    }

    private void Awake()
    {
        isFacingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
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

    //public void Move(float horizontalSpeed, float verticalSpeed)
    //{
    //    facingRightCheck(horizontalSpeed);

    //    rb2d.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    //}

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

    public void GroundCheck()
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
