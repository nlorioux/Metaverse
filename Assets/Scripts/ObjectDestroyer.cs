using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] GameObject objectToDestroyChild;
    // Start is called before the first frame update
    void Start()
    {
        Transform childTransform = gameObject.transform.Find("HolderToDestroy");
        if (childTransform != null)
        {
            Debug.Log("Destroying");
            GameObject childObject = childTransform.gameObject;
            Destroy(childObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
