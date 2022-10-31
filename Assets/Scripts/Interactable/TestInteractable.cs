using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    private GameManager _gameManager;
    private FirstPersonController _firstPersonController;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _firstPersonController = FindObjectOfType<FirstPersonController>();
    }
    public override void OnFocus()
    {
        Debug.Log("Currently looking at:" + gameObject.name);
    }

    public override void OnInteraction()
    { 
        transform.Rotate(0f, 45 * Time.deltaTime, 0f, Space.Self);
    }

    public override void OnLoseFocus()
    {
        Debug.Log(" stopped looking at:" + gameObject.name);
    }

}
