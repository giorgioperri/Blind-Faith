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
    SeeSocket,
    PlaceSocket,
    Sprint,
    Health
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
        tooltipAnimator.SetBool("Open", true);
    }
    
    public void CloseTooltip()
    {
        tooltipAnimator.SetBool("Open", false);

        if (currentTooltip == TooltipTypes.LanternPickup)
        {
            if(LanternManager.Instance.lanternIsRaised) StartCoroutine(StartChargeTooltip());
        }

        currentTooltip = TooltipTypes.None;
    }
    
    private IEnumerator StartChargeTooltip()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        currentTooltip = TooltipTypes.LanternCharge;
        ToggleTooltip("While on the Altar, Look at the Fallen Angel and hold the Left Mouse button to charge up your Lantern");
    }
    
    public IEnumerator StartHealthTooltip()
    {
        currentTooltip = TooltipTypes.Sprint;
        ToggleTooltip("Stay close to the light to regain sanity");
        yield return new WaitForSecondsRealtime(3f);
        CloseTooltip();
    }
}
