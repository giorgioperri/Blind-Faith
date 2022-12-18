using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum LaserColor
{
    Yellow, Green, Red
}

public class LightBeam 
{
    private Vector3 _position, _direction;

    public GameObject lightObj;
    private LineRenderer _lightLaser;
    private List<Vector3> _lightIndices = new List<Vector3>();
    public bool hitTheDoor;
    public LightMeshSpawner lightMeshSpawner;
    public LaserColor laserColor;
    
    public LightBeam(Vector3 position, Vector3 direction, Material material, LaserColor color, LightMeshSpawner _lightMeshSpawner)
    {
        _lightLaser = new LineRenderer();
        lightObj = new GameObject();
        lightObj.tag = "LightBeam";
        lightObj.name = "Light Beam";
        _position = position;
        _direction = direction;
        _lightLaser = lightObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        _lightLaser.startWidth = 0.07f;
        _lightLaser.endWidth = 0.07f;
        _lightLaser.material = material;
        laserColor = color;
        
        switch (laserColor)
        {
            case LaserColor.Yellow:
                _lightLaser.startColor = new Color(1.0f, .84f, .04f,.5f);
                _lightLaser.endColor = _lightLaser.startColor;
                break;
            case LaserColor.Red:
                _lightLaser.startColor = new Color(.94f, .14f, .24f, 0.5f);
                _lightLaser.endColor = _lightLaser.startColor;
                break;
            case LaserColor.Green:
                _lightLaser.startColor = new Color(.62f, .94f, .1f, .5f);
                _lightLaser.endColor = _lightLaser.startColor;
                break;
            default: 
                break;
        }

        lightMeshSpawner = _lightMeshSpawner;

        CastRay(position, direction, _lightLaser);
    }

    private void CastRay(Vector3 position, Vector3 direction, LineRenderer lightLaser)
    {
        _lightIndices.Add(position);
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 999))
        {
            CheckHit(hit, direction, lightLaser);
        }
        else
        {
            _lightIndices.Add(ray.GetPoint(999));
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
        //if(LightMeshSpawner.Instance) LightMeshSpawner.Instance.SetLightBeamPoints(_lightLaser);
        lightMeshSpawner.SetLightBeamPoints(_lightLaser, _lightLaser.startColor);
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {   
        if(hitInfo.collider.gameObject.CompareTag("Mirror"))
        {
            
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            
            if (hitInfo.collider.gameObject.GetComponent<RotateObjects>())
            {
                RotateObjects rotateScript = hitInfo.collider.gameObject.GetComponent<RotateObjects>();

                hitInfo.collider.gameObject.GetComponent<RotateObjects>().raycastDirection = dir;

                if (rotateScript.shouldStabilizeY)
                {
                    dir.y = 0;
                }
                
                hitInfo.collider.gameObject.SendMessage("OnBeamReceived");
            }

            CastRay(pos, dir, laser);
        }
        else if(hitInfo.collider.gameObject.CompareTag("Door"))
        {
            if (laserColor == LaserColor.Red)
            {
                hitInfo.collider.gameObject.GetComponent<Door>().redHit = true;
                _lightIndices.Add(hitInfo.point);
                UpdateLaser();
                return;
            } 
            
            if (laserColor == LaserColor.Green)
            {
                hitInfo.collider.gameObject.GetComponent<Door>().greenHit = true;
                _lightIndices.Add(hitInfo.point);
                UpdateLaser();
                return;
            }
            
            hitTheDoor = true;
            hitInfo.collider.gameObject.SendMessage("OnBeamReceived");
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if(hitInfo.collider.gameObject.CompareTag("Prism"))
        {
            if (laserColor != LaserColor.Yellow)
            {
                _lightIndices.Add(hitInfo.point);
                UpdateLaser();
                return;
            }
            
            GameObject _emitter = hitInfo.transform.Find("Emitter").gameObject;
            hitInfo.transform.parent.SendMessage("OnBeamReceived");
            _emitter.SendMessage("OnBeamReceived");
            _emitter.GetComponent<PrismLight>().shouldShoot = false;
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if (hitInfo.collider.gameObject.CompareTag("Redirector"))
        {
            hitInfo.transform.SendMessage("OnBeamReceived");
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else if (hitInfo.collider.gameObject.CompareTag("GreenTarget"))
        {
            if (laserColor == LaserColor.Green)
            {
                hitInfo.transform.parent.SendMessage("OnCorrectBeamReceived");
            }
            
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        } 
        else if (hitInfo.collider.gameObject.CompareTag("RedTarget"))
        {
            if (laserColor == LaserColor.Red)
            {
                hitInfo.transform.parent.SendMessage("OnCorrectBeamReceived");
            }
            
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        } 
        else if (hitInfo.collider.gameObject.CompareTag("YellowTarget"))
        {
            if (laserColor == LaserColor.Yellow)
            {
                hitInfo.transform.parent.SendMessage("OnCorrectBeamReceived");
            }
            
            _lightIndices.Add(hitInfo.point);
            UpdateLaser();
        } 
        else if (hitInfo.collider.gameObject.CompareTag("Pillar"))
        {
            hitInfo.transform.parent.SendMessage("OnBeamReceived");
            hitInfo.transform.tag = "Untagged";
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
