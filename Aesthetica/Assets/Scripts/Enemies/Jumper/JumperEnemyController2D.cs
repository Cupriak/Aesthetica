﻿using System.Collections;
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
    private Color basicColor;

    [SerializeField] private Collider2D jumperCollider;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    //AI RELATED ATTRIBUTES
    [SerializeField] private EnemyWeapon weapon;
    [SerializeField] private Timer shootTimer;
    private bool canShoot;
    [SerializeField] private Transform shootStartPoint;
    [Range(0, 1)] [SerializeField] private float probabilityToJump;
    [Range(0, 1)] [SerializeField] private float shootingRange;
    [Range(0, 1)] [SerializeField] private float reloadTime;
    [Range(0, 1)] [SerializeField] private float idleActiveness;


    //JUMPER ENEMY SIDE COLLIDER DETECTION SETS THIS VALUE
    [HideInInspector] public int movingDirection;

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

    private void JumperIdleAI()
    {
        controller.MoveHorizontal(movingDirection * runSpeed * idleActiveness);
        if (Random.value < probabilityToJump * idleActiveness*0.7f)
        {
            controller.Jump(jumpSpeed * (idleActiveness*2.5f));
        }
    }

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