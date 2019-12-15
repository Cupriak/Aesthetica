using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController2D : MonoBehaviour
{
    public ObjectController2D controller;

    private float speed = 3f;

    [SerializeField] private LayerMask whatShouldDestroyBullet;

    [SerializeField] private GameObject impactPrefab;

    private void Awake()
    {
        float chance = Random.value;
        if (chance > 0.75f)
        {
            FindObjectOfType<AudioManager>().Play("Shoot01");
        }
        else if (chance > 0.5f)
        {
            FindObjectOfType<AudioManager>().Play("Shoot02");
        }
        else if (chance > 0.25f)
        {
            FindObjectOfType<AudioManager>().Play("Shoot03");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Shoot04");
        }
    }

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
            if (collision.GetComponent<GhostEnemyController2D>() == null)
            {
                Impact();
            }
        }
    }
}
