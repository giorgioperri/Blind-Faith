using System;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjects : Interactable
{
    [SerializeField] private float _angleOfRotation = 30;
    
    private Vector3 _desiredAngle = Vector3.zero;
    private float _turnTime = 1f;
    private float _rate = 60;

    private bool _isTurning;
   
    public override void OnInteraction()
    {
        if (Keyboard.current.fKey.isPressed) return;
        
        if (Keyboard.current.eKey.wasPressedThisFrame && !_isTurning)
        {
            _isTurning = true;
            _desiredAngle = transform.rotation.eulerAngles + new Vector3(0f, _angleOfRotation, 0f);
            StartCoroutine(TurnTo());
        }
    }
    public override void OnAltInteraction()
    {
        if (Keyboard.current.fKey.isPressed) return;
        
        if (Keyboard.current.qKey.wasPressedThisFrame && !_isTurning)
        {
            _isTurning = true;
            _desiredAngle = transform.rotation.eulerAngles - new Vector3(0f, _angleOfRotation, 0f);
            StartCoroutine(TurnTo());
        }
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
