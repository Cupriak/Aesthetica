using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is used by enemies to be able to shoot
/// </summary>
public class EnemyWeapon : MonoBehaviour
{
    /// <summary>
    /// Point where bullets will be spawned
    /// </summary>
    [SerializeField] private Transform startingPoint;

    /// <summary>
    /// Prefab of bullet
    /// </summary>
    [SerializeField] private GameObject bulletPrefab;

    /// <summary>
    /// Soawn bullet prefab in startingPoint
    /// </summary>
    public void Shoot()
    {
        Instantiate(bulletPrefab, startingPoint.position, startingPoint.rotation);
    }
}
