using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float rotationSpeed = 180.0f;

    // Update is called once per frame
    void Update()
    {
        //Controlling character's movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * walkSpeed);
        }
        //Controlling charater's rotation
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
    }
}
