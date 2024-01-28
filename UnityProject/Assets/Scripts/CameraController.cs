using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 1f, -5f);
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float verticalOffset = 15f;
    private float horizontalOffset = 0f;
    private float horizontalOffsetTarget = 0f;
	int turning = 0;

	private void Start() {
		transform.SetParent(null);
	}

    private void Update()
    {
		if (!targetTrans) {
			Destroy(gameObject);
			return;
		}

		if (horizontalOffset != horizontalOffsetTarget) {
			horizontalOffset = Mathf.MoveTowards(horizontalOffset, horizontalOffsetTarget, 60f * Time.deltaTime);
		}

        Quaternion yRot = Quaternion.Euler(0f, targetTrans.eulerAngles.y + horizontalOffset, 0f);
        transform.position = targetTrans.position + yRot * cameraOffset;
        transform.rotation = Quaternion.Euler(verticalOffset, targetTrans.eulerAngles.y + horizontalOffset, 0f);
    }

	public void TiltCameraRight(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Started) {
			if (horizontalOffsetTarget <= 0f && (turning & 1) == 0)
				horizontalOffsetTarget += 30f;
				turning = turning | 1;
		}
		else if (ctx.phase == InputActionPhase.Canceled) {
			horizontalOffsetTarget -= 30f;
				turning = turning & 2;
		}
	}

	public void TiltCameraLeft(InputAction.CallbackContext ctx) {
		if (ctx.phase == InputActionPhase.Started) {
			if (horizontalOffsetTarget >= 0f && (turning & 2) == 0)
				horizontalOffsetTarget -= 30f;
				turning = turning | 2;
		}
		else if (ctx.phase == InputActionPhase.Canceled) {
			horizontalOffsetTarget += 30f;
			turning = turning & 1;
		}
	}
}
