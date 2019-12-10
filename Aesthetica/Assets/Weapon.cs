using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;

    [SerializeField] private GameObject bulletPrefab;

    private void Shoot()
    {
        InputHelper.GetInput();
        if (InputHelper.shoot)
        {
            Instantiate(bulletPrefab, startingPoint.position, startingPoint.rotation);
        }
    }

    private void Update ()
    {
        Shoot();
    }
}
