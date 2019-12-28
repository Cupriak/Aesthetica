using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls impacts
/// </summary>
public class ImpactController : MonoBehaviour
{
    /// <summary>
    /// determines how long impact should last before destroy in ms
    /// </summary>
    [SerializeField] private float impactTime;
    /// <summary>
    /// determines how long distance between player and impact is needed to play impact sound
    /// </summary>
    [SerializeField] private float soundPlayDistance;

    /// <summary>
    /// Called on creation of object.
    /// Destroy object and
    /// if distance between player and impact is less than soundPlayDistance value plays impact sound
    /// </summary>
    void Awake()
    {
        Vector2 playerPosition = FindObjectOfType<PlayerController2D>().gameObject.transform.position;
        float distance = (playerPosition - (Vector2)transform.position).magnitude;

        if (distance < soundPlayDistance)
        {
            FindObjectOfType<AudioManager>().Play("Impact");
        }

        Destroy(gameObject, impactTime);
    }
}
