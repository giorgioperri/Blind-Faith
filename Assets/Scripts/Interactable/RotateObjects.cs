using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjects : Interactable
{
    public override void OnInteraction()
    {
        if (Keyboard.current.fKey.isPressed) return;
        transform.Rotate(0f, 22 * Time.deltaTime, 0f, Space.Self);
    }

    public override void OnAltInteraction()
    {
        if (Keyboard.current.fKey.isPressed) return;
        transform.Rotate(0f, -22 * Time.deltaTime, 0f, Space.Self);
    }
}
