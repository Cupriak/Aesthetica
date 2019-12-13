using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemyTrigger : MonoBehaviour
{
    public CrabEnemyController2D crab;

    [SerializeField] private LayerMask whatCanTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            crab.IsTriggered = true;
            crab.Target = collision.gameObject.transform;
            FindObjectOfType<AudioManager>().Play("CrabAggro");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            crab.IsTriggered = false;
        }
    }
}
