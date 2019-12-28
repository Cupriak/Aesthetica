using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls death effect
/// </summary>
public class DeathController : MonoBehaviour
{
    /// <summary>
    /// Time how long death animation should be played in ms
    /// </summary>
    [SerializeField] private float deathTime;

    /// <summary>
    /// Called on creation of object. Destroy game object with deley defined by deathTime
    /// </summary>
    void Start()
    {
        Destroy(gameObject, deathTime);
    }
}
