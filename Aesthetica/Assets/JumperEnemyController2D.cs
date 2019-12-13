using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemyController2D : MonoBehaviour
{
    public AttributesController attributes;
    public ObjectController2D controller;

    //TRIGGER ATTRIBUTES
    //CrabEnemyTrigger script sets that values
    public bool IsTriggered { get; set; }
    public Transform Target { get; set; }

    //DAMAGE ATTRIBUTES
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Timer hurtTimer;
    private bool isHurt;

    //DEATH ATTRIBUTES
    [SerializeField] private GameObject deathPrefab;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Collider2D jumperCollider;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    //AI RELATED ATTRIBUTES
    [SerializeField] private EnemyWeapon weapon;
    [SerializeField] private Timer shootTimer;
    private bool canShoot;
    [SerializeField] private Transform shootStartPoint;


    //JUMPER ENEMY SIDE COLLIDER DETECTION SETS THIS VALUE
    [HideInInspector] public int movingDirection;

    public void Awake()
    {
        attributes = GetComponent<AttributesController>();
        controller = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movingDirection = 1;
        canShoot = true;
    }

    private void Animate()
    {
        if(controller.IsGrounded)
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
                spriteRenderer.color = new Color(255f, 255f, 255f, 255f);
            }
            else
            {
                spriteRenderer.color = new Color(255f, 0f, 0f, 255f);
            }
        }
    }

    private void JumperIdleAI()
    {
        controller.MoveHorizontal(movingDirection * runSpeed * 0.2f);
    }

    private void JumperFightAI()
    {
        controller.MoveHorizontal(movingDirection * runSpeed);
        controller.Jump(jumpSpeed);

        float aroundDistance = 0.4f;
        bool beAround = Target.position.x > shootStartPoint.position.x - aroundDistance && Target.position.x < shootStartPoint.position.x + aroundDistance ? true : false;

        if (beAround && canShoot)
        {
            weapon.Shoot();
            canShoot = false;
            shootTimer.StartTimer(0.1f);
        }

        if (shootTimer.timeElapsed)
        {
            canShoot = true;
        }

    }

    private void HurtHandler()
    {
        if (hurtTimer.timeElapsed)
        {
            isHurt = false;
        }
    }

    private void OnDeath()
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnEnemyTouch()
    {
        attributes.TakeDamage(1);
        hurtTimer.StartTimer(0.5f);
        isHurt = true;
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(jumperCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
}
