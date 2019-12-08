using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public ObjectController2D objectController;
    public AttributesController attributesController;

    [SerializeField] Collider2D groundCheck;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool rotate;

    [SerializeField] private Timer immortalTimer;
    [SerializeField] private float immortalTime;
    [SerializeField] private bool canBeControlled;

    private bool isImmortal;

    [SerializeField] private LayerMask whatIsEnemy;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Animate()
    {
        if (InputHelper.horizontalMove != 0 && objectController.IsGrounded)
        {
            animator.Play("PlayerRun");
        }
        else if (objectController.IsGrounded)
        {
            animator.Play("PlayerIdle");
        }
        else
        {
            animator.Play("PlayerJump");
        }

        //Blinking red animation when hurt
        if(isImmortal)
        {
            if (immortalTimer.TimeLeft() % 0.2 < 0.1)
            {
                spriteRenderer.color = new Color(255f, 255f, 255f, 255f);
            }
            else
            {
                spriteRenderer.color = new Color(255f, 0f, 0f, 255f);
            }
        }
    }

    private void MovementControl()
    {
        if (canBeControlled)
        {
            if (rotate)
            {
                objectController.Rotate();
            }

            if (!(InputHelper.horizontalMove == 0 && Physics2D.IsTouchingLayers(groundCheck, LayerMask.GetMask("MovingHorizontalPlatform"))))
            {
                objectController.MoveHorizontal(InputHelper.horizontalMove * runSpeed);
            }

            if (InputHelper.jump)
            {
                objectController.Jump(jumpSpeed);
            }

            if (InputHelper.stop)
            {
                objectController.Stop(true, true);
            }
        }
    }

    private void Update()
    {
        InputHelper.GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        HurtHandler();

        MovementControl();
    }

    private void HurtHandler()
    {
        //HURT IMMORTALITY END
        if (immortalTimer.timeElapsed)
        {
            isImmortal = false;
        }

        if (immortalTimer.TimeLeft() <= immortalTime / 2f)
        {
            canBeControlled = true;
        }
    }

    private void OnEnemyTouch()
    {
        isImmortal = true;
        attributesController.TakeDamage(1);
        immortalTimer.StartTimer(immortalTime);

        canBeControlled = false;
        objectController.MoveVertical(2f);
        objectController.MoveHorizontal(objectController.IsFacingRight ? -2f : 2f);

        Debug.Log("HP Left = " + attributesController.Health);
    }

    //FIX THAT SOME ENEMIES WILL NOT BE TRIGGERS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (!isImmortal && Physics2D.IsTouchingLayers(playerCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }

        //WATER DETECTON
        if (Physics2D.IsTouchingLayers(playerCollider, LayerMask.GetMask("Water")))
        {
            objectController.SetDrag(5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //LEAVING WATER
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Water"))
        {
            objectController.SetDrag(0f);
        }
    }
}
