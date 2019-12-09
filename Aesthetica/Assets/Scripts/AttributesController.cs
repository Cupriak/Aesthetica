using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesController : MonoBehaviour
{
    [SerializeField] int initialHealth;

    public int Health { get; private set; }
    public bool IsAlive { get; private set; }
    public bool IsImmortal { get; set; }

    void Awake()
    {
        IsAlive = true;
        IsImmortal = false;
        Health = initialHealth;
    }

    void FixedUpdate()
    {
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }
    }

    public void TakeHealth(int amount)
    {
        if (amount > 0)
        {
            Health += amount;
        }
    }
}
