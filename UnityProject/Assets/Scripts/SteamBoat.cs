using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class SteamBoat : MonoBehaviour
{
	[SerializeField] Vector2 tiltSpeed = Vector2.one;
	[SerializeField] Vector2 craneDistance = new Vector2(25, 50);
	[SerializeField] float maxVelo = 10f;
	[SerializeField] PaddleWheel[] wheels;
	[SerializeField] CrewVaccum vaccum;
	[SerializeField] float deathAngle = 90f;
	[HideInInspector] public bool afloat = true;

	Rigidbody rb;
	CrewManager crew;
	private void Awake() {
		rb = GetComponent<Rigidbody>();
		crew = GetComponent<CrewManager>();
	}

	private void Start() {
		vaccum?.Enable(false);
		if (crew) {
			for (int i = 0; i < 100; ++i) {
				crew.SpawnPerson(PutPerson(), transform.rotation);
			}
		}
	}

	[HideInInspector]
	public Vector2 tiltInput = Vector2.zero;
	public void TiltDir(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Performed)
			tiltInput = ctx.ReadValue<Vector2>();
		else if (ctx.phase == InputActionPhase.Canceled)
			tiltInput = Vector2.zero;
	}

	Vector2 cranePos = Vector2.zero;
	public void CraneDir(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Performed) {
			cranePos = ctx.ReadValue<Vector2>() * craneDistance;
			vaccum.Enable(true);
		}
		else if (ctx.phase == InputActionPhase.Canceled) {
			cranePos = Vector2.zero;
			// Get people
			foreach (Person person in vaccum.people) {
				if (person) {
					person.transform.position = PutPerson();
					person.transform.rotation = transform.rotation;
				}
			}

			vaccum.Enable(false);
		}
	}

	private void FixedUpdate() {
		if (!afloat)
			return;

		if (deathAngle < 0f)
			return;
		
		// If flipping over
		if (Vector3.Angle(transform.up, Vector3.up) > deathAngle) {
			vaccum?.Enable(false);
			if (crew && crew.GetCrewCount() > 0f) {
				// Remove all crew and flip the boat back
				crew.KillCrew();
				StartCoroutine(FlipBoat());
				return;
			}

			afloat = false;
			GetComponent<Floaty>().enabled = false;
			foreach (PaddleWheel paddle in wheels) {
				paddle.inWater = false;
			}
			return;
		}
		
		// Do tilt here
		rb.AddRelativeTorque(tiltInput.y * tiltSpeed.y, 0f, -tiltInput.x * tiltSpeed.x);

		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelo);
	}

	private void Update() {
		// Move Crane to position
		if (afloat && vaccum)
			vaccum.transform.localPosition = Vector3.MoveTowards(vaccum.transform.localPosition,
					new Vector3(cranePos.x * 2f, 0f, cranePos.y * 2f), Time.fixedDeltaTime * 25f);
	}

	public void SetInWater(bool val) {
		foreach (PaddleWheel paddle in wheels) {
			paddle.inWater = val;
		}
	}

	Vector3 PutPerson() {
		return transform.position + transform.up;
	}

	WaitForFixedUpdate wffu = new WaitForFixedUpdate();
	IEnumerator FlipBoat() {
		float tempAngle = deathAngle;
		deathAngle = -1f;
		
		Vector3 euler = transform.eulerAngles;
		float ztarget = 360f;
		if (euler.x > 180f)
			euler.x -= 360f;
		if (euler.z > 180f)
			ztarget = 0f;

		while (transform.up.y < 0.95f) {
			euler.x = Mathf.MoveTowards(euler.x, 0f, 90f * Time.fixedDeltaTime);
			euler.z = Mathf.MoveTowards(euler.z, ztarget, 90f * Time.fixedDeltaTime);
			transform.rotation = Quaternion.Euler(euler);
			yield return wffu;
		}

		deathAngle = tempAngle;
	}
}
