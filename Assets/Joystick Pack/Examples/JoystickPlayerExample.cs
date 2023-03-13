using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick Joystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {    Vector3 direction = Vector3.forward * Joystick.Vertical + Vector3.right * Joystick.Horizontal; 
        if (Joystick.Vertical == 0 && Joystick.Horizontal == 0)
        {
            Debug.Log("it stops"); 
        }
        else
        {
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            Debug.Log("moves"); 
        }
      
       
    }
}