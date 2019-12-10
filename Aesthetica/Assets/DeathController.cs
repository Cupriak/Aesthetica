using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private float deathTime = 1f;

    void Start()
    {
        Destroy(gameObject, deathTime);
    }
}
