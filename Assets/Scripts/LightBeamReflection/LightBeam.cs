using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam 
{
    private Vector3 _position, _direction;

    public GameObject lightObj;
    private LineRenderer _lightLaser;
    private List<Vector3> _lightIndices = new List<Vector3>();
    public bool hitTheDoor;

    public LightBeam(Vector3 position, Vector3 direction, Material material, Color color)
    {
        _lightLaser = new LineRenderer();
        lightObj = new GameObject();
        lightObj.tag = "LightBeam";
        lightObj.name = "Light Beam";
        _position = position;
        _direction = direction;
        _lightLaser = lightObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        _lightLaser.startWidth = 0.1f;
        _lightLaser.endWidth = 0.1f;
        _lightLaser.material = material;
        _lightLaser.startColor = color;
        _lightLaser.endColor = color;

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
        if(hitInfo.collider.gameObject.CompareTag("Mirror"))
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            if (hitInfo.collider.gameObject.GetComponent<RotateObjects>())
            {
                hitInfo.collider.gameObject.SendMessage("OnBeamReceived");
            }

            CastRay(pos, dir, laser);
        }
        else if(hitInfo.collider.gameObject.CompareTag("Door"))
        {
            hitTheDoor = true;
            if (hitInfo.collider.gameObject.GetComponent<Door>().enabled == false && hitTheDoor)
            {
                hitInfo.collider.gameObject.GetComponent<Door>().enabled = true;
                hitInfo.collider.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            }
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        } else if (hitInfo.collider.gameObject.CompareTag("Player"))
        {
            HealthSystem.Instance.Heal(); 
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if(hitInfo.collider.gameObject.CompareTag("Prism"))
        {
            GameObject _emitter = hitInfo.transform.Find("Emitter").gameObject;
            hitInfo.transform.parent.SendMessage("OnBeamReceived");
            _emitter.SendMessage("OnBeamReceived");
            _emitter.GetComponent<PrismLight>().shouldShoot = false;
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            hitTheDoor = false;
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }
}
