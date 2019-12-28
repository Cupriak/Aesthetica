using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is responsible for player shooting
/// </summary>
public class PlayerWeapon : MonoBehaviour
{
    /// <summary>
    /// Point where bullet would be spawned
    /// </summary>
    [SerializeField] private Transform startingPoint;

    /// <summary>
    /// Bullet prefab that would be spawned
    /// </summary>
    [SerializeField] private GameObject bulletPrefab;

    /// <summary>
    /// Timer that determines how often player can shoot
    /// </summary>
    [SerializeField] private Timer shootTimer;

    /// <summary>
    /// Player controller reference to check if player can shoot
    /// </summary>
    [SerializeField] private PlayerController2D player;

    /// <summary>
    /// If player pressed shoot button and can controll character spawn bullet and set shoot timer
    /// </summary>
    private void Shoot()
    {
        InputHelper.GetInput();

        if (player.canBeControlled && shootTimer.timeElapsed && InputHelper.shoot)
        {
            Instantiate(bulletPrefab, startingPoint.position, startingPoint.rotation);

            shootTimer.StartTimer(0.2f);
        }
    }

    /// <summary>
    /// Call every frame.
    /// Call shoot method
    /// </summary>
    private void Update ()
    {
        Shoot();
    }
}
