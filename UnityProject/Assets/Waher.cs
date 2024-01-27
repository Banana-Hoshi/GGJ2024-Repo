using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waher : MonoBehaviour
{
	[SerializeField] float f = 5f;
	private void OnTriggerStay(Collider other) {
		other.attachedRigidbody.AddForce(Vector2.up * f, ForceMode.Acceleration);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.attachedRigidbody.GetComponent<SteamBoat>()) {
			other.attachedRigidbody.GetComponent<SteamBoat>().SetInWater(true);
			
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.attachedRigidbody.GetComponent<SteamBoat>()) {
			other.attachedRigidbody.GetComponent<SteamBoat>().SetInWater(false);
			
		}
	}
}
