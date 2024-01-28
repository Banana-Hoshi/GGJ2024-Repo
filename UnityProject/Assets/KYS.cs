using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.attachedRigidbody)
			Destroy(other.attachedRigidbody.gameObject);
	}
}
