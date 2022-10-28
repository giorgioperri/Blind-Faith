using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerInputErrorWorkaround : MonoBehaviour
{
    private PlayerInput _input;
   
    private void Start()
    {
        _input = GetComponent<PlayerInput>();
    }
 
    private void OnDisable()
    {
        _input.actions = null;
    }
}
