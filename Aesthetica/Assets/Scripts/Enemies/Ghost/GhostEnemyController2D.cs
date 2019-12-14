using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyController2D : MonoBehaviour
{
    public ObjectController2D controller;

    //TRIGGER ATTRIBUTES
    //CrabEnemyTrigger script sets that values
    public bool IsTriggered { get; set; }
    public Transform Target { get; set; }

    //CHASE AI ATTRIBUTES
    [SerializeField] private Transform startPoint;
    private bool getBackToStartPoint;
    [SerializeField] private float chaseHorizontalSpeed;
    [SerializeField] private float chaseVerticalSpeed;
    [SerializeField] private float backToPositionSpeed;

    private Animator animator;

    public void Awake()
    {
        controller = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
    }

    private void Animate()
    {
        controller.Rotate();
        animator.Play("OctopusIdle");
    }

    private void GoBackToPositionAI()
    {
        float aroundDistance = 0.05f;
        bool beAroundX = startPoint.position.x > transform.position.x - aroundDistance && startPoint.position.x < transform.position.x + aroundDistance ? true : false;
        bool beAroundY = startPoint.position.y > transform.position.y - aroundDistance && startPoint.position.y < transform.position.y + aroundDistance ? true : false;
        if (beAroundX && beAroundY)
        {
            getBackToStartPoint = true;
            controller.Stop(true, true);
        }

        if (!getBackToStartPoint)
        {
            //Going back to X start position
            if(!beAroundX)
            {
                if (startPoint.position.x > transform.position.x)
                {
                    controller.MoveHorizontal(backToPositionSpeed);
                }
                else
                {
                    controller.MoveHorizontal(-backToPositionSpeed);
                }
            }
            else
            {
                controller.Stop(true, false);
            }

            //Going back to Y start position
            if(!beAroundY)
            {
                if (startPoint.position.y > transform.position.y)
                {
                    controller.MoveVertical(backToPositionSpeed);
                }
                else
                {
                    controller.MoveVertical(-backToPositionSpeed);
                }
            }
            else
            {
                controller.Stop(false, true);
            }
        }
    }

    private void ChaseTargetAI()
    {
        float aroundDistance = 0.05f;
        bool beAroundX = Target.position.x > transform.position.x - aroundDistance && Target.position.x < transform.position.x + aroundDistance ? true : false;
        bool beAroundY = Target.position.y > transform.position.y - aroundDistance && Target.position.y < transform.position.y + aroundDistance ? true : false;

        if(!beAroundX)
        {
            if (Target.position.x > transform.position.x)
            {
                controller.MoveHorizontal(chaseHorizontalSpeed);
            }
            else
            {
                controller.MoveHorizontal(-chaseHorizontalSpeed);
            }
        }
        else
        {
            controller.Stop(true, false);
        }

        if(!beAroundY)
        {
            if (Target.position.y > transform.position.y)
            {
                controller.MoveVertical(chaseVerticalSpeed);
            }
            else
            {
                controller.MoveVertical(-chaseVerticalSpeed);
            }
        }
        else
        {
            controller.Stop(false, true);
        }
    }

    private void ChaseAI()
    {
        if (IsTriggered)
        {
            getBackToStartPoint = false;
            ChaseTargetAI();
        }
        else
        {
            GoBackToPositionAI();
        }
    }

    private void Update()
    {
        Animate();

        ChaseAI();
    }
}
