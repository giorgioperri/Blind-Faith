using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public enum PipeTypes
{
    Red,
    Green,
    Yellow
}

public class PipeEnd : MonoBehaviour
{
    [SerializeField] private bool _isPipeInput;
    [SerializeField] private bool _shouldShoot;
    [SerializeField] private bool _isPipeOn;
    [SerializeField] private PipeTypes _pipeType;
    [SerializeField] private GameObject redTarget;
    [SerializeField] private GameObject greenTarget;
    [SerializeField] private GameObject yellowTarget;

    [SerializeField] private Material _tubeOff;
    [SerializeField] private Material _tubeGreen;
    [SerializeField] private Material _tubeRed;
    [SerializeField] private Material _tubeYellow;

    private List<GameObject> _pipePieces = new List<GameObject>();
    [SerializeField] private GameObject _pipeEnd;

    
    private LightBeam _beam;
    private GameObject _previousBeam;
    private LaserColor _laserColor;
    public Material material;
    
    private LightMeshSpawner _lightMeshSpawner;

    private void Awake()
    {
        _lightMeshSpawner = gameObject.AddComponent<LightMeshSpawner>();
    }

    [UsedImplicitly]
    public void OnCorrectBeamReceived()
    {
        _shouldShoot = true;
    }

    private void Start()
    {
        switch (_pipeType)
        {
            case PipeTypes.Green:
                _laserColor = LaserColor.Green;
                break;
            case PipeTypes.Red:
                _laserColor = LaserColor.Red;
                break;
            case PipeTypes.Yellow:
                _laserColor = LaserColor.Yellow;
                break;
        }
        
        if (_isPipeInput)
        {
            foreach (Transform piece in transform.parent)
            {
                _pipePieces.Add(piece.gameObject);
            }

            switch (_pipeType)
            {
                case PipeTypes.Green:
                    Instantiate(greenTarget, transform.position, transform.rotation, transform);
                    break;
                case PipeTypes.Red:
                    Instantiate(redTarget, transform.position, transform.rotation, transform);
                    break;
                case PipeTypes.Yellow:
                    Instantiate(yellowTarget, transform.position, transform.rotation, transform);
                    break;
            }
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

        if (LanternManager.Instance.lanternCharge <= 0)
        {
            TurnOffPipe();
            _shouldShoot = false;
            return;
        }

        if (!_isPipeOn || !_isPipeInput) return;

        if (_shouldShoot)
        {
            _beam = new LightBeam(_pipeEnd.transform.position, _pipeEnd.transform.forward, material, _laserColor, _lightMeshSpawner);
            _previousBeam = _beam.lightObj;
        }
        _shouldShoot = false;
    }

    public void TurnOnPipe()
    {
        _isPipeOn = true;
        
        foreach (GameObject pipePiece in _pipePieces)
        {
            switch (_pipeType)
            {
                case PipeTypes.Green:
                    pipePiece.GetComponent<MeshRenderer>().material = _tubeGreen;
                    break;
                case PipeTypes.Red:
                    pipePiece.GetComponent<MeshRenderer>().material = _tubeRed;
                    break;
                case PipeTypes.Yellow:
                    pipePiece.GetComponent<MeshRenderer>().material = _tubeYellow;
                    break;
            }
        }
    }
    
    public void TurnOffPipe()
    {
        _isPipeOn = false;
        
        foreach (GameObject pipePiece in _pipePieces)
        {
            pipePiece.GetComponent<MeshRenderer>().material = _tubeOff;
        }
        
        _lightMeshSpawner.DestroyLightBeam();
    }
}
