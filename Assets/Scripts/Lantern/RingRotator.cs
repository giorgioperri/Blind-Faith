using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RotationAxis
{
    X, Y, Z
}

public class RingRotator : MonoBehaviour
{
    [SerializeField] private RotationAxis _rotationAxis;
    [SerializeField] private float _rotationSpeed = .2f;
    private bool _align;
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        float distanceFromBathingAreaOffset = 
            Vector3.Distance(transform.position, LightBathingArea.Instance.transform.position);

        if (distanceFromBathingAreaOffset > 5)
        {
            distanceFromBathingAreaOffset = 0;
        }
        else
        {
            distanceFromBathingAreaOffset = 1f / distanceFromBathingAreaOffset;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        ray.direction *= 100;
        
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            _align = hit.collider.CompareTag("ChargingTarget");
        }

        if(_align)
        {
            var step = 300 * Time.deltaTime;

            // Rotate our transform a step closer to the target's.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, step);
            return;
        }
        
        switch (_rotationAxis)
        {
            case RotationAxis.X:
                transform.Rotate(new Vector3(1,0,1), _rotationSpeed + distanceFromBathingAreaOffset);
                break;
            case RotationAxis.Y:
                transform.Rotate(new Vector3(1,1,0), _rotationSpeed -.05f + distanceFromBathingAreaOffset);
                break;
            case RotationAxis.Z:
                transform.Rotate(new Vector3(0,1,1), _rotationSpeed -.1f + distanceFromBathingAreaOffset);
                break;
        }

    }
}
