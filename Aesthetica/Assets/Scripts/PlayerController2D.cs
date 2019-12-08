﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public ObjectController2D objectController;
    public AttributesController attributesController;

    [SerializeField] Collider2D groundCheck;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool rotate;
    public bool canBeControlled;

    [SerializeField] private LayerMask whatIsEnemy;

    private Animator animator;
    private Collider2D playerCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Animate()
    {
        if (InputHelper.horizontalMove != 0 && objectController.IsGrounded)
        {
            animator.Play("PlayerRun");
        }
        else if (objectController.IsGrounded)
        {
            animator.Play("PlayerIdle");
        }
        else
        {
            animator.Play("PlayerJump");
        }
    }

    private void MovementControl()
    {
        if (canBeControlled)
        {
            if (rotate)
            {
                objectController.Rotate();
            }

            if (!(InputHelper.horizontalMove == 0 && Physics2D.IsTouchingLayers(groundCheck, LayerMask.GetMask("MovingHorizontalPlatform"))))
            {
                objectController.MoveHorizontal(InputHelper.horizontalMove * runSpeed);
            }

            if (InputHelper.jump)
            {
                objectController.Jump(jumpSpeed);
            }

            if (InputHelper.stop)
            {
                objectController.Stop(true, true);
            }
        }
    }

    private void Update()
    {
        InputHelper.GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        MovementControl();
    }

    //FIX THAT SOME ENEMIES WILL NOT BE TRIGGERS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ENEMY DETECTION
        if (Physics2D.IsTouchingLayers(playerCollider, whatIsEnemy))
        {
            attributesController.TakeDamage(1);
            Debug.Log("HP Left = " + attributesController.Health);
        }

        //WATER DETECTON
        if (Physics2D.IsTouchingLayers(playerCollider, LayerMask.GetMask("Water")))
        {
            objectController.SetDrag(5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //LEAVING WATER
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Water"))
        {
            objectController.SetDrag(0f);
        }
    }
}
