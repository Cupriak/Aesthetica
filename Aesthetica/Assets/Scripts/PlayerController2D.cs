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

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
}
