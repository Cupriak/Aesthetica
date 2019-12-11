using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTurretEnemyTrigger : MonoBehaviour
{
    public PlantTurretEnemyController2D turret;

    [SerializeField] private LayerMask whatCanTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            turret.IsTriggered = true;
            turret.Target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            turret.IsTriggered = false;
        }
    }
}
