using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneArm : MonoBehaviour
{
	[SerializeField] Transform arm;
	[SerializeField] Transform end;
	[SerializeField] Transform target;

	float oneOverLength;
	private void Start() {
		oneOverLength = 1f / end.localPosition.z;
	}
	
	private void LateUpdate() {
		Vector3 diff = target.position - transform.position;
		arm.localScale = new Vector3(1f, 1f, diff.magnitude * oneOverLength);

		end.localPosition = Vector3.forward * diff.magnitude;

		transform.localRotation = Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, diff, transform.up), 0f);
	}
}
