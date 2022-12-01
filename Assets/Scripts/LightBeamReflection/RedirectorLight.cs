using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectorLight : MonoBehaviour
{
    public Color laserColor = Color.yellow;
    public Material material;
    private LightBeam _beam;
    private GameObject _previousBeam;
    [SerializeField] private GameObject _emitter;
    public bool isRedirectorEnlighted;

    public void OnBeamReceived()
    {
        isRedirectorEnlighted = true;
    }
    
    void Update()
    {
        if(_previousBeam) Destroy(_previousBeam);

        if (!isRedirectorEnlighted) return;

        if (LanternManager.Instance.lanternCharge <= 0) return;

        _beam = new LightBeam(_emitter.transform.position, _emitter.transform.forward, material, laserColor);
        _previousBeam = _beam.lightObj;
            
        isRedirectorEnlighted = false;
    }
}
