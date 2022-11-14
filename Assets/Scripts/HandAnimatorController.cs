using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    private Animator _animController;
    private StarterAssetsInputs _inputs;
    [HideInInspector] public bool hasRaisedLantern;
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
        if (FirstPersonController.Instance.currentInteractable &&
            FirstPersonController.Instance.currentInteractable.GetType() == typeof(Socket)) return;
            
        if (_inputs.hasRaisedLantern)
        {
            HandleLanternInput(!_animController.GetBool("hasRaisedLantern"));
        }
    }

    public void HandleLanternInput(bool isUp)
    {
        _animController.SetBool("hasRaisedLantern", isUp);
        if(PlayerSoundController.Instance.EquipLantern != null) 
            PlayerSoundController.Instance.EquipLantern.Post(PlayerSoundController.Instance.gameObject);
        _inputs.hasRaisedLantern = false;
        hasRaisedLantern = true;
        LanternManager.Instance.lanternIsRaised = true;
    }
}
