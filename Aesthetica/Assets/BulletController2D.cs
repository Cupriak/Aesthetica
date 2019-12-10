using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController2D : MonoBehaviour
{
    public ObjectController2D controller;
    private ObjectController2D parentController;

    private float speed = 3f;

    [SerializeField] private LayerMask whatShouldDestroyBullet;

    [SerializeField] private GameObject impactPrefab;

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        controller.MoveHorizontal(transform.right, speed);
    }

    private void Impact()
    {
        Instantiate(impactPrefab, transform.position, transform.rotation);
        Destroy(gameObject, Time.deltaTime);//sometimes enemy dont interact with bullet before its destroyed so we need to wait a moment
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatShouldDestroyBullet))
        {
            Impact();
        }
    }
}
