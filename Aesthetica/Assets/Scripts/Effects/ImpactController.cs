using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : MonoBehaviour
{
    [SerializeField] private float impactTime;
    [SerializeField] private float soundPlayDistance;

    void Awake()
    {
        Vector2 playerPosition = FindObjectOfType<PlayerController2D>().gameObject.transform.position;
        float distance = (playerPosition - (Vector2)transform.position).magnitude;

        if(distance<soundPlayDistance)
        {
            FindObjectOfType<AudioManager>().Play("Impact");
        }
        
        Destroy(gameObject, impactTime);
    }
}
