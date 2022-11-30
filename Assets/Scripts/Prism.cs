using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MonoBehaviour
{
    [SerializeField] public List<GameObject> sideColliders;
    public bool isEnlighted;

    public void OnBeamReceived()
    {
        isEnlighted = true;
    }

    private void Update()
    {
        foreach (GameObject side in sideColliders)
        {
            side.transform.Find("Emitter").GetComponent<PrismLight>().isPrismEnlighted = isEnlighted;
        }

        isEnlighted = false;
    }
}
