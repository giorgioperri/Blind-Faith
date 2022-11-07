using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private Animator _doorAnim = null;
    private ShootLight _beam;

    public override void OnFocus()
    {
        
    }

    public override void OnInteraction()    
    {
        if (transform.GetComponent<Door>().isActiveAndEnabled)
        {
            _doorAnim.Play("DoorOpen", 0, 0.0f);
        }

    }

    public override void OnLoseFocus()
    {
    }
    // Update is called once per frame
    void Update()
    {
        _beam = GameObject.Find("LaserPointer").GetComponent<ShootLight>();
        if (!_beam.hitTheDoor)
        {
            transform.GetComponent<Door>().enabled = false;
            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.gray;
        }
        
    }
    public override void OnPressQ()
    {
    }
}
