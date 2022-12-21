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
    private bool hasLantern;

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
        if(TooltipManager.Instance.currentTooltip == TooltipTypes.PlaceSocket) TooltipManager.Instance.CloseTooltip();
        
        // The implementation works for now, but probably needs improvements in future
        if (!LanternManager.Instance.isInsideSocket && _handController.GetComponent<HandAnimatorController>().hasRaisedLantern && 
            Mouse.current.leftButton.wasPressedThisFrame && !hasLantern)
        {
            _lightSourceLantern.SetActive(true);
            _lanternObject.transform.parent = transform;
            _lanternObject.transform.position = transform.position;
            _lanternObject.transform.rotation = transform.rotation;
            LanternManager.Instance.isInsideSocket = !LanternManager.Instance.isInsideSocket;
            hasLantern = true;
            HandAnimatorController.Instance.HandleLanternInput(false);
        }
        else if (LanternManager.Instance.isInsideSocket && _handController.GetComponent<HandAnimatorController>().hasRaisedLantern &&
            Mouse.current.leftButton.wasPressedThisFrame && hasLantern)
        {
            _lightSourceLantern.GetComponent<ShootLight>().lightMeshSpawner.DestroyLightBeam();
            _lightSourceLantern.SetActive(false);
            _lanternObject.transform.parent = _lanternFollowSocket.transform;
            _lanternObject.transform.position = _lanternFollowSocket.transform.position;
            _lanternObject.transform.rotation = _lanternFollowSocket.transform.rotation;
            LanternManager.Instance.isInsideSocket = !LanternManager.Instance.isInsideSocket;
            hasLantern = false;
            HandAnimatorController.Instance.HandleLanternInput(true);
            GameManager.Instance.DestroyAllLightBeams();
        }
    }
}
