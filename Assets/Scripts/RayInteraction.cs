using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RayInteraction : MonoBehaviour {
    public enum Hand
    {
        LEFT,
        RIGHT
    }

    public Hand hand;
    [Range(0.1f, 10.0f)]
    public float maxDistance = 5f;
    [Range(10, 360)]
    public float rotationSpeed = 120f; // In degrees per second,

    protected List<InputDevice> controllers;
    protected GameObject ray;
    protected GameObject pivot;
	protected GameObject selected;
	protected Transform startParent;
	protected float startDistance;
    protected float distanceWall;

	protected void Start() {
        controllers = new List<InputDevice>();

        ray = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        ray.transform.parent = transform;
        ray.transform.localPosition = 0.5f * maxDistance * Vector3.forward;
        ray.transform.localRotation = Quaternion.Euler(90, 0, 0);
        ray.transform.localScale = new Vector3(0.002f, 0.5f * maxDistance, 0.002f);
        ray.name = "Ray";
        Destroy(ray.GetComponent<Collider>());

        pivot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pivot.transform.localScale = 0.005f * Vector3.one;
        pivot.transform.SetParent(transform, true);
        pivot.name = "Pivot";
		pivot.SetActive(false);
	}

    protected void OnEnable()
    {
        ray.SetActive(true);
    }
    

    protected void OnDisable()
    {
        Release();

        ray.SetActive(false);
    }

    protected void Update () {
        InputDeviceCharacteristics hand_char = hand == Hand.LEFT ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right;
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | hand_char, controllers);

        float trigger = 0;

        if (controllers.Count > 0)
        {
            if (!controllers[0].TryGetFeatureValue(CommonUsages.trigger, out trigger))
            {
                trigger = 0;
            }
        }

        // Get ray
        Ray ray = new Ray(transform.position, transform.forward );

		RaycastHit hit;

		// Detect collision between ray and objects
		if(Physics.Raycast( ray, out  hit, maxDistance,  1 << 9)) { //TODO
           

			// Select object
			if(selected == null && trigger > 0.5) {
				Grab(hit);
			}
		}

		// Release object 
		if(selected != null && trigger <= 0.5) {
			Release();
		}

		// Interact with selected object
		if(selected != null) {
			Move(ray);
		}
	}

	protected void Grab(RaycastHit hit) {
        //TODO
        selected = hit.collider.gameObject;
        startDistance = Vector3.Distance(transform.position, selected.transform.position);
        selected.transform.SetParent(transform);
        selected.GetComponent<Rigidbody>().isKinematic = true;
        // Highlight selected object
        Highlighter highlighter = hit.transform.GetComponent<Highlighter>();

		if(highlighter != null) {
			highlighter.disableAfterDuration = false;
			highlighter.highlight = true;
		}
	}

	protected void Release() {
        if (selected != null)
        {
            // Keep object inside the scene box. Abort release if object outside.

            if (true) //TODO
            {
                // Disable highlight for released object
                Highlighter highlighter = selected.GetComponent<Highlighter>();

                if (highlighter != null)
                {
                    highlighter.highlight = false;
                }

                selected.transform.parent = null;
                selected.GetComponent<Rigidbody>().isKinematic = false;
                // Clear selected variable
                selected = null;
            }
        }
	}

	protected void Move(Ray ray) {
        /*
		 * ==================================================
		 * Keep object inside the scene box
		 * ==================================================
		 */


        // Get pivot distance on the ray in order to keep the object above the ground and inside the room
        RaycastHit hitWall;
        if ( Physics.Raycast(ray, out hitWall, maxDistance, 1 << 8))
        {
            distanceWall = hitWall.distance; 
            selected.transform.position = transform.position + transform.forward * Mathf.Min(distanceWall, startDistance);
        }

        // Set pivot position
        //TODO
 /*       RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject, 1 << 9))
        {
            pivot.transform.position = hitObject.barycentricCoordinate; 
        }*/

        /*
         * ==================================================
         * Rotate object
         * ==================================================
         */

        Vector2 joystick = Vector2.zero;

        if (controllers.Count > 0)
        {
            if (!controllers[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick))
            {
                joystick = Vector2.zero;
            }
        }

            
            // Get angle offset since last frame depending of joystick value
        //TODO

        // Rotate selected object around pivot point
        //TODO
    }
}
