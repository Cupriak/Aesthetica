using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that checks if gate is triggered
/// </summary>
public class GateTriggerController : MonoBehaviour
{
    /// <summary>
    /// Gate that will be triggered
    /// </summary>
    public GateController2D gate;

    /// <summary>
    /// Determines what layer can open gate
    /// </summary>
    [SerializeField] private LayerMask whatCanTrigger;

    /// <summary>
    /// Timer that determine how long gate will be open
    /// </summary>
    [SerializeField] private Timer gateTimer;

    /// <summary>
    /// Called when object enter trigger.
    /// If touched object can trigger gate set gate IsTrigger flag and start gate timer
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            gate.IsTriggered = true;
            gateTimer.StartTimer(1f);
        }
    }
}
