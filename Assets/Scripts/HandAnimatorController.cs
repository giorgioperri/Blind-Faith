using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HandAnimatorController : MonoBehaviour
{
    private Animator _animController;
    private StarterAssetsInputs _inputs;
    
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
            if(PlayerSoundManager.Instance.EquipLantern != null) 
                PlayerSoundManager.Instance.EquipLantern.Post(PlayerSoundManager.Instance.gameObject);
            if(PlayerSoundManager.Instance.LanternLoop != null) 
                PlayerSoundManager.Instance.LanternLoop.Post(PlayerSoundManager.Instance.gameObject);
            _inputs.hasRaisedLantern = false;
        }
        else if (_inputs.hasRaisedLantern && _animController.GetBool("hasRaisedLantern"))
        {
            _animController.SetBool("hasRaisedLantern", false);
            if(PlayerSoundManager.Instance.UnequipLantern != null) 
                PlayerSoundManager.Instance.UnequipLantern.Post(PlayerSoundManager.Instance.gameObject);
            if(PlayerSoundManager.Instance.StopLanternLoop != null) 
                PlayerSoundManager.Instance.StopLanternLoop.Post(PlayerSoundManager.Instance.gameObject);
            _inputs.hasRaisedLantern = false;
        }
    }
}
