using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam 
{
    private Vector3 _position, _direction;

    private GameObject _lightObj;
    private LineRenderer _lightLaser;
    private List<Vector3> _lightIndices = new List<Vector3>();
    public LightBeam(Vector3 position, Vector3 direction, Material material)
    {
        _lightLaser = new LineRenderer();
        _lightObj = new GameObject();
        _lightObj.name = "Light Beam";
        _position = position;
        _direction = direction;
        _lightLaser = _lightObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        _lightLaser.startWidth = 0.1f;
        _lightLaser.endWidth = 0.1f;
        _lightLaser.material = material;
        _lightLaser.startColor = Color.yellow;
        _lightLaser.endColor = Color.yellow;

        CastRay(position, direction, _lightLaser);
    }

    private void CastRay(Vector3 position, Vector3 direction, LineRenderer lightLaser)
    {
        _lightIndices.Add(position);
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 30))
        {
            CheckHit(hit, direction, lightLaser);
        }
        else
        {
            _lightIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    private void UpdateLaser()
    {
        int counter = 0;
        _lightLaser.positionCount = _lightIndices.Count;

        foreach(Vector3 idx in _lightIndices)
        {
            _lightLaser.SetPosition(counter, idx);
            counter++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if(hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if(hitInfo.collider.gameObject.tag == "Door")
        {
            //hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
            hitInfo.collider.gameObject.AddComponent<Door>();
            Vector3 pos = hitInfo.point;
            //CastRay(pos, direction, laser);
        }
        else
        {
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }
}
