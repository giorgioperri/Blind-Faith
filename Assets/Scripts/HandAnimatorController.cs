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
    public static HandAnimatorController Instance;
    
    private void Awake()
    {
        _inputs = FindObjectOfType<StarterAssetsInputs>();
        _animController = gameObject.GetComponent<Animator>();
        
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void Update()
    {
        if (_inputs.hasRaisedLantern && !_animController.GetBool("hasRaisedLantern"))
        {
            RaiseLantern();
        }
        else if (_inputs.hasRaisedLantern && _animController.GetBool("hasRaisedLantern"))
        {
            LowerLantern();
        }
    }

    public void LowerLantern()
    {
        _animController.SetBool("hasRaisedLantern", false);
        if(PlayerSoundController.Instance.UnequipLantern != null) 
            PlayerSoundController.Instance.UnequipLantern.Post(PlayerSoundController.Instance.gameObject);
        _inputs.hasRaisedLantern = false;
        hasRaisedLantern = false;
    }

    public void RaiseLantern()
    {
        _animController.SetBool("hasRaisedLantern", true);
        if(PlayerSoundController.Instance.EquipLantern != null) 
            PlayerSoundController.Instance.EquipLantern.Post(PlayerSoundController.Instance.gameObject);
        _inputs.hasRaisedLantern = false;
        hasRaisedLantern = true;
    }
}
