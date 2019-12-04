﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputCapture
{
    public static float horizontalMove;
    public static float verticalMove;
    public static bool jump;

    public static void GetInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump");
    }
}
