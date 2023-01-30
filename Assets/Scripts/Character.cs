using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Camera;
    public float walkSpeed = 10.0f;
    public float rotationSpeed = 180.0f;

    private void Start()
    {
        //Camera = GameObject.FindGameObjectWithTag("MainCamera");
        //Camera.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Controlling character's movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * walkSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * walkSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * walkSpeed);
        }
        //Controlling charater's rotation
        //Camera.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
        */
    }
}
