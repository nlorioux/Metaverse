using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent;
        //transform.parent = null;
    }

    void LateUpdate()
    {
        parentTransform.position =  Vector3.zero;
        parentTransform.rotation = Quaternion.identity;

        parentTransform.position = transform.position;
        parentTransform.rotation = transform.rotation;
    }
}
