using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EndFader : MonoBehaviour
{
    public Image endWhite;
    private float _computedAlpha;
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _computedAlpha = Vector3.Distance(other.transform.position, transform.position);

        _computedAlpha = math.remap(8, 3, 0, .2f, _computedAlpha);

        endWhite.color = new Color(endWhite.color.r, endWhite.color.g, endWhite.color.b, _computedAlpha);

        if (_computedAlpha >= .15f)
        {
            GameManager.Instance.InitEndEvent();
        }
    }
}
