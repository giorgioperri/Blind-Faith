using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance;
    private Volume _volume;
    private ColorAdjustments _colorAdjustments;
    private Vignette _vignette;
    private Bloom _bloom;
    private ChromaticAberration _chromaticAberration;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }
    void Start()
    {
        VolumeProfile volumeProfile = GetComponent<Volume>().profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));

        //get vignette from profile to access its variables
        if (!volumeProfile.TryGet(out _vignette)) throw new System.NullReferenceException(nameof(_vignette));
        //get variables from colorAdjustments 
        if (!volumeProfile.TryGet(out _colorAdjustments)) throw new System.NullReferenceException(nameof(_colorAdjustments));
        //get variables from bloom 
        if (!volumeProfile.TryGet(out _bloom)) throw new System.NullReferenceException(nameof(_bloom));
        
        if (!volumeProfile.TryGet(out _chromaticAberration)) throw new System.NullReferenceException(nameof(_chromaticAberration));
    }

    public void ChangeAbberration(float chromaticAberrationValue)
    {
        _chromaticAberration.intensity.Override(chromaticAberrationValue);
    }
    
    //saturation values from -100 to 100
    public void ChangeSaturation(float saturationValue)
    {
        if(saturationValue <= -100f)
        {
            saturationValue = -100f;
        }
        else if(saturationValue >= 100f)
        {
            saturationValue = 100f;
        }
        _colorAdjustments.saturation.Override(saturationValue);
    }
    //Intensity values from 0 to 1
    public void ChangeVignetteIntensity(float intensity)
    {
        if(intensity <= 0f)
        {
            intensity = 0f;
        }
        else if(intensity >= 1f)
        {
            intensity = 1f;
        }
        _vignette.intensity.Override(intensity);
    }

    public void ChangeBloomIntensity(float intensity)
    {
        _bloom.intensity.Override(intensity);
    }
}
