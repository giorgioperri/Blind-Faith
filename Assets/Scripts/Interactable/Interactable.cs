using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        gameObject.layer = 10;
    }

    public virtual void OnInteraction() { }
    public virtual void OnFocus() {}
    public virtual void OnLoseFocus() {}
    public virtual void OnAltInteraction() { }
}
