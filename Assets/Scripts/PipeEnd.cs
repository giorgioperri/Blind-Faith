using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public enum PipeTypes
{
    Red,
    Green
}

public class PipeEnd : MonoBehaviour
{
    [SerializeField] private bool _isPipeInput;
    [SerializeField] private bool _shouldShoot;
    [SerializeField] private bool _isPipeOn;
    [SerializeField] private PipeTypes _pipeType;
    [SerializeField] private GameObject redTarget;
    [SerializeField] private GameObject greenTarget;

    [SerializeField] private Material _tubeOff;
    [SerializeField] private Material _tubeGreen;
    [SerializeField] private Material _tubeRed;

    private List<GameObject> _pipePieces = new List<GameObject>();
    [SerializeField] private GameObject _pipeEnd;

    
    private LightBeam _beam;
    private GameObject _previousBeam;
    private Color _laserColor;
    public Material material;

    [UsedImplicitly]
    public void OnCorrectBeamReceived()
    {
        _shouldShoot = true;
    }

    private void Start()
    {
        _laserColor = _pipeType == PipeTypes.Red ? Color.red : Color.green;
        if (_isPipeInput)
        {
            foreach (Transform piece in transform.parent)
            {
                _pipePieces.Add(piece.gameObject);
            }
            
            Instantiate(_pipeType == PipeTypes.Red ? redTarget : greenTarget, transform.position, quaternion.identity, transform);
        }
    }

    private void Update()
    {
        if (_shouldShoot && !_isPipeOn)
        {
            TurnOnPipe();
        }
        else if (!_shouldShoot)
        {
            TurnOffPipe();
        }

        if(_previousBeam) Destroy(_previousBeam);

        if (!_isPipeOn || !_isPipeInput) return;

        if (LanternManager.Instance.lanternCharge <= 0) return;

        if (_shouldShoot)
        {
            _beam = new LightBeam(_pipeEnd.transform.position, _pipeEnd.transform.forward, material, _laserColor);
            _previousBeam = _beam.lightObj;
        }
        _shouldShoot = true;

        _shouldShoot = false;
    }

    public void TurnOnPipe()
    {
        _isPipeOn = true;
        
        foreach (GameObject pipePiece in _pipePieces)
        {
            pipePiece.GetComponent<MeshRenderer>().material = _pipeType == PipeTypes.Red ? _tubeRed : _tubeGreen;
        }
    }
    
    public void TurnOffPipe()
    {
        _isPipeOn = false;
        
        foreach (GameObject pipePiece in _pipePieces)
        {
            pipePiece.GetComponent<MeshRenderer>().material = _tubeOff;
        }
    }
}
