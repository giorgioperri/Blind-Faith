using System;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjects : Interactable
{
    [SerializeField] private float _angleOfRotation = 30;
    
    private Vector3 _desiredAngle = Vector3.zero;
    private float _turnTime = 1f;
    private float _rate = 60;

    private bool _isTurning;
    private bool _grabbedMirror;

    public bool isEnlighted;

    public void OnBeamReceived()
    {
        isEnlighted = true;
    }
    
    public override void OnInteraction()
    {
        if (!isEnlighted) return;
        
        if (Keyboard.current.eKey.wasPressedThisFrame && !_isTurning && GameManager.Instance.areMirrorsStep)
        {
            _isTurning = true;
            _desiredAngle = transform.rotation.eulerAngles + new Vector3(0f, _angleOfRotation, 0f);
            StartCoroutine(TurnTo());
        }
    }
    public override void OnAltInteraction()
    {
        if (!isEnlighted) return;
        
        if (Keyboard.current.qKey.wasPressedThisFrame && !_isTurning && GameManager.Instance.areMirrorsStep)
        {
            _isTurning = true;
            _desiredAngle = transform.rotation.eulerAngles - new Vector3(0f, _angleOfRotation, 0f);
            StartCoroutine(TurnTo());
        }
    }

    private void Update()
    {
        if (GameManager.Instance.areMirrorsStep || !isEnlighted)
        {
            _grabbedMirror = false;
            return;
        }
        
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

    IEnumerator TurnTo () 
    {
        Vector3 original_angle = transform.rotation.eulerAngles;
        for (float i = 0f; i < 1f + _turnTime / _rate; i += _turnTime / _rate) 
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(original_angle, _desiredAngle, i));
            yield return new WaitForSeconds(_turnTime/_rate);
        }
        _isTurning = false;
    }
}
