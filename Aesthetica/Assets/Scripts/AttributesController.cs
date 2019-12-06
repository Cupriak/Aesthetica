using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesController : MonoBehaviour
{
    [SerializeField] int initialHealth;

    public int Health { get; private set; }

    public bool IsAlive { get; private set; }

    void Awake()
    {
        IsAlive = true;
        Health = initialHealth;
	}
	
	void FixedUpdate()
    {
		if(Health<0)
        {
            IsAlive = false;
        }
	}

    public void GetDamage(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
        }
    }

    public void GetHealth(int amount)
    {
        if (amount > 0)
        {
            Health += amount;
        }
    }
}
