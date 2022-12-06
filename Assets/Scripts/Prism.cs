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
    private bool _canDeactivateGrab = true;
    private bool _canPlayLightSound = true;
    private bool _canPlayTurnSound = true;
    
    [SerializeField] private Light _light;
    private bool _playerIsIn;

    public void OnBeamReceived()
    {
        isEnlighted = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsIn = false;
            GameManager.Instance.isInteractingWithMirror = false;
            if (_grabbedMirror)
            {
                _grabbedMirror = false;
                AkSoundEngine.PostEvent("MirrorTurn_Off", gameObject);
                _canPlayTurnSound = true;
            }
        }
    }

    private void Update()
    {
        foreach (GameObject side in sideColliders)
        {
            side.transform.Find("Emitter").GetComponent<PrismLight>().isPrismEnlighted = isEnlighted;
        }
        
        if (_lightStored <= 0 && _canDeactivateGrab)
        {
            AkSoundEngine.PostEvent("MirrorLight_Off", gameObject);
            _canPlayLightSound = true;
            if (_canDeactivateGrab)
            {
                GameManager.Instance.isInteractingWithMirror = false;
                _canDeactivateGrab = false;
            }
        }
        
        if ((GameManager.Instance.areMirrorsStep || !isEnlighted) && _lightStored <= 0) 
        {
            _grabbedMirror = false;
            return;
        }

        _canDeactivateGrab = true;

        if (isEnlighted)
        {
            if (_canPlayLightSound)
            {
                AkSoundEngine.PostEvent("MirrorLight", gameObject);
                _canPlayLightSound = false;
            }
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

        if ((Mouse.current.leftButton.isPressed && _playerIsIn) || _grabbedMirror)
        {
            _grabbedMirror = true;
            GameManager.Instance.isInteractingWithMirror = true;
            
            transform.Rotate(0f, Mouse.current.delta.ReadValue().x, 0f, Space.World);
            if (_canPlayTurnSound)
            {
                AkSoundEngine.PostEvent("MirrorTurn", gameObject);
                _canPlayTurnSound = false;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GameManager.Instance.isInteractingWithMirror = false;
            
            if (_grabbedMirror)
            {
                _grabbedMirror = false;
                AkSoundEngine.PostEvent("MirrorTurn_Off", gameObject);
                _canPlayTurnSound = true;
            }
        }

        isEnlighted = false;
    }
}
