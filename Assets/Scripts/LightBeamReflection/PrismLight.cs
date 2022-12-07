using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismLight : MonoBehaviour
{
    public LaserColor laserColor = LaserColor.Green;
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
        leftPrismLight.laserColor = LaserColor.Green;
        rightPrismLight.laserColor = LaserColor.Red;
    }
    
    void Update()
    {
        if(_previousBeam) Destroy(_previousBeam);

        if (!isPrismEnlighted)
        {
            _lightMeshSpawner.DestroyLightBeam();
            return;
        }

        if (LanternManager.Instance.lanternCharge <= 0) return;

        if (shouldShoot)
        {
            _beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material, laserColor, _lightMeshSpawner);
            _previousBeam = _beam.lightObj;
        }
        shouldShoot = true;
    }
}
