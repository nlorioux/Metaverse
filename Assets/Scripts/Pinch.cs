using System.Collections.Generic;
using UnityEngine;

public class Pinch : MonoBehaviour {

	public Layer layer = Layer.OBJECT;

	protected HandUtils hand;
	protected HashSet<GameObject> contacts;
	protected Dictionary<GameObject, Transform> grabbed;
	protected bool isGrabbing;

    public HandUtils Hand
    {
        get
        {
            return hand;
        }
    }

    protected void Awake() {
		contacts = new HashSet<GameObject>();
		grabbed = new Dictionary<GameObject, Transform>();

		hand = GetComponentInParent<HandUtils>();

		isGrabbing = false;
	}

	protected void Update() {
		if(! isGrabbing && hand.Pinch) {
			Grab();
		}
		else if(isGrabbing && ! hand.Pinch) {
			Release();
		}
	}

	protected void OnTriggerEnter(Collider other) {
		if(other.gameObject.layer == (int) layer && ! contacts.Contains(other.gameObject)) {
			contacts.Add(other.gameObject);
		}
	}

	protected void OnTriggerExit(Collider other) {
		if(contacts.Contains(other.gameObject)) {
			contacts.Remove(other.gameObject);
		}
	}

	protected void Grab() {
		foreach(GameObject contact in contacts) {
			if(contact != null) {
				contact.transform.SetParent(transform);
				contact.GetComponent<Rigidbody>().isKinematic = true;
				grabbed.Add(contact, contact.transform); 
			}
			else {
				grabbed.Remove(contact);
			}
		}

		isGrabbing = true;
	}

	protected void Release() {
		foreach(var pair in grabbed) {
			if(pair.Key != null) {
				pair.Key.transform.parent = null;
				pair.Key.transform.GetComponent<Rigidbody>().isKinematic = false;
			}
		}

		grabbed.Clear();

		isGrabbing = false;
	}
}
