using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithTaregt : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    void Update()
    {
        
        Debug.Log("Horizontal" + _joystick.Horizontal);
       // Debug.Log("Vertical " + _joystick.Vertical);

        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + _joystick.Horizontal , transform.rotation.z)); 
        
    }
}
