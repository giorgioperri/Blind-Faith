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
}
