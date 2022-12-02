using System;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjects : Interactable
{
    private bool _isTurning;
    private bool _grabbedMirror;

    public bool isEnlighted;
    [SerializeField] private float _lightStored;
    [SerializeField] private bool _canDeactivateGrab = true;
    [SerializeField] private Light _light;
    public bool shouldStabilizeY;

    public void OnBeamReceived()
    {
        isEnlighted = true;
    }

    private void Update()
    {
        if (_lightStored <= 0 && _canDeactivateGrab)
        {
            GameManager.Instance.isInteractingWithMirror = false;
            _canDeactivateGrab = false;
        }
        
        if ((GameManager.Instance.areMirrorsStep || !isEnlighted) && _lightStored <= 0) 
        {
            _grabbedMirror = false;
            return;
        }

        _canDeactivateGrab = true;

        if (isEnlighted)
        {
            _lightStored += Time.deltaTime * 30;
        }
        else
        {
            _lightStored -= Time.deltaTime * 20;
        }

        _lightStored = _lightStored switch
        {
            >= 100 => 100,
            <= 0 => 0,
            _ => _lightStored
        };

        _light.intensity = math.remap(0, 100, 0, 15, _lightStored);
        GetComponent<MeshRenderer>().material.SetFloat("_GlowIntensity", math.remap(0, 100, 0, 4, _lightStored));

        if ((Mouse.current.leftButton.isPressed && FirstPersonController.Instance.currentInteractable == this) || _grabbedMirror)
        {
            _grabbedMirror = true;
            GameManager.Instance.isInteractingWithMirror = true;
            transform.Rotate(0f, Mouse.current.delta.ReadValue().x, 0f, Space.Self);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GameManager.Instance.isInteractingWithMirror = false;
            
            if (_grabbedMirror)
            {
                _grabbedMirror = false;
            }
        }

        isEnlighted = false;
    }
}
