using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that checks if jumper enemy touched obsticle and change its direction of movement
/// </summary>
public class JumperEnemySideCollisionDetector : MonoBehaviour
{
    /// <summary>
    /// Reference to jumper controller
    /// </summary>
    [SerializeField] private JumperEnemyController2D jumper;
    /// <summary>
    /// Define what layer will be treated as obsticle
    /// </summary>
    [SerializeField] private LayerMask whatIsObsticle;

    /// <summary>
    /// Calls on collision enter.
    /// If collided with layer that is in whatIsObsticle layermask change direction of jumper
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatIsObsticle))
        {
            jumper.movingDirection = -jumper.movingDirection;
        }
    }

}
