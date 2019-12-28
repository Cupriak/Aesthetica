using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls crab enemy
/// </summary>
public class CrabEnemyController2D : MonoBehaviour
{
    #region Basic Public Attributes
    /// <summary>
    /// Used to change health of crab
    /// </summary>
    public AttributesController attributes;
    /// <summary>
    /// Used to be able to move crab
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
    /// Start color of crab
    /// </summary>
    private Color basicColor;
    /// <summary>
    /// Reference to crab collider
    /// </summary>
    [SerializeField] private Collider2D crabCollider;
    #endregion

    #region Trigger Attributes
    /// <summary>
    /// Flag that is used to check if crab is triggerd
    /// </summary>
    public bool IsTriggered { get; set; }
    /// <summary>
    /// Transform of target that crab is going to chase
    /// </summary>
    public Transform Target { get; set; }
    #endregion

    #region Damage Attributes
    /// <summary>
    /// LayerMask that defines what can hurt crab
    /// </summary>
    [SerializeField] private LayerMask whatIsEnemy;
    /// <summary>
    /// Timer that is used to track how long crab should blink in red color when hurt
    /// </summary>
    [SerializeField] private Timer hurtTimer;
    /// <summary>
    /// Flag that is used to check if crab is hurt
    /// </summary>
    private bool isHurt;
    #endregion

    #region Chase AI Attributes
    /// <summary>
    /// Script responsible for idle AI of crab
    /// </summary>
    [SerializeField] private MovingObjectController2D idleScript;
    /// <summary>
    /// Starting point of the crab. When trigger ends it will go back to this position.
    /// </summary>
    [SerializeField] private Transform startPoint;
    /// <summary>
    /// Flag that checks if crab came to starting point
    /// </summary>
    private bool getBackToStartPoint;
    /// <summary>
    /// Speed of crab while chasing target
    /// </summary>
    [SerializeField] private float chaseSpeed;
    /// <summary>
    /// Flag used to check if crab is chasing target
    /// </summary>
    private bool isChasing;
    #endregion

    #region Death Attributes
    /// <summary>
    /// Prefab that will be spawned upon death to make death effect
    /// </summary>
    [SerializeField] private GameObject deathPrefab;
    #endregion

    #region Methods Derived From Monobehaviour
    /// <summary>
    /// Call on object creation. Initialization of basic attributes.
    /// </summary>
    public void Awake()
    {
        attributes = GetComponent<AttributesController>();
        controller = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;

        spriteRenderer.flipX = true; // crab sprite looks to the left -> we want it to look right
    }

    /// <summary>
    /// Call every frame. Main crab logic.
    /// </summary>
    private void Update()
    {
        Animate();
        HurtHandler();

        ChaseAI();

        if (!attributes.IsAlive)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Call when crab enters trigger.
    /// Check if crab touch enemy and if so call OnEnemyTouch function
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(crabCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
    #endregion

    #region Local Methods 
    /// <summary>
    /// Playing proper animation based on isChasing flag. 
    /// Rotates object if necessary.
    /// </summary>
    private void Animate()
    {
        controller.Rotate();
        if (isChasing)
        {
            animator.Play("CrabWalk");
        }
        else
        {
            animator.Play("CrabIdle");
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
    /// Play crab death sound, destroy crab game object and spawn death prefab on its place
    /// </summary>
    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("CrabDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject, Time.deltaTime);
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

    /// <summary>
    /// AI method that is responsible for going back to start position
    /// </summary>
    private void GoBackToPositionAI()
    {
        float aroundDistance = 0.05f;
        bool beAround = startPoint.position.x > transform.position.x - aroundDistance && startPoint.position.x < transform.position.x + aroundDistance ? true : false;
        if (beAround)
        {
            getBackToStartPoint = true;
        }

        if(!getBackToStartPoint)
        {
            if (startPoint.position.x > transform.position.x)
            {
                controller.MoveHorizontal(chaseSpeed);
            }
            else
            {
                controller.MoveHorizontal(-chaseSpeed);
            }
        }
    }

    /// <summary>
    /// AI method that is responsible for chasing target
    /// </summary>
    private void ChaseTargetAI()
    {
        float aroundDistance = 0.05f;
        bool beAround = Target.position.x > transform.position.x - aroundDistance && Target.position.x < transform.position.x + aroundDistance ? true : false;

        if(!beAround)
        {
            if (Target.position.x > transform.position.x)
            {
                controller.MoveHorizontal(chaseSpeed);
            }
            else
            {
                controller.MoveHorizontal(-chaseSpeed);
            }
        }
        else
        {
            controller.Stop(true, false);
        }
    }

    /// <summary>
    /// Main AI logic. Basing on flags calls proper AI methods and set flags
    /// </summary>
    private void ChaseAI()
    {
        if (IsTriggered)
        {
            idleScript.enabled = false;
            getBackToStartPoint = false;
            isChasing = true;
            ChaseTargetAI();
        }
        else
        {
            isChasing = false;
            GoBackToPositionAI();

            if (getBackToStartPoint)
            {
                idleScript.enabled = true;
            }
        }
    }
    #endregion
}
