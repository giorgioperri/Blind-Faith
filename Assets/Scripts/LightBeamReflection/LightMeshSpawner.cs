using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMeshSpawner : MonoBehaviour
{
    public GameObject lightMeshPrefab;
    public static LightMeshSpawner Instance;
    private List<Vector3> _locallyStoredPoints;
    private List<GameObject> _lightMeshes;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;

        _locallyStoredPoints = new List<Vector3>();
        _lightMeshes = new List<GameObject>();
    }

    public void SetLightBeamPoints(LineRenderer _laser)
    {

        if (_locallyStoredPoints.Count != _laser.positionCount ||
            _locallyStoredPoints[_locallyStoredPoints.Count - 1] != _laser.GetPosition(_laser.positionCount - 1))
        {
            _locallyStoredPoints.Clear();
            foreach (GameObject o in _lightMeshes)
            {
                Destroy(o);
            }
            for (int i = 0; i < _laser.positionCount; i++)
            {
                _locallyStoredPoints.Add(_laser.GetPosition(i));
            }

            UpdateLightMeshes();
        }
    }

    private void UpdateLightMeshes()
    {
        for (int i = 0; i < _locallyStoredPoints.Count - 1; i++)
        {
            Vector3 dir = _locallyStoredPoints[i + 1] - _locallyStoredPoints[i];
            float length = dir.magnitude;
            //int meshesCount = Mathf.FloorToInt(length);
            for (float j = 0.0f; j < length; j+=1.0f)
            {
                Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
                _lightMeshes.Add(Instantiate(lightMeshPrefab, pos, Quaternion.LookRotation(dir, Vector3.up)));
                float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                _lightMeshes[_lightMeshes.Count-1].GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
                Debug.Log(cutoff);
            }
        }
    }
    
}
