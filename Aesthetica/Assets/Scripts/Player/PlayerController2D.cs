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

    //ENEMY RELATED ATTRIBUTES
    [SerializeField] private Timer immortalTimer;
    [SerializeField] private float immortalTime;
    [HideInInspector] public bool canBeControlled;
    private bool isImmortal;

    //DEATH RELATED ATTRIBUTES
    [SerializeField] private GameObject deathPrefab;

    //WATER RELATED ATTRIBUTES
    private bool isInWater;
    private float initialJumpSpeed;

    //JUMP RELATED ATTRIBUTES
    [SerializeField] private Timer jumpTimer;

    [SerializeField] private LayerMask whatIsEnemy;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        initialJumpSpeed = jumpSpeed;
    }

    private void Animate()
    {
        if (canBeControlled)
        {
            if (InputHelper.horizontalMove != 0 && objectController.IsGrounded && InputHelper.shoot)
            {
                animator.Play("PlayerRunShoot");
            }
            else if (InputHelper.horizontalMove != 0 && objectController.IsGrounded)
            {
                animator.Play("PlayerRun");
            }
            else if (InputHelper.shoot && objectController.IsGrounded)
            {
                animator.Play("PlayerShoot");
            }
            else if (objectController.IsGrounded)
            {
                animator.Play("PlayerIdle");
            }
            else
            {
                animator.Play("PlayerJump");
            }
        }
        else
        {
            animator.Play("PlayerHurt");
        }

        //Blinking red animation when hurt
        if (isImmortal)
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

            if (jumpTimer.timeElapsed && InputHelper.jump)
            {
                objectController.Jump(jumpSpeed);
                jumpTimer.StartTimer(0.4f);
            }

            if (InputHelper.stop)
            {
                objectController.Stop(true, true);
            }
        }
    }

    private void OnDeath()
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void DeathHandler()
    {
        if(!attributesController.IsAlive)
        {
            OnDeath();
        }
    }

    private void Update()
    {
        InputHelper.GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        //Stop hurt animation and immortality after certain time
        HurtHandler();

        DeathHandler();

        //Movement method
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

    private void OnWaterEnter()
    {
        if (!isInWater)
        {
            objectController.SetDrag(15f);
            jumpSpeed *= 2.5f;
        }
        isInWater = true;
    }

    private void OnWaterExit()
    {
        objectController.SetDrag(0f);
        jumpSpeed = initialJumpSpeed;
        objectController.MoveVertical(jumpSpeed / 2f); // fixed vertical speed while jumping out of water
        isInWater = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WATER DETECTON
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Water"))
        {
            OnWaterEnter();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (!isImmortal && Physics2D.IsTouchingLayers(playerCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //LEAVING WATER
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Water"))
        {
            OnWaterExit();
        }
    }
}
