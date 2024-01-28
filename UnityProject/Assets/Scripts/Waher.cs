using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waher : MonoBehaviour
{
	[SerializeField] float f = 5f;
	[SerializeField] float t = 0.1f;
	private void OnTriggerStay(Collider other) {
		if (other.attachedRigidbody) {
			Floaty boat = other.attachedRigidbody.GetComponent<Floaty>();
			if (boat && boat.enabled)
				boat.Bump(f, t);
		}
	}

	private void OnTriggerEnter(Collider other) {
		SteamBoat boat = other.attachedRigidbody?.GetComponent<SteamBoat>();
		if (boat && boat.afloat) {
			boat.SetInWater(true);
		}
	}

	private void OnTriggerExit(Collider other) {
		SteamBoat boat = other.attachedRigidbody?.GetComponent<SteamBoat>();
		if (boat && boat.afloat) {
			boat.SetInWater(false);
		}
	}
}
