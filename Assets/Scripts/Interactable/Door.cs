using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : Interactable
{
    [SerializeField]
    private Animator _doorAnim = null;
    private ShootLight _beam;
    public bool isPrismatic;

    public bool greenHit;
    public bool redHit;

    public override void OnInteraction() { }

    public void OnBeamReceived()
    {
        if (isPrismatic) return;
        gameObject.tag = "Untagged";
        _doorAnim.Play("DoorOpen", 0, 0.0f);
        AkSoundEngine.PostEvent("DoorOpen", PlayerSoundController.Instance.gameObject);
        Destroy(this);
    }

    private void Update()
    {
        if (!isPrismatic) return;
        
        _doorAnim.SetBool("greenHit", greenHit);
        _doorAnim.SetBool("redHit", redHit);

        if (greenHit && redHit)
        {
            gameObject.tag = "Untagged";
            _doorAnim.Play("DoorOpen", 0, 0.0f);
            AkSoundEngine.PostEvent("DoorOpen", PlayerSoundController.Instance.gameObject);
            Destroy(this);
        }

        greenHit = false;
        redHit = false;
    }
}
