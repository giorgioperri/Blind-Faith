using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TooltipTypes
{
    None,
    LanternCharge,
    Sprint,
}

public class TooltipManager : MonoBehaviour
{
    public TextMeshProUGUI tooltipObject;
    public Animator tooltipAnimator;
    public TooltipTypes currentTooltip = TooltipTypes.None;
    public static TooltipManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    public void ToggleTooltip(string text)
    {
        tooltipObject.text = text;
        tooltipAnimator.SetTrigger("Toggle");
    }
    
    public void CloseTooltip()
    {
        tooltipAnimator.SetTrigger("Toggle");
    }
}
