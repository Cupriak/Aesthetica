using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering octopus enemy
/// </summary>
public class OctopusEnemyTrigger : MonoBehaviour
{
    /// <summary>
    /// Instance of octopus controller used to change its IsTrigger flag
    /// </summary>
    public OctopusEnemyController2D octopus;

    /// <summary>
    /// LayerMask thet defines what can trigger octopus
    /// </summary>
    [SerializeField] private LayerMask whatCanTrigger;

    /// <summary>
    /// Called when octopus enters trigger
    /// Set flag in octopus IsTrigger to true
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            octopus.IsTriggered = true;
            octopus.Target = collision.gameObject.transform;
        }
    }

    /// <summary>
    /// Called when octopus exits trigger
    /// Set flag in octopus IsTrigger to false
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            octopus.IsTriggered = false;
        }
    }
}
