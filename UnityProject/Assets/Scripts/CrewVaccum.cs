using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrewVaccum : MonoBehaviour
{
	[SerializeField] CrewManager crew;

	Collider col;
	private void Awake() {
		col = GetComponent<Collider>();
	}

	private void OnTriggerEnter(Collider other) {
		Person person = other.attachedRigidbody?.GetComponent<Person>();
		if (person && person.crewManager != crew) {
			person.KillSelf();
			crew.AddPersonToCrew(person);
			person.transform.SetParent(crew.transform);
			people.Add(person);
			StartCoroutine(Magnet(person, other.attachedRigidbody));
		}
	}

	WaitForFixedUpdate wffu = new WaitForFixedUpdate();
	IEnumerator Magnet(Person person, Rigidbody rb) {
		rb.useGravity = false;
		while (person && rb && enabled) {
			rb.AddForce((transform.position - rb.position).normalized * 100f, ForceMode.Acceleration);
			yield return wffu;
		}
		if (rb) {
			rb.velocity = Vector3.zero;
			rb.useGravity = true;
		}
	}

	public List<Person> people = new List<Person>();
	public void Enable(bool val) {
		if (enabled != val) {
			col.enabled = val;
			enabled = val;
			people.Clear();
		}
	}
}
