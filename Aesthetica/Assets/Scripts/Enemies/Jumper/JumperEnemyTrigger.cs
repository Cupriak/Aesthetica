using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemyTrigger : MonoBehaviour
{
    public JumperEnemyController2D jumper;

    [SerializeField] private LayerMask whatCanTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            jumper.IsTriggered = true;
            jumper.Target = collision.gameObject.transform;
            FindObjectOfType<AudioManager>().Play("JumperAggro");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMaskHelper.IsLayerInLayerMask(collision.gameObject.layer, whatCanTrigger))
        {
            jumper.IsTriggered = false;
        }
    }
}
