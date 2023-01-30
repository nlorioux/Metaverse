using UnityEngine;

[RequireComponent(typeof(Pinch))]
public class VoodooHand : MonoBehaviour {

	protected Pinch pinch;

	public Pinch Pinch {
		get {
			return pinch;
		}
	}

	protected void Awake() {
		pinch = GetComponent<Pinch>();
	}

	protected void OnEnable() {
		VoodooDoll manager = FindObjectOfType<VoodooDoll>();

		if(manager != null) {
			manager.SetHand(this);
		}
	}

	protected void OnDisable() {
		VoodooDoll manager = FindObjectOfType<VoodooDoll>();

		if(manager != null) {
			manager.RemoveHand(this);
		}
	}
}
