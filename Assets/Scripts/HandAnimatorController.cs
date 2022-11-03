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
            if(SoundManager.Instance.EquipLantern != null) 
                SoundManager.Instance.EquipLantern.Post(SoundManager.Instance.gameObject);
            if(SoundManager.Instance.LanternLoop != null) 
                SoundManager.Instance.LanternLoop.Post(SoundManager.Instance.gameObject);
            _inputs.hasRaisedLantern = false;
        }
        else if (_inputs.hasRaisedLantern && _animController.GetBool("hasRaisedLantern"))
        {
            _animController.SetBool("hasRaisedLantern", false);
            if(SoundManager.Instance.UnequipLantern != null) 
                SoundManager.Instance.UnequipLantern.Post(SoundManager.Instance.gameObject);
            if(SoundManager.Instance.StopLanternLoop != null) 
                SoundManager.Instance.StopLanternLoop.Post(SoundManager.Instance.gameObject);
            _inputs.hasRaisedLantern = false;
        }
    }
}
