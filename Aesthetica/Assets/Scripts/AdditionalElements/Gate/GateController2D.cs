using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class that controlls Gate game object
/// </summary>
public class GateController2D : MonoBehaviour
{
    #region attributes
    /// <summary>
    /// Used to check is gate is triggered. Values are set  by GateTriggerController class.
    /// </summary>
    public bool IsTriggered { get; set; }

    /// <summary>
    /// Animator component reference. Used for animation.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Game Object that holds instance of gateTriggerController class
    /// </summary>
    [SerializeField] private GameObject doorTrigger;
    /// <summary>
    /// Game Object that holds collider that block the possibility to enter the gate is its closed
    /// </summary>
    [SerializeField] private GameObject doorSide;
    /// <summary>
    /// Trigger collider. Used to initiate door opening
    /// </summary>
    [SerializeField] private Collider2D doorTriggerCollider;
    /// <summary>
    /// Determines what layer can open gate
    /// </summary>
    [SerializeField] private LayerMask whatCanOpenDoor;
    /// <summary>
    /// Timer that counts how long gate should be opened
    /// </summary>
    [SerializeField] private Timer doorTimer;

    /// <summary>
    /// Flag that is used to check if gate is open
    /// </summary>
    private bool doorOpen;
    #endregion

    /// <summary>
    /// Call when object is created. Variable initialization.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Play proper animation based on doorOpen flag
    /// </summary>
    private void Animate()
    {
        if (doorOpen)
        {
            animator.Play("GateOpening");
        }
        else
        {
            animator.Play("GateClosing");
        }
    }

    /// <summary>
    /// Called every frame. 
    /// Responsible for calling animation and checking if door should be open or close
    /// </summary>
    private void Update()
    {
        Animate();
        DoorOpeningDetection();
        DoorClosingDetection();
    }

    /// <summary>
    /// Detects if gate should open
    /// </summary>
    private void DoorOpeningDetection()
    {
        if (Physics2D.IsTouchingLayers(doorTriggerCollider, whatCanOpenDoor))
        {
            doorTrigger.SetActive(false);
            doorSide.SetActive(false);
            doorTimer.StartTimer(3f);
            doorOpen = true;
            FindObjectOfType<AudioManager>().Play("GateOpening");
        }
    }

    /// <summary>
    /// Detects if gate should close
    /// </summary>
    private void DoorClosingDetection()
    {
        if (doorTimer.timeElapsed)
        {
            doorTrigger.SetActive(true);
            doorSide.SetActive(true);
            doorOpen = false;
            FindObjectOfType<AudioManager>().Play("GateClosing");
        }
    }
}
