using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLight : MonoBehaviour
{

    public Material material;
    LightBeam beam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Light Beam"));
        beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
