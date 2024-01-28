using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamBoat))]
public class AIBoat : MonoBehaviour
{
	public Transform target = null;
	[SerializeField] PaddleWheel leftPaddle;
	[SerializeField] PaddleWheel rightPaddle;
	[SerializeField] float leway = 15f;
	[SerializeField] private Vector2 minAngles;
	SteamBoat boat;

	private void Awake() {
		boat = GetComponent<SteamBoat>();
	}

	private void FixedUpdate() {
		if (!boat.afloat)
			return;
		
		if (target) {
			// Steer towards target
			var angle = Vector3.SignedAngle(transform.forward, target.transform.position - transform.position, Vector3.up);
			if (angle > -leway)
				rightPaddle.Spin();
			else
				rightPaddle.StopSpin();
			
			if (angle < leway)
				leftPaddle.Spin();
			else
				leftPaddle.StopSpin();
		}


		Vector3 currentRot = transform.rotation.eulerAngles;
		if (currentRot.x > 180f)
			currentRot.x -= 360f;

		if (currentRot.z > 180f)
			currentRot.z -= 360f;
			
		if (Mathf.Abs(currentRot.x) > minAngles.x)
			boat.tiltInput.y = -Mathf.Sign(currentRot.x);
		else
			boat.tiltInput.y = 0f;

		if (Mathf.Abs(currentRot.z) > minAngles.y)
			boat.tiltInput.x = Mathf.Sign(currentRot.z);
		else
			boat.tiltInput.x = 0f;
	}
}
