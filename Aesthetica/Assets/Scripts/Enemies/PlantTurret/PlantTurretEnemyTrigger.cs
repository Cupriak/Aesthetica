using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering plant turret enemy
/// </summary>
public class PlantTurretEnemyTrigger : MonoBehaviour
{
    /// <summary>
    /// Reference of plant controller used to change its IsTrigger flag
    /// </summary>
    public PlantTurretEnemyController2D turret;

    /// <summary>
    /// LayerMask thet defines what can trigger plant
    /// </summary>
    [SerializeField] private LayerMask whatCanTrigger;

    /// <summary>
    /// Called when plant enters trigger
    /// Set flag in plant IsTrigger to true
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            turret.IsTriggered = true;
            turret.Target = collision.gameObject.transform;
        }
    }

    /// <summary>
    /// Called when plant exits trigger
    /// Set flag in plant IsTrigger to false
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            turret.IsTriggered = false;
        }
    }
}
