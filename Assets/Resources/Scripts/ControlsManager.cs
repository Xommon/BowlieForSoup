using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class ControlsManager
{
    //private static InputActionAsset inputActions;

    //public static Controls controls = new Controls();
    //public static Vector2 movementLefStick, movementRightStick;
    //public static InputAction leftStick, rightStick, a, b, x ,y, r, l, zr, zl, start, select, leftStickPress, rightStickPress, up, down, left, right;

    public static Vector2 Stick(int index)
    {
        if (index == 0)
        {
            // Left Stick
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else if (index == 1)
        {
            // Right Stick
            return new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        }
        else if (index == 2)
        {
            // DPad
            return new Vector2(Input.GetAxis("DHorizontal"), Input.GetAxis("DVertical"));
        }
        else if (index == 3)
        {
            // ZR and ZL
            return new Vector2(Input.GetAxis("ZR"), Input.GetAxis("ZL"));
        }
        return Vector2.zero;
    }

    private static int[] taps = new int[8];
    private static string[] axes = new string[] { "Horizontal", "Vertical", "Mouse Y", "Mouse X", "DHorizontal", "DVertical", "ZR", "ZL" };
    public static bool StickTap(string stickIndex)
    {
        if (!axes.Contains<string>(stickIndex))
        {
            // Cannot find stick index
            Debug.Log("No stick called " + stickIndex);
            return false;
        }
        else if (taps[Array.IndexOf(axes, stickIndex)] == 0 && Input.GetAxis(stickIndex) != 0)
        {
            // Stick engaged for the first time
            taps[Array.IndexOf(axes, stickIndex)] = 1;
            return true;
        }
        else if (taps[Array.IndexOf(axes, stickIndex)] == 1 && Input.GetAxis(stickIndex) != 0)
        {
            // Stick has been engaged before and is still being engaged
            return false;
        }
        else if (taps[Array.IndexOf(axes, stickIndex)] == 1 && Input.GetAxis(stickIndex) == 0)
        {
            // Stick has been engaged before but now the user has disengaged
            taps[Array.IndexOf(axes, stickIndex)] = 0;
            return false;
        }

        return false;
    }

    public static void Button()
    {
        string[] allButtons = new string[] { "A", "B", "X", "Y", "R", "L", "ZR", "ZL", "Start", "Select", "LeftStickButton", "RightStickButton", "Up" };
        for (int i = 0; i < allButtons.Length; i++)
        {
            if (Button(allButtons[i]))
            {
                Debug.Log(allButtons[i] + " Pressed");
                return;
            }
        }

        Debug.Log("No Button Pressed");
    }

    public static bool Button(string name)
    {

        return Input.GetButton(name);
    }

    public static bool ButtonDown(string name)
    {
        return Input.GetButtonDown(name);
    }

    public static bool ButtonUp(string name)
    {
        return Input.GetButtonUp(name);
    }
}