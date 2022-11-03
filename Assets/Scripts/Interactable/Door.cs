using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    public bool open;
    Quaternion targetAngle90 = Quaternion.Euler(0, 0, 90);
    Quaternion targetAngle0 = Quaternion.Euler(0, 0, 0);
    public Quaternion currentAngle;


    public override void OnFocus()
    {
        
    }

    public override void OnInteraction()
    {
        if(transform.parent.rotation.y <= 90.0f)
        {
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, currentAngle, 0.2f);

        }
    }

    public override void OnLoseFocus()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = targetAngle0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPressQ()
    {
    }
}
