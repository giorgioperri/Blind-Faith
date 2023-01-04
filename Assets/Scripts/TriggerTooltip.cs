using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTooltip : MonoBehaviour
{
    private bool _hasShownTooltip = false;
    [SerializeField] private TooltipTypes type;

    private void OnTriggerEnter(Collider other)
    {
        if (_hasShownTooltip) return;

        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case TooltipTypes.PlaceSocket:
                    TooltipManager.Instance.currentTooltip = TooltipTypes.PlaceSocket;
                    TooltipManager.Instance.ToggleTooltip("While close to the Socket, press the Left Mouse Button to place or pick up the Lantern");
                    break;
                case TooltipTypes.Sprint:
                    TooltipManager.Instance.currentTooltip = TooltipTypes.Sprint;
                    TooltipManager.Instance.ToggleTooltip("Use the Left Shift key to sprint");
                    break;
                default:
                    break;
            }
            
            _hasShownTooltip = true;
        }
    }
}
