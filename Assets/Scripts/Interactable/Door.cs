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

    public override void OnInteraction()    
    {
        if (Keyboard.current.fKey.isPressed) return;
        
        if (transform.GetComponent<Door>().isActiveAndEnabled)
        {
            _doorAnim.Play("DoorOpen", 0, 0.0f);
        }
    }
    
    void Update()
    {
        _beam = GameObject.Find("LaserPointer").GetComponent<ShootLight>();
        if (!_beam.hitTheDoor)
        {
            transform.GetComponent<Door>().enabled = false;
            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
