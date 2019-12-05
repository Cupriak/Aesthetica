using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatformController2D : MonoBehaviour
{
    Collider2D collider;

    [SerializeField] ObjectController2D controller;

    [SerializeField] float verticalSpeed;

    [SerializeField] Collider2D lowerBound;
    [SerializeField] Collider2D upperBound;

    [SerializeField] LayerMask whatCanBeTakenByPlatform;

    int movingSide = -1;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        controller.MoveVertical(movingSide * verticalSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change way of platform
        if (Physics2D.IsTouching(collider, lowerBound) || Physics2D.IsTouching(collider, upperBound))
        {
            movingSide = -movingSide;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HERE TRY TO GRAB OBJECTS THAT STOMP ON PLATFORM
        if (LayerMaskHelper.isLayerInLayerMask(collision.gameObject.layer, whatCanBeTakenByPlatform))
        {



            Debug.Log("Vertical Platform grabbed layer = " + LayerMask.LayerToName(collision.gameObject.layer));
        }
    }
}
