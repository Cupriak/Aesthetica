using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls ghost enemy
/// </summary>
public class GhostEnemyController2D : MonoBehaviour
{
    /// <summary>
    /// Used to be able to move ghost
    /// </summary>
    public ObjectController2D controller;

    #region Trigger Attributes
    /// <summary>
    /// Flag that is used to check if crab is triggerd
    /// </summary>
    public bool IsTriggered { get; set; }
    /// <summary>
    /// Transform of target that crab is going to chase
    /// </summary>
    public Transform Target { get; set; }
    #endregion

    #region Chase AI Attributes
    /// <summary>
    /// Start point of ghost
    /// </summary>
    [SerializeField] private Transform startPoint;
    /// <summary>
    /// Flag that determines if ghost is back on start position
    /// </summary>
    private bool getBackToStartPoint;
    /// <summary>
    /// Horizontal speed of ghost
    /// </summary>
    [SerializeField] private float chaseHorizontalSpeed;
    /// <summary>
    /// Vertical speed of ghost
    /// </summary>
    [SerializeField] private float chaseVerticalSpeed;
    /// <summary>
    /// Speed that ghost will use while going back to start position
    /// </summary>
    [SerializeField] private float backToPositionSpeed;
    #endregion

    private Animator animator;

    /// <summary>
    /// Called on object creation. Initialize default values of some fields
    /// </summary>
    public void Awake()
    {
        controller = GetComponent<ObjectController2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called every frame. 
    /// Main object logic that call animation and AI methods
    /// </summary>
    private void Update()
    {
        Animate();

        ChaseAI();
    }
    /// <summary>
    /// Rotate ghost and play its animation
    /// </summary>
    private void Animate()
    {
        controller.Rotate();
        animator.Play("OctopusIdle");
    }

    /// <summary>
    /// AI method that is responsible for going back to start position if ghost loose its target
    /// </summary>
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

    /// <summary>
    /// AI method that is responsible for chasing target
    /// </summary>
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

    /// <summary>
    /// Main AI logic method. Call other AI methods basing on IsTriggered flag
    /// </summary>
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
}
