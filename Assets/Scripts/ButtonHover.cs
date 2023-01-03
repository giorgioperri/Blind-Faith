using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public void Yellow()
    {
        transform.localScale *= 1.2f;
    }
    
    public void White()
    {
        transform.localScale *= .8f;
    }
}
