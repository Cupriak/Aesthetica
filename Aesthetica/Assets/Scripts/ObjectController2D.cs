using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController2D : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatCanCarryObject; //what can carry our object if we stomp on it Not finished yet

    [SerializeField] Collider2D groundCheck;

    public bool IsGrounded { get; private set; }
    public bool IsFacingRight { get; private set; }

    Rigidbody2D rb2d;

    private void Awake()
    {
        IsFacingRight = true;
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
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

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

    public void MoveHorizontal(float speed)
    {
        FacingRightCheck(speed);

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

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    public void Teleport(float positionX, float positionY, float positionZ)
    {
        transform.position = new Vector3(positionX, positionY, positionZ);
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
        if (IsGrounded)
        {
            MoveVertical(jumpSpeed);
        }
    }

    public void PreventRotation()
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
