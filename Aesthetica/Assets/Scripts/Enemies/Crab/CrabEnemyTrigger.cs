using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering crab enemy
/// </summary>
public class CrabEnemyTrigger : MonoBehaviour
{
    /// <summary>
    /// Instance of crab controller used to change its IsTrigger flag
    /// </summary>
    public CrabEnemyController2D crab;

    /// <summary>
    /// LayerMask thet defines what can trigger crab
    /// </summary>
    [SerializeField] private LayerMask whatCanTrigger;

    /// <summary>
    /// Called when crab enters trigger
    /// Set flag in crab IsTrigger to true and plays aggro sound
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            crab.IsTriggered = true;
            crab.Target = collision.gameObject.transform;
            FindObjectOfType<AudioManager>().Play("CrabAggro");
        }
    }

    /// <summary>
    /// Called when crab exits trigger
    /// Set flag in crab IsTrigger to false
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            crab.IsTriggered = false;
        }
    }
}
