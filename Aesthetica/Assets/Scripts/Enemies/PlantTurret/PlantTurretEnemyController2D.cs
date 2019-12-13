using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTurretEnemyController2D : MonoBehaviour
{
    //TRIGGER ATTRIBUTES
    //OctopusEnemyTrigger script sets that values
    public bool IsTriggered { get; set; }
    public Transform Target { get; set; }

    //SHOOT AI ATTRIBUTES
    [SerializeField] private EnemyWeapon weapon;
    [SerializeField] private Timer shootTimer;
    [SerializeField] private Transform shootStartPoint;
    private bool canShoot;

    public void Awake()
    {
        canShoot = true;
    }

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

    private void Update()
    {
        ShootAI();
    }
}
