//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyOctopusController2D : MonoBehaviour
//{

//    public AttributesController attributesController;

//    [SerializeField] Transform playerTransform;

//    private Animator animator;
//    private SpriteRenderer spriteRenderer;

//    public void Awake()
//    {
//        animator = GetComponent<Animator>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }

//    private void LookAtPlayer()
//    {
//        if (playerTransform.position.x > transform.position.x)
//        {
//            spriteRenderer.flipX = true;
//        }
//        else
//        {
//            spriteRenderer.flipX = false;
//        }
//    }

//    private void Animate()
//    {
//        animator.Play("OctopusIdle");

//        LookAtPlayer();
//    }

//    private void Update()
//    {
//        Animate();
//    }
//}