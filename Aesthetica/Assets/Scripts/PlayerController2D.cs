using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] ObjectController2D objectController;
    [SerializeField] AttributesController attributesController;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] bool preventRotation;

    [SerializeField] LayerMask whatIsEnemy;

    Animator animator;
    Collider2D playerCollider;

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
        else if(objectController.IsGrounded)
        {
            animator.Play("PlayerIdle");
        }
        else
        {
            animator.Play("PlayerJump");
        }
    }

    private void Update()
    {
        InputHelper.GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        if (preventRotation)
        {
            objectController.PreventRotation();
        }

        objectController.MoveHorizontal(InputHelper.horizontalMove * runSpeed);

        if(InputHelper.jump)
        {
            objectController.Jump(jumpSpeed);
        }

    }

    //FIX THAT SOME ENEMIES WILL NOT BE TRIGGERS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Physics2D.IsTouchingLayers(playerCollider, whatIsEnemy))
        {
            attributesController.GetDamage(1);
            Debug.Log("HP Left = " + attributesController.Health);
        }
    }
}
