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
        if (_locallyStoredPoints.Count != _laser.positionCount && _lightMeshes.Count == 0)
        {
            DestroyLightBeam();
            for (int i = 0; i < _laser.positionCount; i++)
            {
                _locallyStoredPoints.Add(_laser.GetPosition(i));
            }

            CreateLightMeshes(0);
            Debug.Log("Beam regenerated.");
        }
        // If new beam is shorter than existing one, delete tail segment(s)
        else if (_locallyStoredPoints.Count > _laser.positionCount)
        {
            ClearAndUpdatePointList(_laser);
            DeleteTail(_laser.positionCount-1);
            UpdateLightMeshes(_locallyStoredPoints.Count-1);
            Debug.Log("Tail deleted.");
        }
        // If new beam is longer than existing one, add new segment(s)
        else if (_locallyStoredPoints.Count < _laser.positionCount)
        {
            int createFrom = _locallyStoredPoints.Count - 2;
            DeleteTail(createFrom);
            ClearAndUpdatePointList(_laser);
            CreateLightMeshes(createFrom);
            Debug.Log("Tail segments added.");
        }
        // If reflection points remain the same but the path changes from some point
        else if (_locallyStoredPoints.Count == _laser.positionCount)
        {
            for (int i = 0; i < _laser.positionCount; i++)
            { 
                if (!_locallyStoredPoints[i].Equals(_laser.GetPosition(i)))
                {
                    Debug.Log("Beam Updated!");
                    int updateMeshesFrom = i-1;
                    ClearAndUpdatePointList(_laser);
                    UpdateLightMeshes(updateMeshesFrom);
                    //Debug.Log("Beam updated.");
                    return;
                }
            }
        }
        
    }

    
    private void UpdateLightMeshes(int from)
    {
        for (int i = from; i < _lightMeshes.Count; i++)
        {
            //Debug.Log(from);
            //Debug.Log("Meshes Count: " + _lightMeshes.Count);
            //Debug.Log("Points Count: " + _locallyStoredPoints.Count);
            Vector3 dir = _locallyStoredPoints[i + 1] - _locallyStoredPoints[i];
            float length = dir.magnitude;
            if (_lightMeshes[i].Count <= Mathf.FloorToInt(length))
            {
                for (float j = 0.0f; j < _lightMeshes[i].Count; j += 1.0f)
                {
                    Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
                    GameObject lightObj = _lightMeshes[i][(int)Math.Floor(j)];
                    lightObj.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(dir, Vector3.up));
                    float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                    lightObj.GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
                }
                
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
                for(float j = 0; j < length; j+=1.0f) 
                {
                    Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
                    GameObject lightObj = _lightMeshes[i][(int)Math.Floor(j)];
                    lightObj.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(dir, Vector3.up));
                    float cutoff = length - j < 1.0f ? 1 - (length - j) : 0.0f;
                    lightObj.GetComponent<MeshRenderer>().material.SetFloat("_alphaCutoff", cutoff);
                }
                
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
        for (int i = _lightMeshes.Count-1; i >= from; i--)
        {
            for (int j = _lightMeshes[i].Count-1; j >= 0; j--)
            {
                Destroy(_lightMeshes[i][j]);
            }
            _lightMeshes.RemoveAt(i);
        }
    }


    private void CreateLightMeshes(int from)
    {
        for (int i = from; i < _locallyStoredPoints.Count - 1; i++)
        {
            _lightMeshes.Add(new List<GameObject>());
            Vector3 dir = _locallyStoredPoints[i + 1] - _locallyStoredPoints[i];
            float length = dir.magnitude;
            //int meshesCount = Mathf.FloorToInt(length);
            for (float j = 0.0f; j < length; j+=1.0f)
            {
                Vector3 pos = _locallyStoredPoints[i] + dir * (j / length);
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
