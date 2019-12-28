using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls octopus enemy
/// </summary>
public class OctopusEnemyController2D : MonoBehaviour
{
    #region Basic Public Attributes
    /// <summary>
    /// Used to change health of octopus
    /// </summary>
    public AttributesController attributesController;
    /// <summary>
    /// Used to be able to move octopus
    /// </summary>
    public ObjectController2D objectController;
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
    /// Reference to octopus collider
    /// </summary>
    private Collider2D octopusCollider;
    /// <summary>
    /// Start color of octopus
    /// </summary>
    private Color basicColor;
    #endregion

    #region Trigger Attributes
    /// <summary>
    /// Flag that is used to check if octopus is triggerd
    /// </summary>
    public bool IsTriggered { get; set; }
    /// <summary>
    /// Transform of target that octopus is going to chase
    /// </summary>
    public Transform Target { get; set; }
    #endregion

    #region Damage Attributes
    /// <summary>
    /// LayerMask that defines what can hurt octopus
    /// </summary>
    [SerializeField] private LayerMask whatIsEnemy;
    /// <summary>
    /// Timer that is used to track how long octopus should blink in red color when hurt
    /// </summary>
    [SerializeField] private Timer hurtTimer;
    /// <summary>
    /// Flag that is used to check if octopus is hurt
    /// </summary>
    private bool isHurt;
    #endregion

    #region Death Attributes
    /// <summary>
    /// Prefab that will be spawned upon death to make death effect
    /// </summary>
    [SerializeField] private GameObject deathPrefab;
    #endregion

    #region Shoot AI Attributes
    /// <summary>
    /// Reference to enemy weapon attached to the object
    /// </summary>
    [SerializeField] private EnemyWeapon weapon;
    /// <summary>
    /// Timer to measure how long octopus need to wait before another shoot
    /// </summary>
    [SerializeField] private Timer shootTimer;
    /// <summary>
    /// Flag to check if octopus can shoot again
    /// </summary>
    private bool canShoot;
    #endregion

    #region Methods Derived From Monobehaviour
    /// <summary>
    /// Call on object creation. Initialization of basic attributes.
    /// </summary>
    public void Awake()
    {
        attributesController = GetComponent<AttributesController>();
        objectController = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;
        octopusCollider = GetComponent<Collider2D>();

        spriteRenderer.flipX = true; // octopus sprite looks to the left -> we want it to look right

        canShoot = true;
    }

    /// <summary>
    /// Call every frame. Main octopus logic.
    /// </summary>
    private void Update()
    {
        Animate();
        HurtHandler();

        ShootAI();

        if (!attributesController.IsAlive)
        {
            OnDeath();
        }

        if (IsTriggered)
        {
            LookAtTarget();
        }
    }

    /// <summary>
    /// Call when octopus enters trigger.
    /// Check if octopus touch enemy and if so call OnEnemyTouch function
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(octopusCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
    #endregion

    #region Local Methods
    /// <summary>
    /// Basing on target position set IsFacingRight flag in objectController
    /// </summary>
    private void LookAtTarget()
    {
        if(Target.position.x > transform.position.x)
        {
            objectController.IsFacingRight = true;
        }
        else
        {
            objectController.IsFacingRight = false;
        }
    }

    /// <summary>
    /// Playing animation and blink with red color if hurt. 
    /// Rotates object if necessary
    /// </summary>
    private void Animate()
    {
        objectController.Rotate();
        animator.Play("OctopusIdle");

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
        if(hurtTimer.timeElapsed)
        {
            isHurt = false;
        }
    }

    /// <summary>
    /// Play octopus death sound, destroy octopus game object and spawn death prefab on its place
    /// </summary
    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("OctopusDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject, Time.deltaTime);
    }

    /// <summary>
    /// Take damage, set hurt timer to 0.5sec and set isHurt flag to true
    /// </summary>
    private void OnEnemyTouch()
    {
        attributesController.TakeDamage(1);
        hurtTimer.StartTimer(0.5f);
        isHurt = true;
    }

    /// <summary>
    /// AI method that is responsible for shooting to target
    /// </summary>
    private void ShootAI()
    {
        if(IsTriggered)
        {
            float aroundDistance = 0.05f;
            bool beAround = Target.position.y > transform.position.y - aroundDistance && Target.position.y < transform.position.y + aroundDistance ? true : false;

            if (beAround && canShoot)
            {
                weapon.Shoot();
                canShoot = false;
                shootTimer.StartTimer(0.5f);
            }
        }

        if (shootTimer.timeElapsed)
        {
            canShoot = true;
        }
    }
    #endregion
}
