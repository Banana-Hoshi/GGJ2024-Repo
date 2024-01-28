using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleWheel : MonoBehaviour
{
	[SerializeField] float acceleration = 5f;
	[SerializeField] float rotSpeed = 360f;
	public bool inWater = false;

	Rigidbody rb;

	private void Awake() {
		enabled = false;
		rb = transform.parent.GetComponent<Rigidbody>();
	}

	public void SpinWheel(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Started)
			Spin();
		else if (ctx.phase == InputActionPhase.Canceled)
			StopSpin();
	}


	public void Spin() {
		enabled = true;
	}

    public void StopSpin() {
		enabled = false;
	}

	private void FixedUpdate() {
		transform.localRotation *= Quaternion.Euler(rotSpeed * Time.fixedDeltaTime, 0f, 0f);
		// If in water
		if (inWater)
			rb.AddForceAtPosition(rb.transform.forward * acceleration, transform.position + (rb.transform.rotation * rb.centerOfMass), ForceMode.Acceleration);
	}
}
