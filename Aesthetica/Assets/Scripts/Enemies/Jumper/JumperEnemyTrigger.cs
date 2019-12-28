using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering jumper enemy
/// </summary>
public class JumperEnemyTrigger : MonoBehaviour
{
    /// <summary>
    /// Instance of jumper controller used to change its IsTrigger flag
    /// </summary>
    public JumperEnemyController2D jumper;

    /// <summary>
    /// LayerMask thet defines what can trigger jumper
    /// </summary>
    [SerializeField] private LayerMask whatCanTrigger;

    /// <summary>
    /// Called when jumper enters trigger
    /// Set flag in jumper IsTrigger to true and plays aggro sound
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            jumper.IsTriggered = true;
            jumper.Target = collision.gameObject.transform;
            FindObjectOfType<AudioManager>().Play("JumperAggro");
        }
    }

    /// <summary>
    /// Called when jumper exits trigger
    /// Set flag in jumper IsTrigger to false
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            jumper.IsTriggered = false;
        }
    }
}
