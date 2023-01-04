using System;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class Prism : Interactable
{
    [SerializeField] public List<GameObject> sideColliders;
    private bool _isTurning;
    private bool _grabbedMirror;

    public bool isEnlighted;
    private float _lightStored;

    public void OnBeamReceived()
    {
        isEnlighted = true;
    }

    private void Update()
    {
        foreach (GameObject side in sideColliders)
        {
            side.transform.Find("Emitter").GetComponent<PrismLight>().isPrismEnlighted = isEnlighted;
        }

        isEnlighted = false;
    }
}
