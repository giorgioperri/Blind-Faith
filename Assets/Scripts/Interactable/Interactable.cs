using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        gameObject.layer = 10;
    }

    public abstract void OnInteraction();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();

    public abstract void OnPressQ();
}
