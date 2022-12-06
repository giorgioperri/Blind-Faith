using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableThis : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
