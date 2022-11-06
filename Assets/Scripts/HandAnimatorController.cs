using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HandAnimatorController : MonoBehaviour
{
    private Animator _animController;
    private StarterAssetsInputs _inputs;
    public bool hasRaisedLantern;
    
    private void Awake()
    {
        _inputs = FindObjectOfType<StarterAssetsInputs>();
        _animController = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_inputs.hasRaisedLantern && !_animController.GetBool("hasRaisedLantern"))
        {
            _animController.SetBool("hasRaisedLantern", true);
            if(PlayerSoundController.Instance.EquipLantern != null) 
                PlayerSoundController.Instance.EquipLantern.Post(PlayerSoundController.Instance.gameObject);
            if(PlayerSoundController.Instance.LanternLoop != null) 
                PlayerSoundController.Instance.LanternLoop.Post(PlayerSoundController.Instance.gameObject);
            _inputs.hasRaisedLantern = false;
            hasRaisedLantern = true;
        }
        else if (_inputs.hasRaisedLantern && _animController.GetBool("hasRaisedLantern"))
        {
            _animController.SetBool("hasRaisedLantern", false);
            if(PlayerSoundController.Instance.UnequipLantern != null) 
                PlayerSoundController.Instance.UnequipLantern.Post(PlayerSoundController.Instance.gameObject);
            if(PlayerSoundController.Instance.StopLanternLoop != null) 
                PlayerSoundController.Instance.StopLanternLoop.Post(PlayerSoundController.Instance.gameObject);
            _inputs.hasRaisedLantern = false;
            hasRaisedLantern = false;
        }
    }
}
