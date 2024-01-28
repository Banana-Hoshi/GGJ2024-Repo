using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waher : MonoBehaviour
{
	[SerializeField] float f = 5f;
	private void OnTriggerStay(Collider other) {
		if (other.attachedRigidbody) {
			var boat = other.attachedRigidbody.GetComponent<SteamBoat>();
			if (!boat || !boat.afloat)
				return;
			
			boat.Bump(f);
		}
	}

	private void OnTriggerEnter(Collider other) {
		var boat = other.attachedRigidbody?.GetComponent<SteamBoat>();
		if (boat && boat.afloat) {
			boat.SetInWater(true);
		}
	}

	private void OnTriggerExit(Collider other) {
		var boat = other.attachedRigidbody?.GetComponent<SteamBoat>();
		if (boat && boat.afloat) {
			boat.SetInWater(false);
		}
	}
}
