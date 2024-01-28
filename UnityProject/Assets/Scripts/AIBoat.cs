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
	}
}
