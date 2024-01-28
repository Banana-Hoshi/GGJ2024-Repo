using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonLock : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.attachedRigidbody && other.attachedRigidbody.GetComponent<Person>() &&
				other.attachedRigidbody.GetComponent<Person>().crewManager == null) {
			other.attachedRigidbody.isKinematic = true;
			other.attachedRigidbody.transform.SetParent(transform);
		}
	}
}
