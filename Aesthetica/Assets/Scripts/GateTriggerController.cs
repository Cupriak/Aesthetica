using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerController : MonoBehaviour
{
    public GateController2D gate;

    [SerializeField] private LayerMask whatCanTrigger;

    [SerializeField] private Timer gateTimer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            gate.IsTriggered = true;
            gateTimer.StartTimer(1f);
        }
    }
}
