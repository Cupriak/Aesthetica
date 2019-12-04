using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController2D : MonoBehaviour {

    [SerializeField] ObjectController2D controller;

    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;

    [SerializeField] LayerMask whatCanBeTakenByPlatform;

    int movingSide = 1;
    int movingUp = 1;

	void FixedUpdate ()
    {
        controller.MoveHorizontal(movingSide * horizontalSpeed);
        controller.MoveVertical(movingUp * verticalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //dont change moving side if colide with palyer
        if (!LayerMask.LayerToName(collision.gameObject.layer).Equals("Player"))
        {
            movingSide = -movingSide;
        }

        //no idea how but it should check if layer is in layermask 
        // (layermask == (layermask | (1 << layer)))


        //if (whatCanBeTakenByPlatform == 1 << collision.gameObject.layer)
        if (whatCanBeTakenByPlatform == (whatCanBeTakenByPlatform | (1 << collision.gameObject.layer)))
        {
            //ObjectController2D test = new ObjectController2D();
            //test.TESTAddMove(collision.gameObject.GetComponent<Rigidbody2D>(), movingSide * horizontalSpeed, movingUp * verticalSpeed);

            Debug.Log("Grabbed layer = " + LayerMask.LayerToName(collision.gameObject.layer));

        }

        //TO BE DELETED LATER
        //if (Physics2D.IsTouchingLayers(collision.collider, whatCanBeTakenByPlatform))
        //{
        //    //ObjectController2D.AddMove(collision.rigidbody, movingSide * horizontalSpeed, movingUp * verticalSpeed);
        //    Debug.Log("Grabbed layer = " + LayerMask.LayerToName(collision.gameObject.layer));
        //}

    }
}
