using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour
{
    
    [SerializeField] private float _rotationSpeed = .075f;

    void Update()
    {
        transform.Rotate(new Vector3(1,0,1), _rotationSpeed);
    }
}
