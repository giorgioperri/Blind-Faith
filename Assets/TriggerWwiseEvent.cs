using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWwiseEvent : MonoBehaviour
{
    [Header("Footstep Parameters")]
    [SerializeField] public AK.Wwise.State stateOn;
    [SerializeField] public AK.Wwise.State stateOff;

    private void Start()
    {
        stateOff.SetValue();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stateOn.SetValue();
            Debug.Log("Trigger entere'd");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            stateOff.SetValue();
        }
    }

}
