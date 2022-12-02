using System;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Socket : Interactable
{
    private GameObject _lanternObject;
    private GameObject _lanternFollowSocket;
    private GameObject _lightSourceLantern;
    private GameObject _handController;

    private GameObject _lightBeam;

    private void Start()
    {
        GameObject lantern = LanternManager.Instance.gameObject;
        _lanternObject = lantern;
        _lanternFollowSocket = lantern.transform.parent.gameObject;
        _lightSourceLantern = lantern.transform.Find("LaserPointer").gameObject;
        _handController = HandAnimatorController.Instance.gameObject;
    }

    public override void OnInteraction()
    {
        // The implementation works for now, but probably needs improvements in future
        if (!LanternManager.Instance.isInsideSocket && _handController.GetComponent<HandAnimatorController>().hasRaisedLantern && 
            (Mouse.current.leftButton.wasPressedThisFrame /* Keyboard.current.eKey.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame) */))
        {
            _lightSourceLantern.SetActive(true);
            _lanternObject.transform.parent = transform;
            _lanternObject.transform.position = transform.position;
            _lanternObject.transform.rotation = transform.rotation;
            LanternManager.Instance.isInsideSocket = !LanternManager.Instance.isInsideSocket;
            
            HandAnimatorController.Instance.HandleLanternInput(false);

        }
        else if (LanternManager.Instance.isInsideSocket && _handController.GetComponent<HandAnimatorController>().hasRaisedLantern &&
            (Mouse.current.leftButton.wasPressedThisFrame /* Keyboard.current.eKey.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame)*/))
        {
            _lightBeam = GameObject.Find("Light Beam");
            Destroy(_lightBeam);
            _lightSourceLantern.SetActive(false);
            _lanternObject.transform.parent = _lanternFollowSocket.transform;
            _lanternObject.transform.position = _lanternFollowSocket.transform.position;
            _lanternObject.transform.rotation = _lanternFollowSocket.transform.rotation;
            LanternManager.Instance.isInsideSocket = !LanternManager.Instance.isInsideSocket;

            HandAnimatorController.Instance.HandleLanternInput(true);
        }
    }
}
