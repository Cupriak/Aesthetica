using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemyController2D : MonoBehaviour
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

    //CHASE AI ATTRIBUTES
    [SerializeField] private MovingObjectController2D idleScript;
    [SerializeField] private Transform startPoint;
    private bool getBackToStartPoint;
    [SerializeField] private float chaseSpeed;
    private bool isChasing;

    //DEATH ATTRIBUTES
    [SerializeField] private GameObject deathPrefab;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color basicColor;
    [SerializeField] private Collider2D crabCollider;

    public void Awake()
    {
        attributes = GetComponent<AttributesController>();
        controller = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        basicColor = spriteRenderer.color;

        spriteRenderer.flipX = true; // crab sprite looks to the left -> we want it to look right
    }

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

    private void HurtHandler()
    {
        if (hurtTimer.timeElapsed)
        {
            isHurt = false;
        }
    }

    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject, Time.deltaTime);
    }

    private void OnEnemyTouch()
    {
        attributes.TakeDamage(1);
        hurtTimer.StartTimer(0.5f);
        isHurt = true;
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(crabCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
}
