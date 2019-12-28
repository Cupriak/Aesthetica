using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is responsible for controlling player
/// </summary>
public class PlayerController2D : MonoBehaviour
{
    #region Basic Public Attributes
    /// <summary>
    /// Used to be able to move player
    /// </summary>
    public ObjectController2D controller;
    /// <summary>
    /// Used to change health of player
    /// </summary>
    public AttributesController attributes;
    #endregion

    #region Basic Private Attributes
    /// <summary>
    /// Determines player run speed
    /// </summary>
    [SerializeField] private float runSpeed;
    /// <summary>
    /// Determines player jump speed
    /// </summary>
    [SerializeField] private float jumpSpeed;
    /// <summary>
    /// Flag that determine if player should rotate
    /// </summary>
    [SerializeField] private bool rotate;

    /// <summary>
    /// Reference to object animator 
    /// </summary>
    private Animator animator;
    /// <summary>
    /// Reference to object sprite renderer
    /// </summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// Start color of player
    /// </summary>
    private Color basicColor;
    #endregion

    #region Damage Attributes
    /// <summary>
    /// LayerMask that defines what can hurt player
    /// </summary>
    [SerializeField] private LayerMask whatIsEnemy;
    /// <summary>
    /// Timer that is used to track how long player should blink in red color when hurt and be immortal
    /// </summary>
    [SerializeField] private Timer immortalTimer;
    /// <summary>
    /// Amount of time that player should be immortal after getting hurt in seconds
    /// </summary>
    [SerializeField] private float immortalTime;
    /// <summary>
    /// Flag that determines if player can be controlled
    /// </summary>
    [HideInInspector] public bool canBeControlled;
    /// <summary>
    /// Flag that determines is player can take damage
    /// </summary>
    private bool isImmortal;
    #endregion

    #region Death Attributes
    /// <summary>
    /// Prefab that will be spawned upon death to make death effect
    /// </summary>
    [SerializeField] private GameObject deathPrefab;
    #endregion

    #region Water Attributes
    /// <summary>
    /// Flag that determines if player is in water
    /// </summary>
    private bool isInWater;
    /// <summary>
    /// Initial Jump speed. Need to be saved because jump speed is changing in water
    /// </summary>
    private float initialJumpSpeed;
    #endregion

    #region Jump Attributes
    /// <summary>
    /// Timer that calculates how much time must pass before player can jump again.
    /// </summary>
    [SerializeField] private Timer jumpTimer;
    /// <summary>
    /// Collider that checks if player is grounded
    /// </summary>
    [SerializeField] Collider2D groundCheck;
    #endregion

    #region Methods Derived From Monobehaviour
    /// <summary>
    /// Call on object creation. Initialization of basic attributes.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;

        initialJumpSpeed = jumpSpeed;
    }

    /// <summary>
    /// Call every frame. Get input from player and call animation function
    /// </summary
    private void Update()
    {
        InputHelper.GetInput();
        if (!UIController.isGamePaused)
        {
            Animate();
        }
    }

    /// <summary>
    /// Call fixed times per second. Movement and damage attributes are updated here
    /// </summary>
    private void FixedUpdate()
    {
        //Stop hurt animation and immortality after certain time
        HurtHandler();

        DeathHandler();

        //Movement method
        MovementControl();
    }

    /// <summary>
    /// Call when player enters trigger.
    /// Detect if player is in water. In case of that call OnWaterEnter method
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WATER DETECTON
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Water"))
        {
            OnWaterEnter();
        }
    }

    /// <summary>
    /// Call when player stays in trigger.
    /// Detect if player touch enemy. In case of that call OnEnemyTouch method
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (!isImmortal && LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }

    /// <summary>
    /// Call when player exits trigger.
    /// Detect if player is left water. In case of that call OnWaterExit method
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //LEAVING WATER
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Water"))
        {
            OnWaterExit();
        }
    }
    #endregion

    #region Local Methods 
    /// <summary>
    /// Playing proper animation and blink red color if player is hurt
    /// Rotates object if necessary.
    /// </summary>
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

    /// <summary>
    /// Move player according to button that are pressed
    /// </summary>
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

    /// <summary>
    /// Play death sound, spawn death prefab and destroy player game object
    /// </summary>
    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// Check if player is alive
    /// </summary>
    private void DeathHandler()
    {
        if(!attributes.IsAlive)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Check if player is hurt
    /// </summary>
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

    /// <summary>
    /// Take hp, play hurt sound and push player away from enemy
    /// Also prevent player from controlling character for some time and turn on immortality
    /// </summary>
    private void OnEnemyTouch()
    {
        isImmortal = true;
        attributes.TakeDamage(1);
        immortalTimer.StartTimer(immortalTime);

        canBeControlled = false;
        controller.MoveVertical(2f);
        controller.MoveHorizontal(controller.IsFacingRight ? -2f : 2f);

        FindObjectOfType<AudioManager>().Play("PlayerHurt");
    }

    /// <summary>
    /// Heal player by one health point
    /// </summary>
    public void OnPowerUpTouch()
    {
        attributes.TakeHealth(1);
    }

    /// <summary>
    /// Set drag to 15, flag isInWater to true and multiply jump speed by 2.5
    /// </summary>
    private void OnWaterEnter()
    {
        if (!isInWater)
        {
            controller.SetDrag(15f);
            jumpSpeed *= 2.5f;
        }
        isInWater = true;
    }

    /// <summary>
    /// Set drag to 0, flag isInWater to false and set jump speed to initial jump speed
    /// </summary>
    private void OnWaterExit()
    {
        controller.SetDrag(0f);
        jumpSpeed = initialJumpSpeed;
        controller.MoveVertical(jumpSpeed / 2f); // fixed vertical speed while jumping out of water
        isInWater = false;
    }
    #endregion
}
