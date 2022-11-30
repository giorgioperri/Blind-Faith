using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismLight : MonoBehaviour
{
    public Material material;
    private LightBeam _beam;
    private GameObject _previousBeam;
    public bool isPrismEnlighted;
    public bool shouldShoot = true;

    void Update()
    {
        if(_previousBeam) Destroy(_previousBeam);

        if (!isPrismEnlighted) return;

        if (LanternManager.Instance.lanternCharge <= 0) return;

        if (shouldShoot)
        {
            _beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
            _previousBeam = _beam.lightObj;
        }
        shouldShoot = true;
    }
}
