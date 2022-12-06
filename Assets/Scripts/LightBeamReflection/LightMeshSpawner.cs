using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMeshSpawner : MonoBehaviour
{
    public GameObject lightMeshPrefab;
    public static LightMeshSpawner Instance;
    private List<Vector3> _locallyStoredPoints;
    private List<List<GameObject>> _lightMeshes;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;

        _locallyStoredPoints = new List<Vector3>();
        _lightMeshes = new List<List<GameObject>>();
    }

    public void SetLightBeamPoints(LineRenderer _laser)
    {
        // If no beam exists, create one
        if (_locallyStoredPoints.Count != _laser.positionCount && _lightMeshes.Count == 0)
        {
            DestroyLightBeam();
            ClearAndUpdatePointList(_laser);
            CreateLightMeshes(0, _laser);
            Debug.Log("Beam regenerated.");
        }
        // If new beam is shorter than existing one, delete tail segment(s)
        else if (_locallyStoredPoints.Count > _laser.positionCount)
        {
            DeleteTail(_laser.positionCount-1);
            Debug.Log("Tail deleted.");
        }
        // If new beam is longer than existing one, add new segment(s)
        else if (_locallyStoredPoints.Count < _laser.positionCount)
        {
            int createFrom = _locallyStoredPoints.Count - 1;
            CreateLightMeshes(createFrom, _laser);
            Debug.Log("Tail segments added.");
        }
        // If a reflection point changes, correct alignment for subsequent meshes
        for (int i = 0; i < _laser.positionCount; i++)
        { 
            if (!_locallyStoredPoints[i].Equals(_laser.GetPosition(i))) 
            { 
                int updateMeshesFrom = i-1; 
                ClearAndUpdatePointList(_laser); 
                UpdateLightMeshes(updateMeshesFrom);
                Debug.Log("Beam Updated from Mesh: " + updateMeshesFrom);
                return;
            }
        }
    }

    
    private void UpdateLightMeshes(int from)
    {
        for (int i = from; i < _lightMeshes.Count; i++)
        {
            Vector3 dir = _locallyStoredPoints[i + 1] - _locallyStoredPoints[i];
            float length = dir.magnitude;
            if (_lightMeshes[i].Count <= Mathf.FloorToInt(length))
            {
                // If the existing mesh instances don't span the whole length of the beam segment, first align existing ones
                for (float j = 0.0f; j < _lightMeshes[i].Count; j += 1.0f)
                {
                    Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
                    GameObject lightObj = _lightMeshes[i][(int)Math.Floor(j)];
                    lightObj.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(dir, Vector3.up));
                    float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                    lightObj.GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
                }
                
                // Then create new meshes for the rest of the beam segment
                for(float j = _lightMeshes[i].Count; j < length; j+=1.0f) 
                {
                    Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
                    _lightMeshes[i].Add(Instantiate(lightMeshPrefab, pos, Quaternion.LookRotation(dir, Vector3.up)));
                    float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                    _lightMeshes[i][_lightMeshes[i].Count-1].GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
                }
            }
            else
            {
                // If there are too many existing mesh instances, first align the ones which are inside the length of the segment
                for(float j = 0; j < length; j+=1.0f) 
                {
                    Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
                    GameObject lightObj = _lightMeshes[i][(int)Math.Floor(j)];
                    lightObj.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(dir, Vector3.up));
                    float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                    lightObj.GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
                }
                
                // Then delete the unnecessary ones
                for(int j = _lightMeshes[i].Count-1; j > (int)length; j--) 
                {
                    Destroy(_lightMeshes[i][j]);
                    _lightMeshes[i].RemoveAt(j);
                }
            }
        }
    }

    private void DeleteTail(int from)
    {
        // Delete mesh segments from a given start point
        for (int i = _lightMeshes.Count-1; i >= from; i--)
        {
            for (int j = _lightMeshes[i].Count-1; j >= 0; j--)
            {
                Destroy(_lightMeshes[i][j]);
            }
            _lightMeshes.RemoveAt(i);
        }
    }


    private void CreateLightMeshes(int from, LineRenderer _laser)
    {
        // Create mesh segments from a given start point
        for (int i = from; i < _laser.positionCount - 1; i++)
        {
            _lightMeshes.Add(new List<GameObject>());
            Vector3 dir = _laser.GetPosition(i + 1) - _laser.GetPosition(i);
            float length = dir.magnitude;
            for (float j = 0.0f; j < length; j+=1.0f)
            {
                Vector3 pos = _laser.GetPosition(i) + dir * (j / length);
                _lightMeshes[i].Add(Instantiate(lightMeshPrefab, pos, Quaternion.LookRotation(dir, Vector3.up)));
                float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                _lightMeshes[i][_lightMeshes[i].Count-1].GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
            }
        }
    }

    public void DestroyLightBeam()
    {
        _locallyStoredPoints.Clear();
        foreach (List<GameObject> l in _lightMeshes)
        {
            foreach (GameObject o in l)
            {
                Destroy(o);
            }
        }
        _lightMeshes.Clear();
    }

    private void ClearAndUpdatePointList(LineRenderer _laser)
    {
        _locallyStoredPoints.Clear();
        for (int i = 0; i < _laser.positionCount; i++)
        {
            _locallyStoredPoints.Add(_laser.GetPosition(i));
        }
    }
}
