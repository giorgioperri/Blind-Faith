using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismLight : MonoBehaviour
{
    public Color laserColor = Color.green;
    public Material material;
    private LightBeam _beam;
    private GameObject _previousBeam;
    public bool isPrismEnlighted;
    public bool shouldShoot = true;

    public PrismLight leftPrismLight;
    public PrismLight rightPrismLight;
    
    private LightMeshSpawner _lightMeshSpawner;

    private void Awake()
    {
        _lightMeshSpawner = gameObject.AddComponent<LightMeshSpawner>();
    }

    public void OnBeamReceived()
    {
        leftPrismLight.laserColor = Color.green;
        rightPrismLight.laserColor = Color.red;
    }
    
    void Update()
    {
        if(_previousBeam) Destroy(_previousBeam);

        if (!isPrismEnlighted) return;

        if (LanternManager.Instance.lanternCharge <= 0) return;

        if (shouldShoot)
        {
            _beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material, laserColor, _lightMeshSpawner);
            _previousBeam = _beam.lightObj;
        }
        shouldShoot = true;
    }
}
