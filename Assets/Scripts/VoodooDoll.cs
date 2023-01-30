using UnityEngine;

public class VoodooDoll : MonoBehaviour {

    [Range(0, 10)]
    public float pinchThresholdFactor = 2.0f;

    protected VoodooHand leftHand;
	protected VoodooHand rightHand;
	protected GameObject doll;
	protected GameObject controlled;
	protected Vector3 offset;
	protected bool isGrabbing;

	protected void Awake() {
		leftHand = null;
		rightHand = null;
		doll = null;
		controlled = null;
		offset = Vector3.zero;
		isGrabbing = false;
	}

	protected void Update() {
		if(leftHand != null && rightHand != null) {
            HandUtils left = leftHand.Pinch.Hand;
            HandUtils right = rightHand.Pinch.Hand;

            if (! isGrabbing && left.Pinch && right.Pointed != null) {
				Grab(right.Pointed);
			}
			else if(isGrabbing && ! left.Pinch) {
				Release();
			}
		}
		else if(isGrabbing) {
			Release();
		}

		if(isGrabbing && doll != null && controlled != null) {
			Move();
		}
	}

	public void SetHand(VoodooHand hand) {
		if(gameObject.activeInHierarchy && enabled) {
			switch(hand.Pinch.Hand.hand) {
				case HandUtils.Hand.LEFT:
					if(leftHand == null) {
						leftHand = hand;

						leftHand.Pinch.Hand.pinchThreshold *= pinchThresholdFactor;
						leftHand.Pinch.enabled = false;
					}

					break;
				case HandUtils.Hand.RIGHT:
					if(rightHand == null) {
						rightHand = hand;

						rightHand.Pinch.Hand.showRay = true;
						rightHand.Pinch.layer = Layer.VOODOO_DOLL;
					}

					break;
			}
		}
	}

	public void RemoveHand(VoodooHand hand) {
		if(gameObject.activeInHierarchy && enabled) {
			switch(hand.Pinch.Hand.hand) {
				case HandUtils.Hand.LEFT:
					if(leftHand == hand) {
						leftHand.Pinch.enabled = true;
						leftHand.Pinch.Hand.pinchThreshold /= pinchThresholdFactor;

						leftHand = null;
					}

					break;
				case HandUtils.Hand.RIGHT:
					if(rightHand == hand) {
						rightHand.Pinch.layer = Layer.OBJECT;
						rightHand.Pinch.Hand.showRay = false;

						rightHand = null;
					}

					break;
			}
		}
	}

	protected void Grab(GameObject pointed) {
		//TODO
		
		doll = Instantiate(pointed);
		doll.transform.SetParent(rightHand.transform);
		doll.GetComponent<Rigidbody>().isKinematic = true;
		rightHand.Pinch.Hand.showRay = false;

		isGrabbing = true;
	}

	protected void Release() {
        Vector3 p = transform.InverseTransformPoint(controlled.transform.position);
        controlled.transform.position = transform.TransformPoint(Vector3.Min(Vector3.Max(p, new Vector3(-8, 0, -8)), new Vector3(8, 10, 8)));

		//TODO

		if(rightHand != null) {
			rightHand.Pinch.Hand.showRay = true;
		}

		isGrabbing = false;
	}

	protected void Move() {
		//TODO
	}
}
