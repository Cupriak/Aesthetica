using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHelper
{
    public static float horizontalMove;
    public static float verticalMove;
    public static bool jump;
    public static bool stop;

    public static void GetInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump");
        stop = Input.GetKey(KeyCode.F);
    }
}
