using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 1f, -5f);
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float verticalOffset = 15f;

	private void Start() {
		transform.SetParent(null);
	}

    private void Update()
    {
        Quaternion yRot = Quaternion.Euler(0f, targetTrans.eulerAngles.y, 0f);
        transform.position = targetTrans.position + yRot * cameraOffset;
        transform.rotation = Quaternion.Euler(verticalOffset, targetTrans.eulerAngles.y + horizontalOffset, 0f);
    }
}
