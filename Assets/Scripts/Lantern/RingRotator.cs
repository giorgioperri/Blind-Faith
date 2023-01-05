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
    [SerializeField] private float _rotationSpeed = .4f;
    private Quaternion originalRotation;

    public Transform lightBathingArea;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        if (lightBathingArea == null) return;
        
        float distanceFromBathingAreaOffset = 
            Vector3.Distance(transform.position, lightBathingArea.position);

        if (distanceFromBathingAreaOffset > 5)
        {
            distanceFromBathingAreaOffset = 0;
        }
        else
        {
            distanceFromBathingAreaOffset = .9f / distanceFromBathingAreaOffset;
        }

        if(GameManager.Instance.isLookingAtAngel)
        {
            var step = 300 * Time.deltaTime;
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
