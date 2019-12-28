using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls jumper enemy
/// </summary>
public class JumperEnemyController2D : MonoBehaviour
{
    #region Basic Public Attributes
    /// <summary>
    /// Used to change health of jumper
    /// </summary>
    public AttributesController attributes;
    /// <summary>
    /// Used to be able to move jumper
    /// </summary>
    public ObjectController2D controller;
    #endregion

    #region Basic Private Attributes
    /// <summary>
    /// Reference to object animator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// Reference to object sprite renderer
    /// </summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// Start color of jumper
    /// </summary>
    private Color basicColor;

    /// <summary>
    /// Reference to jumper collider
    /// </summary>
    [SerializeField] private Collider2D jumperCollider;

    /// <summary>
    /// Vertical speed of jumper
    /// </summary>
    [SerializeField] private float runSpeed;
    /// <summary>
    /// Jump speed of jumper
    /// </summary>
    [SerializeField] private float jumpSpeed;
    #endregion

    #region Trigger Attributes
    /// <summary>
    /// Flag that is used to check if jumper is triggerd
    /// </summary>
    public bool IsTriggered { get; set; }
    /// <summary>
    /// Transform of target that jumper is going to shoot
    /// </summary>
    public Transform Target { get; set; }
    #endregion

    #region Damage Attributes
    /// <summary>
    /// LayerMask that defines what can hurt jumper
    /// </summary>
    [SerializeField] private LayerMask whatIsEnemy;
    /// <summary>
    /// Timer that is used to track how long jumper should blink in red color when hurt
    /// </summary>
    [SerializeField] private Timer hurtTimer;
    /// <summary>
    /// Flag that is used to check if jumper is hurt
    /// </summary>
    private bool isHurt;
    #endregion

    #region Death Attributes
    /// <summary>
    /// Prefab that will be spawned upon death to make death effect
    /// </summary>
    [SerializeField] private GameObject deathPrefab;
    #endregion

    #region AI Attributes
    /// <summary>
    /// Reference to enemy weapon that is attached to game object
    /// </summary>
    [SerializeField] private EnemyWeapon weapon;
    /// <summary>
    /// Timer that controlls how fast jumper can shoot
    /// </summary>
    [SerializeField] private Timer shootTimer;
    /// <summary>
    /// Flag that check if jumper can shoot again
    /// </summary>
    private bool canShoot;
    /// <summary>
    /// Point where bullets will be spawned when enemy shoot
    /// </summary>
    [SerializeField] private Transform shootStartPoint;
    /// <summary>
    /// Probability to jump when its possible from 0 to 1.
    /// </summary>
    [Range(0, 1)] [SerializeField] private float probabilityToJump;
    /// <summary>
    /// Lenght between shoot start point to target that is required to trigger shoot
    /// </summary>
    [Range(0, 1)] [SerializeField] private float shootingRange;
    /// <summary>
    /// Time that need to pass before jumper reloads in seconds
    /// </summary>
    [Range(0, 1)] [SerializeField] private float reloadTime;
    /// <summary>
    /// Defines how active jumper will be when idle. It affects its jump probability speed speed and run speed
    /// </summary>
    [Range(0, 1)] [SerializeField] private float idleActiveness;
    #endregion

    #region Side Detection Attributes
    /// <summary>
    /// Direction in which way jumper moves. Should be set to -1 for left side or 1 for right side.
    /// This value is set by JumperSideCollisionDetection script
    /// </summary>
    [HideInInspector] public int movingDirection;
    #endregion

    #region Method Derived From MonoBehaviour
    /// <summary>
    /// Called on object creation.
    /// Initialization of basic attributes
    /// </summary>
    public void Awake()
    {
        attributes = GetComponent<AttributesController>();
        controller = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;
        movingDirection = 1;
        canShoot = true;
    }

    /// <summary>
    /// Called every frame.
    /// Main object logic.
    /// </summary>
    private void Update()
    {
        Animate();
        HurtHandler();

        if (IsTriggered)
        {
            JumperFightAI();
        }
        else
        {
            JumperIdleAI();
        }

        if (!attributes.IsAlive)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Call when jumper enters trigger.
    /// Check if jumper touch enemy and if so call OnEnemyTouch function
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(jumperCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
    #endregion

    #region Local Methods
    /// <summary>
    /// Playing proper animations. Blink red colour if hurt.
    /// </summary>
    private void Animate()
    {
        if (controller.IsGrounded)
        {
            animator.Play("JumperIdle");
        }
        else
        {
            animator.Play("JumperJump");
        }
        //Blinking red animation when hurt
        if (isHurt)
        {
            if (hurtTimer.TimeLeft() % 0.2 < 0.1)
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
    /// AI Method that is responsibe for idle behaviour of jumper
    /// </summary>
    private void JumperIdleAI()
    {
        controller.MoveHorizontal(movingDirection * runSpeed * idleActiveness);
        if (Random.value < probabilityToJump * idleActiveness*0.7f)
        {
            controller.Jump(jumpSpeed * (idleActiveness*2.5f));
        }
    }

    /// <summary>
    /// AI Methrod that is responsible for fighting target
    /// </summary>
    private void JumperFightAI()
    {
        controller.MoveHorizontal(movingDirection * runSpeed);
        if (Random.value < probabilityToJump)
        {
            controller.Jump(jumpSpeed);
        }

        float aroundDistance = shootingRange;
        bool beAround = Target.position.x > shootStartPoint.position.x - aroundDistance && Target.position.x < shootStartPoint.position.x + aroundDistance ? true : false;

        if (beAround && canShoot)
        {
            weapon.Shoot();
            canShoot = false;
            shootTimer.StartTimer(reloadTime);
        }

        if (shootTimer.timeElapsed)
        {
            canShoot = true;
        }

    }

    /// <summary>
    /// Check if hurt timer time elapsed.
    /// If so set isHurt flat to flase
    /// </summary>
    private void HurtHandler()
    {
        if (hurtTimer.timeElapsed)
        {
            isHurt = false;
        }
    }

    /// <summary>
    /// Play jumper death sound, destroy crab game object and spawn death prefab on its place
    /// </summary>
    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("JumperDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// Take damage, set hurt timer to 0.5sec and set isHurt flag to true
    /// </summary>
    private void OnEnemyTouch()
    {
        attributes.TakeDamage(1);
        hurtTimer.StartTimer(0.5f);
        isHurt = true;
    }
    #endregion
}
