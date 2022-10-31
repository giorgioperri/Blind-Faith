using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animController;
    private StarterAssetsInputs _input = FindObjectOfType<StarterAssetsInputs>();

    private void Update()
    {
        _animController.SetBool("raiseLantern", _input.raiseLantern);
    }
}
