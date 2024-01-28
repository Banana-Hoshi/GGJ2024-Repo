using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterHeightCheck : MonoBehaviour
{
	public void Bump(Rigidbody body, float force) {
		body.AddForceAtPosition(Vector3.up * force, transform.position, ForceMode.Force);
	}
}
