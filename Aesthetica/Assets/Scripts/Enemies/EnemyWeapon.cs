using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;

    [SerializeField] private GameObject bulletPrefab;

    public void Shoot()
    {
        Instantiate(bulletPrefab, startingPoint.position, startingPoint.rotation);
    }
}
