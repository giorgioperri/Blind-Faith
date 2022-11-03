using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public override void OnFocus()
    {
        
    }

    public override void OnInteraction()
    {
        Debug.Log("Now I can interact with door");
    }

    public override void OnLoseFocus()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
