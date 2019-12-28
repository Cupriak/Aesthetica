using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls Bullets in game
/// </summary>
public class BulletController2D : MonoBehaviour
{
    /// <summary>
    /// Allows object to move
    /// </summary>
    public ObjectController2D controller;

    /// <summary>
    /// Constant bullet speed
    /// </summary>
    private float speed = 3f;

    /// <summary>
    /// Determines what layer can destroy bullet on contact
    /// </summary>
    [SerializeField] private LayerMask whatShouldDestroyBullet;

    /// <summary>
    /// Prefab that will be spawned upon destroying bullet
    /// </summary>
    [SerializeField] private GameObject impactPrefab;

    /// <summary>
    /// Called on creation of object. 
    /// Play random bullet sound
    /// </summary>
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

    /// <summary>
    /// Called fixed number of times per second.
    /// Main object logic
    /// </summary>
    private void FixedUpdate()
    {
        MoveBullet();
    }

    /// <summary>
    /// Move bullet
    /// </summary>
    private void MoveBullet()
    {
        controller.MoveHorizontal(transform.right, speed);
    }

    /// <summary>
    /// Destroy bullet game object and spawn impact object on its place
    /// </summary>
    private void Impact()
    {
        Instantiate(impactPrefab, transform.position, transform.rotation);
        Destroy(gameObject, Time.deltaTime);//sometimes enemy dont interact with bullet before its destroyed so we need to wait a moment
    }

    /// <summary>
    /// Called when object enter trigger.
    /// If trigger can destroy bullet call impact method.
    /// </summary>
    /// <param name="collision">collider of touched object</param>
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
