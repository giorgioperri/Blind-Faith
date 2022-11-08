using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLight : MonoBehaviour
{
    public Material material;
    private LightBeam _beam;
    private GameObject _previousBeam;
    public GameObject[] doors;
    public bool hitTheDoor;


    public void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
    }
    
    void Update()
    {
        if(_previousBeam) Destroy(_previousBeam);

        if (LanternManager.Instance.lanternCharge <= 0) return;
        
        _beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
        _previousBeam = _beam.lightObj;
        hitTheDoor = _beam.hitTheDoor;
    }
}
