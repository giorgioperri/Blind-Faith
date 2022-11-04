using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : Interactable
{
    public override void OnFocus()
    {
    }

    public override void OnInteraction()
    {
        transform.Rotate(0f, 30 * Time.deltaTime, 0f, Space.Self);
    }

    public override void OnLoseFocus()
    {
    }

    public override void OnPressQ()
    {
        transform.Rotate(0f, -30 * Time.deltaTime, 0f, Space.Self);
    }
}
