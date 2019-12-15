﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHelper
{
    public static float horizontalMove;
    public static float verticalMove;
    public static bool jump;
    public static bool stop;
    public static bool shoot;
    public static bool menu;
    public static bool pause;

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
