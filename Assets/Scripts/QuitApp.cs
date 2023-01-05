using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitApplication()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
