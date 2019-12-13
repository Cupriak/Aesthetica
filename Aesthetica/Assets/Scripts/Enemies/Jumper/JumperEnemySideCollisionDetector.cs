﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemySideCollisionDetector : MonoBehaviour
{
    [SerializeField] private JumperEnemyController2D jumper;
    [SerializeField] private LayerMask whatIsObsticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatIsObsticle))
        {
            jumper.movingDirection = -jumper.movingDirection;
        }
    }

}