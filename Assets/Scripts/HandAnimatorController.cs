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

    private void Start()
    {
        //
    }

    private void Update()
    {
        if (FirstPersonController.Instance.currentInteractable &&
            FirstPersonController.Instance.currentInteractable.GetType() == typeof(Socket))
        {
            if (Mouse.current.leftButton.isPressed)
            {
                _animController.SetBool("extendArms", false);
            } 
            
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && !LanternManager.Instance.isInsideSocket)
        {
            _animController.SetBool("extendArms", true);
        } 
        else if (Mouse.current.leftButton.wasReleasedThisFrame && !LanternManager.Instance.isInsideSocket)
        {
            _animController.SetBool("extendArms", false);
        }
    }

    public void HandleLanternInput(bool isUp)
    {
        _animController.SetBool("hasRaisedLantern", isUp);
        if (isUp)
        {
            if(PlayerSoundController.Instance.EquipLantern != null) 
                PlayerSoundController.Instance.EquipLantern.Post(PlayerSoundController.Instance.gameObject);
        }
        else
        {
            if(PlayerSoundController.Instance.UnequipLantern != null) 
                PlayerSoundController.Instance.UnequipLantern.Post(PlayerSoundController.Instance.gameObject);
        }
        _inputs.hasRaisedLantern = false;
        hasRaisedLantern = true;
        LanternManager.Instance.lanternIsRaised = true;
    }
}
