using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController2D : MonoBehaviour
{
    //TRIGGER ATTRIBUTES
    public bool IsTriggered { get; set; }

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject doorTrigger;
    [SerializeField] private GameObject doorSide;
    [SerializeField] private Collider2D doorTriggerCollider;
    [SerializeField] private LayerMask whatCanOpenDoor;
    [SerializeField] private Timer doorTimer;

    private bool doorOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Animate()
    {
        if (doorOpen)
        {
            animator.Play("GateOpen");
        }
        else
        {
            animator.Play("GateClosed");
        }
    }

    private void Update()
    {
        Animate();
        DoorOpeningDetection();
        DoorClosingDetection();
    }

    private void DoorOpeningDetection()
    {
        if (Physics2D.IsTouchingLayers(doorTriggerCollider, whatCanOpenDoor))
        {
            doorTrigger.SetActive(false);
            doorSide.SetActive(false);
            doorTimer.StartTimer(3f);
            doorOpen = true;
        }
    }

    private void DoorClosingDetection()
    {
        if (doorTimer.timeElapsed)
        {
            doorTrigger.SetActive(true);
            doorSide.SetActive(true);
            doorOpen = false;
        }
    }
}
