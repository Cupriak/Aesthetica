using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusEnemyController2D : MonoBehaviour
{
    public AttributesController attributesController;
    public ObjectController2D objectController;

    //TRIGGER ATTRIBUTES
    //OctopusEnemyTrigger script sets that values
    public bool IsTriggered { get; set; }
    public Transform Target { get; set; }

    //DAMAGE ATTRIBUTES
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Timer hurtTimer;
    private bool isHurt;

    //DEATH ATTRIBUTES
    private bool isDying;
    [SerializeField] private Timer dyingTimer;
    private bool dyingProcedureActivated;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D octopusCollider;

    public void Awake()
    {
        attributesController = GetComponent<AttributesController>();
        objectController = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        octopusCollider = GetComponent<Collider2D>();

        spriteRenderer.flipX = true; // octopus sprite looks to the left -> we want it to look right
    }

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
        objectController.Rotate();
    }

    private void Animate()
    {
        if (!isDying)
        {
            animator.Play("OctopusIdle");
        }
        else
        {
            animator.Play("Death");
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

    private void HurtHandler()
    {
        if(hurtTimer.timeElapsed)
        {
            isHurt = false;
        }
    }

    private void DeathHandler()
    {
        if (!dyingProcedureActivated && !attributesController.IsAlive)
        {
            isHurt = false;
            isDying = true;
            dyingProcedureActivated = true;
            dyingTimer.StartTimer(1f);
        }

        if(dyingProcedureActivated && dyingTimer.timeElapsed)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void OnEnemyTouch()
    {
        attributesController.TakeDamage(1);
        hurtTimer.StartTimer(0.5f);
        isHurt = true;
    }

    private void Update()
    {
        Debug.Log("OCTOPUS HP = " + attributesController.Health);
        HurtHandler();
        DeathHandler();
        Animate();

        if (IsTriggered)
        {
            LookAtTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(octopusCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
}
