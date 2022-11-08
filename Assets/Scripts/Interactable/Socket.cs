using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Socket : Interactable
{
    [SerializeField]
    private GameObject _lanternObject;
    [SerializeField]
    private GameObject _lanternFollowSocket;
    [SerializeField]
    private GameObject _lightSourceLantern;
    [SerializeField]
    private GameObject _handController;

    private GameObject _lightBeam;

    public override void OnFocus()
    {
    }

    public override void OnInteraction()
    {
        // The implementation works for now, but probably needs improvements in future
        if (!LanternManager.Instance.isInsideSocket && _handController.GetComponent<HandAnimatorController>().hasRaisedLantern && 
            (Keyboard.current.eKey.wasPressedThisFrame /*|| Gamepad.current.buttonWest.wasPressedThisFrame)*/))
        {
            
            _lightSourceLantern.SetActive(true);
            _lanternObject.transform.parent = transform;
            _lanternObject.transform.position = transform.position;
            _lanternObject.transform.rotation = transform.rotation;

            
            LanternManager.Instance.isInsideSocket = !LanternManager.Instance.isInsideSocket;

        }
        else if (LanternManager.Instance.isInsideSocket && _handController.GetComponent<HandAnimatorController>().hasRaisedLantern &&
            (Keyboard.current.eKey.wasPressedThisFrame /*|| Gamepad.current.buttonWest.wasPressedThisFrame)*/))
        {
            _lightBeam = GameObject.Find("Light Beam");
            Destroy(_lightBeam);
            _lightSourceLantern.SetActive(false);
            _lanternObject.transform.parent = _lanternFollowSocket.transform;
            _lanternObject.transform.position = _lanternFollowSocket.transform.position;
            _lanternObject.transform.rotation = _lanternFollowSocket.transform.rotation;
            LanternManager.Instance.isInsideSocket = !LanternManager.Instance.isInsideSocket;
        }
    }
    public override void OnLoseFocus()
    {
    }

    public override void OnPressQ()
    {
            
    }
}