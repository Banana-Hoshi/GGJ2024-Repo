using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SteamBoat : MonoBehaviour
{
	[SerializeField] Vector2 tiltSpeed = Vector2.one;
	[SerializeField] Transform crane = null;
	[SerializeField] float maxVelo = 10f;
	[SerializeField] PaddleWheel[] wheels;

	Rigidbody rb;
	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	Vector2 tiltInput = Vector2.zero;
	public void TiltDir(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Performed)
			tiltInput = ctx.ReadValue<Vector2>();
		else if (ctx.phase == InputActionPhase.Canceled)
			tiltInput = Vector2.zero;
	}

	Vector2 cranePos = Vector2.zero;
	public void CraneDir(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Performed)
			cranePos = ctx.ReadValue<Vector2>();
		else if (ctx.phase == InputActionPhase.Canceled)
			cranePos = Vector2.zero;
	}

	private void FixedUpdate() {
		// Do tilt here
		rb.AddRelativeTorque(tiltInput.y * tiltSpeed.y, 0f, -tiltInput.x * tiltSpeed.x);

		// Move Crane to position
		crane.localPosition = Vector3.MoveTowards(crane.localPosition, new Vector3(cranePos.x * 2f, 0f, cranePos.y * 2f), Time.fixedDeltaTime * 4f);

		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelo);
	}

	public void SetInWater(bool val) {
		foreach (PaddleWheel paddle in wheels) {
			paddle.inWater = val;
		}
	}
}
