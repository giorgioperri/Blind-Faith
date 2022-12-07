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
    public LightMeshSpawner lightMeshSpawner;
    
    public void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
        lightMeshSpawner = gameObject.AddComponent<LightMeshSpawner>();
    }
    
    void Update()
    {
        if(_previousBeam) Destroy(_previousBeam);

        if (LanternManager.Instance.lanternCharge <= 0)
        {
            lightMeshSpawner.DestroyLightBeam();
            return;
        }
        
        _beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material, LaserColor.Yellow, lightMeshSpawner);
        _previousBeam = _beam.lightObj;
        hitTheDoor = _beam.hitTheDoor;
    }
}
