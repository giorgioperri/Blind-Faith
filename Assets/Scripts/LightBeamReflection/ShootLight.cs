using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLight : MonoBehaviour
{
    public Material material;
    private LightBeam _beam;

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Light Beam"));
        _beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
