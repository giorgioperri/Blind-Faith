using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HandAnimatorController : MonoBehaviour
{
    private Animator _animController;
    private StarterAssetsInputs _inputs;
    
    private void Awake()
    {
        _inputs = FindObjectOfType<StarterAssetsInputs>();
        _animController = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_inputs.hasRaisedLantern && !_animController.GetBool("hasRaisedLantern"))
        {
            _animController.SetBool("hasRaisedLantern", true);
            _inputs.hasRaisedLantern = false;
        }
        else if (_inputs.hasRaisedLantern && _animController.GetBool("hasRaisedLantern"))
        {
            _animController.SetBool("hasRaisedLantern", false);
            _inputs.hasRaisedLantern = false;
        }
    }
}
