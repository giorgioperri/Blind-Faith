using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBathingArea : MonoBehaviour
{
    public static LightBathingArea Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isBathingInLight = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isBathingInLight = false;
        }
    }
}
