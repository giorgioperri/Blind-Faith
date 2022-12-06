using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TooltipTypes
{
    None,
    LanternCharge,
    LanternPickup,
    WASDMove,
    RotateMirror,
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

        if (currentTooltip == TooltipTypes.LanternPickup)
        {
            if(LanternManager.Instance.lanternIsRaised) StartCoroutine(StartChargeTooltip());
        }
        
        if (currentTooltip == TooltipTypes.WASDMove)
        {
            StartCoroutine(StartSprintTooltip());
        }

        currentTooltip = TooltipTypes.None;
    }
    
    private IEnumerator StartChargeTooltip()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        currentTooltip = TooltipTypes.LanternCharge;
        ToggleTooltip("While on the Altar, Look at the Fallen Angel and hold the Left Mouse button to charge up your Lantern");
    }
    
    private IEnumerator StartSprintTooltip()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        currentTooltip = TooltipTypes.Sprint;
        ToggleTooltip("Use the Left Shift key to sprint");
        yield return new WaitForSecondsRealtime(2.5f);
        CloseTooltip();
    }
}
