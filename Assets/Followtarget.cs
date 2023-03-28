using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followtarget : MonoBehaviour
{
    public Transform target;
    private bool FirstPerson = false ;

    // Update is called once per frame
    void Update()
    {   if (FirstPerson== false)
        {
            transform.position = target.position - new Vector3(0f, -1.65f, 1.5f);
        }
        else
        {
            transform.position = target.position + new Vector3(0f, 1.6f, 0.14f);
        }
      
    }

    public void First()
    {
        FirstPerson = true;
    }
    public void Third()
    {
        FirstPerson = false;
    }
}
