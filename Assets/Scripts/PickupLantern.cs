using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupLantern : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TooltipManager.Instance.currentTooltip = TooltipTypes.LanternPickup;
            TooltipManager.Instance.ToggleTooltip("Press the Left Mouse Button to pick up the lantern");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && TooltipManager.Instance.currentTooltip == TooltipTypes.LanternPickup)
        {
            TooltipManager.Instance.CloseTooltip();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Mouse.current.leftButton.isPressed)
        {
            HandAnimatorController.Instance.HandleLanternInput(true);
            if (TooltipManager.Instance.currentTooltip == TooltipTypes.LanternPickup) 
                TooltipManager.Instance.CloseTooltip();
            Destroy(gameObject);
        }
    }

   
}
