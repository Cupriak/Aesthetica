using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyTrigger : MonoBehaviour
{
    public GhostEnemyController2D ghost;

    [SerializeField] private LayerMask whatCanTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            ghost.IsTriggered = true;
            ghost.Target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            ghost.IsTriggered = false;
        }
    }
}
