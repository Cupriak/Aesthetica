using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private float deathTime;

    void Start()
    {
        Destroy(gameObject, deathTime);
    }
}
