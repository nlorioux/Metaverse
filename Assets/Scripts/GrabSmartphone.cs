using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabSmartphone : MonoBehaviour
{

    public bool hasSelected;
    public GameObject lastSelectedObject;
    public Color lastColor;

    private Touch theTouch;
    private Transform cameraTransform;
    public float pickUpDistance = 20f;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private GameObject grabButton;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        hasSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (hasSelected && (lastSelectedObject.transform.position - transform.position).magnitude > 4f)
            {
                lastSelectedObject.GetComponent<Renderer>().material.color = lastColor;
                hasSelected = false;
                grabButton.SetActive(false);
            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(theTouch.position), out RaycastHit hit, pickUpDistance, pickUpLayerMask) && hasSelected)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject == lastSelectedObject)
                {
                    lastSelectedObject.GetComponent<Renderer>().material.color = lastColor;
                    hasSelected = false;
                    grabButton.SetActive(false);
                }

            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(theTouch.position), out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask) && !hasSelected)
            {
                lastColor = raycastHit.collider.gameObject.GetComponent<Renderer>().material.color;
                raycastHit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                lastSelectedObject = raycastHit.collider.gameObject;
                hasSelected = true;
                grabButton.SetActive(true);
            }
        }
    }
}