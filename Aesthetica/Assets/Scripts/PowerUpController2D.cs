﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController2D : MonoBehaviour
{
    private void Disappear()
    {
        //Destroy(gameObject, Time.deltaTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController2D player = collision.GetComponent<PlayerController2D>();
        if(player != null)
        {
            player.OnPowerUpTouch();
            Disappear();
        }
    }
}
