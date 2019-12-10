using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : MonoBehaviour
{
    [SerializeField] private float impactTime;

    void Start()
    {
        Destroy(gameObject, impactTime);
    }
}
