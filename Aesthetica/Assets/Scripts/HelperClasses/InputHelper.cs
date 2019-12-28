using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that help collect input from player
/// </summary>
public static class InputHelper
{
    /// <summary>
    /// Value between -1 and 1 that determine horizontal movement
    /// </summary>
    public static float horizontalMove;
    /// <summary>
    /// Value between -1 and 1 that determine vertical movement
    /// </summary>
    public static float verticalMove;
    /// <summary>
    /// Flag that determines if player should jump
    /// </summary>
    public static bool jump;
    /// <summary>
    /// Flag that determines if player should stop moving
    /// </summary>
    public static bool stop;
    /// <summary>
    /// Flag that determines if player should shoot
    /// </summary>
    public static bool shoot;
    /// <summary>
    /// Flag that determines if menu should be opened
    /// </summary>
    public static bool menu;
    /// <summary>
    /// Flag that determines if game should be paused
    /// </summary>
    public static bool pause;

    /// <summary>
    /// Get input from user
    /// </summary>
    public static void GetInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        jump = Input.GetButton("Jump");
        stop = Input.GetKey(KeyCode.F);
        shoot = Input.GetButton("Fire1");
        menu = Input.GetButton("OpenMenu");
        pause = Input.GetButtonDown("Pause");
    }
}
