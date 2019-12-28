using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls PowerUps in game
/// </summary>
public class PowerUpController2D : MonoBehaviour
{
    /// <summary>
    /// Destroys game object
    /// </summary>
    private void Disappear()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// Activates when game object enter trigger.
    /// If collision was with player it plays power up sound and activates the function that is responsible for touching power up.
    /// </summary>
    /// <param name="collision">collider of touched object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController2D player = collision.GetComponent<PlayerController2D>();
        if(player != null)
        {
            player.OnPowerUpTouch();
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Disappear();
        }
    }
}
