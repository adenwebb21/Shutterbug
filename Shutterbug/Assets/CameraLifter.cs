using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLifter : MonoBehaviour
{
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null)
            return; // No gamepad connected.

        if (mouse.rightButton.isPressed)
        {
            // 'Use' code here
        }
    }
}
