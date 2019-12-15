using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public ObjectController2D controller;
    public AttributesController attributes;

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
    private Color basicColor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;

        initialJumpSpeed = jumpSpeed;
    }

    private void Animate()
    {
        if (canBeControlled)
        {
            if (InputHelper.horizontalMove != 0 && controller.IsGrounded && InputHelper.shoot)
            {
                animator.Play("PlayerRunShoot");
            }
            else if (InputHelper.horizontalMove != 0 && controller.IsGrounded)
            {
                animator.Play("PlayerRun");
            }
            else if (InputHelper.shoot && controller.IsGrounded)
            {
                animator.Play("PlayerShoot");
            }
            else if (controller.IsGrounded)
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
                spriteRenderer.color = basicColor;
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
                controller.Rotate();
            }

            if (!(InputHelper.horizontalMove == 0 && Physics2D.IsTouchingLayers(groundCheck, LayerMask.GetMask("MovingHorizontalPlatform"))))
            {
                controller.MoveHorizontal(InputHelper.horizontalMove * runSpeed);
            }

            if (jumpTimer.timeElapsed && InputHelper.jump)
            {
                controller.Jump(jumpSpeed);
                jumpTimer.StartTimer(0.4f);
                if(controller.IsGrounded)
                {
                    FindObjectOfType<AudioManager>().Play("PlayerJump");
                }
            }

            if (InputHelper.stop)
            {
                controller.Stop(true, true);
            }
        }
    }

    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void DeathHandler()
    {
        if(!attributes.IsAlive)
        {
            OnDeath();
        }
    }

    private void Update()
    {
        InputHelper.GetInput();
        if(!UIController.isGamePaused)
        {
            Animate();
        }
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
        attributes.TakeDamage(1);
        immortalTimer.StartTimer(immortalTime);

        canBeControlled = false;
        controller.MoveVertical(2f);
        controller.MoveHorizontal(controller.IsFacingRight ? -2f : 2f);
    }

    public void OnPowerUpTouch()
    {
        attributes.TakeHealth(1);
    }

    private void OnWaterEnter()
    {
        if (!isInWater)
        {
            controller.SetDrag(15f);
            jumpSpeed *= 2.5f;
        }
        isInWater = true;
    }

    private void OnWaterExit()
    {
        controller.SetDrag(0f);
        jumpSpeed = initialJumpSpeed;
        controller.MoveVertical(jumpSpeed / 2f); // fixed vertical speed while jumping out of water
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
        //if (!isImmortal && Physics2D.IsTouchingLayers(playerCollider, whatIsEnemy))
        if(!isImmortal && LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatIsEnemy))
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
