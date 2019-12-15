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
    [SerializeField] private GameObject deathPrefab;

    //SHOOT AI ATTRIBUTES
    [SerializeField] private EnemyWeapon weapon;
    [SerializeField] private Timer shootTimer;
    private bool canShoot;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D octopusCollider;
    private Color basicColor;

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

    private void HurtHandler()
    {
        if(hurtTimer.timeElapsed)
        {
            isHurt = false;
        }
    }

    private void OnDeath()
    {
        FindObjectOfType<AudioManager>().Play("OctopusDeath");
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject, Time.deltaTime);
    }

    private void OnEnemyTouch()
    {
        attributesController.TakeDamage(1);
        hurtTimer.StartTimer(0.5f);
        isHurt = true;
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(octopusCollider, whatIsEnemy))
        {
            OnEnemyTouch();
        }
    }
}
