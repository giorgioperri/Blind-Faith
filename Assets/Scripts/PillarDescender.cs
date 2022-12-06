using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDescender : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    public void OnBeamReceived()
    {
        anim.SetTrigger("Descend");
        AkSoundEngine.PostEvent("MassiveDevice", gameObject);
    }
}
