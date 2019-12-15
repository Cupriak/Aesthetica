using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private ObjectController2D player;
    [SerializeField] private bool allowWaypointTeleportation;

    private void debugTeleportation()
    {
        if (waypoints.Length == 0) return;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (waypoints.Length >= 1)
            {
                player.Teleport(waypoints[0]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (waypoints.Length >= 2)
            {
                player.Teleport(waypoints[1]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (waypoints.Length >= 3)
            {
                player.Teleport(waypoints[2]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (waypoints.Length >= 4)
            {
                player.Teleport(waypoints[3]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            if (waypoints.Length >= 5)
            {
                player.Teleport(waypoints[4]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            if (waypoints.Length >= 6)
            {
                player.Teleport(waypoints[5]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            if (waypoints.Length >= 7)
            {
                player.Teleport(waypoints[6]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            if (waypoints.Length >= 8)
            {
                player.Teleport(waypoints[7]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            if (waypoints.Length >= 9)
            {
                player.Teleport(waypoints[8]);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha0))
        {
            if (waypoints.Length >= 10)
            {
                player.Teleport(waypoints[9]);
            }
        }
    }

	void Update ()
    {
        InputHelper.GetInput();
        if(InputHelper.menu)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (InputHelper.pause)
        {
            FindObjectOfType<UIController>().Pause();
        }

        if (allowWaypointTeleportation)
        {
            debugTeleportation();
        } 
    }
}
