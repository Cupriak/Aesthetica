using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is used by every object that can get damage or be killed
/// </summary>
public class AttributesController : MonoBehaviour
{
    /// <summary>
    /// Initial health points
    /// </summary>
    [SerializeField] private int initialHealth;

    /// <summary>
    /// Current health points
    /// </summary>
    public int Health { get; private set; }
    /// <summary>
    /// Flag that determine if owner is alive
    /// </summary>
    public bool IsAlive { get; private set; }
    /// <summary>
    /// Flag that determine if owner can be damaged
    /// </summary>
    public bool IsImmortal { get; set; }

    /// <summary>
    /// Call on object creation. Initialization of basic fields
    /// </summary>
    void Awake()
    {
        IsAlive = true;
        IsImmortal = false;
        Health = initialHealth;
    }

    /// <summary>
    /// Checks if health is less or equal than 0
    /// </summary>
    private void CheckIfAlive()
    {
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }
    /// <summary>
    /// Called fixed number of times per second
    /// Tracking if object is still alive
    /// </summary>
    void FixedUpdate()
    {
        CheckIfAlive();
    }

    /// <summary>
    /// Lower health
    /// </summary>
    /// <param name="amount">number of health points that will be taken</param>
    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }
    }

    /// <summary>
    /// Raise health
    /// </summary>
    /// <param name="amount">number of health points that will be added</param>
    public void TakeHealth(int amount)
    {
        if (amount > 0)
        {
            Health += amount;
        }
    }
}
