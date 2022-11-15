using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjects : Interactable
{

    [SerializeField] private float _angleOfRotation = 30;
    public override void OnInteraction()
    {
        if (Keyboard.current.fKey.isPressed) return;

        else if (Keyboard.current.eKey.wasPressedThisFrame) transform.Rotate(0f, _angleOfRotation, 0f, Space.Self);
    }
    public override void OnAltInteraction()
    {
        if (Keyboard.current.fKey.isPressed) return;

        else if (Keyboard.current.qKey.wasPressedThisFrame) transform.Rotate(0f, -_angleOfRotation, 0f, Space.Self);
        
    }
}
