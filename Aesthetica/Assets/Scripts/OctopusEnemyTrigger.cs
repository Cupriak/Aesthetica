using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusEnemyTrigger : MonoBehaviour
{
    public OctopusEnemyController2D octopus;

    [SerializeField] private LayerMask whatCanTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            octopus.IsTriggered = true;
            octopus.Target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            octopus.IsTriggered = false;
        }
    }
}
