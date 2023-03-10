using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform objectToFollow;
    public float speed = 5f;
    public Vector3 offset;

    private void Update()
    {
        // Check if the object to follow is set
        if (objectToFollow != null)
        {
            // Calculate the direction to the target object
            Vector3 direction = objectToFollow.position - transform.position;

            // Move towards the target object at a constant speed
            transform.position += direction.normalized * speed * Time.deltaTime ;
        }
    }
}
