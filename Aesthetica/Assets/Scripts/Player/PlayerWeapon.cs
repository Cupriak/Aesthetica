using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Timer shootTimer;

    [SerializeField] private PlayerController2D player;

    private void Shoot()
    {
        InputHelper.GetInput();

        if (player.canBeControlled && shootTimer.timeElapsed && InputHelper.shoot)
        {
            Instantiate(bulletPrefab, startingPoint.position, startingPoint.rotation);

            shootTimer.StartTimer(0.2f);
        }
    }

    private void Update ()
    {
        Shoot();
    }
}
