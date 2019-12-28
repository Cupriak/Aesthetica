using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls plant turret enemy
/// </summary>
public class PlantTurretEnemyController2D : MonoBehaviour
{
    #region Trigger Attributes
    /// <summary>
    /// Flag that is used to check if plant is triggerd
    /// </summary>
    public bool IsTriggered { get; set; }
    /// <summary>
    /// Transform of target that plant is going to shoot
    /// </summary>
    public Transform Target { get; set; }
    #endregion

    #region Shoot AI Attributes
    /// <summary>
    /// Reference to enemy weapon that is attached to game object
    /// </summary>
    [SerializeField] private EnemyWeapon weapon;
    /// <summary>
    /// Timer that controlls how fast plant can shoot
    /// </summary>
    [SerializeField] private Timer shootTimer;
    /// <summary>
    /// Point where bullets will be spawned when enemy shoot
    /// </summary>
    [SerializeField] private Transform shootStartPoint;
    /// <summary>
    /// Flag that check if plant can shoot again
    /// </summary>
    private bool canShoot;
    #endregion

    /// <summary>
    /// Called on object creation. 
    /// Set canShoot flag to true
    /// </summary>
    public void Awake()
    {
        canShoot = true;
    }

    /// <summary>
    /// Call every frame
    /// Main object logic
    /// </summary>
    private void Update()
    {
        ShootAI();
    }

    /// <summary>
    /// AI Method that is responsible for shooting to target
    /// </summary>
    private void ShootAI()
    {
        if (IsTriggered)
        {
            float aroundDistance = 0.05f;
            bool beAround = Target.position.y > shootStartPoint.position.y - aroundDistance && Target.position.y < shootStartPoint.position.y + aroundDistance ? true : false;

            if (beAround && canShoot)
            {
                weapon.Shoot();
                canShoot = false;
                shootTimer.StartTimer(0.5f);
            }
        }

        if (shootTimer.timeElapsed)
        {
            canShoot = true;
        }
    }
}
