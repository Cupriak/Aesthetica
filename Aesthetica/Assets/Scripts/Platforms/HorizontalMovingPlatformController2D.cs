using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatformController2D : MonoBehaviour
{
    Collider2D collider;

    [SerializeField] ObjectController2D controller;

    [SerializeField] float horizontalSpeed;

    [SerializeField] Collider2D leftBound;
    [SerializeField] Collider2D rightBound;

    [SerializeField] LayerMask whatCanBeTakenByPlatform;

    int movingSide = 1;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate ()
    {
        controller.MoveHorizontal(movingSide * horizontalSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change way of platform
        if (Physics2D.IsTouching(collider, leftBound) || Physics2D.IsTouching(collider, rightBound))
        {
            movingSide = -movingSide;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //HERE TRY TO GRAB OBJECTS THAT STOMP ON PLATFORM
        if(LayerMaskHelper.isLayerInLayerMask(collision.gameObject.layer, whatCanBeTakenByPlatform))
        {



            Debug.Log("Horizontal Platform grabbed layer = " + LayerMask.LayerToName(collision.gameObject.layer));
        }
    }
}
