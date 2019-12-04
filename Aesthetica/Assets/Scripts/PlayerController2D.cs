using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] ObjectController2D controller;

    Animator animator;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] bool preventRotation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void animate()
    {
        if (InputCapture.horizontalMove != 0 && controller.isGrounded)
        {
            animator.Play("PlayerRunAnimation");
        }
        else if(controller.isGrounded)
        {
            animator.Play("PlayerIdleAnimation");
        }
        else
        {
            animator.Play("PlayerJumpAnimation");
        }
    }

    private void Update()
    {
        InputCapture.GetInput();
        animate();
    }

    private void FixedUpdate()
    {
        controller.GroundCheck();

        if (preventRotation)
        {
            controller.PreventRotation();
        }

        controller.MoveHorizontal(InputCapture.horizontalMove * runSpeed);

        if(InputCapture.jump)
        {
            controller.Jump(jumpSpeed);
        }

    }
}
