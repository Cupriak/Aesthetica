using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering ghost enemy
/// </summary>
public class GhostEnemyTrigger : MonoBehaviour
{
    /// <summary>
    /// Instance of ghost controller used to change its IsTrigger flag
    /// </summary>
    public GhostEnemyController2D ghost;

    /// <summary>
    /// LayerMask thet defines what can trigger ghost
    /// </summary>
    [SerializeField] private LayerMask whatCanTrigger;

    /// <summary>
    /// Called when ghost enters trigger
    /// Set flag in ghost IsTrigger to true and plays aggro sound
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            ghost.IsTriggered = true;
            ghost.Target = collision.gameObject.transform;
            FindObjectOfType<AudioManager>().Play("GhostAggro");
        }
    }

    /// <summary>
    /// Called when ghost exits trigger
    /// Set flag in ghost IsTrigger to false
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            ghost.IsTriggered = false;
        }
    }
}
